// ------------------------------------------------------------------------------
//  <auto-generated>
//    Generated by Xsd2Code. Version 3.4.1.34438 Microsoft Reciprocal License (Ms-RL) 
//    <NameSpace>CapitalTrackingSchemaV3</NameSpace><Collection>List</Collection><codeType>CSharp</codeType><EnableDataBinding>False</EnableDataBinding><EnableLazyLoading>False</EnableLazyLoading><TrackingChangesEnable>False</TrackingChangesEnable><GenTrackingClasses>False</GenTrackingClasses><HidePrivateFieldInIDE>True</HidePrivateFieldInIDE><EnableSummaryComment>True</EnableSummaryComment><VirtualProp>False</VirtualProp><IncludeSerializeMethod>True</IncludeSerializeMethod><UseBaseClass>True</UseBaseClass><GenBaseClass>True</GenBaseClass><GenerateCloneMethod>False</GenerateCloneMethod><GenerateDataContracts>True</GenerateDataContracts><CodeBaseTag>Net20</CodeBaseTag><SerializeMethodName>Serialize</SerializeMethodName><DeserializeMethodName>Deserialize</DeserializeMethodName><SaveToFileMethodName>SaveToFile</SaveToFileMethodName><LoadFromFileMethodName>LoadFromFile</LoadFromFileMethodName><GenerateXMLAttributes>True</GenerateXMLAttributes><OrderXMLAttrib>False</OrderXMLAttrib><EnableEncoding>False</EnableEncoding><AutomaticProperties>False</AutomaticProperties><GenerateShouldSerialize>False</GenerateShouldSerialize><DisableDebug>True</DisableDebug><PropNameSpecified>Default</PropNameSpecified><Encoder>UTF8</Encoder><CustomUsings></CustomUsings><ExcludeIncludedTypes>False</ExcludeIncludedTypes><EnableInitializeFields>False</EnableInitializeFields>
//  </auto-generated>
// ------------------------------------------------------------------------------
namespace Data.Model.Tracking
{
    using System;
    using System.Diagnostics;
    using System.Xml.Serialization;
    using System.Collections;
    using System.Xml.Schema;
    using System.ComponentModel;
    using System.IO;
    using System.Text;
    using System.Collections.Generic;


    #region Base entity class
    public partial class EntityBase<T>
    {

        private static System.Xml.Serialization.XmlSerializer serializer;

        private static System.Xml.Serialization.XmlSerializer Serializer
        {
            get
            {
                if ((serializer == null))
                {
                    serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
                }
                return serializer;
            }
        }

        #region Serialize/Deserialize
        /// <summary>
        /// Serializes current EntityBase object into an XML document
        /// </summary>
        /// <returns>string XML value</returns>
        public virtual string Serialize()
        {
            System.IO.StreamReader streamReader = null;
            System.IO.MemoryStream memoryStream = null;
            try
            {
                memoryStream = new System.IO.MemoryStream();
                Serializer.Serialize(memoryStream, this);
                memoryStream.Seek(0, System.IO.SeekOrigin.Begin);
                streamReader = new System.IO.StreamReader(memoryStream);
                return streamReader.ReadToEnd();
            }
            finally
            {
                if ((streamReader != null))
                {
                    streamReader.Dispose();
                }
                if ((memoryStream != null))
                {
                    memoryStream.Dispose();
                }
            }
        }

        /// <summary>
        /// Deserializes workflow markup into an EntityBase object
        /// </summary>
        /// <param name="xml">string workflow markup to deserialize</param>
        /// <param name="obj">Output EntityBase object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool Deserialize(string xml, out T obj, out System.Exception exception)
        {
            exception = null;
            obj = default(T);
            try
            {
                obj = Deserialize(xml);
                return true;
            }
            catch (System.Exception ex)
            {
                exception = ex;
                return false;
            }
        }

        public static bool Deserialize(string xml, out T obj)
        {
            System.Exception exception = null;
            return Deserialize(xml, out obj, out exception);
        }

