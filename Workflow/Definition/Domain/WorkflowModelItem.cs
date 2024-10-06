﻿/* Empiria Workflow ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Definition                        Component : Domain Layer                            *
*  Assembly : Empiria.Workflow.dll                       Pattern   : Partitioned type                        *
*  Type     : WorkflowModelItem                          License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Partitioned type that describes a workflow model item. A workflow model item defines a         *
*             relationship between workflow objects and also serves to give the structure of a workflow.     *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Json;

using Empiria.Ontology;
using Empiria.Parties;
using Empiria.StateEnums;

using Empiria.Workflow.Definition.Data;

namespace Empiria.Workflow.Definition {

  /// <summary>Partitioned type that describes a workflow model item. A workflow model item defines a
  /// relationship between workflow objects and also serves to give the structure of a workflow.</summary>
  [PartitionedType(typeof(WorkflowModelItemType))]
  public class WorkflowModelItem : BaseObject, INamedEntity {

    #region Constructors and parsers

    protected WorkflowModelItem() {
      // Required by Empiria Framework.
    }

    protected WorkflowModelItem(WorkflowModelItemType powertype) : base(powertype) {
      // Required by Empiria Framework for all partitioned types.
    }

    static public WorkflowModelItem Parse(int id) {
      return BaseObject.ParseId<WorkflowModelItem>(id);
    }


    static public WorkflowModelItem Parse(string uid) {
      return BaseObject.ParseKey<WorkflowModelItem>(uid);
    }


    static public FixedList<T> GetSources<T>(ProcessDef processDef,
                                             WorkflowModelItemType modelItemType,
                                             WorkflowObject targetObject) where T : WorkflowObject {
      return WorkflowModelItemsData.GetSources<T>(processDef, modelItemType, targetObject);
    }

    static public FixedList<T> GetTargets<T>(ProcessDef processDef,
                                             WorkflowModelItemType modelItemType,
                                             WorkflowObject sourceObject) where T : WorkflowObject {
      return WorkflowModelItemsData.GetTargets<T>(processDef, modelItemType, sourceObject);
    }

    static public WorkflowModelItem Empty => BaseObject.ParseEmpty<WorkflowModelItem>();

    #endregion Constructors and parsers

    #region Properties

    public WorkflowModelItemType WorkflowModelItemType {
      get {
        return (WorkflowModelItemType) base.GetEmpiriaType();
      }
    }

    [DataField("WMS_MDL_ITEM_PROCESS_DEF_ID")]
    public ProcessDef ProcessDef {
      get; protected set;
    }


    [DataField("WMS_MDL_ITEM_SOURCE_OBJECT_ID")]
    public WorkflowObject SourceObject {
      get; protected set;
    }


    [DataField("WMS_MDL_ITEM_TARGET_OBJECT_ID")]
    public WorkflowObject TargetObject {
      get; protected set;
    }


    [DataField("WMS_MDL_ITEM_CODE")]
    public string Code {
      get; protected set;
    }


    [DataField("WMS_MDL_ITEM_NAME")]
    public string Name {
      get; protected set;
    }


    string INamedEntity.Name {
      get {
        if (this.Name.Length != 0) {
          return Name;
        }
        return TargetObject.Name;
      }
    }


    [DataField("WMS_MDL_ITEM_DESCRIPTION")]
    public string Description {
      get; protected set;
    }


    [DataField("WMS_MDL_ITEM_TAGS")]
    public string Tags {
      get; protected set;
    }


    [DataField("WMS_MDL_ITEM_EXT_OBJECT_TYPE_ID")]
    public int ExternalObjectTypeId {
      get; protected set;
    }


    [DataField("WMS_MDL_ITEM_CONFIG_DATA")]
    protected JsonObject ConfigurationData {
      get; private set;
    }


    [DataField("WMS_MDL_ITEM_EXT_DATA")]
    protected JsonObject ExtendedData {
      get; private set;
    }


    public virtual string Keywords {
      get {
        return EmpiriaString.BuildKeywords(Name, Code, SourceObject.Keywords, TargetObject.Keywords);
      }
    }

    [DataField("WMS_MDL_ITEM_POSITION")]
    internal int Position {
      get; set;
    }


    [DataField("WMS_MDL_ITEM_START_DATE")]
    internal DateTime StartDate {
      get; set;
    }


    [DataField("WMS_MDL_ITEM_END_DATE")]
    internal DateTime EndDate {
      get; private set;
    }


    [DataField("WMS_MDL_ITEM_POSTED_BY_ID")]
    internal int PostedById {
      get; private set;
    }


    [DataField("WMS_MDL_ITEM_POSTING_TIME")]
    internal DateTime PostingTime {
      get; private set;
    }


    [DataField("WMS_MDL_ITEM_STATUS", Default = EntityStatus.Active)]
    public EntityStatus Status {
      get; protected set;
    }


    public AssignationRules AssignationRules {
      get {
        if (ConfigurationData.Contains(WorkflowConstants.ASSIGNATION_RULES)) {
          return ConfigurationData.Get<AssignationRules>(WorkflowConstants.ASSIGNATION_RULES);

        } else if (TargetObject is StepDef stepDef) {
          return stepDef.AssignationRules;

        } else {
          return new AssignationRules();
        }
      }
    }


    public bool Autoactivate {
      get {
        if (ConfigurationData.Contains(WorkflowConstants.AUTOACTIVATE)) {
          return ConfigurationData.Get<bool>(WorkflowConstants.AUTOACTIVATE);

        } else if (TargetObject is StepDef stepDef) {
          return stepDef.Autoactivate;

        } else {
          return false;
        }
      }
    }


    public bool IsOptional {
      get {
        return ConfigurationData.Get(WorkflowConstants.IS_OPTIONAL, false);
      }
    }

    #endregion Properties

    #region Methods

    internal T GetParty<T>(string partyIdField, T defaultValue) where T : Party {
      Assertion.Require(partyIdField, nameof(partyIdField));
      Assertion.Require(defaultValue, nameof(defaultValue));

      if (ConfigurationData.Contains($"parties/{partyIdField}")) {
        return ConfigurationData.Get<T>($"parties/{partyIdField}");

      } else if (ProcessDef.HasParty(partyIdField)) {
        return ProcessDef.GetParty<T>(partyIdField);

      } else if (TargetObject is StepDef stepDef && stepDef.HasParty(partyIdField)) {
        return stepDef.GetParty<T>(partyIdField);

      } else {
        return defaultValue;
      }
    }

    #endregion Methods

  } // class WorkflowModelItem

} // namespace Empiria.Workflow.Definition
