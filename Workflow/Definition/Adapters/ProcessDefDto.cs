﻿/* Empiria Workflow ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Definition                        Component : Adapters Layer                          *
*  Assembly : Empiria.Workflow.dll                       Pattern   : Output DTO                              *
*  Type     : ProcessDefDto                              License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Output data transfer object for Process definition instances.                                  *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using Empiria.DataObjects;

namespace Empiria.Workflow.Definition.Adapters {

  /// <summary>Output data transfer object for Process definition instances.</summary>
  public class ProcessDefDto {

    public string UID {
      get; internal set;
    }


    public string Name {
      get; internal set;
    }

    public FixedList<DataFieldDto> InputData {
      get; internal set;
    }

  }  // class ProcessDefDto

}  // namespace Empiria.Workflow.Definition.Adapters
