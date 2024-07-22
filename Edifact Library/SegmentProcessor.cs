//--------------------------------------------------------------
// <CreatedBy>
//     Anthony R Yates
//	   July 08, 2005
//     Runtime Version: 1.1.4322.2032
//	   
//     Contains SegmentProcessor Class (see class summary).
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

namespace EDIFACT
{
    /// <summary>
    /// SegmentProcessor is used by EDIFACT message classes (for example: DESADV, ORDERS, ORDRSP, etc) to populate
    /// itself with Segment objects that represent the complete EDIFACT message. SegmentProcessor accepts in an array
    /// of <see cref="Segment"/>, that constitute a complete EDI message.
    /// Each field within a segement populates a segment class, which then gets passed off to the Add method
    /// in the calling EDI message class. The Add method of a calling EDIFACT message class must first be added to 
    /// the <see cref="AddFunction"/> property, an <see cref="AddSegmentDelegate"/>, prior to calling <see cref="ProcessSegments"/>.
    /// <code>
    /// Uses:
    /// SegmentProcessor sp = new SegmentProcessor(new AddSegmentDelegate(INVOIC.Add));
    /// sp.ProcessSegments(segments);
    /// <c>-or-</c>
    /// SegmentProcessor sp = new SegmentProcessor();
    /// sp.AddFunction = new AddSegmentDelegate(ORDRSP.Add);
    /// sp.ProcessSegments(segments);
    /// <c>-or-</c>
    /// AddSegmentDelegate segmentDelegate = new AddSegmentDelegate(DESADV.Add);
    /// SegmentProcessor sp = new SegmentProcessor();
    /// sp.AddFunction = segmentDelegate;
    /// sp.ProcessSegments(segments);
    /// </code>
    /// </summary>
    public class SegmentProcessor
    {

        #region Public Properties

