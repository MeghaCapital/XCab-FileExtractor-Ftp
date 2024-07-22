//--------------------------------------------------------------
// <CreatedBy>
//     Anthony R Yates
//	   Feb. 26, 2005
//     Runtime Version: 1.1.4322.2032
//	   
//     Contains EDIFACT Enumerations
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

namespace EDIFACT
{
    /// <summary>
    /// The MessageType enumerator lists all message types this handler can parse.
    /// </summary>
    public enum MessageTypeIdentifier
    {
        UNDEFINED,  //Undefined
        APERAK,     //Application error and acknowledgement
        DESADV,     //Despatch Advice
        INVOIC,     //Invoice message
        ORDERS,     //Orders
        ORDRSP,     //Order Response
        PRICAT      //Price/Sales Catalogue
    }
    /// <summary>
    /// The SegmentType enumeration lists all possible segments a handler can process.
    /// The three dots on the right represent those that has associated class. Those that
    /// do not contain corresponding classes, are followed by a comment that tells what
    /// message the segment type is a part of. For example, AJT is not implemented, but
    /// is listed in the D96A Invoice specification.
    /// </summary>
    public enum SegmentType
    {					/// ... = Implemented
		AJT,    //D96A Invoice
        ALC,    //Allowance Or Charges				...
        ALI,    //Additional Information			...
        APR,    //D96 Orders
        BGM,    //Beginning Of Message				...
        CAV,    //D96 Orders
        CCI,    //D96 Orders
        CNT,    //Control Total						...
        COM,    //Communication Contract			...
        CPS,    //Consignment Packing Sequence		...
        CTA,    //Contact Information				...
        CUX,    //Currencies						...
        DGS,    //D96 DesAdv
        DLM,    //D96 DesAdv
        DOC,    //D96 Orders		
        DTM,    //Date/Time/Period					...
        ERC,    //Error Code						...
        EQA,    //D96 DesAdv
        EQD,    //D96 Orders
        FII,    //Financial Institution Information	...
        FTX,    //Free Text							...
        GIN,    //Goods Identity Number				...
        GIR,    //D96 Orders
        HAN,    //Handling Instructions				...
        IMD,    //Item Description					...
        INP,    //D96A Invoice
        LIN,    //Line Item							...
        LOC,    //Place/Location Identification		...
        MEA,    //Measurements						...
        MOA,    //Monetary Amount					...
        NAD,    //Name And Address					...
        PAC,    //Package							...
        PAI,    //D96 Orders
        PAT,    //Payment Terms Basis				...
        PCD,    //Percentage Details				...
        PCI,    //Package Identification			...
        PGI,    //Product Group Information			...
        PIA,    //Additional Product ID				...
        PIT,    //Price Item Line					...
        PRI,    //Pricing Details					...
        QTY,    //Quantity							...
        QVA,    //Quantity Variances				...
        QVR,    //Quantity Variances				...
        RCS,    //D96 Orders
        RFF,    //Reference							...
        RNG,    //D96 Orders
        RTE,    //Rate Details						...
        SCC,    //D96 Orders
        SEL,    //D96 DesAdv
        SGP,    //D96 DesAdv
        STG,    //D96 Orders
        TAX,    //Duty/Tax/Fee Details				...
        TDT,    //D96 Orders
        TOD,    //Terms Of Delivery					...
        UNA,    //Section							...
        UNB,    //Section Control					...
        UNH,    //Message Header					...
        UNS,    //Section Control					...
        UNT,    //Message Trailer					...
        UNZ     //									...
    }   //59 (41 Implemented, 19 Not Implemented)

