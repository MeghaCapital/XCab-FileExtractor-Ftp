//--------------------------------------------------------------
// <CreatedBy>
//     Anthony R Yates
//	   July 7, 2005
//     Runtime Version: 1.1.4322.2032
//	   
//     It contains D96A ORDRSP Classes
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

namespace EDIFACT.D96A.ORDRSP
{

    public struct Declarations
    {
        public const string SchemaVersion = "http://www.default.com/D96A/ordrsp";
    }

    [XmlRoot(ElementName = "D96A_ORDRSP", Namespace = Declarations.SchemaVersion, IsNullable = false), Serializable]
    public class D96A_ORDRSP : IMessage
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

        public D96A_ORDRSP()
        {
        }

        ~D96A_ORDRSP()
        {
            this.ORDRSP = null;
        }

        #region IMessage Members

        public void PopulateMessage(ref Segment[] segments)
        {
            try
            {
                SegmentProcessor sp = new SegmentProcessor(new AddSegmentDelegate(ORDRSP.Add));
                sp.ProcessSegments(segments);
            }
            catch (Exception e)
            {
                throw new Exception((string.Format("Exception occured in \"SegmentProcessor\".\n{0}.\n{1}\n{2}", e.Message, e.InnerException, e.StackTrace)));
            }
        }