        AddSegmentDelegate addFunction;
        /// <summary>
        /// An <see cref="AddSegmentDelegate"/> that accepts a method with the signature:
        /// methodName(<see cref="object"/> type, <see cref="SegmentType"/> segmentObject).
        /// </summary>
        public AddSegmentDelegate AddFunction
        {
            get { return addFunction; }
            set { addFunction = new AddSegmentDelegate(value); }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public SegmentProcessor()
        {

        }

        /// <summary>
        /// Accepts an <see cref="AddSegmentDelegate"/> that poplulates the <see cref="AddFunction"/> property.
        /// </summary>
        /// <param name="myDelegate">An AddSegmentDelegate used to populate the SegmentProcessors <see cref="AddFunction"/> property.</param>
        public SegmentProcessor(AddSegmentDelegate myDelegate)
        {
            this.addFunction = myDelegate;
        }

        //		/// <summary>
        //		/// Accepts an <see cref="AddSegmentDelegate"/> and an array of <see cref="Segment"/>.
        //		/// </summary>
        //		/// <param name="addFunction">Poplulates the <see cref="AddFunction"/> property</param>
        //		/// <param name="segmentArray">An array of <see cref="Segment"/> used to populate an EDIFACT message.</param>
        //		public SegmentProcessor(AddSegmentDelegate addFunction, Segment [] segmentArray)
        //		{
        //			this.AddFunction += addFunction;
        //			ProcessSegments(segmentArray);
        //		}

        #endregion

        /// <summary>
        /// Accepts an array of <see cref="Segment"/> used to create and populate each segment object with
        /// corresponding field values. Each populated segement object gets passed in SegmentProcessors <see cref="AddFunction"/> property,
        /// an <see cref="AddSegmentDelegate"/> which contains a pointer to the calling EDIFACT messages Add method.
        /// </summary>
        /// <param name="segmentArray">An array of <see cref="Segment"/> used to populate an EDIFACT message.</param>
        public void ProcessSegments(Segment[] segmentArray)
        {
            DateTime startTime = HighResClock.Now;
            for (int i = 0; i < segmentArray.Length; i++)
            {
                try
                {
                    switch (segmentArray[i].Name)
                    {
                        case "ALC":
                            ALC alc = new ALC();
                            for (int j = 0; j < segmentArray[i].Fields.Count; j++)
                            {
                                switch (j)
                                {
                                    case 0:
                                        {
                                            alc.allowanceChargeQualifier = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                    case 1:
                                        {
                                            alc.allowanceChargeInformation = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                    case 2:
                                        {
                                            alc.settlementCoded = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                    case 3:
                                        {
                                            alc.calculationSequenceIndicator = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                    case 4:
                                        {
                                            alc.specialServicesCoded = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                    case 5:
                                        {
                                            if (segmentArray[i].Fields.Item(j).Value != null && segmentArray[i].Fields.Item(j).Value.Length > 0)
                                                alc.codeListQualifier = Convert.ToUInt16(segmentArray[i].Fields.Item(j).Value);
                                            break;
                                        }
                                    case 6:
                                        {
                                            if (segmentArray[i].Fields.Item(j).Value != null && segmentArray[i].Fields.Item(j).Value.Length > 0)
                                                alc.codeListResponsibleAgency = Convert.ToByte(segmentArray[i].Fields.Item(j).Value);
                                            break;
                                        }
                                    case 7:
                                        {
                                            alc.specialService = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                }
                            }
                            addFunction(SegmentType.ALC, alc);
                            alc = null;
                            break;
                        case "ALI":
                            ALI ali = new ALI();
                            for (int j = 0; j < segmentArray[i].Fields.Count; j++)
                            {
                                switch (j)
                                {
                                    case 0:
                                        {
                                            if (segmentArray[i].Fields.Item(j).Value != null && segmentArray[i].Fields.Item(j).Value.Length > 0)
                                                ali.countryOfOrigin = Convert.ToUInt16(segmentArray[i].Fields.Item(j).Value);
                                            break;
                                        }
                                    case 1:
                                        {
                                            if (segmentArray[i].Fields.Item(j).Value != null && segmentArray[i].Fields.Item(j).Value.Length > 0)
                                                ali.dutyRegimeType = Convert.ToUInt16(segmentArray[i].Fields.Item(j).Value);
                                            break;
                                        }
                                    case 2:
                                        {
                                            if (segmentArray[i].Fields.Item(j).Value != null && segmentArray[i].Fields.Item(j).Value.Length > 0)
                                                ali.specialConditions = Convert.ToUInt16(segmentArray[i].Fields.Item(j).Value);
                                            break;
                                        }
                                }
                            }
                            addFunction(SegmentType.ALI, ali);
                            ali = null;
                            break;
                        case "BGM":
                            BGM bgm = new BGM();
                            for (int j = 0; j < segmentArray[i].Fields.Count; j++)
                            {
                                switch (j)
                                {
                                    case 0:
                                        {
                                            bgm.messageName = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                    case 1:
                                        {
                                            bgm.messageNumber = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                    case 2:
                                        {
                                            bgm.codeListResponsibleAgency = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                    case 3:
                                        {
                                            bgm.responseType = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                }
                            }
                            addFunction(SegmentType.BGM, bgm);
                            bgm = null;
                            break;
                        case "CNT":
                            CNT cnt = new CNT();
                            for (int j = 0; j < segmentArray[i].Fields.Count; j++)
                            {
                                switch (j)
                                {
                                    case 0:
                                        {
                                            if (segmentArray[i].Fields.Item(j).Value != null && segmentArray[i].Fields.Item(j).Value.Length > 0)
                                                cnt.controlQualifier = Convert.ToByte(segmentArray[i].Fields.Item(j).Value);
                                            break;
                                        }
                                    case 1:
                                        {
                                            if (segmentArray[i].Fields.Item(j).Value != null && segmentArray[i].Fields.Item(j).Value.Length > 0)
                                                cnt.controlValue = Convert.ToUInt16(segmentArray[i].Fields.Item(j).Value);
                                            break;
                                        }
                                }
                            }
                            addFunction(SegmentType.CNT, cnt);
                            cnt = null;
                            break;
                        case "COM":
                            COM com = new COM();
                            for (int j = 0; j < segmentArray[i].Fields.Count; j++)
                            {
                                switch (j)
                                {
                                    case 0:
                                        {
                                            com.communicationNumber = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                    case 1:
                                        {
                                            com.channelQualifier = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                }
                            }
                            addFunction(SegmentType.COM, com);
                            com = null;
                            break;
                        case "CTA":
                            CTA cta = new CTA();
                            for (int j = 0; j < segmentArray[i].Fields.Count; j++)
                            {
                                switch (j)
                                {
                                    case 0:
                                        {
                                            cta.contactFunctionCoded = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                    case 1:
                                        {
                                            cta.departmentOrEmployeeIDNumber = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                    case 2:
                                        {
                                            cta.deptOrEmployee = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                }
                            }
                            addFunction(SegmentType.CTA, cta);
                            cta = null;
                            break;
                        case "CPS":
                            CPS cps = new CPS();
                            for (int j = 0; j < segmentArray[i].Fields.Count; j++)
                            {
                                switch (j)
                                {
                                    case 0:
                                        {
                                            if (segmentArray[i].Fields.Item(j).Value != null && segmentArray[i].Fields.Item(j).Value.Length > 0)
                                                cps.hierarchicalIdNumber = Convert.ToByte(segmentArray[i].Fields.Item(j).Value);
                                            break;
                                        }
                                    case 1:
                                        {
                                            if (segmentArray[i].Fields.Item(j).Value != null && segmentArray[i].Fields.Item(j).Value.Length > 0)
                                                cps.hierachticalParentID = Convert.ToByte(segmentArray[i].Fields.Item(j).Value);
                                            break;
                                        }
                                    case 2:
                                        {
                                            if (segmentArray[i].Fields.Item(j).Value != null && segmentArray[i].Fields.Item(j).Value.Length > 0)
                                                cps.packagingLevelCoded = Convert.ToUInt16(segmentArray[i].Fields.Item(j).Value);
                                            break;
                                        }
                                }
                            }
                            addFunction(SegmentType.CPS, cps);
                            cps = null;
                            break;
                        case "CUX":
                            CUX cux = new CUX();
                            if (segmentArray[i].Fields.Item(0).Value == "2")
                            {
                                for (int j = 0; j < segmentArray[i].Fields.Count; j++)
                                {
                                    switch (j)
                                    {
                                        case 0:
                                            {
                                                if (segmentArray[i].Fields.Item(j).Value != null && segmentArray[i].Fields.Item(j).Value.Length > 0)
                                                    cux.referenceDetailsQualifier = Convert.ToUInt16(segmentArray[i].Fields.Item(j).Value);
                                                break;
                                            }
                                        case 1:
                                            {
                                                cux.referenceCoded = segmentArray[i].Fields.Item(j).Value;
                                                break;
                                            }
                                        case 2:
                                            {
                                                if (segmentArray[i].Fields.Item(j).Value != null && segmentArray[i].Fields.Item(j).Value.Length > 0)
                                                    cux.invoicingQualifier = Convert.ToUInt16(segmentArray[i].Fields.Item(j).Value);
                                                break;
                                            }
                                        case 3:
                                            {
                                                cux.rateOfExchange = segmentArray[i].Fields.Item(j).Value;
                                                break;
                                            }
                                        case 4:
                                            {
                                                cux.currencyMarketExchange = segmentArray[i].Fields.Item(j).Value;
                                                break;
                                            }
                                    }
                                }
                            }
                            else if (segmentArray[i].Fields.Item(0).Value == "3")
                            {
                                for (int j = 0; j < segmentArray[i].Fields.Count; j++)
                                {
                                    switch (j)
                                    {
                                        case 0:
                                            {
                                                if (segmentArray[i].Fields.Item(j).Value != null && segmentArray[i].Fields.Item(j).Value.Length > 0)
                                                    cux.targetDetailsQualifier = Convert.ToUInt16(segmentArray[i].Fields.Item(j).Value);
                                                break;
                                            }
                                        case 1:
                                            {
                                                cux.targetCoded = segmentArray[i].Fields.Item(j).Value;
                                                break;
                                            }
                                        case 2:
                                            {
                                                if (segmentArray[i].Fields.Item(j).Value != null && segmentArray[i].Fields.Item(j).Value.Length > 0)
                                                    cux.paymentQualifier = Convert.ToUInt16(segmentArray[i].Fields.Item(j).Value);
                                                break;
                                            }
                                        case 3:
                                            {
                                                cux.rateOfExchange = segmentArray[i].Fields.Item(j).Value;
                                                break;
                                            }
                                        case 4:
                                            {
                                                cux.currencyMarketExchange = segmentArray[i].Fields.Item(j).Value;
                                                break;
                                            }
                                    }
                                }
                            }
                            addFunction(SegmentType.CUX, cux);
                            cux = null;
                            break;
                        case "DTM":
                            DTM dtm = new DTM();
                            for (int j = 0; j < segmentArray[i].Fields.Count; j++)
                            {
                                switch (j)
                                {
                                    case 0:
                                        {
                                            dtm.dateTimePeriodQualifier = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                    case 1:
                                        {
                                            dtm.dateTimePeriod = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                    case 2:
                                        {
                                            dtm.dateTimePeriodFormatQualifier = Convert.ToUInt16(segmentArray[i].Fields.Item(j).Value);
                                            break;
                                        }
                                }
                            }
                            addFunction(SegmentType.DTM, dtm);
                            dtm = null;
                            break;
                        case "ERC":
                            ERC erc = new ERC();
                            for (int j = 0; j < segmentArray[i].Fields.Count; j++)
                            {
                                switch (j)
                                {
                                    case 0:
                                        {
                                            if (segmentArray[i].Fields.Item(j).Value != null && segmentArray[i].Fields.Item(j).Value.Length > 0)
                                                erc.failCode = Convert.ToByte(segmentArray[i].Fields.Item(j).Value);
                                            break;
                                        }
                                }
                            }
                            addFunction(SegmentType.ERC, erc);
                            erc = null;
                            break;
                        case "FII":
                            FII fii = new FII();
                            for (int j = 0; j < segmentArray[i].Fields.Count; j++)
                            {
                                switch (j)
                                {
                                    case 0:
                                        {
                                            fii.partyQualifier = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                    case 1:
                                        {
                                            if (segmentArray[i].Fields.Item(j).Value != null && segmentArray[i].Fields.Item(j).Value.Length > 0)
                                                fii.accountNumber = Convert.ToUInt64(segmentArray[i].Fields.Item(j).Value);
                                            break;
                                        }
                                }
                            }
                            addFunction(SegmentType.FII, fii);
                            fii = null;
                            break;
                        case "FTX":
                            FTX ftx = new FTX();
                            for (int j = 0; j < segmentArray[i].Fields.Count; j++)
                            {
                                switch (j)
                                {
                                    case 0:
                                        {
                                            ftx.textSubjectQualifier = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                    case 1:
                                        {
                                            if (segmentArray[i].Fields.Item(j).Value != null && segmentArray[i].Fields.Item(j).Value.Length > 0)
                                                ftx.freeTextCoded = Convert.ToUInt16(segmentArray[i].Fields.Item(j).Value);
                                            break;
                                        }
                                    //									case 2:	
                                    //									{ //TEXT???
                                    //										if(segmentArray[i].Fields.Item(j).Value != null && segmentArray[i].Fields.Item(j).Value.Length > 0)
                                    //											ftx.codeListQualifier = Convert.ToUInt16(segmentArray[i].Fields.Item(j).Value);
                                    //										break;
                                    //									}
                                    case 2:
                                        {
                                            ftx.textLiteral = segmentArray[i].Fields.Item(j).Value + "\n";
                                            break;
                                        }
                                    case 3:
                                        {
                                            ftx.textLiteral = segmentArray[i].Fields.Item(j).Value + "\n";
                                            break;
                                        }
                                }
                            }
                            addFunction(SegmentType.FTX, ftx);
                            ftx = null;
                            break;
                        case "GIN":
                            GIN gin = new GIN();
                            for (int j = 0; j < segmentArray[i].Fields.Count; j++)
                            {
                                switch (j)
                                {
                                    case 0:
                                        {
                                            gin.identityNumberQualifier = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                    case 1:
                                        {
                                            gin.identityNumber = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                }
                            }

                            addFunction(SegmentType.GIN, gin);
                            gin = null;
                            break;
                        case "HAN":
                            HAN han = new HAN();
                            for (int j = 0; j < segmentArray[i].Fields.Count; j++)
                            {
                                switch (j)
                                {
                                    case 0:
                                        {
                                            han.handlingInstructionsCoded = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                    case 1:
                                        {
                                            if (segmentArray[i].Fields.Item(j).Value != null && segmentArray[i].Fields.Item(j).Value.Length > 0)
                                                han.codeListQualifier = Convert.ToUInt16(segmentArray[i].Fields.Item(j).Value);
                                            break;
                                        }
                                    case 2:
                                        {
                                            if (segmentArray[i].Fields.Item(j).Value != null && segmentArray[i].Fields.Item(j).Value.Length > 0)
                                                han.codeListResponsibleAgency = Convert.ToByte(segmentArray[i].Fields.Item(j).Value);
                                            break;
                                        }
                                    case 3:
                                        {
                                            han.handlingInstructions = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                    case 4:
                                        {
                                            han.hazMatClassCodeID = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                    case 5:
                                        {
                                            if (segmentArray[i].Fields.Item(j).Value != null && segmentArray[i].Fields.Item(j).Value.Length > 0)
                                                han.hazMatcodeListQualifier = Convert.ToUInt16(segmentArray[i].Fields.Item(j).Value);
                                            break;
                                        }
                                    case 6:
                                        {
                                            if (segmentArray[i].Fields.Item(j).Value != null && segmentArray[i].Fields.Item(j).Value.Length > 0)
                                                han.hazMatcodeListResponsibleAgency = Convert.ToByte(segmentArray[i].Fields.Item(j).Value);
                                            break;
                                        }
                                }
                            }
                            addFunction(SegmentType.HAN, han);
                            han = null;
                            break;
                        case "IMD":
                            IMD imd = new IMD();

                            if (segmentArray[i].Fields.Item(0).Value == "C")
                            {
                                for (int j = 0; j < segmentArray[i].Fields.Count; j++)
                                {
                                    switch (j)
                                    {
                                        case 0:
                                            {
                                                imd.itemDescriptionType = segmentArray[i].Fields.Item(j).Value;
                                                break;
                                            }
                                        case 1:
                                            {
                                                //	imd.itemCharacteristic = segmentArray[i].Fields.Item(j).Value;
                                                break;
                                            }
                                        case 2:
                                            {
                                                imd.itemDescriptionID = segmentArray[i].Fields.Item(j).Value;
                                                break;
                                            }
                                    }
                                }
                            }
                            else
                            {
                                for (int j = 0; j < segmentArray[i].Fields.Count; j++)
                                {
                                    switch (j)
                                    {
                                        case 0:
                                            {
                                                imd.itemDescriptionType = segmentArray[i].Fields.Item(j).Value;
                                                break;
                                            }
                                        case 1:
                                            {
                                                //imd.itemCharacteristic = segmentArray[i].Fields.Item(j).Value;
                                                break;
                                            }
                                        case 2:
                                            {
                                                //imd.itemDescriptionId = segmentArray[i].Fields.Item(j).Value;
                                                break;
                                            }
                                        case 5:
                                            {
                                                imd.itemDescription = segmentArray[i].Fields.Item(j).Value;
                                                break;
                                            }
                                    }
                                }
                            }
                            addFunction(SegmentType.IMD, imd);
                            imd = null;
                            break;
                        case "LIN":
                            LIN lin = new LIN();
                            for (int j = 0; j < segmentArray[i].Fields.Count; j++)
                            {
                                switch (j)
                                {
                                    case 0:
                                        {
                                            if (segmentArray[i].Fields.Item(j).Value != null && segmentArray[i].Fields.Item(j).Value.Length > 0)
                                                lin.lineItemNumber = Convert.ToByte(segmentArray[i].Fields.Item(j).Value);
                                            break;
                                        }
                                    case 1:
                                        {
                                            if (segmentArray[i].Fields.Item(j).Value != null && segmentArray[i].Fields.Item(j).Value.Length > 0)
                                                lin.actionRequest = Convert.ToByte(segmentArray[i].Fields.Item(j).Value);
                                            break;
                                        }
                                    case 2:
                                        {
                                            lin.itemNumber = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                    case 3:
                                        {
                                            lin.itemNumberType = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                }
                            }
                            //lineNumber = Convert.ToInt32(lin.lineItemNumber);
                            addFunction(SegmentType.LIN, lin);
                            lin = null;
                            break;
                        case "LOC":
                            LOC loc = new LOC();
                            for (int j = 0; j < segmentArray[i].Fields.Count; j++)
                            {
                                switch (j)
                                {
                                    case 0:
                                        {
                                            if (segmentArray[i].Fields.Item(j).Value != null && segmentArray[i].Fields.Item(j).Value.Length > 0)
                                                loc.placeLocationQualifier = Convert.ToByte(segmentArray[i].Fields.Item(j).Value);
                                            break;
                                        }
                                    case 1:
                                        {
                                            loc.placeLocationID = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                    case 2:
                                        {
                                            if (segmentArray[i].Fields.Item(j).Value != null && segmentArray[i].Fields.Item(j).Value.Length > 0)
                                                loc.codeListQualifier = Convert.ToUInt16(segmentArray[i].Fields.Item(j).Value);
                                            break;
                                        }
                                    case 3:
                                        {
                                            if (segmentArray[i].Fields.Item(j).Value != null && segmentArray[i].Fields.Item(j).Value.Length > 0)
                                                loc.codeListResponsibleAgency = Convert.ToByte(segmentArray[i].Fields.Item(j).Value);
                                            break;
                                        }
                                    case 4:
                                        {
                                            loc.placeLocation = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                        //case 5:	{
                                        //	loc.codeListQualifier = Convert.ToUInt16(segmentArray[i].Fields.Item(j).Value);
                                        //	break;
                                        //}
                                        //case 6:	{
                                        //	loc.codeListQualifier = Convert.ToUInt16(segmentArray[i].Fields.Item(j).Value);
                                        //	break;
                                        //}
                                }
                            }
                            addFunction(SegmentType.LOC, loc);
                            loc = null;
                            break;
                        case "MEA":
                            MEA mea = new MEA();
                            for (int j = 0; j < segmentArray[i].Fields.Count; j++)
                            {
                                switch (j)
                                {
                                    case 0:
                                        {
                                            mea.measurementApplicationQualifier = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                    case 1:
                                        {
                                            mea.measurementDimensionCoded = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                    case 2:
                                        {
                                            mea.measurementSignificanceCoded = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                    case 3:
                                        {
                                            mea.measurementAttributeCoded = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                    case 4:
                                        {
                                            mea.measureUnitQualifier = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                    case 5:
                                        {
                                            mea.measurementValue = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                    case 6:
                                        {
                                            mea.rangeMinimum = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                    case 7:
                                        {
                                            mea.rangeMaximum = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                    case 8:
                                        {
                                            if (segmentArray[i].Fields.Item(j).Value != null && segmentArray[i].Fields.Item(j).Value.Length > 0)
                                                mea.surfaceLayerIndicator = Convert.ToUInt16(segmentArray[i].Fields.Item(j).Value);
                                            break;
                                        }
                                }
                            }
                            addFunction(SegmentType.MEA, mea);
                            mea = null;
                            break;
                        case "MOA":
                            MOA moa = new MOA();
                            for (int j = 0; j < segmentArray[i].Fields.Count; j++)
                            {
                                switch (j)
                                {
                                    case 0:
                                        {
                                            moa.amountTypeQualifier = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                    case 1:
                                        {
                                            moa.monetaryAmount = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                }
                            }
                            addFunction(SegmentType.MOA, moa);
                            moa = null;
                            break;
                        case "NAD":
                            NAD nad = new NAD();
                            for (int j = 0; j < segmentArray[i].Fields.Count; j++)
                            {
                                switch (j)
                                {
                                    case 0:
                                        {
                                            nad.partyQualifier = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                    case 1:
                                        {
                                            nad.partyIDIdentification = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                    case 2:
                                        {
                                            if (segmentArray[i].Fields.Item(j).Value != null && segmentArray[i].Fields.Item(j).Value.Length > 0)
                                                nad.codeListQualifier = Convert.ToUInt16(segmentArray[i].Fields.Item(j).Value);
                                            break;
                                        }
                                    case 3:
                                        {
                                            if (segmentArray[i].Fields.Item(j).Value != null && segmentArray[i].Fields.Item(j).Value.Length > 0)
                                                nad.codeListResponsibleAgency = Convert.ToUInt16(segmentArray[i].Fields.Item(j).Value);
                                            break;
                                        }
                                    case 4:
                                        {
                                            nad.nameAndAddress = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                    case 5:
                                        {
                                            nad.partyName = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                    case 6:
                                        {
                                            nad.streetName = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                    case 7:
                                        {
                                            nad.cityName = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                    case 8:
                                        {
                                            nad.countrySubEntityID = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                    case 9:
                                        {
                                            nad.postCodeID = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                    case 10:
                                        {
                                            nad.countryCoded = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                }
                            }
                            addFunction(SegmentType.NAD, nad);
                            nad = null;
                            break;
                        case "PAC":
                            PAC pac = new PAC();
                            for (int j = 0; j < segmentArray[i].Fields.Count; j++)
                            {
                                switch (j)
                                {
                                    case 0:
                                        {
                                            if (segmentArray[i].Fields.Item(j).Value != null && segmentArray[i].Fields.Item(j).Value.Length > 0)
                                                pac.numberOfPackages = Convert.ToUInt16(segmentArray[i].Fields.Item(j).Value);
                                            break;
                                        }
                                    case 1:
                                        {
                                            if (segmentArray[i].Fields.Item(j).Value != null && segmentArray[i].Fields.Item(j).Value.Length > 0)
                                                pac.packagingLevel = Convert.ToUInt16(segmentArray[i].Fields.Item(j).Value);
                                            break;
                                        }
                                    case 2:
                                        {
                                            pac.packageTypeID = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                    //	case 3: {
                                    //		if(segmentArray[i].Fields.Item(j).Value != null && segmentArray[i].Fields.Item(j).Value.Length > 0)
                                    //			pac.codeListQualifier = Convert.ToUInt16(segmentArray[i].Fields.Item(j).Value);
                                    //			break;
                                    //		}
                                    //	case 4: {
                                    //		if(segmentArray[i].Fields.Item(j).Value != null && segmentArray[i].Fields.Item(j).Value.Length > 0)
                                    //			pac.codeListResponsibleAgency = Convert.ToByte(segmentArray[i].Fields.Item(j).Value);
                                    //			break;
                                    //		}
                                    case 3:
                                        {
                                            pac.typeOfPackages = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                }
                            }
                            addFunction(SegmentType.PAC, pac);
                            pac = null;
                            break;
                        case "PAT":
                            PAT pat = new PAT();
                            for (int j = 0; j < segmentArray[i].Fields.Count; j++)
                            {
                                switch (j)
                                {
                                    case 0:
                                        {
                                            if (segmentArray[i].Fields.Item(j).Value != null && segmentArray[i].Fields.Item(j).Value.Length > 0)
                                                pat.termsTypeQualifier = Convert.ToByte(segmentArray[i].Fields.Item(j).Value);
                                            break;
                                        }
                                    case 1:
                                        {
                                            if (segmentArray[i].Fields.Item(j).Value != null && segmentArray[i].Fields.Item(j).Value.Length > 0)
                                                pat.timeReferenceCoded = Convert.ToUInt16(segmentArray[i].Fields.Item(j).Value);
                                            break;
                                        }
                                    case 2:
                                        {
                                            if (segmentArray[i].Fields.Item(j).Value != null && segmentArray[i].Fields.Item(j).Value.Length > 0)
                                                pat.timeRelationCoded = Convert.ToUInt16(segmentArray[i].Fields.Item(j).Value);
                                            break;
                                        }
                                    case 3:
                                        {
                                            if (segmentArray[i].Fields.Item(j).Value != null && segmentArray[i].Fields.Item(j).Value.Length > 0)
                                                pat.typeOfPeriodCoded = Convert.ToUInt16(segmentArray[i].Fields.Item(j).Value);
                                            break;
                                        }
                                    case 4:
                                        {
                                            if (segmentArray[i].Fields.Item(j).Value != null && segmentArray[i].Fields.Item(j).Value.Length > 0)
                                                pat.numberOfPeriods = Convert.ToUInt16(segmentArray[i].Fields.Item(j).Value);
                                            break;
                                        }
                                }
                            }
                            addFunction(SegmentType.PAT, pat);
                            pat = null;
                            break;
                        case "PCD":
                            PCD pcd = new PCD();
                            for (int j = 0; j < segmentArray[i].Fields.Count; j++)
                            {
                                switch (j)
                                {
                                    case 0:
                                        {
                                            if (segmentArray[i].Fields.Item(j).Value != null && segmentArray[i].Fields.Item(j).Value.Length > 0)
                                                pcd.percentageQualifier = Convert.ToUInt16(segmentArray[i].Fields.Item(j).Value);
                                            break;
                                        }
                                    case 1:
                                        {
                                            pcd.percentage = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                }
                            }
                            addFunction(SegmentType.PCD, pcd);
                            pcd = null;
                            break;
                        case "PCI":
                            PCI pci = new PCI();
                            for (int j = 0; j < segmentArray[i].Fields.Count; j++)
                            {
                                switch (j)
                                {
                                    case 0:
                                        {
                                            pci.markingInstructionsCoded = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                    case 1:
                                        {
                                            pci.shippingMarks = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                    case 2:
                                        {
                                            if (segmentArray[i].Fields.Item(j).Value != null && segmentArray[i].Fields.Item(j).Value.Length > 0)
                                                pci.containerPackageStatus = Convert.ToUInt16(segmentArray[i].Fields.Item(j).Value);
                                            break;
                                        }
                                }
                            }
                            //containerPackageStatus
                            addFunction(SegmentType.PCI, pci);
                            pci = null;
                            break;
                        case "PGI":
                            PGI pgi = new PGI();
                            for (int j = 0; j < segmentArray[i].Fields.Count; j++)
                            {
                                switch (j)
                                {
                                    case 0:
                                        {
                                            if (segmentArray[i].Fields.Item(j).Value != null && segmentArray[i].Fields.Item(j).Value.Length > 0)
                                                pgi.priceTariffTypeCoded = Convert.ToByte(segmentArray[i].Fields.Item(j).Value);
                                            break;
                                        }
                                }
                            }
                            addFunction(SegmentType.PGI, pgi);
                            pgi = null;
                            break;
                        case "PIA":
                            PIA pia = new PIA();
                            for (int j = 0; j < segmentArray[i].Fields.Count; j++)
                            {
                                switch (j)
                                {
                                    case 0:
                                        {
                                            if (segmentArray[i].Fields.Item(j).Value != null && segmentArray[i].Fields.Item(j).Value.Length > 0)
                                                pia.idFunctionQualifier = Convert.ToUInt16(segmentArray[i].Fields.Item(j).Value);
                                            break;
                                        }
                                    case 1:
                                        {
                                            pia.itemNumber = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                    case 2:
                                        {
                                            pia.itemNumberType = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                    case 3:
                                        {
                                            if (segmentArray[i].Fields.Item(j).Value != null && segmentArray[i].Fields.Item(j).Value.Length > 0)
                                                pia.codeListQualifier = Convert.ToUInt16(segmentArray[i].Fields.Item(j).Value);
                                            break;
                                        }
                                    case 4:
                                        {
                                            if (segmentArray[i].Fields.Item(j).Value != null && segmentArray[i].Fields.Item(j).Value.Length > 0)
                                                pia.codeListResponsAgency = Convert.ToByte(segmentArray[i].Fields.Item(j).Value);
                                            break;
                                        }
                                }
                            }
                            addFunction(SegmentType.PIA, pia);
                            pia = null;
                            break;
                        case "PIT":
                            PIT pit = new PIT();
                            for (int j = 0; j < segmentArray[i].Fields.Count; j++)
                            {
                                switch (j)
                                {
                                    case 0:
                                        {
                                            if (segmentArray[i].Fields.Item(j).Value != null && segmentArray[i].Fields.Item(j).Value.Length > 0)
                                                pit.lineItemNumber = Convert.ToUInt16(segmentArray[i].Fields.Item(j).Value);
                                            break;
                                        }
                                    case 1:
                                        {   // TODO : Localization
                                            if (segmentArray[i].Fields.Item(j).Value != null && segmentArray[i].Fields.Item(j).Value.Length > 0)
                                                pit.actionRequestNotificationCoded = Convert.ToUInt16(segmentArray[i].Fields.Item(j).Value);
                                            break;
                                        }
                                    case 2:
                                        {
                                            if (segmentArray[i].Fields.Item(j).Value != null && segmentArray[i].Fields.Item(j).Value.Length > 0)
                                                pit.articleAvailabilityCoded = Convert.ToUInt16(segmentArray[i].Fields.Item(j).Value);
                                            break;
                                        }
                                    case 3:
                                        {
                                            if (segmentArray[i].Fields.Item(j).Value != null && segmentArray[i].Fields.Item(j).Value.Length > 0)
                                                pit.subLineIndicatorCoded = Convert.ToByte(segmentArray[i].Fields.Item(j).Value);
                                            break;
                                        }
                                }
                            }
                            addFunction(SegmentType.PIT, pit);
                            pit = null;
                            break;
                        case "PRI":
                            PRI pri = new PRI();
                            for (int j = 0; j < segmentArray[i].Fields.Count; j++)
                            {
                                switch (j)
                                {
                                    case 0:
                                        {
                                            pri.priceQualifier = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                    case 1:
                                        {
                                            pri.price = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                    case 2:
                                        {
                                            pri.priceType = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                    case 3:
                                        {
                                            pri.priceTypeQualifier = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                    case 4:
                                        {
                                            pri.unitPriceBasis = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                    case 5:
                                        {
                                            pri.measureUnitQualifier = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                    case 6:
                                        {
                                            pri.subLinePriceChange = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                }
                            }
                            addFunction(SegmentType.PRI, pri);
                            pri = null;
                            break;
                        case "QTY":
                            QTY qty = new QTY();
                            for (int j = 0; j < segmentArray[i].Fields.Count; j++)
                            {
                                switch (j)
                                {
                                    case 0:
                                        {
                                            qty.qtyQualifier = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                    case 1:
                                        {   // TODO : Localization
                                            //if(segmentArray[i].Fields.Item(j).Value != null && segmentArray[i].Fields.Item(j).Value.Length > 0)
                                            qty.quantity = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                    case 2:
                                        {
                                            qty.measureUnitQualifier = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                }
                            }
                            addFunction(SegmentType.QTY, qty);
                            qty = null;
                            break;
                        case "QVA":
                            QVA qva = new QVA();
                            for (int j = 0; j < segmentArray[i].Fields.Count; j++)
                            {
                                switch (j)
                                {
                                    case 0:
                                        {
                                            if (segmentArray[i].Fields.Item(j).Value != null && segmentArray[i].Fields.Item(j).Value.Length > 0)
                                                qva.quantityDifferenceInformation = Convert.ToInt32(segmentArray[i].Fields.Item(j).Value);
                                            break;
                                        }
                                    case 1:
                                        {
                                            if (segmentArray[i].Fields.Item(j).Value != null && segmentArray[i].Fields.Item(j).Value.Length > 0)
                                                qva.quantityQualifier = Convert.ToUInt16(segmentArray[i].Fields.Item(j).Value);
                                            break;
                                        }
                                    case 2:
                                        {
                                            qva.discrepancyCoded = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                    case 3:
                                        {
                                            qva.changeReasonCoded = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                    case 4:
                                        {
                                            qva.changeReason = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                }
                            }
                            addFunction(SegmentType.QVA, qva);
                            qva = null;
                            break;
                        case "QVR":
                            QVR qvr = new QVR();
                            for (int j = 0; j < segmentArray[i].Fields.Count; j++)
                            {
                                switch (j)
                                {
                                    case 0:
                                        {
                                            if (segmentArray[i].Fields.Item(j).Value != null && segmentArray[i].Fields.Item(j).Value.Length > 0)
                                                qvr.qtyDifference = Convert.ToInt32(segmentArray[i].Fields.Item(j).Value);
                                            break;
                                        }
                                    case 1:
                                        {
                                            if (segmentArray[i].Fields.Item(j).Value != null && segmentArray[i].Fields.Item(j).Value.Length > 0)
                                                qvr.quantityQualifier = Convert.ToUInt16(segmentArray[i].Fields.Item(j).Value);
                                            break;
                                        }
                                    case 2:
                                        {
                                            qvr.discrepencyCode = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                    case 3:
                                        {
                                            qvr.changeReason = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                    case 4:
                                        {
                                            qvr.changeText = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                }
                            }
                            addFunction(SegmentType.QVR, qvr);
                            qvr = null;
                            break;
                        case "RFF":
                            RFF rff = new RFF();
                            for (int j = 0; j < segmentArray[i].Fields.Count; j++)
                            {
                                switch (j)
                                {
                                    case 0:
                                        {
                                            rff.referenceQualifier = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                    case 1:
                                        {
                                            rff.referenceNumber = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                }
                            }
                            addFunction(SegmentType.RFF, rff);
                            rff = null;
                            break;
                        case "RTE":
                            RTE rte = new RTE();
                            for (int j = 0; j < segmentArray[i].Fields.Count; j++)
                            {
                                switch (j)
                                {
                                    case 0:
                                        {
                                            if (segmentArray[i].Fields.Item(j).Value != null && segmentArray[i].Fields.Item(j).Value.Length > 0)
                                                rte.rateTypeQualifier = Convert.ToByte(segmentArray[i].Fields.Item(j).Value);
                                            break;
                                        }
                                    case 1:
                                        {
                                            rte.ratePerUnit = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                }
                            }
                            addFunction(SegmentType.RTE, rte);
                            rte = null;
                            break;
                        case "TAX":
                            TAX tax = new TAX();
                            for (int j = 0; j < segmentArray[i].Fields.Count; j++)
                            {
                                switch (j)
                                {
                                    case 0:
                                        {
                                            if (segmentArray[i].Fields.Item(j).Value != null && segmentArray[i].Fields.Item(j).Value.Length > 0)
                                                tax.dtfFunctionQualifier = Convert.ToUInt16(segmentArray[i].Fields.Item(j).Value);
                                            break;
                                        }
                                    case 1:
                                        {
                                            tax.dtfTypeCoded = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                    case 2:
                                        {
                                            if (segmentArray[i].Fields.Item(j).Value != null && segmentArray[i].Fields.Item(j).Value.Length > 0)
                                                tax.c241CodeListResponsibleAgency = Convert.ToUInt16(segmentArray[i].Fields.Item(j).Value);
                                            break;
                                        }
                                    case 3:
                                        {
                                            tax.dtfAssessBasis = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                    case 4:
                                        {
                                            tax.dtfAccountDetail = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                    case 5:
                                        {
                                            tax.dtfRate = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                    case 7:
                                        {
                                            tax.dtfRateBasisID = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                    case 8:
                                        {
                                            tax.dtfCategory = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                }
                            }
                            addFunction(SegmentType.TAX, tax);
                            tax = null;
                            break;
                        case "TOD":
                            TOD tod = new TOD();
                            if (segmentArray[i].Fields.Count < 3)
                            {
                                for (int j = 0; j < segmentArray[i].Fields.Count; j++)
                                {
                                    switch (j)
                                    {
                                        case 0:
                                            {
                                                tod.todFunction = segmentArray[i].Fields.Item(j).Value;
                                                break;
                                            }
                                        //										case 1: 
                                        //										{
                                        //											if(segmentArray[i].Fields.Item(j).Value != null && segmentArray[i].Fields.Item(j).Value.Length > 0)
                                        //												tod.transportMethodOfPayment = Convert.ToUInt16(segmentArray[i].Fields.Item(j).Value);
                                        //											break;
                                        //										}
                                        case 1:
                                            {
                                                tod.todCoded = segmentArray[i].Fields.Item(j).Value;
                                                break;
                                            }
                                    }
                                }
                            }
                            else
                            {
                                for (int j = 0; j < segmentArray[i].Fields.Count; j++)
                                {
                                    switch (j)
                                    {
                                        case 0:
                                            {
                                                tod.todFunction = segmentArray[i].Fields.Item(j).Value;
                                                break;
                                            }
                                        case 1:
                                            {
                                                if (segmentArray[i].Fields.Item(j).Value != null && segmentArray[i].Fields.Item(j).Value.Length > 0)
                                                    tod.transportMethodOfPayment = Convert.ToUInt16(segmentArray[i].Fields.Item(j).Value);
                                                break;
                                            }
                                        case 2:
                                            {
                                                tod.todCoded = segmentArray[i].Fields.Item(j).Value;
                                                break;
                                            }
                                    }
                                }
                            }
                            addFunction(SegmentType.TOD, tod);
                            tod = null;
                            break;
                        case "UNA":
                            /*
											  (s.Name);
											  ("junk", s.m_fields[0].Value);
											  */
                            break;
                        case "UNB":
                            /*
											  (s.Name);
											  ("unCode", s.m_fields[0].Value);
											  ("unVersion", s.m_fields[0].subValues[0]);
											  ("from", s.m_fields[1].Value);
											  ("fromCode", s.m_fields[1].subValues[0]);
											  ("to", s.m_fields[2].Value);
											  ("toCode", s.m_fields[2].subValues[0]);
											  ("date", s.m_fields[3].Value);
											  ("time", s.m_fields[3].subValues[0]);
											  ("messageOrderNumber", s.m_fields[4].Value);
											  */
                            break;
                        case "UNH":
                            UNH unh = new UNH();
                            for (int j = 0; j < segmentArray[i].Fields.Count; j++)
                            {
                                switch (j)
                                {
                                    case 0:
                                        {
                                            unh.referenceNumber = segmentArray[i].Fields.Item(j).Value;
                                            //order.Number = unh.messageReference; //bgm.messageNumber;
                                            break;
                                        }
                                    case 1:
                                        {
                                            unh.typeIdentifier = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                    case 2:
                                        {
                                            unh.versionNumber = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                    case 3:
                                        {
                                            unh.releaseNumber = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                    case 4:
                                        {
                                            unh.controllingAgency = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                    case 5:
                                        {
                                            unh.associationAssignedCode = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                }
                            }
                            addFunction(SegmentType.UNH, unh);
                            unh = null;
                            break;
                        case "UNS":
                            UNS uns = new UNS();
                            for (int j = 0; j < segmentArray[i].Fields.Count; j++)
                            {
                                switch (j)
                                {
                                    case 0:
                                        {
                                            uns.sectionIdentification = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                }
                            }
                            addFunction(SegmentType.UNS, uns);
                            uns = null;
                            break;
                        case "UNT":
                            UNT unt = new UNT();
                            for (int j = 0; j < segmentArray[i].Fields.Count; j++)
                            {
                                switch (j)
                                {
                                    case 0:
                                        {
                                            if (segmentArray[i].Fields.Item(j).Value != null && segmentArray[i].Fields.Item(j).Value.Length > 0)
                                                unt.numberOfSegments = Convert.ToUInt32(segmentArray[i].Fields.Item(j).Value);
                                            break;
                                        }
                                    case 1:
                                        {
                                            unt.messageReferenceNumber = segmentArray[i].Fields.Item(j).Value;
                                            break;
                                        }
                                }
                            }
                            addFunction(SegmentType.UNT, unt);
                            unt = null;
                            break;
                        case "UNZ":
                            /*
											  (s.Name);
											  ("numberOfsegmentArray", s.m_fields[0].Value);
											  ("messageReferenceNumber", s.m_fields[1].Value);
											  */
                            break;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Exception Occured while populating: {0}, #{1}.", segmentArray[i].Name, i);
                }
            }
            TimeSpan duration = HighResClock.Now - startTime;
            Console.WriteLine("ProcessSegments: {0}ms", duration.TotalMilliseconds);
        }
    }
}
