/* Empiria Workflow ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Services                          Component : Integration Services Layer              *
*  Assembly : Empiria.Workflow.dll                       Pattern   : Service Provider                        *
*  Type     : WorkflowStepsProvider                      License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides services over Empiria workflow steps, implementing the IWorkItemProvider interface.   *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using Empiria.Services.Providers;

using Empiria.Workflow.Execution;

namespace Empiria.Workflow.Services {

  /// <summary>Provides services over Empiria workflow steps,
  /// implementing the IWorkItemProvider interface.</summary>
  internal class WorkflowStepsProvider : IWorkItemProvider {

    #region Services

    public void SendEvent(IWorkItemEvent workItemEvent) {
      Assertion.Require(workItemEvent, nameof(workItemEvent));

      WorkflowStepEvent stepEvent = BuildWorkflowStepEvent(workItemEvent);

      WorkflowInstance workflowInstance = stepEvent.Step.WorkflowInstance;

      workflowInstance.OnEvent(stepEvent);
    }

    #endregion Services

    #region Helpers

    private WorkflowStepEvent BuildWorkflowStepEvent(IWorkItemEvent workItemEvent) {
      var step = WorkflowStep.Parse(workItemEvent.WorkItem.WorkItemUID);

      return new WorkflowStepEvent(workItemEvent.Type, step, workItemEvent.WorkItem);
    }

    #endregion Helpers

  }  // class WorkflowStepsProvider

}  // namespace Empiria.Workflow.Services
