/* Empiria Workflow ******************************************************************************************
*                                                                                                            *
*  Module   : Requests Management                        Component : Adapters Layer                          *
*  Assembly : Empiria.Workflow.dll                       Pattern   : Mapper                                  *
*  Type     : RequestMapper                              License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Maps Requests instances to their DTOs.                                                         *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using Empiria.StateEnums;

using Empiria.Documents;
using Empiria.History.Services;

using Empiria.Workflow.Definition.Adapters;
using Empiria.Workflow.Execution.Adapters;

namespace Empiria.Workflow.Requests.Adapters {

  /// <summary>Maps Requests instances to their DTOs.</summary>
  static internal class RequestMapper {

    static internal FixedList<RequestHolderDto> Map(FixedList<Request> requests) {
      return requests.Select(x => Map(x)).ToFixedList();
    }


    static internal RequestHolderDto Map(Request request) {
      return new RequestHolderDto {
        Request = MapRequest(request),
        WorkflowInstances = WorkflowInstanceMapper.Map(request.GetWorkflowInstances()),
        Steps = WorkflowStepMapper.Map(request.GetSteps()),
        Documents = DocumentServices.GetAllEntityDocuments(request),
        History = HistoryServices.GetEntityHistory(request),
        Actions = MapActions(request),

        // ToDo: To be removed
        Tasks = WorkflowStepMapper.Map(request.GetSteps()),
      };
    }

    static internal FixedList<RequestDescriptorDto> MapToDescriptor(FixedList<Request> requests) {
      return requests.Select(x => MapToDescriptor(x)).ToFixedList();
    }

    #region Helpers

    static private BaseActions MapActions(Request request) {
      return new BaseActions {
        CanEditDocuments = true
      };
    }


    static private RequestDto MapRequest(Request request) {
      return new RequestDto {
        UID = request.UID,
        RequestNo = request.RequestNo,
        InternalControlNo = request.InternalControlNo,
        Name = request.Name,
        Description = request.Description,
        RequestedBy = request.RequestedBy.MapToNamedEntity(),
        RequestedByOrgUnit = request.RequestedByOrgUnit.MapToNamedEntity(),
        ResponsibleOrgUnit = request.ResponsibleOrgUnit.MapToNamedEntity(),
        Priority = request.Priority.MapToDto(),
        DueTime = request.DueTime,
        StartedBy = request.StartedBy.MapToNamedEntity(),
        StartTime = request.StartTime,
        EndTime = request.EndTime,
        Status = request.Status.GetName(),
        Fields = request.RequestTypeFields,
        Actions = request.Actions.MapToDto(),
        RequestDef = RequestDefMapper.Map(request.RequestDef),

        // ToDo: To be removed
        RequestType = RequestDefMapper.Map(request.RequestDef)
      };
    }


    static private RequestDescriptorDto MapToDescriptor(Request request) {
      return new RequestDescriptorDto {
        UID = request.UID,
        RequestNo = request.RequestNo,
        InternalControlNo = request.InternalControlNo,
        Name = request.Name,
        Description = request.Description,
        RequestedBy = request.RequestedBy.Name,
        RequestedByOrgUnit = request.RequestedByOrgUnit.Name,
        ResponsibleOrgUnit = request.ResponsibleOrgUnit.Name,
        Priority = request.Priority.GetName(),
        DueTime = request.DueTime,
        StartedBy = request.StartedBy.Name,
        StartTime = request.StartTime,
        EndTime = request.EndTime,
        Status = request.Status.GetName()
      };
    }

    #endregion Helpers

  }  // class RequestMapper

}  // namespace Empiria.Workflow.Requests.Adapters
