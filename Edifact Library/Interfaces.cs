//--------------------------------------------------------------
// <CreatedBy>
//     Anthony R Yates
//	   Feb. 26, 2005
//     Runtime Version: 1.1.4322.2032
//	   
//     Contains General Interfaces
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
    //	/// <summary>
    //	/// IParser is an interface with one method, Parse().
    //	/// </summary>
    //	public interface IParser
    //	{
    //		/// <summary>
    //		/// Parses an array of segments and returns the message type specified.
    //		/// </summary>
    //		/// <param name="msgType">The message type to return</param>
    //		/// <param name="segments">The array of segments to parse</param>
    //		/// <returns>A message type object.</returns>
    //		IMessage Parse(MessageTypeIdentifier message, Segment [] segments);
    //		IMessage GetMessage();
    //	}

    public interface IMessage
    {
        void PopulateMessage(ref Segment[] segments);
    }
}

/* Original Intention:
 * Parser p = new Parser()
 * IMessage m = p.Parse(IMessage msg, Segment [] segs));
 * m.SerializeToDsk()
 * m.SerializeToXml()
 * 
 * */
