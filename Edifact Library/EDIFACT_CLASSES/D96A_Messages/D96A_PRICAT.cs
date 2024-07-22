//--------------------------------------------------------------
// <CreatedBy>
//     Anthony R Yates
//	   Jul. 07, 2005
//     Runtime Version: 1.1.4322.2032
//	   
//     It contains D96A PRICAT Classes
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

namespace EDIFACT.D96A.PRICAT
{

    public struct Declarations
    {
        public const string SchemaVersion = "http://www.default.com/D96A/pricat";
    }

    #region D96A_PRICAT Class
    [XmlRoot(ElementName = "D96A_PRICAT", Namespace = Declarations.SchemaVersion, IsNullable = false), Serializable]
    public class D96A_PRICAT : IMessage
    {
        [XmlElement(Type = typeof(PRICAT), ElementName = "PRICAT", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public PRICAT __PRICAT;

        [XmlIgnore]
        public PRICAT PRICAT
        {
            get
            {
                if (__PRICAT == null) __PRICAT = new PRICAT();
                return __PRICAT;
            }
            set { __PRICAT = value; }
        }

        public D96A_PRICAT()
        {
        }

        ~D96A_PRICAT()
        {
            this.PRICAT = null;
        }

        #region Public Members

        public void PopulateMessage(ref Segment[] segments)
        {
            SegmentProcessor sp = new SegmentProcessor(new AddSegmentDelegate(this.PRICAT.Add));
            sp.ProcessSegments(segments);
        }

        #endregion

    }
    #endregion

    #region PRICAT Class
    [XmlType(TypeName = "PRICAT", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class PRICAT
    {

        #region PRICAT Public Members
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

        [XmlElement(Type = typeof(GRP9), ElementName = "GRP9", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public GRP9 __GRP9;

        [XmlIgnore]
        public GRP9 GRP9
        {
            get
            {
                if (__GRP9 == null) __GRP9 = new GRP9();
                return __GRP9;
            }
            set { __GRP9 = value; }
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

        #region Private Members

        //private SegmentGroup lastGroup;
        private SegmentType lastAccessed;

        #endregion

        #region Public Methods

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
                        string qualifier = ((DTM)obj).dateTimePeriodQualifier;


                        if (qualifier == "137" || qualifier == "194" || qualifier == "206")
                        {
                            this.DTMCollection.Add((DTM)obj);

                            if (SegmentType.LIN == lastAccessed &&
                                (qualifier == "194" || qualifier == "206"))
                            {
                                if ((i = this.GRP16.Count) > 0)
                                {
                                    if ((j = this.GRP16[i - 1].GRP33.Count) > 0)
                                        this.GRP16[i - 1].GRP33[j - 1].DTMCollection.Add((DTM)obj);
                                }
                            }
                        }
                        else if (qualifier == "171")
                        {
                            if ((i = this.GRP1.Count) > 0)
                                this.GRP1[i - 1].DTM = (DTM)obj;
                        }
                        else
                        {
                            if ((i = this.GRP16.Count) > 0)
                            {
                                if ((j = this.GRP16[i - 1].GRP33.Count) > 0)
                                    this.GRP16[i - 1].GRP33[j - 1].DTMCollection.Add((DTM)obj);
                            }
                        }
                        break;
                    }
                case SegmentType.RFF:
                    {
                        string qualifier = ((RFF)obj).referenceQualifier;

                        if (qualifier == "CT" || qualifier == "PL" || qualifier == "ZZZ")
                        {
                            this.GRP1.Add(new RFFDTM((RFF)obj));
                        }
                        else if (qualifier == "GN" || qualifier == "VA")
                        {
                            if ((i = this.GRP2.Count) > 0)
                            {
                                this.GRP2[i - 1].GRP3.RFF = (RFF)obj;
                            }
                        }
                        else if (qualifier == "API")
                        {
                            if ((i = this.GRP16.Count) > 0)
                            {
                                this.GRP16[i - 1].GRP26.NAD.GRP27.RFF = (RFF)obj;
                            }
                        }
                        break;
                    }
                case SegmentType.NAD:
                    {
                        if (SegmentType.PGI == lastAccessed)
                        {
                            if ((i = this.GRP16.Count) > 0)
                            {
                                this.GRP16[i - 1].GRP26.NAD = new GRP26NAD((NAD)obj);
                            }
                        }
                        else
                            this.GRP2.Add(new D96A_PC_NAD((NAD)obj));
                        break;

                    }

                case SegmentType.CTA:
                    {
                        if ((i = this.GRP2.Count) > 0)
                        {
                            this.GRP2[i - 1].GRP4.CTA = new CTACOM((CTA)obj);
                        }
                        break;
                    }
                case SegmentType.COM:
                    {
                        if ((i = this.GRP2.Count) > 0)
                        {
                            this.GRP2[i - 1].GRP4.CTA.COM = (COM)obj;
                        }
                        break;
                    }
                case SegmentType.TOD:
                    {
                        this.GRP9.TOD = new TODLOC((TOD)obj);
                        break;
                    }
                case SegmentType.LOC:
                    {
                        this.GRP9.TOD.LOCCollection.Add((LOC)obj);
                        break;
                    }
                case SegmentType.PGI:
                    {
                        lastAccessed = type;
                        this.GRP16.Add(new PGINAD((PGI)obj));
                        break;
                    }
                case SegmentType.LIN:
                    {
                        if ((i = this.GRP16.Count) > 0)
                        {
                            this.GRP16[i - 1].GRP33.Add(new D96A_PC_LIN((LIN)obj));
                        }
                        break;
                    }
                case SegmentType.PIA:
                    {
                        if ((i = this.GRP16.Count) > 0)
                        {
                            if ((j = this.GRP16[i - 1].GRP33.Count) > 0)
                            {
                                this.GRP16[i - 1].GRP33[j - 1].PIACollection.Add((PIA)obj);
                            }
                        }
                        break;
                    }
                case SegmentType.IMD:
                    {
                        if ((i = this.GRP16.Count) > 0)
                        {
                            if ((j = this.GRP16[i - 1].GRP33.Count) > 0)
                            {
                                this.GRP16[i - 1].GRP33[j - 1].IMDCollection.Add((IMD)obj);
                            }
                        }
                        break;
                    }
                case SegmentType.MEA:
                    {
                        if ((i = this.GRP16.Count) > 0)
                        {
                            if (SegmentType.PAC == lastAccessed)
                            {
                                if ((j = this.GRP16[i - 1].GRP33.Count) > 0)
                                {
                                    if ((k = this.GRP16[i - 1].GRP33[j - 1].GRP44.Count) > 0)
                                    {
                                        this.GRP16[i - 1].GRP33[j - 1].GRP44[k - 1].Add((MEA)obj);
                                    }
                                }
                            }
                            else
                            {
                                if ((j = this.GRP16[i - 1].GRP33.Count) > 0)
                                    this.GRP16[i - 1].GRP33[j - 1].MEACollection.Add((MEA)obj);
                            }
                        }
                        break;
                    }
                case SegmentType.QTY:
                    {
                        if ((i = this.GRP16.Count) > 0)
                        {
                            if ((j = this.GRP16[i - 1].GRP33.Count) > 0)
                            {
                                this.GRP16[i - 1].GRP33[j - 1].QTYCollection.Add((QTY)obj);
                            }
                        }
                        break;
                    }
                case SegmentType.HAN:
                    {
                        if ((i = this.GRP16.Count) > 0)
                        {
                            if ((j = this.GRP16[i - 1].GRP33.Count) > 0)
                            {
                                this.GRP16[i - 1].GRP33[j - 1].HANCollection.Add((HAN)obj);
                            }
                        }
                        break;
                    }
                case SegmentType.FTX:
                    {
                        if ((i = this.GRP16.Count) > 0)
                        {
                            if ((j = this.GRP16[i - 1].GRP33.Count) > 0)
                            {
                                this.GRP16[i - 1].GRP33[j - 1].FTXCollection.Add((FTX)obj);
                            }
                        }
                        break;
                    }
                case SegmentType.TAX:
                    {
                        if ((i = this.GRP16.Count) > 0)
                        {
                            if ((j = this.GRP16[i - 1].GRP33.Count) > 0)
                            {
                                this.GRP16[i - 1].GRP33[j - 1].GRP35.Add((TAX)obj);
                            }
                        }
                        break;
                    }
                case SegmentType.PRI:
                    {
                        if ((i = this.GRP16.Count) > 0)
                        {
                            if ((j = this.GRP16[i - 1].GRP33.Count) > 0)
                            {
                                this.GRP16[i - 1].GRP33[j - 1].GRP37.Add((PRI)obj);
                            }
                        }
                        break;
                    }
                case SegmentType.PAC:
                    {
                        lastAccessed = type;
                        if ((i = this.GRP16.Count) > 0)
                        {
                            if ((j = this.GRP16[i - 1].GRP33.Count) > 0)
                            {
                                this.GRP16[i - 1].GRP33[j - 1].GRP44.Add(new PACMEA((PAC)obj));
                            }
                        }
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

        #endregion

        #region Constructor
        public PRICAT()
        {
        }
        #endregion

    }
    #endregion

    #region GRP 1
    [XmlType(TypeName = "GRP1", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP1
    {
        [System.Runtime.InteropServices.DispIdAttribute(-4)]
        public IEnumerator GetEnumerator()
        {
            return RFFCollection.GetEnumerator();
        }

        public RFFDTM Add(RFFDTM obj)
        {
            return RFFCollection.Add(obj);
        }

        [XmlIgnore]
        public RFFDTM this[int index]
        {
            get { return (RFFDTM)RFFCollection[index]; }
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

        public RFFDTM Remove(int index)
        {
            RFFDTM obj = RFFCollection[index];
            RFFCollection.Remove(obj);
            return obj;
        }

        public void Remove(object obj)
        {
            RFFCollection.Remove(obj);
        }

        [XmlElement(Type = typeof(RFFDTM), ElementName = "RFF", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public RFFDTMCollection __RFFCollection;

        [XmlIgnore]
        public RFFDTMCollection RFFCollection
        {
            get
            {
                if (__RFFCollection == null) __RFFCollection = new RFFDTMCollection();
                return __RFFCollection;
            }
            set { __RFFCollection = value; }
        }

        public GRP1()
        {
        }
    }
    #endregion

    #region GRP 2
    [XmlType(TypeName = "GRP2", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP2
    {
        [System.Runtime.InteropServices.DispIdAttribute(-4)]
        public IEnumerator GetEnumerator()
        {
            return D96A_PC_NADCollection.GetEnumerator();
        }

        public D96A_PC_NAD Add(D96A_PC_NAD obj)
        {
            return D96A_PC_NADCollection.Add(obj);
        }

        [XmlIgnore]
        public D96A_PC_NAD this[int index]
        {
            get { return (D96A_PC_NAD)D96A_PC_NADCollection[index]; }
        }

        [XmlIgnore]
        public int Count
        {
            get { return D96A_PC_NADCollection.Count; }
        }

        public void Clear()
        {
            D96A_PC_NADCollection.Clear();
        }

        public D96A_PC_NAD Remove(int index)
        {
            D96A_PC_NAD obj = D96A_PC_NADCollection[index];
            D96A_PC_NADCollection.Remove(obj);
            return obj;
        }

        public void Remove(object obj)
        {
            D96A_PC_NADCollection.Remove(obj);
        }

        [XmlElement(Type = typeof(D96A_PC_NAD), ElementName = "NAD", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public D96A_PC_NADCollection __NADCollection;

        [XmlIgnore]
        public D96A_PC_NADCollection D96A_PC_NADCollection
        {
            get
            {
                if (__NADCollection == null) __NADCollection = new D96A_PC_NADCollection();
                return __NADCollection;
            }
            set { __NADCollection = value; }
        }

        public GRP2()
        {
        }
    }


    [XmlType(TypeName = "NAD", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class D96A_PC_NAD : EDIFACT.BASETYPES.NAD
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

        public D96A_PC_NAD()
        {
        }

        public D96A_PC_NAD(NAD nadObject)
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
    public class D96A_PC_NADCollection : ArrayList
    {
        public D96A_PC_NAD Add(D96A_PC_NAD obj)
        {
            base.Add(obj);
            return obj;
        }

        public D96A_PC_NAD Add()
        {
            return Add(new D96A_PC_NAD());
        }

        public void Insert(int index, D96A_PC_NAD obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(D96A_PC_NAD obj)
        {
            base.Remove(obj);
        }

        new public D96A_PC_NAD this[int index]
        {
            get { return (D96A_PC_NAD)base[index]; }
            set { base[index] = value; }
        }
    }
    #endregion

    #region GRP 3
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

    #region GRP 4
    [XmlType(TypeName = "GRP4", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP4
    {

        [XmlElement(Type = typeof(CTACOM), ElementName = "CTA", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public CTACOM __CTA;

        [XmlIgnore]
        public CTACOM CTA
        {
            get
            {
                if (__CTA == null) __CTA = new CTACOM();
                return __CTA;
            }
            set { __CTA = value; }
        }

        public GRP4()
        {
        }
    }


    [XmlType(TypeName = "CTA", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class CTACOM : EDIFACT.BASETYPES.CTA
    {

        [XmlElement(Type = typeof(COM), ElementName = "COM", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public COM __COM;

        [XmlIgnore]
        public COM COM
        {
            get
            {
                if (__COM == null) __COM = new COM();
                return __COM;
            }
            set { __COM = value; }
        }

        public CTACOM()
        {
        }

        public CTACOM(CTA ctaObject)
        {
            this.contactFunctionCoded = ((CTA)ctaObject).contactFunctionCoded;
            this.departmentOrEmployeeIDNumber = ((CTA)ctaObject).departmentOrEmployeeIDNumber;
            this.deptOrEmployee = ((CTA)ctaObject).deptOrEmployee;
        }
    }
    #endregion

    #region GRP 9
    [XmlType(TypeName = "GRP9", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP9
    {
        [XmlElement(Type = typeof(TODLOC), ElementName = "TOD", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public TODLOC __TOD;

        [XmlIgnore]
        public TODLOC TOD
        {
            get
            {
                if (__TOD == null) __TOD = new TODLOC();
                return __TOD;
            }
            set { __TOD = value; }
        }

        public GRP9()
        {
        }
    }


    [XmlType(TypeName = "TOD", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class TODLOC : TOD
    {
        [XmlElement(Type = typeof(LOC), ElementName = "LOC", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public LOCCollection __LOCCollection;

        [XmlIgnore]
        public LOCCollection LOCCollection
        {
            get
            {
                if (__LOCCollection == null) __LOCCollection = new LOCCollection();
                return __LOCCollection;
            }
            set { __LOCCollection = value; }
        }

        public TODLOC()
        {
        }

        public TODLOC(TOD todObject)
        {
            this.codeListQualifier = todObject.codeListQualifier;
            this.codeListResponsibleAgency = todObject.codeListResponsibleAgency;
            this.termOfDelivery = todObject.termOfDelivery;
            this.todCoded = todObject.todCoded;
            this.todFunction = todObject.todFunction;
            this.transportMethodOfPayment = todObject.transportMethodOfPayment;
        }
    }
    #endregion

    #region GRP 16 AND PGINAD
    [XmlType(TypeName = "GRP16", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP16
    {
        [System.Runtime.InteropServices.DispIdAttribute(-4)]
        public IEnumerator GetEnumerator()
        {
            return PGINADCollection.GetEnumerator();
        }

        public PGINAD Add(PGINAD obj)
        {
            return PGINADCollection.Add(obj);
        }

        [XmlIgnore]
        public PGINAD this[int index]
        {
            get { return (PGINAD)PGINADCollection[index]; }
        }

        [XmlIgnore]
        public int Count
        {
            get { return PGINADCollection.Count; }
        }

        public void Clear()
        {
            PGINADCollection.Clear();
        }

        public PGINAD Remove(int index)
        {
            PGINAD obj = PGINADCollection[index];
            PGINADCollection.Remove(obj);
            return obj;
        }

        public void Remove(object obj)
        {
            PGINADCollection.Remove(obj);
        }

        [XmlElement(Type = typeof(PGINAD), ElementName = "PGI", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public PGINADCollection __PGICollection;

        [XmlIgnore]
        public PGINADCollection PGINADCollection
        {
            get
            {
                if (__PGICollection == null) __PGICollection = new PGINADCollection();
                return __PGICollection;
            }
            set { __PGICollection = value; }
        }

        public GRP16()
        {
        }
    }


    [XmlType(TypeName = "PGI", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class PGINAD : EDIFACT.BASETYPES.PGI
    {

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

        [XmlElement(Type = typeof(GRP33), ElementName = "GRP33", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public GRP33 __GRP33;

        [XmlIgnore]
        public GRP33 GRP33
        {
            get
            {
                if (__GRP33 == null) __GRP33 = new GRP33();
                return __GRP33;
            }
            set { __GRP33 = value; }
        }

        public PGINAD()
        {
        }

        public PGINAD(PGI pgiObject)
        {
            this.priceGroup = ((PGI)pgiObject).priceGroup;
            this.priceTariffTypeCoded = ((PGI)pgiObject).priceTariffTypeCoded;
            this.pricingGroupCoded = ((PGI)pgiObject).pricingGroupCoded;
        }
    }

    [Serializable]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public class PGINADCollection : ArrayList
    {
        public PGINAD Add(PGINAD obj)
        {
            base.Add(obj);
            return obj;
        }

        public PGINAD Add()
        {
            return Add(new PGINAD());
        }

        public void Insert(int index, PGINAD obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(PGINAD obj)
        {
            base.Remove(obj);
        }

        new public PGINAD this[int index]
        {
            get { return (PGINAD)base[index]; }
            set { base[index] = value; }
        }
    }
    #endregion

    #region GRP 26, 27
    [XmlType(TypeName = "GRP26", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP26
    {

        [XmlElement(Type = typeof(GRP26NAD), ElementName = "NAD", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public GRP26NAD __NAD;

        [XmlIgnore]
        public GRP26NAD NAD
        {
            get
            {
                if (__NAD == null) __NAD = new GRP26NAD();
                return __NAD;
            }
            set { __NAD = value; }
        }

        public GRP26()
        {
        }
    }


    [XmlType(TypeName = "GRP26NAD", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP26NAD : EDIFACT.BASETYPES.NAD
    {

        [XmlElement(Type = typeof(GRP27), ElementName = "GRP27", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public GRP27 __GRP27;

        [XmlIgnore]
        public GRP27 GRP27
        {
            get
            {
                if (__GRP27 == null) __GRP27 = new GRP27();
                return __GRP27;
            }
            set { __GRP27 = value; }
        }

        public GRP26NAD()
        {
        }

        public GRP26NAD(NAD nadObject)
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


    [XmlType(TypeName = "GRP27", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP27
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

        public GRP27()
        {
        }
    }
    #endregion

    #region GRP33 and D96A_PC_LIN
    [XmlType(TypeName = "GRP33", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP33
    {
        [System.Runtime.InteropServices.DispIdAttribute(-4)]
        public IEnumerator GetEnumerator()
        {
            return D96A_PC_LINCollection.GetEnumerator();
        }

        public D96A_PC_LIN Add(D96A_PC_LIN obj)
        {
            return D96A_PC_LINCollection.Add(obj);
        }

        [XmlIgnore]
        public D96A_PC_LIN this[int index]
        {
            get { return (D96A_PC_LIN)D96A_PC_LINCollection[index]; }
        }

        [XmlIgnore]
        public int Count
        {
            get { return D96A_PC_LINCollection.Count; }
        }

        public void Clear()
        {
            D96A_PC_LINCollection.Clear();
        }

        public D96A_PC_LIN Remove(int index)
        {
            D96A_PC_LIN obj = D96A_PC_LINCollection[index];
            D96A_PC_LINCollection.Remove(obj);
            return obj;
        }

        public void Remove(object obj)
        {
            D96A_PC_LINCollection.Remove(obj);
        }

        [XmlElement(Type = typeof(D96A_PC_LIN), ElementName = "LIN", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public D96A_PC_LINCollection __LINCollection;

        [XmlIgnore]
        public D96A_PC_LINCollection D96A_PC_LINCollection
        {
            get
            {
                if (__LINCollection == null) __LINCollection = new D96A_PC_LINCollection();
                return __LINCollection;
            }
            set { __LINCollection = value; }
        }

        public GRP33()
        {
        }
    }


    [XmlType(TypeName = "LIN", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class D96A_PC_LIN : EDIFACT.BASETYPES.LIN
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

        [XmlElement(Type = typeof(GRP37), ElementName = "GRP37", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public GRP37 __GRP37;

        [XmlIgnore]
        public GRP37 GRP37
        {
            get
            {
                if (__GRP37 == null) __GRP37 = new GRP37();
                return __GRP37;
            }
            set { __GRP37 = value; }
        }

        [XmlElement(Type = typeof(GRP44), ElementName = "GRP44", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public GRP44 __GRP44;

        [XmlIgnore]
        public GRP44 GRP44
        {
            get
            {
                if (__GRP44 == null) __GRP44 = new GRP44();
                return __GRP44;
            }
            set { __GRP44 = value; }
        }

        public D96A_PC_LIN()
        {
        }

        public D96A_PC_LIN(LIN linObject)
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
    public class D96A_PC_LINCollection : ArrayList
    {
        public D96A_PC_LIN Add(D96A_PC_LIN obj)
        {
            base.Add(obj);
            return obj;
        }

        public D96A_PC_LIN Add()
        {
            return Add(new D96A_PC_LIN());
        }

        public void Insert(int index, D96A_PC_LIN obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(D96A_PC_LIN obj)
        {
            base.Remove(obj);
        }

        new public D96A_PC_LIN this[int index]
        {
            get { return (D96A_PC_LIN)base[index]; }
            set { base[index] = value; }
        }
    }
    #endregion

    #region GRP 35
    [XmlType(TypeName = "GRP35", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP35
    {
        [System.Runtime.InteropServices.DispIdAttribute(-4)]
        public IEnumerator GetEnumerator()
        {
            return TAXCollection.GetEnumerator();
        }

        public TAX Add(TAX obj)
        {
            return TAXCollection.Add(obj);
        }

        [XmlIgnore]
        public TAX this[int index]
        {
            get { return (TAX)TAXCollection[index]; }
        }

        [XmlIgnore]
        public int Count
        {
            get { return TAXCollection.Count; }
        }

        public void Clear()
        {
            TAXCollection.Clear();
        }

        public TAX Remove(int index)
        {
            TAX obj = TAXCollection[index];
            TAXCollection.Remove(obj);
            return obj;
        }

        public void Remove(object obj)
        {
            TAXCollection.Remove(obj);
        }

        [XmlElement(Type = typeof(TAX), ElementName = "TAX", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public TAXCollection __TAXCollection;

        [XmlIgnore]
        public TAXCollection TAXCollection
        {
            get
            {
                if (__TAXCollection == null) __TAXCollection = new TAXCollection();
                return __TAXCollection;
            }
            set { __TAXCollection = value; }
        }

        public GRP35()
        {
        }
    }
    #endregion

    #region GRP 37
    [XmlType(TypeName = "GRP37", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP37
    {
        [System.Runtime.InteropServices.DispIdAttribute(-4)]
        public IEnumerator GetEnumerator()
        {
            return PRICollection.GetEnumerator();
        }

        public PRI Add(PRI obj)
        {
            return PRICollection.Add(obj);
        }

        [XmlIgnore]
        public PRI this[int index]
        {
            get { return (PRI)PRICollection[index]; }
        }

        [XmlIgnore]
        public int Count
        {
            get { return PRICollection.Count; }
        }

        public void Clear()
        {
            PRICollection.Clear();
        }

        public PRI Remove(int index)
        {
            PRI obj = PRICollection[index];
            PRICollection.Remove(obj);
            return obj;
        }

        public void Remove(object obj)
        {
            PRICollection.Remove(obj);
        }

        [XmlElement(Type = typeof(PRI), ElementName = "PRI", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public PRICollection __PRICollection;

        [XmlIgnore]
        public PRICollection PRICollection
        {
            get
            {
                if (__PRICollection == null) __PRICollection = new PRICollection();
                return __PRICollection;
            }
            set { __PRICollection = value; }
        }

        public GRP37()
        {
        }
    }
    #endregion

    #region GRP 44 and PACMEA
    [XmlType(TypeName = "GRP44", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP44
    {
        [System.Runtime.InteropServices.DispIdAttribute(-4)]
        public IEnumerator GetEnumerator()
        {
            return PACMEACollection.GetEnumerator();
        }

        public PACMEA Add(PACMEA obj)
        {
            return PACMEACollection.Add(obj);
        }

        [XmlIgnore]
        public PACMEA this[int index]
        {
            get { return (PACMEA)PACMEACollection[index]; }
        }

        [XmlIgnore]
        public int Count
        {
            get { return PACMEACollection.Count; }
        }

        public void Clear()
        {
            PACMEACollection.Clear();
        }

        public PACMEA Remove(int index)
        {
            PACMEA obj = PACMEACollection[index];
            PACMEACollection.Remove(obj);
            return obj;
        }

        public void Remove(object obj)
        {
            PACMEACollection.Remove(obj);
        }

        [XmlElement(Type = typeof(PACMEA), ElementName = "PAC", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public PACMEACollection __PACCollection;

        [XmlIgnore]
        public PACMEACollection PACMEACollection
        {
            get
            {
                if (__PACCollection == null) __PACCollection = new PACMEACollection();
                return __PACCollection;
            }
            set { __PACCollection = value; }
        }

        public GRP44()
        {
        }
    }


    [XmlType(TypeName = "PAC", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class PACMEA : EDIFACT.BASETYPES.PAC
    {
        [System.Runtime.InteropServices.DispIdAttribute(-4)]
        public IEnumerator GetEnumerator()
        {
            return MEACollection.GetEnumerator();
        }

        public MEA Add(MEA obj)
        {
            return MEACollection.Add(obj);
        }

        [XmlIgnore]
        public MEA this[int index]
        {
            get { return (MEA)MEACollection[index]; }
        }

        [XmlIgnore]
        public int Count
        {
            get { return MEACollection.Count; }
        }

        public void Clear()
        {
            MEACollection.Clear();
        }

        public MEA Remove(int index)
        {
            MEA obj = MEACollection[index];
            MEACollection.Remove(obj);
            return obj;
        }

        public void Remove(object obj)
        {
            MEACollection.Remove(obj);
        }

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

        public PACMEA()
        {
        }

        public PACMEA(PAC pacObject)
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
    public class PACMEACollection : ArrayList
    {
        public PACMEA Add(PACMEA obj)
        {
            base.Add(obj);
            return obj;
        }

        public PACMEA Add()
        {
            return Add(new PACMEA());
        }

        public void Insert(int index, PACMEA obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(PACMEA obj)
        {
            base.Remove(obj);
        }

        new public PACMEA this[int index]
        {
            get { return (PACMEA)base[index]; }
            set { base[index] = value; }
        }
    }
    #endregion

    #region RFF_DTM
    [XmlType(TypeName = "RFF", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
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
    #endregion

}