        #endregion
    }


    [XmlType(TypeName = "ORDRSP", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class ORDRSP
    {
        #region Class Members

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

        [XmlElement(Type = typeof(GRP12), ElementName = "GRP12", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public GRP12 __GRP12;

        [XmlIgnore]
        public GRP12 GRP12
        {
            get
            {
                if (__GRP12 == null) __GRP12 = new GRP12();
                return __GRP12;
            }
            set { __GRP12 = value; }
        }

        [XmlElement(Type = typeof(GRP26), ElementName = "GRP26", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public GRP26 __GRP26;

        [XmlIgnore]
        public GRP26 GRP26
        {
            get
            {
                if (__GRP26 == null) __GRP26 = new GRP26();
                return __GRP26;
            }
            set { __GRP26 = value; }
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

        public ORDRSP()
        {
        }

        #region Add Method

        public void Add(SegmentType type, object obj)
        {
            int i = 0;
            int j = 0;

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

                        if (qualifier == 44)
                        {
                            if ((i = this.GRP26.Count) > 0)
                                this.GRP26[i - 1].DTM = (DTM)obj;
                            break;
                        }
                        else
                        {
                            if (qualifier == 69 && (i = this.GRP26.Count) > 0)
                                if ((j = GRP26[i - 1].GRP35.Count) > 0)
                                    GRP26[i - 1].GRP35[j - 1].DTM = (DTM)obj;

                            //2, 63, 64, 69, 72, 75, 77, 137
                            this.DTMCollection.Add((DTM)obj);
                        }
                        break;
                    }
                case SegmentType.NAD:
                    {
                        this.GRP3.Add(new ORDRSPNAD((NAD)obj));
                        break;
                    }
                case SegmentType.CTA:
                    {
                        if ((i = GRP3.Count) > 0)
                            this.GRP3[i - 1].GRP6.Add((CTA)obj);
                        break;
                    }
                case SegmentType.RFF:
                    {
                        string qualifier = ((RFF)obj).referenceQualifier;

                        if (qualifier == "ON")
                        {
                            this.GRP1.RFF = (RFF)obj;
                            if ((i = this.GRP3.Count) > 0)
                                GRP3[i - 1].GRP4.RFF = (RFF)obj;
                        }
                        else if (qualifier == "GN" || qualifier == "VA")
                        {
                            if ((i = GRP3.Count) > 0)
                                this.GRP3[i - 1].GRP4.RFF = (RFF)obj;
                        }
                        break;
                    }
                case SegmentType.TOD:
                    {
                        this.GRP12.TOD = (TOD)obj;
                        break;
                    }
                case SegmentType.LIN:
                    {
                        this.GRP26.Add(new ORDRSPLIN((LIN)obj));
                        break;
                    }
                case SegmentType.PIA:
                    {
                        if ((i = GRP26.Count) > 0)
                            GRP26[i - 1].PIACollection.Add((PIA)obj);
                        break;
                    }
                case SegmentType.IMD:
                    {
                        if ((i = GRP26.Count) > 0)
                            GRP26[i - 1].IMDCollection.Add((IMD)obj);
                        break;
                    }
                case SegmentType.QTY:
                    {
                        if ((i = GRP26.Count) > 0)
                            GRP26[i - 1].QTYCollection.Add(new QTY((QTY)obj));
                        break;
                    }
                case SegmentType.QVR:
                    {
                        if ((i = GRP26.Count) > 0)
                            GRP26[i - 1].QVR = (QVR)obj;
                        break;
                    }
                case SegmentType.FTX:
                    {
                        if ((i = GRP26.Count) > 0)
                            GRP26[i - 1].FTXCollection.Add((FTX)obj);
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

    #region D96A Specific Classes

    #region GRP 1
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

    #region GRP 3
    [XmlType(TypeName = "GRP3", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP3
    {
        [System.Runtime.InteropServices.DispIdAttribute(-4)]
        public IEnumerator GetEnumerator()
        {
            return ORDRSPNADCollection.GetEnumerator();
        }

        public ORDRSPNAD Add(ORDRSPNAD obj)
        {
            return ORDRSPNADCollection.Add(obj);
        }

        [XmlIgnore]
        public ORDRSPNAD this[int index]
        {
            get { return (ORDRSPNAD)ORDRSPNADCollection[index]; }
        }

        [XmlIgnore]
        public int Count
        {
            get { return ORDRSPNADCollection.Count; }
        }

        public void Clear()
        {
            ORDRSPNADCollection.Clear();
        }

        public ORDRSPNAD Remove(int index)
        {
            ORDRSPNAD obj = ORDRSPNADCollection[index];
            ORDRSPNADCollection.Remove(obj);
            return obj;
        }

        public void Remove(object obj)
        {
            ORDRSPNADCollection.Remove(obj);
        }

        [XmlElement(Type = typeof(ORDRSPNAD), ElementName = "NAD", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public ORDRSPNADCollection __NADCollection;

        [XmlIgnore]
        public ORDRSPNADCollection ORDRSPNADCollection
        {
            get
            {
                if (__NADCollection == null) __NADCollection = new ORDRSPNADCollection();
                return __NADCollection;
            }
            set { __NADCollection = value; }
        }

        public GRP3()
        {
        }
    }


    [XmlType(TypeName = "NAD", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class ORDRSPNAD : EDIFACT.BASETYPES.NAD
    {

        [XmlElement(Type = typeof(GRP4), ElementName = "GRP4", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public GRP4 __GRP4;

        [XmlIgnore]
        public GRP4 GRP4
        {
            get
            {
                if (__GRP4 == null) __GRP4 = new GRP4();
                return __GRP4;
            }
            set { __GRP4 = value; }
        }

        [XmlElement(Type = typeof(GRP6), ElementName = "GRP6", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public GRP6 __GRP6;

        [XmlIgnore]
        public GRP6 GRP6
        {
            get
            {
                if (__GRP6 == null) __GRP6 = new GRP6();
                return __GRP6;
            }
            set { __GRP6 = value; }
        }

        public ORDRSPNAD()
        {
        }

        public ORDRSPNAD(NAD nadObject)
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
    public class ORDRSPNADCollection : ArrayList
    {
        public ORDRSPNAD Add(ORDRSPNAD obj)
        {
            base.Add(obj);
            return obj;
        }

        public ORDRSPNAD Add()
        {
            return Add(new ORDRSPNAD());
        }

        public void Insert(int index, ORDRSPNAD obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(ORDRSPNAD obj)
        {
            base.Remove(obj);
        }

        new public ORDRSPNAD this[int index]
        {
            get { return (ORDRSPNAD)base[index]; }
            set { base[index] = value; }
        }
    }
    #endregion


    [XmlType(TypeName = "GRP4", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP4
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

        public GRP4()
        {
        }
    }

    [XmlType(TypeName = "GRP6", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP6
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

        public GRP6()
        {
        }
    }


    [XmlType(TypeName = "GRP12", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP12
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

        public GRP12()
        {
        }
    }

    [XmlType(TypeName = "GRP26", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP26
    {
        [System.Runtime.InteropServices.DispIdAttribute(-4)]
        public IEnumerator GetEnumerator()
        {
            return ORDRSPLINCollection.GetEnumerator();
        }

        public ORDRSPLIN Add(ORDRSPLIN obj)
        {
            return ORDRSPLINCollection.Add(obj);
        }

        [XmlIgnore]
        public ORDRSPLIN this[int index]
        {
            get { return (ORDRSPLIN)ORDRSPLINCollection[index]; }
        }

        [XmlIgnore]
        public int Count
        {
            get { return ORDRSPLINCollection.Count; }
        }

        public void Clear()
        {
            ORDRSPLINCollection.Clear();
        }

        public ORDRSPLIN Remove(int index)
        {
            ORDRSPLIN obj = ORDRSPLINCollection[index];
            ORDRSPLINCollection.Remove(obj);
            return obj;
        }

        public void Remove(object obj)
        {
            ORDRSPLINCollection.Remove(obj);
        }

        [XmlElement(Type = typeof(ORDRSPLIN), ElementName = "LIN", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public ORDRSPLINCollection __LINCollection;

        [XmlIgnore]
        public ORDRSPLINCollection ORDRSPLINCollection
        {
            get
            {
                if (__LINCollection == null) __LINCollection = new ORDRSPLINCollection();
                return __LINCollection;
            }
            set { __LINCollection = value; }
        }

        public GRP26()
        {
        }
    }


    [XmlType(TypeName = "LIN", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class ORDRSPLIN : EDIFACT.BASETYPES.LIN
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

        [XmlElement(Type = typeof(QVR), ElementName = "QVR", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public QVR __QVR;

        [XmlIgnore]
        public QVR QVR
        {
            get
            {
                if (__QVR == null) __QVR = new QVR();
                return __QVR;
            }
            set { __QVR = value; }
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

        [XmlElement(Type = typeof(GRP31), ElementName = "GRP31", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public GRP31 __GRP31;

        [XmlIgnore]
        public GRP31 GRP31
        {
            get
            {
                if (__GRP31 == null) __GRP31 = new GRP31();
                return __GRP31;
            }
            set { __GRP31 = value; }
        }

        [XmlElement(Type = typeof(GRP35), ElementName = "GRP35", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public GRP35 __GRP35;

        [XmlIgnore]
        public GRP35 GRP35
        {
            get
            {
                if (__GRP35 == null) __GRP35 = new GRP35();
                return __GRP35;
            }
            set { __GRP35 = value; }
        }

        public ORDRSPLIN()
        {
        }

        public ORDRSPLIN(LIN linObject)
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
    public class ORDRSPLINCollection : ArrayList
    {
        public ORDRSPLIN Add(ORDRSPLIN obj)
        {
            base.Add(obj);
            return obj;
        }

        public ORDRSPLIN Add()
        {
            return Add(new ORDRSPLIN());
        }

        public void Insert(int index, ORDRSPLIN obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(ORDRSPLIN obj)
        {
            base.Remove(obj);
        }

        new public ORDRSPLIN this[int index]
        {
            get { return (ORDRSPLIN)base[index]; }
            set { base[index] = value; }
        }
    }


    [XmlType(TypeName = "GRP31", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP31
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

        public GRP31()
        {
        }
    }


    [XmlType(TypeName = "GRP35", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP35
    {
        [System.Runtime.InteropServices.DispIdAttribute(-4)]
        public IEnumerator GetEnumerator()
        {
            return ORDRSPLOCCollection.GetEnumerator();
        }

        public ORDRSPLOC Add(ORDRSPLOC obj)
        {
            return ORDRSPLOCCollection.Add(obj);
        }

        [XmlIgnore]
        public ORDRSPLOC this[int index]
        {
            get { return (ORDRSPLOC)ORDRSPLOCCollection[index]; }
        }

        [XmlIgnore]
        public int Count
        {
            get { return ORDRSPLOCCollection.Count; }
        }

        public void Clear()
        {
            ORDRSPLOCCollection.Clear();
        }

        public ORDRSPLOC Remove(int index)
        {
            ORDRSPLOC obj = ORDRSPLOCCollection[index];
            ORDRSPLOCCollection.Remove(obj);
            return obj;
        }

        public void Remove(object obj)
        {
            ORDRSPLOCCollection.Remove(obj);
        }

        [XmlElement(Type = typeof(ORDRSPLOC), ElementName = "LOC", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public ORDRSPLOCCollection __LOCCollection;

        [XmlIgnore]
        public ORDRSPLOCCollection ORDRSPLOCCollection
        {
            get
            {
                if (__LOCCollection == null) __LOCCollection = new ORDRSPLOCCollection();
                return __LOCCollection;
            }
            set { __LOCCollection = value; }
        }

        public GRP35()
        {
        }
    }


    [XmlType(TypeName = "LOC", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class ORDRSPLOC : EDIFACT.BASETYPES.LOC
    {
        [XmlElement(Type = typeof(QTY), ElementName = "QTY", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
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

        public ORDRSPLOC()
        {
        }
    }


    [Serializable]
    public class ORDRSPLOCCollection : ArrayList
    {
        public ORDRSPLOC Add(ORDRSPLOC obj)
        {
            base.Add(obj);
            return obj;
        }

        public ORDRSPLOC Add()
        {
            return Add(new ORDRSPLOC());
        }

        public void Insert(int index, ORDRSPLOC obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(ORDRSPLOC obj)
        {
            base.Remove(obj);
        }

        new public ORDRSPLOC this[int index]
        {
            get { return (ORDRSPLOC)base[index]; }
            set { base[index] = value; }
        }
    }

    #endregion
}