    /// <summary>
    /// The SegmentGroup enumeration lists all possible segment groups a handler can process. 
    /// </summary>
    public enum SegmentGroup
    {
        Initial,
        Grp1,
        Grp2,
        Grp3,
        Grp4,
        Grp5,
        Grp6,
        Grp7,
        Grp8,
        Grp9,
        Grp10,
        Grp11,
        Grp12,
        Grp13,
        Grp14,
        Grp15,
        Grp16,
        Grp17,
        Grp18,
        Grp19,
        Grp20,
        Grp21,
        Grp22,
        Grp23,
        Grp24,
        Grp25,
        Grp26,
        Grp27,
        Grp28,
        Grp29,
        Grp30,
        Grp31,
        Grp32,
        Grp33,
        Grp34,
        Grp35,
        Grp36,
        Grp37,
        Grp38,
        Grp39,
        Grp40,
        Grp41,
        Grp42,
        Grp43,
        Grp44,
        Grp45,
        Grp46,
        Grp47,
        Grp48,
        Grp49,
        Grp50,
        Grp51,
        Grp52,
        Grp53,
        Grp54,
        Grp55,
        Grp56,
        Grp57,
        Grp58,
        Grp59,
        Grp60,
        Grp61,
        Grp62,
        Grp63,
        Grp64,
        Grp65,
        Grp66,
        Grp67,
        Grp68,
        Grp69,
        Grp70,
        Final,
        HOWMANY
    }

    #region Code Listing INVOIC-DEDIP2-ENG (D93A):

    enum ControllingAgency
    {
        UN //UN/ECE/TRADE/WP.4, United Nations Standard Messages (UNSM)
    }

    //0081
    enum SectionIdentification
    {
        S,      // Detail/summary section separation
    }
    //1001
    enum DocumentMessageNameCoded
    {
        _380,   // Commercial invoice
        _381    // Credit note
    }
    public enum InvoiceType
    {
        _380,
        _381
    }

    //1153
    enum ReferenceQualifier
    {
        AAK,// Despatch advice number
        API,// Additional identification of parties
        CR,// Customer reference number
        GN,// Government reference number
        IV,// Invoice number
        ON,// Order number (purchase)
        PQ,// Payment reference
        VA,// VAT registration number
        VN // Order number (vendor)
    }

    //1225
    enum MessageFunctionCoded
    {
        _31 // Copy
    }
    //2005
    enum DateTimePeriodQualifier
    {
        _7,// Effective date/time
        _12,// Terms discount due date/time
        _13,// Terms net due date
        _35,// Delivery date/time, actual
        _134,// Rate of exchange date/time
        _137,// Document/message date/time
        _263// Invoicing period
    }
    //2151
    enum TypeOfPeriodCoded
    {
        D,// Day
        M //Month
    }
    //2379
    enum DateTimePeriodFormatQualifier
    {
        _102, //CCYYMMDD
        _203, //CCYYMMDDHHMM
        _718 //CCYYMMDD-CCYYMMDD
    }

    //2475
    enum PaymentTimeReferenceCoded
    {
        _5 // Date of invoice
    }

    //3035
    enum PartyQualifier
    {
        AT,// Authorized importer				;
        BY,// Buyer								;;
        CA,// Carrier							;;
        DL,// Factor							;;
        DP,// Delivery Party					;;
        II,// Issuer Of Invoice					;;
        IV,// Invoicee							;;
        MF,// Manufacturer Of Goods				;;
        RB,// Receiving Financial Institution	;
        RH,// Seller's Financial Institution	;
        SF,// Ship From							;
        SU,// Supplier							;;
        UD // Ultimate Customer					;;
    }
    //3055
    enum CodeListResponsibleAgencyCoded
    {
        _9,     // EAN (International Article Numbering association)
        _89,    // Assigned by distributor
        _91,    // Assigned by seller or seller's agent
        _157    // NO, Norwegian Customs
    }

    //3139
    enum ContactFunctionCoded
    {
        AD,// Accounting contact
        PD // Purchasing contact
    }

    //4055
    enum TermsOfDeliveryFunctionCoded
    {
        _3,     // Price and despatch condition
    }

    //4183
    enum SpecialConditionsCoded
    {
        _6,     // Subject to bonus
    }

    //4279
    enum PaymentTermsTypeQualifier
    {
        _1,     // Basic
        _3,     // Fixed date
        _20,    // Penalty terms
        _22     // Discount
    }

    //4347
    enum ProductIdFunctionQualifier
    {
        _1,// Additional identification
        _5 // Product identification
    }
    //4451
    enum TextSubjectQualifier
    {
        CHG,// Change information
        INV,// Invoice instruction
        PUR,// Purchasing information
        ZZZ // Mutually defined
    }

