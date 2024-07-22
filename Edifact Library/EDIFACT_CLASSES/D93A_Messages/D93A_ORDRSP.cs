//--------------------------------------------------------------
// <CreatedBy>
//     Anthony R Yates
//	   Feb. 26, 2005
//     Runtime Version: 1.1.4322.2032
//	   
//     Contains D93A ORDRSP Classes
// </CreatedBy>

// Copyright (C) 2005 Anthony Yates
//
// This software is provided AS IS. No warranty is granted, 
// neither expressed nor implied. USE THIS SOFTWARE AT YOUR OWN RISK.
// NO REPRESENTATION OF MERCHANTABILITY or FITNESS FOR ANY 
// PURPOSE is given.
//
// License to use this software is limited by the following terms:
// 1) This code may be used in any program, including programs developed
//    for commercial purposes, provided that this notice is included verbatim.
//    
// Also, in return for using this code, please attempt to make your fixes and
// updates available in some way, such as by sending your updates to the
// author.
//
//--------------------------------------------------------------

using EDIFACT.BASETYPES;
using System;
using System.Collections;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace EDIFACT.D93A.ORDRSP
{
    #region Declarations SchemaVersion
    public struct Declarations
    {
        public const string SchemaVersion = "http://www.default.org/D93A/ordrsp";
    }
    #endregion

    #region D93A_ORDRSP Class

    [XmlRoot(ElementName = "D93A_ORDRSP", Namespace = Declarations.SchemaVersion, IsNullable = false), Serializable]
    public class D93A_ORDRSP : IMessage
    {
        [XmlElement(Type = typeof(ORDRSP), ElementName = "ORDRSP", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public ORDRSP __ORDRSP;

        [XmlIgnore]
        public ORDRSP ORDRSP
        {
            get
            {
                if (__ORDRSP == null) __ORDRSP = new ORDRSP();
                return __ORDRSP;
            }
            set { __ORDRSP = value; }
        }

        public D93A_ORDRSP()
        {
        }

        #region IMessage Members

        #region PopulateMessage Method

        public void PopulateMessage(ref Segment[] segments)
        {
            try
            {
                SegmentProcessor sp = new SegmentProcessor(new AddSegmentDelegate(this.ORDRSP.Add));
                sp.ProcessSegments(segments);
            }
            catch (Exception e)
            {
                throw new Exception((string.Format("Exception occured in \"SegmentProcessor\".\n{0}.\n{1}\n{2}", e.Message, e.InnerException, e.StackTrace)));
            }
        }

        #endregion

        #endregion
    }

    #endregion

    #region ORDRSP Class

    [XmlType(TypeName = "ORDRSP", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class ORDRSP
    {
        #region Class Fields

        [XmlAttribute(AttributeName = "number", Form = XmlSchemaForm.Unqualified, DataType = "unsignedInt", Namespace = Declarations.SchemaVersion)]
        public uint __number;

        [XmlIgnore]
        public bool __numberSpecified;

        [XmlIgnore]
        public uint number
        {
            get { return __number; }
            set { __number = value; __numberSpecified = true; }
        }

        [XmlElement(Type = typeof(UNH), ElementName = "UNH", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public UNH __UNH;

        [XmlIgnore]
        public UNH UNH
        {
            get
            {
                if (__UNH == null) __UNH = new UNH();
                return __UNH;
            }
            set { __UNH = value; }
        }

        [XmlElement(Type = typeof(BGM), ElementName = "BGM", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public BGM __BGM;

        [XmlIgnore]
        public BGM BGM
        {
            get
            {
                if (__BGM == null) __BGM = new BGM();
                return __BGM;
            }
            set { __BGM = value; }
        }

        [XmlElement(Type = typeof(DTM), ElementName = "DTM", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public DTMCollection __DTMCollection;

        [XmlIgnore]
        public DTMCollection DTMCollection
        {
            get
            {
                if (__DTMCollection == null) __DTMCollection = new DTMCollection();
                return __DTMCollection;
            }
            set { __DTMCollection = value; }
        }

        [XmlElement(Type = typeof(GRP1), ElementName = "GRP1", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public GRP1 __GRP1;

        [XmlIgnore]
        public GRP1 GRP1
        {
            get
            {
                if (__GRP1 == null) __GRP1 = new GRP1();
                return __GRP1;
            }
            set { __GRP1 = value; }
        }

        [XmlElement(Type = typeof(GRP2), ElementName = "GRP2", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public GRP2Collection __GRP2Collection;

        [XmlIgnore]
        public GRP2Collection GRP2Collection
        {
            get
            {
                if (__GRP2Collection == null) __GRP2Collection = new GRP2Collection();
                return __GRP2Collection;
            }
            set { __GRP2Collection = value; }
        }

        [XmlElement(Type = typeof(GRP11), ElementName = "GRP11", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public GRP11 __GRP11;

        [XmlIgnore]
        public GRP11 GRP11
        {
            get
            {
                if (__GRP11 == null) __GRP11 = new GRP11();
                return __GRP11;
            }
            set { __GRP11 = value; }
        }

        [XmlElement(Type = typeof(GRP25), ElementName = "GRP25", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public GRP25Collection __GRP25Collection;

        [XmlIgnore]
        public GRP25Collection GRP25Collection
        {
            get
            {
                if (__GRP25Collection == null) __GRP25Collection = new GRP25Collection();
                return __GRP25Collection;
            }
            set { __GRP25Collection = value; }
        }

        [XmlElement(Type = typeof(UNS), ElementName = "UNS", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public UNS __UNS;

        [XmlIgnore]
        public UNS UNS
        {
            get
            {
                if (__UNS == null) __UNS = new UNS();
                return __UNS;
            }
            set { __UNS = value; }
        }

        [XmlElement(Type = typeof(CNT), ElementName = "CNT", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public CNT __CNT;

        [XmlIgnore]
        public CNT CNT
        {
            get
            {
                if (__CNT == null) __CNT = new CNT();
                return __CNT;
            }
            set { __CNT = value; }
        }

        [XmlElement(Type = typeof(UNT), ElementName = "UNT", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public UNT __UNT;

        [XmlIgnore]
        public UNT UNT
        {
            get
            {
                if (__UNT == null) __UNT = new UNT();
                return __UNT;
            }
            set { __UNT = value; }
        }

        #endregion

        #region Constructor

        public ORDRSP()
        {
        }

        #endregion

        #region Add Method

        public void Add(SegmentType type, object obj)
        {
            switch (type)
            {
                case SegmentType.UNH:
                    {
                        this.UNH = (UNH)obj;
                        break;
                    }
                case SegmentType.BGM:
                    {
                        this.BGM = (BGM)obj;
                        break;
                    }
                case SegmentType.DTM:
                    {
                        if (((DTM)obj).dateTimePeriodQualifier == "44") //Availability:
                        {
                            if (this.GRP25Collection.Count > 0)
                                this.GRP25Collection[this.GRP25Collection.Count - 1].LIN.DTM = (DTM)obj;
                        }
                        else if (((DTM)obj).dateTimePeriodQualifier == "69") //Promised For
                        {
                            this.DTMCollection.Add((DTM)obj);
                            int i, j;
                            if ((i = this.GRP25Collection.Count) > 0 &&
                                (j = this.GRP25Collection[i - 1].LIN.GRP32Collection.Count) > 0)
                                this.GRP25Collection[i - 1].LIN.GRP32Collection[j - 1].LOC_QD.DTM = (DTM)obj;
                        }
                        else
                        {                                   //2  Requested		
                            this.DTMCollection.Add((DTM)obj);   //63 Latest, 64 Earliest
                        }                                       //69* Promised For,72 "" + After and Including, 75 "" + Prior to and Including
                        break;                                  //77 Deliv. week starting with
                    }                                           //137 Doc/msg date
                case SegmentType.NAD:
                    {
                        GRP2 tempGRP2 = new GRP2((NAD)obj);
                        this.GRP2Collection.Add(tempGRP2);
                        tempGRP2 = null;
                        break;
                    }
                case SegmentType.CTA:
                    {
                        if (this.GRP2Collection.Count > 0)
                        {
                            GRP5 tempGRP5 = new GRP5();
                            tempGRP5.CTA = (CTA)obj;
                            this.GRP2Collection[this.GRP2Collection.Count - 1].NAD.GRP5Collection.Add(tempGRP5);
                            tempGRP5 = null;
                        }
                        break;
                    }
                case SegmentType.RFF:
                    {
                        int i;
                        if (((RFF)obj).referenceQualifier == "ON" && //Order Number (purchased)
                            (i = this.GRP25Collection.Count) > 0)
                        {
                            this.GRP1.RFF = (RFF)obj;
                            this.GRP25Collection[i - 1].LIN.GRP28.RFF = (RFF)obj;
                        }
                        if (((RFF)obj).referenceQualifier == "GN" ||    //Gov't Ref Num
                            ((RFF)obj).referenceQualifier == "VA")  //VAT Reg Num
                        {
                            if (this.GRP2Collection.Count > 0)
                                this.GRP2Collection[this.GRP2Collection.Count - 1].NAD.GRP3.RFF = (RFF)obj;
                        }
                        break;
                    }
                case SegmentType.TOD:
                    {
                        this.GRP11.TOD = (TOD)obj;
                        break;
                    }
                case SegmentType.LIN:
                    {
                        GRP25 tempGRP25 = new GRP25((LIN)obj);
                        this.GRP25Collection.Add(tempGRP25);
                        tempGRP25 = null;
                        break;
                    }
                case SegmentType.PIA:
                    {
                        if (this.GRP25Collection.Count > 0)
                        {
                            this.GRP25Collection[GRP25Collection.Count - 1].LIN.PIACollection.Add((PIA)obj);
                        }
                        break;
                    }
                case SegmentType.IMD:
                    {
                        if (this.GRP25Collection.Count > 0)
                        {
                            this.GRP25Collection[GRP25Collection.Count - 1].LIN.IMDCollection.Add((IMD)obj);
                        }
                        break;
                    }
                case SegmentType.QTY:
                    {
                        int i, j;
                        if ((i = this.GRP25Collection.Count) > 0)
                        {
                            if (((QTY)obj).qtyQualifier == "21" ||      //21  Ordered Quantity 
                                ((QTY)obj).qtyQualifier == "113")
                            {   //113 Quantity to be Delivered
                                this.GRP25Collection[i - 1].LIN.QTYCollection.Add((QTY)obj);
                            }
                            else if (((QTY)obj).qtyQualifier == "11" && //Split Quantity 
                                (j = this.GRP25Collection[i - 1].LIN.GRP32Collection.Count) > 0)
                            {
                                this.GRP25Collection[i - 1].LIN.GRP32Collection[j - 1].LOC_QD.QTY = (QTY)obj;
                            }
                        }
                        break;
                    }
                case SegmentType.QVA:
                    {
                        if (this.GRP25Collection.Count > 0)
                        {
                            this.GRP25Collection[GRP25Collection.Count - 1].LIN.QVA = (QVA)obj;
                        }
                        break;
                    }
                case SegmentType.FTX:
                    {
                        if (this.GRP25Collection.Count > 0)
                        {
                            this.GRP25Collection[GRP25Collection.Count - 1].LIN.FTXCollection.Add((FTX)obj);
                        }
                        break;
                    }
                case SegmentType.LOC:
                    {
                        int i;
                        if ((i = this.GRP25Collection.Count) > 0)
                        {
                            LOC_QD tempLOC = new LOC_QD((LOC)obj);
                            GRP32 tempGRP32 = new GRP32();
                            tempGRP32.LOC_QD = tempLOC;
                            this.GRP25Collection[i - 1].LIN.GRP32Collection.Add(tempGRP32);
                            tempGRP32 = null;
                            tempLOC = null;
                        }
                        break;
                    }
                case SegmentType.UNS:
                    {
                        this.UNS = (UNS)obj;
                        break;
                    }
                case SegmentType.CNT:
                    {
                        this.CNT = (CNT)obj;
                        break;
                    }
                case SegmentType.UNT:
                    {
                        this.UNT = (UNT)obj;
                        break;
                    }
            }
            if (obj != null) obj = null;
        }

        #endregion
    }

    #endregion

    #region GROUPS

    #region GRP1
    [XmlType(TypeName = "GRP1", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP1
    {

        [XmlElement(Type = typeof(RFF), ElementName = "RFF", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public RFF __RFF;

        [XmlIgnore]
        public RFF RFF
        {
            get
            {
                if (__RFF == null) __RFF = new RFF();
                return __RFF;
            }
            set { __RFF = value; }
        }

        public GRP1()
        {
        }
    }
    #endregion

    #region GRP2

    [XmlType(TypeName = "GRP2", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP2
    {

        [XmlElement(Type = typeof(NADG3G5), ElementName = "NAD", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public NADG3G5 __NADG3G5;

        [XmlIgnore]
        public NADG3G5 NAD
        {
            get
            {
                if (__NADG3G5 == null) __NADG3G5 = new NADG3G5();
                return __NADG3G5;
            }
            set { __NADG3G5 = value; }
        }

        public GRP2()
        {
        }
        public GRP2(NAD nadObject)
        {
            this.NAD.cityName = nadObject.cityName;
            this.NAD.codeListQualifier = nadObject.codeListQualifier;
            this.NAD.codeListResponsibleAgency = nadObject.codeListResponsibleAgency;
            this.NAD.countryCoded = nadObject.countryCoded;
            this.NAD.countrySubEntityID = nadObject.countrySubEntityID;
            this.NAD.nameAndAddress = nadObject.nameAndAddress;
            this.NAD.partyIDIdentification = nadObject.partyIDIdentification;
            this.NAD.partyName = nadObject.partyName;
            this.NAD.partyQualifier = nadObject.partyQualifier;
            this.NAD.postCodeID = nadObject.postCodeID;
            this.NAD.streetName = nadObject.streetName;
        }
    }

    [Serializable]
    public class GRP2Collection : ArrayList
    {
        public GRP2 Add(GRP2 obj)
        {
            base.Add(obj);
            return obj;
        }

        public GRP2 Add()
        {
            return Add(new GRP2());
        }

        public void Insert(int index, GRP2 obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(GRP2 obj)
        {
            base.Remove(obj);
        }

        new public GRP2 this[int index]
        {
            get { return (GRP2)base[index]; }
            set { base[index] = value; }
        }
    }

    #endregion

    #region GRP3
    [XmlType(TypeName = "GRP3", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP3
    {

        [XmlElement(Type = typeof(RFF), ElementName = "RFF", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public RFF __RFF;

        [XmlIgnore]
        public RFF RFF
        {
            get
            {
                if (__RFF == null) __RFF = new RFF();
                return __RFF;
            }
            set { __RFF = value; }
        }

        public GRP3()
        {
        }
    }

    #endregion

    #region GRP5
    [XmlType(TypeName = "GRP5", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP5
    {
        [XmlElement(Type = typeof(CTA), ElementName = "CTA", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public CTA __CTA;

        [XmlIgnore]
        public CTA CTA
        {
            get
            {
                if (__CTA == null) __CTA = new CTA();
                return __CTA;
            }
            set { __CTA = value; }
        }

        public GRP5()
        {
        }
    }

    [Serializable]
    public class GRP5Collection : ArrayList
    {
        public GRP5 Add(GRP5 obj)
        {
            base.Add(obj);
            return obj;
        }

        public GRP5 Add()
        {
            return Add(new GRP5());
        }

        public void Insert(int index, GRP5 obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(GRP5 obj)
        {
            base.Remove(obj);
        }

        new public GRP5 this[int index]
        {
            get { return (GRP5)base[index]; }
            set { base[index] = value; }
        }
    }
    #endregion

    #region GRP11
    [XmlType(TypeName = "GRP11", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP11
    {

        [XmlElement(Type = typeof(TOD), ElementName = "TOD", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public TOD __TOD;

        [XmlIgnore]
        public TOD TOD
        {
            get
            {
                if (__TOD == null) __TOD = new TOD();
                return __TOD;
            }
            set { __TOD = value; }
        }

        public GRP11()
        {
        }
    }

    #endregion

    #region GRP25
    [XmlType(TypeName = "GRP25", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP25
    {
        [XmlElement(Type = typeof(LIN_ORDRSP), ElementName = "LIN", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public LIN_ORDRSP __LIN;

        [XmlIgnore]
        public LIN_ORDRSP LIN
        {
            get
            {
                if (__LIN == null) __LIN = new LIN_ORDRSP();
                return __LIN;
            }
            set { __LIN = value; }
        }

        public GRP25() { }
        public GRP25(LIN lineObject)
        {
            this.LIN.actionRequest = lineObject.actionRequest;
            this.LIN.codeListQualifier = lineObject.actionRequest;
            this.LIN.codeListResponsibleAgency = lineObject.codeListResponsibleAgency;
            this.LIN.configurationCoded = lineObject.configurationCoded;
            this.LIN.configurationLevel = lineObject.configurationLevel;
            this.LIN.itemNumber = lineObject.itemNumber;
            this.LIN.itemNumberID = lineObject.itemNumberID;
            this.LIN.itemNumberType = lineObject.itemNumberType;
            this.LIN.lineItemNumber = lineObject.lineItemNumber;
            this.LIN.subLineIndicator = lineObject.subLineIndicator;
        }
    }

    [Serializable]
    public class GRP25Collection : ArrayList
    {
        public GRP25 Add(GRP25 obj)
        {
            base.Add(obj);
            return obj;
        }

        public GRP25 Add()
        {
            return Add(new GRP25());
        }

        public void Insert(int index, GRP25 obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(GRP25 obj)
        {
            base.Remove(obj);
        }

        new public GRP25 this[int index]
        {
            get { return (GRP25)base[index]; }
            set { base[index] = value; }
        }
    }
    #endregion

    #region GRP28
    [XmlType(TypeName = "GRP28", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP28
    {

        [XmlElement(Type = typeof(RFF), ElementName = "RFF", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public RFF __RFF;

        [XmlIgnore]
        public RFF RFF
        {
            get
            {
                if (__RFF == null) __RFF = new RFF();
                return __RFF;
            }
            set { __RFF = value; }
        }

        public GRP28()
        {
        }
    }

    #endregion

    #region GRP32
    [XmlType(TypeName = "GRP32", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP32
    {
        [XmlElement(Type = typeof(LOC_QD), ElementName = "LOC", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public LOC_QD __LOC;

        [XmlIgnore]
        public LOC_QD LOC_QD
        {
            get
            {
                if (__LOC == null) __LOC = new LOC_QD();
                return __LOC;
            }
            set { __LOC = value; }
        }

        public GRP32()
        {
        }
    }

    [Serializable]
    public class GRP32Collection : ArrayList
    {
        public GRP32 Add(GRP32 obj)
        {
            base.Add(obj);
            return obj;
        }

        public GRP32 Add()
        {
            return Add(new GRP32());
        }

        public void Insert(int index, GRP32 obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(GRP32 obj)
        {
            base.Remove(obj);
        }

        new public GRP32 this[int index]
        {
            get { return (GRP32)base[index]; } //(GRP32) 
            set { base[index] = value; }
        }
    }

    #endregion

    #region LIN_ORDRSP
    [XmlType(TypeName = "LIN_ORDRSP", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class LIN_ORDRSP : EDIFACT.BASETYPES.LIN
    {
        [XmlElement(Type = typeof(PIA), ElementName = "PIA", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public PIACollection __PIACollection;

        [XmlIgnore]
        public PIACollection PIACollection
        {
            get
            {
                if (__PIACollection == null) __PIACollection = new PIACollection();
                return __PIACollection;
            }
            set { __PIACollection = value; }
        }

        [XmlElement(Type = typeof(IMD), ElementName = "IMD", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public IMDCollection __IMDCollection;

        [XmlIgnore]
        public IMDCollection IMDCollection
        {
            get
            {
                if (__IMDCollection == null) __IMDCollection = new IMDCollection();
                return __IMDCollection;
            }
            set { __IMDCollection = value; }
        }

        [XmlElement(Type = typeof(QTY), ElementName = "QTY", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public QTYCollection __QTYCollection;

        [XmlIgnore]
        public QTYCollection QTYCollection
        {
            get
            {
                if (__QTYCollection == null) __QTYCollection = new QTYCollection();
                return __QTYCollection;
            }
            set { __QTYCollection = value; }
        }

        [XmlElement(Type = typeof(DTM), ElementName = "DTM", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public DTM __DTM;

        [XmlIgnore]
        public DTM DTM
        {
            get
            {
                if (__DTM == null) __DTM = new DTM();
                return __DTM;
            }
            set { __DTM = value; }
        }

        [XmlElement(Type = typeof(QVA), ElementName = "QVA", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public QVA __QVA;

        [XmlIgnore]
        public QVA QVA
        {
            get
            {
                if (__QVA == null) __QVA = new QVA();
                return __QVA;
            }
            set { __QVA = value; }
        }

        [XmlElement(Type = typeof(FTX), ElementName = "FTX", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public FTXCollection __FTXCollection;

        [XmlIgnore]
        public FTXCollection FTXCollection
        {
            get
            {
                if (__FTXCollection == null) __FTXCollection = new FTXCollection();
                return __FTXCollection;
            }
            set { __FTXCollection = value; }
        }

        [XmlElement(Type = typeof(GRP28), ElementName = "GRP28", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public GRP28 __GRP28;

        [XmlIgnore]
        public GRP28 GRP28
        {
            get
            {
                if (__GRP28 == null) __GRP28 = new GRP28();
                return __GRP28;
            }
            set { __GRP28 = value; }
        }

        [XmlElement(Type = typeof(GRP32), ElementName = "GRP32", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public GRP32Collection __GRP32Collection;

        [XmlIgnore]
        public GRP32Collection GRP32Collection
        {
            get
            {
                if (__GRP32Collection == null) __GRP32Collection = new GRP32Collection();
                return __GRP32Collection;
            }
            set { __GRP32Collection = value; }
        }

        public LIN_ORDRSP()
        {
        }
    }
    #endregion

    #region LOC_QD

    [XmlType(TypeName = "LOC_QD", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class LOC_QD : EDIFACT.BASETYPES.LOC
    {
        [XmlElement(Type = typeof(QTY), ElementName = "QTY", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public QTY __QTY;

        [XmlIgnore]
        public QTY QTY
        {
            get
            {
                if (__QTY == null) __QTY = new QTY();
                return __QTY;
            }
            set { __QTY = value; }
        }

        [XmlElement(Type = typeof(DTM), ElementName = "DTM", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public DTM __DTM;

        [XmlIgnore]
        public DTM DTM
        {
            get
            {
                if (__DTM == null) __DTM = new DTM();
                return __DTM;
            }
            set { __DTM = value; }
        }

        public LOC_QD()
        {
        }
        public LOC_QD(LOC locObject)
        {
            this.codeListQualifier = locObject.codeListQualifier;
            this.codeListResponsibleAgency = locObject.codeListResponsibleAgency;
            this.placeLocation = locObject.placeLocation;
            this.placeLocationID = locObject.placeLocationID;
            this.placeLocationQualifier = locObject.placeLocationQualifier;
            this.relatedPlaceLocation = locObject.relatedPlaceLocation;
            this.relatedPlaceLocationID = locObject.relatedPlaceLocationID;
            this.relationCoded = locObject.relationCoded;
            this.relCodeListQualifier = locObject.relCodeListQualifier;
            this.relCodeListResponsibleAgency = locObject.relCodeListResponsibleAgency;
        }
    }

    #endregion

    #region NADG3G5

    [XmlType(TypeName = "NADG3G5", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class NADG3G5 : EDIFACT.BASETYPES.NAD
    {
        [XmlElement(Type = typeof(GRP3), ElementName = "GRP3", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public GRP3 __GRP3;

        [XmlIgnore]
        public GRP3 GRP3
        {
            get
            {
                if (__GRP3 == null) __GRP3 = new GRP3();
                return __GRP3;
            }
            set { __GRP3 = value; }
        }

        [XmlElement(Type = typeof(GRP5), ElementName = "GRP5", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public GRP5Collection __GRP5;

        [XmlIgnore]
        public GRP5Collection GRP5Collection
        {
            get
            {
                if (__GRP5 == null) __GRP5 = new GRP5Collection();
                return __GRP5;
            }
            set { __GRP5 = value; }
        }

        public NADG3G5()
        {
        }
    }

    #endregion

    #endregion


}