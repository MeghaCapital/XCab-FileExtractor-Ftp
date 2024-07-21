namespace Data.Api.Bookings.Basf;
// NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
public partial class YCOSDELVRY02
{

    private YCOSDELVRY02IDOC iDOCField;

    /// <remarks/>
    public YCOSDELVRY02IDOC IDOC
    {
        get
        {
            return this.iDOCField;
        }
        set
        {
            this.iDOCField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class YCOSDELVRY02IDOC
{

    private YCOSDELVRY02IDOCEDI_DC40 eDI_DC40Field;

    private YCOSDELVRY02IDOCE1EDL20 e1EDL20Field;

    private byte bEGINField;

    /// <remarks/>
    public YCOSDELVRY02IDOCEDI_DC40 EDI_DC40
    {
        get
        {
            return this.eDI_DC40Field;
        }
        set
        {
            this.eDI_DC40Field = value;
        }
    }

    /// <remarks/>
    public YCOSDELVRY02IDOCE1EDL20 E1EDL20
    {
        get
        {
            return this.e1EDL20Field;
        }
        set
        {
            this.e1EDL20Field = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte BEGIN
    {
        get
        {
            return this.bEGINField;
        }
        set
        {
            this.bEGINField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class YCOSDELVRY02IDOCEDI_DC40
{

    private string tABNAMField;

    private byte mANDTField;

    private ulong dOCNUMField;

    private ushort dOCRELField;

    private byte sTATUSField;

    private byte dIRECTField;

    private byte oUTMODField;

    private string iDOCTYPField;

    private string cIMTYPField;

    private string mESTYPField;

    private string sNDPORField;

    private string sNDPRTField;

    private string sNDPRNField;

    private string rCVPORField;

    private string rCVPRTField;

    private string rCVPRNField;

    private uint rCVLADField;

    private uint cREDATField;

    private ushort cRETIMField;

    private ulong sERIALField;

    private byte sEGMENTField;

    /// <remarks/>
    public string TABNAM
    {
        get
        {
            return this.tABNAMField;
        }
        set
        {
            this.tABNAMField = value;
        }
    }

    /// <remarks/>
    public byte MANDT
    {
        get
        {
            return this.mANDTField;
        }
        set
        {
            this.mANDTField = value;
        }
    }

    /// <remarks/>
    public ulong DOCNUM
    {
        get
        {
            return this.dOCNUMField;
        }
        set
        {
            this.dOCNUMField = value;
        }
    }

    /// <remarks/>
    public ushort DOCREL
    {
        get
        {
            return this.dOCRELField;
        }
        set
        {
            this.dOCRELField = value;
        }
    }

    /// <remarks/>
    public byte STATUS
    {
        get
        {
            return this.sTATUSField;
        }
        set
        {
            this.sTATUSField = value;
        }
    }

    /// <remarks/>
    public byte DIRECT
    {
        get
        {
            return this.dIRECTField;
        }
        set
        {
            this.dIRECTField = value;
        }
    }

    /// <remarks/>
    public byte OUTMOD
    {
        get
        {
            return this.oUTMODField;
        }
        set
        {
            this.oUTMODField = value;
        }
    }

    /// <remarks/>
    public string IDOCTYP
    {
        get
        {
            return this.iDOCTYPField;
        }
        set
        {
            this.iDOCTYPField = value;
        }
    }

    /// <remarks/>
    public string CIMTYP
    {
        get
        {
            return this.cIMTYPField;
        }
        set
        {
            this.cIMTYPField = value;
        }
    }

    /// <remarks/>
    public string MESTYP
    {
        get
        {
            return this.mESTYPField;
        }
        set
        {
            this.mESTYPField = value;
        }
    }

    /// <remarks/>
    public string SNDPOR
    {
        get
        {
            return this.sNDPORField;
        }
        set
        {
            this.sNDPORField = value;
        }
    }

    /// <remarks/>
    public string SNDPRT
    {
        get
        {
            return this.sNDPRTField;
        }
        set
        {
            this.sNDPRTField = value;
        }
    }

    /// <remarks/>
    public string SNDPRN
    {
        get
        {
            return this.sNDPRNField;
        }
        set
        {
            this.sNDPRNField = value;
        }
    }

    /// <remarks/>
    public string RCVPOR
    {
        get
        {
            return this.rCVPORField;
        }
        set
        {
            this.rCVPORField = value;
        }
    }

    /// <remarks/>
    public string RCVPRT
    {
        get
        {
            return this.rCVPRTField;
        }
        set
        {
            this.rCVPRTField = value;
        }
    }

    /// <remarks/>
    public string RCVPRN
    {
        get
        {
            return this.rCVPRNField;
        }
        set
        {
            this.rCVPRNField = value;
        }
    }

    /// <remarks/>
    public uint RCVLAD
    {
        get
        {
            return this.rCVLADField;
        }
        set
        {
            this.rCVLADField = value;
        }
    }

    /// <remarks/>
    public uint CREDAT
    {
        get
        {
            return this.cREDATField;
        }
        set
        {
            this.cREDATField = value;
        }
    }

    /// <remarks/>
    public ushort CRETIM
    {
        get
        {
            return this.cRETIMField;
        }
        set
        {
            this.cRETIMField = value;
        }
    }

    /// <remarks/>
    public ulong SERIAL
    {
        get
        {
            return this.sERIALField;
        }
        set
        {
            this.sERIALField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte SEGMENT
    {
        get
        {
            return this.sEGMENTField;
        }
        set
        {
            this.sEGMENTField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class YCOSDELVRY02IDOCE1EDL20
{

    private ulong vBELNField;

    private string vSTELField;

    private string vKORGField;

    private string aBLADField;

    private string iNCO1Field;

    private string iNCO2Field;

    private string rOUTEField;

    private string vSBEDField;

    private decimal bTGEWField;

    private decimal nTGEWField;

    private string gEWEIField;

    private decimal vOLUMField;

    private byte aNZPKField;

    private uint pODATField;

    private uint pOTIMField;

    private YCOSDELVRY02IDOCE1EDL20E1EDL22 e1EDL22Field;

    private YCOSDELVRY02IDOCE1EDL20E1EDL21 e1EDL21Field;

    private YCOSDELVRY02IDOCE1EDL20E1EDL18 e1EDL18Field;

    private YCOSDELVRY02IDOCE1EDL20E1ADRM1[] e1ADRM1Field;

    private YCOSDELVRY02IDOCE1EDL20E1EDT13[] e1EDT13Field;

    private YCOSDELVRY02IDOCE1EDL20E1TXTH8 e1TXTH8Field;

    private YCOSDELVRY02IDOCE1EDL20E1EDDH2 e1EDDH2Field;

    private YCOSDELVRY02IDOCE1EDL20E1EDL28 e1EDL28Field;

    private YCOSDELVRY02IDOCE1EDL20E1EDL24[] e1EDL24Field;

    private byte sEGMENTField;

    /// <remarks/>
    public ulong VBELN
    {
        get
        {
            return this.vBELNField;
        }
        set
        {
            this.vBELNField = value;
        }
    }

    /// <remarks/>
    public string VSTEL
    {
        get
        {
            return this.vSTELField;
        }
        set
        {
            this.vSTELField = value;
        }
    }

    /// <remarks/>
    public string VKORG
    {
        get
        {
            return this.vKORGField;
        }
        set
        {
            this.vKORGField = value;
        }
    }

    /// <remarks/>
    public string ABLAD
    {
        get
        {
            return this.aBLADField;
        }
        set
        {
            this.aBLADField = value;
        }
    }

    /// <remarks/>
    public string INCO1
    {
        get
        {
            return this.iNCO1Field;
        }
        set
        {
            this.iNCO1Field = value;
        }
    }

    /// <remarks/>
    public string INCO2
    {
        get
        {
            return this.iNCO2Field;
        }
        set
        {
            this.iNCO2Field = value;
        }
    }

    /// <remarks/>
    public string ROUTE
    {
        get
        {
            return this.rOUTEField;
        }
        set
        {
            this.rOUTEField = value;
        }
    }

    /// <remarks/>
    public string VSBED
    {
        get
        {
            return this.vSBEDField;
        }
        set
        {
            this.vSBEDField = value;
        }
    }

    /// <remarks/>
    public decimal BTGEW
    {
        get
        {
            return this.bTGEWField;
        }
        set
        {
            this.bTGEWField = value;
        }
    }

    /// <remarks/>
    public decimal NTGEW
    {
        get
        {
            return this.nTGEWField;
        }
        set
        {
            this.nTGEWField = value;
        }
    }

    /// <remarks/>
    public string GEWEI
    {
        get
        {
            return this.gEWEIField;
        }
        set
        {
            this.gEWEIField = value;
        }
    }

    /// <remarks/>
    public decimal VOLUM
    {
        get
        {
            return this.vOLUMField;
        }
        set
        {
            this.vOLUMField = value;
        }
    }

    /// <remarks/>
    public byte ANZPK
    {
        get
        {
            return this.aNZPKField;
        }
        set
        {
            this.aNZPKField = value;
        }
    }

    /// <remarks/>
    public uint PODAT
    {
        get
        {
            return this.pODATField;
        }
        set
        {
            this.pODATField = value;
        }
    }

    /// <remarks/>
    public uint POTIM
    {
        get
        {
            return this.pOTIMField;
        }
        set
        {
            this.pOTIMField = value;
        }
    }

    /// <remarks/>
    public YCOSDELVRY02IDOCE1EDL20E1EDL22 E1EDL22
    {
        get
        {
            return this.e1EDL22Field;
        }
        set
        {
            this.e1EDL22Field = value;
        }
    }

    /// <remarks/>
    public YCOSDELVRY02IDOCE1EDL20E1EDL21 E1EDL21
    {
        get
        {
            return this.e1EDL21Field;
        }
        set
        {
            this.e1EDL21Field = value;
        }
    }

    /// <remarks/>
    public YCOSDELVRY02IDOCE1EDL20E1EDL18 E1EDL18
    {
        get
        {
            return this.e1EDL18Field;
        }
        set
        {
            this.e1EDL18Field = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("E1ADRM1")]
    public YCOSDELVRY02IDOCE1EDL20E1ADRM1[] E1ADRM1
    {
        get
        {
            return this.e1ADRM1Field;
        }
        set
        {
            this.e1ADRM1Field = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("E1EDT13")]
    public YCOSDELVRY02IDOCE1EDL20E1EDT13[] E1EDT13
    {
        get
        {
            return this.e1EDT13Field;
        }
        set
        {
            this.e1EDT13Field = value;
        }
    }

    /// <remarks/>
    public YCOSDELVRY02IDOCE1EDL20E1TXTH8 E1TXTH8
    {
        get
        {
            return this.e1TXTH8Field;
        }
        set
        {
            this.e1TXTH8Field = value;
        }
    }

    /// <remarks/>
    public YCOSDELVRY02IDOCE1EDL20E1EDDH2 E1EDDH2
    {
        get
        {
            return this.e1EDDH2Field;
        }
        set
        {
            this.e1EDDH2Field = value;
        }
    }

    /// <remarks/>
    public YCOSDELVRY02IDOCE1EDL20E1EDL28 E1EDL28
    {
        get
        {
            return this.e1EDL28Field;
        }
        set
        {
            this.e1EDL28Field = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("E1EDL24")]
    public YCOSDELVRY02IDOCE1EDL20E1EDL24[] E1EDL24
    {
        get
        {
            return this.e1EDL24Field;
        }
        set
        {
            this.e1EDL24Field = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte SEGMENT
    {
        get
        {
            return this.sEGMENTField;
        }
        set
        {
            this.sEGMENTField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class YCOSDELVRY02IDOCE1EDL20E1EDL22
{

    private string vSTEL_BEZField;

    private string vKORG_BEZField;

    private string iNCO1_BEZField;

    private string rOUTE_BEZField;

    private string vSBED_BEZField;

    private byte sEGMENTField;

    /// <remarks/>
    public string VSTEL_BEZ
    {
        get
        {
            return this.vSTEL_BEZField;
        }
        set
        {
            this.vSTEL_BEZField = value;
        }
    }

    /// <remarks/>
    public string VKORG_BEZ
    {
        get
        {
            return this.vKORG_BEZField;
        }
        set
        {
            this.vKORG_BEZField = value;
        }
    }

    /// <remarks/>
    public string INCO1_BEZ
    {
        get
        {
            return this.iNCO1_BEZField;
        }
        set
        {
            this.iNCO1_BEZField = value;
        }
    }

    /// <remarks/>
    public string ROUTE_BEZ
    {
        get
        {
            return this.rOUTE_BEZField;
        }
        set
        {
            this.rOUTE_BEZField = value;
        }
    }

    /// <remarks/>
    public string VSBED_BEZ
    {
        get
        {
            return this.vSBED_BEZField;
        }
        set
        {
            this.vSBED_BEZField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte SEGMENT
    {
        get
        {
            return this.sEGMENTField;
        }
        set
        {
            this.sEGMENTField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class YCOSDELVRY02IDOCE1EDL20E1EDL21
{

    private string lFARTField;

    private byte lPRIOField;

    private byte tRAGRField;

    private YCOSDELVRY02IDOCE1EDL20E1EDL21E1EDL23 e1EDL23Field;

    private byte sEGMENTField;

    /// <remarks/>
    public string LFART
    {
        get
        {
            return this.lFARTField;
        }
        set
        {
            this.lFARTField = value;
        }
    }

    /// <remarks/>
    public byte LPRIO
    {
        get
        {
            return this.lPRIOField;
        }
        set
        {
            this.lPRIOField = value;
        }
    }

    /// <remarks/>
    public byte TRAGR
    {
        get
        {
            return this.tRAGRField;
        }
        set
        {
            this.tRAGRField = value;
        }
    }

    /// <remarks/>
    public YCOSDELVRY02IDOCE1EDL20E1EDL21E1EDL23 E1EDL23
    {
        get
        {
            return this.e1EDL23Field;
        }
        set
        {
            this.e1EDL23Field = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte SEGMENT
    {
        get
        {
            return this.sEGMENTField;
        }
        set
        {
            this.sEGMENTField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class YCOSDELVRY02IDOCE1EDL20E1EDL21E1EDL23
{

    private string lFART_BEZField;

    private string tRAGR_BEZField;

    private byte sEGMENTField;

    /// <remarks/>
    public string LFART_BEZ
    {
        get
        {
            return this.lFART_BEZField;
        }
        set
        {
            this.lFART_BEZField = value;
        }
    }

    /// <remarks/>
    public string TRAGR_BEZ
    {
        get
        {
            return this.tRAGR_BEZField;
        }
        set
        {
            this.tRAGR_BEZField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte SEGMENT
    {
        get
        {
            return this.sEGMENTField;
        }
        set
        {
            this.sEGMENTField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class YCOSDELVRY02IDOCE1EDL20E1EDL18
{

    private string qUALFField;

    private byte sEGMENTField;

    /// <remarks/>
    public string QUALF
    {
        get
        {
            return this.qUALFField;
        }
        set
        {
            this.qUALFField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte SEGMENT
    {
        get
        {
            return this.sEGMENTField;
        }
        set
        {
            this.sEGMENTField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class YCOSDELVRY02IDOCE1EDL20E1ADRM1
{

    private string pARTNER_QField;

    private string pARTNER_IDField;

    private string lANGUAGEField;

    private string fORMOFADDRField;

    private string nAME1Field;

    private string sTREET1Field;

    private byte hOUSE_SUPLField;

    private bool hOUSE_SUPLFieldSpecified;

    private uint pOSTL_COD1Field;

    private bool pOSTL_COD1FieldSpecified;

    private string cITY1Field;

    private ushort pOSTL_PBOXField;

    private bool pOSTL_PBOXFieldSpecified;

    private ushort pOSTL_COD2Field;

    private bool pOSTL_COD2FieldSpecified;

    private string pOSTL_CITYField;

    private string nAME_TEXTField;

    private string tELEPHONE1Field;

    private string tELEPHONE2Field;

    private bool tELEPHONE2FieldSpecified;

    private string tELEFAXField;

    private string e_MAILField;

    private string cOUNTRY1Field;

    private string rEGIONField;

    private byte sEGMENTField;

    /// <remarks/>
    public string PARTNER_Q
    {
        get
        {
            return this.pARTNER_QField;
        }
        set
        {
            this.pARTNER_QField = value;
        }
    }

    /// <remarks/>
    public string PARTNER_ID
    {
        get
        {
            return this.pARTNER_IDField;
        }
        set
        {
            this.pARTNER_IDField = value;
        }
    }

    /// <remarks/>
    public string LANGUAGE
    {
        get
        {
            return this.lANGUAGEField;
        }
        set
        {
            this.lANGUAGEField = value;
        }
    }

    /// <remarks/>
    public string FORMOFADDR
    {
        get
        {
            return this.fORMOFADDRField;
        }
        set
        {
            this.fORMOFADDRField = value;
        }
    }

    /// <remarks/>
    public string NAME1
    {
        get
        {
            return this.nAME1Field;
        }
        set
        {
            this.nAME1Field = value;
        }
    }

    /// <remarks/>
    public string STREET1
    {
        get
        {
            return this.sTREET1Field;
        }
        set
        {
            this.sTREET1Field = value;
        }
    }

    /// <remarks/>
    public byte HOUSE_SUPL
    {
        get
        {
            return this.hOUSE_SUPLField;
        }
        set
        {
            this.hOUSE_SUPLField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool HOUSE_SUPLSpecified
    {
        get
        {
            return this.hOUSE_SUPLFieldSpecified;
        }
        set
        {
            this.hOUSE_SUPLFieldSpecified = value;
        }
    }

    /// <remarks/>
    public uint POSTL_COD1
    {
        get
        {
            return this.pOSTL_COD1Field;
        }
        set
        {
            this.pOSTL_COD1Field = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool POSTL_COD1Specified
    {
        get
        {
            return this.pOSTL_COD1FieldSpecified;
        }
        set
        {
            this.pOSTL_COD1FieldSpecified = value;
        }
    }

    /// <remarks/>
    public string CITY1
    {
        get
        {
            return this.cITY1Field;
        }
        set
        {
            this.cITY1Field = value;
        }
    }

    /// <remarks/>
    public ushort POSTL_PBOX
    {
        get
        {
            return this.pOSTL_PBOXField;
        }
        set
        {
            this.pOSTL_PBOXField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool POSTL_PBOXSpecified
    {
        get
        {
            return this.pOSTL_PBOXFieldSpecified;
        }
        set
        {
            this.pOSTL_PBOXFieldSpecified = value;
        }
    }

    /// <remarks/>
    public ushort POSTL_COD2
    {
        get
        {
            return this.pOSTL_COD2Field;
        }
        set
        {
            this.pOSTL_COD2Field = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool POSTL_COD2Specified
    {
        get
        {
            return this.pOSTL_COD2FieldSpecified;
        }
        set
        {
            this.pOSTL_COD2FieldSpecified = value;
        }
    }

    /// <remarks/>
    public string POSTL_CITY
    {
        get
        {
            return this.pOSTL_CITYField;
        }
        set
        {
            this.pOSTL_CITYField = value;
        }
    }

    /// <remarks/>
    public string NAME_TEXT
    {
        get
        {
            return this.nAME_TEXTField;
        }
        set
        {
            this.nAME_TEXTField = value;
        }
    }

    /// <remarks/>
    public string TELEPHONE1
    {
        get
        {
            return this.tELEPHONE1Field;
        }
        set
        {
            this.tELEPHONE1Field = value;
        }
    }
    public string TELEPHONE2
    {
        get
        {
            return this.tELEPHONE2Field;
        }
        set
        {
            this.tELEPHONE2Field = value;
        }
    }
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool TELEPHONE2Specified
    {
        get
        {
            return this.tELEPHONE2FieldSpecified;
        }
        set
        {
            this.tELEPHONE2FieldSpecified = value;
        }
    }

    /// <remarks/>
    public string TELEFAX
    {
        get
        {
            return this.tELEFAXField;
        }
        set
        {
            this.tELEFAXField = value;
        }
    }

    /// <remarks/>
    public string E_MAIL
    {
        get
        {
            return this.e_MAILField;
        }
        set
        {
            this.e_MAILField = value;
        }
    }

    /// <remarks/>
    public string COUNTRY1
    {
        get
        {
            return this.cOUNTRY1Field;
        }
        set
        {
            this.cOUNTRY1Field = value;
        }
    }

    /// <remarks/>
    public string REGION
    {
        get
        {
            return this.rEGIONField;
        }
        set
        {
            this.rEGIONField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte SEGMENT
    {
        get
        {
            return this.sEGMENTField;
        }
        set
        {
            this.sEGMENTField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class YCOSDELVRY02IDOCE1EDL20E1EDT13
{

    private byte qUALFField;

    private uint nTANFField;

    private byte nTANZField;

    private uint nTENDField;

    private byte nTENZField;

    private string tZONE_BEGField;

    private byte iSDDField;

    private byte iSDZField;

    private byte iEDDField;

    private byte iEDZField;

    private string tZONE_ENDField;

    private byte sEGMENTField;

    /// <remarks/>
    public byte QUALF
    {
        get
        {
            return this.qUALFField;
        }
        set
        {
            this.qUALFField = value;
        }
    }

    /// <remarks/>
    public uint NTANF
    {
        get
        {
            return this.nTANFField;
        }
        set
        {
            this.nTANFField = value;
        }
    }

    /// <remarks/>
    public byte NTANZ
    {
        get
        {
            return this.nTANZField;
        }
        set
        {
            this.nTANZField = value;
        }
    }

    /// <remarks/>
    public uint NTEND
    {
        get
        {
            return this.nTENDField;
        }
        set
        {
            this.nTENDField = value;
        }
    }

    /// <remarks/>
    public byte NTENZ
    {
        get
        {
            return this.nTENZField;
        }
        set
        {
            this.nTENZField = value;
        }
    }

    /// <remarks/>
    public string TZONE_BEG
    {
        get
        {
            return this.tZONE_BEGField;
        }
        set
        {
            this.tZONE_BEGField = value;
        }
    }

    /// <remarks/>
    public byte ISDD
    {
        get
        {
            return this.iSDDField;
        }
        set
        {
            this.iSDDField = value;
        }
    }

    /// <remarks/>
    public byte ISDZ
    {
        get
        {
            return this.iSDZField;
        }
        set
        {
            this.iSDZField = value;
        }
    }

    /// <remarks/>
    public byte IEDD
    {
        get
        {
            return this.iEDDField;
        }
        set
        {
            this.iEDDField = value;
        }
    }

    /// <remarks/>
    public byte IEDZ
    {
        get
        {
            return this.iEDZField;
        }
        set
        {
            this.iEDZField = value;
        }
    }

    /// <remarks/>
    public string TZONE_END
    {
        get
        {
            return this.tZONE_ENDField;
        }
        set
        {
            this.tZONE_ENDField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte SEGMENT
    {
        get
        {
            return this.sEGMENTField;
        }
        set
        {
            this.sEGMENTField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class YCOSDELVRY02IDOCE1EDL20E1TXTH8
{

    private string tDOBJECTField;

    private ulong tDOBNAMEField;

    private string tDIDField;

    private string tDSPRASField;

    private string lANGUA_ISOField;

    private YCOSDELVRY02IDOCE1EDL20E1TXTH8E1TXTP8[] e1TXTP8Field;

    private byte sEGMENTField;

    /// <remarks/>
    public string TDOBJECT
    {
        get
        {
            return this.tDOBJECTField;
        }
        set
        {
            this.tDOBJECTField = value;
        }
    }

    /// <remarks/>
    public ulong TDOBNAME
    {
        get
        {
            return this.tDOBNAMEField;
        }
        set
        {
            this.tDOBNAMEField = value;
        }
    }

    /// <remarks/>
    public string TDID
    {
        get
        {
            return this.tDIDField;
        }
        set
        {
            this.tDIDField = value;
        }
    }

    /// <remarks/>
    public string TDSPRAS
    {
        get
        {
            return this.tDSPRASField;
        }
        set
        {
            this.tDSPRASField = value;
        }
    }

    /// <remarks/>
    public string LANGUA_ISO
    {
        get
        {
            return this.lANGUA_ISOField;
        }
        set
        {
            this.lANGUA_ISOField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("E1TXTP8")]
    public YCOSDELVRY02IDOCE1EDL20E1TXTH8E1TXTP8[] E1TXTP8
    {
        get
        {
            return this.e1TXTP8Field;
        }
        set
        {
            this.e1TXTP8Field = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte SEGMENT
    {
        get
        {
            return this.sEGMENTField;
        }
        set
        {
            this.sEGMENTField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class YCOSDELVRY02IDOCE1EDL20E1TXTH8E1TXTP8
{

    private string tDFORMATField;

    private string tDLINEField;

    private byte sEGMENTField;

    /// <remarks/>
    public string TDFORMAT
    {
        get
        {
            return this.tDFORMATField;
        }
        set
        {
            this.tDFORMATField = value;
        }
    }

    /// <remarks/>
    public string TDLINE
    {
        get
        {
            return this.tDLINEField;
        }
        set
        {
            this.tDLINEField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte SEGMENT
    {
        get
        {
            return this.sEGMENTField;
        }
        set
        {
            this.sEGMENTField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class YCOSDELVRY02IDOCE1EDL20E1EDDH2
{

    private string tDOBJECTField;

    private string tDOBNAMEField;

    private byte tDIDField;

    private string lANGUA_ISOField;

    private string pHRSELField;

    private string iDENTIFIERField;

    private string lANGUA_PHRField;

    private YCOSDELVRY02IDOCE1EDL20E1EDDH2E1EDDP2 e1EDDP2Field;

    private byte sEGMENTField;

    /// <remarks/>
    public string TDOBJECT
    {
        get
        {
            return this.tDOBJECTField;
        }
        set
        {
            this.tDOBJECTField = value;
        }
    }

    /// <remarks/>
    public string TDOBNAME
    {
        get
        {
            return this.tDOBNAMEField;
        }
        set
        {
            this.tDOBNAMEField = value;
        }
    }

    /// <remarks/>
    public byte TDID
    {
        get
        {
            return this.tDIDField;
        }
        set
        {
            this.tDIDField = value;
        }
    }

    /// <remarks/>
    public string LANGUA_ISO
    {
        get
        {
            return this.lANGUA_ISOField;
        }
        set
        {
            this.lANGUA_ISOField = value;
        }
    }

    /// <remarks/>
    public string PHRSEL
    {
        get
        {
            return this.pHRSELField;
        }
        set
        {
            this.pHRSELField = value;
        }
    }

    /// <remarks/>
    public string IDENTIFIER
    {
        get
        {
            return this.iDENTIFIERField;
        }
        set
        {
            this.iDENTIFIERField = value;
        }
    }

    /// <remarks/>
    public string LANGUA_PHR
    {
        get
        {
            return this.lANGUA_PHRField;
        }
        set
        {
            this.lANGUA_PHRField = value;
        }
    }

    /// <remarks/>
    public YCOSDELVRY02IDOCE1EDL20E1EDDH2E1EDDP2 E1EDDP2
    {
        get
        {
            return this.e1EDDP2Field;
        }
        set
        {
            this.e1EDDP2Field = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte SEGMENT
    {
        get
        {
            return this.sEGMENTField;
        }
        set
        {
            this.sEGMENTField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class YCOSDELVRY02IDOCE1EDL20E1EDDH2E1EDDP2
{

    private string tDFORMATField;

    private string tDLINEField;

    private byte sEGMENTField;

    /// <remarks/>
    public string TDFORMAT
    {
        get
        {
            return this.tDFORMATField;
        }
        set
        {
            this.tDFORMATField = value;
        }
    }

    /// <remarks/>
    public string TDLINE
    {
        get
        {
            return this.tDLINEField;
        }
        set
        {
            this.tDLINEField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte SEGMENT
    {
        get
        {
            return this.sEGMENTField;
        }
        set
        {
            this.sEGMENTField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class YCOSDELVRY02IDOCE1EDL20E1EDL28
{

    private string rOUTEField;

    private byte vSARTField;

    private decimal dISTZField;

    private YCOSDELVRY02IDOCE1EDL20E1EDL28E1EDL29 e1EDL29Field;

    private byte sEGMENTField;

    /// <remarks/>
    public string ROUTE
    {
        get
        {
            return this.rOUTEField;
        }
        set
        {
            this.rOUTEField = value;
        }
    }

    /// <remarks/>
    public byte VSART
    {
        get
        {
            return this.vSARTField;
        }
        set
        {
            this.vSARTField = value;
        }
    }

    /// <remarks/>
    public decimal DISTZ
    {
        get
        {
            return this.dISTZField;
        }
        set
        {
            this.dISTZField = value;
        }
    }

    /// <remarks/>
    public YCOSDELVRY02IDOCE1EDL20E1EDL28E1EDL29 E1EDL29
    {
        get
        {
            return this.e1EDL29Field;
        }
        set
        {
            this.e1EDL29Field = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte SEGMENT
    {
        get
        {
            return this.sEGMENTField;
        }
        set
        {
            this.sEGMENTField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class YCOSDELVRY02IDOCE1EDL20E1EDL28E1EDL29
{

    private string rOUTE_BEZField;

    private string vSART_BEZField;

    private byte sEGMENTField;

    /// <remarks/>
    public string ROUTE_BEZ
    {
        get
        {
            return this.rOUTE_BEZField;
        }
        set
        {
            this.rOUTE_BEZField = value;
        }
    }

    /// <remarks/>
    public string VSART_BEZ
    {
        get
        {
            return this.vSART_BEZField;
        }
        set
        {
            this.vSART_BEZField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte SEGMENT
    {
        get
        {
            return this.sEGMENTField;
        }
        set
        {
            this.sEGMENTField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class YCOSDELVRY02IDOCE1EDL20E1EDL24
{

    private uint pOSNRField;

    private uint mATNRField;

    private uint mATWAField;

    private string aRKTXField;

    private string mATKLField;

    private string wERKSField;

    private ushort lGORTField;

    private float cHARGField;

    private bool cHARGFieldSpecified;

    private string kDMATField;

    private decimal lFIMGField;

    private string vRKMEField;

    private decimal lGMNGField;

    private string mEINSField;

    private decimal nTGEWField;

    private decimal bRGEWField;

    private string gEWEIField;

    private decimal vOLUMField;

    private byte hIPOSField;

    private bool hIPOSFieldSpecified;

    private byte hIEVWField;

    private bool hIEVWFieldSpecified;

    private string lADGRField;

    private byte tRAGRField;

    private byte vKBURField;

    private byte vKGRPField;

    private string vTWEGField;

    private byte sPARTField;

    private byte gRKORField;

    private string kDMAT35Field;

    private byte pOSEXField;

    private uint mATNR_EXTERNALField;

    private string mATNR_GUIDField;

    private uint mATWA_EXTERNALField;

    private string mATWA_GUIDField;

    private uint vFDATField;

    private byte eXPIRY_DATE_EXTField;

    private ulong vGBELField;

    private byte vGPOSField;

    private decimal oRMNGField;

    private byte eXPIRY_DATE_EXT_BField;

    private YCOSDELVRY02IDOCE1EDL20E1EDL24E1EDL25 e1EDL25Field;

    private YCOSDELVRY02IDOCE1EDL20E1EDL24E1EDL26 e1EDL26Field;

    private YCOSDELVRY02IDOCE1EDL20E1EDL24E1EDD10 e1EDD10Field;

    private YCOSDELVRY02IDOCE1EDL20E1EDL24E1EDL15[] e1EDL15Field;

    private YCOSDELVRY02IDOCE1EDL20E1EDL24E1EDL35 e1EDL35Field;

    private YCOSDELVRY02IDOCE1EDL20E1EDL24E1EDL43[] e1EDL43Field;

    private YCOSDELVRY02IDOCE1EDL20E1EDL24E1EDL41[] e1EDL41Field;

    private YCOSDELVRY02IDOCE1EDL20E1EDL24E1TXTH9 e1TXTH9Field;

    private byte sEGMENTField;

    /// <remarks/>
    public uint POSNR
    {
        get
        {
            return this.pOSNRField;
        }
        set
        {
            this.pOSNRField = value;
        }
    }

    /// <remarks/>
    public uint MATNR
    {
        get
        {
            return this.mATNRField;
        }
        set
        {
            this.mATNRField = value;
        }
    }

    /// <remarks/>
    public uint MATWA
    {
        get
        {
            return this.mATWAField;
        }
        set
        {
            this.mATWAField = value;
        }
    }

    /// <remarks/>
    public string ARKTX
    {
        get
        {
            return this.aRKTXField;
        }
        set
        {
            this.aRKTXField = value;
        }
    }

    /// <remarks/>
    public string MATKL
    {
        get
        {
            return this.mATKLField;
        }
        set
        {
            this.mATKLField = value;
        }
    }

    /// <remarks/>
    public string WERKS
    {
        get
        {
            return this.wERKSField;
        }
        set
        {
            this.wERKSField = value;
        }
    }

    /// <remarks/>
    public ushort LGORT
    {
        get
        {
            return this.lGORTField;
        }
        set
        {
            this.lGORTField = value;
        }
    }

    /// <remarks/>
    public float CHARG
    {
        get
        {
            return this.cHARGField;
        }
        set
        {
            this.cHARGField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool CHARGSpecified
    {
        get
        {
            return this.cHARGFieldSpecified;
        }
        set
        {
            this.cHARGFieldSpecified = value;
        }
    }

    /// <remarks/>
    public string KDMAT
    {
        get
        {
            return this.kDMATField;
        }
        set
        {
            this.kDMATField = value;
        }
    }

    /// <remarks/>
    public decimal LFIMG
    {
        get
        {
            return this.lFIMGField;
        }
        set
        {
            this.lFIMGField = value;
        }
    }

    /// <remarks/>
    public string VRKME
    {
        get
        {
            return this.vRKMEField;
        }
        set
        {
            this.vRKMEField = value;
        }
    }

    /// <remarks/>
    public decimal LGMNG
    {
        get
        {
            return this.lGMNGField;
        }
        set
        {
            this.lGMNGField = value;
        }
    }

    /// <remarks/>
    public string MEINS
    {
        get
        {
            return this.mEINSField;
        }
        set
        {
            this.mEINSField = value;
        }
    }

    /// <remarks/>
    public decimal NTGEW
    {
        get
        {
            return this.nTGEWField;
        }
        set
        {
            this.nTGEWField = value;
        }
    }

    /// <remarks/>
    public decimal BRGEW
    {
        get
        {
            return this.bRGEWField;
        }
        set
        {
            this.bRGEWField = value;
        }
    }

    /// <remarks/>
    public string GEWEI
    {
        get
        {
            return this.gEWEIField;
        }
        set
        {
            this.gEWEIField = value;
        }
    }

    /// <remarks/>
    public decimal VOLUM
    {
        get
        {
            return this.vOLUMField;
        }
        set
        {
            this.vOLUMField = value;
        }
    }

    /// <remarks/>
    public byte HIPOS
    {
        get
        {
            return this.hIPOSField;
        }
        set
        {
            this.hIPOSField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool HIPOSSpecified
    {
        get
        {
            return this.hIPOSFieldSpecified;
        }
        set
        {
            this.hIPOSFieldSpecified = value;
        }
    }

    /// <remarks/>
    public byte HIEVW
    {
        get
        {
            return this.hIEVWField;
        }
        set
        {
            this.hIEVWField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool HIEVWSpecified
    {
        get
        {
            return this.hIEVWFieldSpecified;
        }
        set
        {
            this.hIEVWFieldSpecified = value;
        }
    }

    /// <remarks/>
    public string LADGR
    {
        get
        {
            return this.lADGRField;
        }
        set
        {
            this.lADGRField = value;
        }
    }

    /// <remarks/>
    public byte TRAGR
    {
        get
        {
            return this.tRAGRField;
        }
        set
        {
            this.tRAGRField = value;
        }
    }

    /// <remarks/>
    public byte VKBUR
    {
        get
        {
            return this.vKBURField;
        }
        set
        {
            this.vKBURField = value;
        }
    }

    /// <remarks/>
    public byte VKGRP
    {
        get
        {
            return this.vKGRPField;
        }
        set
        {
            this.vKGRPField = value;
        }
    }

    /// <remarks/>
    public string VTWEG
    {
        get
        {
            return this.vTWEGField;
        }
        set
        {
            this.vTWEGField = value;
        }
    }

    /// <remarks/>
    public byte SPART
    {
        get
        {
            return this.sPARTField;
        }
        set
        {
            this.sPARTField = value;
        }
    }

    /// <remarks/>
    public byte GRKOR
    {
        get
        {
            return this.gRKORField;
        }
        set
        {
            this.gRKORField = value;
        }
    }

    /// <remarks/>
    public string KDMAT35
    {
        get
        {
            return this.kDMAT35Field;
        }
        set
        {
            this.kDMAT35Field = value;
        }
    }

    /// <remarks/>
    public byte POSEX
    {
        get
        {
            return this.pOSEXField;
        }
        set
        {
            this.pOSEXField = value;
        }
    }

    /// <remarks/>
    public uint MATNR_EXTERNAL
    {
        get
        {
            return this.mATNR_EXTERNALField;
        }
        set
        {
            this.mATNR_EXTERNALField = value;
        }
    }

    /// <remarks/>
    public string MATNR_GUID
    {
        get
        {
            return this.mATNR_GUIDField;
        }
        set
        {
            this.mATNR_GUIDField = value;
        }
    }

    /// <remarks/>
    public uint MATWA_EXTERNAL
    {
        get
        {
            return this.mATWA_EXTERNALField;
        }
        set
        {
            this.mATWA_EXTERNALField = value;
        }
    }

    /// <remarks/>
    public string MATWA_GUID
    {
        get
        {
            return this.mATWA_GUIDField;
        }
        set
        {
            this.mATWA_GUIDField = value;
        }
    }

    /// <remarks/>
    public uint VFDAT
    {
        get
        {
            return this.vFDATField;
        }
        set
        {
            this.vFDATField = value;
        }
    }

    /// <remarks/>
    public byte EXPIRY_DATE_EXT
    {
        get
        {
            return this.eXPIRY_DATE_EXTField;
        }
        set
        {
            this.eXPIRY_DATE_EXTField = value;
        }
    }

    /// <remarks/>
    public ulong VGBEL
    {
        get
        {
            return this.vGBELField;
        }
        set
        {
            this.vGBELField = value;
        }
    }

    /// <remarks/>
    public byte VGPOS
    {
        get
        {
            return this.vGPOSField;
        }
        set
        {
            this.vGPOSField = value;
        }
    }

    /// <remarks/>
    public decimal ORMNG
    {
        get
        {
            return this.oRMNGField;
        }
        set
        {
            this.oRMNGField = value;
        }
    }

    /// <remarks/>
    public byte EXPIRY_DATE_EXT_B
    {
        get
        {
            return this.eXPIRY_DATE_EXT_BField;
        }
        set
        {
            this.eXPIRY_DATE_EXT_BField = value;
        }
    }

    /// <remarks/>
    public YCOSDELVRY02IDOCE1EDL20E1EDL24E1EDL25 E1EDL25
    {
        get
        {
            return this.e1EDL25Field;
        }
        set
        {
            this.e1EDL25Field = value;
        }
    }

    /// <remarks/>
    public YCOSDELVRY02IDOCE1EDL20E1EDL24E1EDL26 E1EDL26
    {
        get
        {
            return this.e1EDL26Field;
        }
        set
        {
            this.e1EDL26Field = value;
        }
    }

    /// <remarks/>
    public YCOSDELVRY02IDOCE1EDL20E1EDL24E1EDD10 E1EDD10
    {
        get
        {
            return this.e1EDD10Field;
        }
        set
        {
            this.e1EDD10Field = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("E1EDL15")]
    public YCOSDELVRY02IDOCE1EDL20E1EDL24E1EDL15[] E1EDL15
    {
        get
        {
            return this.e1EDL15Field;
        }
        set
        {
            this.e1EDL15Field = value;
        }
    }

    /// <remarks/>
    public YCOSDELVRY02IDOCE1EDL20E1EDL24E1EDL35 E1EDL35
    {
        get
        {
            return this.e1EDL35Field;
        }
        set
        {
            this.e1EDL35Field = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("E1EDL43")]
    public YCOSDELVRY02IDOCE1EDL20E1EDL24E1EDL43[] E1EDL43
    {
        get
        {
            return this.e1EDL43Field;
        }
        set
        {
            this.e1EDL43Field = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("E1EDL41")]
    public YCOSDELVRY02IDOCE1EDL20E1EDL24E1EDL41[] E1EDL41
    {
        get
        {
            return this.e1EDL41Field;
        }
        set
        {
            this.e1EDL41Field = value;
        }
    }

    /// <remarks/>
    public YCOSDELVRY02IDOCE1EDL20E1EDL24E1TXTH9 E1TXTH9
    {
        get
        {
            return this.e1TXTH9Field;
        }
        set
        {
            this.e1TXTH9Field = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte SEGMENT
    {
        get
        {
            return this.sEGMENTField;
        }
        set
        {
            this.sEGMENTField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class YCOSDELVRY02IDOCE1EDL20E1EDL24E1EDL25
{

    private string lGORT_BEZField;

    private string lADGR_BEZField;

    private string tRAGR_BEZField;

    private string vKBUR_BEZField;

    private string vKGRP_BEZField;

    private string vTWEG_BEZField;

    private string sPART_BEZField;

    private byte sEGMENTField;

    /// <remarks/>
    public string LGORT_BEZ
    {
        get
        {
            return this.lGORT_BEZField;
        }
        set
        {
            this.lGORT_BEZField = value;
        }
    }

    /// <remarks/>
    public string LADGR_BEZ
    {
        get
        {
            return this.lADGR_BEZField;
        }
        set
        {
            this.lADGR_BEZField = value;
        }
    }

    /// <remarks/>
    public string TRAGR_BEZ
    {
        get
        {
            return this.tRAGR_BEZField;
        }
        set
        {
            this.tRAGR_BEZField = value;
        }
    }

    /// <remarks/>
    public string VKBUR_BEZ
    {
        get
        {
            return this.vKBUR_BEZField;
        }
        set
        {
            this.vKBUR_BEZField = value;
        }
    }

    /// <remarks/>
    public string VKGRP_BEZ
    {
        get
        {
            return this.vKGRP_BEZField;
        }
        set
        {
            this.vKGRP_BEZField = value;
        }
    }

    /// <remarks/>
    public string VTWEG_BEZ
    {
        get
        {
            return this.vTWEG_BEZField;
        }
        set
        {
            this.vTWEG_BEZField = value;
        }
    }

    /// <remarks/>
    public string SPART_BEZ
    {
        get
        {
            return this.sPART_BEZField;
        }
        set
        {
            this.sPART_BEZField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte SEGMENT
    {
        get
        {
            return this.sEGMENTField;
        }
        set
        {
            this.sEGMENTField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class YCOSDELVRY02IDOCE1EDL20E1EDL24E1EDL26
{

    private string pSTYVField;

    private string mATKLField;

    private byte uMVKZField;

    private byte uMVKNField;

    private string kZTLFField;

    private decimal uEBTOField;

    private decimal uNTTOField;

    private byte xCHBWField;

    private string kVGR4Field;
   
    private string mVGR3Field;

    private ushort mVGR4Field;
    
    private YCOSDELVRY02IDOCE1EDL20E1EDL24E1EDL26E1EDL27 e1EDL27Field;

    private byte sEGMENTField;

    /// <remarks/>
    public string PSTYV
    {
        get
        {
            return this.pSTYVField;
        }
        set
        {
            this.pSTYVField = value;
        }
    }

    /// <remarks/>
    public string MATKL
    {
        get
        {
            return this.mATKLField;
        }
        set
        {
            this.mATKLField = value;
        }
    }

    /// <remarks/>
    public byte UMVKZ
    {
        get
        {
            return this.uMVKZField;
        }
        set
        {
            this.uMVKZField = value;
        }
    }

    /// <remarks/>
    public byte UMVKN
    {
        get
        {
            return this.uMVKNField;
        }
        set
        {
            this.uMVKNField = value;
        }
    }

    /// <remarks/>
    public string KZTLF
    {
        get
        {
            return this.kZTLFField;
        }
        set
        {
            this.kZTLFField = value;
        }
    }

    /// <remarks/>
    public decimal UEBTO
    {
        get
        {
            return this.uEBTOField;
        }
        set
        {
            this.uEBTOField = value;
        }
    }

    /// <remarks/>
    public decimal UNTTO
    {
        get
        {
            return this.uNTTOField;
        }
        set
        {
            this.uNTTOField = value;
        }
    }

    /// <remarks/>
    public byte XCHBW
    {
        get
        {
            return this.xCHBWField;
        }
        set
        {
            this.xCHBWField = value;
        }
    }

    /// <remarks/>
    public string KVGR4
    {
        get
        {
            return this.kVGR4Field;
        }
        set
        {
            this.kVGR4Field = value;
        }
    }
    // <remarks/>
    public string MVGR3
    {
        get
        {
            return this.mVGR3Field;
        }
        set
        {
            this.mVGR3Field = value;
        }
    }

    /// <remarks/>
    public ushort MVGR4
    {
        get
        {
            return this.mVGR4Field;
        }
        set
        {
            this.mVGR4Field = value;
        }
    }

    /// <remarks/>
    public YCOSDELVRY02IDOCE1EDL20E1EDL24E1EDL26E1EDL27 E1EDL27
    {
        get
        {
            return this.e1EDL27Field;
        }
        set
        {
            this.e1EDL27Field = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte SEGMENT
    {
        get
        {
            return this.sEGMENTField;
        }
        set
        {
            this.sEGMENTField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class YCOSDELVRY02IDOCE1EDL20E1EDL24E1EDL26E1EDL27
{

    private string pSTYV_BEZField;

    private string mATKL_BEZField;

    private string wERKS_BEZField;

    private string kVGR4_BEZField;

    private byte sEGMENTField;

    /// <remarks/>
    public string PSTYV_BEZ
    {
        get
        {
            return this.pSTYV_BEZField;
        }
        set
        {
            this.pSTYV_BEZField = value;
        }
    }

    /// <remarks/>
    public string MATKL_BEZ
    {
        get
        {
            return this.mATKL_BEZField;
        }
        set
        {
            this.mATKL_BEZField = value;
        }
    }

    /// <remarks/>
    public string WERKS_BEZ
    {
        get
        {
            return this.wERKS_BEZField;
        }
        set
        {
            this.wERKS_BEZField = value;
        }
    }

    /// <remarks/>
    public string KVGR4_BEZ
    {
        get
        {
            return this.kVGR4_BEZField;
        }
        set
        {
            this.kVGR4_BEZField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte SEGMENT
    {
        get
        {
            return this.sEGMENTField;
        }
        set
        {
            this.sEGMENTField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class YCOSDELVRY02IDOCE1EDL20E1EDL24E1EDD10
{

    private byte mOTField;

    private uint vALDATField;

    private string dGNHMField;

    private byte sDBCField;

    private byte sLBEField;

    private string lWDGField;

    private decimal hQTUField;

    private string nHMEField;

    private string lWDGNField;

    private string rVLIDField;

    private byte mOS1Field;

    private byte mOS2Field;

    private byte mOS3Field;

    private byte mOS4Field;

    private byte mOS5Field;

    private byte mOS6Field;

    private byte mOS7Field;

    private byte mOS8Field;

    private byte mOS9Field;

    private byte mOSAField;

    private byte dGWOSField;

    private byte eSMLG1Field;

    private byte lFDNRField;

    private decimal dG_NET_WEIGHTField;

    private decimal dG_GROSS_WEIGHTField;

    private string lANGUField;

    private YCOSDELVRY02IDOCE1EDL20E1EDL24E1EDD10E1EDD11 e1EDD11Field;

    private YCOSDELVRY02IDOCE1EDL20E1EDL24E1EDD10E1EDD12 e1EDD12Field;

    private YCOSDELVRY02IDOCE1EDL20E1EDL24E1EDD10E1EDDH3[] e1EDDH3Field;

    private byte sEGMENTField;

    /// <remarks/>
    public byte MOT
    {
        get
        {
            return this.mOTField;
        }
        set
        {
            this.mOTField = value;
        }
    }

    /// <remarks/>
    public uint VALDAT
    {
        get
        {
            return this.vALDATField;
        }
        set
        {
            this.vALDATField = value;
        }
    }

    /// <remarks/>
    public string DGNHM
    {
        get
        {
            return this.dGNHMField;
        }
        set
        {
            this.dGNHMField = value;
        }
    }

    /// <remarks/>
    public byte SDBC
    {
        get
        {
            return this.sDBCField;
        }
        set
        {
            this.sDBCField = value;
        }
    }

    /// <remarks/>
    public byte SLBE
    {
        get
        {
            return this.sLBEField;
        }
        set
        {
            this.sLBEField = value;
        }
    }

    /// <remarks/>
    public string LWDG
    {
        get
        {
            return this.lWDGField;
        }
        set
        {
            this.lWDGField = value;
        }
    }

    /// <remarks/>
    public decimal HQTU
    {
        get
        {
            return this.hQTUField;
        }
        set
        {
            this.hQTUField = value;
        }
    }

    /// <remarks/>
    public string NHME
    {
        get
        {
            return this.nHMEField;
        }
        set
        {
            this.nHMEField = value;
        }
    }

    /// <remarks/>
    public string LWDGN
    {
        get
        {
            return this.lWDGNField;
        }
        set
        {
            this.lWDGNField = value;
        }
    }

    /// <remarks/>
    public string RVLID
    {
        get
        {
            return this.rVLIDField;
        }
        set
        {
            this.rVLIDField = value;
        }
    }

    /// <remarks/>
    public byte MOS1
    {
        get
        {
            return this.mOS1Field;
        }
        set
        {
            this.mOS1Field = value;
        }
    }

    /// <remarks/>
    public byte MOS2
    {
        get
        {
            return this.mOS2Field;
        }
        set
        {
            this.mOS2Field = value;
        }
    }

    /// <remarks/>
    public byte MOS3
    {
        get
        {
            return this.mOS3Field;
        }
        set
        {
            this.mOS3Field = value;
        }
    }

    /// <remarks/>
    public byte MOS4
    {
        get
        {
            return this.mOS4Field;
        }
        set
        {
            this.mOS4Field = value;
        }
    }

    /// <remarks/>
    public byte MOS5
    {
        get
        {
            return this.mOS5Field;
        }
        set
        {
            this.mOS5Field = value;
        }
    }

    /// <remarks/>
    public byte MOS6
    {
        get
        {
            return this.mOS6Field;
        }
        set
        {
            this.mOS6Field = value;
        }
    }

    /// <remarks/>
    public byte MOS7
    {
        get
        {
            return this.mOS7Field;
        }
        set
        {
            this.mOS7Field = value;
        }
    }

    /// <remarks/>
    public byte MOS8
    {
        get
        {
            return this.mOS8Field;
        }
        set
        {
            this.mOS8Field = value;
        }
    }

    /// <remarks/>
    public byte MOS9
    {
        get
        {
            return this.mOS9Field;
        }
        set
        {
            this.mOS9Field = value;
        }
    }

    /// <remarks/>
    public byte MOSA
    {
        get
        {
            return this.mOSAField;
        }
        set
        {
            this.mOSAField = value;
        }
    }

    /// <remarks/>
    public byte DGWOS
    {
        get
        {
            return this.dGWOSField;
        }
        set
        {
            this.dGWOSField = value;
        }
    }

    /// <remarks/>
    public byte ESMLG1
    {
        get
        {
            return this.eSMLG1Field;
        }
        set
        {
            this.eSMLG1Field = value;
        }
    }

    /// <remarks/>
    public byte LFDNR
    {
        get
        {
            return this.lFDNRField;
        }
        set
        {
            this.lFDNRField = value;
        }
    }

    /// <remarks/>
    public decimal DG_NET_WEIGHT
    {
        get
        {
            return this.dG_NET_WEIGHTField;
        }
        set
        {
            this.dG_NET_WEIGHTField = value;
        }
    }

    /// <remarks/>
    public decimal DG_GROSS_WEIGHT
    {
        get
        {
            return this.dG_GROSS_WEIGHTField;
        }
        set
        {
            this.dG_GROSS_WEIGHTField = value;
        }
    }

    /// <remarks/>
    public string LANGU
    {
        get
        {
            return this.lANGUField;
        }
        set
        {
            this.lANGUField = value;
        }
    }

    /// <remarks/>
    public YCOSDELVRY02IDOCE1EDL20E1EDL24E1EDD10E1EDD11 E1EDD11
    {
        get
        {
            return this.e1EDD11Field;
        }
        set
        {
            this.e1EDD11Field = value;
        }
    }

    /// <remarks/>
    public YCOSDELVRY02IDOCE1EDL20E1EDL24E1EDD10E1EDD12 E1EDD12
    {
        get
        {
            return this.e1EDD12Field;
        }
        set
        {
            this.e1EDD12Field = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("E1EDDH3")]
    public YCOSDELVRY02IDOCE1EDL20E1EDL24E1EDD10E1EDDH3[] E1EDDH3
    {
        get
        {
            return this.e1EDDH3Field;
        }
        set
        {
            this.e1EDDH3Field = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte SEGMENT
    {
        get
        {
            return this.sEGMENTField;
        }
        set
        {
            this.sEGMENTField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class YCOSDELVRY02IDOCE1EDL20E1EDL24E1EDD10E1EDD11
{

    private string vKTRTField;

    private byte sEGMENTField;

    /// <remarks/>
    public string VKTRT
    {
        get
        {
            return this.vKTRTField;
        }
        set
        {
            this.vKTRTField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte SEGMENT
    {
        get
        {
            return this.sEGMENTField;
        }
        set
        {
            this.sEGMENTField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class YCOSDELVRY02IDOCE1EDL20E1EDL24E1EDD10E1EDD12
{

    private decimal fLTMPField;

    private string tPFUNField;

    private decimal sOOWAField;

    private string uSOOWField;

    private decimal dENTWEField;

    private byte uDENTWEField;

    private decimal bOPOIField;

    private string uBOPOField;

    private string mEPOIField;

    private string uMEPOField;

    private byte sEGMENTField;

    private decimal tPSNLField;
    private string tPSLUField;
    private decimal tPSNHField;
    private string tPSHUField;
    /// <remarks/>
    public decimal TPSNL
    {
        get
        {
            return this.tPSNLField;
        }
        set
        {
            this.tPSNLField = value;
        }
    }

    /// <remarks/>
    public string TPSLU
    {
        get
        {
            return this.tPSLUField;
        }
        set
        {
            this.tPSLUField = value;
        }
    }

    /// <remarks/>
    public decimal TPSNH
    {
        get
        {
            return this.tPSNHField;
        }
        set
        {
            this.tPSNHField = value;
        }
    }
    public string TPSHU
    {
        get
        {
            return this.tPSHUField;
        }
        set
        {
            this.tPSHUField = value;
        }
    }

    /// <remarks/>
    public decimal FLTMP
    {
        get
        {
            return this.fLTMPField;
        }
        set
        {
            this.fLTMPField = value;
        }
    }

    /// <remarks/>
    public string TPFUN
    {
        get
        {
            return this.tPFUNField;
        }
        set
        {
            this.tPFUNField = value;
        }
    }

    /// <remarks/>
    public decimal SOOWA
    {
        get
        {
            return this.sOOWAField;
        }
        set
        {
            this.sOOWAField = value;
        }
    }

    /// <remarks/>
    public string USOOW
    {
        get
        {
            return this.uSOOWField;
        }
        set
        {
            this.uSOOWField = value;
        }
    }

    /// <remarks/>
    public decimal DENTWE
    {
        get
        {
            return this.dENTWEField;
        }
        set
        {
            this.dENTWEField = value;
        }
    }

    /// <remarks/>
    public byte UDENTWE
    {
        get
        {
            return this.uDENTWEField;
        }
        set
        {
            this.uDENTWEField = value;
        }
    }

    /// <remarks/>
    public decimal BOPOI
    {
        get
        {
            return this.bOPOIField;
        }
        set
        {
            this.bOPOIField = value;
        }
    }

    /// <remarks/>
    public string UBOPO
    {
        get
        {
            return this.uBOPOField;
        }
        set
        {
            this.uBOPOField = value;
        }
    }

    /// <remarks/>
    public string MEPOI
    {
        get
        {
            return this.mEPOIField;
        }
        set
        {
            this.mEPOIField = value;
        }
    }

    /// <remarks/>
    public string UMEPO
    {
        get
        {
            return this.uMEPOField;
        }
        set
        {
            this.uMEPOField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte SEGMENT
    {
        get
        {
            return this.sEGMENTField;
        }
        set
        {
            this.sEGMENTField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class YCOSDELVRY02IDOCE1EDL20E1EDL24E1EDD10E1EDDH3
{

    private string tDOBJECTField;

    private string tDOBNAMEField;

    private byte tDIDField;

    private string lANGUA_ISOField;

    private string pHRSELField;

    private string iDENTIFIERField;

    private string lANGUA_PHRField;

    private YCOSDELVRY02IDOCE1EDL20E1EDL24E1EDD10E1EDDH3E1EDDP3[] e1EDDP3Field;

    private byte sEGMENTField;

    /// <remarks/>
    public string TDOBJECT
    {
        get
        {
            return this.tDOBJECTField;
        }
        set
        {
            this.tDOBJECTField = value;
        }
    }

    /// <remarks/>
    public string TDOBNAME
    {
        get
        {
            return this.tDOBNAMEField;
        }
        set
        {
            this.tDOBNAMEField = value;
        }
    }

    /// <remarks/>
    public byte TDID
    {
        get
        {
            return this.tDIDField;
        }
        set
        {
            this.tDIDField = value;
        }
    }

    /// <remarks/>
    public string LANGUA_ISO
    {
        get
        {
            return this.lANGUA_ISOField;
        }
        set
        {
            this.lANGUA_ISOField = value;
        }
    }

    /// <remarks/>
    public string PHRSEL
    {
        get
        {
            return this.pHRSELField;
        }
        set
        {
            this.pHRSELField = value;
        }
    }

    /// <remarks/>
    public string IDENTIFIER
    {
        get
        {
            return this.iDENTIFIERField;
        }
        set
        {
            this.iDENTIFIERField = value;
        }
    }

    /// <remarks/>
    public string LANGUA_PHR
    {
        get
        {
            return this.lANGUA_PHRField;
        }
        set
        {
            this.lANGUA_PHRField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("E1EDDP3")]
    public YCOSDELVRY02IDOCE1EDL20E1EDL24E1EDD10E1EDDH3E1EDDP3[] E1EDDP3
    {
        get
        {
            return this.e1EDDP3Field;
        }
        set
        {
            this.e1EDDP3Field = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte SEGMENT
    {
        get
        {
            return this.sEGMENTField;
        }
        set
        {
            this.sEGMENTField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class YCOSDELVRY02IDOCE1EDL20E1EDL24E1EDD10E1EDDH3E1EDDP3
{

    private string tDFORMATField;

    private string tDLINEField;

    private byte sEGMENTField;

    /// <remarks/>
    public string TDFORMAT
    {
        get
        {
            return this.tDFORMATField;
        }
        set
        {
            this.tDFORMATField = value;
        }
    }

    /// <remarks/>
    public string TDLINE
    {
        get
        {
            return this.tDLINEField;
        }
        set
        {
            this.tDLINEField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte SEGMENT
    {
        get
        {
            return this.sEGMENTField;
        }
        set
        {
            this.sEGMENTField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class YCOSDELVRY02IDOCE1EDL20E1EDL24E1EDL15
{

    private ulong aTINNField;

    private string aTNAMField;

    private string aTBEZField;

    private string aTWRTField;

    private string aTWTBField;

    private float eWAHRField;

    private byte sEGMENTField;

    /// <remarks/>
    public ulong ATINN
    {
        get
        {
            return this.aTINNField;
        }
        set
        {
            this.aTINNField = value;
        }
    }

    /// <remarks/>
    public string ATNAM
    {
        get
        {
            return this.aTNAMField;
        }
        set
        {
            this.aTNAMField = value;
        }
    }

    /// <remarks/>
    public string ATBEZ
    {
        get
        {
            return this.aTBEZField;
        }
        set
        {
            this.aTBEZField = value;
        }
    }

    /// <remarks/>
    public string ATWRT
    {
        get
        {
            return this.aTWRTField;
        }
        set
        {
            this.aTWRTField = value;
        }
    }

    /// <remarks/>
    public string ATWTB
    {
        get
        {
            return this.aTWTBField;
        }
        set
        {
            this.aTWTBField = value;
        }
    }

    /// <remarks/>
    public float EWAHR
    {
        get
        {
            return this.eWAHRField;
        }
        set
        {
            this.eWAHRField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte SEGMENT
    {
        get
        {
            return this.sEGMENTField;
        }
        set
        {
            this.sEGMENTField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class YCOSDELVRY02IDOCE1EDL20E1EDL24E1EDL35
{

    private uint sTAWNField;

    private string hERKLField;

    private YCOSDELVRY02IDOCE1EDL20E1EDL24E1EDL35E1EDL36 e1EDL36Field;

    private byte sEGMENTField;
    private string hERKRField;
    /// <remarks/>
    public string HERKR
    {
        get
        {
            return this.hERKRField;
        }
        set
        {
            this.hERKRField = value;
        }
    }

    /// <remarks/>
    public uint STAWN
    {
        get
        {
            return this.sTAWNField;
        }
        set
        {
            this.sTAWNField = value;
        }
    }

    /// <remarks/>
    public string HERKL
    {
        get
        {
            return this.hERKLField;
        }
        set
        {
            this.hERKLField = value;
        }
    }

    /// <remarks/>
    public YCOSDELVRY02IDOCE1EDL20E1EDL24E1EDL35E1EDL36 E1EDL36
    {
        get
        {
            return this.e1EDL36Field;
        }
        set
        {
            this.e1EDL36Field = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte SEGMENT
    {
        get
        {
            return this.sEGMENTField;
        }
        set
        {
            this.sEGMENTField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class YCOSDELVRY02IDOCE1EDL20E1EDL24E1EDL35E1EDL36
{

    private string sTXT1Field;

    private string sTXT2Field;

    private string sTXT3Field;

    private string sTXT4Field;

    private string hERKL_BEZField;

    private byte sEGMENTField;
    private string hERKR_BEZField;
    /// <remarks/>
    public string HERKL_BEZ
    {
        get
        {
            return this.hERKL_BEZField;
        }
        set
        {
            this.hERKL_BEZField = value;
        }
    }

    /// <remarks/>
    public string HERKR_BEZ
    {
        get
        {
            return this.hERKR_BEZField;
        }
        set
        {
            this.hERKR_BEZField = value;
        }
    }
    /// <remarks/>
    public string STXT1
    {
        get
        {
            return this.sTXT1Field;
        }
        set
        {
            this.sTXT1Field = value;
        }
    }

    /// <remarks/>
    public string STXT2
    {
        get
        {
            return this.sTXT2Field;
        }
        set
        {
            this.sTXT2Field = value;
        }
    }

    /// <remarks/>
    public string STXT3
    {
        get
        {
            return this.sTXT3Field;
        }
        set
        {
            this.sTXT3Field = value;
        }
    }

    /// <remarks/>
    public string STXT4
    {
        get
        {
            return this.sTXT4Field;
        }
        set
        {
            this.sTXT4Field = value;
        }
    }

 

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte SEGMENT
    {
        get
        {
            return this.sEGMENTField;
        }
        set
        {
            this.sEGMENTField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class YCOSDELVRY02IDOCE1EDL20E1EDL24E1EDL43
{

    private string qUALFField;

    private ulong bELNRField;

    private byte pOSNRField;

    private uint dATUMField;

    private byte sEGMENTField;
    private bool dATUMFieldSpecified;

    /// <remarks/>
    public string QUALF
    {
        get
        {
            return this.qUALFField;
        }
        set
        {
            this.qUALFField = value;
        }
    }

    /// <remarks/>
    public ulong BELNR
    {
        get
        {
            return this.bELNRField;
        }
        set
        {
            this.bELNRField = value;
        }
    }

    /// <remarks/>
    public byte POSNR
    {
        get
        {
            return this.pOSNRField;
        }
        set
        {
            this.pOSNRField = value;
        }
    }

    /// <remarks/>
    public uint DATUM
    {
        get
        {
            return this.dATUMField;
        }
        set
        {
            this.dATUMField = value;
        }
    }
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool DATUMSpecified
    {
        get
        {
            return this.dATUMFieldSpecified;
        }
        set
        {
            this.dATUMFieldSpecified = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte SEGMENT
    {
        get
        {
            return this.sEGMENTField;
        }
        set
        {
            this.sEGMENTField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class YCOSDELVRY02IDOCE1EDL20E1EDL24E1EDL41
{

    private byte qUALIField;

    private uint bSTNRField;

    private uint bSTDTField;

    private string bSARKField;

    private byte pOSEXField;

    private YCOSDELVRY02IDOCE1EDL20E1EDL24E1EDL41E1EDL42 e1EDL42Field;

    private byte sEGMENTField;
    

    /// <remarks/>
    public byte QUALI
    {
        get
        {
            return this.qUALIField;
        }
        set
        {
            this.qUALIField = value;
        }
    }

    /// <remarks/>
    public uint BSTNR
    {
        get
        {
            return this.bSTNRField;
        }
        set
        {
            this.bSTNRField = value;
        }
    }

    /// <remarks/>
    public uint BSTDT
    {
        get
        {
            return this.bSTDTField;
        }
        set
        {
            this.bSTDTField = value;
        }
    }

    /// <remarks/>
    public string BSARK
    {
        get
        {
            return this.bSARKField;
        }
        set
        {
            this.bSARKField = value;
        }
    }

    /// <remarks/>
    public byte POSEX
    {
        get
        {
            return this.pOSEXField;
        }
        set
        {
            this.pOSEXField = value;
        }
    }

    /// <remarks/>
    public YCOSDELVRY02IDOCE1EDL20E1EDL24E1EDL41E1EDL42 E1EDL42
    {
        get
        {
            return this.e1EDL42Field;
        }
        set
        {
            this.e1EDL42Field = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte SEGMENT
    {
        get
        {
            return this.sEGMENTField;
        }
        set
        {
            this.sEGMENTField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class YCOSDELVRY02IDOCE1EDL20E1EDL24E1EDL41E1EDL42
{

    private string bSARK_BEZField;

    private byte sEGMENTField;

    /// <remarks/>
    public string BSARK_BEZ
    {
        get
        {
            return this.bSARK_BEZField;
        }
        set
        {
            this.bSARK_BEZField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte SEGMENT
    {
        get
        {
            return this.sEGMENTField;
        }
        set
        {
            this.sEGMENTField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class YCOSDELVRY02IDOCE1EDL20E1EDL24E1TXTH9
{

    private string tDOBJECTField;

    private ulong tDOBNAMEField;

    private string tDIDField;

    private string tDSPRASField;

    private string lANGUA_ISOField;

    private YCOSDELVRY02IDOCE1EDL20E1EDL24E1TXTH9E1TXTP9[] e1TXTP9Field;

    private byte sEGMENTField;

    /// <remarks/>
    public string TDOBJECT
    {
        get
        {
            return this.tDOBJECTField;
        }
        set
        {
            this.tDOBJECTField = value;
        }
    }

    /// <remarks/>
    public ulong TDOBNAME
    {
        get
        {
            return this.tDOBNAMEField;
        }
        set
        {
            this.tDOBNAMEField = value;
        }
    }

    /// <remarks/>
    public string TDID
    {
        get
        {
            return this.tDIDField;
        }
        set
        {
            this.tDIDField = value;
        }
    }

    /// <remarks/>
    public string TDSPRAS
    {
        get
        {
            return this.tDSPRASField;
        }
        set
        {
            this.tDSPRASField = value;
        }
    }

    /// <remarks/>
    public string LANGUA_ISO
    {
        get
        {
            return this.lANGUA_ISOField;
        }
        set
        {
            this.lANGUA_ISOField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("E1TXTP9")]
    public YCOSDELVRY02IDOCE1EDL20E1EDL24E1TXTH9E1TXTP9[] E1TXTP9
    {
        get
        {
            return this.e1TXTP9Field;
        }
        set
        {
            this.e1TXTP9Field = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte SEGMENT
    {
        get
        {
            return this.sEGMENTField;
        }
        set
        {
            this.sEGMENTField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class YCOSDELVRY02IDOCE1EDL20E1EDL24E1TXTH9E1TXTP9
{

    private string tDFORMATField;

    private string tDLINEField;

    private byte sEGMENTField;

    /// <remarks/>
    public string TDFORMAT
    {
        get
        {
            return this.tDFORMATField;
        }
        set
        {
            this.tDFORMATField = value;
        }
    }

    /// <remarks/>
    public string TDLINE
    {
        get
        {
            return this.tDLINEField;
        }
        set
        {
            this.tDLINEField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte SEGMENT
    {
        get
        {
            return this.sEGMENTField;
        }
        set
        {
            this.sEGMENTField = value;
        }
    }
}

