
CrmSvcUtil.exe ^
/url:https://modcamsdev.api.crm4.dynamics.com/XRMServices/2011/Organization.svc ^
/out:..\\EarlyBoundTypes.cs ^
/username:srihari.koneti@tisski.com ^
/password:Shinfield6 ^
/namespace:CAMS.Xrm ^
/serviceContextName:CamsContext


CrmSvcUtil.exe ^
/codewriterfilter:"Microsoft.Crm.Sdk.Samples.FilteringService, GeneratePicklistEnums" ^
/codecustomization:"Microsoft.Crm.Sdk.Samples.CodeCustomizationService, GeneratePicklistEnums" ^
/namingservice:"Microsoft.Crm.Sdk.Samples.NamingService, GeneratePicklistEnums" ^
/url:https://modcamsdev.api.crm4.dynamics.com/XRMServices/2011/Organization.svc ^
/username:srihari.koneti@tisski.com ^
/password:Shinfield6 ^
/namespace:CAMS.Xrm ^
/out:..\\OptionSets.cs

CrmSvcUtil.exe ^
/codecustomization:"GenerateAttributeConstants.CodeCustomizationService, GenerateAttributeConstants" ^
/codewriterfilter:"GenerateAttributeConstants.FilteringService, GenerateAttributeConstants" ^
/url:https://modcamsdev.api.crm4.dynamics.com/XRMServices/2011/Organization.svc ^
/username:srihari.koneti@tisski.com ^
/password:Shinfield6 ^
/namespace:CAMS.Xrm ^
/out:..\\Attributes.cs