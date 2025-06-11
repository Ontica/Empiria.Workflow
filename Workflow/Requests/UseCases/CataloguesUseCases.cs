/* Empiria Workflow ******************************************************************************************
*                                                                                                            *
*  Module   : Requests Management                        Component : Use cases Layer                         *
*  Assembly : Empiria.Workflow.dll                       Pattern   : Use case interactor class               *
*  Type     : RequestsCataloguesUseCases                 License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Use cases that returns catalogues information for requests.                                    *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;
using System.Collections.Generic;

using Empiria.Parties;
using Empiria.Services;
using Empiria.StateEnums;

namespace Empiria.Workflow.Requests.UseCases {

  /// <summary>Use cases taht returns catalogues information for requests.</summary>
  public class CataloguesUseCases : UseCase {

    #region Constructors and parsers

    protected CataloguesUseCases() {
      // no-op
    }

    static public CataloguesUseCases UseCaseInteractor() {
      return CreateInstance<CataloguesUseCases>();
    }

    #endregion Constructors and parsers

    #region Use cases

    public FixedList<NamedEntityDto> GetOrganizationalUnitsPlayingRole(string role) {
      Assertion.Require(role, nameof(role));

      FixedList<OrganizationalUnit> list = Party.GetList<OrganizationalUnit>(DateTime.Today)
                                                .FindAll(x => x.PlaysRole(role));

      list.Sort((x, y) => x.Code.CompareTo(y.Code));

      if (ExecutionServer.CurrentPrincipal.IsInRole("budget-manager") ||
          ExecutionServer.CurrentPrincipal.IsInRole("budget-authorizer") ||
          ExecutionServer.CurrentPrincipal.IsInRole("cash-flow-authorizer") ||
          ExecutionServer.CurrentPrincipal.IsInRole("cash-flow-manager")) {

        return list.Select(x => new NamedEntityDto(x.UID, x.FullName))
                   .ToFixedList();

      }

      var party = Party.ParseWithContact(ExecutionServer.CurrentContact);

      if (role == "budgeting" && ExecutionServer.CurrentPrincipal.IsInRole("acquisition-manager")) {
        return list.FindAll(x => x.Id == ExecutionServer.CurrentContact.Organization.Id)
                   .Select(x => new NamedEntityDto(x.UID, x.FullName))
                   .ToFixedList();
      }
      if (role == "cash-flow" && (ExecutionServer.CurrentPrincipal.IsInRole("financial-project-manager") ||
                                  ExecutionServer.CurrentPrincipal.IsInRole("cash-flow-projector"))) {
        return list.FindAll(x => x.Id == ExecutionServer.CurrentContact.Organization.Id)
                   .Select(x => new NamedEntityDto(x.UID, x.FullName))
                   .ToFixedList();
      }

      return new FixedList<NamedEntityDto>();
    }


    public FixedList<NamedEntityDto> ResponsibleList(string workitemTypeUID) {
      return new FixedList<NamedEntityDto>();
    }


    public FixedList<NamedEntityDto> StatusList() {
      var list = new List<NamedEntityDto> {
        new NamedEntityDto(ActivityStatus.All.ToString(), ActivityStatus.All.GetPluralName()),
        new NamedEntityDto(ActivityStatus.Pending.ToString(), ActivityStatus.Pending.GetPluralName()),
        new NamedEntityDto(ActivityStatus.Active.ToString(), ActivityStatus.Active.GetPluralName()),
        new NamedEntityDto(ActivityStatus.Suspended.ToString(), ActivityStatus.Suspended.GetPluralName()),
        new NamedEntityDto(ActivityStatus.Completed.ToString(), ActivityStatus.Completed.GetPluralName()),
        new NamedEntityDto(ActivityStatus.Canceled.ToString(), ActivityStatus.Canceled.GetPluralName()),
        new NamedEntityDto(ActivityStatus.Deleted.ToString(), ActivityStatus.Deleted.GetPluralName()),
      };

      return list.ToFixedList();
    }

    #endregion Use cases

  }  // class CataloguesUseCases

}  // namespace Empiria.Workflow.Requests.UseCases
