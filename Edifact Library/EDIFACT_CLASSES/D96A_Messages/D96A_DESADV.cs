//--------------------------------------------------------------
// <CreatedBy>
//     Anthony R Yates
//	   Jul. 07, 2005
//     Runtime Version: 1.1.4322.2032
//	   
//     It contains D96A DESADV Classes
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

namespace EDIFACT.D96A.DESADV
{
    #region Schema Declarations
    public struct Declarations
    {
        public const string SchemaVersion = "http://www.default.com/D96A/desadv";
        public const string SchemaVersionPCIDTM = "http://www.default.com/D96A/desadv/PCIDTM";
    }
    #endregion

    #region D96A_DESADV Class
    [XmlRoot(ElementName = "D96A_DESADV", Namespace = Declarations.SchemaVersion, IsNullable = false), Serializable]
    public class D96A_DESADV : IMessage
    {
        #region Public Properties
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
        #endregion

        #region Constructor
        public D96A_DESADV()
        {
        }
        #endregion

        #region IMessage Members

        public void PopulateMessage(ref Segment[] segments)
        {
            SegmentProcessor sp = new SegmentProcessor(new EDIFACT.AddSegmentDelegate(this.DESADV.Add));
            sp.ProcessSegments(segments);
        }

        #endregion
    }
    #endregion

