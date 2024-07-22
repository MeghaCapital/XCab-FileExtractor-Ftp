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

namespace EDIFACT
{
    /// <summary>
    /// AddSegmentDelegate is used to point to the Add method in an EDIFACT message class (for example,  <see cref="ORDERS"/>).
    /// </summary>
    public delegate void AddSegmentDelegate(SegmentType segmentType, object segmentObject);

}