//--------------------------------------------------------------
// <CreatedBy>
//     Anthony R Yates
//	   Jul. 04, 2005
//     Runtime Version: 1.1.4322.2032
//	   
//     It contains D96A INVOICE Classes
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

namespace EDIFACT.D96A.INVOIC
{

    #region Declarations Schema Version
    public struct Declarations
    {
        public const string SchemaVersion = "http://www.default.com/D96A/invoic";
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
    //Jul 5, 2005. Good To Go!	
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

        public GRP2() { }

        public GRP2(NAD nadObject)
        {
            this.NAD = new NADFII(nadObject);
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
    //Jul 5, 2005. Good To Go!	
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
    //Jul 5, 2005. Good To Go!	
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
    //Jul 5, 2005. Good To Go!	
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
    //Jul 5, 2005. Good To Go!
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

    #region GRP12
    //Jul 5, 2005. Good To Go!
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

    #endregion

    #region GRP15
    //Jul 5, 2005. Good To Go!
    [XmlType(TypeName = "GRP15", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP15
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

        public GRP15(ALC alcObject)
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
        public GRP15() { }
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

    #endregion

    #region GRP17
    //Jul 5, 2005. Good To Go!
    [XmlType(TypeName = "GRP17", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP17
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

        public GRP17()
        {
        }

        public GRP17(QTY qtyObject)
        {
            this.QTY = qtyObject;
        }
    }
    #endregion

    #region GRP18
    //Jul 5, 2005. Good To Go!
    [XmlType(TypeName = "GRP18", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP18
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

        public GRP18()
        {
        }

        public GRP18(PCD pcdObject)
        {
            this.PCD = pcdObject;
        }
    }

    #endregion

    #region GRP19
    //Jul 5, 2005. Good To Go!
    [XmlType(TypeName = "GRP19", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP19
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

        public GRP19()
        {
        }

        public GRP19(MOA moaObject)
        {
            this.MOA = moaObject;
        }
    }

    #endregion

    #region GRP21
    //Jul 5, 2005. Good To Go!
    [XmlType(TypeName = "GRP21", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP21
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

        public GRP21()
        {
        }

        public GRP21(TAX taxObject)
        {
            this.TAX = taxObject;
        }
    }

    #endregion

    #region GRP25
    //Jul 5, 2005. Good To Go!
    [XmlType(TypeName = "GRP25", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP25
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

        public GRP25()
        {
        }
        public GRP25(LIN linObject)
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
    //Jul 5, 2005. Good To Go!
    [XmlType(TypeName = "GRP26", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP26
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

        public GRP26() { }

        public GRP26(MOA moaObject)
        {
            this.MOA = moaObject;
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

    #region GRP28
    //Jul 5, 2005. Good To Go!
    [XmlType(TypeName = "GRP28", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP28
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

        public GRP28()
        {
        }

        public GRP28(PRI priObject)
        {
            this.PRI = priObject;
        }
    }

    [Serializable]
    public class GRP28Collection : ArrayList
    {
        public GRP28 Add(GRP28 obj)
        {
            base.Add(obj);
            return obj;
        }

        public GRP28 Add()
        {
            return Add(new GRP28());
        }

        public void Insert(int index, GRP28 obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(GRP28 obj)
        {
            base.Remove(obj);
        }

        new public GRP28 this[int index]
        {
            get { return (GRP28)base[index]; }
            set { base[index] = value; }
        }
    }
    #endregion

    #region GRP29
    //Jul 5, 2005. Good To Go!
    [XmlType(TypeName = "GRP29", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP29
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

        public GRP29()
        {
        }

        public GRP29(RFF rffObject)
        {
            this.RFF = rffObject;
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
    //Jul 5, 2005. Good To Go!
    [XmlType(TypeName = "GRP30", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP30
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

        public GRP30()
        {
        }
        public GRP30(PAC pacObject)
        {
            this.PAC = pacObject;
        }
    }

    #endregion

    #region GRP33
    //Jul 5, 2005. Good To Go!
    [XmlType(TypeName = "GRP33", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP33
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

        public GRP33()
        {
        }
        public GRP33(TAX taxObject)
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
    //Jul 5, 2005. Good To Go!
    [XmlType(TypeName = "GRP34", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP34
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

        public GRP34()
        {
        }

        public GRP34(NAD nadObject)
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

    #endregion

    #region GRP38
    //Jul 5, 2005. Good To Go!
    [XmlType(TypeName = "GRP38", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP38
    {
        [XmlElement(Type = typeof(ALC_GRP38), ElementName = "ALC", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public ALC_GRP38 __ALC;

        [XmlIgnore]
        public ALC_GRP38 ALC
        {
            get
            {
                if (__ALC == null) __ALC = new ALC_GRP38();
                return __ALC;
            }
            set { __ALC = value; }
        }

        public GRP38()
        {
        }
        public GRP38(ALC alcObject)
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
    //Jul 5, 2005. Good To Go!
    [XmlType(TypeName = "GRP39", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP39
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

        public GRP39() { }

        public GRP39(QTY qtyObject)
        {
            this.QTY = qtyObject;
        }
    }

    #endregion

    #region GRP40
    //Jul 5, 2005. Good To Go!
    [XmlType(TypeName = "GRP40", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP40
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

        public GRP40() { }

        public GRP40(PCD pcdObject)
        {
            this.PCD = pcdObject;
        }
    }

    #endregion

    #region GRP41
    //Jul 5, 2005. Good To Go!
    [XmlType(TypeName = "GRP41", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP41
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

        public GRP41() { }

        public GRP41(MOA moaObject)
        {
            this.MOA.amountTypeQualifier = moaObject.amountTypeQualifier;
            this.MOA.currencyCoded = moaObject.currencyCoded;
            this.MOA.currencyQualifier = moaObject.currencyQualifier;
            this.MOA.monetaryAmount = moaObject.monetaryAmount;
            this.MOA.statusCoded = moaObject.statusCoded;
        }
    }

    #endregion

    #region GRP42
    //Jul 5, 2005. Good To Go!
    [XmlType(TypeName = "GRP42", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP42
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

        public GRP42() { }

        public GRP42(RTE rteObject)
        {
            this.RTE = rteObject;
        }
    }

    #endregion

    #region GRP48
    //Jul 5, 2005. Good To Go!
    [XmlType(TypeName = "GRP48", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP48
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

        public GRP48()
        {
        }
        public GRP48(MOA moaObject)
        {
            this.MOA = moaObject;
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

    #region GRP50
    //Jul 5, 2005. Good To Go!
    [XmlType(TypeName = "GRP50", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP50
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

        public GRP50()
        {
        }
        public GRP50(TAX taxObject)
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
    public class GRP50Collection : ArrayList
    {
        public GRP50 Add(GRP50 obj)
        {
            base.Add(obj);
            return obj;
        }

        public GRP50 Add()
        {
            return Add(new GRP50());
        }

        public void Insert(int index, GRP50 obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(GRP50 obj)
        {
            base.Remove(obj);
        }

        new public GRP50 this[int index]
        {
            get { return (GRP50)base[index]; }
            set { base[index] = value; }
        }
    }

    #endregion

    #region GRP51
    //Jul 5, 2005. Good To Go!
    [XmlType(TypeName = "GRP51", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class GRP51
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

        public GRP51()
        {
        }

        public GRP51(ALC alcObject)
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
    public class GRP51Collection : ArrayList
    {
        public GRP51 Add(GRP51 obj)
        {
            base.Add(obj);
            return obj;
        }

        public GRP51 Add()
        {
            return Add(new GRP51());
        }

        public void Insert(int index, GRP51 obj)
        {
            base.Insert(index, obj);
        }

        public void Remove(GRP51 obj)
        {
            base.Remove(obj);
        }

        new public GRP51 this[int index]
        {
            get { return (GRP51)base[index]; }
            set { base[index] = value; }
        }
    }

    #endregion

    #endregion

    #region D96A_INVOIC Class

    [XmlRoot(ElementName = "D96A_INVOIC", Namespace = Declarations.SchemaVersion, IsNullable = false), Serializable]
    public class D96A_INVOIC : IMessage
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

        public D96A_INVOIC()
        {
        }

        ~D96A_INVOIC()
        {
            this.INVOIC = null;
        }

        #region IMessage Members

        #region PopulateMessage Method

        public void PopulateMessage(ref Segment[] segments)
        {
            SegmentProcessor sp = new SegmentProcessor(new EDIFACT.AddSegmentDelegate(this.INVOIC.Add));
            sp.ProcessSegments(segments);
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

        [XmlElement(Type = typeof(GRP12), ElementName = "GRP12", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
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


        [XmlElement(Type = typeof(GRP15), ElementName = "GRP15", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
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

        [XmlElement(Type = typeof(GRP50), ElementName = "GRP50", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public GRP50Collection __GRP50Collection;

        [XmlIgnore]
        public GRP50Collection GRP50Collection
        {
            get
            {
                if (__GRP50Collection == null) __GRP50Collection = new GRP50Collection();
                return __GRP50Collection;
            }
            set { __GRP50Collection = value; }
        }

        [XmlElement(Type = typeof(GRP51), ElementName = "GRP51", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public GRP51Collection __GRP51Collection;

        [XmlIgnore]
        public GRP51Collection GRP51Collection
        {
            get
            {
                if (__GRP51Collection == null) __GRP51Collection = new GRP51Collection();
                return __GRP51Collection;
            }
            set { __GRP51Collection = value; }
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
            int i = 0;
            int j = 0;

            switch (type)
            {
                case SegmentType.UNH: //ARY Good July 5
                    {
                        this.UNH = (UNH)obj;
                        break;
                    }
                case SegmentType.BGM: //ARY Good July 5
                    {
                        this.BGM = (BGM)obj;
                        break;
                    }
                case SegmentType.DTM: //ARY Good July 5, 7,12,13,35,134,137,171,263
                    {
                        if (((DTM)obj).dateTimePeriodQualifier == "35" || //Delivery Date/Time (Actual) / Leveringsdato
                            ((DTM)obj).dateTimePeriodQualifier == "137" || //Doc/Msg Date/Time	/ Meldingsdato
                            ((DTM)obj).dateTimePeriodQualifier == "263")  //Invoicing Period	/ Fakturaperiode
                        {
                            this.DTMCollection.Add((DTM)obj);
                            if (((DTM)obj).dateTimePeriodQualifier == "35" && linAccessed)
                            {
                                this.GRP25Collection[GRP25Collection.Count - 1].LIN.DTM = (DTM)obj;
                            }
                        }
                        else if (((DTM)obj).dateTimePeriodQualifier == "7" ||
                                 ((DTM)obj).dateTimePeriodQualifier == "12" ||
                                 ((DTM)obj).dateTimePeriodQualifier == "13")
                        {
                            if ((i = this.GRP8Collection.Count) > 0)
                                this.GRP8Collection[i - 1].PAT.DTM = (DTM)obj;
                        }
                        else if (((DTM)obj).dateTimePeriodQualifier == "134")
                        { //Rate of Exchange Date
                            this.GRP7.CUX.DTM = (DTM)obj;
                        }
                        else if (((DTM)obj).dateTimePeriodQualifier == "171")
                        { //Rate of Exchange Date
                            if ((i = this.GRP1Collection.Count) > 0)
                                this.GRP1Collection[i - 1].RFF.DTM = (DTM)obj;
                        }
                        break;
                    }
                case SegmentType.FTX: //ARY Good July 5
                    {
                        if (((FTX)obj).textSubjectQualifier == "INV" ||
                            ((FTX)obj).textSubjectQualifier == "PUR")
                        {
                            this.FTXCollection.Add((FTX)obj);
                        }
                        else if ((i = this.GRP25Collection.Count) > 0 &&
                            ((FTX)obj).textSubjectQualifier == "CHG" || //Change Information
                            ((FTX)obj).textSubjectQualifier == "ZZZ")       //Mutually Defined
                        {
                            this.GRP25Collection[i - 1].LIN.FTXCollection.Add((FTX)obj);
                        }
                        break;
                    }
                case SegmentType.NAD: //ARY Good July 5
                    {
                        string party = ((NAD)obj).partyQualifier.ToUpper();

                        switch (party)
                        {
                            case "AT":
                                {
                                    if ((i = this.GRP25Collection.Count) > 0)
                                        this.GRP25Collection[i - 1].LIN.GRP34.NAD = (NAD)obj;
                                    break;
                                }
                            case "BY": { goto case "UD"; }
                            case "CA": { goto case "UD"; }
                            case "DL": { goto case "UD"; }
                            case "DP": { goto case "UD"; }
                            case "II": { goto case "UD"; }
                            case "IV": { goto case "UD"; }
                            case "MF": { goto case "UD"; }
                            case "RB": { break; } //Not Seen in EAN Standard although listed
                            case "RH": { break; } //  "                                 "
                            case "SF": { goto case "UD"; }
                            case "SU": { goto case "UD"; }
                            case "UD":
                                {
                                    this.GRP2Collection.Add(new GRP2((NAD)obj));
                                    break;
                                }
                            default:
                                {
                                    throw new Exception(string.Format("NAD conditions do not compute. Qualifier: {0}, Party ID: {1}, Party Name: {2}.",
                                        ((NAD)obj).partyQualifier, ((NAD)obj).partyIDIdentification, ((NAD)obj).partyName));
                                }
                        }

                        if (lastSegmentAccessed != SegmentType.LIN)
                            lastSegmentAccessed = type;
                        break;
                    }
                case SegmentType.FII: //ARY Good July 5
                    {                                                   //TODO : Perform Validation
                        if ((i = this.GRP2Collection.Count) > 0)
                        {       //FII.partyQualifier = RB when NAD = DL (Receiving FI)
                            this.GRP2Collection[i - 1].NAD.FII = (FII)obj;//FII.partyQualifier = RH when NAD = SU (Sellers FI)
                        }
                        break;
                    }
                case SegmentType.CTA: //ARY Good July 5
                    {
                        if ((i = this.GRP2Collection.Count) > 0)
                        {
                            this.GRP2Collection[i - 1].NAD.GRP5.CTA = (CTA)obj;
                        }
                        break;
                    }
                case SegmentType.RFF: //ARY Good July 5, 2005
                    {
                        if (((RFF)obj).referenceQualifier == "AAK" || //Despatch Advice Number
                            ((RFF)obj).referenceQualifier == "CR" || //Customer Reference Number
                            ((RFF)obj).referenceQualifier == "ON" || //Order Number (Purchase)
                            ((RFF)obj).referenceQualifier == "IV" || //Invoice Number
                            ((RFF)obj).referenceQualifier == "VN")   //Order Number (Vendor)
                        { //Grp1 and most to Grp26
                            this.GRP1Collection.Add(new GRP1((RFF)obj));
                            if ((i = this.GRP25Collection.Count) > 0 && ((RFF)obj).referenceQualifier != "CR")
                            {
                                this.GRP25Collection[i - 1].LIN.GRP29Collection.Add(new GRP29((RFF)obj));
                            }
                        }
                        else if (((RFF)obj).referenceQualifier == "GN" || //Gov't Ref Num
                            ((RFF)obj).referenceQualifier == "VA" || //VAT Reg Num
                            ((RFF)obj).referenceQualifier == "PQ" || //Payment Reference
                            ((RFF)obj).referenceQualifier == "API")  //Additional ID of Parties
                        { //Grp3
                            if ((i = this.GRP2Collection.Count) > 0)
                            {
                                this.GRP2Collection[i - 1].NAD.GRP3Collection.Add(new GRP3((RFF)obj));
                            }
                        }
                        break;
                    }
                case SegmentType.TAX: //ARY Good July 5
                    {
                        if (unsAccessed)
                        {
                            this.GRP50Collection.Add(new GRP50((TAX)obj));
                            break;
                        }
                        if (lastSegmentAccessed == SegmentType.LIN)
                        { //TODO : Test for LIN->NAD
                            if ((i = this.GRP25Collection.Count) > 0)
                            {
                                this.GRP25Collection[i - 1].LIN.GRP33Collection.Add(new GRP33((TAX)obj));
                            }
                            break;
                        }
                        if (lastSegmentAccessed == SegmentType.ALC)
                        {
                            if ((i = this.GRP15Collection.Count) > 0)
                                this.GRP15Collection[i - 1].ALC.GRP21.TAX = (TAX)obj;
                            break;
                        }
                        if ((lastSegmentAccessed == SegmentType.NAD) && (!unsAccessed))
                        {
                            this.GRP6Collection.Add(new GRP6((TAX)obj));
                            break;
                        }
                        throw new Exception("Unhandled TAX segment conditions detected.");
                    }
                case SegmentType.MOA: //124, 21, 8, (66,203), 
                    {
                        int qualifier = 0;
                        try
                        {   //Get the qualifier number
                            qualifier = int.Parse(((MOA)obj).amountTypeQualifier);
                        }
                        catch (FormatException)
                        {   //SHOULD NOT GET HERE FOR D96A
                            //Qualifer 'NET' = D93A, not D96A)
                            if (((MOA)obj).amountTypeQualifier == "NET")
                                this.GRP48Collection.Add(new GRP48((MOA)obj));
                            break;
                        }
                        catch (Exception)
                        {
                            throw new Exception("MOA.amountTypeQualifier is not a recognized value.");
                        }

                        //Stuff MOA into appropriate group(s) based off qualifier number
                        switch (qualifier)
                        {
                            case 8:
                                if (unsAccessed)
                                    if ((i = this.GRP51Collection.Count) > 0)
                                        this.GRP51Collection[i - 1].ALC.MOA = (MOA)obj;

                                if (lastSegmentAccessed == SegmentType.ALC)
                                {
                                    if ((i = this.GRP15Collection.Count) > 0)
                                        this.GRP15Collection[i - 1].ALC.GRP19 = new GRP19((MOA)obj);
                                }
                                else
                                    if ((i = this.GRP25Collection.Count) > 0)
                                    if ((j = this.GRP25Collection[i - 1].LIN.GRP38Collection.Count) > 0)
                                        this.GRP25Collection[i - 1].LIN.GRP38Collection[j - 1].ALC.GRP41.MOA = (MOA)obj;
                                break;
                            case 9:
                                goto case 260;
                            case 21:
                                if ((i = this.GRP8Collection.Count) > 0)
                                    this.GRP8Collection[i - 1].PAT.MOA = (MOA)obj;
                                break;
                            case 66:
                                if (!linFinished)
                                    if ((i = this.GRP25Collection.Count) > 0)
                                        this.GRP25Collection[i - 1].LIN.GRP26Collection.Add(new GRP26((MOA)obj));
                                //Net price x : Quantity for all line items
                                goto case 260;
                            case 79:
                                goto case 260;
                            case 86:
                                goto case 260;

                            case 124:
                                //
                                if ((i = this.GRP50Collection.Count) > 0)
                                {
                                    this.GRP50Collection[i - 1].TAX.MOA = (MOA)obj;
                                    goto case 260;
                                }

                                if ((i = this.GRP25Collection.Count) > 0 &&
                                    (j = this.GRP25Collection[i - 1].LIN.GRP33Collection.Count) > 0)
                                {
                                    this.GRP25Collection[i - 1].LIN.GRP33Collection[j - 1].TAX.MOA = (MOA)obj;
                                    goto case 260;
                                }

                                if ((i = this.GRP6Collection.Count) > 0)
                                    this.GRP6Collection[i - 1].TAX.MOA = (MOA)obj;

                                goto case 260;
                            //TODO : Ensure required 86, 125, 150, 203 are present
                            //		 for validation
                            case 125:
                                goto case 260;

                            case 129:
                                goto case 260;

                            case 131:
                                goto case 260;

                            case 150:
                                goto case 260;

                            case 165:
                                goto case 260;

                            case 176:
                                if ((i = this.GRP50Collection.Count) > 0)
                                    this.GRP50Collection[i - 1].TAX.MOA = (MOA)obj;
                                break;
                            case 203:
                                if (!linFinished)
                                    if ((i = this.GRP25Collection.Count) > 0)
                                        this.GRP25Collection[i - 1].LIN.GRP26Collection.Add(new GRP26((MOA)obj));
                                goto case 260;

                            case 259:
                                goto case 260;

                            case 260:
                                this.GRP48Collection.Add(new GRP48((MOA)obj));
                                break;
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
                        if (((PCD)obj).percentageQualifier == 12 || ((PCD)obj).percentageQualifier == 15)
                        {
                            if ((i = this.GRP8Collection.Count) > 0)
                                this.GRP8Collection[i - 1].PAT.PCD = (PCD)obj;
                            break;
                        }
                        else
                        {
                            if (lastSegmentAccessed == SegmentType.ALC && (i = this.GRP15Collection.Count) > 0)
                            {
                                this.GRP15Collection[i - 1].ALC.GRP18.PCD = (PCD)obj;
                            }
                            else if (lastSegmentAccessed == SegmentType.LIN)
                            {
                                if ((i = this.GRP25Collection.Count) > 0)
                                    if ((j = GRP25Collection[i - 1].LIN.GRP38Collection.Count) > 0)
                                        GRP25Collection[i - 1].LIN.GRP38Collection[j - 1].ALC.GRP40.PCD = (PCD)obj;
                            }
                            else
                                throw new Exception(string.Format("PCD conditions do not compute. Qualifier: {0}, Percentage: {1}",
                                ((PCD)obj).percentageQualifier, ((PCD)obj).percentage));
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
                        linAccessed = true;
                        this.GRP25Collection.Add(new GRP25((LIN)obj));
                        lastSegmentAccessed = SegmentType.LIN;
                        break;
                    }
                case SegmentType.ALC:
                    {
                        if (unsAccessed)
                        {
                            this.GRP51Collection.Add(new GRP51((ALC)obj));
                            break;
                        }
                        else if (lastSegmentAccessed == SegmentType.LIN)
                        {
                            if ((i = this.GRP25Collection.Count) > 0)
                                GRP25Collection[i - 1].LIN.GRP38Collection.Add(new GRP38((ALC)obj));
                            break;
                        }
                        else
                            this.GRP15Collection.Add(new GRP15((ALC)obj));

                        lastSegmentAccessed = SegmentType.ALC;
                        break;
                    }
                case SegmentType.RTE:
                    {
                        if ((i = this.GRP25Collection.Count) > 0)//  GRP25.LINCollection.Count > 0) 
                            if ((j = this.GRP25Collection[i - 1].LIN.GRP38Collection.Count) > 0)
                                this.GRP25Collection[i - 1].LIN.GRP38Collection[j - 1].ALC.GRP42.RTE = (RTE)obj;
                        break;
                    }
                case SegmentType.PIA:
                    {
                        if ((i = this.GRP25Collection.Count) > 0)
                            this.GRP25Collection[i - 1].LIN.PIACollection.Add((PIA)obj);
                        break;
                    }
                case SegmentType.IMD:
                    {
                        if ((i = this.GRP25Collection.Count) > 0)
                            this.GRP25Collection[i - 1].LIN.IMDCollection.Add((IMD)obj);
                        break;
                    }
                case SegmentType.QTY:
                    {
                        int qtyQualifier = Int32.Parse(((QTY)obj).qtyQualifier);
                        //ARY July 6, 2005: Could check qualifier instead of
                        //collection count, then insert into proper place...
                        if ((i = this.GRP25Collection.Count) > 0)
                        {
                            if (qtyQualifier == 1)
                            {
                                if ((j = this.GRP25Collection[i - 1].LIN.GRP38Collection.Count) > 0)
                                    this.GRP25Collection[i - 1].LIN.GRP38Collection[j - 1].ALC.GRP39.QTY = (QTY)obj;
                            }
                            else if (qtyQualifier == 21 || qtyQualifier == 46 ||
                                qtyQualifier == 47 || qtyQualifier == 59)
                            {
                                this.GRP25Collection[i - 1].LIN.QTYCollection.Add((QTY)obj);
                            }
                            else
                                throw new Exception(string.Format("QTY conditions do not compute. Qualifier: {0}, Quantity: {1}, Measurement: {2}.",
                                    ((QTY)obj).qtyQualifier, ((QTY)obj).quantity, ((QTY)obj).measureUnitQualifier));
                        }
                        else if ((i = this.GRP15Collection.Count) > 0)
                        {
                            this.GRP15Collection[i - 1].ALC.GRP17.QTY = (QTY)obj;
                        }
                        break;
                    }
                case SegmentType.ALI:
                    {
                        if ((i = this.GRP25Collection.Count) > 0)
                            this.GRP25Collection[i - 1].LIN.ALI = (ALI)obj;
                        break;
                    }
                case SegmentType.PRI:
                    {
                        if ((i = this.GRP25Collection.Count) > 0)
                            this.GRP25Collection[i - 1].LIN.GRP28Collection.Add(new GRP28((PRI)obj));
                        break;
                    }
                case SegmentType.PAC:
                    {
                        if ((i = this.GRP25Collection.Count) > 0)
                            this.GRP25Collection[i - 1].LIN.GRP30.PAC = (PAC)obj;
                        break;
                    }
                case SegmentType.LOC:
                    {
                        throw new Exception("LOC segment detected. There should not be a LOC segment in a D96A EDIFACT message.");
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

    #region RFFDTM
    //Jul 5, 2005. Good To Go!
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
    //Jul 5, 2005. Good To Go!
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
    //Jul 5, 2005. Good To Go!
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
    //Jul 5, 2005. Good To Go!
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
    //Jul 5, 2005. Good To Go!
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
    //Jul 5, 2005. Good To Go!
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

        public ALCMOA(MOA moaObject)
        {
            this.MOA = moaObject;
        }
    }
    #endregion

    #region ALC_QPMT
    //Jul 5, 2005. Good To Go!
    [XmlType(TypeName = "ALC_QPMT", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class ALC_QPMT : EDIFACT.BASETYPES.ALC
    {

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

        [XmlElement(Type = typeof(GRP19), ElementName = "GRP19", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public GRP19 __GRP19;

        [XmlIgnore]
        public GRP19 GRP19
        {
            get
            {
                if (__GRP19 == null) __GRP19 = new GRP19();
                return __GRP19;
            }
            set { __GRP19 = value; }
        }

        [XmlElement(Type = typeof(GRP21), ElementName = "GRP21", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public GRP21 __GRP21;

        [XmlIgnore]
        public GRP21 GRP21
        {
            get
            {
                if (__GRP21 == null) __GRP21 = new GRP21();
                return __GRP21;
            }
            set { __GRP21 = value; }
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

    #region LIN_INV
    //Jul 5, 2005. Good To Go!
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

        [XmlElement(Type = typeof(GRP28), ElementName = "GRP28", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public GRP28Collection __GRP28Collection;

        [XmlIgnore]
        public GRP28Collection GRP28Collection
        {
            get
            {
                if (__GRP28Collection == null) __GRP28Collection = new GRP28Collection();
                return __GRP28Collection;
            }
            set { __GRP28Collection = value; }
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
        public GRP30 __GRP30;

        [XmlIgnore]
        public GRP30 GRP30
        {
            get
            {
                if (__GRP30 == null) __GRP30 = new GRP30();
                return __GRP30;
            }
            set { __GRP30 = value; }
        }

        [XmlElement(Type = typeof(GRP33), ElementName = "GRP33", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
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

        [XmlElement(Type = typeof(GRP34), ElementName = "GRP34", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public GRP34 __GRP34;

        [XmlIgnore]
        public GRP34 GRP34
        {
            get
            {
                if (__GRP34 == null) __GRP34 = new GRP34();
                return __GRP34;
            }
            set { __GRP34 = value; }
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

    #region ALC_GRP38
    //Jul 5, 2005. Good To Go!
    [XmlType(TypeName = "ALC_GRP38", Namespace = Declarations.SchemaVersion), XmlRoot, Serializable]
    public class ALC_GRP38 : EDIFACT.BASETYPES.ALC
    {
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

        [XmlElement(Type = typeof(GRP40), ElementName = "GRP40", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public GRP40 __GRP40;

        [XmlIgnore]
        public GRP40 GRP40
        {
            get
            {
                if (__GRP40 == null) __GRP40 = new GRP40();
                return __GRP40;
            }
            set { __GRP40 = value; }
        }

        [XmlElement(Type = typeof(GRP41), ElementName = "GRP41", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public GRP41 __GRP41;

        [XmlIgnore]
        public GRP41 GRP41
        {
            get
            {
                if (__GRP41 == null) __GRP41 = new GRP41();
                return __GRP41;
            }
            set { __GRP41 = value; }
        }

        [XmlElement(Type = typeof(GRP42), ElementName = "GRP42", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public GRP42 __GRP42;

        [XmlIgnore]
        public GRP42 GRP42
        {
            get
            {
                if (__GRP42 == null) __GRP42 = new GRP42();
                return __GRP42;
            }
            set { __GRP42 = value; }
        }

        public ALC_GRP38() { }

        public ALC_GRP38(ALC alcObject)
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
    }

    #endregion
}
