﻿/* Empiria Workflow ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Execution                         Component : Data Layer                              *
*  Assembly : Empiria.Workflow.dll                       Pattern   : Data Services                           *
*  Type     : WorkflowExecutionData                      License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides data read and write methods for workflow execution types.                             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System.Collections.Generic;

using Empiria.Data;

using Empiria.Workflow.Requests;

namespace Empiria.Workflow.Execution.Data {

  /// <summary>Provides data read and write methods for workflow execution types.</summary>
  static internal class WorkflowExecutionData {

    #region Read methods

    static internal List<WorkflowStep> GetSteps(WorkflowInstance workflowInstance) {
      if (workflowInstance.IsEmptyInstance || workflowInstance.IsNew) {
        return new List<WorkflowStep>();
      }

      var sql = "SELECT * FROM WMS_Steps " +
               $"WHERE WMS_INSTANCE_ID = {workflowInstance.Id} AND " +
                "WMS_STEP_STATUS <> 'X' " +
                "ORDER BY WMS_STEP_NO";

      var op = DataOperation.Parse(sql);

      return DataReader.GetList<WorkflowStep>(op);
    }


    static internal List<WorkflowInstance> GetWorkflowInstances(Request request) {
      if (request.IsEmptyInstance || request.IsNew) {
        return new List<WorkflowInstance>();
      }

      var sql = "SELECT * FROM WMS_Instances " +
                $"WHERE WMS_INST_REQUEST_ID = {request.Id} AND " +
                "WMS_INST_STATUS <> 'X' " +
                "ORDER BY WMS_INSTANCE_ID";

      var op = DataOperation.Parse(sql);

      return DataReader.GetList<WorkflowInstance>(op);
    }

    #endregion Read methods

    #region Write methods

    static internal void Write(WorkflowInstance o, string extensionData) {
      var op = DataOperation.Parse("write_WMS_Instance", o.Id, o.UID,
                o.ProcessDefinition.Id, o.Request.Id, o.RequestedBy.Id,
                o.RequestedByOrgUnit.Id, o.ResponsibleOrgUnit.Id,
                (char) o.Priority, o.DueTime, o.StartedBy.Id, o.StartTime,
                o.EndTime, o.Parent.Id, extensionData,
                o.Keywords, (char) o.Status);

      DataWriter.Execute(op);
    }


    static internal void Write(WorkflowStep o, string extensionData) {
      var op = DataOperation.Parse("write_WMS_Step", o.Id, o.UID,
                      o.WorkflowInstance.Id, o.WorkflowModelItem.Id, o.StepDefinition.Id,
                      o.StepNo, o.Description, o.Tags, o.RequestedBy.Id, o.RequestedByOrgUnit.Id,
                      o.AssignedTo.Id, o.AssignedToOrgUnit.Id, (char) o.Priority, o.DueTime,
                      o.StartTime, o.EndTime, o.PreviousStep.Id, o.NextStep.Id,
                      extensionData, o.Keywords, (char) o.Status);

      DataWriter.Execute(op);
    }

    #endregion Write methods

  }  // class WorkflowExecutionData

}  // namespace Empiria.Workflow.Execution.Data
