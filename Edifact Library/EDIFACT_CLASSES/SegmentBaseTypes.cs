// Copyright 2004, Microsoft Corporation
// <CreatedBy>
//     Anthony R Yates
//	   Feb. 23, 2005
//     Runtime Version: 1.1.4322.2032
//	   
//     Contains Generic EDI Base Classes
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

using System;
using System.Collections;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace EDIFACT.BASETYPES
{

    public struct Declarations
    {
        public const string SchemaVersion = "http://www.default.com/basetypes";
        public const string SchemaVersionD96a = "http://www.default.com/d96a/basetypes";
    }

    #region SegmentTypeCollections

    [Serializable]
    public class PCICollection : ArrayList
    {
        public PCI Add(PCI obj)
        {
            base.Add(obj);
            return obj;
        }

        public PCI Add()
        {
            return Add(new PCI());
        }

        public void Insert(int index, PCI obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(PCI obj)
        {
            base.Remove(obj);
        }

        new public PCI this[int index]
        {
            get { return (PCI)base[index]; }
            set { base[index] = value; }
        }
    }

    [Serializable]
    public class LOCCollection : ArrayList
    {
        public LOC Add(LOC obj)
        {
            base.Add(obj);
            return obj;
        }

        public LOC Add()
        {
            return Add(new LOC());
        }

        public void Insert(int index, LOC obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(LOC obj)
        {
            base.Remove(obj);
        }

        new public LOC this[int index]
        {
            get { return (LOC)base[index]; }
            set { base[index] = value; }
        }
    }

    [Serializable]
    public class RTECollection : ArrayList
    {
        public RTE Add(RTE obj)
        {
            base.Add(obj);
            return obj;
        }

        public RTE Add()
        {
            return Add(new RTE());
        }

        public void Insert(int index, RTE obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(RTE obj)
        {
            base.Remove(obj);
        }

        new public RTE this[int index]
        {
            get { return (RTE)base[index]; }
            set { base[index] = value; }
        }
    }

    [Serializable]
    public class LINCollection : ArrayList
    {
        public LIN Add(LIN obj)
        {
            base.Add(obj);
            return obj;
        }

        public LIN Add()
        {
            return Add(new LIN());
        }

        public void Insert(int index, LIN obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(LIN obj)
        {
            base.Remove(obj);
        }

        new public LIN this[int index]
        {
            get { return (LIN)base[index]; }
            set { base[index] = value; }
        }
    }

    [Serializable]
    public class TAXCollection : ArrayList
    {
        public TAX Add(TAX obj)
        {
            base.Add(obj);
            return obj;
        }

        public TAX Add()
        {
            return Add(new TAX());
        }

        public void Insert(int index, TAX obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(TAX obj)
        {
            base.Remove(obj);
        }

        new public TAX this[int index]
        {
            get { return (TAX)base[index]; }
            set { base[index] = value; }
        }
    }

    [Serializable]
    public class CUXCollection : ArrayList
    {
        public CUX Add(CUX obj)
        {
            base.Add(obj);
            return obj;
        }

        public CUX Add()
        {
            return Add(new CUX());
        }

        public void Insert(int index, CUX obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(CUX obj)
        {
            base.Remove(obj);
        }

        new public CUX this[int index]
        {
            get { return (CUX)base[index]; }
            set { base[index] = value; }
        }
    }

    [Serializable]
    public class PACCollection : ArrayList
    {
        public PAC Add(PAC obj)
        {
            base.Add(obj);
            return obj;
        }

        public PAC Add()
        {
            return Add(new PAC());
        }

        public void Insert(int index, PAC obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(PAC obj)
        {
            base.Remove(obj);
        }

        new public PAC this[int index]
        {
            get { return (PAC)base[index]; }
            set { base[index] = value; }
        }
    }

    [Serializable]
    public class PCDCollection : ArrayList
    {
        public PCD Add(PCD obj)
        {
            base.Add(obj);
            return obj;
        }

        public PCD Add()
        {
            return Add(new PCD());
        }

        public void Insert(int index, PCD obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(PCD obj)
        {
            base.Remove(obj);
        }

        new public PCD this[int index]
        {
            get { return (PCD)base[index]; }
            set { base[index] = value; }
        }
    }

    [Serializable]
    public class MEACollection : ArrayList
    {
        public MEA Add(MEA obj)
        {
            base.Add(obj);
            return obj;
        }

        public MEA Add()
        {
            return Add(new MEA());
        }

        public void Insert(int index, MEA obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(MEA obj)
        {
            base.Remove(obj);
        }

        new public MEA this[int index]
        {
            get { return (MEA)base[index]; }
            set { base[index] = value; }
        }
    }

    [Serializable]
    public class ALICollection : ArrayList
    {
        public ALI Add(ALI obj)
        {
            base.Add(obj);
            return obj;
        }

        public ALI Add()
        {
            return Add(new ALI());
        }

        public void Insert(int index, ALI obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(ALI obj)
        {
            base.Remove(obj);
        }

        new public ALI this[int index]
        {
            get { return (ALI)base[index]; }
            set { base[index] = value; }
        }
    }

    [Serializable]
    public class QTYCollection : ArrayList
    {
        public QTY Add(QTY obj)
        {
            base.Add(obj);
            return obj;
        }

        public QTY Add()
        {
            return Add(new QTY());
        }

        public void Insert(int index, QTY obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(QTY obj)
        {
            base.Remove(obj);
        }

        new public QTY this[int index]
        {
            get { return (QTY)base[index]; }
            set { base[index] = value; }
        }
    }

    [Serializable]
    public class RFFCollection : ArrayList
    {
        public RFF Add(RFF obj)
        {
            base.Add(obj);
            return obj;
        }

        public RFF Add()
        {
            return Add(new RFF());
        }

        public void Insert(int index, RFF obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(RFF obj)
        {
            base.Remove(obj);
        }

        new public RFF this[int index]
        {
            get { return (RFF)base[index]; }
            set { base[index] = value; }
        }
    }

    [Serializable]
    public class TODCollection : ArrayList
    {
        public TOD Add(TOD obj)
        {
            base.Add(obj);
            return obj;
        }

        public TOD Add()
        {
            return Add(new TOD());
        }

        public void Insert(int index, TOD obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(TOD obj)
        {
            base.Remove(obj);
        }

        new public TOD this[int index]
        {
            get { return (TOD)base[index]; }
            set { base[index] = value; }
        }
    }

    [Serializable]
    public class CPSCollection : ArrayList
    {
        public CPS Add(CPS obj)
        {
            base.Add(obj);
            return obj;
        }

        public CPS Add()
        {
            return Add(new CPS());
        }

        public void Insert(int index, CPS obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(CPS obj)
        {
            base.Remove(obj);
        }

        new public CPS this[int index]
        {
            get { return (CPS)base[index]; }
            set { base[index] = value; }
        }
    }

    [Serializable]
    public class QVACollection : ArrayList
    {
        public QVA Add(QVA obj)
        {
            base.Add(obj);
            return obj;
        }

        public QVA Add()
        {
            return Add(new QVA());
        }

        public void Insert(int index, QVA obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(QVA obj)
        {
            base.Remove(obj);
        }

        new public QVA this[int index]
        {
            get { return (QVA)base[index]; }
            set { base[index] = value; }
        }
    }

    [Serializable]
    public class ALCCollection : ArrayList
    {
        public ALC Add(ALC obj)
        {
            base.Add(obj);
            return obj;
        }

        public ALC Add()
        {
            return Add(new ALC());
        }

        public void Insert(int index, ALC obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(ALC obj)
        {
            base.Remove(obj);
        }

        new public ALC this[int index]
        {
            get { return (ALC)base[index]; }
            set { base[index] = value; }
        }
    }

    [Serializable]
    public class IMDCollection : ArrayList
    {
        public IMD Add(IMD obj)
        {
            base.Add(obj);
            return obj;
        }

        public IMD Add()
        {
            return Add(new IMD());
        }

        public void Insert(int index, IMD obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(IMD obj)
        {
            base.Remove(obj);
        }

        new public IMD this[int index]
        {
            get { return (IMD)base[index]; }
            set { base[index] = value; }
        }
    }

    [Serializable]
    public class FTXCollection : ArrayList
    {
        public FTX Add(FTX obj)
        {
            base.Add(obj);
            return obj;
        }

        public FTX Add()
        {
            return Add(new FTX());
        }

        public void Insert(int index, FTX obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(FTX obj)
        {
            base.Remove(obj);
        }

        new public FTX this[int index]
        {
            get { return (FTX)base[index]; }
            set { base[index] = value; }
        }
    }

    [Serializable]
    public class MOACollection : ArrayList
    {
        public MOA Add(MOA obj)
        {
            base.Add(obj);
            return obj;
        }

        public MOA Add()
        {
            return Add(new MOA());
        }

        public void Insert(int index, MOA obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(MOA obj)
        {
            base.Remove(obj);
        }

        new public MOA this[int index]
        {
            get { return (MOA)base[index]; }
            set { base[index] = value; }
        }
    }

    [Serializable]
    public class PIACollection : ArrayList
    {
        public PIA Add(PIA obj)
        {
            base.Add(obj);
            return obj;
        }

        public PIA Add()
        {
            return Add(new PIA());
        }

        public void Insert(int index, PIA obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(PIA obj)
        {
            base.Remove(obj);
        }

        new public PIA this[int index]
        {
            get { return (PIA)base[index]; }
            set { base[index] = value; }
        }
    }

    [Serializable]
    public class PATCollection : ArrayList
    {
        public PAT Add(PAT obj)
        {
            base.Add(obj);
            return obj;
        }

        public PAT Add()
        {
            return Add(new PAT());
        }

        public void Insert(int index, PAT obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(PAT obj)
        {
            base.Remove(obj);
        }

        new public PAT this[int index]
        {
            get { return (PAT)base[index]; }
            set { base[index] = value; }
        }
    }

    [Serializable]
    public class GINCollection : ArrayList
    {
        public GIN Add(GIN obj)
        {
            base.Add(obj);
            return obj;
        }

        public GIN Add()
        {
            return Add(new GIN());
        }

        public void Insert(int index, GIN obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(GIN obj)
        {
            base.Remove(obj);
        }

        new public GIN this[int index]
        {
            get { return (GIN)base[index]; }
            set { base[index] = value; }
        }
    }

    [Serializable]
    public class FIICollection : ArrayList
    {
        public FII Add(FII obj)
        {
            base.Add(obj);
            return obj;
        }

        public FII Add()
        {
            return Add(new FII());
        }

        public void Insert(int index, FII obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(FII obj)
        {
            base.Remove(obj);
        }

        new public FII this[int index]
        {
            get { return (FII)base[index]; }
            set { base[index] = value; }
        }
    }

    [Serializable]
    public class NADCollection : ArrayList
    {
        public NAD Add(NAD obj)
        {
            base.Add(obj);
            return obj;
        }

        public NAD Add()
        {
            return Add(new NAD());
        }

        public void Insert(int index, NAD obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(NAD obj)
        {
            base.Remove(obj);
        }

        new public NAD this[int index]
        {
            get { return (NAD)base[index]; }
            set { base[index] = value; }
        }
    }

    [Serializable]
    public class HANCollection : ArrayList
    {
        public HAN Add(HAN obj)
        {
            base.Add(obj);
            return obj;
        }

        public HAN Add()
        {
            return Add(new HAN());
        }

        public void Insert(int index, HAN obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(HAN obj)
        {
            base.Remove(obj);
        }

        new public HAN this[int index]
        {
            get { return (HAN)base[index]; }
            set { base[index] = value; }
        }
    }

    [Serializable]
    public class DTMCollection : ArrayList
    {
        public DTM Add(DTM obj)
        {
            base.Add(obj);
            return obj;
        }

        public DTM Add()
        {
            return Add(new DTM());
        }

        public void Insert(int index, DTM obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(DTM obj)
        {
            base.Remove(obj);
        }

        new public DTM this[int index]
        {
            get { return (DTM)base[index]; }
            set { base[index] = value; }
        }
    }

    [Serializable]
    public class PRICollection : ArrayList
    {
        public PRI Add(PRI obj)
        {
            base.Add(obj);
            return obj;
        }

        public PRI Add()
        {
            return Add(new PRI());
        }

        public void Insert(int index, PRI obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(PRI obj)
        {
            base.Remove(obj);
        }

        new public PRI this[int index]
        {
            get { return (PRI)base[index]; }
            set { base[index] = value; }
        }
    }

    [Serializable]
    public class CTACollection : ArrayList
    {
        public CTA Add(CTA obj)
        {
            base.Add(obj);
            return obj;
        }

        public CTA Add()
        {
            return Add(new CTA());
        }

        public void Insert(int index, CTA obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(CTA obj)
        {
            base.Remove(obj);
        }

        new public CTA this[int index]
        {
            get { return (CTA)base[index]; }
            set { base[index] = value; }
        }
    }

    #endregion


    [XmlRoot(ElementName = "BASETYPES", Namespace = Declarations.SchemaVersion, IsNullable = false), Serializable]
    public class BASETYPES
    {

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

        [XmlElement(Type = typeof(RFF), ElementName = "RFF", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
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

        [XmlElement(Type = typeof(NAD), ElementName = "NAD", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public NADCollection __NADCollection;

        [XmlIgnore]
        public NADCollection NADCollection
        {
            get
            {
                if (__NADCollection == null) __NADCollection = new NADCollection();
                return __NADCollection;
            }
            set { __NADCollection = value; }
        }

        [XmlElement(Type = typeof(FII), ElementName = "FII", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public FIICollection __FIICollection;

        [XmlIgnore]
        public FIICollection FIICollection
        {
            get
            {
                if (__FIICollection == null) __FIICollection = new FIICollection();
                return __FIICollection;
            }
            set { __FIICollection = value; }
        }

        [XmlElement(Type = typeof(CTA), ElementName = "CTA", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
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

        [XmlElement(Type = typeof(TAX), ElementName = "TAX", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
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

        [XmlElement(Type = typeof(MOA), ElementName = "MOA", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public MOACollection __MOACollection;

        [XmlIgnore]
        public MOACollection MOACollection
        {
            get
            {
                if (__MOACollection == null) __MOACollection = new MOACollection();
                return __MOACollection;
            }
            set { __MOACollection = value; }
        }

        [XmlElement(Type = typeof(CUX), ElementName = "CUX", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public CUXCollection __CUXCollection;

        [XmlIgnore]
        public CUXCollection CUXCollection
        {
            get
            {
                if (__CUXCollection == null) __CUXCollection = new CUXCollection();
                return __CUXCollection;
            }
            set { __CUXCollection = value; }
        }

        [XmlElement(Type = typeof(PAT), ElementName = "PAT", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public PATCollection __PATCollection;

        [XmlIgnore]
        public PATCollection PATCollection
        {
            get
            {
                if (__PATCollection == null) __PATCollection = new PATCollection();
                return __PATCollection;
            }
            set { __PATCollection = value; }
        }

        [XmlElement(Type = typeof(PCD), ElementName = "PCD", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public PCDCollection __PCDCollection;

        [XmlIgnore]
        public PCDCollection PCDCollection
        {
            get
            {
                if (__PCDCollection == null) __PCDCollection = new PCDCollection();
                return __PCDCollection;
            }
            set { __PCDCollection = value; }
        }

        [XmlElement(Type = typeof(TOD), ElementName = "TOD", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public TODCollection __TODCollection;

        [XmlIgnore]
        public TODCollection TODCollection
        {
            get
            {
                if (__TODCollection == null) __TODCollection = new TODCollection();
                return __TODCollection;
            }
            set { __TODCollection = value; }
        }

        [XmlElement(Type = typeof(ALC), ElementName = "ALC", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public ALCCollection __ALCCollection;

        [XmlIgnore]
        public ALCCollection ALCCollection
        {
            get
            {
                if (__ALCCollection == null) __ALCCollection = new ALCCollection();
                return __ALCCollection;
            }
            set { __ALCCollection = value; }
        }

        [XmlElement(Type = typeof(QTY), ElementName = "QTY", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
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

        [XmlElement(Type = typeof(CPS), ElementName = "CPS", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public CPSCollection __CPSCollection;

        [XmlIgnore]
        public CPSCollection CPSCollection
        {
            get
            {
                if (__CPSCollection == null) __CPSCollection = new CPSCollection();
                return __CPSCollection;
            }
            set { __CPSCollection = value; }
        }

        [XmlElement(Type = typeof(LIN), ElementName = "LIN", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public LINCollection __LINCollection;

        [XmlIgnore]
        public LINCollection LINCollection
        {
            get
            {
                if (__LINCollection == null) __LINCollection = new LINCollection();
                return __LINCollection;
            }
            set { __LINCollection = value; }
        }

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

        [XmlElement(Type = typeof(QVA), ElementName = "QVA", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public QVACollection __QVACollection;

        [XmlIgnore]
        public QVACollection QVACollection
        {
            get
            {
                if (__QVACollection == null) __QVACollection = new QVACollection();
                return __QVACollection;
            }
            set { __QVACollection = value; }
        }

        [XmlElement(Type = typeof(ALI), ElementName = "ALI", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public ALICollection __ALICollection;

        [XmlIgnore]
        public ALICollection ALICollection
        {
            get
            {
                if (__ALICollection == null) __ALICollection = new ALICollection();
                return __ALICollection;
            }
            set { __ALICollection = value; }
        }

        [XmlElement(Type = typeof(PRI), ElementName = "PRI", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
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

        [XmlElement(Type = typeof(PAC), ElementName = "PAC", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public PACCollection __PACCollection;

        [XmlIgnore]
        public PACCollection PACCollection
        {
            get
            {
                if (__PACCollection == null) __PACCollection = new PACCollection();
                return __PACCollection;
            }
            set { __PACCollection = value; }
        }

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

        [XmlElement(Type = typeof(HAN), ElementName = "HAN", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
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

        [XmlElement(Type = typeof(PCI), ElementName = "PCI", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public PCICollection __PCICollection;

        [XmlIgnore]
        public PCICollection PCICollection
        {
            get
            {
                if (__PCICollection == null) __PCICollection = new PCICollection();
                return __PCICollection;
            }
            set { __PCICollection = value; }
        }

        [XmlElement(Type = typeof(GIN), ElementName = "GIN", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
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

        [XmlElement(Type = typeof(LOC), ElementName = "LOC", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
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

        [XmlElement(Type = typeof(RTE), ElementName = "RTE", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public RTECollection __RTECollection;

        [XmlIgnore]
        public RTECollection RTECollection
        {
            get
            {
                if (__RTECollection == null) __RTECollection = new RTECollection();
                return __RTECollection;
            }
            set { __RTECollection = value; }
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

        public BASETYPES()
        {
        }
    }


    [XmlType(TypeName = "UNH", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class UNH
    {

        [XmlAttribute(AttributeName = "referenceNumber", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __referenceNumber;

        [XmlIgnore]
        public bool __referenceNumberSpecified;

        [XmlIgnore]
        public string referenceNumber
        {
            get { return __referenceNumber; }
            set { __referenceNumber = value; __referenceNumberSpecified = true; }
        }

        [XmlAttribute(AttributeName = "typeIdentifier", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __typeIdentifier;

        [XmlIgnore]
        public string typeIdentifier
        {
            get { return __typeIdentifier; }
            set { __typeIdentifier = value; }
        }

        [XmlAttribute(AttributeName = "versionNumber", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __versionNumber;

        [XmlIgnore]
        public string versionNumber
        {
            get { return __versionNumber; }
            set { __versionNumber = value; }
        }

        [XmlAttribute(AttributeName = "releaseNumber", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __releaseNumber;

        [XmlIgnore]
        public string releaseNumber
        {
            get { return __releaseNumber; }
            set { __releaseNumber = value; }
        }

        [XmlAttribute(AttributeName = "controllingAgency", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __controllingAgency;

        [XmlIgnore]
        public string controllingAgency
        {
            get { return __controllingAgency; }
            set { __controllingAgency = value; }
        }

        [XmlAttribute(AttributeName = "associationAssignedCode", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __associationAssignedCode;

        [XmlIgnore]
        public string associationAssignedCode
        {
            get { return __associationAssignedCode; }
            set { __associationAssignedCode = value; }
        }

        [XmlAttribute(AttributeName = "commonAccessReference", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __commonAccessReference;

        [XmlIgnore]
        public string commonAccessReference
        {
            get { return __commonAccessReference; }
            set { __commonAccessReference = value; }
        }

        [XmlAttribute(AttributeName = "statusOfTransfer", Form = XmlSchemaForm.Unqualified, DataType = "unsignedByte", Namespace = Declarations.SchemaVersion)]
        public byte __statusOfTransfer;

        [XmlIgnore]
        public bool __statusOfTransferSpecified;

        [XmlIgnore]
        public byte statusOfTransfer
        {
            get { return __statusOfTransfer; }
            set { __statusOfTransfer = value; __statusOfTransferSpecified = true; }
        }

        [XmlAttribute(AttributeName = "sequenceOfTransfers", Form = XmlSchemaForm.Unqualified, DataType = "unsignedByte", Namespace = Declarations.SchemaVersion)]
        public byte __sequenceOfTransfers;

        [XmlIgnore]
        public bool __sequenceOfTransfersSpecified;

        [XmlIgnore]
        public byte sequenceOfTransfers
        {
            get { return __sequenceOfTransfers; }
            set { __sequenceOfTransfers = value; __sequenceOfTransfersSpecified = true; }
        }

        [XmlAttribute(AttributeName = "firstLastSequenceTransferIndication", Form = XmlSchemaForm.Unqualified, DataType = "unsignedByte", Namespace = Declarations.SchemaVersion)]
        public byte __firstLastSequenceTransferIndication;

        [XmlIgnore]
        public bool __firstLastSequenceTransferIndicationSpecified;

        [XmlIgnore]
        public byte firstLastSequenceTransferIndication
        {
            get { return __firstLastSequenceTransferIndication; }
            set { __firstLastSequenceTransferIndication = value; __firstLastSequenceTransferIndicationSpecified = true; }
        }

        public UNH()
        {
        }
    }


    [XmlType(TypeName = "BGM", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class BGM
    {

        [XmlAttribute(AttributeName = "messageName", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __messageName;

        [XmlIgnore]
        public bool __messageNameSpecified;

        [XmlIgnore]
        public string messageName
        {
            get { return __messageName; }
            set { __messageName = value; __messageNameSpecified = true; }
        }

        [XmlAttribute(AttributeName = "codeListQualifier", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        public ushort __codeListQualifier;

        [XmlIgnore]
        public bool __codeListQualifierSpecified;

        [XmlIgnore]
        public ushort codeListQualifier
        {
            get { return __codeListQualifier; }
            set { __codeListQualifier = value; __codeListQualifierSpecified = true; }
        }

        [XmlAttribute(AttributeName = "codeListResponsibleAgency", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __codeListResponsibleAgency;

        [XmlIgnore]
        public bool __codeListResponsibleAgencySpecified;

        [XmlIgnore]
        public string codeListResponsibleAgency
        {
            get { return __codeListResponsibleAgency; }
            set { __codeListResponsibleAgency = value; __codeListResponsibleAgencySpecified = true; }
        }

        [XmlAttribute(AttributeName = "messageNumber", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __messageNumber;

        [XmlIgnore]
        public string messageNumber
        {
            get { return __messageNumber; }
            set { __messageNumber = value; }
        }

        [XmlAttribute(AttributeName = "messageFunction", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __messageFunction;

        [XmlIgnore]
        public string messageFunction
        {
            get { return __messageFunction; }
            set { __messageFunction = value; }
        }

        [XmlAttribute(AttributeName = "responseType", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __responseType;

        [XmlIgnore]
        public string responseType
        {
            get { return __responseType; }
            set { __responseType = value; }
        }

        public BGM()
        {
        }
    }


    [XmlType(TypeName = "DTM", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class DTM
    {

        [XmlAttribute(AttributeName = "dateTimePeriodQualifier", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __dateTimePeriodQualifier;

        [XmlIgnore]
        public bool __dateTimePeriodQualifierSpecified;

        [XmlIgnore]
        public string dateTimePeriodQualifier
        {
            get { return __dateTimePeriodQualifier; }
            set { __dateTimePeriodQualifier = value; __dateTimePeriodQualifierSpecified = true; }
        }

        [XmlAttribute(AttributeName = "dateTimePeriod", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __dateTimePeriod;

        [XmlIgnore]
        public string dateTimePeriod
        {
            get { return __dateTimePeriod; }
            set { __dateTimePeriod = value; }
        }

        [XmlAttribute(AttributeName = "dateTimePeriodFormatQualifier", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        public ushort __dateTimePeriodFormatQualifier;

        [XmlIgnore]
        public bool __dateTimePeriodFormatQualifierSpecified;

        [XmlIgnore]
        public ushort dateTimePeriodFormatQualifier
        {
            get { return __dateTimePeriodFormatQualifier; }
            set { __dateTimePeriodFormatQualifier = value; __dateTimePeriodFormatQualifierSpecified = true; }
        }

        public DTM()
        {
        }
    }

    [XmlType(TypeName = "ERC", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class ERC
    {

        [XmlAttribute(AttributeName = "failCode", Form = XmlSchemaForm.Unqualified, DataType = "unsignedByte", Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public byte __failCode;

        [XmlIgnore]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public bool __failCodeSpecified;

        [XmlIgnore]
        public byte failCode
        {
            get { return __failCode; }
            set { __failCode = value; __failCodeSpecified = true; }
        }

        public ERC()
        {
        }

        public ERC(ERC ercObject)
        {
            this.failCode = ((ERC)ercObject).failCode;
        }
    }


    [XmlType(TypeName = "FTX", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class FTX
    {
        [XmlAttribute(AttributeName = "textSubjectQualifier", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __textSubjectQualifier;

        [XmlIgnore]
        public string textSubjectQualifier
        {
            get { return __textSubjectQualifier; }
            set { __textSubjectQualifier = value; }
        }

        [XmlAttribute(AttributeName = "textFunctionCoded", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        public ushort __textFunctionCoded;

        [XmlIgnore]
        public bool __textFunctionCodedSpecified;

        [XmlIgnore]
        public ushort textFunctionCoded
        {
            get { return __textFunctionCoded; }
            set { __textFunctionCoded = value; __textFunctionCodedSpecified = true; }
        }

        [XmlAttribute(AttributeName = "textReference", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __textReference;

        [XmlIgnore]
        public string textReference
        {
            get { return __textReference; }
            set { __textReference = value; }
        }

        [XmlAttribute(AttributeName = "freeTextCoded", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        public ushort __freeTextCoded;

        [XmlIgnore]
        public bool __freeTextCodedSpecified;

        [XmlIgnore]
        public ushort freeTextCoded
        {
            get { return __freeTextCoded; }
            set { __freeTextCoded = value; __freeTextCodedSpecified = true; }
        }

        [XmlAttribute(AttributeName = "codeListQualifier", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        public ushort __codeListQualifier;

        [XmlIgnore]
        public bool __codeListQualifierSpecified;

        [XmlIgnore]
        public ushort codeListQualifier
        {
            get { return __codeListQualifier; }
            set { __codeListQualifier = value; __codeListQualifierSpecified = true; }
        }

        [XmlAttribute(AttributeName = "codeListResponsibleAgency", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        public ushort __codeListResponsibleAgency;

        [XmlIgnore]
        public bool __codeListResponsibleAgencySpecified;

        [XmlIgnore]
        public ushort codeListResponsibleAgency
        {
            get { return __codeListResponsibleAgency; }
            set { __codeListResponsibleAgency = value; __codeListResponsibleAgencySpecified = true; }
        }

        [XmlAttribute(AttributeName = "textLiteral", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __textLiteral;

        [XmlIgnore]
        public string textLiteral
        {
            get { return __textLiteral; }
            set { __textLiteral = value; }
        }

        [XmlAttribute(AttributeName = "languageCoded", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        public ushort __languageCoded;

        [XmlIgnore]
        public bool __languageCodedSpecified;

        [XmlIgnore]
        public ushort languageCoded
        {
            get { return __languageCoded; }
            set { __languageCoded = value; __languageCodedSpecified = true; }
        }

        public FTX()
        {
        }
    }


    [XmlType(TypeName = "RFF", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class RFF
    {

        [XmlAttribute(AttributeName = "referenceQualifier", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __referenceQualifier;

        [XmlIgnore]
        public string referenceQualifier
        {
            get { return __referenceQualifier; }
            set { __referenceQualifier = value; }
        }

        [XmlAttribute(AttributeName = "referenceNumber", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __referenceNumber;

        [XmlIgnore]
        public string referenceNumber
        {
            get { return __referenceNumber; }
            set { __referenceNumber = value; }
        }

        [XmlAttribute(AttributeName = "lineNumber", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __lineNumber;

        [XmlIgnore]
        public string lineNumber
        {
            get { return __lineNumber; }
            set { __lineNumber = value; }
        }

        [XmlAttribute(AttributeName = "referenceVersionNumber", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __referenceVersionNumber;

        [XmlIgnore]
        public string referenceVersionNumber
        {
            get { return __referenceVersionNumber; }
            set { __referenceVersionNumber = value; }
        }

        public RFF()
        {
        }
    }


    [XmlType(TypeName = "NAD", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class NAD
    {

        [XmlAttribute(AttributeName = "partyQualifier", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __partyQualifier;

        [XmlIgnore]
        public string partyQualifier
        {
            get { return __partyQualifier; }
            set { __partyQualifier = value; }
        }

        [XmlAttribute(AttributeName = "partyIDIdentification", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __partyIDIdentification;

        [XmlIgnore]
        public string partyIDIdentification
        {
            get { return __partyIDIdentification; }
            set { __partyIDIdentification = value; }
        }

        [XmlAttribute(AttributeName = "codeListQualifier", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        public ushort __codeListQualifier;

        [XmlIgnore]
        public bool __codeListQualifierSpecified;

        [XmlIgnore]
        public ushort codeListQualifier
        {
            get { return __codeListQualifier; }
            set { __codeListQualifier = value; __codeListQualifierSpecified = true; }
        }

        [XmlAttribute(AttributeName = "codeListResponsibleAgency", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        public ushort __codeListResponsibleAgency;

        [XmlIgnore]
        public bool __codeListResponsibleAgencySpecified;

        [XmlIgnore]
        public ushort codeListResponsibleAgency
        {
            get { return __codeListResponsibleAgency; }
            set { __codeListResponsibleAgency = value; __codeListResponsibleAgencySpecified = true; }
        }

        [XmlAttribute(AttributeName = "nameAndAddress", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __nameAndAddress;

        [XmlIgnore]
        public string nameAndAddress
        {
            get { return __nameAndAddress; }
            set { __nameAndAddress = value; }
        }

        [XmlAttribute(AttributeName = "partyName", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __partyName;

        [XmlIgnore]
        public string partyName
        {
            get { return __partyName; }
            set { __partyName = value; }
        }

        [XmlAttribute(AttributeName = "streetName", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __streetName;

        [XmlIgnore]
        public string streetName
        {
            get { return __streetName; }
            set { __streetName = value; }
        }

        [XmlAttribute(AttributeName = "cityName", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __cityName;

        [XmlIgnore]
        public string cityName
        {
            get { return __cityName; }
            set { __cityName = value; }
        }

        [XmlAttribute(AttributeName = "countrySubEntityID", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __countrySubEntityID;

        [XmlIgnore]
        public string countrySubEntityID
        {
            get { return __countrySubEntityID; }
            set { __countrySubEntityID = value; }
        }

        [XmlAttribute(AttributeName = "postCodeID", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __postCodeID;

        [XmlIgnore]
        public string postCodeID
        {
            get { return __postCodeID; }
            set { __postCodeID = value; }
        }

        [XmlAttribute(AttributeName = "countryCoded", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __countryCoded;

        [XmlIgnore]
        public bool __countryCodedSpecified;

        [XmlIgnore]
        public string countryCoded
        {
            get { return __countryCoded; }
            set { __countryCoded = value; __countryCodedSpecified = true; }
        }

        public NAD()
        {
        }
    }


    [XmlType(TypeName = "FII", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class FII
    {

        [XmlAttribute(AttributeName = "partyQualifier", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __partyQualifier;

        [XmlIgnore]
        public string partyQualifier
        {
            get { return __partyQualifier; }
            set { __partyQualifier = value; }
        }

        [XmlAttribute(AttributeName = "accountNumber", Form = XmlSchemaForm.Unqualified, DataType = "unsignedLong", Namespace = Declarations.SchemaVersion)]
        public ulong __accountNumber;

        [XmlIgnore]
        public bool __accountNumberSpecified;

        [XmlIgnore]
        public ulong accountNumber
        {
            get { return __accountNumber; }
            set { __accountNumber = value; __accountNumberSpecified = true; }
        }

        public FII()
        {
        }
    }


    [XmlType(TypeName = "CTA", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class CTA
    {

        [XmlAttribute(AttributeName = "contactFunctionCoded", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __contactFunctionCoded;

        [XmlIgnore]
        public string contactFunctionCoded
        {
            get { return __contactFunctionCoded; }
            set { __contactFunctionCoded = value; }
        }

        [XmlAttribute(AttributeName = "departmentOrEmployeeIDNumber", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __departmentOrEmployeeIDNumber;

        [XmlIgnore]
        public string departmentOrEmployeeIDNumber
        {
            get { return __departmentOrEmployeeIDNumber; }
            set { __departmentOrEmployeeIDNumber = value; }
        }

        [XmlAttribute(AttributeName = "deptOrEmployee", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __deptOrEmployee;

        [XmlIgnore]
        public string deptOrEmployee
        {
            get { return __deptOrEmployee; }
            set { __deptOrEmployee = value; }
        }

        public CTA()
        {
        }
    }


    [XmlType(TypeName = "TAX", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class TAX
    {

        [XmlAttribute(AttributeName = "dtfFunctionQualifier", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        public ushort __dtfFunctionQualifier;

        [XmlIgnore]
        public bool __dtfFunctionQualifierSpecified;

        [XmlIgnore]
        public ushort dtfFunctionQualifier
        {
            get { return __dtfFunctionQualifier; }
            set { __dtfFunctionQualifier = value; __dtfFunctionQualifierSpecified = true; }
        }

        [XmlAttribute(AttributeName = "dtfTypeCoded", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __dtfTypeCoded;

        [XmlIgnore]
        public string dtfTypeCoded
        {
            get { return __dtfTypeCoded; }
            set { __dtfTypeCoded = value; }
        }

        [XmlAttribute(AttributeName = "c241CodeListQualifier", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        public ushort __c241CodeListQualifier;

        [XmlIgnore]
        public bool __c241CodeListQualifierSpecified;

        [XmlIgnore]
        public ushort c241CodeListQualifier
        {
            get { return __c241CodeListQualifier; }
            set { __c241CodeListQualifier = value; __c241CodeListQualifierSpecified = true; }
        }

        [XmlAttribute(AttributeName = "c241CodeListResponsibleAgency", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        public ushort __c241CodeListResponsibleAgency;

        [XmlIgnore]
        public bool __c241CodeListResponsibleAgencySpecified;

        [XmlIgnore]
        public ushort c241CodeListResponsibleAgency
        {
            get { return __c241CodeListResponsibleAgency; }
            set { __c241CodeListResponsibleAgency = value; __c241CodeListResponsibleAgencySpecified = true; }
        }

        [XmlAttribute(AttributeName = "dtfType", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __dtfType;

        [XmlIgnore]
        public string dtfType
        {
            get { return __dtfType; }
            set { __dtfType = value; }
        }

        [XmlAttribute(AttributeName = "dtfAccountDetail", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __dtfAccountDetail;

        [XmlIgnore]
        public string dtfAccountDetail
        {
            get { return __dtfAccountDetail; }
            set { __dtfAccountDetail = value; }
        }

        [XmlAttribute(AttributeName = "dtfAccountID", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __dtfAccountID;

        [XmlIgnore]
        public string dtfAccountID
        {
            get { return __dtfAccountID; }
            set { __dtfAccountID = value; }
        }

        [XmlAttribute(AttributeName = "c533codeListQualifier", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        public ushort __c533codeListQualifier;

        [XmlIgnore]
        public bool __c533codeListQualifierSpecified;

        [XmlIgnore]
        public ushort c533codeListQualifier
        {
            get { return __c533codeListQualifier; }
            set { __c533codeListQualifier = value; __c533codeListQualifierSpecified = true; }
        }

        [XmlAttribute(AttributeName = "c533CodeListResponsibleAgency", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        public ushort __c533CodeListResponsibleAgency;

        [XmlIgnore]
        public bool __c533CodeListResponsibleAgencySpecified;

        [XmlIgnore]
        public ushort c533CodeListResponsibleAgency
        {
            get { return __c533CodeListResponsibleAgency; }
            set { __c533CodeListResponsibleAgency = value; __c533CodeListResponsibleAgencySpecified = true; }
        }

        [XmlAttribute(AttributeName = "dtfAssessBasis", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __dtfAssessBasis;

        [XmlIgnore]
        public string dtfAssessBasis
        {
            get { return __dtfAssessBasis; }
            set { __dtfAssessBasis = value; }
        }

        [XmlAttribute(AttributeName = "dtfRateID", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __dtfRateID;

        [XmlIgnore]
        public string dtfRateID
        {
            get { return __dtfRateID; }
            set { __dtfRateID = value; }
        }

        [XmlAttribute(AttributeName = "c243CodeListQualifier", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        public ushort __c243CodeListQualifier;

        [XmlIgnore]
        public bool __c243CodeListQualifierSpecified;

        [XmlIgnore]
        public ushort c243CodeListQualifier
        {
            get { return __c243CodeListQualifier; }
            set { __c243CodeListQualifier = value; __c243CodeListQualifierSpecified = true; }
        }

        [XmlAttribute(AttributeName = "c243CodeListResponsibleAgency", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        public ushort __c243CodeListResponsibleAgency;

        [XmlIgnore]
        public bool __c243CodeListResponsibleAgencySpecified;

        [XmlIgnore]
        public ushort c243CodeListResponsibleAgency
        {
            get { return __c243CodeListResponsibleAgency; }
            set { __c243CodeListResponsibleAgency = value; __c243CodeListResponsibleAgencySpecified = true; }
        }

        [XmlAttribute(AttributeName = "dtfRate", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __dtfRate;

        [XmlIgnore]
        public string dtfRate
        {
            get { return __dtfRate; }
            set { __dtfRate = value; }
        }

        [XmlAttribute(AttributeName = "dtfRateBasisID", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __dtfRateBasisID;

        [XmlIgnore]
        public string dtfRateBasisID
        {
            get { return __dtfRateBasisID; }
            set { __dtfRateBasisID = value; }
        }

        [XmlAttribute(AttributeName = "subC243CodeListQualifier", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        public ushort __subC243CodeListQualifier;

        [XmlIgnore]
        public bool __subC243CodeListQualifierSpecified;

        [XmlIgnore]
        public ushort subC243CodeListQualifier
        {
            get { return __subC243CodeListQualifier; }
            set { __subC243CodeListQualifier = value; __subC243CodeListQualifierSpecified = true; }
        }

        [XmlAttribute(AttributeName = "subC243CodeListResponsibleAgency", Form = XmlSchemaForm.Unqualified, DataType = "unsignedByte", Namespace = Declarations.SchemaVersion)]
        public byte __subC243CodeListResponsibleAgency;

        [XmlIgnore]
        public bool __subC243CodeListResponsibleAgencySpecified;

        [XmlIgnore]
        public byte subC243CodeListResponsibleAgency
        {
            get { return __subC243CodeListResponsibleAgency; }
            set { __subC243CodeListResponsibleAgency = value; __subC243CodeListResponsibleAgencySpecified = true; }
        }

        [XmlAttribute(AttributeName = "dtfCategory", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __dtfCategory;

        [XmlIgnore]
        public string dtfCategory
        {
            get { return __dtfCategory; }
            set { __dtfCategory = value; }
        }

        [XmlAttribute(AttributeName = "partyTaxIDNumber", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __partyTaxIDNumber;

        [XmlIgnore]
        public string partyTaxIDNumber
        {
            get { return __partyTaxIDNumber; }
            set { __partyTaxIDNumber = value; }
        }

        public TAX()
        {
        }
    }


    [XmlType(TypeName = "MOA", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class MOA
    {

        [XmlAttribute(AttributeName = "amountTypeQualifier", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __amountTypeQualifier;

        [XmlIgnore]
        public bool __amountTypeQualifierSpecified;

        [XmlIgnore]
        public string amountTypeQualifier
        {
            get { return __amountTypeQualifier; }
            set { __amountTypeQualifier = value; __amountTypeQualifierSpecified = true; }
        }

        [XmlAttribute(AttributeName = "monetaryAmount", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __monetaryAmount;

        [XmlIgnore]
        public bool __monetaryAmountSpecified;

        [XmlIgnore]
        public string monetaryAmount
        {
            get { return __monetaryAmount; }
            set { __monetaryAmount = value; __monetaryAmountSpecified = true; }
        }

        [XmlAttribute(AttributeName = "currencyCoded", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        public ushort __currencyCoded;

        [XmlIgnore]
        public bool __currencyCodedSpecified;

        [XmlIgnore]
        public ushort currencyCoded
        {
            get { return __currencyCoded; }
            set { __currencyCoded = value; __currencyCodedSpecified = true; }
        }

        [XmlAttribute(AttributeName = "currencyQualifier", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        public ushort __currencyQualifier;

        [XmlIgnore]
        public bool __currencyQualifierSpecified;

        [XmlIgnore]
        public ushort currencyQualifier
        {
            get { return __currencyQualifier; }
            set { __currencyQualifier = value; __currencyQualifierSpecified = true; }
        }

        [XmlAttribute(AttributeName = "statusCoded", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        public ushort __statusCoded;

        [XmlIgnore]
        public bool __statusCodedSpecified;

        [XmlIgnore]
        public ushort statusCoded
        {
            get { return __statusCoded; }
            set { __statusCoded = value; __statusCodedSpecified = true; }
        }

        public MOA()
        {
        }
    }


    [XmlType(TypeName = "CUX", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class CUX
    {

        [XmlAttribute(AttributeName = "referenceDetailsQualifier", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        public ushort __referenceDetailsQualifier;

        [XmlIgnore]
        public bool __referenceDetailsQualifierSpecified;

        [XmlIgnore]
        public ushort referenceDetailsQualifier
        {
            get { return __referenceDetailsQualifier; }
            set { __referenceDetailsQualifier = value; __referenceDetailsQualifierSpecified = true; }
        }

        [XmlAttribute(AttributeName = "referenceCoded", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __referenceCoded;

        [XmlIgnore]
        public string referenceCoded
        {
            get { return __referenceCoded; }
            set { __referenceCoded = value; }
        }

        [XmlAttribute(AttributeName = "invoicingQualifier", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        public ushort __invoicingQualifier;

        [XmlIgnore]
        public bool __invoicingQualifierSpecified;

        [XmlIgnore]
        public ushort invoicingQualifier
        {
            get { return __invoicingQualifier; }
            set { __invoicingQualifier = value; __invoicingQualifierSpecified = true; }
        }

        [XmlAttribute(AttributeName = "referenceRateBasis", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __referenceRateBasis;

        [XmlIgnore]
        public string referenceRateBasis
        {
            get { return __referenceRateBasis; }
            set { __referenceRateBasis = value; }
        }

        [XmlAttribute(AttributeName = "targetDetailsQualifier", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        public ushort __targetDetailsQualifier;

        [XmlIgnore]
        public bool __targetDetailsQualifierSpecified;

        [XmlIgnore]
        public ushort targetDetailsQualifier
        {
            get { return __targetDetailsQualifier; }
            set { __targetDetailsQualifier = value; __targetDetailsQualifierSpecified = true; }
        }

        [XmlAttribute(AttributeName = "targetCoded", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __targetCoded;

        [XmlIgnore]
        public bool __targetCodedSpecified;

        [XmlIgnore]
        public string targetCoded
        {
            get { return __targetCoded; }
            set { __targetCoded = value; __targetCodedSpecified = true; }
        }

        [XmlAttribute(AttributeName = "paymentQualifier", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        public ushort __paymentQualifier;

        [XmlIgnore]
        public bool __paymentQualifierSpecified;

        [XmlIgnore]
        public ushort paymentQualifier
        {
            get { return __paymentQualifier; }
            set { __paymentQualifier = value; __paymentQualifierSpecified = true; }
        }

        [XmlAttribute(AttributeName = "targetRateBasis", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __targetRateBasis;

        [XmlIgnore]
        public string targetRateBasis
        {
            get { return __targetRateBasis; }
            set { __targetRateBasis = value; }
        }

        [XmlAttribute(AttributeName = "rateOfExchange", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __rateOfExchange;

        [XmlIgnore]
        public string rateOfExchange
        {
            get { return __rateOfExchange; }
            set { __rateOfExchange = value; }
        }

        [XmlAttribute(AttributeName = "currencyMarketExchange", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __currencyMarketExchange;

        [XmlIgnore]
        public string currencyMarketExchange
        {
            get { return __currencyMarketExchange; }
            set { __currencyMarketExchange = value; }
        }

        public CUX()
        {
        }
    }

    [XmlType(TypeName = "COM", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class COM
    {

        [XmlAttribute(AttributeName = "communicationNumber", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public string __communicationNumber;

        [XmlIgnore]
        public string communicationNumber
        {
            get { return __communicationNumber; }
            set { __communicationNumber = value; }
        }

        [XmlAttribute(AttributeName = "channelQualifier", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public string __channelQualifier;

        [XmlIgnore]
        public string channelQualifier
        {
            get { return __channelQualifier; }
            set { __channelQualifier = value; }
        }

        public COM()
        {
        }
    }

    [Serializable]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public class COMCollection : ArrayList
    {
        public COM Add(COM obj)
        {
            base.Add(obj);
            return obj;
        }

        public COM Add()
        {
            return Add(new COM());
        }

        public void Insert(int index, COM obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(COM obj)
        {
            base.Remove(obj);
        }

        new public COM this[int index]
        {
            get { return (COM)base[index]; }
            set { base[index] = value; }
        }
    }

    [XmlType(TypeName = "PIT", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class PIT
    {

        [XmlAttribute(AttributeName = "lineItemNumber", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public ushort __lineItemNumber;

        [XmlIgnore]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public bool __lineItemNumberSpecified;

        [XmlIgnore]
        public ushort lineItemNumber
        {
            get { return __lineItemNumber; }
            set { __lineItemNumber = value; __lineItemNumberSpecified = true; }
        }

        [XmlAttribute(AttributeName = "actionRequestNotificationCoded", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public ushort __actionRequestNotificationCoded;

        [XmlIgnore]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public bool __actionRequestNotificationCodedSpecified;

        [XmlIgnore]
        public ushort actionRequestNotificationCoded
        {
            get { return __actionRequestNotificationCoded; }
            set { __actionRequestNotificationCoded = value; __actionRequestNotificationCodedSpecified = true; }
        }

        [XmlAttribute(AttributeName = "priceChangeIndicator", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public ushort __priceChangeIndicator;

        [XmlIgnore]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public bool __priceChangeIndicatorSpecified;

        [XmlIgnore]
        public ushort priceChangeIndicator
        {
            get { return __priceChangeIndicator; }
            set { __priceChangeIndicator = value; __priceChangeIndicatorSpecified = true; }
        }

        [XmlAttribute(AttributeName = "codeListQualifier", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public ushort __codeListQualifier;

        [XmlIgnore]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public bool __codeListQualifierSpecified;

        [XmlIgnore]
        public ushort codeListQualifier
        {
            get { return __codeListQualifier; }
            set { __codeListQualifier = value; __codeListQualifierSpecified = true; }
        }

        [XmlAttribute(AttributeName = "codeListResponsibleAgency", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public ushort __codeListResponsibleAgency;

        [XmlIgnore]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public bool __codeListResponsibleAgencySpecified;

        [XmlIgnore]
        public ushort codeListResponsibleAgency
        {
            get { return __codeListResponsibleAgency; }
            set { __codeListResponsibleAgency = value; __codeListResponsibleAgencySpecified = true; }
        }

        [XmlAttribute(AttributeName = "articleAvailabilityCoded", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public ushort __articleAvailabilityCoded;

        [XmlIgnore]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public bool __articleAvailabilityCodedSpecified;

        [XmlIgnore]
        public ushort articleAvailabilityCoded
        {
            get { return __articleAvailabilityCoded; }
            set { __articleAvailabilityCoded = value; __articleAvailabilityCodedSpecified = true; }
        }

        [XmlAttribute(AttributeName = "subLineIndicatorCoded", Form = XmlSchemaForm.Unqualified, DataType = "unsignedByte", Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public byte __subLineIndicatorCoded;

        [XmlIgnore]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public bool __subLineIndicatorCodedSpecified;

        [XmlIgnore]
        public byte subLineIndicatorCoded
        {
            get { return __subLineIndicatorCoded; }
            set { __subLineIndicatorCoded = value; __subLineIndicatorCodedSpecified = true; }
        }

        [XmlAttribute(AttributeName = "configurationLevel", Form = XmlSchemaForm.Unqualified, DataType = "unsignedByte", Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public byte __configurationLevel;

        [XmlIgnore]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public bool __configurationLevelSpecified;

        [XmlIgnore]
        public byte configurationLevel
        {
            get { return __configurationLevel; }
            set { __configurationLevel = value; __configurationLevelSpecified = true; }
        }

        [XmlAttribute(AttributeName = "configurationCoded", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public ushort __configurationCoded;

        [XmlIgnore]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public bool __configurationCodedSpecified;

        [XmlIgnore]
        public ushort configurationCoded
        {
            get { return __configurationCoded; }
            set { __configurationCoded = value; __configurationCodedSpecified = true; }
        }

        public PIT()
        {
        }
    }


    [XmlType(TypeName = "PGI", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class PGI
    {

        [XmlAttribute(AttributeName = "priceTariffTypeCoded", Form = XmlSchemaForm.Unqualified, DataType = "unsignedByte", Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public byte __priceTariffTypeCoded;

        [XmlIgnore]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public bool __priceTariffTypeCodedSpecified;

        [XmlIgnore]
        public byte priceTariffTypeCoded
        {
            get { return __priceTariffTypeCoded; }
            set { __priceTariffTypeCoded = value; __priceTariffTypeCodedSpecified = true; }
        }

        [XmlAttribute(AttributeName = "pricingGroupCoded", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public ushort __pricingGroupCoded;

        [XmlIgnore]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public bool __pricingGroupCodedSpecified;

        [XmlIgnore]
        public ushort pricingGroupCoded
        {
            get { return __pricingGroupCoded; }
            set { __pricingGroupCoded = value; __pricingGroupCodedSpecified = true; }
        }

        [XmlAttribute(AttributeName = "priceGroup", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public string __priceGroup;

        [XmlIgnore]
        public string priceGroup
        {
            get { return __priceGroup; }
            set { __priceGroup = value; }
        }

        public PGI()
        {
        }
    }

    [Serializable]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public class PGICollection : ArrayList
    {
        public PGI Add(PGI obj)
        {
            base.Add(obj);
            return obj;
        }

        public PGI Add()
        {
            return Add(new PGI());
        }

        public void Insert(int index, PGI obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(PGI obj)
        {
            base.Remove(obj);
        }

        new public PGI this[int index]
        {
            get { return (PGI)base[index]; }
            set { base[index] = value; }
        }
    }

    [XmlType(TypeName = "PAT", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class PAT
    {

        [XmlAttribute(AttributeName = "termsTypeQualifier", Form = XmlSchemaForm.Unqualified, DataType = "unsignedByte", Namespace = Declarations.SchemaVersion)]
        public byte __termsTypeQualifier;

        [XmlIgnore]
        public bool __termsTypeQualifierSpecified;

        [XmlIgnore]
        public byte termsTypeQualifier
        {
            get { return __termsTypeQualifier; }
            set { __termsTypeQualifier = value; __termsTypeQualifierSpecified = true; }
        }

        [XmlAttribute(AttributeName = "termsofPaymentID", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __termsofPaymentID;

        [XmlIgnore]
        public string termsofPaymentID
        {
            get { return __termsofPaymentID; }
            set { __termsofPaymentID = value; }
        }

        [XmlAttribute(AttributeName = "codeListQualifier", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        public ushort __codeListQualifier;

        [XmlIgnore]
        public bool __codeListQualifierSpecified;

        [XmlIgnore]
        public ushort codeListQualifier
        {
            get { return __codeListQualifier; }
            set { __codeListQualifier = value; __codeListQualifierSpecified = true; }
        }

        [XmlAttribute(AttributeName = "codeListResponsibleAgency", Form = XmlSchemaForm.Unqualified, DataType = "unsignedByte", Namespace = Declarations.SchemaVersion)]
        public byte __codeListResponsibleAgency;

        [XmlIgnore]
        public bool __codeListResponsibleAgencySpecified;

        [XmlIgnore]
        public byte codeListResponsibleAgency
        {
            get { return __codeListResponsibleAgency; }
            set { __codeListResponsibleAgency = value; __codeListResponsibleAgencySpecified = true; }
        }

        [XmlAttribute(AttributeName = "termOfPayment", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __termOfPayment;

        [XmlIgnore]
        public string termOfPayment
        {
            get { return __termOfPayment; }
            set { __termOfPayment = value; }
        }

        [XmlAttribute(AttributeName = "timeReferenceCoded", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        public ushort __timeReferenceCoded;

        [XmlIgnore]
        public bool __timeReferenceCodedSpecified;

        [XmlIgnore]
        public ushort timeReferenceCoded
        {
            get { return __timeReferenceCoded; }
            set { __timeReferenceCoded = value; __timeReferenceCodedSpecified = true; }
        }

        [XmlAttribute(AttributeName = "timeRelationCoded", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        public ushort __timeRelationCoded;

        [XmlIgnore]
        public bool __timeRelationCodedSpecified;

        [XmlIgnore]
        public ushort timeRelationCoded
        {
            get { return __timeRelationCoded; }
            set { __timeRelationCoded = value; __timeRelationCodedSpecified = true; }
        }

        [XmlAttribute(AttributeName = "typeOfPeriodCoded", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        public ushort __typeOfPeriodCoded;

        [XmlIgnore]
        public bool __typeOfPeriodCodedSpecified;

        [XmlIgnore]
        public ushort typeOfPeriodCoded
        {
            get { return __typeOfPeriodCoded; }
            set { __typeOfPeriodCoded = value; __typeOfPeriodCodedSpecified = true; }
        }

        [XmlAttribute(AttributeName = "numberOfPeriods", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        public ushort __numberOfPeriods;

        [XmlIgnore]
        public bool __numberOfPeriodsSpecified;

        [XmlIgnore]
        public ushort numberOfPeriods
        {
            get { return __numberOfPeriods; }
            set { __numberOfPeriods = value; __numberOfPeriodsSpecified = true; }
        }

        public PAT()
        {
        }
    }


    [XmlType(TypeName = "PCD", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class PCD
    {

        [XmlAttribute(AttributeName = "percentageQualifier", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        public ushort __percentageQualifier;

        [XmlIgnore]
        public bool __percentageQualifierSpecified;

        [XmlIgnore]
        public ushort percentageQualifier
        {
            get { return __percentageQualifier; }
            set { __percentageQualifier = value; __percentageQualifierSpecified = true; }
        }

        [XmlAttribute(AttributeName = "percentage", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __percentage;

        [XmlIgnore]
        public bool __percentageSpecified;

        [XmlIgnore]
        public string percentage
        {
            get { return __percentage; }
            set { __percentage = value; __percentageSpecified = true; }
        }

        [XmlAttribute(AttributeName = "percentageBasisCoded", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        public ushort __percentageBasisCoded;

        [XmlIgnore]
        public bool __percentageBasisCodedSpecified;

        [XmlIgnore]
        public ushort percentageBasisCoded
        {
            get { return __percentageBasisCoded; }
            set { __percentageBasisCoded = value; __percentageBasisCodedSpecified = true; }
        }

        [XmlAttribute(AttributeName = "codeListQualifier", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        public ushort __codeListQualifier;

        [XmlIgnore]
        public bool __codeListQualifierSpecified;

        [XmlIgnore]
        public ushort codeListQualifier
        {
            get { return __codeListQualifier; }
            set { __codeListQualifier = value; __codeListQualifierSpecified = true; }
        }

        [XmlAttribute(AttributeName = "codeListResponsibleAgency", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        public ushort __codeListResponsibleAgency;

        [XmlIgnore]
        public bool __codeListResponsibleAgencySpecified;

        [XmlIgnore]
        public ushort codeListResponsibleAgency
        {
            get { return __codeListResponsibleAgency; }
            set { __codeListResponsibleAgency = value; __codeListResponsibleAgencySpecified = true; }
        }

        public PCD()
        {
        }
    }


    [XmlType(TypeName = "TOD", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class TOD
    {

        [XmlAttribute(AttributeName = "todFunction", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __todFunction;

        [XmlIgnore]
        public bool __todFunctionSpecified;

        [XmlIgnore]
        public string todFunction
        {
            get { return __todFunction; }
            set { __todFunction = value; __todFunctionSpecified = true; }
        }

        [XmlAttribute(AttributeName = "transportMethodOfPayment", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        public ushort __transportMethodOfPayment;

        [XmlIgnore]
        public bool __transportMethodOfPaymentSpecified;

        [XmlIgnore]
        public ushort transportMethodOfPayment
        {
            get { return __transportMethodOfPayment; }
            set { __transportMethodOfPayment = value; __transportMethodOfPaymentSpecified = true; }
        }

        [XmlAttribute(AttributeName = "todCoded", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __todCoded;

        [XmlIgnore]
        public string todCoded
        {
            get { return __todCoded; }
            set { __todCoded = value; }
        }

        [XmlAttribute(AttributeName = "codeListQualifier", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        public ushort __codeListQualifier;

        [XmlIgnore]
        public bool __codeListQualifierSpecified;

        [XmlIgnore]
        public ushort codeListQualifier
        {
            get { return __codeListQualifier; }
            set { __codeListQualifier = value; __codeListQualifierSpecified = true; }
        }

        [XmlAttribute(AttributeName = "codeListResponsibleAgency", Form = XmlSchemaForm.Unqualified, DataType = "unsignedByte", Namespace = Declarations.SchemaVersion)]
        public byte __codeListResponsibleAgency;

        [XmlIgnore]
        public bool __codeListResponsibleAgencySpecified;

        [XmlIgnore]
        public byte codeListResponsibleAgency
        {
            get { return __codeListResponsibleAgency; }
            set { __codeListResponsibleAgency = value; __codeListResponsibleAgencySpecified = true; }
        }

        [XmlAttribute(AttributeName = "termOfDelivery", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __termOfDelivery;

        [XmlIgnore]
        public string termOfDelivery
        {
            get { return __termOfDelivery; }
            set { __termOfDelivery = value; }
        }

        public TOD()
        {
        }
    }


    [XmlType(TypeName = "ALC", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class ALC
    {

        [XmlAttribute(AttributeName = "allowanceChargeQualifier", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __allowanceChargeQualifier;

        [XmlIgnore]
        public string allowanceChargeQualifier
        {
            get { return __allowanceChargeQualifier; }
            set { __allowanceChargeQualifier = value; }
        }

        [XmlAttribute(AttributeName = "allowanceChargeInformation", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __allowanceChargeInformation;

        [XmlIgnore]
        public string allowanceChargeInformation
        {
            get { return __allowanceChargeInformation; }
            set { __allowanceChargeInformation = value; }
        }

        [XmlAttribute(AttributeName = "allowanceOrChargeNumber", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __allowanceOrChargeNumber;

        [XmlIgnore]
        public string allowanceOrChargeNumber
        {
            get { return __allowanceOrChargeNumber; }
            set { __allowanceOrChargeNumber = value; }
        }

        [XmlAttribute(AttributeName = "chargeAllowanceDescription", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __chargeAllowanceDescription;

        [XmlIgnore]
        public string chargeAllowanceDescription
        {
            get { return __chargeAllowanceDescription; }
            set { __chargeAllowanceDescription = value; }
        }

        [XmlAttribute(AttributeName = "settlementCoded", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __settlementCoded;

        [XmlIgnore]
        public string settlementCoded
        {
            get { return __settlementCoded; }
            set { __settlementCoded = value; }
        }

        [XmlAttribute(AttributeName = "calculationSequenceIndicator", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __calculationSequenceIndicator;

        [XmlIgnore]
        public string calculationSequenceIndicator
        {
            get { return __calculationSequenceIndicator; }
            set { __calculationSequenceIndicator = value; }
        }

        [XmlAttribute(AttributeName = "specialServicesID", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __specialServicesID;

        [XmlIgnore]
        public string specialServicesID
        {
            get { return __specialServicesID; }
            set { __specialServicesID = value; }
        }

        [XmlAttribute(AttributeName = "specialServicesCoded", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __specialServicesCoded;

        [XmlIgnore]
        public bool __specialServicesCodedSpecified;

        [XmlIgnore]
        public string specialServicesCoded
        {
            get { return __specialServicesCoded; }
            set { __specialServicesCoded = value; __specialServicesCodedSpecified = true; }
        }

        [XmlAttribute(AttributeName = "codeListQualifier", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        public ushort __codeListQualifier;

        [XmlIgnore]
        public bool __codeListQualifierSpecified;

        [XmlIgnore]
        public ushort codeListQualifier
        {
            get { return __codeListQualifier; }
            set { __codeListQualifier = value; __codeListQualifierSpecified = true; }
        }

        [XmlAttribute(AttributeName = "codeListResponsibleAgency", Form = XmlSchemaForm.Unqualified, DataType = "unsignedByte", Namespace = Declarations.SchemaVersion)]
        public byte __codeListResponsibleAgency;

        [XmlIgnore]
        public bool __codeListResponsibleAgencySpecified;

        [XmlIgnore]
        public byte codeListResponsibleAgency
        {
            get { return __codeListResponsibleAgency; }
            set { __codeListResponsibleAgency = value; __codeListResponsibleAgencySpecified = true; }
        }

        [XmlAttribute(AttributeName = "specialService", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __specialService;

        [XmlIgnore]
        public string specialService
        {
            get { return __specialService; }
            set { __specialService = value; }
        }

        public ALC()
        {
        }
    }


    [XmlType(TypeName = "QTY", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class QTY
    {

        [XmlAttribute(AttributeName = "qtyQualifier", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __qtyQualifier;

        [XmlIgnore]
        public bool __qtyQualifierSpecified;

        [XmlIgnore]
        public string qtyQualifier
        {
            get { return __qtyQualifier; }
            set { __qtyQualifier = value; __qtyQualifierSpecified = true; }
        }

        [XmlAttribute(AttributeName = "quantity", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __quantity;

        [XmlIgnore]
        public bool __quantitySpecified;

        [XmlIgnore]
        public string quantity
        {
            get { return __quantity; }
            set { __quantity = value; __quantitySpecified = true; }
        }

        [XmlAttribute(AttributeName = "measureUnitQualifier", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __measureUnitQualifier;

        [XmlIgnore]
        public string measureUnitQualifier
        {
            get { return __measureUnitQualifier; }
            set { __measureUnitQualifier = value; }
        }

        public QTY()
        {
        }

        public QTY(QTY qtyObject)
        {
            this.measureUnitQualifier = ((QTY)qtyObject).measureUnitQualifier;
            this.qtyQualifier = ((QTY)qtyObject).qtyQualifier;
            this.quantity = ((QTY)qtyObject).quantity;
        }
    }


    [XmlType(TypeName = "CPS", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class CPS
    {

        [XmlAttribute(AttributeName = "hierarchicalIdNumber", Form = XmlSchemaForm.Unqualified, DataType = "unsignedByte", Namespace = Declarations.SchemaVersion)]
        public byte __hierarchicalIdNumber;

        [XmlIgnore]
        public bool __hierarchicalIdNumberSpecified;

        [XmlIgnore]
        public byte hierarchicalIdNumber
        {
            get { return __hierarchicalIdNumber; }
            set { __hierarchicalIdNumber = value; __hierarchicalIdNumberSpecified = true; }
        }

        [XmlAttribute(AttributeName = "hierachticalParentID", Form = XmlSchemaForm.Unqualified, DataType = "unsignedByte", Namespace = Declarations.SchemaVersion)]
        public byte __hierachticalParentID;

        [XmlIgnore]
        public bool __hierachticalParentIDSpecified;

        [XmlIgnore]
        public byte hierachticalParentID
        {
            get { return __hierachticalParentID; }
            set { __hierachticalParentID = value; __hierachticalParentIDSpecified = true; }
        }

        [XmlAttribute(AttributeName = "packagingLevelCoded", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        public ushort __packagingLevelCoded;

        [XmlIgnore]
        public bool __packagingLevelCodedSpecified;

        [XmlIgnore]
        public ushort packagingLevelCoded
        {
            get { return __packagingLevelCoded; }
            set { __packagingLevelCoded = value; __packagingLevelCodedSpecified = true; }
        }

        public CPS()
        {
        }
    }


    [XmlType(TypeName = "LIN", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class LIN
    {

        [XmlAttribute(AttributeName = "lineItemNumber", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        public ushort __lineItemNumber;

        [XmlIgnore]
        public bool __lineItemNumberSpecified;

        [XmlIgnore]
        public ushort lineItemNumber
        {
            get { return __lineItemNumber; }
            set { __lineItemNumber = value; __lineItemNumberSpecified = true; }
        }

        [XmlAttribute(AttributeName = "actionRequest", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        public ushort __actionRequest;

        [XmlIgnore]
        public bool __actionRequestSpecified;

        [XmlIgnore]
        public ushort actionRequest
        {
            get { return __actionRequest; }
            set { __actionRequest = value; __actionRequestSpecified = true; }
        }

        [XmlAttribute(AttributeName = "itemNumberID", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __itemNumberID;

        [XmlIgnore]
        public string itemNumberID
        {
            get { return __itemNumberID; }
            set { __itemNumberID = value; }
        }

        [XmlAttribute(AttributeName = "itemNumber", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __itemNumber;

        [XmlIgnore]
        public string itemNumber
        {
            get { return __itemNumber; }
            set { __itemNumber = value; }
        }

        [XmlAttribute(AttributeName = "itemNumberType", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __itemNumberType;

        [XmlIgnore]
        public string itemNumberType
        {
            get { return __itemNumberType; }
            set { __itemNumberType = value; }
        }

        [XmlAttribute(AttributeName = "codeListQualifier", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        public ushort __codeListQualifier;

        [XmlIgnore]
        public bool __codeListQualifierSpecified;

        [XmlIgnore]
        public ushort codeListQualifier
        {
            get { return __codeListQualifier; }
            set { __codeListQualifier = value; __codeListQualifierSpecified = true; }
        }

        [XmlAttribute(AttributeName = "codeListResponsibleAgency", Form = XmlSchemaForm.Unqualified, DataType = "unsignedByte", Namespace = Declarations.SchemaVersion)]
        public byte __codeListResponsibleAgency;

        [XmlIgnore]
        public bool __codeListResponsibleAgencySpecified;

        [XmlIgnore]
        public byte codeListResponsibleAgency
        {
            get { return __codeListResponsibleAgency; }
            set { __codeListResponsibleAgency = value; __codeListResponsibleAgencySpecified = true; }
        }

        [XmlAttribute(AttributeName = "subLineIndicator", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __subLineIndicator;

        [XmlIgnore]
        public string subLineIndicator
        {
            get { return __subLineIndicator; }
            set { __subLineIndicator = value; }
        }

        [XmlAttribute(AttributeName = "configurationLevel", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __configurationLevel;

        [XmlIgnore]
        public string configurationLevel
        {
            get { return __configurationLevel; }
            set { __configurationLevel = value; }
        }

        [XmlAttribute(AttributeName = "configurationCoded", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        public ushort __configurationCoded;

        [XmlIgnore]
        public bool __configurationCodedSpecified;

        [XmlIgnore]
        public ushort configurationCoded
        {
            get { return __configurationCoded; }
            set { __configurationCoded = value; __configurationCodedSpecified = true; }
        }

        public LIN()
        {
        }
    }


    [XmlType(TypeName = "PIA", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class PIA
    {

        [XmlAttribute(AttributeName = "idFunctionQualifier", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        public ushort __idFunctionQualifier;

        [XmlIgnore]
        public bool __idFunctionQualifierSpecified;

        [XmlIgnore]
        public ushort idFunctionQualifier
        {
            get { return __idFunctionQualifier; }
            set { __idFunctionQualifier = value; __idFunctionQualifierSpecified = true; }
        }

        [XmlAttribute(AttributeName = "itemNumberID", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        public ushort __itemNumberID;

        [XmlIgnore]
        public bool __itemNumberIDSpecified;

        [XmlIgnore]
        public ushort itemNumberID
        {
            get { return __itemNumberID; }
            set { __itemNumberID = value; __itemNumberIDSpecified = true; }
        }

        [XmlAttribute(AttributeName = "itemNumber", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __itemNumber;

        [XmlIgnore]
        public string itemNumber
        {
            get { return __itemNumber; }
            set { __itemNumber = value; }
        }

        [XmlAttribute(AttributeName = "itemNumberType", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __itemNumberType;

        [XmlIgnore]
        public string itemNumberType
        {
            get { return __itemNumberType; }
            set { __itemNumberType = value; }
        }

        [XmlAttribute(AttributeName = "codeListQualifier", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        public ushort __codeListQualifier;

        [XmlIgnore]
        public bool __codeListQualifierSpecified;

        [XmlIgnore]
        public ushort codeListQualifier
        {
            get { return __codeListQualifier; }
            set { __codeListQualifier = value; __codeListQualifierSpecified = true; }
        }

        [XmlAttribute(AttributeName = "codeListResponsAgency", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        public ushort __codeListResponsAgency;

        [XmlIgnore]
        public bool __codeListResponsAgencySpecified;

        [XmlIgnore]
        public ushort codeListResponsAgency
        {
            get { return __codeListResponsAgency; }
            set { __codeListResponsAgency = value; __codeListResponsAgencySpecified = true; }
        }

        public PIA()
        {
        }
    }


    [XmlType(TypeName = "IMD", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class IMD
    {

        [XmlAttribute(AttributeName = "itemDescriptionType", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __itemDescriptionType;

        [XmlIgnore]
        public string itemDescriptionType
        {
            get { return __itemDescriptionType; }
            set { __itemDescriptionType = value; }
        }

        [XmlAttribute(AttributeName = "itemCharacteristics", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __itemCharacteristics;

        [XmlIgnore]
        public bool __itemCharacteristicsSpecified;

        [XmlIgnore]
        public string itemCharacteristics
        {
            get { return __itemCharacteristics; }
            set { __itemCharacteristics = value; __itemCharacteristicsSpecified = true; }
        }

        [XmlAttribute(AttributeName = "itemDescriptionID", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __itemDescriptionID;

        [XmlIgnore]
        public string itemDescriptionID
        {
            get { return __itemDescriptionID; }
            set { __itemDescriptionID = value; }
        }

        [XmlAttribute(AttributeName = "codeListQualifier", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        public ushort __codeListQualifier;

        [XmlIgnore]
        public bool __codeListQualifierSpecified;

        [XmlIgnore]
        public ushort codeListQualifier
        {
            get { return __codeListQualifier; }
            set { __codeListQualifier = value; __codeListQualifierSpecified = true; }
        }

        [XmlAttribute(AttributeName = "codeListResponsAgency", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        public ushort __codeListResponsAgency;

        [XmlIgnore]
        public bool __codeListResponsAgencySpecified;

        [XmlIgnore]
        public ushort codeListResponsAgency
        {
            get { return __codeListResponsAgency; }
            set { __codeListResponsAgency = value; __codeListResponsAgencySpecified = true; }
        }

        [XmlAttribute(AttributeName = "itemDescription", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __itemDescription;

        [XmlIgnore]
        public string itemDescription
        {
            get { return __itemDescription; }
            set { __itemDescription = value; }
        }

        [XmlAttribute(AttributeName = "surfaceLayerIndicator", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        public ushort __surfaceLayerIndicator;

        [XmlIgnore]
        public bool __surfaceLayerIndicatorSpecified;

        [XmlIgnore]
        public ushort surfaceLayerIndicator
        {
            get { return __surfaceLayerIndicator; }
            set { __surfaceLayerIndicator = value; __surfaceLayerIndicatorSpecified = true; }
        }

        public IMD()
        {
        }
    }


    [XmlType(TypeName = "QVA", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class QVA
    {

        [XmlAttribute(AttributeName = "quantityDifferenceInformation", Form = XmlSchemaForm.Unqualified, DataType = "int", Namespace = Declarations.SchemaVersion)]
        public int __quantityDifferenceInformation;

        [XmlIgnore]
        public bool __quantityDifferenceInformationSpecified;

        [XmlIgnore]
        public int quantityDifferenceInformation
        {
            get { return __quantityDifferenceInformation; }
            set { __quantityDifferenceInformation = value; __quantityDifferenceInformationSpecified = true; }
        }

        [XmlAttribute(AttributeName = "quantityQualifier", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        public ushort __quantityQualifier;

        [XmlIgnore]
        public bool __quantityQualifierSpecified;

        [XmlIgnore]
        public ushort quantityQualifier
        {
            get { return __quantityQualifier; }
            set { __quantityQualifier = value; __quantityQualifierSpecified = true; }
        }

        [XmlAttribute(AttributeName = "discrepancyCoded", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __discrepancyCoded;

        [XmlIgnore]
        public string discrepancyCoded
        {
            get { return __discrepancyCoded; }
            set { __discrepancyCoded = value; }
        }

        [XmlAttribute(AttributeName = "changeReasonCoded", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __changeReasonCoded;

        [XmlIgnore]
        public string changeReasonCoded
        {
            get { return __changeReasonCoded; }
            set { __changeReasonCoded = value; }
        }

        [XmlAttribute(AttributeName = "changeReason", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __changeReason;

        [XmlIgnore]
        public string changeReason
        {
            get { return __changeReason; }
            set { __changeReason = value; }
        }

        public QVA()
        {
        }
    }

    [XmlType(TypeName = "QVR", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class QVR
    {

        [XmlAttribute(AttributeName = "qtyDifference", Form = XmlSchemaForm.Unqualified, DataType = "int", Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public int __qtyDifference;

        [XmlIgnore]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public bool __qtyDifferenceSpecified;

        [XmlIgnore]
        public int qtyDifference
        {
            get { return __qtyDifference; }
            set { __qtyDifference = value; __qtyDifferenceSpecified = true; }
        }

        [XmlAttribute(AttributeName = "quantityQualifier", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public ushort __quantityQualifier;

        [XmlIgnore]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public bool __quantityQualifierSpecified;

        [XmlIgnore]
        public ushort quantityQualifier
        {
            get { return __quantityQualifier; }
            set { __quantityQualifier = value; __quantityQualifierSpecified = true; }
        }

        [XmlAttribute(AttributeName = "discrepencyCode", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public string __discrepencyCode;

        [XmlIgnore]
        public string discrepencyCode
        {
            get { return __discrepencyCode; }
            set { __discrepencyCode = value; }
        }

        [XmlAttribute(AttributeName = "changeReason", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public string __changeReason;

        [XmlIgnore]
        public string changeReason
        {
            get { return __changeReason; }
            set { __changeReason = value; }
        }

        [XmlAttribute(AttributeName = "changeText", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public string __changeText;

        [XmlIgnore]
        public string changeText
        {
            get { return __changeText; }
            set { __changeText = value; }
        }

        public QVR()
        {
        }
    }


    [XmlType(TypeName = "ALI", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class ALI
    {

        [XmlAttribute(AttributeName = "countryOfOrigin", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        public ushort __countryOfOrigin;

        [XmlIgnore]
        public bool __countryOfOriginSpecified;

        [XmlIgnore]
        public ushort countryOfOrigin
        {
            get { return __countryOfOrigin; }
            set { __countryOfOrigin = value; __countryOfOriginSpecified = true; }
        }

        [XmlAttribute(AttributeName = "dutyRegimeType", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        public ushort __dutyRegimeType;

        [XmlIgnore]
        public bool __dutyRegimeTypeSpecified;

        [XmlIgnore]
        public ushort dutyRegimeType
        {
            get { return __dutyRegimeType; }
            set { __dutyRegimeType = value; __dutyRegimeTypeSpecified = true; }
        }

        [XmlAttribute(AttributeName = "specialConditions", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        public ushort __specialConditions;

        [XmlIgnore]
        public bool __specialConditionsSpecified;

        [XmlIgnore]
        public ushort specialConditions
        {
            get { return __specialConditions; }
            set { __specialConditions = value; __specialConditionsSpecified = true; }
        }

        public ALI()
        {
        }
    }


    [XmlType(TypeName = "PRI", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class PRI
    {

        [XmlAttribute(AttributeName = "priceQualifier", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __priceQualifier;

        [XmlIgnore]
        public string priceQualifier
        {
            get { return __priceQualifier; }
            set { __priceQualifier = value; }
        }

        [XmlAttribute(AttributeName = "price", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __price;

        [XmlIgnore]
        public string price
        {
            get { return __price; }
            set { __price = value; }
        }

        [XmlAttribute(AttributeName = "priceType", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __priceType;

        [XmlIgnore]
        public bool __priceTypeSpecified;

        [XmlIgnore]
        public string priceType
        {
            get { return __priceType; }
            set { __priceType = value; __priceTypeSpecified = true; }
        }

        [XmlAttribute(AttributeName = "priceTypeQualifier", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __priceTypeQualifier;

        [XmlIgnore]
        public string priceTypeQualifier
        {
            get { return __priceTypeQualifier; }
            set { __priceTypeQualifier = value; }
        }

        [XmlAttribute(AttributeName = "unitPriceBasis", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __unitPriceBasis;

        [XmlIgnore]
        public string unitPriceBasis
        {
            get { return __unitPriceBasis; }
            set { __unitPriceBasis = value; }
        }

        [XmlAttribute(AttributeName = "measureUnitQualifier", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __measureUnitQualifier;

        [XmlIgnore]
        public string measureUnitQualifier
        {
            get { return __measureUnitQualifier; }
            set { __measureUnitQualifier = value; }
        }

        [XmlAttribute(AttributeName = "subLinePriceChange", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __subLinePriceChange;

        [XmlIgnore]
        public bool __subLinePriceChangeSpecified;

        [XmlIgnore]
        public string subLinePriceChange
        {
            get { return __subLinePriceChange; }
            set { __subLinePriceChange = value; __subLinePriceChangeSpecified = true; }
        }

        public PRI()
        {
        }
    }


    [XmlType(TypeName = "PAC", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class PAC
    {

        [XmlAttribute(AttributeName = "numberOfPackages", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        public ushort __numberOfPackages;

        [XmlIgnore]
        public bool __numberOfPackagesSpecified;

        [XmlIgnore]
        public ushort numberOfPackages
        {
            get { return __numberOfPackages; }
            set { __numberOfPackages = value; __numberOfPackagesSpecified = true; }
        }

        [XmlAttribute(AttributeName = "packagingLevel", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        public ushort __packagingLevel;

        [XmlIgnore]
        public bool __packagingLevelSpecified;

        [XmlIgnore]
        public ushort packagingLevel
        {
            get { return __packagingLevel; }
            set { __packagingLevel = value; __packagingLevelSpecified = true; }
        }

        [XmlAttribute(AttributeName = "packagingRelatedInformation", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        public ushort __packagingRelatedInformation;

        [XmlIgnore]
        public bool __packagingRelatedInformationSpecified;

        [XmlIgnore]
        public ushort packagingRelatedInformation
        {
            get { return __packagingRelatedInformation; }
            set { __packagingRelatedInformation = value; __packagingRelatedInformationSpecified = true; }
        }

        [XmlAttribute(AttributeName = "packagingTermsAndConditions", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        public ushort __packagingTermsAndConditions;

        [XmlIgnore]
        public bool __packagingTermsAndConditionsSpecified;

        [XmlIgnore]
        public ushort packagingTermsAndConditions
        {
            get { return __packagingTermsAndConditions; }
            set { __packagingTermsAndConditions = value; __packagingTermsAndConditionsSpecified = true; }
        }

        [XmlAttribute(AttributeName = "packageTypeID", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __packageTypeID;

        [XmlIgnore]
        public string packageTypeID
        {
            get { return __packageTypeID; }
            set { __packageTypeID = value; }
        }

        [XmlAttribute(AttributeName = "codeListQualifier", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        public ushort __codeListQualifier;

        [XmlIgnore]
        public bool __codeListQualifierSpecified;

        [XmlIgnore]
        public ushort codeListQualifier
        {
            get { return __codeListQualifier; }
            set { __codeListQualifier = value; __codeListQualifierSpecified = true; }
        }

        [XmlAttribute(AttributeName = "codeListResponsibleAgency", Form = XmlSchemaForm.Unqualified, DataType = "unsignedByte", Namespace = Declarations.SchemaVersion)]
        public byte __codeListResponsibleAgency;

        [XmlIgnore]
        public bool __codeListResponsibleAgencySpecified;

        [XmlIgnore]
        public byte codeListResponsibleAgency
        {
            get { return __codeListResponsibleAgency; }
            set { __codeListResponsibleAgency = value; __codeListResponsibleAgencySpecified = true; }
        }

        [XmlAttribute(AttributeName = "packageTypeDescription", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __packageTypeDescription;

        [XmlIgnore]
        public string packageTypeDescription
        {
            get { return __packageTypeDescription; }
            set { __packageTypeDescription = value; }
        }

        [XmlAttribute(AttributeName = "itemDescriptionType", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        public ushort __itemDescriptionType;

        [XmlIgnore]
        public bool __itemDescriptionTypeSpecified;

        [XmlIgnore]
        public ushort itemDescriptionType
        {
            get { return __itemDescriptionType; }
            set { __itemDescriptionType = value; __itemDescriptionTypeSpecified = true; }
        }

        [XmlAttribute(AttributeName = "typeOfPackages", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __typeOfPackages;

        [XmlIgnore]
        public string typeOfPackages
        {
            get { return __typeOfPackages; }
            set { __typeOfPackages = value; }
        }

        [XmlAttribute(AttributeName = "itemNumberType", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        public ushort __itemNumberType;

        [XmlIgnore]
        public bool __itemNumberTypeSpecified;

        [XmlIgnore]
        public ushort itemNumberType
        {
            get { return __itemNumberType; }
            set { __itemNumberType = value; __itemNumberTypeSpecified = true; }
        }

        [XmlAttribute(AttributeName = "rtnPackFreightPmtResponsibility", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        public ushort __rtnPackFreightPmtResponsibility;

        [XmlIgnore]
        public bool __rtnPackFreightPmtResponsibilitySpecified;

        [XmlIgnore]
        public ushort rtnPackFreightPmtResponsibility
        {
            get { return __rtnPackFreightPmtResponsibility; }
            set { __rtnPackFreightPmtResponsibility = value; __rtnPackFreightPmtResponsibilitySpecified = true; }
        }

        [XmlAttribute(AttributeName = "rtnPackLoadContents", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        public ushort __rtnPackLoadContents;

        [XmlIgnore]
        public bool __rtnPackLoadContentsSpecified;

        [XmlIgnore]
        public ushort rtnPackLoadContents
        {
            get { return __rtnPackLoadContents; }
            set { __rtnPackLoadContents = value; __rtnPackLoadContentsSpecified = true; }
        }

        public PAC()
        {
        }
    }


    [XmlType(TypeName = "MEA", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class MEA
    {

        [XmlAttribute(AttributeName = "measurementApplicationQualifier", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __measurementApplicationQualifier;

        [XmlIgnore]
        public string measurementApplicationQualifier
        {
            get { return __measurementApplicationQualifier; }
            set { __measurementApplicationQualifier = value; }
        }

        [XmlAttribute(AttributeName = "measurementDimensionCoded", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __measurementDimensionCoded;

        [XmlIgnore]
        public string measurementDimensionCoded
        {
            get { return __measurementDimensionCoded; }
            set { __measurementDimensionCoded = value; }
        }

        [XmlAttribute(AttributeName = "measurementSignificanceCoded", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __measurementSignificanceCoded;

        [XmlIgnore]
        public string measurementSignificanceCoded
        {
            get { return __measurementSignificanceCoded; }
            set { __measurementSignificanceCoded = value; }
        }

        [XmlAttribute(AttributeName = "measurementAttributeCoded", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __measurementAttributeCoded;

        [XmlIgnore]
        public string measurementAttributeCoded
        {
            get { return __measurementAttributeCoded; }
            set { __measurementAttributeCoded = value; }
        }

        [XmlAttribute(AttributeName = "measureUnitQualifier", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __measureUnitQualifier;

        [XmlIgnore]
        public string measureUnitQualifier
        {
            get { return __measureUnitQualifier; }
            set { __measureUnitQualifier = value; }
        }

        [XmlAttribute(AttributeName = "measurementValue", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __measurementValue;

        [XmlIgnore]
        public string measurementValue
        {
            get { return __measurementValue; }
            set { __measurementValue = value; }
        }

        [XmlAttribute(AttributeName = "rangeMinimum", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __rangeMinimum;

        [XmlIgnore]
        public string rangeMinimum
        {
            get { return __rangeMinimum; }
            set { __rangeMinimum = value; }
        }

        [XmlAttribute(AttributeName = "rangeMaximum", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __rangeMaximum;

        [XmlIgnore]
        public string rangeMaximum
        {
            get { return __rangeMaximum; }
            set { __rangeMaximum = value; }
        }

        [XmlAttribute(AttributeName = "surfaceLayerIndicator", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        public ushort __surfaceLayerIndicator;

        [XmlIgnore]
        public bool __surfaceLayerIndicatorSpecified;

        [XmlIgnore]
        public ushort surfaceLayerIndicator
        {
            get { return __surfaceLayerIndicator; }
            set { __surfaceLayerIndicator = value; __surfaceLayerIndicatorSpecified = true; }
        }

        public MEA()
        {
        }
    }


    [XmlType(TypeName = "HAN", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class HAN
    {

        [XmlAttribute(AttributeName = "handlingInstructionsCoded", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __handlingInstructionsCoded;

        [XmlIgnore]
        public string handlingInstructionsCoded
        {
            get { return __handlingInstructionsCoded; }
            set { __handlingInstructionsCoded = value; }
        }

        [XmlAttribute(AttributeName = "codeListQualifier", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        public ushort __codeListQualifier;

        [XmlIgnore]
        public bool __codeListQualifierSpecified;

        [XmlIgnore]
        public ushort codeListQualifier
        {
            get { return __codeListQualifier; }
            set { __codeListQualifier = value; __codeListQualifierSpecified = true; }
        }

        [XmlAttribute(AttributeName = "codeListResponsibleAgency", Form = XmlSchemaForm.Unqualified, DataType = "unsignedByte", Namespace = Declarations.SchemaVersion)]
        public byte __codeListResponsibleAgency;

        [XmlIgnore]
        public bool __codeListResponsibleAgencySpecified;

        [XmlIgnore]
        public byte codeListResponsibleAgency
        {
            get { return __codeListResponsibleAgency; }
            set { __codeListResponsibleAgency = value; __codeListResponsibleAgencySpecified = true; }
        }

        [XmlAttribute(AttributeName = "handlingInstructions", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __handlingInstructions;

        [XmlIgnore]
        public string handlingInstructions
        {
            get { return __handlingInstructions; }
            set { __handlingInstructions = value; }
        }

        [XmlAttribute(AttributeName = "hazMatClassCodeID", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __hazMatClassCodeID;

        [XmlIgnore]
        public string hazMatClassCodeID
        {
            get { return __hazMatClassCodeID; }
            set { __hazMatClassCodeID = value; }
        }

        [XmlAttribute(AttributeName = "hazMatcodeListQualifier", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        public ushort __hazMatcodeListQualifier;

        [XmlIgnore]
        public bool __hazMatcodeListQualifierSpecified;

        [XmlIgnore]
        public ushort hazMatcodeListQualifier
        {
            get { return __hazMatcodeListQualifier; }
            set { __hazMatcodeListQualifier = value; __hazMatcodeListQualifierSpecified = true; }
        }

        [XmlAttribute(AttributeName = "hazMatcodeListResponsibleAgency", Form = XmlSchemaForm.Unqualified, DataType = "unsignedByte", Namespace = Declarations.SchemaVersion)]
        public byte __hazMatcodeListResponsibleAgency;

        [XmlIgnore]
        public bool __hazMatcodeListResponsibleAgencySpecified;

        [XmlIgnore]
        public byte hazMatcodeListResponsibleAgency
        {
            get { return __hazMatcodeListResponsibleAgency; }
            set { __hazMatcodeListResponsibleAgency = value; __hazMatcodeListResponsibleAgencySpecified = true; }
        }

        public HAN()
        {
        }
    }


    [XmlType(TypeName = "PCI", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class PCI
    {

        [XmlAttribute(AttributeName = "markingInstructionsCoded", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __markingInstructionsCoded;

        [XmlIgnore]
        public string markingInstructionsCoded
        {
            get { return __markingInstructionsCoded; }
            set { __markingInstructionsCoded = value; }
        }

        [XmlAttribute(AttributeName = "shippingMarks", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __shippingMarks;

        [XmlIgnore]
        public string shippingMarks
        {
            get { return __shippingMarks; }
            set { __shippingMarks = value; }
        }

        [XmlAttribute(AttributeName = "containerPackageStatus", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        public ushort __containerPackageStatus;

        [XmlIgnore]
        public bool __containerPackageStatusSpecified;

        [XmlIgnore]
        public ushort containerPackageStatus
        {
            get { return __containerPackageStatus; }
            set { __containerPackageStatus = value; __containerPackageStatusSpecified = true; }
        }

        public PCI()
        {
        }
    }


    [XmlType(TypeName = "GIN", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GIN
    {

        [XmlAttribute(AttributeName = "identityNumberQualifier", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __identityNumberQualifier;

        [XmlIgnore]
        public string identityNumberQualifier
        {
            get { return __identityNumberQualifier; }
            set { __identityNumberQualifier = value; }
        }

        [XmlAttribute(AttributeName = "identityNumber", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __identityNumber;

        [XmlIgnore]
        public string identityNumber
        {
            get { return __identityNumber; }
            set { __identityNumber = value; }
        }

        public GIN()
        {
        }
    }


    [XmlType(TypeName = "LOC", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class LOC
    {

        [XmlAttribute(AttributeName = "placeLocationQualifier", Form = XmlSchemaForm.Unqualified, DataType = "unsignedByte", Namespace = Declarations.SchemaVersion)]
        public byte __placeLocationQualifier;

        [XmlIgnore]
        public bool __placeLocationQualifierSpecified;

        [XmlIgnore]
        public byte placeLocationQualifier
        {
            get { return __placeLocationQualifier; }
            set { __placeLocationQualifier = value; __placeLocationQualifierSpecified = true; }
        }

        [XmlAttribute(AttributeName = "placeLocationID", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __placeLocationID;

        [XmlIgnore]
        public string placeLocationID
        {
            get { return __placeLocationID; }
            set { __placeLocationID = value; }
        }

        [XmlAttribute(AttributeName = "codeListQualifier", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        public ushort __codeListQualifier;

        [XmlIgnore]
        public bool __codeListQualifierSpecified;

        [XmlIgnore]
        public ushort codeListQualifier
        {
            get { return __codeListQualifier; }
            set { __codeListQualifier = value; __codeListQualifierSpecified = true; }
        }

        [XmlAttribute(AttributeName = "codeListResponsibleAgency", Form = XmlSchemaForm.Unqualified, DataType = "unsignedByte", Namespace = Declarations.SchemaVersion)]
        public byte __codeListResponsibleAgency;

        [XmlIgnore]
        public bool __codeListResponsibleAgencySpecified;

        [XmlIgnore]
        public byte codeListResponsibleAgency
        {
            get { return __codeListResponsibleAgency; }
            set { __codeListResponsibleAgency = value; __codeListResponsibleAgencySpecified = true; }
        }

        [XmlAttribute(AttributeName = "placeLocation", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __placeLocation;

        [XmlIgnore]
        public string placeLocation
        {
            get { return __placeLocation; }
            set { __placeLocation = value; }
        }

        [XmlAttribute(AttributeName = "relatedPlaceLocationID", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __relatedPlaceLocationID;

        [XmlIgnore]
        public string relatedPlaceLocationID
        {
            get { return __relatedPlaceLocationID; }
            set { __relatedPlaceLocationID = value; }
        }

        [XmlAttribute(AttributeName = "relCodeListQualifier", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        public ushort __relCodeListQualifier;

        [XmlIgnore]
        public bool __relCodeListQualifierSpecified;

        [XmlIgnore]
        public ushort relCodeListQualifier
        {
            get { return __relCodeListQualifier; }
            set { __relCodeListQualifier = value; __relCodeListQualifierSpecified = true; }
        }

        [XmlAttribute(AttributeName = "relCodeListResponsibleAgency", Form = XmlSchemaForm.Unqualified, DataType = "unsignedByte", Namespace = Declarations.SchemaVersion)]
        public byte __relCodeListResponsibleAgency;

        [XmlIgnore]
        public bool __relCodeListResponsibleAgencySpecified;

        [XmlIgnore]
        public byte relCodeListResponsibleAgency
        {
            get { return __relCodeListResponsibleAgency; }
            set { __relCodeListResponsibleAgency = value; __relCodeListResponsibleAgencySpecified = true; }
        }

        [XmlAttribute(AttributeName = "relatedPlaceLocation", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __relatedPlaceLocation;

        [XmlIgnore]
        public string relatedPlaceLocation
        {
            get { return __relatedPlaceLocation; }
            set { __relatedPlaceLocation = value; }
        }

        [XmlAttribute(AttributeName = "relationCoded", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        public ushort __relationCoded;

        [XmlIgnore]
        public bool __relationCodedSpecified;

        [XmlIgnore]
        public ushort relationCoded
        {
            get { return __relationCoded; }
            set { __relationCoded = value; __relationCodedSpecified = true; }
        }

        public LOC()
        {
        }
    }


    [XmlType(TypeName = "RTE", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class RTE
    {

        [XmlAttribute(AttributeName = "rateTypeQualifier", Form = XmlSchemaForm.Unqualified, DataType = "unsignedByte", Namespace = Declarations.SchemaVersion)]
        public byte __rateTypeQualifier;

        [XmlIgnore]
        public bool __rateTypeQualifierSpecified;

        [XmlIgnore]
        public byte rateTypeQualifier
        {
            get { return __rateTypeQualifier; }
            set { __rateTypeQualifier = value; __rateTypeQualifierSpecified = true; }
        }

        [XmlAttribute(AttributeName = "ratePerUnit", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __ratePerUnit;

        [XmlIgnore]
        public string ratePerUnit
        {
            get { return __ratePerUnit; }
            set { __ratePerUnit = value; }
        }

        [XmlAttribute(AttributeName = "unitPriceBasis", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __unitPriceBasis;

        [XmlIgnore]
        public string unitPriceBasis
        {
            get { return __unitPriceBasis; }
            set { __unitPriceBasis = value; }
        }

        [XmlAttribute(AttributeName = "measureUnitQualifier", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        public ushort __measureUnitQualifier;

        [XmlIgnore]
        public bool __measureUnitQualifierSpecified;

        [XmlIgnore]
        public ushort measureUnitQualifier
        {
            get { return __measureUnitQualifier; }
            set { __measureUnitQualifier = value; __measureUnitQualifierSpecified = true; }
        }

        public RTE()
        {
        }
    }


    [XmlType(TypeName = "UNS", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class UNS
    {

        [XmlAttribute(AttributeName = "sectionIdentification", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __sectionIdentification;

        [XmlIgnore]
        public string sectionIdentification
        {
            get { return __sectionIdentification; }
            set { __sectionIdentification = value; }
        }

        public UNS()
        {
        }
    }


    [XmlType(TypeName = "CNT", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class CNT
    {

        [XmlAttribute(AttributeName = "controlQualifier", Form = XmlSchemaForm.Unqualified, DataType = "unsignedByte", Namespace = Declarations.SchemaVersion)]
        public byte __controlQualifier;

        [XmlIgnore]
        public bool __controlQualifierSpecified;

        [XmlIgnore]
        public byte controlQualifier
        {
            get { return __controlQualifier; }
            set { __controlQualifier = value; __controlQualifierSpecified = true; }
        }

        [XmlAttribute(AttributeName = "controlValue", Form = XmlSchemaForm.Unqualified, DataType = "unsignedInt", Namespace = Declarations.SchemaVersion)]
        public uint __controlValue;

        [XmlIgnore]
        public bool __controlValueSpecified;

        [XmlIgnore]
        public uint controlValue
        {
            get { return __controlValue; }
            set { __controlValue = value; __controlValueSpecified = true; }
        }

        [XmlAttribute(AttributeName = "measureUnitQualifier", Form = XmlSchemaForm.Unqualified, DataType = "unsignedShort", Namespace = Declarations.SchemaVersion)]
        public ushort __measureUnitQualifier;

        [XmlIgnore]
        public bool __measureUnitQualifierSpecified;

        [XmlIgnore]
        public ushort measureUnitQualifier
        {
            get { return __measureUnitQualifier; }
            set { __measureUnitQualifier = value; __measureUnitQualifierSpecified = true; }
        }

        public CNT()
        {
        }
    }


    [XmlType(TypeName = "UNT", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class UNT
    {

        [XmlAttribute(AttributeName = "numberOfSegments", Form = XmlSchemaForm.Unqualified, DataType = "unsignedInt", Namespace = Declarations.SchemaVersion)]
        public uint __numberOfSegments;

        [XmlIgnore]
        public bool __numberOfSegmentsSpecified;

        [XmlIgnore]
        public uint numberOfSegments
        {
            get { return __numberOfSegments; }
            set { __numberOfSegments = value; __numberOfSegmentsSpecified = true; }
        }

        [XmlAttribute(AttributeName = "messageReferenceNumber", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        public string __messageReferenceNumber;

        [XmlIgnore]
        public bool __messageReferenceNumberSpecified;

        [XmlIgnore]
        public string messageReferenceNumber
        {
            get { return __messageReferenceNumber; }
            set { __messageReferenceNumber = value; __messageReferenceNumberSpecified = true; }
        }

        public UNT()
        {
        }
    }

    /// <summary>
    ///     <para>
    ///       A collection that stores objects.
    ///    </para>
    /// </summary>
    [Serializable()]
    public class TypeCollection : CollectionBase
    {

        /// <summary>
        ///     <para>
        ///       Initializes a new instance of <see cref='EDIFACT.BASETYPES.TypeCollection'/>.
        ///    </para>
        /// </summary>
        public TypeCollection()
        {
        }

        /// <summary>
        ///     <para>
        ///       Initializes a new instance of <see cref='EDIFACT.BASETYPES.TypeCollection'/> based on another <see cref='EDIFACT.BASETYPES.Type.TypeCollection'/>.
        ///    </para>
        /// </summary>
        /// <param name='value'>
        ///       A <see cref='EDIFACT.BASETYPES.TypeCollection'/> from which the contents are copied
        /// </param>
        public TypeCollection(TypeCollection value)
        {
            this.AddRange(value);
        }

        /// <summary>
        ///     <para>
        ///       Initializes a new instance of <see cref='EDIFACT.BASETYPES.TypeCollection'/> containing any array of <see cref='EDIFACT.BASETYPES.Type.Type'/> objects.
        ///    </para>
        /// </summary>
        /// <param name='value'>
        ///       A array of <see cref='EDIFACT.BASETYPES.Type.Type'/> objects with which to intialize the collection
        /// </param>
        public TypeCollection(Type[] value)
        {
            this.AddRange(value);
        }

        /// <summary>
        /// <para>Represents the entry at the specified index of the <see cref='EDIFACT.BASETYPES.Type.Type'/>.</para>
        /// </summary>
        /// <param name='index'><para>The zero-based index of the entry to locate in the collection.</para></param>
        /// <value>
        ///    <para> The entry at the specified index of the collection.</para>
        /// </value>
        /// <exception cref='System.ArgumentOutOfRangeException'><paramref name='index'/> is outside the valid range of indexes for the collection.</exception>
        public Type this[int index]
        {
            get
            {
                return ((Type)(List[index]));
            }
            set
            {
                List[index] = value;
            }
        }

        /// <summary>
        ///    <para>Adds a <see cref='EDIFACT.BASETYPES.Type.Type'/> with the specified value to the 
        ///    <see cref='EDIFACT.BASETYPES.Type.TypeCollection'/> .</para>
        /// </summary>
        /// <param name='value'>The <see cref='TEDIFACT.BASETYPES.Type.Type'/> to add.</param>
        /// <returns>
        ///    <para>The index at which the new element was inserted.</para>
        /// </returns>
        /// <seealso cref='EDIFACT.BASETYPES.Type.TypeCollection.AddRange'/>
        public int Add(Type value)
        {
            return List.Add(value);
        }

        /// <summary>
        /// <para>Copies the elements of an array to the end of the <see cref='EDIFACT.BASETYPES.Type.TypeCollection'/>.</para>
        /// </summary>
        /// <param name='value'>
        ///    An array of type <see cref='EDIFACT.BASETYPES.Type.Type'/> containing the objects to add to the collection.
        /// </param>
        /// <returns>
        ///   <para>None.</para>
        /// </returns>
        /// <seealso cref='EDIFACT.BASETYPES.Type.TypeCollection.Add'/>
        public void AddRange(Type[] value)
        {
            for (int i = 0; (i < value.Length); i = (i + 1))
            {
                this.Add(value[i]);
            }
        }

        /// <summary>
        ///     <para>
        ///       Adds the contents of another <see cref='EDIFACT.BASETYPES.Type.TypeCollection'/> to the end of the collection.
        ///    </para>
        /// </summary>
        /// <param name='value'>
        ///    A <see cref='EDIFACT.BASETYPES.Type.TypeCollection'/> containing the objects to add to the collection.
        /// </param>
        /// <returns>
        ///   <para>None.</para>
        /// </returns>
        /// <seealso cref='EDIFACT.BASETYPES.Type.TypeCollection.Add'/>
        public void AddRange(TypeCollection value)
        {
            for (int i = 0; (i < value.Count); i = (i + 1))
            {
                this.Add(value[i]);
            }
        }

        /// <summary>
        /// <para>Gets a value indicating whether the 
        ///    <see cref='EDIFACT.BASETYPES.Type.TypeCollection'/> contains the specified <see cref='EDIFACT.BASETYPES.Type.Type'/>.</para>
        /// </summary>
        /// <param name='value'>The <see cref='EDIFACT.BASETYPES.Type.Type'/> to locate.</param>
        /// <returns>
        /// <para><see langword='true'/> if the <see cref='EDIFACT.BASETYPES.Type.Type'/> is contained in the collection; 
        ///   otherwise, <see langword='false'/>.</para>
        /// </returns>
        /// <seealso cref='EDIFACT.BASETYPES.Type.TypeCollection.IndexOf'/>
        public bool Contains(Type value)
        {
            return List.Contains(value);
        }

        /// <summary>
        /// <para>Copies the <see cref='EDIFACT.BASETYPES.Type.TypeCollection'/> values to a one-dimensional <see cref='System.Array'/> instance at the 
        ///    specified index.</para>
        /// </summary>
        /// <param name='array'><para>The one-dimensional <see cref='System.Array'/> that is the destination of the values copied from <see cref='EDIFACT.BASETYPES.Type.TypeCollection'/> .</para></param>
        /// <param name='index'>The index in <paramref name='array'/> where copying begins.</param>
        /// <returns>
        ///   <para>None.</para>
        /// </returns>
        /// <exception cref='System.ArgumentException'><para><paramref name='array'/> is multidimensional.</para> <para>-or-</para> <para>The number of elements in the <see cref='EDIFACT.BASETYPES.Type.TypeCollection'/> is greater than the available space between <paramref name='arrayIndex'/> and the end of <paramref name='array'/>.</para></exception>
        /// <exception cref='System.ArgumentNullException'><paramref name='array'/> is <see langword='null'/>. </exception>
        /// <exception cref='System.ArgumentOutOfRangeException'><paramref name='arrayIndex'/> is less than <paramref name='array'/>'s lowbound. </exception>
        /// <seealso cref='System.Array'/>
        public void CopyTo(Type[] array, int index)
        {
            List.CopyTo(array, index);
        }

        /// <summary>
        ///    <para>Returns the index of a <see cref='EDIFACT.BASETYPES.Type.Type'/> in 
        ///       the <see cref='EDIFACT.BASETYPES.Type.TypeCollection'/> .</para>
        /// </summary>
        /// <param name='value'>The <see cref='EDIFACT.BASETYPES.Type.Type'/> to locate.</param>
        /// <returns>
        /// <para>The index of the <see cref='EDIFACT.BASETYPES.Type.Type'/> of <paramref name='value'/> in the 
        /// <see cref='EDIFACT.BASETYPES.Type.TypeCollection'/>, if found; otherwise, -1.</para>
        /// </returns>
        /// <seealso cref='EDIFACT.BASETYPES.Type.TypeCollection.Contains'/>
        public int IndexOf(Type value)
        {
            return List.IndexOf(value);
        }

        /// <summary>
        /// <para>Inserts a <see cref='EDIFACT.BASETYPES.Type.Type'/> into the <see cref='EDIFACT.BASETYPES.Type.TypeCollection'/> at the specified index.</para>
        /// </summary>
        /// <param name='index'>The zero-based index where <paramref name='value'/> should be inserted.</param>
        /// <param name=' value'>The <see cref='EDIFACT.BASETYPES.Type.Type'/> to insert.</param>
        /// <returns><para>None.</para></returns>
        /// <seealso cref='EDIFACT.BASETYPES.Type.TypeCollection.Add'/>
        public void Insert(int index, Type value)
        {
            List.Insert(index, value);
        }

        /// <summary>
        ///    <para>Returns an enumerator that can iterate through 
        ///       the <see cref='EDIFACT.BASETYPES.Type.TypeCollection'/> .</para>
        /// </summary>
        /// <returns><para>None.</para></returns>
        /// <seealso cref='System.Collections.IEnumerator'/>
        public new TypeEnumerator GetEnumerator()
        {
            return new TypeEnumerator(this);
        }

        /// <summary>
        ///    <para> Removes a specific <see cref='EDIFACT.BASETYPES.Type.Type'/> from the 
        ///    <see cref='EDIFACT.BASETYPES.Type.TypeCollection'/> .</para>
        /// </summary>
        /// <param name='value'>The <see cref='EDIFACT.BASETYPES.Type.Type'/> to remove from the <see cref='EDIFACT.BASETYPES.Type.TypeCollection'/> .</param>
        /// <returns><para>None.</para></returns>
        /// <exception cref='System.ArgumentException'><paramref name='value'/> is not found in the Collection. </exception>
        public void Remove(Type value)
        {
            List.Remove(value);
        }

        public class TypeEnumerator : object, IEnumerator
        {

            private IEnumerator baseEnumerator;

            private IEnumerable temp;

            public TypeEnumerator(TypeCollection mappings)
            {
                this.temp = ((IEnumerable)(mappings));
                this.baseEnumerator = temp.GetEnumerator();
            }

            public Type Current
            {
                get
                {
                    return ((Type)(baseEnumerator.Current));
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    return baseEnumerator.Current;
                }
            }

            public bool MoveNext()
            {
                return baseEnumerator.MoveNext();
            }

            bool IEnumerator.MoveNext()
            {
                return baseEnumerator.MoveNext();
            }

            public void Reset()
            {
                baseEnumerator.Reset();
            }

            void IEnumerator.Reset()
            {
                baseEnumerator.Reset();
            }
        }
    }
}
