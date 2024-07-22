//--------------------------------------------------------------
// <CreatedBy>
//     Anthony R Yates
//	   Feb. 26, 2005
//     Runtime Version: 1.1.4322.2032
//	   
//     Contains D93A DESADV Classes
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

namespace EDIFACT.D93A.DESADV
{
    #region Declaration Schema Version
    public struct Declarations
    {
        public const string SchemaVersion = "http://www.default.com/D93A/desadv";
    }
    #endregion

    #region D93A_DESADV Class

    [XmlRoot(ElementName = "D93ADESADV", Namespace = Declarations.SchemaVersion, IsNullable = false), Serializable]
    public class D93A_DESADV : IMessage
    {

        [XmlElement(Type = typeof(DESADV), ElementName = "DESADV", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public DESADV __DESADV;

        [XmlIgnore]
        public DESADV DESADV
        {
            get
            {
                if (__DESADV == null) __DESADV = new DESADV();
                return __DESADV;
            }
            set { __DESADV = value; }
        }

        public D93A_DESADV()
        {
        }
        #region IMessage Members

        public void PopulateMessage(ref Segment[] segments)
        {
            try
            {
                SegmentProcessor sp = new SegmentProcessor(new AddSegmentDelegate(this.DESADV.Add));
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

    #region DESADV Class

    [XmlType(TypeName = "DESADV", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class DESADV
    {
        bool linAccessed;

        #region DESADV Members

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
        public GRP1Collection __GRP1Collection;

        [XmlIgnore]
        public GRP1Collection GRP1Collection
        {
            get
            {
                if (__GRP1Collection == null) __GRP1Collection = new GRP1Collection();
                return __GRP1Collection;
            }
            set { __GRP1Collection = value; }
        }

        [XmlElement(Type = typeof(GRP2), ElementName = "GRP2", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
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

        [XmlElement(Type = typeof(GRP10), ElementName = "GRP10", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public GRP10Collection __GRP10Collection;

        [XmlIgnore]
        public GRP10Collection GRP10Collection
        {
            get
            {
                if (__GRP10Collection == null) __GRP10Collection = new GRP10Collection();
                return __GRP10Collection;
            }
            set { __GRP10Collection = value; }
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

        public DESADV()
        {
        }
        #endregion

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
                        if (((DTM)obj).dateTimePeriodQualifier == "69" ||
                            ((DTM)obj).dateTimePeriodQualifier == "137" ||
                            ((DTM)obj).dateTimePeriodQualifier == "200")
                        {
                            this.DTMCollection.Add((DTM)obj);
                        } //TODO : Fix dateTimePeriodQualifier Datatype
                        else if (((DTM)obj).dateTimePeriodQualifier == "94" ||
                                 ((DTM)obj).dateTimePeriodQualifier == "21E" ||
                                 ((DTM)obj).dateTimePeriodQualifier == "22E" ||
                                 ((DTM)obj).dateTimePeriodQualifier == "23E")
                        {
                            int i, j, k;
                            if ((i = this.GRP10Collection.Count) > 0)
                            {
                                if ((j = this.GRP10Collection[i - 1].CPS.GRP15Collection.Count) > 0)
                                {
                                    if ((k = this.GRP10Collection[i - 1].CPS.GRP15Collection[j - 1].LIN.GRP20Collection.Count) > 0)
                                    {
                                        this.GRP10Collection[i - 1].CPS.GRP15Collection[j - 1].LIN.GRP20Collection[k - 1].PCI.DTMCollection.Add(((DTM)obj));
                                    }
                                }
                            }

                        }
                        break;
                    }
                case SegmentType.RFF:
                    {
                        if (((RFF)obj).referenceQualifier == "AAK" ||
                            ((RFF)obj).referenceQualifier == "ON" ||
                            ((RFF)obj).referenceQualifier == "VN")
                        {
                            if (linAccessed)
                            {
                                int i, j;
                                if ((i = this.GRP10Collection.Count) > 0)
                                    if ((j = this.GRP10Collection[i - 1].CPS.GRP15Collection.Count) > 0)
                                        this.GRP10Collection[i - 1].CPS.GRP15Collection[j - 1].LIN.GRP16Collection.Add(new GRP16((RFF)obj));
                            }
                            this.GRP1Collection.Add(new GRP1((RFF)obj));
                        }
                        else if (((RFF)obj).referenceQualifier == "GN" ||
                                 ((RFF)obj).referenceQualifier == "VA")
                        {
                            int i;
                            if ((i = this.GRP2Collection.Count) > 0)
                                this.GRP2Collection[i - 1].NAD.GRP3.RFF = (RFF)obj;
                        }
                        break;
                    }
                case SegmentType.NAD:
                    {
                        this.GRP2Collection.Add(new GRP2((NAD)obj));
                        break;
                    }
                case SegmentType.CTA:
                    {
                        int i;
                        if ((i = this.GRP2Collection.Count) > 0)
                            this.GRP2Collection[i - 1].NAD.GRP4Collection.Add(new GRP4((CTA)obj));
                        break;
                    }
                case SegmentType.TOD:
                    {
                        this.GRP5.TOD = (TOD)obj;
                        break;
                    }
                case SegmentType.CPS:
                    {
                        this.GRP10Collection.Add(new GRP10((CPS)obj));
                        break;
                    }
                case SegmentType.PAC:
                    {
                        int i;
                        if ((i = this.GRP10Collection.Count) > 0)
                            this.GRP10Collection[i - 1].CPS.GRP11Collection.Add(new GRP11((PAC)obj));
                        break;
                    }
                case SegmentType.MEA:
                    {
                        int i, j;
                        if ((i = this.GRP10Collection.Count) > 0)
                        {
                            if ((j = this.GRP10Collection[i - 1].CPS.GRP11Collection.Count) > 0)
                            {
                                this.GRP10Collection[i - 1].CPS.GRP11Collection[j - 1].PAC.MEACollection.Add((MEA)obj);
                            }
                        }
                        break;
                    }
                case SegmentType.HAN:
                    {
                        int i, j;
                        if ((i = this.GRP10Collection.Count) > 0)
                        {
                            if ((j = this.GRP10Collection[i - 1].CPS.GRP11Collection.Count) > 0)
                            {
                                this.GRP10Collection[i - 1].CPS.GRP11Collection[j - 1].PAC.GRP12Collection.Add(new GRP12((HAN)obj));
                            }
                        }
                        break;
                    }
                case SegmentType.PCI:
                    {
                        int i, j;
                        if ((i = this.GRP10Collection.Count) > 0)
                        {
                            if (linAccessed)
                            {
                                if ((j = this.GRP10Collection[i - 1].CPS.GRP15Collection.Count) > 0)
                                    this.GRP10Collection[i - 1].CPS.GRP15Collection[j - 1].LIN.GRP20Collection.Add(new GRP20((PCI)obj));
                                break;
                            }
                            if ((j = this.GRP10Collection[i - 1].CPS.GRP11Collection.Count) > 0)
                            {
                                this.GRP10Collection[i - 1].CPS.GRP11Collection[j - 1].PAC.GRP13Collection.Add(new GRP13((PCI)obj));
                            }
                        }
                        break;
                    }
                case SegmentType.GIN:
                    {

                        int i, j, k;
                        if ((i = this.GRP10Collection.Count) > 0)
                        {
                            if ((j = this.GRP10Collection[i - 1].CPS.GRP11Collection.Count) > 0)
                            {
                                if ((k = this.GRP10Collection[i - 1].CPS.GRP11Collection[j - 1].PAC.GRP13Collection.Count) > 0)
                                {
                                    this.GRP10Collection[i - 1].CPS.GRP11Collection[j - 1].PAC.GRP13Collection[k - 1].PCI.GRP14Collection.Add(new GRP14((GIN)obj));
                                }
                            }
                        }
                        break;
                    }
                case SegmentType.LIN:
                    {
                        linAccessed = true;
                        int i;
                        if ((i = this.GRP10Collection.Count) > 0)
                            this.GRP10Collection[i - 1].CPS.GRP15Collection.Add(new GRP15((LIN)obj));
                        break;
                    }
                case SegmentType.PIA:
                    {
                        int i, j;
                        if ((i = this.GRP10Collection.Count) > 0)
                            if ((j = this.GRP10Collection[i - 1].CPS.GRP15Collection.Count) > 0)
                                this.GRP10Collection[i - 1].CPS.GRP15Collection[j - 1].LIN.PIACollection.Add((PIA)obj);
                        break;
                    }
                case SegmentType.IMD:
                    {
                        int i, j;
                        if ((i = this.GRP10Collection.Count) > 0)
                            if ((j = this.GRP10Collection[i - 1].CPS.GRP15Collection.Count) > 0)
                                this.GRP10Collection[i - 1].CPS.GRP15Collection[j - 1].LIN.IMDCollection.Add((IMD)obj);
                        break;
                    }
                case SegmentType.QTY:
                    {
                        int i, j;
                        if ((i = this.GRP10Collection.Count) > 0)
                            if ((j = this.GRP10Collection[i - 1].CPS.GRP15Collection.Count) > 0)
                                this.GRP10Collection[i - 1].CPS.GRP15Collection[j - 1].LIN.QTYCollection.Add((QTY)obj);
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
    }

    #endregion

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

        public GRP1(RFF rffObject)
        {
            this.RFF = rffObject;
        }
    }

    [Serializable]
    public class GRP1Collection : ArrayList
    {
        public GRP1 Add(GRP1 obj)
        {
            base.Add(obj);
            return obj;
        }

        public GRP1 Add()
        {
            return Add(new GRP1());
        }

        public void Insert(int index, GRP1 obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(GRP1 obj)
        {
            base.Remove(obj);
        }

        new public GRP1 this[int index]
        {
            get { return (GRP1)base[index]; }
            set { base[index] = value; }
        }
    }

    #endregion

    #region GRP2

    [XmlType(TypeName = "GRP2", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP2
    {

        [XmlElement(Type = typeof(NAD_RC), ElementName = "NAD", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public NAD_RC __NAD;

        [XmlIgnore]
        public NAD_RC NAD
        {
            get
            {
                if (__NAD == null) __NAD = new NAD_RC();
                return __NAD;
            }
            set { __NAD = value; }
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

    [XmlType(TypeName = "NAD_RC", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class NAD_RC : NAD
    {

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

        [XmlElement(Type = typeof(GRP4), ElementName = "GRP4", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public GRP4Collection __GRP4Collection;

        [XmlIgnore]
        public GRP4Collection GRP4Collection
        {
            get
            {
                if (__GRP4Collection == null) __GRP4Collection = new GRP4Collection();
                return __GRP4Collection;
            }
            set { __GRP4Collection = value; }
        }

        public NAD_RC()
        {
        }

        public NAD_RC(NAD nadObject)
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

    #endregion

    #region GRP4

    [XmlType(TypeName = "GRP4", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP4
    {

        [XmlElement(Type = typeof(CTA), ElementName = "CTA", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
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

        public GRP4()
        {
        }

        public GRP4(CTA ctaObject)
        {
            this.CTA = ctaObject;
        }
    }

    [Serializable]
    public class GRP4Collection : ArrayList
    {
        public GRP4 Add(GRP4 obj)
        {
            base.Add(obj);
            return obj;
        }

        public GRP4 Add()
        {
            return Add(new GRP4());
        }

        public void Insert(int index, GRP4 obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(GRP4 obj)
        {
            base.Remove(obj);
        }

        new public GRP4 this[int index]
        {
            get { return (GRP4)base[index]; }
            set { base[index] = value; }
        }
    }
    #endregion

    #region GRP5
    [XmlType(TypeName = "GRP5", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP5
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

        public GRP5()
        {
        }
    }
    #endregion

    #region GRP10
    [XmlType(TypeName = "GRP10", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP10
    {

        [XmlElement(Type = typeof(CPS_PL), ElementName = "CPS", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public CPS_PL __CPS;

        [XmlIgnore]
        public CPS_PL CPS
        {
            get
            {
                if (__CPS == null) __CPS = new CPS_PL();
                return __CPS;
            }
            set { __CPS = value; }
        }

        public GRP10() { }

        public GRP10(CPS cpsObject)
        {
            this.CPS.hierachticalParentID = cpsObject.hierachticalParentID;
            this.CPS.hierarchicalIdNumber = cpsObject.hierarchicalIdNumber;
            this.CPS.packagingLevelCoded = cpsObject.packagingLevelCoded;
        }
    }

    [Serializable]
    public class GRP10Collection : ArrayList
    {
        public GRP10 Add(GRP10 obj)
        {
            base.Add(obj);
            return obj;
        }

        public GRP10 Add()
        {
            return Add(new GRP10());
        }

        public void Insert(int index, GRP10 obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(GRP10 obj)
        {
            base.Remove(obj);
        }

        new public GRP10 this[int index]
        {
            get { return (GRP10)base[index]; }
            set { base[index] = value; }
        }
    }

    [XmlType(TypeName = "CPS_PL", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class CPS_PL : CPS
    {

        [XmlElement(Type = typeof(GRP11), ElementName = "GRP11", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public GRP11Collection __GRP11Collection;

        [XmlIgnore]
        public GRP11Collection GRP11Collection
        {
            get
            {
                if (__GRP11Collection == null) __GRP11Collection = new GRP11Collection();
                return __GRP11Collection;
            }
            set { __GRP11Collection = value; }
        }

        [XmlElement(Type = typeof(GRP15), ElementName = "GRP15", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public GRP15Collection __GRP15Collection;

        [XmlIgnore]
        public GRP15Collection GRP15Collection
        {
            get
            {
                if (__GRP15Collection == null) __GRP15Collection = new GRP15Collection();
                return __GRP15Collection;
            }
            set { __GRP15Collection = value; }
        }

        public CPS_PL()
        {
        }
    }

    #endregion

    #region GRP11
    [XmlType(TypeName = "GRP11", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP11
    {

        [XmlElement(Type = typeof(PAC_MQHP), ElementName = "PAC", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public PAC_MQHP __PAC;

        [XmlIgnore]
        public PAC_MQHP PAC
        {
            get
            {
                if (__PAC == null) __PAC = new PAC_MQHP();
                return __PAC;
            }
            set { __PAC = value; }
        }

        public GRP11()
        {
        }

        public GRP11(PAC pacObject)
        {
            this.PAC.codeListQualifier = pacObject.codeListQualifier;
            this.PAC.codeListResponsibleAgency = pacObject.codeListResponsibleAgency;
            this.PAC.itemDescriptionType = pacObject.itemDescriptionType;
            this.PAC.itemNumberType = pacObject.itemNumberType;
            this.PAC.numberOfPackages = pacObject.numberOfPackages;
            this.PAC.packageTypeDescription = pacObject.packageTypeDescription;
            this.PAC.packageTypeID = pacObject.packageTypeID;
            this.PAC.packagingLevel = pacObject.packagingLevel;
            this.PAC.packagingRelatedInformation = pacObject.packagingRelatedInformation;
            this.PAC.packagingTermsAndConditions = pacObject.packagingTermsAndConditions;
            this.PAC.rtnPackFreightPmtResponsibility = pacObject.rtnPackFreightPmtResponsibility;
            this.PAC.rtnPackLoadContents = pacObject.rtnPackLoadContents;
            this.PAC.typeOfPackages = pacObject.typeOfPackages;
        }
    }

    [Serializable]
    public class GRP11Collection : ArrayList
    {
        public GRP11 Add(GRP11 obj)
        {
            base.Add(obj);
            return obj;
        }

        public GRP11 Add()
        {
            return Add(new GRP11());
        }

        public void Insert(int index, GRP11 obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(GRP11 obj)
        {
            base.Remove(obj);
        }

        new public GRP11 this[int index]
        {
            get { return (GRP11)base[index]; }
            set { base[index] = value; }
        }
    }



    [XmlType(TypeName = "PAC_MQHP", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class PAC_MQHP : PAC
    {
        [XmlElement(Type = typeof(MEA), ElementName = "MEA", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public MEACollection __MEACollection;

        [XmlIgnore]
        public MEACollection MEACollection
        {
            get
            {
                if (__MEACollection == null) __MEACollection = new MEACollection();
                return __MEACollection;
            }
            set { __MEACollection = value; }
        }

        [XmlElement(Type = typeof(GRP12), ElementName = "GRP12", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public GRP12Collection __GRP12Collection;

        [XmlIgnore]
        public GRP12Collection GRP12Collection
        {
            get
            {
                if (__GRP12Collection == null) __GRP12Collection = new GRP12Collection();
                return __GRP12Collection;
            }
            set { __GRP12Collection = value; }
        }

        [XmlElement(Type = typeof(GRP13), ElementName = "GRP13", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public GRP13Collection __GRP13Collection;

        [XmlIgnore]
        public GRP13Collection GRP13Collection
        {
            get
            {
                if (__GRP13Collection == null) __GRP13Collection = new GRP13Collection();
                return __GRP13Collection;
            }
            set { __GRP13Collection = value; }
        }

        public PAC_MQHP()
        {
        }
    }

    #endregion

    #region GRP12
    [XmlType(TypeName = "GRP12", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP12
    {

        [XmlElement(Type = typeof(HAN), ElementName = "HAN", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public HAN __HAN;

        [XmlIgnore]
        public HAN HAN
        {
            get
            {
                if (__HAN == null) __HAN = new HAN();
                return __HAN;
            }
            set { __HAN = value; }
        }

        public GRP12()
        {
        }
        public GRP12(HAN hanObject)
        {
            this.HAN = hanObject;
        }
    }

    [Serializable]
    public class GRP12Collection : ArrayList
    {
        public GRP12 Add(GRP12 obj)
        {
            base.Add(obj);
            return obj;
        }

        public GRP12 Add()
        {
            return Add(new GRP12());
        }

        public void Insert(int index, GRP12 obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(GRP12 obj)
        {
            base.Remove(obj);
        }

        new public GRP12 this[int index]
        {
            get { return (GRP12)base[index]; }
            set { base[index] = value; }
        }
    }
    #endregion

    #region GRP13 & GRP14

    [XmlType(TypeName = "GRP13", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP13
    {

        [XmlElement(Type = typeof(PCI_RDGG), ElementName = "PCI", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public PCI_RDGG __PCI;

        [XmlIgnore]
        public PCI_RDGG PCI
        {
            get
            {
                if (__PCI == null) __PCI = new PCI_RDGG();
                return __PCI;
            }
            set { __PCI = value; }
        }

        public GRP13()
        {
        }

        public GRP13(PCI pciObject)
        {
            this.PCI.containerPackageStatus = pciObject.containerPackageStatus;
            this.PCI.markingInstructionsCoded = pciObject.markingInstructionsCoded;
            this.PCI.shippingMarks = pciObject.shippingMarks;
        }
    }

    [Serializable]
    public class GRP13Collection : ArrayList
    {
        public GRP13 Add(GRP13 obj)
        {
            base.Add(obj);
            return obj;
        }

        public GRP13 Add()
        {
            return Add(new GRP13());
        }

        public void Insert(int index, GRP13 obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(GRP13 obj)
        {
            base.Remove(obj);
        }

        new public GRP13 this[int index]
        {
            get { return (GRP13)base[index]; }
            set { base[index] = value; }
        }
    }


    [XmlType(TypeName = "PCI_RDGG", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class PCI_RDGG : PCI
    {

        [XmlElement(Type = typeof(GRP14), ElementName = "GRP14", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public GRP14Collection __GRP14Collection;

        [XmlIgnore]
        public GRP14Collection GRP14Collection
        {
            get
            {
                if (__GRP14Collection == null) __GRP14Collection = new GRP14Collection();
                return __GRP14Collection;
            }
            set { __GRP14Collection = value; }
        }

        public PCI_RDGG()
        {
        }
    }

    [XmlType(TypeName = "GRP14", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP14
    {

        [XmlElement(Type = typeof(GIN), ElementName = "GIN", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public GIN __GIN;

        [XmlIgnore]
        public GIN GIN
        {
            get
            {
                if (__GIN == null) __GIN = new GIN();
                return __GIN;
            }
            set { __GIN = value; }
        }

        public GRP14()
        {
        }

        public GRP14(GIN ginObject)
        {
            this.GIN = ginObject;
        }
    }

    [Serializable]
    public class GRP14Collection : ArrayList
    {
        public GRP14 Add(GRP14 obj)
        {
            base.Add(obj);
            return obj;
        }

        public GRP14 Add()
        {
            return Add(new GRP14());
        }

        public void Insert(int index, GRP14 obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(GRP14 obj)
        {
            base.Remove(obj);
        }

        new public GRP14 this[int index]
        {
            get { return (GRP14)base[index]; }
            set { base[index] = value; }
        }
    }
    #endregion

    #region GRP15
    [XmlType(TypeName = "GRP15", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP15
    {

        [XmlElement(Type = typeof(LIN_PIQ), ElementName = "LIN", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public LIN_PIQ __LIN;

        [XmlIgnore]
        public LIN_PIQ LIN
        {
            get
            {
                if (__LIN == null) __LIN = new LIN_PIQ();
                return __LIN;
            }
            set { __LIN = value; }
        }

        public GRP15()
        {

        }
        public GRP15(LIN linObject)
        {
            this.LIN.actionRequest = linObject.actionRequest;
            this.LIN.codeListQualifier = linObject.codeListQualifier;
            this.LIN.codeListResponsibleAgency = linObject.codeListResponsibleAgency;
            this.LIN.configurationCoded = linObject.configurationCoded;
            this.LIN.configurationLevel = linObject.configurationLevel;
            this.LIN.itemNumber = linObject.itemNumber;
            this.LIN.itemNumberID = linObject.itemNumberID;
            this.LIN.itemNumberType = linObject.itemNumberType;
            this.LIN.lineItemNumber = linObject.lineItemNumber;
            this.LIN.subLineIndicator = linObject.subLineIndicator;
        }
    }

    [Serializable]
    public class GRP15Collection : ArrayList
    {
        public GRP15 Add(GRP15 obj)
        {
            base.Add(obj);
            return obj;
        }

        public GRP15 Add()
        {
            return Add(new GRP15());
        }

        public void Insert(int index, GRP15 obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(GRP15 obj)
        {
            base.Remove(obj);
        }

        new public GRP15 this[int index]
        {
            get { return (GRP15)base[index]; }
            set { base[index] = value; }
        }
    }



    [XmlType(TypeName = "LIN_PIQ", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class LIN_PIQ : LIN
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

        [XmlElement(Type = typeof(GRP16), ElementName = "GRP16", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public GRP16Collection __GRP16Collection;

        [XmlIgnore]
        public GRP16Collection GRP16Collection
        {
            get
            {
                if (__GRP16Collection == null) __GRP16Collection = new GRP16Collection();
                return __GRP16Collection;
            }
            set { __GRP16Collection = value; }
        }

        [XmlElement(Type = typeof(GRP20), ElementName = "GRP20", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public GRP20Collection __GRP20Collection;

        [XmlIgnore]
        public GRP20Collection GRP20Collection
        {
            get
            {
                if (__GRP20Collection == null) __GRP20Collection = new GRP20Collection();
                return __GRP20Collection;
            }
            set { __GRP20Collection = value; }
        }

        public LIN_PIQ()
        {
        }
    }
    #endregion

    #region GRP16
    [XmlType(TypeName = "GRP16", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP16
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

        public GRP16()
        {
        }
        public GRP16(RFF rffObject)
        {
            this.RFF = rffObject;
        }
    }

    [Serializable]
    public class GRP16Collection : ArrayList
    {
        public GRP16 Add(GRP16 obj)
        {
            base.Add(obj);
            return obj;
        }

        public GRP16 Add()
        {
            return Add(new GRP16());
        }

        public void Insert(int index, GRP16 obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(GRP16 obj)
        {
            base.Remove(obj);
        }

        new public GRP16 this[int index]
        {
            get { return (GRP16)base[index]; }
            set { base[index] = value; }
        }
    }

    #endregion

    #region GRP20
    [XmlType(TypeName = "GRP20", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP20
    {

        [XmlElement(Type = typeof(PCI_DTM), ElementName = "PCI", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public PCI_DTM __PCI;

        [XmlIgnore]
        public PCI_DTM PCI
        {
            get
            {
                if (__PCI == null) __PCI = new PCI_DTM();
                return __PCI;
            }
            set { __PCI = value; }
        }

        public GRP20()
        {
        }

        public GRP20(PCI pciObject)
        {
            this.PCI.containerPackageStatus = pciObject.containerPackageStatus;
            this.PCI.markingInstructionsCoded = pciObject.markingInstructionsCoded;
            this.PCI.shippingMarks = pciObject.shippingMarks;
        }
    }

    [Serializable]
    public class GRP20Collection : ArrayList
    {
        public GRP20 Add(GRP20 obj)
        {
            base.Add(obj);
            return obj;
        }

        public GRP20 Add()
        {
            return Add(new GRP20());
        }

        public void Insert(int index, GRP20 obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(GRP20 obj)
        {
            base.Remove(obj);
        }

        new public GRP20 this[int index]
        {
            get { return (GRP20)base[index]; }
            set { base[index] = value; }
        }
    }


    [XmlType(TypeName = "PCI_DTM", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class PCI_DTM : PCI
    {

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

        public PCI_DTM()
        {

        }
    }
    #endregion

}