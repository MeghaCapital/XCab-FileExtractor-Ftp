//--------------------------------------------------------------
// <CreatedBy>
//     Anthony R Yates
//	   Jul. 06, 2005
//     Runtime Version: 1.1.4322.2032
//	   
//     It contains D96A ORDERS Classes
// </CreatedBy>

// Copyright (C) 2005 Anthony Yates a.yates@iosolutionsinc.com
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

namespace EDIFACT.D96A.ORDERS
{

    #region Schema Declaration

    public struct Declarations
    {
        public const string SchemaVersion = "http://www.default.com/D96A/orders";
    }

    #endregion

    #region D96AORDERS Class

    [XmlRoot(ElementName = "D96AORDERS", Namespace = Declarations.SchemaVersion, IsNullable = false), Serializable]
    public class D96A_ORDERS : IMessage
    {

        [XmlElement(Type = typeof(ORDERS), ElementName = "ORDERS", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public ORDERS __ORDERS;

        [XmlIgnore]
        public ORDERS ORDERS
        {
            get
            {
                if (__ORDERS == null) __ORDERS = new ORDERS();
                return __ORDERS;
            }
            set { __ORDERS = value; }
        }

        public D96A_ORDERS()
        {
        }

        ~D96A_ORDERS()
        {
            this.ORDERS = null;
        }

        #region IMessage Members

        public void PopulateMessage(ref Segment[] segments)
        {
            try
            {
                SegmentProcessor sp = new SegmentProcessor(new AddSegmentDelegate(this.ORDERS.Add));
                sp.ProcessSegments(segments);
            }
            catch (Exception e)
            {
                throw new Exception((string.Format("Exception occured in \"SegmentProcessor\".\n{0}.\n{1}\n{2}", e.Message, e.InnerException, e.StackTrace)));
            }
        }

        #endregion
    }

    #endregion

    #region ORDERS Class

    [XmlType(TypeName = "ORDERS", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class ORDERS
    {

        #region Class Fields

        //privates
        SegmentType lastAccessed;

        [XmlElement(Type = typeof(UNH), ElementName = "UNH", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
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
        [EditorBrowsable(EditorBrowsableState.Advanced)]
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
        [EditorBrowsable(EditorBrowsableState.Advanced)]
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
        [EditorBrowsable(EditorBrowsableState.Advanced)]
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
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public GRP2 __GRP2;

        [XmlIgnore]
        public GRP2 GRP2
        {
            get
            {
                if (__GRP2 == null) __GRP2 = new GRP2();
                return __GRP2;
            }
            set { __GRP2 = value; }
        }

        [XmlElement(Type = typeof(GRP11), ElementName = "GRP11", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
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
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public GRP25 __GRP25;

        [XmlIgnore]
        public GRP25 GRP25
        {
            get
            {
                if (__GRP25 == null) __GRP25 = new GRP25();
                return __GRP25;
            }
            set { __GRP25 = value; }
        }

        [XmlElement(Type = typeof(UNS), ElementName = "UNS", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
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
        [EditorBrowsable(EditorBrowsableState.Advanced)]
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
        [EditorBrowsable(EditorBrowsableState.Advanced)]
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

        public ORDERS()
        {
        }

        #endregion

        #region Add Method

        public void Add(SegmentType type, object obj)
        {
            int i = 0;
            //int j = 0;

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
                        int qualifier = Int32.Parse(((DTM)obj).dateTimePeriodQualifier);

                        if (qualifier == 171)
                        {
                            if ((i = this.GRP1.Count) > 0)
                                this.GRP1[i - 1].DTM = (DTM)obj;
                        }
                        else
                        {   //2, 63, 64, 137
                            this.DTMCollection.Add((DTM)obj);
                        }
                        break;
                    }
                case SegmentType.NAD:
                    {
                        this.GRP2.Add(new D96AORDERSNAD((NAD)obj));
                        break;
                    }
                case SegmentType.CTA:
                    {
                        if ((i = GRP2.Count) > 0)
                            this.GRP2[i - 1].GRP5.Add((CTA)obj);
                        break;
                    }
                case SegmentType.RFF:
                    {
                        string qualifier = ((RFF)obj).referenceQualifier;

                        if (qualifier == "CT" || qualifier == "PL")
                        {
                            this.GRP1.Add(new RFFDTM((RFF)obj));
                        }
                        else if (qualifier == "GN" || qualifier == "VA")
                        {
                            if ((i = GRP2.Count) > 0)
                                this.GRP2[i - 1].GRP3.RFF = (RFF)obj;
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
                        this.GRP25.Add(new D96ALIN((LIN)obj));
                        lastAccessed = type;
                        break;
                    }
                case SegmentType.PIA:
                    {
                        if ((i = GRP25.Count) > 0)
                            GRP25[i - 1].PIACollection.Add((PIA)obj);
                        break;
                    }
                case SegmentType.IMD:
                    {
                        if ((i = GRP25.Count) > 0)
                            GRP25[i - 1].IMDCollection.Add((IMD)obj);
                        break;
                    }
                case SegmentType.QTY:
                    {
                        if ((i = GRP25.Count) > 0)
                            GRP25[i - 1].QTY = (QTY)obj;
                        break;
                    }
                case SegmentType.FTX:
                    {
                        if ((i = GRP25.Count) > 0)
                            GRP25[i - 1].FTXCollection.Add((FTX)obj);
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
        }

        #endregion
    }

    #endregion


    [XmlType(TypeName = "GRP1", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP1
    {
        [System.Runtime.InteropServices.DispIdAttribute(-4)]
        public IEnumerator GetEnumerator()
        {
            return RFFDTMCollection.GetEnumerator();
        }

        public RFFDTM Add(RFFDTM obj)
        {
            return RFFDTMCollection.Add(obj);
        }

        [XmlIgnore]
        public RFFDTM this[int index]
        {
            get { return (RFFDTM)RFFDTMCollection[index]; }
        }

        [XmlIgnore]
        public int Count
        {
            get { return RFFDTMCollection.Count; }
        }

        public void Clear()
        {
            RFFDTMCollection.Clear();
        }

        public RFFDTM Remove(int index)
        {
            RFFDTM obj = RFFDTMCollection[index];
            RFFDTMCollection.Remove(obj);
            return obj;
        }

        public void Remove(object obj)
        {
            RFFDTMCollection.Remove(obj);
        }

        [XmlElement(Type = typeof(RFFDTM), ElementName = "RFF", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public RFFDTMCollection __RFFDTMCollection;

        [XmlIgnore]
        public RFFDTMCollection RFFDTMCollection
        {
            get
            {
                if (__RFFDTMCollection == null) __RFFDTMCollection = new RFFDTMCollection();
                return __RFFDTMCollection;
            }
            set { __RFFDTMCollection = value; }
        }

        public GRP1()
        {
        }
    }

    [XmlType(TypeName = "GRP2", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP2
    {
        [System.Runtime.InteropServices.DispIdAttribute(-4)]
        public IEnumerator GetEnumerator()
        {
            return D96AORDERSNADCollection.GetEnumerator();
        }

        public D96AORDERSNAD Add(D96AORDERSNAD obj)
        {
            return D96AORDERSNADCollection.Add(obj);
        }

        [XmlIgnore]
        public D96AORDERSNAD this[int index]
        {
            get { return (D96AORDERSNAD)D96AORDERSNADCollection[index]; }
        }

        [XmlIgnore]
        public int Count
        {
            get { return D96AORDERSNADCollection.Count; }
        }

        public void Clear()
        {
            D96AORDERSNADCollection.Clear();
        }

        public D96AORDERSNAD Remove(int index)
        {
            D96AORDERSNAD obj = D96AORDERSNADCollection[index];
            D96AORDERSNADCollection.Remove(obj);
            return obj;
        }

        public void Remove(object obj)
        {
            D96AORDERSNADCollection.Remove(obj);
        }

        [XmlElement(Type = typeof(D96AORDERSNAD), ElementName = "NAD", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public D96AORDERSNADCollection __NADCollection;

        [XmlIgnore]
        public D96AORDERSNADCollection D96AORDERSNADCollection
        {
            get
            {
                if (__NADCollection == null) __NADCollection = new D96AORDERSNADCollection();
                return __NADCollection;
            }
            set { __NADCollection = value; }
        }

        public GRP2()
        {
        }
    }


    [XmlType(TypeName = "NAD", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class D96AORDERSNAD : NAD
    {
        //INHERIT FROM NAD

        [XmlElement(Type = typeof(GRP3), ElementName = "GRP3", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
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
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public GRP5 __GRP5;

        [XmlIgnore]
        public GRP5 GRP5
        {
            get
            {
                if (__GRP5 == null) __GRP5 = new GRP5();
                return __GRP5;
            }
            set { __GRP5 = value; }
        }

        public D96AORDERSNAD()
        {
        }

        public D96AORDERSNAD(NAD nadObject)
        {
            this.cityName = nadObject.cityName;
            this.codeListQualifier = nadObject.codeListQualifier;
            this.codeListResponsibleAgency = nadObject.codeListResponsibleAgency;
            this.countryCoded = nadObject.countryCoded;
            this.countrySubEntityID = nadObject.countrySubEntityID;
            this.nameAndAddress = nadObject.nameAndAddress;
            this.partyIDIdentification = nadObject.partyIDIdentification;
            this.partyName = nadObject.partyName;
            this.partyQualifier = nadObject.partyQualifier;
            this.postCodeID = nadObject.postCodeID;
            this.streetName = nadObject.streetName;
        }
    }

    [Serializable]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public class D96AORDERSNADCollection : ArrayList
    {
        public D96AORDERSNAD Add(D96AORDERSNAD obj)
        {
            base.Add(obj);
            return obj;
        }

        public D96AORDERSNAD Add()
        {
            return Add(new D96AORDERSNAD());
        }

        public void Insert(int index, D96AORDERSNAD obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(D96AORDERSNAD obj)
        {
            base.Remove(obj);
        }

        new public D96AORDERSNAD this[int index]
        {
            get { return (D96AORDERSNAD)base[index]; }
            set { base[index] = value; }
        }
    }


    [XmlType(TypeName = "GRP3", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP3
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

        public GRP3()
        {
        }
    }


    [XmlType(TypeName = "GRP5", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP5
    {
        [System.Runtime.InteropServices.DispIdAttribute(-4)]
        public IEnumerator GetEnumerator()
        {
            return CTACollection.GetEnumerator();
        }

        public CTA Add(CTA obj)
        {
            return CTACollection.Add(obj);
        }

        [XmlIgnore]
        public CTA this[int index]
        {
            get { return (CTA)CTACollection[index]; }
        }

        [XmlIgnore]
        public int Count
        {
            get { return CTACollection.Count; }
        }

        public void Clear()
        {
            CTACollection.Clear();
        }

        public CTA Remove(int index)
        {
            CTA obj = CTACollection[index];
            CTACollection.Remove(obj);
            return obj;
        }

        public void Remove(object obj)
        {
            CTACollection.Remove(obj);
        }

        [XmlElement(Type = typeof(CTA), ElementName = "CTA", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public CTACollection __CTACollection;

        [XmlIgnore]
        public CTACollection CTACollection
        {
            get
            {
                if (__CTACollection == null) __CTACollection = new CTACollection();
                return __CTACollection;
            }
            set { __CTACollection = value; }
        }

        public GRP5()
        {
        }
    }

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

    [XmlType(TypeName = "GRP25", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP25
    {
        [System.Runtime.InteropServices.DispIdAttribute(-4)]
        public IEnumerator GetEnumerator()
        {
            return D96ALINCollection.GetEnumerator();
        }

        public D96ALIN Add(D96ALIN obj)
        {
            return D96ALINCollection.Add(obj);
        }

        [XmlIgnore]
        public D96ALIN this[int index]
        {
            get { return (D96ALIN)D96ALINCollection[index]; }
        }

        [XmlIgnore]
        public int Count
        {
            get { return D96ALINCollection.Count; }
        }

        public void Clear()
        {
            D96ALINCollection.Clear();
        }

        public D96ALIN Remove(int index)
        {
            D96ALIN obj = D96ALINCollection[index];
            D96ALINCollection.Remove(obj);
            return obj;
        }

        public void Remove(object obj)
        {
            D96ALINCollection.Remove(obj);
        }

        [XmlElement(Type = typeof(D96ALIN), ElementName = "LIN", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public D96ALINCollection __D96ALINCollection;

        [XmlIgnore]
        public D96ALINCollection D96ALINCollection
        {
            get
            {
                if (__D96ALINCollection == null) __D96ALINCollection = new D96ALINCollection();
                return __D96ALINCollection;
            }
            set { __D96ALINCollection = value; }
        }

        public GRP25()
        {
        }
    }

    /***********************************************************************************
	 * *********************************************************************************
	 * SPECIAL TYPES THAT COMBINE BASE TYPES********************************************
	 * *********************************************************************************
	 * *********************************************************************************/

    [XmlType(TypeName = "RFFDTM", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class RFFDTM : EDIFACT.BASETYPES.RFF
    {

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

        public RFFDTM()
        {
        }
        public RFFDTM(RFF rffObject)
        {
            this.lineNumber = rffObject.lineNumber;
            this.referenceNumber = rffObject.referenceNumber;
            this.referenceQualifier = rffObject.referenceQualifier;
            this.referenceVersionNumber = rffObject.referenceVersionNumber;
        }
    }

    [Serializable]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public class RFFDTMCollection : ArrayList
    {
        public RFFDTM Add(RFFDTM obj)
        {
            base.Add(obj);
            return obj;
        }

        public RFFDTM Add()
        {
            return Add(new RFFDTM());
        }

        public void Insert(int index, RFFDTM obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(RFFDTM obj)
        {
            base.Remove(obj);
        }

        new public RFFDTM this[int index]
        {
            get { return (RFFDTM)base[index]; }
            set { base[index] = value; }
        }
    }

    [XmlType(TypeName = "D96ALIN", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class D96ALIN : EDIFACT.BASETYPES.LIN
    {

        [XmlElement(Type = typeof(PIA), ElementName = "PIA", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
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

        [XmlElement(Type = typeof(FTX), ElementName = "FTX", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
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

        public D96ALIN()
        {
        }

        public D96ALIN(LIN linObject)
        {
            this.actionRequest = linObject.actionRequest;
            this.codeListQualifier = linObject.codeListQualifier;
            this.codeListResponsibleAgency = linObject.codeListResponsibleAgency;
            this.configurationCoded = linObject.configurationCoded;
            this.configurationLevel = linObject.configurationLevel;
            this.itemNumber = linObject.itemNumber;
            this.itemNumberID = linObject.itemNumberID;
            this.itemNumberType = linObject.itemNumberType;
            this.lineItemNumber = linObject.lineItemNumber;
            this.subLineIndicator = linObject.subLineIndicator;
        }
    }

    [Serializable]
    public class D96ALINCollection : ArrayList
    {
        public D96ALIN Add(D96ALIN obj)
        {
            base.Add(obj);
            return obj;
        }

        public D96ALIN Add()
        {
            return Add(new D96ALIN());
        }

        public void Insert(int index, D96ALIN obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(D96ALIN obj)
        {
            base.Remove(obj);
        }

        new public D96ALIN this[int index]
        {
            get { return (D96ALIN)base[index]; }
            set { base[index] = value; }
        }
    }
}