    //5025
    enum MonetaryAmountTypeQualifier
    {
        _8,// Allowance or charge amount
        _9,// Amount due/amount payable
        _21,// Cash discount
        _66,// Goods item total
        _79,// Total line items amount
        _86,// Message total monetary amount
        _124,// Tax amount
        _125,// Taxable amount
        _129,// Total amount subject to payment discount
        _131,// Total charges/allowances
        _150,// Value added tax
        _165,// Adjustment amount
        _176,// Message total duty/tax/fee amount
        _203,// Line item amount
        _259,// Total charges
        _260,// Total allowances
        NET// Net total
    }

    //5125
    enum PriceQualifier
    {
        AAA,// Calculation net
        AAB,// Calculation gross
        XXX// Information price
    }

    //5153
    enum DutyTaxFeeTypeCoded
    {
        VAT // Value added tax
    }

    //5245
    enum PercentageQualifier
    {
        _3,// Allowance or charge
        _12,// Discount
        _15 // Penalty percentage
    }

    //5249
    enum PercentageBasisCoded
    {
        _1, // Per unit
        _13 // Invoice value
    }

    //5273
    enum DutyTaxFeeRateBasisIdentification
    {
        _2,// Weight
        _3 //Quantity
    }

    //5283
    enum DutyTaxFeeFunctionQualifier
    {
        _7 // Tax
    }
    //5305 
    enum DutyTaxFeeCategoryCoded
    {
        E,// Exempt from tax
        S // Standard rate
    }
    //5387
    enum PriceTypeQualifier
    {
        AAK,// New price
        AAL,// Old price
        DPR,// Discount price
        SRP // Suggested retail price
    }
    //5419
    enum RateTypeQualifier
    {
        _1, // Allowance rate
        _2  //Charge rate
    }
    //5463
    enum AllowanceOrChargeQualifier
    {
        A,// Allowance
        C // Charge
    }
    //6063
    enum QuantityQualifier
    {
        _1,// Discrete quantity
        _21,// Ordered quantity
        _46,// Pieces delivered
        _47,// Invoiced quantity
        _59 // Numbers of consumer units in the traded unit
    }

    //6069
    enum ControlQualifier
    {
        _2 // Number of line items in message
    }

    //6343
    enum CurrencyQualifier
    {
        _4,//Invoicing currency
        _11 // Payment currency
    }
    //6347
    enum CurrencyDetailsQualifier
    {
        _2, // Reference currency
        _3 //Target currency
    }

    enum MeasureUnitQualifier
    {
        GR,     // Gram
        KGM,    // Kilo
        LTR,    // Litres
        MTQ,    // Cubic meter
        MTR,    // Meter
        PCE     // Pieces
    }

    enum ItemDescriptionIdentification
    {
        CU,     // Consumer unit
        DU,     // Despatch unit
        IT,     // Intermediate unit
        TU,     // Traded unit
        VQ      // Variable quantity product
    }

    enum ItemDescriptionTypeCoded
    {
        C,      // Code (from industry code list)
        FL      // Free form - Long item description
    }

    enum ItemNumberTypeCoded
    {
        BP,     // Buyer's part number
        EFO,    // Elektronikkforbundet
        EN,     // International Article Numbering Association (EAN)
        GD,     // Norwegian Grocery Sector standard goods group
        GN,     // ENVA
        GU,     // Supplier's standard goods group
        NB,     // Batch number
        PV,     // Promotional variant number
        SA,     // Supplier's article number
        UP,     // UPC (Universal product code)
    }

    #endregion

    public enum DiscountTreatment
    {
        UN,
        UG,
        TN,
        TG
    }

    public enum InvoiceStatus
    {
        _9,
        _10,
        _53
    }

    public enum TaxTreatment
    {
        NIL,
        GIL,
        NLL,
        GLL,
        NON
    }


    /// <summary>
    /// The Delimiters enumeration lists the possible delimiters.
    /// </summary>
    enum Delimiters
    {
        APOS = 39,
        PLUS = 43,
        COLON = 58
    }
    /// <summary>
    /// The EDI enumeration list the different sections of a message.
    /// </summary>
    enum EDI
    {
        Document,
        Segment,
        Field
    }

    /// <summary>
    /// The FileType enumeration lists the different files this handler can load.
    /// </summary>
    enum FileType
    {
        EDI,
        XML,
        XSD,
        XSL
    }
}