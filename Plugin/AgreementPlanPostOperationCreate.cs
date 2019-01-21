using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace CAMS.Xrm.Plugin
{
	[CrmPluginRegistration(MessageNameEnum.Create,
		cams_agreementplan.EntityLogicalName, StageEnum.PostOperation, ExecutionModeEnum.Synchronous,
		"", "Cams.Xrm.Plugin.AgreementPlanPostOperationCreate:Create of cams_agreementplan", 1,
		IsolationModeEnum.Sandbox
		, Image1Type = ImageTypeEnum.PostImage
		, Image1Name = "postImage"
		, Image1Attributes = ""
		, Description = "Cams.Xrm.Plugin.AgreementPlanPostOperationCreate:Create of cams_agreementplan"
		, Id = "6BB3A26E-C7B2-4637-A839-E597BBAC3463"
	)]
	public class AgreementPlanPostOperationCreate : PluginBase
	{
		private readonly string postImageAlias = "postImage";
		public AgreementPlanPostOperationCreate() : base(typeof(AgreementPlanPostOperationCreate))
		{
		}

		protected override void ExecuteCrmPlugin(LocalPluginContext localPluginContext)
		{
			if (localPluginContext?.PluginExecutionContext.InputParameters["Target"] == null)
			{
				throw new InvalidPluginExecutionException("localContext");
			}

			var service = localPluginContext.OrganizationService;
			var tracingService = localPluginContext.TracingService;

			cams_agreementplan entity =
				(localPluginContext.PluginExecutionContext.PostEntityImages != null &&
				 localPluginContext.PluginExecutionContext.PostEntityImages.Contains(postImageAlias))
					? localPluginContext.PluginExecutionContext.PostEntityImages[postImageAlias].ToEntity<cams_agreementplan>()
					: null;

			if (entity != null)
			{

				if (entity.cams_AgreementPlanBoilerplate != null)
				{
					
					var documentPartBoilerPlates = GetDocumentPartBolierPlates(service, entity.cams_AgreementPlanBoilerplate.Id);
					
					if (documentPartBoilerPlates.Any())
					{
						
						foreach (var documentPartBoilerPlate in documentPartBoilerPlates)
						{
							var documentPart = new cams_annex
							{
								cams_AgreementplanId = new EntityReference(cams_agreementplan.EntityLogicalName, entity.Id),
								cams_DocumentPartBoilerplate = new EntityReference(cams_documentpartboilerplate.EntityLogicalName, documentPartBoilerPlate.Id),
								cams_annexindex = documentPartBoilerPlate.cams_annexindex,
								cams_agreementplanparttype = documentPartBoilerPlate.cams_agreementplanparttype,
								cams_name = documentPartBoilerPlate.cams_name
							};

							var documentPartId = service.Create(documentPart);
							
						}
					}
				}
			}
		}



		private List<cams_documentpartboilerplate> GetDocumentPartBolierPlates(IOrganizationService service, Guid agreementPlanBoilerPlateId)
		{
			List<cams_documentpartboilerplate> documentPartBoilerPlates = new List<cams_documentpartboilerplate>();

			var query = new QueryExpression("cams_documentpartboilerplate");

			query.ColumnSet.AddColumns("cams_documentpartboilerplateid", "cams_name", "createdon", "cams_agreementplanparttype", "cams_annexindex");
			query.AddOrder("cams_name", OrderType.Ascending);

			var linkEntity = query.AddLink("cams_agreementplanboilerplate", "cams_agreementplanboilerplateid", "cams_agreementplanboilerplateid");
			linkEntity.EntityAlias = "ac";

			linkEntity.LinkCriteria.AddCondition("cams_agreementplanboilerplateid", ConditionOperator.Equal, agreementPlanBoilerPlateId.ToString("D"));

			var response = service.RetrieveMultiple(query);

			if (response != null && response.Entities.Count > 0)
			{
				response.Entities.ToList().ForEach(x => documentPartBoilerPlates.Add(x.ToEntity<cams_documentpartboilerplate>()));
			}
			return documentPartBoilerPlates;
		}
	}
}
