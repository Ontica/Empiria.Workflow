/* Empiria Workflow ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Definition                          Component : Web Api                               *
*  Assembly : Empiria.Workflow.WebApi.dll                  Pattern   : Web api Controller                    *
*  Type     : RequestDocumentsController                   License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Web API used to retrive and update workflow requests documents.                                *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System.Web.Http;

using Empiria.WebApi;

using Empiria.Storage;

using Empiria.Documents;
using Empiria.Documents.Services;

namespace Empiria.Workflow.Requests.WebApi {

  /// <summary>Web API used to retrive and update workflow requests documents.</summary>
  public class RequestDocumentsController : WebApiController {

    #region Command web apis

    [HttpDelete]
    [Route("v4/requests/{requestUID:guid}/documents/{documentUID:guid}")]
    public NoDataModel RemoveRequestDocument([FromUri] string requestUID,
                                             [FromUri] string documentUID) {

      var request = Requests.Request.Parse(requestUID);
      var document = Document.Parse(documentUID);

      DocumentServices.RemoveDocument(request, document);

      return new NoDataModel(Request);
    }


    [HttpPost]
    [Route("v4/requests/{requestUID:guid}/documents")]
    public SingleObjectModel StoreRequestDocument([FromUri] string requestUID) {

      var request = Requests.Request.Parse(requestUID);

      DocumentFields fields = GetFormDataFromHttpRequest<DocumentFields>("document");

      InputFile documentFile = GetInputFileFromHttpRequest(fields.DocumentProductUID);

      var document = DocumentServices.StoreDocument(documentFile, request, fields);

      return new SingleObjectModel(Request, document);
    }


    [HttpPut, HttpPatch]
    [Route("v4/requests/{requestUID:guid}/documents/{documentUID:guid}")]
    public SingleObjectModel UpdateRequestDocument([FromUri] string requestUID,
                                                   [FromUri] string documentUID,
                                                   [FromBody] DocumentFields fields) {
      RequireBody(fields);

      var request = Requests.Request.Parse(requestUID);
      var document = Document.Parse(documentUID);

      var documentDto = DocumentServices.UpdateDocument(request, document, fields);

      return new SingleObjectModel(Request, documentDto);
    }

    #endregion Web Apis

  }  // class RequestDocumentsController

}  // namespace Empiria.Workflow.Requests.WebApi
