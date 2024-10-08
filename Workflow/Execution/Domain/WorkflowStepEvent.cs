/* Empiria Workflow ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Execution                         Component : Domain Layer                            *
*  Assembly : Empiria.Workflow.dll                       Pattern   : Immutable Value type                    *
*  Type     : WorkflowStepEvent                          License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Holds information about a workflow step event, that can be processed by a workflow engine.     *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

namespace Empiria.Workflow.Execution {

  /// <summary>Holds information about a workflow step event, that can be
  /// processed by a workflow engine.</summary>
  internal class WorkflowStepEvent {

    public WorkflowStepEvent(string type, WorkflowStep step, IWorkItem workItem) {
      Assertion.Require(type, nameof(type));
      Assertion.Require(step, nameof(step));
      Assertion.Require(workItem, nameof(workItem));

      Type = type;
      Step = step;
      WorkItem = workItem;
    }

    #region Properties

    public string Type {
      get;
    }

    public WorkflowStep Step {
      get;
    }

    public IWorkItem WorkItem {
      get;
    }

    #endregion Properties

  }  // class WorkflowStepEvent

}  // namespace Empiria.Workflow.Execution