    #region DESADV Class
    [XmlType(TypeName = "DESADV", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class DESADV
    {

        #region Public Class Members

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
        public GRP10 __GRP10;

        [XmlIgnore]
        public GRP10 GRP10
        {
            get
            {
                if (__GRP10 == null) __GRP10 = new GRP10();
                return __GRP10;
            }
            set { __GRP10 = value; }
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

        #region Private Class Members

        SegmentType lastSegment;

        #endregion

        #region Constructor
        public DESADV()
        {
        }
        #endregion

        #region Add Method
        public void Add(SegmentType type, object obj)
        {
            int i = 0;
            int j = 0;
            int k = 0;

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

                        if (qualifier == 69 || qualifier == 137)
                        {
                            this.DTMCollection.Add((DTM)obj);
                        }
                        else if (qualifier == 11 || qualifier == 94
                              || qualifier == 360 || qualifier == 361)
                        {
                            //Access and add to GRP10\GRP15\GRP20.DTM
                            if ((i = this.GRP10.Count) > 0)
                                if ((j = this.GRP10[i - 1].GRP15.Count) > 0)
                                    if ((k = this.GRP10[i - 1].GRP15[j - 1].GRP20.Count) > 0)
                                        this.GRP10[i - 1].GRP15[j - 1].GRP20[k - 1].Add((DTM)obj);
                        }
                        break;
                    }
                case SegmentType.RFF:
                    {
                        string qualifier = ((RFF)obj).referenceQualifier;

                        if (qualifier == "AAK" || qualifier == "ON" || qualifier == "VN")
                        {
                            this.GRP1.Add((RFF)obj);

                            if (qualifier != "AAK")
                            {
                                if ((i = this.GRP10.Count) > 0)
                                    if ((j = this.GRP10[i - 1].GRP15.Count) > 0)
                                        this.GRP10[i - 1].GRP15[j - 1].GRP16.Add((RFF)obj);
                            }
                        }
                        else if (((RFF)obj).referenceQualifier == "GN" ||
                            ((RFF)obj).referenceQualifier == "VA")
                        {
                            if ((i = this.GRP2.Count) > 0)
                                this.GRP2[i - 1].GRP3.RFF = (RFF)obj;
                        }
                        break;
                    }
                case SegmentType.NAD:
                    {
                        this.GRP2.Add(new DESADVNAD((NAD)obj));
                        break;
                    }
                case SegmentType.CTA:
                    {
                        if ((i = this.GRP2.Count) > 0)
                            this.GRP2[i - 1].GRP4.Add((CTA)obj);
                        break;
                    }
                case SegmentType.TOD:
                    {
                        this.GRP5.TOD = (TOD)obj;
                        break;
                    }
                case SegmentType.CPS:
                    {
                        this.GRP10.Add(new DESADVCPS((CPS)obj));
                        break;
                    }
                case SegmentType.PAC:
                    {
                        lastSegment = type;

                        if ((i = this.GRP10.Count) > 0)
                            this.GRP10[i - 1].GRP11.Add(new DESADVPAC((PAC)obj));
                        break;
                    }
                case SegmentType.MEA:
                    {
                        if ((i = this.GRP10.Count) > 0)
                            if ((j = this.GRP10[i - 1].GRP11.Count) > 0)
                                this.GRP10[i - 1].GRP11[j - 1].MEACollection.Add((MEA)obj);
                        break;
                    }
                case SegmentType.HAN:
                    {
                        if ((i = this.GRP10.Count) > 0)
                            if ((j = this.GRP10[i - 1].GRP11.Count) > 0)
                                this.GRP10[i - 1].GRP11[j - 1].GRP12.Add((HAN)obj);
                        break;
                    }
                case SegmentType.PCI:
                    {
                        if ((i = this.GRP10.Count) > 0)
                        {
                            if (lastSegment == SegmentType.PAC)
                            {
                                if ((j = this.GRP10[i - 1].GRP11.Count) > 0)
                                {
                                    this.GRP10[i - 1].GRP11[j - 1].GRP13.Add(new DESADVPCI((PCI)obj));
                                }
                            }
                            else
                                if ((j = this.GRP10[i - 1].GRP15.Count) > 0)
                                this.GRP10[i - 1].GRP15[j - 1].GRP20.Add(new DESADVPCIDTM((PCI)obj));
                        }
                        break;
                    }
                case SegmentType.GIN:
                    {
                        if ((i = this.GRP10.Count) > 0)
                            if ((j = this.GRP10[i - 1].GRP11.Count) > 0)
                                if ((k = this.GRP10[i - 1].GRP11[j - 1].GRP13.Count) > 0)
                                    this.GRP10[i - 1].GRP11[j - 1].GRP13[k - 1].GRP14.Add((GIN)obj);
                        break;
                    }
                case SegmentType.LIN:
                    {
                        lastSegment = type;

                        if ((i = this.GRP10.Count) > 0)
                            this.GRP10[i - 1].GRP15.Add(new DESADVLIN((LIN)obj));
                        break;
                    }
                case SegmentType.PIA:
                    {
                        if ((i = this.GRP10.Count) > 0)
                            if ((j = this.GRP10[i - 1].GRP15.Count) > 0)
                                this.GRP10[i - 1].GRP15[j - 1].PIACollection.Add((PIA)obj);
                        break;
                    }
                case SegmentType.IMD:
                    {
                        if ((i = this.GRP10.Count) > 0)
                            if ((j = this.GRP10[i - 1].GRP15.Count) > 0)
                                this.GRP10[i - 1].GRP15[j - 1].IMDCollection.Add((IMD)obj);
                        break;
                    }
                case SegmentType.QTY:
                    {
                        if ((i = this.GRP10.Count) > 0)
                            if ((j = this.GRP10[i - 1].GRP15.Count) > 0)
                                this.GRP10[i - 1].GRP15[j - 1].QTYCollection.Add((QTY)obj);
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

    #region GRP1
    [XmlType(TypeName = "GRP1", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP1
    {
        [System.Runtime.InteropServices.DispIdAttribute(-4)]
        public IEnumerator GetEnumerator()
        {
            return RFFCollection.GetEnumerator();
        }

        public RFF Add(RFF obj)
        {
            return RFFCollection.Add(obj);
        }

        [XmlIgnore]
        public RFF this[int index]
        {
            get { return (RFF)RFFCollection[index]; }
        }

        [XmlIgnore]
        public int Count
        {
            get { return RFFCollection.Count; }
        }

        public void Clear()
        {
            RFFCollection.Clear();
        }

        public RFF Remove(int index)
        {
            RFF obj = RFFCollection[index];
            RFFCollection.Remove(obj);
            return obj;
        }

        public void Remove(object obj)
        {
            RFFCollection.Remove(obj);
        }

        [XmlElement(Type = typeof(RFF), ElementName = "RFF", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public RFFCollection __RFFCollection;

        [XmlIgnore]
        public RFFCollection RFFCollection
        {
            get
            {
                if (__RFFCollection == null) __RFFCollection = new RFFCollection();
                return __RFFCollection;
            }
            set { __RFFCollection = value; }
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
        [System.Runtime.InteropServices.DispIdAttribute(-4)]
        public IEnumerator GetEnumerator()
        {
            return DESADVNADCollection.GetEnumerator();
        }

        public DESADVNAD Add(DESADVNAD obj)
        {
            return DESADVNADCollection.Add(obj);
        }

        [XmlIgnore]
        public DESADVNAD this[int index]
        {
            get { return (DESADVNAD)DESADVNADCollection[index]; }
        }

        [XmlIgnore]
        public int Count
        {
            get { return DESADVNADCollection.Count; }
        }

        public void Clear()
        {
            DESADVNADCollection.Clear();
        }

        public DESADVNAD Remove(int index)
        {
            DESADVNAD obj = DESADVNADCollection[index];
            DESADVNADCollection.Remove(obj);
            return obj;
        }

        public void Remove(object obj)
        {
            DESADVNADCollection.Remove(obj);
        }

        [XmlElement(Type = typeof(DESADVNAD), ElementName = "NAD", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public DESADVNADCollection __NADCollection;

        [XmlIgnore]
        public DESADVNADCollection DESADVNADCollection
        {
            get
            {
                if (__NADCollection == null) __NADCollection = new DESADVNADCollection();
                return __NADCollection;
            }
            set { __NADCollection = value; }
        }

        public GRP2()
        {
        }
    }
    #endregion

    #region DESADVNAD
    [XmlType(TypeName = "NAD", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class DESADVNAD : EDIFACT.BASETYPES.NAD
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

        public DESADVNAD()
        {
        }

        public DESADVNAD(NAD nadObject)
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
    public class DESADVNADCollection : ArrayList
    {
        public DESADVNAD Add(DESADVNAD obj)
        {
            base.Add(obj);
            return obj;
        }

        public DESADVNAD Add()
        {
            return Add(new DESADVNAD());
        }

        public void Insert(int index, DESADVNAD obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(DESADVNAD obj)
        {
            base.Remove(obj);
        }

        new public DESADVNAD this[int index]
        {
            get { return (DESADVNAD)base[index]; }
            set { base[index] = value; }
        }
    }

    #endregion

    #region GRP 3, 4 ,5
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


    [XmlType(TypeName = "GRP4", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP4
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

        public GRP4()
        {
        }
    }

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

    #region GRP 10
    [XmlType(TypeName = "GRP10", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP10
    {
        [System.Runtime.InteropServices.DispIdAttribute(-4)]
        public IEnumerator GetEnumerator()
        {
            return DESADVCPSCollection.GetEnumerator();
        }

        public DESADVCPS Add(DESADVCPS obj)
        {
            return DESADVCPSCollection.Add(obj);
        }

        [XmlIgnore]
        public DESADVCPS this[int index]
        {
            get { return (DESADVCPS)DESADVCPSCollection[index]; }
        }

        [XmlIgnore]
        public int Count
        {
            get { return DESADVCPSCollection.Count; }
        }

        public void Clear()
        {
            DESADVCPSCollection.Clear();
        }

        public DESADVCPS Remove(int index)
        {
            DESADVCPS obj = DESADVCPSCollection[index];
            DESADVCPSCollection.Remove(obj);
            return obj;
        }

        public void Remove(object obj)
        {
            DESADVCPSCollection.Remove(obj);
        }

        [XmlElement(Type = typeof(DESADVCPS), ElementName = "CPS", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public DESADVCPSCollection __CPSCollection;

        [XmlIgnore]
        public DESADVCPSCollection DESADVCPSCollection
        {
            get
            {
                if (__CPSCollection == null) __CPSCollection = new DESADVCPSCollection();
                return __CPSCollection;
            }
            set { __CPSCollection = value; }
        }

        public GRP10()
        {
        }
    }
    #endregion

    #region DESADVCPS
    [XmlType(TypeName = "CPS", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class DESADVCPS : EDIFACT.BASETYPES.CPS
    {

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

        [XmlElement(Type = typeof(GRP15), ElementName = "GRP15", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public GRP15 __GRP15;

        [XmlIgnore]
        public GRP15 GRP15
        {
            get
            {
                if (__GRP15 == null) __GRP15 = new GRP15();
                return __GRP15;
            }
            set { __GRP15 = value; }
        }

        public DESADVCPS()
        {
        }

        public DESADVCPS(CPS cpsObject)
        {
            this.hierachticalParentID = ((CPS)cpsObject).hierachticalParentID;
            this.hierarchicalIdNumber = ((CPS)cpsObject).hierarchicalIdNumber;
            this.packagingLevelCoded = ((CPS)cpsObject).packagingLevelCoded;
        }
    }

    [Serializable]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public class DESADVCPSCollection : ArrayList
    {
        public DESADVCPS Add(DESADVCPS obj)
        {
            base.Add(obj);
            return obj;
        }

        public DESADVCPS Add()
        {
            return Add(new DESADVCPS());
        }

        public void Insert(int index, DESADVCPS obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(DESADVCPS obj)
        {
            base.Remove(obj);
        }

        new public DESADVCPS this[int index]
        {
            get { return (DESADVCPS)base[index]; }
            set { base[index] = value; }
        }
    }
    #endregion

    #region GRP 11
    [XmlType(TypeName = "GRP11", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP11
    {
        [System.Runtime.InteropServices.DispIdAttribute(-4)]
        public IEnumerator GetEnumerator()
        {
            return DESADVPACCollection.GetEnumerator();
        }

        public DESADVPAC Add(DESADVPAC obj)
        {
            return DESADVPACCollection.Add(obj);
        }

        [XmlIgnore]
        public DESADVPAC this[int index]
        {
            get { return (DESADVPAC)DESADVPACCollection[index]; }
        }

        [XmlIgnore]
        public int Count
        {
            get { return DESADVPACCollection.Count; }
        }

        public void Clear()
        {
            DESADVPACCollection.Clear();
        }

        public DESADVPAC Remove(int index)
        {
            DESADVPAC obj = DESADVPACCollection[index];
            DESADVPACCollection.Remove(obj);
            return obj;
        }

        public void Remove(object obj)
        {
            DESADVPACCollection.Remove(obj);
        }

        [XmlElement(Type = typeof(DESADVPAC), ElementName = "PAC", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public DESADVPACCollection __PACCollection;

        [XmlIgnore]
        public DESADVPACCollection DESADVPACCollection
        {
            get
            {
                if (__PACCollection == null) __PACCollection = new DESADVPACCollection();
                return __PACCollection;
            }
            set { __PACCollection = value; }
        }

        public GRP11()
        {
        }
    }
    #endregion

    #region DESADVPAC
    [XmlType(TypeName = "PAC", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class DESADVPAC : EDIFACT.BASETYPES.PAC
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

        [XmlElement(Type = typeof(GRP13), ElementName = "GRP13", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public GRP13 __GRP13;

        [XmlIgnore]
        public GRP13 GRP13
        {
            get
            {
                if (__GRP13 == null) __GRP13 = new GRP13();
                return __GRP13;
            }
            set { __GRP13 = value; }
        }

        public DESADVPAC()
        {
        }

        public DESADVPAC(PAC pacObject)
        {
            this.codeListQualifier = ((PAC)pacObject).codeListQualifier;
            this.codeListResponsibleAgency = ((PAC)pacObject).codeListResponsibleAgency;
            this.itemDescriptionType = ((PAC)pacObject).itemDescriptionType;
            this.itemNumberType = ((PAC)pacObject).itemNumberType;
            this.numberOfPackages = ((PAC)pacObject).numberOfPackages;
            this.packageTypeDescription = ((PAC)pacObject).packageTypeDescription;
            this.packageTypeID = ((PAC)pacObject).packageTypeID;
            this.packagingLevel = ((PAC)pacObject).packagingLevel;
            this.packagingRelatedInformation = ((PAC)pacObject).packagingRelatedInformation;
            this.packagingTermsAndConditions = ((PAC)pacObject).packagingTermsAndConditions;
            this.rtnPackFreightPmtResponsibility = ((PAC)pacObject).rtnPackFreightPmtResponsibility;
            this.rtnPackLoadContents = ((PAC)pacObject).rtnPackLoadContents;
            this.typeOfPackages = ((PAC)pacObject).typeOfPackages;
        }
    }

    [Serializable]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public class DESADVPACCollection : ArrayList
    {
        public DESADVPAC Add(DESADVPAC obj)
        {
            base.Add(obj);
            return obj;
        }

        public DESADVPAC Add()
        {
            return Add(new DESADVPAC());
        }

        public void Insert(int index, DESADVPAC obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(DESADVPAC obj)
        {
            base.Remove(obj);
        }

        new public DESADVPAC this[int index]
        {
            get { return (DESADVPAC)base[index]; }
            set { base[index] = value; }
        }
    }
    #endregion

    #region GRP12
    [XmlType(TypeName = "GRP12", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP12
    {
        [System.Runtime.InteropServices.DispIdAttribute(-4)]
        public IEnumerator GetEnumerator()
        {
            return HANCollection.GetEnumerator();
        }

        public HAN Add(HAN obj)
        {
            return HANCollection.Add(obj);
        }

        [XmlIgnore]
        public HAN this[int index]
        {
            get { return (HAN)HANCollection[index]; }
        }

        [XmlIgnore]
        public int Count
        {
            get { return HANCollection.Count; }
        }

        public void Clear()
        {
            HANCollection.Clear();
        }

        public HAN Remove(int index)
        {
            HAN obj = HANCollection[index];
            HANCollection.Remove(obj);
            return obj;
        }

        public void Remove(object obj)
        {
            HANCollection.Remove(obj);
        }

        [XmlElement(Type = typeof(HAN), ElementName = "HAN", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public HANCollection __HANCollection;

        [XmlIgnore]
        public HANCollection HANCollection
        {
            get
            {
                if (__HANCollection == null) __HANCollection = new HANCollection();
                return __HANCollection;
            }
            set { __HANCollection = value; }
        }

        public GRP12()
        {
        }
    }
    #endregion

    #region GRP 13
    [XmlType(TypeName = "GRP13", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP13
    {
        [System.Runtime.InteropServices.DispIdAttribute(-4)]
        public IEnumerator GetEnumerator()
        {
            return DESADVPCICollection.GetEnumerator();
        }

        public DESADVPCI Add(DESADVPCI obj)
        {
            return DESADVPCICollection.Add(obj);
        }

        [XmlIgnore]
        public DESADVPCI this[int index]
        {
            get { return (DESADVPCI)DESADVPCICollection[index]; }
        }

        [XmlIgnore]
        public int Count
        {
            get { return DESADVPCICollection.Count; }
        }

        public void Clear()
        {
            DESADVPCICollection.Clear();
        }

        public DESADVPCI Remove(int index)
        {
            DESADVPCI obj = DESADVPCICollection[index];
            DESADVPCICollection.Remove(obj);
            return obj;
        }

        public void Remove(object obj)
        {
            DESADVPCICollection.Remove(obj);
        }

        [XmlElement(Type = typeof(DESADVPCI), ElementName = "PCI", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public DESADVPCICollection __PCICollection;

        [XmlIgnore]
        public DESADVPCICollection DESADVPCICollection
        {
            get
            {
                if (__PCICollection == null) __PCICollection = new DESADVPCICollection();
                return __PCICollection;
            }
            set { __PCICollection = value; }
        }

        public GRP13()
        {
        }
    }
    #endregion

    #region DESADVPCI
    [XmlType(TypeName = "PCI", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class DESADVPCI : EDIFACT.BASETYPES.PCI
    {

        [XmlElement(Type = typeof(GRP14), ElementName = "GRP14", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public GRP14 __GRP14;

        [XmlIgnore]
        public GRP14 GRP14
        {
            get
            {
                if (__GRP14 == null) __GRP14 = new GRP14();
                return __GRP14;
            }
            set { __GRP14 = value; }
        }

        public DESADVPCI()
        {
        }

        public DESADVPCI(PCI pciObject)
        {
            this.containerPackageStatus = ((PCI)pciObject).containerPackageStatus;
            this.markingInstructionsCoded = ((PCI)pciObject).markingInstructionsCoded;
            this.shippingMarks = ((PCI)pciObject).shippingMarks;
        }
    }

    [Serializable]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public class DESADVPCICollection : ArrayList
    {
        public DESADVPCI Add(DESADVPCI obj)
        {
            base.Add(obj);
            return obj;
        }

        public DESADVPCI Add()
        {
            return Add(new DESADVPCI());
        }

        public void Insert(int index, DESADVPCI obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(DESADVPCI obj)
        {
            base.Remove(obj);
        }

        new public DESADVPCI this[int index]
        {
            get { return (DESADVPCI)base[index]; }
            set { base[index] = value; }
        }
    }
    #endregion

    #region GRP 14
    [XmlType(TypeName = "GRP14", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP14
    {
        [System.Runtime.InteropServices.DispIdAttribute(-4)]
        public IEnumerator GetEnumerator()
        {
            return GINCollection.GetEnumerator();
        }

        public GIN Add(GIN obj)
        {
            return GINCollection.Add(obj);
        }

        [XmlIgnore]
        public GIN this[int index]
        {
            get { return (GIN)GINCollection[index]; }
        }

        [XmlIgnore]
        public int Count
        {
            get { return GINCollection.Count; }
        }

        public void Clear()
        {
            GINCollection.Clear();
        }

        public GIN Remove(int index)
        {
            GIN obj = GINCollection[index];
            GINCollection.Remove(obj);
            return obj;
        }

        public void Remove(object obj)
        {
            GINCollection.Remove(obj);
        }

        [XmlElement(Type = typeof(GIN), ElementName = "GIN", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public GINCollection __GINCollection;

        [XmlIgnore]
        public GINCollection GINCollection
        {
            get
            {
                if (__GINCollection == null) __GINCollection = new GINCollection();
                return __GINCollection;
            }
            set { __GINCollection = value; }
        }

        public GRP14()
        {
        }
    }
    #endregion

    #region GRP 15
    [XmlType(TypeName = "GRP15", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP15
    {
        [System.Runtime.InteropServices.DispIdAttribute(-4)]
        public IEnumerator GetEnumerator()
        {
            return DESADVLINCollection.GetEnumerator();
        }

        public DESADVLIN Add(DESADVLIN obj)
        {
            return DESADVLINCollection.Add(obj);
        }

        [XmlIgnore]
        public DESADVLIN this[int index]
        {
            get { return (DESADVLIN)DESADVLINCollection[index]; }
        }

        [XmlIgnore]
        public int Count
        {
            get { return DESADVLINCollection.Count; }
        }

        public void Clear()
        {
            DESADVLINCollection.Clear();
        }

        public DESADVLIN Remove(int index)
        {
            DESADVLIN obj = DESADVLINCollection[index];
            DESADVLINCollection.Remove(obj);
            return obj;
        }

        public void Remove(object obj)
        {
            DESADVLINCollection.Remove(obj);
        }

        [XmlElement(Type = typeof(DESADVLIN), ElementName = "LIN", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public DESADVLINCollection __LINCollection;

        [XmlIgnore]
        public DESADVLINCollection DESADVLINCollection
        {
            get
            {
                if (__LINCollection == null) __LINCollection = new DESADVLINCollection();
                return __LINCollection;
            }
            set { __LINCollection = value; }
        }

        public GRP15()
        {
        }
    }
    #endregion

    #region DESADVLIN
    [XmlType(TypeName = "LIN", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class DESADVLIN : EDIFACT.BASETYPES.LIN
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
        public GRP16 __GRP16;

        [XmlIgnore]
        public GRP16 GRP16
        {
            get
            {
                if (__GRP16 == null) __GRP16 = new GRP16();
                return __GRP16;
            }
            set { __GRP16 = value; }
        }

        [XmlElement(Type = typeof(GRP20), ElementName = "GRP20", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public GRP20 __GRP20;

        [XmlIgnore]
        public GRP20 GRP20
        {
            get
            {
                if (__GRP20 == null) __GRP20 = new GRP20();
                return __GRP20;
            }
            set { __GRP20 = value; }
        }

        public DESADVLIN()
        {
        }

        public DESADVLIN(LIN linObject)
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
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public class DESADVLINCollection : ArrayList
    {
        public DESADVLIN Add(DESADVLIN obj)
        {
            base.Add(obj);
            return obj;
        }

        public DESADVLIN Add()
        {
            return Add(new DESADVLIN());
        }

        public void Insert(int index, DESADVLIN obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(DESADVLIN obj)
        {
            base.Remove(obj);
        }

        new public DESADVLIN this[int index]
        {
            get { return (DESADVLIN)base[index]; }
            set { base[index] = value; }
        }
    }
    #endregion

    #region GRP 16
    [XmlType(TypeName = "GRP16", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP16
    {
        [System.Runtime.InteropServices.DispIdAttribute(-4)]
        public IEnumerator GetEnumerator()
        {
            return RFFCollection.GetEnumerator();
        }

        public RFF Add(RFF obj)
        {
            return RFFCollection.Add(obj);
        }

        [XmlIgnore]
        public RFF this[int index]
        {
            get { return (RFF)RFFCollection[index]; }
        }

        [XmlIgnore]
        public int Count
        {
            get { return RFFCollection.Count; }
        }

        public void Clear()
        {
            RFFCollection.Clear();
        }

        public RFF Remove(int index)
        {
            RFF obj = RFFCollection[index];
            RFFCollection.Remove(obj);
            return obj;
        }

        public void Remove(object obj)
        {
            RFFCollection.Remove(obj);
        }

        [XmlElement(Type = typeof(RFF), ElementName = "RFF", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public RFFCollection __RFFCollection;

        [XmlIgnore]
        public RFFCollection RFFCollection
        {
            get
            {
                if (__RFFCollection == null) __RFFCollection = new RFFCollection();
                return __RFFCollection;
            }
            set { __RFFCollection = value; }
        }

        public GRP16()
        {
        }
    }
    #endregion

    #region GRP 20
    [XmlType(TypeName = "GRP20", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP20
    {
        [System.Runtime.InteropServices.DispIdAttribute(-4)]
        public IEnumerator GetEnumerator()
        {
            return DESADVPCIDTMCollection.GetEnumerator();
        }

        public DESADVPCIDTM Add(DESADVPCIDTM obj)
        {
            return DESADVPCIDTMCollection.Add(obj);
        }

        [XmlIgnore]
        public DESADVPCIDTM this[int index]
        {
            get { return (DESADVPCIDTM)DESADVPCIDTMCollection[index]; }
        }

        [XmlIgnore]
        public int Count
        {
            get { return DESADVPCIDTMCollection.Count; }
        }

        public void Clear()
        {
            DESADVPCIDTMCollection.Clear();
        }

        public DESADVPCIDTM Remove(int index)
        {
            DESADVPCIDTM obj = DESADVPCIDTMCollection[index];
            DESADVPCIDTMCollection.Remove(obj);
            return obj;
        }

        public void Remove(object obj)
        {
            DESADVPCIDTMCollection.Remove(obj);
        }

        [XmlElement(Type = typeof(DESADVPCIDTM), ElementName = "PCI", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public DESADVPCIDTMCollection __PCICollection;

        [XmlIgnore]
        public DESADVPCIDTMCollection DESADVPCIDTMCollection
        {
            get
            {
                if (__PCICollection == null) __PCICollection = new DESADVPCIDTMCollection();
                return __PCICollection;
            }
            set { __PCICollection = value; }
        }

        public GRP20()
        {
        }
    }
    #endregion

    #region DESADVPCIDTM
    [XmlType(TypeName = "PCI", Namespace = Declarations.SchemaVersionPCIDTM), XmlRoot, Serializable]
    public class DESADVPCIDTM : EDIFACT.BASETYPES.PCI
    {
        [System.Runtime.InteropServices.DispIdAttribute(-4)]
        public IEnumerator GetEnumerator()
        {
            return DTMCollection.GetEnumerator();
        }

        public DTM Add(DTM obj)
        {
            return DTMCollection.Add(obj);
        }

        [XmlIgnore]
        public DTM this[int index]
        {
            get { return (DTM)DTMCollection[index]; }
        }

        [XmlIgnore]
        public int Count
        {
            get { return DTMCollection.Count; }
        }

        public void Clear()
        {
            DTMCollection.Clear();
        }

        public DTM Remove(int index)
        {
            DTM obj = DTMCollection[index];
            DTMCollection.Remove(obj);
            return obj;
        }

        public void Remove(object obj)
        {
            DTMCollection.Remove(obj);
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

        public DESADVPCIDTM()
        {
        }

        public DESADVPCIDTM(PCI pciObject)
        {
            this.containerPackageStatus = ((PCI)pciObject).containerPackageStatus;
            this.markingInstructionsCoded = ((PCI)pciObject).markingInstructionsCoded;
            this.shippingMarks = ((PCI)pciObject).shippingMarks;
        }
    }

    [Serializable]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public class DESADVPCIDTMCollection : ArrayList
    {
        public DESADVPCIDTM Add(DESADVPCIDTM obj)
        {
            base.Add(obj);
            return obj;
        }

        public DESADVPCIDTM Add()
        {
            return Add(new DESADVPCIDTM());
        }

        public void Insert(int index, DESADVPCIDTM obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(DESADVPCIDTM obj)
        {
            base.Remove(obj);
        }

        new public DESADVPCIDTM this[int index]
        {
            get { return (DESADVPCIDTM)base[index]; }
            set { base[index] = value; }
        }
    }
    #endregion

}
