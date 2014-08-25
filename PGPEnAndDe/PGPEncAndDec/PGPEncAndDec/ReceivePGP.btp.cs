namespace PGPEncAndDec
{
    using System;
    using System.Collections.Generic;
    using Microsoft.BizTalk.PipelineOM;
    using Microsoft.BizTalk.Component;
    using Microsoft.BizTalk.Component.Interop;
    
    
    public sealed class ReceivePGP : Microsoft.BizTalk.PipelineOM.ReceivePipeline
    {
        
        private const string _strPipeline = "<?xml version=\"1.0\" encoding=\"utf-16\"?><Document xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instanc"+
"e\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" MajorVersion=\"1\" MinorVersion=\"0\">  <Description /> "+
" <CategoryId>f66b9f5e-43ff-4f5f-ba46-885348ae1b4e</CategoryId>  <FriendlyName>Receive</FriendlyName>"+
"  <Stages>    <Stage>      <PolicyFileStage _locAttrData=\"Name\" _locID=\"1\" Name=\"Decode\" minOccurs=\""+
"0\" maxOccurs=\"-1\" execMethod=\"All\" stageId=\"9d0e4103-4cce-4536-83fa-4a5040674ad6\" />      <Component"+
"s>        <Component>          <Name>Incapital.BizTalk.PipelineComponents.DecryptPGP,DecryptPGP, Ver"+
"sion=1.0.0.0, Culture=neutral, PublicKeyToken=8d56ff0f5793b8a2</Name>          <ComponentName>Decryp"+
"tPGP</ComponentName>          <Description>PGP Decryption</Description>          <Version>1.0</Versi"+
"on>          <Properties>            <Property Name=\"TempDirectory\" />            <Property Name=\"Pr"+
"ivateKeyFile\" />            <Property Name=\"Passphrase\">              <Value xsi:type=\"xsd:string\">i"+
"ncapnet20</Value>            </Property>          </Properties>          <CachedDisplayName>DecryptP"+
"GP</CachedDisplayName>          <CachedIsManaged>true</CachedIsManaged>        </Component>      </C"+
"omponents>    </Stage>    <Stage>      <PolicyFileStage _locAttrData=\"Name\" _locID=\"2\" Name=\"Disasse"+
"mble\" minOccurs=\"0\" maxOccurs=\"-1\" execMethod=\"FirstMatch\" stageId=\"9d0e4105-4cce-4536-83fa-4a504067"+
"4ad6\" />      <Components />    </Stage>    <Stage>      <PolicyFileStage _locAttrData=\"Name\" _locID"+
"=\"3\" Name=\"Validate\" minOccurs=\"0\" maxOccurs=\"-1\" execMethod=\"All\" stageId=\"9d0e410d-4cce-4536-83fa-"+
"4a5040674ad6\" />      <Components />    </Stage>    <Stage>      <PolicyFileStage _locAttrData=\"Name"+
"\" _locID=\"4\" Name=\"ResolveParty\" minOccurs=\"0\" maxOccurs=\"-1\" execMethod=\"All\" stageId=\"9d0e410e-4cc"+
"e-4536-83fa-4a5040674ad6\" />      <Components />    </Stage>  </Stages></Document>";
        
        private const string _versionDependentGuid = "d1460383-d811-44d0-a20e-7225a2565687";
        
        public ReceivePGP()
        {
            Microsoft.BizTalk.PipelineOM.Stage stage = this.AddStage(new System.Guid("9d0e4103-4cce-4536-83fa-4a5040674ad6"), Microsoft.BizTalk.PipelineOM.ExecutionMode.all);
            IBaseComponent comp0 = Microsoft.BizTalk.PipelineOM.PipelineManager.CreateComponent("Incapital.BizTalk.PipelineComponents.DecryptPGP,DecryptPGP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=8d56ff0f5793b8a2");;
            if (comp0 is IPersistPropertyBag)
            {
                string comp0XmlProperties = "<?xml version=\"1.0\" encoding=\"utf-16\"?><PropertyBag xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-inst"+
"ance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">  <Properties>    <Property Name=\"TempDirectory\" "+
"/>    <Property Name=\"PrivateKeyFile\" />    <Property Name=\"Passphrase\">      <Value xsi:type=\"xsd:s"+
"tring\">incapnet20</Value>    </Property>  </Properties></PropertyBag>";
                PropertyBag pb = PropertyBag.DeserializeFromXml(comp0XmlProperties);;
                ((IPersistPropertyBag)(comp0)).Load(pb, 0);
            }
            this.AddComponent(stage, comp0);
        }
        
        public override string XmlContent
        {
            get
            {
                return _strPipeline;
            }
        }
        
        public override System.Guid VersionDependentGuid
        {
            get
            {
                return new System.Guid(_versionDependentGuid);
            }
        }
    }
}
