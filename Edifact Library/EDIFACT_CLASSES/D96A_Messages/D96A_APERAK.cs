//--------------------------------------------------------------
// <CreatedBy>
//     Anthony R Yates
//	   Jul. 08, 2005
//     Runtime Version: 1.1.4322.2032
//	   
//     It contains D96A APERAK Classes
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

namespace EDIFACT.D96A.APERAK
{

    public struct Declarations
    {
        public const string SchemaVersion = "http://www.default.com/D96A/aperak";
    }

    [XmlRoot(ElementName = "D96A_APERAK", Namespace = Declarations.SchemaVersion, IsNullable = false), Serializable]
    public class D96A_APERAK : IMessage
    {

        [XmlElement(Type = typeof(APERAK), ElementName = "APERAK", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public APERAK __APERAK;

        [XmlIgnore]
        public APERAK APERAK
        {
            get
            {
                if (__APERAK == null) __APERAK = new APERAK();
                return __APERAK;
            }
            set { __APERAK = value; }
        }

        public D96A_APERAK()
        {
        }

        ~D96A_APERAK()
        {
            sp = null;
            APERAK = null;
        }

        SegmentProcessor sp = new SegmentProcessor();

        #region Public Methods

        public void PopulateMessage(ref Segment[] segments)
        {
            sp.AddFunction = new EDIFACT.AddSegmentDelegate(this.APERAK.Add);
            sp.ProcessSegments(segments);
        }

        #endregion

    }


    [XmlType(TypeName = "APERAK", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class APERAK
    {
        #region Public Members
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

        public APERAK()
        {
        }

        #region Public Methods

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
                        {   //137
                            this.DTMCollection.Add((DTM)obj);
                        }
                        break;
                    }
                case SegmentType.NAD:
                    {
                        this.GRP2.Add((NAD)obj);
                        break;
                    }
                case SegmentType.RFF:
                    {
                        this.GRP1.Add(new RFFDTM((RFF)obj));
                        break;
                    }
                case SegmentType.ERC:
                    {
                        this.GRP3.ERC = (ERC)obj;
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


    [XmlType(TypeName = "RFF", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class RFFDTM : EDIFACT.BASETYPES.RFF
    {

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

    [XmlType(TypeName = "GRP2", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP2
    {
        [System.Runtime.InteropServices.DispIdAttribute(-4)]
        public IEnumerator GetEnumerator()
        {
            return NADCollection.GetEnumerator();
        }

        public NAD Add(NAD obj)
        {
            return NADCollection.Add(obj);
        }

        [XmlIgnore]
        public NAD this[int index]
        {
            get { return (NAD)NADCollection[index]; }
        }

        [XmlIgnore]
        public int Count
        {
            get { return NADCollection.Count; }
        }

        public void Clear()
        {
            NADCollection.Clear();
        }

        public NAD Remove(int index)
        {
            NAD obj = NADCollection[index];
            NADCollection.Remove(obj);
            return obj;
        }

        public void Remove(object obj)
        {
            NADCollection.Remove(obj);
        }

        [XmlElement(Type = typeof(NAD), ElementName = "NAD", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
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

        public GRP2()
        {
        }
    }

    [XmlType(TypeName = "GRP3", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP3
    {

        [XmlElement(Type = typeof(ERC), ElementName = "ERC", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public ERC __ERC;

        [XmlIgnore]
        public ERC ERC
        {
            get
            {
                if (__ERC == null) __ERC = new ERC();
                return __ERC;
            }
            set { __ERC = value; }
        }

        public GRP3()
        {
        }
    }
}
