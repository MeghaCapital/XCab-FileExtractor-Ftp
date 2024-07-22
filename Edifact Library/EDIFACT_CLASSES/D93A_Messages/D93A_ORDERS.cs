//--------------------------------------------------------------
// <CreatedBy>
//     Anthony R Yates
//	   Feb. 26, 2005
//     Runtime Version: 1.1.4322.2032
//	   
//     It contains D93A ORDERS Classes
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
using System.Xml.Schema;
using System.Xml.Serialization;

namespace EDIFACT.D93A.ORDERS
{
    #region Declaration Schema Version
    public struct Declarations
    {
        public const string SchemaVersion = "http://www.default.com/D93A/orders";
    }
    #endregion

    #region D93A_ORDERS Class
    /// <remarks/>
    [XmlRoot(ElementName = "D93A_ORDERS", Namespace = Declarations.SchemaVersion, IsNullable = false), Serializable]
    public class D93A_ORDERS : IMessage
    {

        [XmlElement(Type = typeof(ORDERS), ElementName = "ORDERS", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
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

        public D93A_ORDERS()
        {
        }

        #region IMessage Members

        public void Add(EDIFACT.SegmentType type, object obj)
        {
            this.ORDERS.Add(type, obj);
        }

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

    /// <summary>
    /// ORDER represents a EDIFACT D93A Order.
    /// </summary>
    /// <remarks/>
    [Serializable]
    [System.Xml.Serialization.XmlRootAttribute("ORDER", Namespace = Declarations.SchemaVersion, IsNullable = false)]
    public class ORDERS
    {
        public ORDERS() { }

        bool LINE_STARTED = false;
        static int CURRENT_ITEM = 0;
        ITEM item = null;

        #region Class Fields
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("UNH")]
        public UNH UNH;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("BGM")]
        public BGM BGM;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("DTM")]
        public DTMCollection DTM = new DTMCollection();

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("GRP1")]
        public GRP1 GRP1 = new GRP1();

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("GRP2")]
        public GRP2 GRP2 = new GRP2();

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("GRP11")]
        public GRP11 GRP11 = new GRP11();

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("GRP25")]
        public GRP25 GRP25 = new GRP25();

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("UNS")]
        public UNS UNS;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("CNT")]
        public CNT CNT;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("UNT")]
        public UNT UNT;

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
                        DTM tempDTM = new DTM();
                        tempDTM = (DTM)obj;

                        if (tempDTM.dateTimePeriodQualifier == "171")
                        {
                            this.GRP1.DTM = tempDTM;
                        }
                        else
                        {
                            this.DTM.Add(tempDTM);
                        }
                        break;
                    }
                case SegmentType.NAD:
                    {
                        this.GRP2.NAD.Add((NAD)obj);
                        break;
                    }
                case SegmentType.RFF:
                    {
                        RFF tempRFF = new RFF();
                        tempRFF = (RFF)obj;

                        if (tempRFF.referenceQualifier == "CT" ||
                            tempRFF.referenceQualifier == "PL")
                        {
                            this.GRP1.RFF = tempRFF;
                        }
                        else if (tempRFF.referenceQualifier == "GN" ||
                                 tempRFF.referenceQualifier == "VA")
                        {
                            this.GRP2.GRP3.RFF = tempRFF;
                        }
                        break;
                    }
                case SegmentType.LIN:
                    {
                        if (LINE_STARTED)
                            throw new Exception("Can not start new LINE item until first previous LINE complete.");
                        item = new ITEM();
                        item.LIN = (LIN)obj;
                        LINE_STARTED = true;
                        break;
                    }
                case SegmentType.PIA:
                    {
                        if (item == null || !LINE_STARTED)
                            throw new Exception("Cannot add PIA to item without new ITEM");
                        item.PIA = (PIA)obj;
                        break;
                    }
                case SegmentType.IMD:
                    {
                        if (item == null || !LINE_STARTED)
                            throw new Exception("Cannot add IMD to item without new ITEM");
                        item.IMD.Add((IMD)obj);
                        break;
                    }
                case SegmentType.QTY:
                    {
                        if (item == null || !LINE_STARTED)
                            throw new Exception("Cannot add QTY to item without new ITEM");
                        item.QTY = (QTY)obj;
                        this.GRP25.ITEM.Add(item);
                        CURRENT_ITEM = this.GRP25.ITEM.Count - 1;
                        LINE_STARTED = false;
                        break;
                    }
                case SegmentType.FTX:
                    {
                        if (this.GRP25.ITEM.Count <= 0) return;
                        this.GRP25.ITEM[CURRENT_ITEM].FTX = (FTX)obj;
                        break;
                    }
                case SegmentType.TOD:
                    {
                        this.GRP11.TOD = (TOD)obj;
                        break;
                    }
                case SegmentType.CTA:
                    {
                        this.GRP2.GRP5.CTA = (CTA)obj;
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

    }
    #endregion

    #region Groups
    /// <remarks/>
    [Serializable]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = Declarations.SchemaVersion)]
    public class GRP1
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("RFF")]
        public RFF RFF;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("DTM")]
        public DTM DTM;
    }

    /// <remarks/>
    [Serializable]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = Declarations.SchemaVersion)]
    public class GRP2
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("NAD")]
        public NADCollection NAD = new NADCollection();

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("GRP3")]
        public GRP3 GRP3 = new GRP3();

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("GRP5")]
        public GRP5 GRP5 = new GRP5();
    }

    /// <remarks/>
    [Serializable]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = Declarations.SchemaVersion)]
    public class GRP3
    {
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("RFF")]
        public RFF RFF;
    }

    /// <remarks/>
    [Serializable]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = Declarations.SchemaVersion)]
    public class GRP5
    {
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("CTA")]
        public CTA CTA;
    }


    /// <remarks/>
    [Serializable]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = Declarations.SchemaVersion)]
    public class GRP11
    {
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("TOD")]
        public TOD TOD;
    }

    /// <remarks/>
    [Serializable]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = Declarations.SchemaVersion)]
    public class GRP25
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ITEM")]
        public ITEMCollection ITEM = new ITEMCollection();
    }

    /// <remarks/>
    [Serializable]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = Declarations.SchemaVersion)]
    public class ITEM
    {
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("LIN")]
        public LIN LIN;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("PIA")]
        public PIA PIA;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("IMD")]
        public IMDCollection IMD = new IMDCollection();

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("QTY")]
        public QTY QTY;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("FTX")]
        public FTX FTX;
    }

    /// <summary>
    /// Summary description for D93A_ITEMCollection.
    /// </summary>
    [Serializable]
    public class ITEMCollection : System.Collections.CollectionBase
    {
        public ITEMCollection()
        {   // TODO: Add constructor logic here
        }

        public int Add(ITEM orderItem)
        {
            return (List.Add(orderItem));
        }
        /// <summary>
        /// Remove decrements the number of fields in the collection.
        /// </summary>
        public void Remove(int index)
        {
            // Check to see if there is a field at the supplied index.
            if (index > Count - 1 || index < 0)
            // If no field exists, a messagebox is shown and the operation 
            // is cancelled.
            {
                return;
                //System.Windows.Forms.MessageBox.Show("Index not valid!");
            }
            else
            {
                List.RemoveAt(index);
            }
        }
        /// <summary>
        /// Item accesses a field in the collection by its index value.
        /// </summary>
        //		public D93A_ITEM Item(int Index)
        //		{
        //			// The appropriate item is retrieved from the List object and
        //			// explicitly cast to the Field type, then returned to the 
        //			// caller.
        //			return (D93A_ITEM) List[Index];
        //		}

        public ITEM this[int index]
        {
            get
            {
                return ((ITEM)List[index]);
            }
            set
            {
                List[index] = value;
            }
        }
    }
    #endregion
}