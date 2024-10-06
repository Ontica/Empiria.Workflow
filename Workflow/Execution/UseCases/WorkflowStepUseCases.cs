/* Empiria Workflow ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Execution                         Component : Use cases Layer                         *
*  Assembly : Empiria.Workflow.dll                       Pattern   : Use case interactor class               *
*  Type     : WorkflowStepUseCases                       License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Use cases for workflow steps.                                                                  *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using Empiria.Services;

namespace Empiria.Workflow.Execution.UseCases {

  /// <summary>Use cases for workflow steps.</summary>
  public class WorkflowStepUseCases : UseCase {

    #region Constructors and parsers

    protected WorkflowStepUseCases() {
      // no-op
    }

    static public WorkflowStepUseCases UseCaseInteractor() {
      return UseCase.CreateInstance<WorkflowStepUseCases>();
    }

    #endregion Constructors and parsers

    #region Use cases

    #endregion Use cases

  }  // class WorkflowStepUseCases

}  // namespace Empiria.Workflow.Execution.UseCases
