//--------------------------------------------------------------
// <CreatedBy>
//     Anthony R Yates
//	   Feb. 27, 2005
//     Runtime Version: 1.1.4322.2032
//	   
//     It contains D93A PRICAT Classes
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

namespace EDIFACT.D93A.PRICAT
{

    #region Schema Declaration
    public struct Declarations
    {
        public const string SchemaVersion = "http://www.default.com/D93A/pricat";
    }
    #endregion

    #region D93A_PRICAT Class

    [XmlRoot(ElementName = "D93A_PRICAT", Namespace = Declarations.SchemaVersion, IsNullable = false), Serializable]
    public class D93A_PRICAT : IMessage
    {
        #region Properties

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
        #endregion

        #region Contructor
        public D93A_PRICAT()
        {
        }
        #endregion

        #region IMessage Members

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
        bool pgiAccessed;

        #region Class Fields
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

                        int i, j;
                        if (((DTM)obj).dateTimePeriodQualifier == "137" ||
                            ((DTM)obj).dateTimePeriodQualifier == "194" ||
                            ((DTM)obj).dateTimePeriodQualifier == "206")
                        {
                            this.DTMCollection.Add((DTM)obj);

                            if (((DTM)obj).dateTimePeriodQualifier != "137")
                            {
                                if ((i = this.GRP16Collection.Count) > 0)
                                {
                                    if ((j = this.GRP16Collection[i - 1].PGI.GRP33Collection.Count) > 0)
                                        this.GRP16Collection[i - 1].PGI.GRP33Collection[j - 1].PIT.DTMCollection.Add((DTM)obj);
                                }
                            }
                        }
                        else if (((DTM)obj).dateTimePeriodQualifier == "171")
                        {
                            if ((i = this.GRP1Collection.Count) > 0)
                                this.GRP1Collection[i - 1].RFF.DTM = (DTM)obj;
                        }
                        else
                        {
                            if ((i = this.GRP16Collection.Count) > 0)
                            {
                                if ((j = this.GRP16Collection[i - 1].PGI.GRP33Collection.Count) > 0)
                                    this.GRP16Collection[i - 1].PGI.GRP33Collection[j - 1].PIT.DTMCollection.Add((DTM)obj);
                            }
                        }
                        break;
                    }
                case SegmentType.RFF:
                    {

                        int i;
                        if (((RFF)obj).referenceQualifier == "CT" ||
                            ((RFF)obj).referenceQualifier == "PL" ||
                            ((RFF)obj).referenceQualifier == "ZZZ")
                        {
                            this.GRP1Collection.Add(new GRP1((RFF)obj));
                        }
                        else if (((RFF)obj).referenceQualifier == "GN" ||
                            ((RFF)obj).referenceQualifier == "VA")
                        {
                            if ((i = this.GRP2Collection.Count) > 0)
                            {
                                this.GRP2Collection[i - 1].NAD.GRP3 = new GRP3((RFF)obj);
                            }
                        }
                        else if (((RFF)obj).referenceQualifier == "API")
                        {
                            if ((i = this.GRP16Collection.Count) > 0)
                            {
                                if (this.GRP16Collection[i - 1].PGI.GRP25 != null)
                                    this.GRP16Collection[i - 1].PGI.GRP25.NAD.GRP26 = new GRP26((RFF)obj);

                            }
                        }
                        break;
                    }
                case SegmentType.NAD:
                    {
                        int i;
                        if (!pgiAccessed)
                        {
                            this.GRP2Collection.Add(new GRP2((NAD)obj));
                            break;
                        }
                        if ((i = this.GRP16Collection.Count) > 0)
                        {
                            this.GRP16Collection[i - 1].PGI.GRP25.NAD = new NADGRP26((NAD)obj);
                        }
                    }
                    break;
                case SegmentType.CTA:
                    {
                        int i;
                        if ((i = this.GRP2Collection.Count) > 0)
                        {
                            this.GRP2Collection[i - 1].NAD.GRP4.CTA = new CTACOM((CTA)obj);
                        }
                        break;
                    }
                case SegmentType.COM:
                    {
                        int i;
                        if ((i = this.GRP2Collection.Count) > 0)
                        {
                            this.GRP2Collection[i - 1].NAD.GRP4.CTA.COMCollection.Add((COM)obj);
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
                        this.GRP9.TOD.LOC = (LOC)obj;
                        break;
                    }
                case SegmentType.PGI:
                    {
                        this.GRP16Collection.Add(new GRP16((PGI)obj));
                        pgiAccessed = true;
                        break;
                    }
                case SegmentType.PIT:
                    {
                        int i;
                        if ((i = this.GRP16Collection.Count) > 0)
                        {
                            this.GRP16Collection[i - 1].PGI.GRP33Collection.Add(new GRP33((PIT)obj));
                        }
                        break;
                    }
                case SegmentType.PIA:
                    {
                        int i, j;
                        if ((i = this.GRP16Collection.Count) > 0)
                        {
                            if ((j = this.GRP16Collection[i - 1].PGI.GRP33Collection.Count) > 0)
                            {
                                this.GRP16Collection[i - 1].PGI.GRP33Collection[j - 1].PIT.PIACollection.Add((PIA)obj);
                            }
                        }
                        break;
                    }
                case SegmentType.IMD:
                    {
                        int i, j;
                        if ((i = this.GRP16Collection.Count) > 0)
                        {
                            if ((j = this.GRP16Collection[i - 1].PGI.GRP33Collection.Count) > 0)
                            {
                                this.GRP16Collection[i - 1].PGI.GRP33Collection[j - 1].PIT.IMDCollection.Add((IMD)obj);
                            }
                        }
                        break;
                    }
                case SegmentType.MEA:
                    {
                        int i, j, k;
                        if ((i = this.GRP16Collection.Count) > 0)
                        {
                            if ((j = this.GRP16Collection[i - 1].PGI.GRP33Collection.Count) > 0)
                            {
                                this.GRP16Collection[i - 1].PGI.GRP33Collection[j - 1].PIT.MEACollection.Add((MEA)obj);
                                if (((MEA)obj).measurementApplicationQualifier == "PD")
                                {
                                    if ((k = this.GRP16Collection[i - 1].PGI.GRP33Collection[j - 1].PIT.GRP43Collection.Count) > 0)
                                    {
                                        this.GRP16Collection[i - 1].PGI.GRP33Collection[j - 1].PIT.GRP43Collection[k - 1].PAC.MEACollection.Add((MEA)obj);
                                    }
                                }
                            }
                        }
                        break;
                    }
                case SegmentType.QTY:
                    {
                        int i, j;
                        if ((i = this.GRP16Collection.Count) > 0)
                        {
                            if ((j = this.GRP16Collection[i - 1].PGI.GRP33Collection.Count) > 0)
                            {
                                this.GRP16Collection[i - 1].PGI.GRP33Collection[j - 1].PIT.QTYCollection.Add((QTY)obj);
                            }
                        }
                        break;
                    }
                case SegmentType.HAN:
                    {
                        int i, j;
                        if ((i = this.GRP16Collection.Count) > 0)
                        {
                            if ((j = this.GRP16Collection[i - 1].PGI.GRP33Collection.Count) > 0)
                            {
                                this.GRP16Collection[i - 1].PGI.GRP33Collection[j - 1].PIT.HANCollection.Add((HAN)obj);
                            }
                        }
                        break;
                    }
                case SegmentType.FTX:
                    {
                        int i, j;
                        if ((i = this.GRP16Collection.Count) > 0)
                        {
                            if ((j = this.GRP16Collection[i - 1].PGI.GRP33Collection.Count) > 0)
                            {
                                this.GRP16Collection[i - 1].PGI.GRP33Collection[j - 1].PIT.FTXCollection.Add((FTX)obj);
                            }
                        }
                        break;
                    }
                case SegmentType.TAX:
                    {
                        int i, j;
                        if ((i = this.GRP16Collection.Count) > 0)
                        {
                            if ((j = this.GRP16Collection[i - 1].PGI.GRP33Collection.Count) > 0)
                            {
                                this.GRP16Collection[i - 1].PGI.GRP33Collection[j - 1].PIT.GRP34Collection.Add(new GRP34((TAX)obj));
                            }
                        }
                        break;
                    }
                case SegmentType.PRI:
                    {
                        int i, j;
                        if ((i = this.GRP16Collection.Count) > 0)
                        {
                            if ((j = this.GRP16Collection[i - 1].PGI.GRP33Collection.Count) > 0)
                            {
                                this.GRP16Collection[i - 1].PGI.GRP33Collection[j - 1].PIT.GRP35.PRI = (PRI)obj;
                            }
                        }
                        break;
                    }
                case SegmentType.PAC:
                    {
                        int i, j;
                        if ((i = this.GRP16Collection.Count) > 0)
                        {
                            if ((j = this.GRP16Collection[i - 1].PGI.GRP33Collection.Count) > 0)
                            {
                                this.GRP16Collection[i - 1].PGI.GRP33Collection[j - 1].PIT.GRP43Collection.Add(new GRP43((PAC)obj));
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

        public PRICAT()
        {
        }
    }
    #endregion

    #region GRP1
    [XmlType(TypeName = "GRP1", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP1
    {

        [XmlElement(Type = typeof(RFFDTM), ElementName = "RFF", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public RFFDTM __RFF;

        [XmlIgnore]
        public RFFDTM RFF
        {
            get
            {
                if (__RFF == null) __RFF = new RFFDTM();
                return __RFF;
            }
            set { __RFF = value; }
        }

        public GRP1()
        {
        }

        public GRP1(RFF rffObject)
        {
            this.RFF.lineNumber = rffObject.lineNumber;
            this.RFF.referenceNumber = rffObject.referenceNumber;
            this.RFF.referenceQualifier = rffObject.referenceQualifier;
            this.RFF.referenceVersionNumber = rffObject.referenceVersionNumber;
        }
    }
    [Serializable]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
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

    [Serializable]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
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

        public GRP3(RFF rffObject)
        {
            this.RFF.lineNumber = rffObject.lineNumber;
            this.RFF.referenceNumber = rffObject.referenceNumber;
            this.RFF.referenceQualifier = rffObject.referenceQualifier;
            this.RFF.referenceVersionNumber = rffObject.referenceVersionNumber;
        }


    }

    #endregion

    #region GRP4

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
    #endregion

    #region GRP9

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
    #endregion

    #region GRP16

    [XmlType(TypeName = "GRP16", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP16
    {

        [XmlElement(Type = typeof(PGI_NP), ElementName = "PGI", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public PGI_NP __PGI;

        [XmlIgnore]
        public PGI_NP PGI
        {
            get
            {
                if (__PGI == null) __PGI = new PGI_NP();
                return __PGI;
            }
            set { __PGI = value; }
        }

        public GRP16()
        {
        }

        public GRP16(PGI pgiObject)
        {
            this.PGI.priceGroup = pgiObject.priceGroup;
            this.PGI.priceTariffTypeCoded = pgiObject.priceTariffTypeCoded;
            this.PGI.pricingGroupCoded = pgiObject.pricingGroupCoded;
        }
    }

    [Serializable]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
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

    #region GRP25

    [XmlType(TypeName = "GRP25", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP25
    {

        [XmlElement(Type = typeof(NADGRP26), ElementName = "NAD", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public NADGRP26 __NAD;

        [XmlIgnore]
        public NADGRP26 NAD
        {
            get
            {
                if (__NAD == null) __NAD = new NADGRP26();
                return __NAD;
            }
            set { __NAD = value; }
        }

        public GRP25()
        {
        }
    }
    #endregion

    #region GRP26
    [XmlType(TypeName = "GRP26", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP26
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

        public GRP26()
        {
        }

        public GRP26(RFF rffObject)
        {
            this.RFF.lineNumber = rffObject.lineNumber;
            this.RFF.referenceNumber = rffObject.referenceNumber;
            this.RFF.referenceQualifier = rffObject.referenceQualifier;
            this.RFF.referenceVersionNumber = rffObject.referenceVersionNumber;
        }
    }
    #endregion

    #region GRP33
    [XmlType(TypeName = "GRP33", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP33
    {

        [XmlElement(Type = typeof(PIT_PIM), ElementName = "PIT", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public PIT_PIM __PIT;

        [XmlIgnore]
        public PIT_PIM PIT
        {
            get
            {
                if (__PIT == null) __PIT = new PIT_PIM();
                return __PIT;
            }
            set { __PIT = value; }
        }

        public GRP33()
        {
        }

        public GRP33(PIT pitObject)
        {
            this.PIT.actionRequestNotificationCoded = pitObject.actionRequestNotificationCoded;
            this.PIT.articleAvailabilityCoded = pitObject.articleAvailabilityCoded;
            this.PIT.codeListQualifier = pitObject.codeListQualifier;
            this.PIT.codeListResponsibleAgency = pitObject.codeListResponsibleAgency;
            this.PIT.configurationCoded = pitObject.configurationCoded;
            this.PIT.configurationLevel = pitObject.configurationLevel;
            this.PIT.lineItemNumber = pitObject.lineItemNumber;
            this.PIT.priceChangeIndicator = pitObject.priceChangeIndicator;
            this.PIT.subLineIndicatorCoded = pitObject.subLineIndicatorCoded;
        }
    }

    [Serializable]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public class GRP33Collection : ArrayList
    {
        public GRP33 Add(GRP33 obj)
        {
            base.Add(obj);
            return obj;
        }

        public GRP33 Add()
        {
            return Add(new GRP33());
        }

        public void Insert(int index, GRP33 obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(GRP33 obj)
        {
            base.Remove(obj);
        }

        new public GRP33 this[int index]
        {
            get { return (GRP33)base[index]; }
            set { base[index] = value; }
        }
    }

    #endregion

    #region GRP34

    [XmlType(TypeName = "GRP34", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP34
    {

        [XmlElement(Type = typeof(TAX), ElementName = "TAX", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public TAX __TAX;

        [XmlIgnore]
        public TAX TAX
        {
            get
            {
                if (__TAX == null) __TAX = new TAX();
                return __TAX;
            }
            set { __TAX = value; }
        }

        public GRP34()
        {
        }

        public GRP34(TAX taxObject)
        {
            this.TAX = taxObject;
        }
    }

    [Serializable]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public class GRP34Collection : ArrayList
    {
        public GRP34 Add(GRP34 obj)
        {
            base.Add(obj);
            return obj;
        }

        public GRP34 Add()
        {
            return Add(new GRP34());
        }

        public void Insert(int index, GRP34 obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(GRP34 obj)
        {
            base.Remove(obj);
        }

        new public GRP34 this[int index]
        {
            get { return (GRP34)base[index]; }
            set { base[index] = value; }
        }
    }
    #endregion

    #region GRP35
    [XmlType(TypeName = "GRP35", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP35
    {

        [XmlElement(Type = typeof(PRI), ElementName = "PRI", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public PRI __PRI;

        [XmlIgnore]
        public PRI PRI
        {
            get
            {
                if (__PRI == null) __PRI = new PRI();
                return __PRI;
            }
            set { __PRI = value; }
        }

        public GRP35()
        {
        }
    }
    #endregion

    #region GRP43
    [XmlType(TypeName = "GRP43", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP43
    {

        [XmlElement(Type = typeof(PACMEA), ElementName = "PAC", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public PACMEA __PAC;

        [XmlIgnore]
        public PACMEA PAC
        {
            get
            {
                if (__PAC == null) __PAC = new PACMEA();
                return __PAC;
            }
            set { __PAC = value; }
        }

        public GRP43()
        {
        }

        public GRP43(PAC pacObject)
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
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public class GRP43Collection : ArrayList
    {
        public GRP43 Add(GRP43 obj)
        {
            base.Add(obj);
            return obj;
        }

        public GRP43 Add()
        {
            return Add(new GRP43());
        }

        public void Insert(int index, GRP43 obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(GRP43 obj)
        {
            base.Remove(obj);
        }

        new public GRP43 this[int index]
        {
            get { return (GRP43)base[index]; }
            set { base[index] = value; }
        }
    }
    #endregion

    #region RFFDTM
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
    #endregion

    #region NAD_RC

    [XmlType(TypeName = "NAD", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class NAD_RC : EDIFACT.BASETYPES.NAD
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

        public NAD_RC()
        {
        }
    }
    #endregion

    #region CTACOM

    [XmlType(TypeName = "CTA", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class CTACOM : EDIFACT.BASETYPES.CTA
    {
        [XmlElement(Type = typeof(COM), ElementName = "COM", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public COMCollection __COMCollection;

        [XmlIgnore]
        public COMCollection COMCollection
        {
            get
            {
                if (__COMCollection == null) __COMCollection = new COMCollection();
                return __COMCollection;
            }
            set { __COMCollection = value; }
        }

        public CTACOM()
        {
        }
        public CTACOM(CTA ctaObject)
        {
            this.contactFunctionCoded = ctaObject.contactFunctionCoded;
            this.departmentOrEmployeeIDNumber = ctaObject.departmentOrEmployeeIDNumber;
            this.deptOrEmployee = ctaObject.deptOrEmployee;
        }
    }
    #endregion

    #region TODLOC

    [XmlType(TypeName = "TODLOC", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class TODLOC : TOD
    {
        [XmlElement(Type = typeof(LOC), ElementName = "LOC", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public LOC __LOC;

        [XmlIgnore]
        public LOC LOC
        {
            get
            {
                if (__LOC == null) __LOC = new LOC();
                return __LOC;
            }
            set { __LOC = value; }
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

    #region PGI_NP

    [XmlType(TypeName = "PGI_NP", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class PGI_NP : PGI
    {
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

        [XmlElement(Type = typeof(GRP33), ElementName = "GRP33", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public GRP33Collection __GRP33Collection;

        [XmlIgnore]
        public GRP33Collection GRP33Collection
        {
            get
            {
                if (__GRP33Collection == null) __GRP33Collection = new GRP33Collection();
                return __GRP33Collection;
            }
            set { __GRP33Collection = value; }
        }

        public PGI_NP()
        {
        }
    }
    #endregion

    #region NADGRP26
    [XmlType(TypeName = "NADGRP26", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class NADGRP26 : NAD
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

        public NADGRP26()
        {
        }

        public NADGRP26(NAD nadObject)
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
    #endregion

    #region PIT_PIM
    [XmlType(TypeName = "PIT_PIM", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class PIT_PIM : PIT
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

        [XmlElement(Type = typeof(GRP34), ElementName = "GRP34", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public GRP34Collection __GRP34Collection;

        [XmlIgnore]
        public GRP34Collection GRP34Collection
        {
            get
            {
                if (__GRP34Collection == null) __GRP34Collection = new GRP34Collection();
                return __GRP34Collection;
            }
            set { __GRP34Collection = value; }
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

        [XmlElement(Type = typeof(GRP43), ElementName = "GRP43", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public GRP43Collection __GRP43Collection;

        [XmlIgnore]
        public GRP43Collection GRP43Collection
        {
            get
            {
                if (__GRP43Collection == null) __GRP43Collection = new GRP43Collection();
                return __GRP43Collection;
            }
            set { __GRP43Collection = value; }
        }

        public PIT_PIM()
        {
        }
    }
    #endregion

    #region PACMEA
    [XmlType(TypeName = "PAC", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class PACMEA : EDIFACT.BASETYPES.PAC
    {
        [XmlElement(Type = typeof(MEA), ElementName = "MEA", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
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
    }
    #endregion
}
