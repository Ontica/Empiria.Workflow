﻿/* Empiria Workflow ******************************************************************************************
*                                                                                                            *
*  Module   : Requests Management                        Component : Domain Layer                            *
*  Assembly : Empiria.Workflow.dll                       Pattern   : Power type                              *
*  Type     : RequestType                                License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Power type that describes a Request partitioned type.                                          *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using Empiria.Ontology;

using Empiria.Workflow.Requests.Adapters;

namespace Empiria.Workflow.Requests {

  /// <summary>Power type that describes a Request partitioned type.</summary>
  [Powertype(typeof(Request))]
  public sealed class RequestType : Powertype {

    #region Constructors and parsers

    private RequestType() {
      // Empiria power types always have this constructor.
    }

    static public new RequestType Parse(int typeId) {
      return ObjectTypeInfo.Parse<RequestType>(typeId);
    }

    static public new RequestType Parse(string typeName) {
      return RequestType.Parse<RequestType>(typeName);
    }

    static public RequestType Empty => RequestType.Parse("ObjectTypeInfo.WorkflowRequest");

    #endregion Constructors and parsers

    #region Methods

    public Request CreateRequest(RequestFields fields) {
      Assertion.Require(fields, nameof(fields));

      var request = base.CreateObject<Request>();

      request.Update(fields);

      return request;
    }

    #endregion Methods

  } // class RequestType

} // namespace Empiria.Workflow.Requests