        public static T Deserialize(string xml)
        {
            System.IO.StringReader stringReader = null;
            try
            {
                stringReader = new System.IO.StringReader(xml);
                return ((T)(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader))));
            }
            finally
            {
                if ((stringReader != null))
                {
                    stringReader.Dispose();
                }
            }
        }

        /// <summary>
        /// Serializes current EntityBase object into file
        /// </summary>
        /// <param name="fileName">full path of outupt xml file</param>
        /// <param name="exception">output Exception value if failed</param>
        /// <returns>true if can serialize and save into file; otherwise, false</returns>
        public virtual bool SaveToFile(string fileName, out System.Exception exception)
        {
            exception = null;
            try
            {
                SaveToFile(fileName);
                return true;
            }
            catch (System.Exception e)
            {
                exception = e;
                return false;
            }
        }

        public virtual void SaveToFile(string fileName)
        {
            System.IO.StreamWriter streamWriter = null;
            try
            {
                string xmlString = Serialize();
                System.IO.FileInfo xmlFile = new System.IO.FileInfo(fileName);
                streamWriter = xmlFile.CreateText();
                streamWriter.WriteLine(xmlString);
                streamWriter.Close();
            }
            finally
            {
                if ((streamWriter != null))
                {
                    streamWriter.Dispose();
                }
            }
        }

        /// <summary>
        /// Deserializes xml markup from file into an EntityBase object
        /// </summary>
        /// <param name="fileName">string xml file to load and deserialize</param>
        /// <param name="obj">Output EntityBase object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool LoadFromFile(string fileName, out T obj, out System.Exception exception)
        {
            exception = null;
            obj = default(T);
            try
            {
                obj = LoadFromFile(fileName);
                return true;
            }
            catch (System.Exception ex)
            {
                exception = ex;
                return false;
            }
        }

        public static bool LoadFromFile(string fileName, out T obj)
        {
            System.Exception exception = null;
            return LoadFromFile(fileName, out obj, out exception);
        }

        public static T LoadFromFile(string fileName)
        {
            System.IO.FileStream file = null;
            System.IO.StreamReader sr = null;
            try
            {
                file = new System.IO.FileStream(fileName, FileMode.Open, FileAccess.Read);
                sr = new System.IO.StreamReader(file);
                string xmlString = sr.ReadToEnd();
                sr.Close();
                file.Close();
                return Deserialize(xmlString);
            }
            finally
            {
                if ((file != null))
                {
                    file.Dispose();
                }
                if ((sr != null))
                {
                    sr.Dispose();
                }
            }
        }
        #endregion
    }
    #endregion

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class TrackingResponse : EntityBase<TrackingResponse>
    {

        [EditorBrowsable(EditorBrowsableState.Never)]
        private string ref1Field;

        [EditorBrowsable(EditorBrowsableState.Never)]
        private string ref2Field;

        [EditorBrowsable(EditorBrowsableState.Never)]
        private AddressDetailsType senderDetailsField;

        [EditorBrowsable(EditorBrowsableState.Never)]
        private AddressDetailsType receiverDetailsField;

        [EditorBrowsable(EditorBrowsableState.Never)]
        private string consignmentNumberField;

        [EditorBrowsable(EditorBrowsableState.Never)]
        private string jobNumberField;

        [EditorBrowsable(EditorBrowsableState.Never)]
        private System.DateTime jobDateField;

        [EditorBrowsable(EditorBrowsableState.Never)]
        private TrackingResponseTrackingType trackingTypeField;

        [EditorBrowsable(EditorBrowsableState.Never)]
        private TrackingResponseEventCoordinates eventCoordinatesField;

        [EditorBrowsable(EditorBrowsableState.Never)]
        private System.DateTime eventDateTimeField;

        [EditorBrowsable(EditorBrowsableState.Never)]
        private string eventLocationField;

        [EditorBrowsable(EditorBrowsableState.Never)]
        private string etaField;

        [EditorBrowsable(EditorBrowsableState.Never)]
        private List<itemType> itemsScannedField;

        [EditorBrowsable(EditorBrowsableState.Never)]
        private List<object> itemsNotScannedField;

        [EditorBrowsable(EditorBrowsableState.Never)]
        private string accountCodeField;

        [EditorBrowsable(EditorBrowsableState.Never)]
        private CapBusiness capitalStateField;

        [EditorBrowsable(EditorBrowsableState.Never)]
        private JobModificationType jobModificationField;

        [EditorBrowsable(EditorBrowsableState.Never)]
        private PodInformationType pODUrlField;

        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Ref1
        {
            get
            {
                return this.ref1Field;
            }
            set
            {
                this.ref1Field = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Ref2
        {
            get
            {
                return this.ref2Field;
            }
            set
            {
                this.ref2Field = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public AddressDetailsType SenderDetails
        {
            get
            {
                return this.senderDetailsField;
            }
            set
            {
                this.senderDetailsField = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public AddressDetailsType ReceiverDetails
        {
            get
            {
                return this.receiverDetailsField;
            }
            set
            {
                this.receiverDetailsField = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ConsignmentNumber
        {
            get
            {
                return this.consignmentNumberField;
            }
            set
            {
                this.consignmentNumberField = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "positiveInteger")]
        public string JobNumber
        {
            get
            {
                return this.jobNumberField;
            }
            set
            {
                this.jobNumberField = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime JobDate
        {
            get
            {
                return this.jobDateField;
            }
            set
            {
                this.jobDateField = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public TrackingResponseTrackingType TrackingType
        {
            get
            {
                return this.trackingTypeField;
            }
            set
            {
                this.trackingTypeField = value;
            }
        }

        /// <summary>
        /// Coordinates are only supplied where available
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public TrackingResponseEventCoordinates EventCoordinates
        {
            get
            {
                return this.eventCoordinatesField;
            }
            set
            {
                this.eventCoordinatesField = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public System.DateTime EventDateTime
        {
            get
            {
                return this.eventDateTimeField;
            }
            set
            {
                this.eventDateTimeField = value;
            }
        }

        /// <summary>
        /// Reverese Geocoded Address for the event e.g. 153 Wellington Rd, Clayton VIC 3168, Australia
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string EventLocation
        {
            get
            {
                return this.eventLocationField;
            }
            set
            {
                this.eventLocationField = value;
            }
        }

        /// <summary>
        /// Eta for the Job. Etas for Permanent job are for the same day the job was allocated to the driver.
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Eta
        {
            get
            {
                return this.etaField;
            }
            set
            {
                this.etaField = value;
            }
        }

        /// <summary>
        /// Items scanned will only be present where items have been scanned on the job
        /// </summary>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("Item", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public List<itemType> ItemsScanned
        {
            get
            {
                return this.itemsScannedField;
            }
            set
            {
                this.itemsScannedField = value;
            }
        }

        /// <summary>
        /// Items not scanned will only be present where items have been scanned on the job and items have also not been present for a reason
        /// </summary>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("Item", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public List<object> ItemsNotScanned
        {
            get
            {
                return this.itemsNotScannedField;
            }
            set
            {
                this.itemsNotScannedField = value;
            }
        }

        /// <summary>
        /// Your account code will be provided to you by your Capital Transport representative
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string AccountCode
        {
            get
            {
                return this.accountCodeField;
            }
            set
            {
                this.accountCodeField = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CapBusiness CapitalState
        {
            get
            {
                return this.capitalStateField;
            }
            set
            {
                this.capitalStateField = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public JobModificationType JobModification
        {
            get
            {
                return this.jobModificationField;
            }
            set
            {
                this.jobModificationField = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public PodInformationType PODUrl
        {
            get
            {
                return this.pODUrlField;
            }
            set
            {
                this.pODUrlField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = true)]
    public partial class AddressDetailsType : EntityBase<AddressDetailsType>
    {

        [EditorBrowsable(EditorBrowsableState.Never)]
        private string addressLine1Field;

        [EditorBrowsable(EditorBrowsableState.Never)]
        private string addressLine2Field;

        [EditorBrowsable(EditorBrowsableState.Never)]
        private string suburbField;

        [EditorBrowsable(EditorBrowsableState.Never)]
        private string postcodeField;

        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string AddressLine1
        {
            get
            {
                return this.addressLine1Field;
            }
            set
            {
                this.addressLine1Field = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string AddressLine2
        {
            get
            {
                return this.addressLine2Field;
            }
            set
            {
                this.addressLine2Field = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Suburb
        {
            get
            {
                return this.suburbField;
            }
            set
            {
                this.suburbField = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "integer")]
        public string Postcode
        {
            get
            {
                return this.postcodeField;
            }
            set
            {
                this.postcodeField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = true)]
    public partial class PodInformationType : EntityBase<PodInformationType>
    {

        [EditorBrowsable(EditorBrowsableState.Never)]
        private string podJobNumberField;

        [EditorBrowsable(EditorBrowsableState.Never)]
        private string podSubJobNumberField;

        [EditorBrowsable(EditorBrowsableState.Never)]
        private string signatoriesNameField;

        [EditorBrowsable(EditorBrowsableState.Never)]
        private string base64ImageField;

        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string PodJobNumber
        {
            get
            {
                return this.podJobNumberField;
            }
            set
            {
                this.podJobNumberField = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string PodSubJobNumber
        {
            get
            {
                return this.podSubJobNumberField;
            }
            set
            {
                this.podSubJobNumberField = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string SignatoriesName
        {
            get
            {
                return this.signatoriesNameField;
            }
            set
            {
                this.signatoriesNameField = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Base64Image
        {
            get
            {
                return this.base64ImageField;
            }
            set
            {
                this.base64ImageField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = true)]
    public partial class JobModificationType : EntityBase<JobModificationType>
    {

        [EditorBrowsable(EditorBrowsableState.Never)]
        private string originalJobNumberField;

        [EditorBrowsable(EditorBrowsableState.Never)]
        private string modifiedJobNumberField;

        [EditorBrowsable(EditorBrowsableState.Never)]
        private string modificationNotesField;

        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string OriginalJobNumber
        {
            get
            {
                return this.originalJobNumberField;
            }
            set
            {
                this.originalJobNumberField = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ModifiedJobNumber
        {
            get
            {
                return this.modifiedJobNumberField;
            }
            set
            {
                this.modifiedJobNumberField = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ModificationNotes
        {
            get
            {
                return this.modificationNotesField;
            }
            set
            {
                this.modificationNotesField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = true)]
    public partial class itemType : EntityBase<itemType>
    {

        [EditorBrowsable(EditorBrowsableState.Never)]
        private string itmDescriptionField;

        [EditorBrowsable(EditorBrowsableState.Never)]
        private decimal itmLengthField;

        [EditorBrowsable(EditorBrowsableState.Never)]
        private bool itmLengthFieldSpecified;

        [EditorBrowsable(EditorBrowsableState.Never)]
        private decimal itmWidthField;

        [EditorBrowsable(EditorBrowsableState.Never)]
        private bool itmWidthFieldSpecified;

        [EditorBrowsable(EditorBrowsableState.Never)]
        private decimal itmHeightField;

        [EditorBrowsable(EditorBrowsableState.Never)]
        private bool itmHeightFieldSpecified;

        [EditorBrowsable(EditorBrowsableState.Never)]
        private decimal itmWeightField;

        [EditorBrowsable(EditorBrowsableState.Never)]
        private bool itmWeightFieldSpecified;

        [EditorBrowsable(EditorBrowsableState.Never)]
        private decimal itmCubicField;

        [EditorBrowsable(EditorBrowsableState.Never)]
        private bool itmCubicFieldSpecified;

        [EditorBrowsable(EditorBrowsableState.Never)]
        private string itmBarcodeField;

        [EditorBrowsable(EditorBrowsableState.Never)]
        private string itmScanReasonField;

        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string itmDescription
        {
            get
            {
                return this.itmDescriptionField;
            }
            set
            {
                this.itmDescriptionField = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal itmLength
        {
            get
            {
                return this.itmLengthField;
            }
            set
            {
                this.itmLengthField = value;
            }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool itmLengthSpecified
        {
            get
            {
                return this.itmLengthFieldSpecified;
            }
            set
            {
                this.itmLengthFieldSpecified = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal itmWidth
        {
            get
            {
                return this.itmWidthField;
            }
            set
            {
                this.itmWidthField = value;
            }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool itmWidthSpecified
        {
            get
            {
                return this.itmWidthFieldSpecified;
            }
            set
            {
                this.itmWidthFieldSpecified = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal itmHeight
        {
            get
            {
                return this.itmHeightField;
            }
            set
            {
                this.itmHeightField = value;
            }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool itmHeightSpecified
        {
            get
            {
                return this.itmHeightFieldSpecified;
            }
            set
            {
                this.itmHeightFieldSpecified = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal itmWeight
        {
            get
            {
                return this.itmWeightField;
            }
            set
            {
                this.itmWeightField = value;
            }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool itmWeightSpecified
        {
            get
            {
                return this.itmWeightFieldSpecified;
            }
            set
            {
                this.itmWeightFieldSpecified = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal itmCubic
        {
            get
            {
                return this.itmCubicField;
            }
            set
            {
                this.itmCubicField = value;
            }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool itmCubicSpecified
        {
            get
            {
                return this.itmCubicFieldSpecified;
            }
            set
            {
                this.itmCubicFieldSpecified = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string itmBarcode
        {
            get
            {
                return this.itmBarcodeField;
            }
            set
            {
                this.itmBarcodeField = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string itmScanReason
        {
            get
            {
                return this.itmScanReasonField;
            }
            set
            {
                this.itmScanReasonField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public enum TrackingResponseTrackingType
    {

        /// <remarks/>
        Booked,

        /// <remarks/>
        ReBooked,

        /// <remarks/>
        JobModified,

        /// <remarks/>
        PickupArrive,

        /// <remarks/>
        PickupComplete,

        /// <remarks/>
        DeliveryArrive,

        /// <remarks/>
        DeliveryComplete,

        /// <remarks/>
        Cancelled,
    }

    /// <summary>
    /// Coordinates are only supplied where available
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class TrackingResponseEventCoordinates : EntityBase<TrackingResponseEventCoordinates>
    {

        [EditorBrowsable(EditorBrowsableState.Never)]
        private object latitudeField;

        [EditorBrowsable(EditorBrowsableState.Never)]
        private object longitudeField;

        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public object Latitude
        {
            get
            {
                return this.latitudeField;
            }
            set
            {
                this.latitudeField = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public object Longitude
        {
            get
            {
                return this.longitudeField;
            }
            set
            {
                this.longitudeField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public enum CapBusiness
    {

        /// <remarks/>
        VIC,

        /// <remarks/>
        WA,

        /// <remarks/>
        SA,

        /// <remarks/>
        NSW,

        /// <remarks/>
        QLD,

        /// <remarks/>
        ACT,
    }
}
