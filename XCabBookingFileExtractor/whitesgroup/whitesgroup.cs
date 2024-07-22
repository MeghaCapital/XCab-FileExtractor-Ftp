namespace xmlshredder.whitesgroup
{
    using System.Collections.Generic;


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Xsd2", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class Message
    {

        private MessageMessageHeader messageHeaderField;

        private MessageMessageBody messageBodyField;

        /// <remarks/>
        public MessageMessageHeader MessageHeader
        {
            get
            {
                return this.messageHeaderField;
            }
            set
            {
                this.messageHeaderField = value;
            }
        }

        /// <remarks/>
        public MessageMessageBody MessageBody
        {
            get
            {
                return this.messageBodyField;
            }
            set
            {
                this.messageBodyField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Xsd2", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class MessageMessageHeader
    {

        private string senderIDField;

        private string recipientIDField;

        private System.DateTime preparedField;

        private string messageIDField;

        private string messageTypeField;

        private string messageVersionField;

        /// <remarks/>
        public string SenderID
        {
            get
            {
                return this.senderIDField;
            }
            set
            {
                this.senderIDField = value;
            }
        }

        /// <remarks/>
        public string RecipientID
        {
            get
            {
                return this.recipientIDField;
            }
            set
            {
                this.recipientIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime Prepared
        {
            get
            {
                return this.preparedField;
            }
            set
            {
                this.preparedField = value;
            }
        }

        /// <remarks/>
        public string MessageID
        {
            get
            {
                return this.messageIDField;
            }
            set
            {
                this.messageIDField = value;
            }
        }

        /// <remarks/>
        public string MessageType
        {
            get
            {
                return this.messageTypeField;
            }
            set
            {
                this.messageTypeField = value;
            }
        }

        /// <remarks/>
        public string MessageVersion
        {
            get
            {
                return this.messageVersionField;
            }
            set
            {
                this.messageVersionField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Xsd2", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class MessageMessageBody
    {

        private MessageMessageBodyManifestHeader manifestHeaderField;

        private MessageMessageBodyConsignments consignmentsField;

        private MessageMessageBodyControlTotal controlTotalField;

        /// <remarks/>
        public MessageMessageBodyManifestHeader ManifestHeader
        {
            get
            {
                return this.manifestHeaderField;
            }
            set
            {
                this.manifestHeaderField = value;
            }
        }

        /// <remarks/>
        public MessageMessageBodyConsignments Consignments
        {
            get
            {
                return this.consignmentsField;
            }
            set
            {
                this.consignmentsField = value;
            }
        }

        /// <remarks/>
        public MessageMessageBodyControlTotal ControlTotal
        {
            get
            {
                return this.controlTotalField;
            }
            set
            {
                this.controlTotalField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Xsd2", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class MessageMessageBodyManifestHeader
    {

        private MessageMessageBodyManifestHeaderParty partyField;

        /// <remarks/>
        public MessageMessageBodyManifestHeaderParty Party
        {
            get
            {
                return this.partyField;
            }
            set
            {
                this.partyField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Xsd2", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class MessageMessageBodyManifestHeaderParty
    {

        private string codeField;

        private string nameField;

        private string address1Field;

        private string suburbField;

        private string cityField;

        private int postCodeField;

        private string stateField;

        private object phoneField;

        private string roleField;

        /// <remarks/>
        public string Code
        {
            get
            {
                return this.codeField;
            }
            set
            {
                this.codeField = value;
            }
        }

        /// <remarks/>
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        public string Address1
        {
            get
            {
                return this.address1Field;
            }
            set
            {
                this.address1Field = value;
            }
        }

        /// <remarks/>
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

        /// <remarks/>
        public string City
        {
            get
            {
                return this.cityField;
            }
            set
            {
                this.cityField = value;
            }
        }

        /// <remarks/>
        public int PostCode
        {
            get
            {
                return this.postCodeField;
            }
            set
            {
                this.postCodeField = value;
            }
        }

        /// <remarks/>
        public string State
        {
            get
            {
                return this.stateField;
            }
            set
            {
                this.stateField = value;
            }
        }

        /// <remarks/>
        public object Phone
        {
            get
            {
                return this.phoneField;
            }
            set
            {
                this.phoneField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Role
        {
            get
            {
                return this.roleField;
            }
            set
            {
                this.roleField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Xsd2", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class MessageMessageBodyConsignments
    {

        private MessageMessageBodyConsignmentsConsignment consignmentField;

        /// <remarks/>
        public MessageMessageBodyConsignmentsConsignment Consignment
        {
            get
            {
                return this.consignmentField;
            }
            set
            {
                this.consignmentField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Xsd2", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class MessageMessageBodyConsignmentsConsignment
    {

        private string consignmentNoField;

        private System.DateTime consignmentDateField;

        private List<MessageMessageBodyConsignmentsConsignmentParty> partyField;

        private string reference1Field;

        private string reference2Field;

        private string serviceRequiredField;

        private object contactField;

        private List<MessageMessageBodyConsignmentsConsignmentTotal> totalField;

        private MessageMessageBodyConsignmentsConsignmentLine lineField;

        private object hireField;

        private MessageMessageBodyConsignmentsConsignmentReferences referencesField;

        /// <remarks/>
        public string ConsignmentNo
        {
            get
            {
                return this.consignmentNoField;
            }
            set
            {
                this.consignmentNoField = value;
            }
        }

        /// <remarks/>
        public System.DateTime ConsignmentDate
        {
            get
            {
                return this.consignmentDateField;
            }
            set
            {
                this.consignmentDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Party")]
        public List<MessageMessageBodyConsignmentsConsignmentParty> Party
        {
            get
            {
                return this.partyField;
            }
            set
            {
                this.partyField = value;
            }
        }

        /// <remarks/>
        public string Reference1
        {
            get
            {
                return this.reference1Field;
            }
            set
            {
                this.reference1Field = value;
            }
        }

        /// <remarks/>
        public string Reference2
        {
            get
            {
                return this.reference2Field;
            }
            set
            {
                this.reference2Field = value;
            }
        }

        /// <remarks/>
        public string ServiceRequired
        {
            get
            {
                return this.serviceRequiredField;
            }
            set
            {
                this.serviceRequiredField = value;
            }
        }

        /// <remarks/>
        public object Contact
        {
            get
            {
                return this.contactField;
            }
            set
            {
                this.contactField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Total")]
        public List<MessageMessageBodyConsignmentsConsignmentTotal> Total
        {
            get
            {
                return this.totalField;
            }
            set
            {
                this.totalField = value;
            }
        }

        /// <remarks/>
        public MessageMessageBodyConsignmentsConsignmentLine Line
        {
            get
            {
                return this.lineField;
            }
            set
            {
                this.lineField = value;
            }
        }

        /// <remarks/>
        public object Hire
        {
            get
            {
                return this.hireField;
            }
            set
            {
                this.hireField = value;
            }
        }

        /// <remarks/>
        public MessageMessageBodyConsignmentsConsignmentReferences References
        {
            get
            {
                return this.referencesField;
            }
            set
            {
                this.referencesField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Xsd2", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class MessageMessageBodyConsignmentsConsignmentParty
    {

        private string codeField;

        private string nameField;

        private string address1Field;

        private string suburbField;

        private string cityField;

        private string postCodeField;

        private string stateField;

        private object phoneField;

        private string roleField;

        /// <remarks/>
        public string Code
        {
            get
            {
                return this.codeField;
            }
            set
            {
                this.codeField = value;
            }
        }

        /// <remarks/>
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        public string Address1
        {
            get
            {
                return this.address1Field;
            }
            set
            {
                this.address1Field = value;
            }
        }

        /// <remarks/>
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

        /// <remarks/>
        public string City
        {
            get
            {
                return this.cityField;
            }
            set
            {
                this.cityField = value;
            }
        }

        /// <remarks/>
        public string PostCode
        {
            get
            {
                return this.postCodeField;
            }
            set
            {
                this.postCodeField = value;
            }
        }

        /// <remarks/>
        public string State
        {
            get
            {
                return this.stateField;
            }
            set
            {
                this.stateField = value;
            }
        }

        /// <remarks/>
        public object Phone
        {
            get
            {
                return this.phoneField;
            }
            set
            {
                this.phoneField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Role
        {
            get
            {
                return this.roleField;
            }
            set
            {
                this.roleField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Xsd2", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class MessageMessageBodyConsignmentsConsignmentTotal
    {

        private string unitsField;

        private decimal valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Units
        {
            get
            {
                return this.unitsField;
            }
            set
            {
                this.unitsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public decimal Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Xsd2", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class MessageMessageBodyConsignmentsConsignmentLine
    {

        private int itemsField;

        private string packageTypeField;

        private string descriptionField;

        private List<MessageMessageBodyConsignmentsConsignmentLineMeasurement> measurementField;

        private MessageMessageBodyConsignmentsConsignmentLineDimensions dimensionsField;

        /// <remarks/>
        public int Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }

        /// <remarks/>
        public string PackageType
        {
            get
            {
                return this.packageTypeField;
            }
            set
            {
                this.packageTypeField = value;
            }
        }

        /// <remarks/>
        public string Description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Measurement")]
        public List<MessageMessageBodyConsignmentsConsignmentLineMeasurement> Measurement
        {
            get
            {
                return this.measurementField;
            }
            set
            {
                this.measurementField = value;
            }
        }

        /// <remarks/>
        public MessageMessageBodyConsignmentsConsignmentLineDimensions Dimensions
        {
            get
            {
                return this.dimensionsField;
            }
            set
            {
                this.dimensionsField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Xsd2", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class MessageMessageBodyConsignmentsConsignmentLineMeasurement
    {

        private decimal valueField;

        private string unitField;

        private string propertyField;

        /// <remarks/>
        public decimal Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }

        /// <remarks/>
        public string Unit
        {
            get
            {
                return this.unitField;
            }
            set
            {
                this.unitField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Property
        {
            get
            {
                return this.propertyField;
            }
            set
            {
                this.propertyField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Xsd2", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class MessageMessageBodyConsignmentsConsignmentLineDimensions
    {

        private int quantityField;

        private decimal lengthField;

        private decimal widthField;

        private decimal heightField;

        /// <remarks/>
        public int Quantity
        {
            get
            {
                return this.quantityField;
            }
            set
            {
                this.quantityField = value;
            }
        }

        /// <remarks/>
        public decimal Length
        {
            get
            {
                return this.lengthField;
            }
            set
            {
                this.lengthField = value;
            }
        }

        /// <remarks/>
        public decimal Width
        {
            get
            {
                return this.widthField;
            }
            set
            {
                this.widthField = value;
            }
        }

        /// <remarks/>
        public decimal Height
        {
            get
            {
                return this.heightField;
            }
            set
            {
                this.heightField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Xsd2", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class MessageMessageBodyConsignmentsConsignmentReferences
    {

        private MessageMessageBodyConsignmentsConsignmentReferencesReference referenceField;

        /// <remarks/>
        public MessageMessageBodyConsignmentsConsignmentReferencesReference Reference
        {
            get
            {
                return this.referenceField;
            }
            set
            {
                this.referenceField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Xsd2", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class MessageMessageBodyConsignmentsConsignmentReferencesReference
    {

        private string typeField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Xsd2", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class MessageMessageBodyControlTotal
    {

        private MessageMessageBodyControlTotalTotal totalField;

        /// <remarks/>
        public MessageMessageBodyControlTotalTotal Total
        {
            get
            {
                return this.totalField;
            }
            set
            {
                this.totalField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Xsd2", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class MessageMessageBodyControlTotalTotal
    {

        private string unitsField;

        private int valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Units
        {
            get
            {
                return this.unitsField;
            }
            set
            {
                this.unitsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public int Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }
}
