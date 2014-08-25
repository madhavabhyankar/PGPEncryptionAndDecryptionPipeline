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
    [System.Runtime.InteropServices.Guid("a2fc0acf-9156-4917-9c8d-9298eba2b42b")]
    [ComponentCategory(CategoryTypes.CATID_Decoder)]
    public class DecryptPGP : Microsoft.BizTalk.Component.Interop.IComponent, IBaseComponent, IPersistPropertyBag, IComponentUI
    {
        
        private System.Resources.ResourceManager resourceManager = new System.Resources.ResourceManager("Incapital.BizTalk.PipelineComponents.DecryptPGP", Assembly.GetExecutingAssembly());
        
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
        
        private string _PrivateKeyFile;
        
        public string PrivateKeyFile
        {
            get
            {
                return _PrivateKeyFile;
            }
            set
            {
                _PrivateKeyFile = value;
            }
        }
        
        private string _Passphrase;
        
        public string Passphrase
        {
            get
            {
                return _Passphrase;
            }
            set
            {
                _Passphrase = value;
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
            classid = new System.Guid("a2fc0acf-9156-4917-9c8d-9298eba2b42b");
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
            val = this.ReadPropertyBag(pb, "PrivateKeyFile");
            if ((val != null))
            {
                this._PrivateKeyFile = ((string)(val));
            }
            val = this.ReadPropertyBag(pb, "Passphrase");
            if ((val != null))
            {
                this._Passphrase = ((string)(val));
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
            this.WritePropertyBag(pb, "PrivateKeyFile", this.PrivateKeyFile);
            this.WritePropertyBag(pb, "Passphrase", this.Passphrase);
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
            IBaseMessagePart bodyPart = inmsg.BodyPart;
            IBaseMessageContext context = inmsg.Context;
            string filename = "";

            if (bodyPart != null)
            {
                filename = context.Read("ReceivedFileName", "http://schemas.microsoft.com/BizTalk/2003/file-properties").ToString();

                if (filename.Contains("\\")) { filename = filename.Substring(filename.LastIndexOf("\\") + 1); }
                if (filename.Contains("/")) { filename = filename.Substring(filename.LastIndexOf("/") + 1); }

                if (0 < _TempDirectory.Length)
                    if (!Directory.Exists(_TempDirectory))
                        Directory.CreateDirectory(_TempDirectory);
                filename = Path.Combine(_TempDirectory, filename);
                string tempFile = Path.Combine(_TempDirectory, Guid.NewGuid().ToString());

                Stream decStream = PGPWrapper.DecryptStream(bodyPart.Data, _PrivateKeyFile, _Passphrase, tempFile, _TempDirectory);

                decStream.Seek(0, SeekOrigin.Begin);
                bodyPart.Data = decStream;
                pc.ResourceTracker.AddResource(decStream);
                context.Write("ReceivedFileName", "http://schemas.microsoft.com/BizTalk/2003/file-properties", filename.Replace(".pgp", ""));

            }
            return inmsg;
        }
        #endregion
    }
}