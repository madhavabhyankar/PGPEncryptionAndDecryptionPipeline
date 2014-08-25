namespace Incapital.BizTalk.PipelineComponents
{
    using System;
    using System.IO;
    using System.Text;
    using System.Drawing;
    using System.Resources;
    using System.Reflection;
    using System.Diagnostics;
    using System.Collections;
    using System.ComponentModel;
    using Microsoft.BizTalk.Message.Interop;
    using Microsoft.BizTalk.Component.Interop;
    using Microsoft.BizTalk.Component;
    using Microsoft.BizTalk.Messaging;
    
    
    [ComponentCategory(CategoryTypes.CATID_PipelineComponent)]
    [System.Runtime.InteropServices.Guid("cb2c8ddd-5644-47e7-ab85-424c1601bec7")]
    [ComponentCategory(CategoryTypes.CATID_Encoder)]
    public class EncryptPGP : Microsoft.BizTalk.Component.Interop.IComponent, IBaseComponent, IPersistPropertyBag, IComponentUI
    {
        
        private System.Resources.ResourceManager resourceManager = new System.Resources.ResourceManager("Incapital.BizTalk.PipelineComponents.EncryptPGP", Assembly.GetExecutingAssembly());
        
        private string _TempDirectory;
        
        public string TempDirectory
        {
            get
            {
                return _TempDirectory;
            }
            set
            {
                _TempDirectory = value;
            }
        }
        
        private string _PublicKeyFile;
        
        public string PublicKeyFile
        {
            get
            {
                return _PublicKeyFile;
            }
            set
            {
                _PublicKeyFile = value;
            }
        }
        
        private bool _IntegrityCheckFlag;
        
        public bool IntegrityCheckFlag
        {
            get
            {
                return _IntegrityCheckFlag;
            }
            set
            {
                _IntegrityCheckFlag = value;
            }
        }
        
        private bool _ASCIIArmorFlag;
        
        public bool ASCIIArmorFlag
        {
            get
            {
                return _ASCIIArmorFlag;
            }
            set
            {
                _ASCIIArmorFlag = value;
            }
        }
        
        private string _EncryptedFileExtension;
        
        public string EncryptedFileExtension
        {
            get
            {
                return _EncryptedFileExtension;
            }
            set
            {
                _EncryptedFileExtension = value;
            }
        }
        
        #region IBaseComponent members
        /// <summary>
        /// Name of the component
        /// </summary>
        [Browsable(false)]
        public string Name
        {
            get
            {
                return resourceManager.GetString("COMPONENTNAME", System.Globalization.CultureInfo.InvariantCulture);
            }
        }
        
        /// <summary>
        /// Version of the component
        /// </summary>
        [Browsable(false)]
        public string Version
        {
            get
            {
                return resourceManager.GetString("COMPONENTVERSION", System.Globalization.CultureInfo.InvariantCulture);
            }
        }
        
        /// <summary>
        /// Description of the component
        /// </summary>
        [Browsable(false)]
        public string Description
        {
            get
            {
                return resourceManager.GetString("COMPONENTDESCRIPTION", System.Globalization.CultureInfo.InvariantCulture);
            }
        }
        #endregion
        
        #region IPersistPropertyBag members
        /// <summary>
        /// Gets class ID of component for usage from unmanaged code.
        /// </summary>
        /// <param name="classid">
        /// Class ID of the component
        /// </param>
        public void GetClassID(out System.Guid classid)
        {
            classid = new System.Guid("cb2c8ddd-5644-47e7-ab85-424c1601bec7");
        }
        
        /// <summary>
        /// not implemented
        /// </summary>
        public void InitNew()
        {
        }
        
        /// <summary>
        /// Loads configuration properties for the component
        /// </summary>
        /// <param name="pb">Configuration property bag</param>
        /// <param name="errlog">Error status</param>
        public virtual void Load(Microsoft.BizTalk.Component.Interop.IPropertyBag pb, int errlog)
        {
            object val = null;
            val = this.ReadPropertyBag(pb, "TempDirectory");
            if ((val != null))
            {
                this._TempDirectory = ((string)(val));
            }
            val = this.ReadPropertyBag(pb, "PublicKeyFile");
            if ((val != null))
            {
                this._PublicKeyFile = ((string)(val));
            }
            val = this.ReadPropertyBag(pb, "IntegrityCheckFlag");
            if ((val != null))
            {
                this._IntegrityCheckFlag = ((bool)(val));
            }
            val = this.ReadPropertyBag(pb, "ASCIIArmorFlag");
            if ((val != null))
            {
                this._ASCIIArmorFlag = ((bool)(val));
            }
            val = this.ReadPropertyBag(pb, "EncryptedFileExtension");
            if ((val != null))
            {
                this._EncryptedFileExtension = ((string)(val));
            }
        }
        
        /// <summary>
        /// Saves the current component configuration into the property bag
        /// </summary>
        /// <param name="pb">Configuration property bag</param>
        /// <param name="fClearDirty">not used</param>
        /// <param name="fSaveAllProperties">not used</param>
        public virtual void Save(Microsoft.BizTalk.Component.Interop.IPropertyBag pb, bool fClearDirty, bool fSaveAllProperties)
        {
            this.WritePropertyBag(pb, "TempDirectory", this.TempDirectory);
            this.WritePropertyBag(pb, "PublicKeyFile", this.PublicKeyFile);
            this.WritePropertyBag(pb, "IntegrityCheckFlag", this.IntegrityCheckFlag);
            this.WritePropertyBag(pb, "ASCIIArmorFlag", this.ASCIIArmorFlag);
            this.WritePropertyBag(pb, "EncryptedFileExtension", this.EncryptedFileExtension);
        }
        
        #region utility functionality
        /// <summary>
        /// Reads property value from property bag
        /// </summary>
        /// <param name="pb">Property bag</param>
        /// <param name="propName">Name of property</param>
        /// <returns>Value of the property</returns>
        private object ReadPropertyBag(Microsoft.BizTalk.Component.Interop.IPropertyBag pb, string propName)
        {
            object val = null;
            try
            {
                pb.Read(propName, out val, 0);
            }
            catch (System.ArgumentException )
            {
                return val;
            }
            catch (System.Exception e)
            {
                throw new System.ApplicationException(e.Message);
            }
            return val;
        }
        
        /// <summary>
        /// Writes property values into a property bag.
        /// </summary>
        /// <param name="pb">Property bag.</param>
        /// <param name="propName">Name of property.</param>
        /// <param name="val">Value of property.</param>
        private void WritePropertyBag(Microsoft.BizTalk.Component.Interop.IPropertyBag pb, string propName, object val)
        {
            try
            {
                pb.Write(propName, ref val);
            }
            catch (System.Exception e)
            {
                throw new System.ApplicationException(e.Message);
            }
        }
        #endregion
        #endregion
        
        #region IComponentUI members
        /// <summary>
        /// Component icon to use in BizTalk Editor
        /// </summary>
        [Browsable(false)]
        public IntPtr Icon
        {
            get
            {
                return ((System.Drawing.Bitmap)(this.resourceManager.GetObject("COMPONENTICON", System.Globalization.CultureInfo.InvariantCulture))).GetHicon();
            }
        }
        
        /// <summary>
        /// The Validate method is called by the BizTalk Editor during the build 
        /// of a BizTalk project.
        /// </summary>
        /// <param name="obj">An Object containing the configuration properties.</param>
        /// <returns>The IEnumerator enables the caller to enumerate through a collection of strings containing error messages. These error messages appear as compiler error messages. To report successful property validation, the method should return an empty enumerator.</returns>
        public System.Collections.IEnumerator Validate(object obj)
        {
            // example implementation:
            // ArrayList errorList = new ArrayList();
            // errorList.Add("This is a compiler error");
            // return errorList.GetEnumerator();
            return null;
        }
        #endregion
        
        #region IComponent members
        /// <summary>
        /// Implements IComponent.Execute method.
        /// </summary>
        /// <param name="pc">Pipeline context</param>
        /// <param name="inmsg">Input message</param>
        /// <returns>Original input message</returns>
        /// <remarks>
        /// IComponent.Execute method is used to initiate
        /// the processing of the message in this pipeline component.
        /// </remarks>
        public Microsoft.BizTalk.Message.Interop.IBaseMessage Execute(Microsoft.BizTalk.Component.Interop.IPipelineContext pc, Microsoft.BizTalk.Message.Interop.IBaseMessage inmsg)
        {
            System.Diagnostics.Trace.WriteLine(">>> PgpEncrypt.Execute ()  v.3");
            IBaseMessagePart bodyPart = inmsg.BodyPart;
            IBaseMessageContext context = inmsg.Context;
            string filename = "";

            if (bodyPart != null)
            {
                filename = context.Read("ReceivedFileName", "http://schemas.microsoft.com/BizTalk/2003/file-properties").ToString();

                if (filename.Contains("\\")) { filename = filename.Substring(filename.LastIndexOf("\\") + 1); }
                if (filename.Contains("/")) { filename = filename.Substring(filename.LastIndexOf("/") + 1); }

                if (!String.IsNullOrEmpty(_TempDirectory))
                    if (!Directory.Exists(this._TempDirectory))
                        Directory.CreateDirectory(this._TempDirectory);
                filename = Path.Combine(this._TempDirectory, filename);

                string tempFile = Path.Combine(this._TempDirectory, Guid.NewGuid().ToString());

                Stream encStream = PGPWrapper.EncryptStream(bodyPart.Data, this._PublicKeyFile, tempFile, this._EncryptedFileExtension, this._ASCIIArmorFlag, this._IntegrityCheckFlag);

                encStream.Seek(0, SeekOrigin.Begin);
                bodyPart.Data = encStream;
                pc.ResourceTracker.AddResource(encStream);
                context.Write("ReceivedFileName", "http://schemas.microsoft.com/BizTalk/2003/file-properties", filename + ".pgp");
            }
            return inmsg;
        }
        #endregion
    }
}
