//--------------------------------------------------------------
// <CreatedBy>
//     Anthony R Yates
//	   Feb. 26, 2005
//     Runtime Version: 1.1.4322.2032
//	   
//     It contains D93A INVOICE Classes
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
//using Utilities;

namespace EDIFACT.D93A.INVOIC
{

    #region Declarations Schema Version
    public struct Declarations
    {
        public const string SchemaVersion = "http://www.default.com/D93A/invoic";
    }
    #endregion

    #region Class GROUPS

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

        [XmlElement(Type = typeof(NADFII), ElementName = "NAD", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public NADFII __NAD;

        [XmlIgnore]
        public NADFII NAD
        {
            get
            {
                if (__NAD == null) __NAD = new NADFII();
                return __NAD;
            }
            set { __NAD = value; }
        }

        public GRP2()
        {
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
        public GRP3(RFF rffObject)
        {
            this.RFF = rffObject;
        }
    }

    [Serializable]
    public class GRP3Collection : ArrayList
    {
        public GRP3 Add(GRP3 obj)
        {
            base.Add(obj);
            return obj;
        }

        public GRP3 Add()
        {
            return Add(new GRP3());
        }

        public void Insert(int index, GRP3 obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(GRP3 obj)
        {
            base.Remove(obj);
        }

        new public GRP3 this[int index]
        {
            get { return (GRP3)base[index]; }
            set { base[index] = value; }
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

    #endregion

    #region GRP6

    [XmlType(TypeName = "GRP6", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP6
    {

        [XmlElement(Type = typeof(TAXMOA), ElementName = "TAX", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public TAXMOA __TAX;

        [XmlIgnore]
        public TAXMOA TAX
        {
            get
            {
                if (__TAX == null) __TAX = new TAXMOA();
                return __TAX;
            }
            set { __TAX = value; }
        }

        public GRP6()
        {
        }
        public GRP6(TAX taxObject)
        {
            this.TAX.c241CodeListQualifier = taxObject.c241CodeListQualifier;
            this.TAX.c241CodeListResponsibleAgency = taxObject.c241CodeListResponsibleAgency;
            this.TAX.c243CodeListQualifier = taxObject.c243CodeListQualifier;
            this.TAX.c243CodeListResponsibleAgency = taxObject.c243CodeListResponsibleAgency;
            this.TAX.c533codeListQualifier = taxObject.c533codeListQualifier;
            this.TAX.c533CodeListResponsibleAgency = taxObject.c533CodeListResponsibleAgency;
            this.TAX.dtfAccountDetail = taxObject.dtfAccountDetail;
            this.TAX.dtfAccountID = taxObject.dtfAccountID;
            this.TAX.dtfAssessBasis = taxObject.dtfAssessBasis;
            this.TAX.dtfCategory = taxObject.dtfCategory;
            this.TAX.dtfFunctionQualifier = taxObject.dtfFunctionQualifier;
            this.TAX.dtfRate = taxObject.dtfRate;
            this.TAX.dtfRateBasisID = taxObject.dtfRateBasisID;
            this.TAX.dtfRateID = taxObject.dtfRateID;
            this.TAX.dtfType = taxObject.dtfType;
            this.TAX.dtfTypeCoded = taxObject.dtfTypeCoded;
            this.TAX.partyTaxIDNumber = taxObject.partyTaxIDNumber;
            this.TAX.subC243CodeListQualifier = taxObject.subC243CodeListQualifier;
            this.TAX.subC243CodeListResponsibleAgency = taxObject.subC243CodeListResponsibleAgency;
        }
    }

    [Serializable]
    public class GRP6Collection : ArrayList
    {
        public GRP6 Add(GRP6 obj)
        {
            base.Add(obj);
            return obj;
        }

        public GRP6 Add()
        {
            return Add(new GRP6());
        }

        public void Insert(int index, GRP6 obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(GRP6 obj)
        {
            base.Remove(obj);
        }

        new public GRP6 this[int index]
        {
            get { return (GRP6)base[index]; }
            set { base[index] = value; }
        }
    }
    #endregion

    #region GRP7

    [XmlType(TypeName = "GRP7", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP7
    {

        [XmlElement(Type = typeof(CUXDTM), ElementName = "CUX", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public CUXDTM __CUX;

        [XmlIgnore]
        public CUXDTM CUX
        {
            get
            {
                if (__CUX == null) __CUX = new CUXDTM();
                return __CUX;
            }
            set { __CUX = value; }
        }

        public GRP7()
        {
        }
    }

    #endregion

    #region GRP8

    [XmlType(TypeName = "GRP8", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP8
    {

        [XmlElement(Type = typeof(PATDPM), ElementName = "PAT", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public PATDPM __PAT;

        [XmlIgnore]
        public PATDPM PAT
        {
            get
            {
                if (__PAT == null) __PAT = new PATDPM();
                return __PAT;
            }
            set { __PAT = value; }
        }

        public GRP8() { }

        public GRP8(PAT patObject)
        {
            this.PAT.codeListQualifier = patObject.codeListQualifier;
            this.PAT.codeListResponsibleAgency = patObject.codeListResponsibleAgency;
            this.PAT.numberOfPeriods = patObject.numberOfPeriods;
            this.PAT.termOfPayment = patObject.termOfPayment;
            this.PAT.termsofPaymentID = patObject.termsofPaymentID;
            this.PAT.termsTypeQualifier = patObject.termsTypeQualifier;
            this.PAT.timeReferenceCoded = patObject.timeReferenceCoded;
            this.PAT.timeRelationCoded = patObject.timeRelationCoded;
            this.PAT.typeOfPeriodCoded = patObject.typeOfPeriodCoded;
        }
    }

    [Serializable]
    public class GRP8Collection : ArrayList
    {
        public GRP8 Add(GRP8 obj)
        {
            base.Add(obj);
            return obj;
        }

        public GRP8 Add()
        {
            return Add(new GRP8());
        }

        public void Insert(int index, GRP8 obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(GRP8 obj)
        {
            base.Remove(obj);
        }

        new public GRP8 this[int index]
        {
            get { return (GRP8)base[index]; }
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

    #region GRP14

    [XmlType(TypeName = "GRP14", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP14
    {

        [XmlElement(Type = typeof(ALC_QPMT), ElementName = "ALC", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public ALC_QPMT __ALC;

        [XmlIgnore]
        public ALC_QPMT ALC
        {
            get
            {
                if (__ALC == null) __ALC = new ALC_QPMT();
                return __ALC;
            }
            set { __ALC = value; }
        }

        public GRP14(ALC alcObject)
        {
            this.ALC.allowanceChargeInformation = alcObject.allowanceChargeInformation;
            this.ALC.allowanceChargeQualifier = alcObject.allowanceChargeQualifier;
            this.ALC.allowanceOrChargeNumber = alcObject.allowanceOrChargeNumber;
            this.ALC.calculationSequenceIndicator = alcObject.calculationSequenceIndicator;
            this.ALC.chargeAllowanceDescription = alcObject.chargeAllowanceDescription;
            this.ALC.codeListQualifier = alcObject.codeListQualifier;
            this.ALC.codeListResponsibleAgency = alcObject.codeListResponsibleAgency;
            this.ALC.settlementCoded = alcObject.settlementCoded;
            this.ALC.specialService = alcObject.specialService;
            this.ALC.specialServicesCoded = alcObject.specialServicesCoded;
            this.ALC.specialServicesID = alcObject.specialServicesID;
        }
        public GRP14() { }
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

    #region GRP16
    [XmlType(TypeName = "GRP16", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP16
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

        public GRP16()
        {
        }
    }
    #endregion

    #region GRP17

    [XmlType(TypeName = "GRP17", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP17
    {

        [XmlElement(Type = typeof(PCD), ElementName = "PCD", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public PCD __PCD;

        [XmlIgnore]
        public PCD PCD
        {
            get
            {
                if (__PCD == null) __PCD = new PCD();
                return __PCD;
            }
            set { __PCD = value; }
        }

        public GRP17()
        {
        }
    }

    #endregion

    #region GRP18

    [XmlType(TypeName = "GRP18", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP18
    {

        [XmlElement(Type = typeof(MOA), ElementName = "MOA", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public MOA __MOA;

        [XmlIgnore]
        public MOA MOA
        {
            get
            {
                if (__MOA == null) __MOA = new MOA();
                return __MOA;
            }
            set { __MOA = value; }
        }

        public GRP18()
        {
        }

        public GRP18(MOA moaObject)
        {
            this.MOA = moaObject;
        }
    }

    #endregion

    #region GRP20

    [XmlType(TypeName = "GRP20", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP20
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

        public GRP20()
        {
        }
    }

    #endregion

    #region GRP22

    [XmlType(TypeName = "GRP22", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP22
    {

        [XmlElement(Type = typeof(LIN_INV), ElementName = "LIN", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public LIN_INV __LIN;

        [XmlIgnore]
        public LIN_INV LIN
        {
            get
            {
                if (__LIN == null) __LIN = new LIN_INV();
                return __LIN;
            }
            set { __LIN = value; }
        }

        public GRP22()
        {
        }
        public GRP22(LIN linObject)
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
    public class GRP22Collection : ArrayList
    {
        public GRP22 Add(GRP22 obj)
        {
            base.Add(obj);
            return obj;
        }

        public GRP22 Add()
        {
            return Add(new GRP22());
        }

        public void Insert(int index, GRP22 obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(GRP22 obj)
        {
            base.Remove(obj);
        }

        new public GRP22 this[int index]
        {
            get { return (GRP22)base[index]; }
            set { base[index] = value; }
        }
    }

    #endregion

    #region GRP23

    [XmlType(TypeName = "GRP23", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP23
    {
        [XmlElement(Type = typeof(MOA), ElementName = "MOA", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public MOA __MOA;

        [XmlIgnore]
        public MOA MOA
        {
            get
            {
                if (__MOA == null) __MOA = new MOA();
                return __MOA;
            }
            set { __MOA = value; }
        }

        public GRP23()
        {
        }
    }

    [Serializable]
    public class GRP23Collection : ArrayList
    {
        public GRP23 Add(GRP23 obj)
        {
            base.Add(obj);
            return obj;
        }

        public GRP23 Add()
        {
            return Add(new GRP23());
        }

        public void Insert(int index, GRP23 obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(GRP23 obj)
        {
            base.Remove(obj);
        }

        new public GRP23 this[int index]
        {
            get { return (GRP23)base[index]; }
            set { base[index] = value; }
        }
    }

    #endregion

    #region GRP25

    [XmlType(TypeName = "GRP25", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP25
    {
        [XmlElement(Type = typeof(PRI), ElementName = "PRI", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
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

        public GRP25()
        {
        }

        public GRP25(PRI priObject)
        {
            this.PRI = priObject;
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

    #region GRP26

    [XmlType(TypeName = "GRP26", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP26
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

        public GRP26()
        {
        }

        public GRP26(RFF rffObject)
        {
            this.RFF = rffObject;
        }
    }

    [Serializable]
    public class GRP26Collection : ArrayList
    {
        public GRP26 Add(GRP26 obj)
        {
            base.Add(obj);
            return obj;
        }

        public GRP26 Add()
        {
            return Add(new GRP26());
        }

        public void Insert(int index, GRP26 obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(GRP26 obj)
        {
            base.Remove(obj);
        }

        new public GRP26 this[int index]
        {
            get { return (GRP26)base[index]; }
            set { base[index] = value; }
        }
    }
    #endregion

    #region GRP27

    [XmlType(TypeName = "GRP27", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP27
    {
        [XmlElement(Type = typeof(PAC), ElementName = "PAC", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public PAC __PAC;

        [XmlIgnore]
        public PAC PAC
        {
            get
            {
                if (__PAC == null) __PAC = new PAC();
                return __PAC;
            }
            set { __PAC = value; }
        }

        public GRP27()
        {
        }
        public GRP27(PAC pacObject)
        {
            this.PAC = pacObject;
        }
    }

    [Serializable]
    public class GRP27Collection : ArrayList
    {
        public GRP27 Add(GRP27 obj)
        {
            base.Add(obj);
            return obj;
        }

        public GRP27 Add()
        {
            return Add(new GRP27());
        }

        public void Insert(int index, GRP27 obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(GRP27 obj)
        {
            base.Remove(obj);
        }

        new public GRP27 this[int index]
        {
            get { return (GRP27)base[index]; }
            set { base[index] = value; }
        }
    }

    #endregion

    #region GRP29

    [XmlType(TypeName = "GRP29", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP29
    {
        [XmlElement(Type = typeof(LOC), ElementName = "LOC", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
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

        public GRP29()
        {
        }
        public GRP29(LOC locObject)
        {
            this.LOC = locObject;
        }
    }

    [Serializable]
    public class GRP29Collection : ArrayList
    {
        public GRP29 Add(GRP29 obj)
        {
            base.Add(obj);
            return obj;
        }

        public GRP29 Add()
        {
            return Add(new GRP29());
        }

        public void Insert(int index, GRP29 obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(GRP29 obj)
        {
            base.Remove(obj);
        }

        new public GRP29 this[int index]
        {
            get { return (GRP29)base[index]; }
            set { base[index] = value; }
        }
    }

    #endregion

    #region GRP30

    [XmlType(TypeName = "GRP30", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP30
    {
        [XmlElement(Type = typeof(TAXMOA), ElementName = "TAX", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public TAXMOA __TAX;

        [XmlIgnore]
        public TAXMOA TAX
        {
            get
            {
                if (__TAX == null) __TAX = new TAXMOA();
                return __TAX;
            }
            set { __TAX = value; }
        }

        public GRP30()
        {
        }
        public GRP30(TAX taxObject)
        {
            this.TAX.c241CodeListQualifier = taxObject.c241CodeListQualifier;
            this.TAX.dtfFunctionQualifier = taxObject.dtfFunctionQualifier;
            this.TAX.dtfTypeCoded = taxObject.dtfTypeCoded;
            this.TAX.c241CodeListResponsibleAgency = taxObject.c241CodeListResponsibleAgency;
            this.TAX.dtfAssessBasis = taxObject.dtfAssessBasis;
            this.TAX.dtfAccountDetail = taxObject.dtfAccountDetail;
            this.TAX.dtfRate = taxObject.dtfRate;
            this.TAX.dtfRateBasisID = taxObject.dtfRateBasisID;
            this.TAX.dtfCategory = taxObject.dtfCategory;
        }
    }

    [Serializable]
    public class GRP30Collection : ArrayList
    {
        public GRP30 Add(GRP30 obj)
        {
            base.Add(obj);
            return obj;
        }

        public GRP30 Add()
        {
            return Add(new GRP30());
        }

        public void Insert(int index, GRP30 obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(GRP30 obj)
        {
            base.Remove(obj);
        }

        new public GRP30 this[int index]
        {
            get { return (GRP30)base[index]; }
            set { base[index] = value; }
        }
    }

    #endregion

    #region GRP31

    [XmlType(TypeName = "GRP31", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP31
    {
        [XmlElement(Type = typeof(NAD), ElementName = "NAD", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public NAD __NAD;

        [XmlIgnore]
        public NAD NAD
        {
            get
            {
                if (__NAD == null) __NAD = new NAD();
                return __NAD;
            }
            set { __NAD = value; }
        }

        public GRP31()
        {
        }

        public GRP31(NAD nadObject)
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
    public class GRP31Collection : ArrayList
    {
        public GRP31 Add(GRP31 obj)
        {
            base.Add(obj);
            return obj;
        }

        public GRP31 Add()
        {
            return Add(new GRP31());
        }

        public void Insert(int index, GRP31 obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(GRP31 obj)
        {
            base.Remove(obj);
        }

        new public GRP31 this[int index]
        {
            get { return (GRP31)base[index]; }
            set { base[index] = value; }
        }
    }

    #endregion

    #region GRP35

    [XmlType(TypeName = "GRP35", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP35
    {
        [XmlElement(Type = typeof(ALC_GRP35), ElementName = "ALC", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public ALC_GRP35 __ALC;

        [XmlIgnore]
        public ALC_GRP35 ALC
        {
            get
            {
                if (__ALC == null) __ALC = new ALC_GRP35();
                return __ALC;
            }
            set { __ALC = value; }
        }

        public GRP35()
        {
        }
        public GRP35(ALC alcObject)
        {
            this.ALC.allowanceChargeInformation = alcObject.allowanceChargeInformation;
            this.ALC.allowanceChargeQualifier = alcObject.allowanceChargeQualifier;
            this.ALC.allowanceOrChargeNumber = alcObject.allowanceOrChargeNumber;
            this.ALC.calculationSequenceIndicator = alcObject.calculationSequenceIndicator;
            this.ALC.chargeAllowanceDescription = alcObject.chargeAllowanceDescription;
            this.ALC.codeListQualifier = alcObject.codeListQualifier;
            this.ALC.codeListResponsibleAgency = alcObject.codeListResponsibleAgency;
            this.ALC.settlementCoded = alcObject.settlementCoded;
            this.ALC.specialService = alcObject.specialService;
            this.ALC.specialServicesCoded = alcObject.specialServicesCoded;
            this.ALC.specialServicesID = alcObject.specialServicesID;
        }
    }

    [Serializable]
    public class GRP35Collection : ArrayList
    {
        public GRP35 Add(GRP35 obj)
        {
            base.Add(obj);
            return obj;
        }

        public GRP35 Add()
        {
            return Add(new GRP35());
        }

        public void Insert(int index, GRP35 obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(GRP35 obj)
        {
            base.Remove(obj);
        }

        new public GRP35 this[int index]
        {
            get { return (GRP35)base[index]; }
            set { base[index] = value; }
        }
    }


    #endregion

    #region GRP36

    [XmlType(TypeName = "GRP36", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP36
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

        public GRP36()
        {
        }
    }

    #endregion

    #region GRP37

    [XmlType(TypeName = "GRP37", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP37
    {

        [XmlElement(Type = typeof(PCD), ElementName = "PCD", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public PCD __PCD;

        [XmlIgnore]
        public PCD PCD
        {
            get
            {
                if (__PCD == null) __PCD = new PCD();
                return __PCD;
            }
            set { __PCD = value; }
        }

        public GRP37()
        {
        }
    }

    #endregion

    #region GRP38

    [XmlType(TypeName = "GRP38", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP38
    {
        [XmlElement(Type = typeof(MOA), ElementName = "MOA", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public MOA __MOA;

        [XmlIgnore]
        public MOA MOA
        {
            get
            {
                if (__MOA == null) __MOA = new MOA();
                return __MOA;
            }
            set { __MOA = value; }
        }

        public GRP38()
        {
        }

        public GRP38(MOA moaObject)
        {
            this.MOA.amountTypeQualifier = moaObject.amountTypeQualifier;
            this.MOA.currencyCoded = moaObject.currencyCoded;
            this.MOA.currencyQualifier = moaObject.currencyQualifier;
            this.MOA.monetaryAmount = moaObject.monetaryAmount;
            this.MOA.statusCoded = moaObject.statusCoded;
        }
    }

    [Serializable]
    public class GRP38Collection : ArrayList
    {
        public GRP38 Add(GRP38 obj)
        {
            base.Add(obj);
            return obj;
        }

        public GRP38 Add()
        {
            return Add(new GRP38());
        }

        public void Insert(int index, GRP38 obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(GRP38 obj)
        {
            base.Remove(obj);
        }

        new public GRP38 this[int index]
        {
            get { return (GRP38)base[index]; }
            set { base[index] = value; }
        }
    }
    #endregion

    #region GRP39
    [XmlType(TypeName = "GRP39", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP39
    {

        [XmlElement(Type = typeof(RTE), ElementName = "RTE", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public RTE __RTE;

        [XmlIgnore]
        public RTE RTE
        {
            get
            {
                if (__RTE == null) __RTE = new RTE();
                return __RTE;
            }
            set { __RTE = value; }
        }

        public GRP39()
        {
        }
    }

    #endregion

    #region GRP45

    [XmlType(TypeName = "GRP45", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP45
    {
        [XmlElement(Type = typeof(MOA), ElementName = "MOA", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public MOA __MOA;

        [XmlIgnore]
        public MOA MOA
        {
            get
            {
                if (__MOA == null) __MOA = new MOA();
                return __MOA;
            }
            set { __MOA = value; }
        }

        public GRP45()
        {
        }
        public GRP45(MOA moaObject)
        {
            this.MOA = moaObject;
        }
    }

    [Serializable]
    public class GRP45Collection : ArrayList
    {
        public GRP45 Add(GRP45 obj)
        {
            base.Add(obj);
            return obj;
        }

        public GRP45 Add()
        {
            return Add(new GRP45());
        }

        public void Insert(int index, GRP45 obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(GRP45 obj)
        {
            base.Remove(obj);
        }

        new public GRP45 this[int index]
        {
            get { return (GRP45)base[index]; }
            set { base[index] = value; }
        }
    }

    #endregion

    #region GRP47

    [XmlType(TypeName = "GRP47", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP47
    {
        [XmlElement(Type = typeof(TAXMOA), ElementName = "TAX", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public TAXMOA __TAX;

        [XmlIgnore]
        public TAXMOA TAX
        {
            get
            {
                if (__TAX == null) __TAX = new TAXMOA();
                return __TAX;
            }
            set { __TAX = value; }
        }

        public GRP47()
        {
        }
        public GRP47(TAX taxObject)
        {
            this.TAX.c241CodeListQualifier = taxObject.c241CodeListQualifier;
            this.TAX.dtfFunctionQualifier = taxObject.dtfFunctionQualifier;
            this.TAX.dtfTypeCoded = taxObject.dtfTypeCoded;
            this.TAX.c241CodeListResponsibleAgency = taxObject.c241CodeListResponsibleAgency;
            this.TAX.dtfAssessBasis = taxObject.dtfAssessBasis;
            this.TAX.dtfAccountDetail = taxObject.dtfAccountDetail;
            this.TAX.dtfRate = taxObject.dtfRate;
            this.TAX.dtfRateBasisID = taxObject.dtfRateBasisID;
            this.TAX.dtfCategory = taxObject.dtfCategory;
        }
    }

    [Serializable]
    public class GRP47Collection : ArrayList
    {
        public GRP47 Add(GRP47 obj)
        {
            base.Add(obj);
            return obj;
        }

        public GRP47 Add()
        {
            return Add(new GRP47());
        }

        public void Insert(int index, GRP47 obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(GRP47 obj)
        {
            base.Remove(obj);
        }

        new public GRP47 this[int index]
        {
            get { return (GRP47)base[index]; }
            set { base[index] = value; }
        }
    }

    #endregion

    #region GRP48

    [XmlType(TypeName = "GRP48", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP48
    {
        [XmlElement(Type = typeof(ALCMOA), ElementName = "ALC", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public ALCMOA __ALC;

        [XmlIgnore]
        public ALCMOA ALC
        {
            get
            {
                if (__ALC == null) __ALC = new ALCMOA();
                return __ALC;
            }
            set { __ALC = value; }
        }

        public GRP48()
        {
        }

        public GRP48(ALC alcObject)
        {
            this.ALC.allowanceChargeInformation = alcObject.allowanceChargeInformation;
            this.ALC.allowanceChargeQualifier = alcObject.allowanceChargeQualifier;
            this.ALC.allowanceOrChargeNumber = alcObject.allowanceOrChargeNumber;
            this.ALC.calculationSequenceIndicator = alcObject.calculationSequenceIndicator;
            this.ALC.chargeAllowanceDescription = alcObject.chargeAllowanceDescription;
            this.ALC.codeListQualifier = alcObject.codeListQualifier;
            this.ALC.codeListResponsibleAgency = alcObject.codeListResponsibleAgency;
            this.ALC.settlementCoded = alcObject.settlementCoded;
            this.ALC.specialService = alcObject.specialService;
            this.ALC.specialServicesCoded = alcObject.specialServicesCoded;
            this.ALC.specialServicesID = alcObject.specialServicesID;
        }
    }

    [Serializable]
    public class GRP48Collection : ArrayList
    {
        public GRP48 Add(GRP48 obj)
        {
            base.Add(obj);
            return obj;
        }

        public GRP48 Add()
        {
            return Add(new GRP48());
        }

        public void Insert(int index, GRP48 obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(GRP48 obj)
        {
            base.Remove(obj);
        }

        new public GRP48 this[int index]
        {
            get { return (GRP48)base[index]; }
            set { base[index] = value; }
        }
    }

    #endregion

    #endregion

    #region D93A_INVOIC Class

    [XmlRoot(ElementName = "D93A_INVOIC", Namespace = Declarations.SchemaVersion, IsNullable = false), Serializable]
    public class D93A_INVOIC : IMessage
    {

        [XmlElement(Type = typeof(INVOIC), ElementName = "INVOIC", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public INVOIC __INVOIC;

        [XmlIgnore]
        public INVOIC INVOIC
        {
            get
            {
                if (__INVOIC == null) __INVOIC = new INVOIC();
                return __INVOIC;
            }
            set { __INVOIC = value; }
        }

        public D93A_INVOIC()
        {
        }
        #region IMessage Members

        #region PopulateMessage Method

        public void PopulateMessage(ref Segment[] segments)
        {
            try
            {
                SegmentProcessor sp = new SegmentProcessor(new AddSegmentDelegate(this.INVOIC.Add));
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

    #region INVOIC Class

    [XmlType(TypeName = "INVOIC", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class INVOIC
    {
        bool linAccessed, unsAccessed, linFinished;
        SegmentType lastSegmentAccessed;

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

        [XmlElement(Type = typeof(GRP1), ElementName = "GRP1", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
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

        [XmlElement(Type = typeof(GRP6), ElementName = "GRP6", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public GRP6Collection __GRP6Collection;

        [XmlIgnore]
        public GRP6Collection GRP6Collection
        {
            get
            {
                if (__GRP6Collection == null) __GRP6Collection = new GRP6Collection();
                return __GRP6Collection;
            }
            set { __GRP6Collection = value; }
        }

        [XmlElement(Type = typeof(GRP7), ElementName = "GRP7", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public GRP7 __GRP7;

        [XmlIgnore]
        public GRP7 GRP7
        {
            get
            {
                if (__GRP7 == null) __GRP7 = new GRP7();
                return __GRP7;
            }
            set { __GRP7 = value; }
        }

        [XmlElement(Type = typeof(GRP8), ElementName = "GRP8", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public GRP8Collection __GRP8Collection;

        [XmlIgnore]
        public GRP8Collection GRP8Collection
        {
            get
            {
                if (__GRP8Collection == null) __GRP8Collection = new GRP8Collection();
                return __GRP8Collection;
            }
            set { __GRP8Collection = value; }
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


        [XmlElement(Type = typeof(GRP14), ElementName = "GRP14", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
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

        [XmlElement(Type = typeof(GRP22), ElementName = "GRP22", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public GRP22Collection __GRP22Collection;

        [XmlIgnore]
        public GRP22Collection GRP22Collection
        {
            get
            {
                if (__GRP22Collection == null) __GRP22Collection = new GRP22Collection();
                return __GRP22Collection;
            }
            set { __GRP22Collection = value; }
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


        [XmlElement(Type = typeof(GRP45), ElementName = "GRP45", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public GRP45Collection __GRP45Collection;

        [XmlIgnore]
        public GRP45Collection GRP45Collection
        {
            get
            {
                if (__GRP45Collection == null) __GRP45Collection = new GRP45Collection();
                return __GRP45Collection;
            }
            set { __GRP45Collection = value; }
        }

        [XmlElement(Type = typeof(GRP47), ElementName = "GRP47", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public GRP47Collection __GRP47Collection;

        [XmlIgnore]
        public GRP47Collection GRP47Collection
        {
            get
            {
                if (__GRP47Collection == null) __GRP47Collection = new GRP47Collection();
                return __GRP47Collection;
            }
            set { __GRP47Collection = value; }
        }

        [XmlElement(Type = typeof(GRP48), ElementName = "GRP48", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public GRP48Collection __GRP48Collection;

        [XmlIgnore]
        public GRP48Collection GRP48Collection
        {
            get
            {
                if (__GRP48Collection == null) __GRP48Collection = new GRP48Collection();
                return __GRP48Collection;
            }
            set { __GRP48Collection = value; }
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
                        if (((DTM)obj).dateTimePeriodQualifier == "35" || //Delivery Date/Time (Actual)
                            ((DTM)obj).dateTimePeriodQualifier == "137" || //Doc/Msg Date/Time
                            ((DTM)obj).dateTimePeriodQualifier == "263")  //Invoicing Period
                        {
                            this.DTMCollection.Add((DTM)obj);
                            if (((DTM)obj).dateTimePeriodQualifier == "137" && this.GRP1Collection.Count > 0)
                            {
                                this.GRP1Collection[GRP1Collection.Count - 1].RFF.DTM = (DTM)obj;
                            }
                            if (((DTM)obj).dateTimePeriodQualifier == "35" && linAccessed)
                            {
                                this.GRP22Collection[GRP22Collection.Count - 1].LIN.DTM = (DTM)obj;
                            }
                        }
                        else if (((DTM)obj).dateTimePeriodQualifier == "7" ||
                                 ((DTM)obj).dateTimePeriodQualifier == "12" ||
                                 ((DTM)obj).dateTimePeriodQualifier == "13")
                        {
                            int i;
                            if ((i = this.GRP8Collection.Count) > 0)
                                this.GRP8Collection[i - 1].PAT.DTM = (DTM)obj;

                        }
                        else if (((DTM)obj).dateTimePeriodQualifier == "134")
                        { //Rate of Exchange Date
                            this.GRP7.CUX.DTM = (DTM)obj;
                        }
                        break;
                    }
                case SegmentType.FTX:
                    {
                        int i;
                        if (((FTX)obj).textSubjectQualifier == "INV" ||
                            ((FTX)obj).textSubjectQualifier == "PUR")
                        {
                            this.FTXCollection.Add((FTX)obj);
                        }
                        else if ((i = this.GRP22Collection.Count) > 0 &&
                                 ((FTX)obj).textSubjectQualifier == "CHG" || //Change Information
                                 ((FTX)obj).textSubjectQualifier == "ZZZ")       //Mutually Defined
                        {
                            this.GRP22Collection[i - 1].LIN.FTXCollection.Add((FTX)obj);
                        }
                        break;
                    }
                case SegmentType.NAD:
                    {
                        int i;
                        if (((NAD)obj).partyQualifier == "AT" ||
                            ((NAD)obj).partyQualifier == "DP")
                        {

                            if ((i = this.GRP22Collection.Count) > 0)
                            {
                                this.GRP22Collection[i - 1].LIN.GRP31Collection.Add(new GRP31((NAD)obj));
                            }

                            if (((NAD)obj).partyQualifier == "DP")
                            {
                                GRP2 tempGRP2 = new GRP2();
                                NADFII tempNAD = new NADFII((NAD)obj);
                                tempGRP2.NAD = tempNAD;
                                this.GRP2Collection.Add(tempGRP2);
                                tempNAD = null;
                                tempGRP2 = null;
                            }
                        }
                        else
                        {
                            NADFII nadfii = new NADFII((NAD)obj);
                            GRP2 tempGRP2 = new GRP2();

                            tempGRP2.NAD = nadfii;
                            this.GRP2Collection.Add(tempGRP2);

                            nadfii = null;
                            tempGRP2 = null;
                        }
                        if (lastSegmentAccessed != SegmentType.LIN)
                            lastSegmentAccessed = type;
                        break;
                    }
                case SegmentType.FII:
                    {
                        int i;                                          //TODO : Perform Validation
                        if ((i = this.GRP2Collection.Count) > 0)
                        {       //FII.partyQualifier = RB when NAD = DL (Receiving FI)
                            this.GRP2Collection[i - 1].NAD.FII = (FII)obj;//FII.partyQualifier = RH when NAD = SU (Sellers FI)
                        }
                        break;
                    }
                case SegmentType.CTA:
                    {
                        int i;
                        if ((i = this.GRP2Collection.Count) > 0)
                        {
                            this.GRP2Collection[i - 1].NAD.GRP5.CTA = (CTA)obj;
                        }
                        break;
                    }
                case SegmentType.RFF:
                    {
                        int i;
                        if (((RFF)obj).referenceQualifier == "AAK" || //Despatch Advice Number
                            ((RFF)obj).referenceQualifier == "CR" || //Customer Reference Number
                            ((RFF)obj).referenceQualifier == "ON" || //Order Number (Purchase)
                            ((RFF)obj).referenceQualifier == "IV" || //Invoice Number
                            ((RFF)obj).referenceQualifier == "VN")   //Order Number (Vendor)
                        { //Grp1 and most to Grp26
                            this.GRP1Collection.Add(new GRP1((RFF)obj));
                            if ((i = this.GRP22Collection.Count) > 0 && ((RFF)obj).referenceQualifier != "CR")
                            {
                                this.GRP22Collection[i - 1].LIN.GRP26Collection.Add(new GRP26((RFF)obj));
                            }
                        }
                        else if (((RFF)obj).referenceQualifier == "GN" || //Gov't Ref Num
                                 ((RFF)obj).referenceQualifier == "VA" || //VAT Reg Num
                                 ((RFF)obj).referenceQualifier == "PQ" || //Payment Reference
                                 ((RFF)obj).referenceQualifier == "API")  //Additional ID of Parties
                        { //Grp3
                            if ((i = this.GRP2Collection.Count) > 0)
                            {
                                GRP3 tempGRP3 = new GRP3((RFF)obj);
                                this.GRP2Collection[i - 1].NAD.GRP3Collection.Add(tempGRP3);
                                tempGRP3 = null;
                            }
                        }
                        break;
                    }
                case SegmentType.TAX:
                    {
                        int i;
                        if (unsAccessed)
                        {
                            this.GRP47Collection.Add(new GRP47((TAX)obj));
                            break;
                        }
                        if (lastSegmentAccessed == SegmentType.LIN)
                        { //TODO : Test for LIN->NAD
                            if ((i = this.GRP22Collection.Count) > 0)
                            {
                                this.GRP22Collection[i - 1].LIN.GRP30Collection.Add(new GRP30((TAX)obj));
                            }
                            break;
                        }
                        if (lastSegmentAccessed == SegmentType.ALC)
                        {
                            if ((i = this.GRP14Collection.Count) > 0)
                                this.GRP14Collection[i - 1].ALC.GRP20.TAX = (TAX)obj;
                            break;
                        }
                        if ((lastSegmentAccessed == SegmentType.NAD) && (!unsAccessed))
                        {
                            this.GRP6Collection.Add(new GRP6((TAX)obj));
                            break;
                        }
                        break;
                    }
                case SegmentType.MOA:
                    {
                        int i, j;
                        if (((MOA)obj).amountTypeQualifier == "124")
                        {
                            if ((i = this.GRP6Collection.Count) > 0)
                                this.GRP6Collection[i - 1].TAX.MOA = (MOA)obj;
                            if ((i = this.GRP22Collection.Count) > 0 &&
                                (j = this.GRP22Collection[i - 1].LIN.GRP30Collection.Count) > 0)
                                this.GRP22Collection[i - 1].LIN.GRP30Collection[j - 1].TAX.MOA = (MOA)obj;
                            GRP45 tempGRP45 = new GRP45((MOA)obj);
                            this.GRP45Collection.Add(tempGRP45);
                            tempGRP45 = null;
                            break;
                        }
                        else if (((MOA)obj).amountTypeQualifier == "8")
                        {
                            if (unsAccessed)
                            {
                                if ((i = this.GRP48Collection.Count) > 0)
                                {
                                    this.GRP48Collection[i - 1].ALC.MOA = (MOA)obj;
                                }
                            }
                            if (lastSegmentAccessed == SegmentType.ALC)
                            {
                                if ((i = this.GRP14Collection.Count) > 0)
                                {
                                    this.GRP14Collection[i - 1].ALC.GRP18 = new GRP18((MOA)obj);
                                }
                            }
                            else
                            {
                                if ((i = this.GRP22Collection.Count) > 0)
                                {
                                    if ((j = this.GRP22Collection[i - 1].LIN.GRP35Collection.Count) > 0)
                                        this.GRP22Collection[i - 1].LIN.GRP35Collection[j - 1].ALC.GRP38Collection.Add(new GRP38((MOA)obj));
                                }
                            }
                            break;
                        }
                        else if (((MOA)obj).amountTypeQualifier == "21")
                        {
                            if ((i = this.GRP8Collection.Count) > 0)
                                this.GRP8Collection[i - 1].PAT.MOA = (MOA)obj;
                            break;
                        }
                        else if (((MOA)obj).amountTypeQualifier == "66" || ((MOA)obj).amountTypeQualifier == "203")
                        {
                            //TODO : Add logic to calculate Goods Item Total (66) and
                            // line item amount (203)
                            if (!linFinished)
                            {
                                GRP23 grp23 = new GRP23();
                                grp23.MOA = (MOA)obj;
                                if ((i = this.GRP22Collection.Count) > 0)
                                    this.GRP22Collection[i - 1].LIN.GRP23Collection.Add(grp23);
                                grp23 = null;
                            }
                        }
                        else if (((MOA)obj).amountTypeQualifier == "79" || ((MOA)obj).amountTypeQualifier == "176")
                        {
                            if ((i = this.GRP47Collection.Count) > 0)
                                this.GRP47Collection[i - 1].TAX.MOA = (MOA)obj;
                        }
                        else
                        {
                            this.GRP45Collection.Add(new GRP45((MOA)obj));
                        }
                        break;
                    }
                case SegmentType.CUX:
                    {
                        this.GRP7.CUX = new CUXDTM((CUX)obj);
                        break;
                    }
                case SegmentType.PAT:
                    {
                        this.GRP8Collection.Add(new GRP8((PAT)obj));
                        break;
                    }
                case SegmentType.PCD:
                    {
                        int i, j;
                        if (((PCD)obj).percentageQualifier == 12 || ((PCD)obj).percentageQualifier == 15)
                        {
                            if ((i = this.GRP8Collection.Count) > 0)
                                this.GRP8Collection[i - 1].PAT.PCD = (PCD)obj;
                            break;
                        }
                        else
                        {
                            if (lastSegmentAccessed == SegmentType.ALC && (this.GRP14Collection.Count > 0))
                            {
                                this.GRP14Collection[GRP14Collection.Count - 1].ALC.GRP17.PCD = (PCD)obj;
                            }
                            else if (lastSegmentAccessed == SegmentType.LIN)
                            {
                                if ((i = this.GRP22Collection.Count) > 0)
                                    if ((j = GRP22Collection[i - 1].LIN.GRP35Collection.Count) > 0)
                                        GRP22Collection[i - 1].LIN.GRP35Collection[j - 1].ALC.GRP37.PCD = (PCD)obj;
                            }

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
                        linAccessed = true;
                        this.GRP22Collection.Add(new GRP22((LIN)obj));
                        lastSegmentAccessed = SegmentType.LIN;
                        break;
                    }
                case SegmentType.ALC:
                    {
                        int i;

                        if (unsAccessed)
                        {
                            this.GRP48Collection.Add(new GRP48((ALC)obj));
                            break;
                        }
                        else if (lastSegmentAccessed == SegmentType.LIN)
                        {
                            if ((i = this.GRP22Collection.Count) > 0)
                                GRP22Collection[i - 1].LIN.GRP35Collection.Add(new GRP35((ALC)obj));
                            break;
                        }
                        else
                        {
                            GRP14 tempGRP14 = new GRP14();
                            tempGRP14.ALC = new ALC_QPMT((ALC)obj);
                            this.GRP14Collection.Add(tempGRP14);
                            tempGRP14 = null;
                        }
                        lastSegmentAccessed = SegmentType.ALC;
                        break;
                    }
                case SegmentType.RTE:
                    {
                        int i, j;
                        if ((i = this.GRP22Collection.Count) > 0)//  GRP25.LINCollection.Count > 0) 
                        {
                            if ((j = this.GRP22Collection[i - 1].LIN.GRP35Collection.Count) > 0)
                            {
                                this.GRP22Collection[i - 1].LIN.GRP35Collection[j - 1].ALC.GRP39.RTE = (RTE)obj;
                            }
                        }
                        break;
                    }
                case SegmentType.PIA:
                    {
                        int i;
                        if ((i = this.GRP22Collection.Count) > 0)//  GRP25.LINCollection.Count > 0) 
                        {
                            this.GRP22Collection[i - 1].LIN.PIACollection.Add((PIA)obj);
                        }
                        break;
                    }
                case SegmentType.IMD:
                    {
                        int i;
                        if ((i = this.GRP22Collection.Count) > 0)//  GRP25.LINCollection.Count > 0) 
                        {
                            this.GRP22Collection[i - 1].LIN.IMDCollection.Add((IMD)obj);
                        }
                        break;
                    }
                case SegmentType.QTY:
                    {
                        int i, j;
                        if ((i = this.GRP22Collection.Count) > 0)
                        {
                            if (((QTY)obj).qtyQualifier == "21" || ((QTY)obj).qtyQualifier == "46" ||
                                ((QTY)obj).qtyQualifier == "47" || ((QTY)obj).qtyQualifier == "59")
                            {
                                this.GRP22Collection[i - 1].LIN.QTYCollection.Add((QTY)obj);
                            }
                            else if (((QTY)obj).qtyQualifier == "1")
                            {
                                if ((j = this.GRP22Collection[i - 1].LIN.GRP35Collection.Count) > 0)
                                    this.GRP22Collection[i - 1].LIN.GRP35Collection[j - 1].ALC.GRP36.QTY = (QTY)obj;
                            }
                        }
                        break;
                    }
                case SegmentType.ALI:
                    {
                        int i;
                        if ((i = this.GRP22Collection.Count) > 0)
                        {
                            this.GRP22Collection[i - 1].LIN.ALI = (ALI)obj;
                        }
                        break;
                    }
                case SegmentType.PRI:
                    {
                        int i;
                        if ((i = this.GRP22Collection.Count) > 0)
                        {
                            this.GRP22Collection[i - 1].LIN.GRP25Collection.Add(new GRP25((PRI)obj));
                        }
                        break;
                    }
                case SegmentType.PAC:
                    {
                        int i;
                        if ((i = this.GRP22Collection.Count) > 0)
                        {
                            this.GRP22Collection[i - 1].LIN.GRP27Collection.Add(new GRP27((PAC)obj));
                        }
                        break;
                    }
                case SegmentType.LOC:
                    {
                        int i;
                        if ((i = this.GRP22Collection.Count) > 0)
                        {
                            this.GRP22Collection[i - 1].LIN.GRP29Collection.Add(new GRP29((LOC)obj));
                        }
                        break;
                    }
                case SegmentType.UNS:
                    {
                        linFinished = true;
                        unsAccessed = true;
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

        public INVOIC()
        {
            linAccessed = false;
            unsAccessed = false;
        }
    }
    #endregion

    #region RFF DTM

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

    #region NADFII
    [XmlType(TypeName = "NAD", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class NADFII : EDIFACT.BASETYPES.NAD
    {
        [XmlElement(Type = typeof(FII), ElementName = "FII", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public FII __FII;

        [XmlIgnore]
        public FII FII
        {
            get
            {
                if (__FII == null) __FII = new FII();
                return __FII;
            }
            set { __FII = value; }
        }

        [XmlElement(Type = typeof(GRP3), ElementName = "GRP3", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public GRP3Collection __GRP3Collection;

        [XmlIgnore]
        public GRP3Collection GRP3Collection
        {
            get
            {
                if (__GRP3Collection == null) __GRP3Collection = new GRP3Collection();
                return __GRP3Collection;
            }
            set { __GRP3Collection = value; }
        }

        [XmlElement(Type = typeof(GRP5), ElementName = "GRP5", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
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

        public NADFII()
        {
        }
        public NADFII(NAD nadObject)
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

    #region TAXMOA

    [XmlType(TypeName = "TAX", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class TAXMOA : EDIFACT.BASETYPES.TAX
    {
        [XmlElement(Type = typeof(MOA), ElementName = "MOA", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public MOA __MOA;

        [XmlIgnore]
        public MOA MOA
        {
            get
            {
                if (__MOA == null) __MOA = new MOA();
                return __MOA;
            }
            set { __MOA = value; }
        }

        public TAXMOA()
        {
        }
        public TAXMOA(TAX taxObject)
        {
            this.c241CodeListQualifier = taxObject.c241CodeListQualifier;
            this.dtfFunctionQualifier = taxObject.dtfFunctionQualifier;
            this.dtfTypeCoded = taxObject.dtfTypeCoded;
            this.c241CodeListResponsibleAgency = taxObject.c241CodeListResponsibleAgency;
            this.dtfAssessBasis = taxObject.dtfAssessBasis;
            this.dtfAccountDetail = taxObject.dtfAccountDetail;
            this.dtfRate = taxObject.dtfRate;
            this.dtfRateBasisID = taxObject.dtfRateBasisID;
            this.dtfCategory = taxObject.dtfCategory;
        }
    }

    #endregion

    #region CUXDTM

    [XmlType(TypeName = "CUX", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class CUXDTM : EDIFACT.BASETYPES.CUX
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

        public CUXDTM()
        {
        }

        public CUXDTM(DTM dtmObject)
        {
            this.DTM = dtmObject;
        }

        public CUXDTM(CUX cuxObject)
        {
            this.currencyMarketExchange = cuxObject.currencyMarketExchange;
            this.invoicingQualifier = cuxObject.invoicingQualifier;
            this.paymentQualifier = cuxObject.paymentQualifier;
            this.rateOfExchange = cuxObject.rateOfExchange;
            this.referenceCoded = cuxObject.referenceCoded;
            this.referenceDetailsQualifier = cuxObject.referenceDetailsQualifier;
            this.referenceRateBasis = cuxObject.referenceRateBasis;
            this.targetCoded = cuxObject.targetCoded;
            this.targetDetailsQualifier = cuxObject.targetDetailsQualifier;
            this.targetRateBasis = cuxObject.targetRateBasis;
        }
    }

    #endregion

    #region PATDPM

    [XmlType(TypeName = "PAT", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class PATDPM : EDIFACT.BASETYPES.PAT
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

        [XmlElement(Type = typeof(PCD), ElementName = "PCD", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public PCD __PCD;

        [XmlIgnore]
        public PCD PCD
        {
            get
            {
                if (__PCD == null) __PCD = new PCD();
                return __PCD;
            }
            set { __PCD = value; }
        }

        [XmlElement(Type = typeof(MOA), ElementName = "MOA", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public MOA __MOA;

        [XmlIgnore]
        public MOA MOA
        {
            get
            {
                if (__MOA == null) __MOA = new MOA();
                return __MOA;
            }
            set { __MOA = value; }
        }

        public PATDPM()
        {
        }

        public PATDPM(PAT patObject)
        {
            this.codeListQualifier = patObject.codeListQualifier;
            this.codeListResponsibleAgency = patObject.codeListResponsibleAgency;
            this.numberOfPeriods = patObject.numberOfPeriods;
            this.termOfPayment = patObject.termOfPayment;
            this.termsofPaymentID = patObject.termsofPaymentID;
            this.termsTypeQualifier = patObject.termsTypeQualifier;
            this.timeReferenceCoded = patObject.timeReferenceCoded;
            this.timeRelationCoded = patObject.timeRelationCoded;
            this.typeOfPeriodCoded = patObject.typeOfPeriodCoded;
        }
    }
    #endregion

    #region ALCMOA
    [XmlType(TypeName = "ALCMOA", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class ALCMOA : EDIFACT.BASETYPES.ALC
    {
        [XmlElement(Type = typeof(MOA), ElementName = "MOA", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public MOA __MOA;

        [XmlIgnore]
        public MOA MOA
        {
            get
            {
                if (__MOA == null) __MOA = new MOA();
                return __MOA;
            }
            set { __MOA = value; }
        }

        public ALCMOA()
        {
        }
    }
    #endregion

    #region ALC_QPMT

    [XmlType(TypeName = "ALC_QPMT", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class ALC_QPMT : EDIFACT.BASETYPES.ALC
    {

        [XmlElement(Type = typeof(GRP16), ElementName = "GRP16", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
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

        [XmlElement(Type = typeof(GRP17), ElementName = "GRP17", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public GRP17 __GRP17;

        [XmlIgnore]
        public GRP17 GRP17
        {
            get
            {
                if (__GRP17 == null) __GRP17 = new GRP17();
                return __GRP17;
            }
            set { __GRP17 = value; }
        }

        [XmlElement(Type = typeof(GRP18), ElementName = "GRP18", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public GRP18 __GRP18;

        [XmlIgnore]
        public GRP18 GRP18
        {
            get
            {
                if (__GRP18 == null) __GRP18 = new GRP18();
                return __GRP18;
            }
            set { __GRP18 = value; }
        }

        [XmlElement(Type = typeof(GRP20), ElementName = "GRP20", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
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

        public ALC_QPMT(ALC alcObject)
        {
            this.allowanceChargeInformation = alcObject.allowanceChargeInformation;
            this.allowanceChargeQualifier = alcObject.allowanceChargeQualifier;
            this.allowanceOrChargeNumber = alcObject.allowanceOrChargeNumber;
            this.calculationSequenceIndicator = alcObject.calculationSequenceIndicator;
            this.chargeAllowanceDescription = alcObject.chargeAllowanceDescription;
            this.codeListQualifier = alcObject.codeListQualifier;
            this.codeListResponsibleAgency = alcObject.codeListResponsibleAgency;
            this.settlementCoded = alcObject.settlementCoded;
            this.specialService = alcObject.specialService;
            this.specialServicesCoded = alcObject.specialServicesCoded;
            this.specialServicesID = alcObject.specialServicesID;
        }
        public ALC_QPMT() { }
    }
    #endregion

    #region ALC_GRP35

    [XmlType(TypeName = "ALC_GRP35", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class ALC_GRP35 : EDIFACT.BASETYPES.ALC
    {
        [XmlElement(Type = typeof(GRP36), ElementName = "GRP36", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public GRP36 __GRP36;

        [XmlIgnore]
        public GRP36 GRP36
        {
            get
            {
                if (__GRP36 == null) __GRP36 = new GRP36();
                return __GRP36;
            }
            set { __GRP36 = value; }
        }

        [XmlElement(Type = typeof(GRP37), ElementName = "GRP37", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
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

        [XmlElement(Type = typeof(GRP38), ElementName = "GRP38", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public GRP38Collection __GRP38Collection;

        [XmlIgnore]
        public GRP38Collection GRP38Collection
        {
            get
            {
                if (__GRP38Collection == null) __GRP38Collection = new GRP38Collection();
                return __GRP38Collection;
            }
            set { __GRP38Collection = value; }
        }

        [XmlElement(Type = typeof(GRP39), ElementName = "GRP39", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public GRP39 __GRP39;

        [XmlIgnore]
        public GRP39 GRP39
        {
            get
            {
                if (__GRP39 == null) __GRP39 = new GRP39();
                return __GRP39;
            }
            set { __GRP39 = value; }
        }

        public ALC_GRP35(ALC alcObject)
        {
            this.allowanceChargeInformation = alcObject.allowanceChargeInformation;
            this.allowanceChargeQualifier = alcObject.allowanceChargeQualifier;
            this.allowanceOrChargeNumber = alcObject.allowanceOrChargeNumber;
            this.calculationSequenceIndicator = alcObject.calculationSequenceIndicator;
            this.chargeAllowanceDescription = alcObject.chargeAllowanceDescription;
            this.codeListQualifier = alcObject.codeListQualifier;
            this.codeListResponsibleAgency = alcObject.codeListResponsibleAgency;
            this.settlementCoded = alcObject.settlementCoded;
            this.specialService = alcObject.specialService;
            this.specialServicesCoded = alcObject.specialServicesCoded;
            this.specialServicesID = alcObject.specialServicesID;
        }
        public ALC_GRP35() { }
    }

    #endregion

    #region LIN_INV
    [XmlType(TypeName = "LIN_INV", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class LIN_INV : EDIFACT.BASETYPES.LIN
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

        [XmlElement(Type = typeof(ALI), ElementName = "ALI", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public ALI __ALI;

        [XmlIgnore]
        public ALI ALI
        {
            get
            {
                if (__ALI == null) __ALI = new ALI();
                return __ALI;
            }
            set { __ALI = value; }
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

        [XmlElement(Type = typeof(GRP23), ElementName = "GRP23", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public GRP23Collection __GRP23Collection;

        [XmlIgnore]
        public GRP23Collection GRP23Collection
        {
            get
            {
                if (__GRP23Collection == null) __GRP23Collection = new GRP23Collection();
                return __GRP23Collection;
            }
            set { __GRP23Collection = value; }
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

        [XmlElement(Type = typeof(GRP26), ElementName = "GRP26", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public GRP26Collection __GRP26Collection;

        [XmlIgnore]
        public GRP26Collection GRP26Collection
        {
            get
            {
                if (__GRP26Collection == null) __GRP26Collection = new GRP26Collection();
                return __GRP26Collection;
            }
            set { __GRP26Collection = value; }
        }

        [XmlElement(Type = typeof(GRP27), ElementName = "GRP27", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public GRP27Collection __GRP27Collection;

        [XmlIgnore]
        public GRP27Collection GRP27Collection
        {
            get
            {
                if (__GRP27Collection == null) __GRP27Collection = new GRP27Collection();
                return __GRP27Collection;
            }
            set { __GRP27Collection = value; }
        }

        [XmlElement(Type = typeof(GRP29), ElementName = "GRP29", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public GRP29Collection __GRP29Collection;

        [XmlIgnore]
        public GRP29Collection GRP29Collection
        {
            get
            {
                if (__GRP29Collection == null) __GRP29Collection = new GRP29Collection();
                return __GRP29Collection;
            }
            set { __GRP29Collection = value; }
        }

        [XmlElement(Type = typeof(GRP30), ElementName = "GRP30", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public GRP30Collection __GRP30Collection;

        [XmlIgnore]
        public GRP30Collection GRP30Collection
        {
            get
            {
                if (__GRP30Collection == null) __GRP30Collection = new GRP30Collection();
                return __GRP30Collection;
            }
            set { __GRP30Collection = value; }
        }

        [XmlElement(Type = typeof(GRP31), ElementName = "GRP31", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public GRP31Collection __GRP31Collection;

        [XmlIgnore]
        public GRP31Collection GRP31Collection
        {
            get
            {
                if (__GRP31Collection == null) __GRP31Collection = new GRP31Collection();
                return __GRP31Collection;
            }
            set { __GRP31Collection = value; }
        }

        [XmlElement(Type = typeof(GRP35), ElementName = "GRP35", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public GRP35Collection __GRP35Collection;

        [XmlIgnore]
        public GRP35Collection GRP35Collection
        {
            get
            {
                if (__GRP35Collection == null) __GRP35Collection = new GRP35Collection();
                return __GRP35Collection;
            }
            set { __GRP35Collection = value; }
        }

        public LIN_INV()
        {
        }
        public LIN_INV(LIN linObject)
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

    #endregion
}