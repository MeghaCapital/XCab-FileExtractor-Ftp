//--------------------------------------------------------------
// <CreatedBy>
//     Anthony R Yates
//	   Feb. 26, 2005
//     Runtime Version: 1.1.4322.2032
//
//     Contains Segment Classes
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

namespace EDIFACT
{
    /// <summary>
    /// A segment represents a single entity in an edi document (i.e. UNH..., BGM..., etc).
    /// </summary>
    public class Segment
    {
        /// <summary>
        /// Name property is the name of the segment.
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Segment default constructor
        /// </summary>
        public Segment()
        {
        }

        /// <summary>
        /// Segment Constructor
        /// </summary>
        /// <param name="segment">Reference to a segment string.</param>
        public Segment(ref string segment)
        {
            //_segment = segment;		
            ParseSegment(ref segment);
        }

        /// <summary>
        /// Fields are the collection of fields in this segment.
        /// </summary>
        public FieldCollection Fields = new FieldCollection();

        private string name;
        private char[] delimiters = { '+', ':' };

        private void ParseSegment(ref string _segment)
        {
            string[] strTemp;
            strTemp = _segment.Split(delimiters);

            for (Int32 i = 0; i < strTemp.Length; i++)
            {
                if (i == 0)
                {   //Get Segment Name/Acronym
                    this.Name = strTemp[i];
                    continue;
                }
                Field fldTemp = new Field(strTemp[i]);
                Fields.Add(fldTemp);
            }
            strTemp = null;
        }

        private bool IsDelimiter(char c)
        {
            if (c.Equals(delimiters[0]) || c.Equals(delimiters[1]))
                return true;
            return false;
        }
    }//Segment

    /// <summary>
    /// SegmentCollection is a collection of Segments.
    /// </summary>
    public class SegmentCollection : System.Collections.CollectionBase
    {
        /// <summary>
        /// Add increments the number of fields in the collection.
        /// </summary>
        public void Add(Segment segment)
        {
            List.Add(segment);
        }
        /// <summary>
        /// Remove deletes an item from the collections and decrements the number of items by one.
        /// </summary>
        public void Remove(int index)
        {
            // Check to see if there is a field at the supplied index.
            if (index > Count - 1 || index < 0) return;
            else
                List.RemoveAt(index);
        }
        //		/// <summary>
        //		/// Item accesses a field in the collection by its index value.
        //		/// </summary>
        //		public Segment Item(int Index)
        //		{
        //			// The appropriate item is retrieved from the List object and
        //			// explicitly cast as Segment, and returned to the caller.
        //			return (Segment) List[Index];
        //		}

        public Segment this[int index]
        {
            get { return (Segment)List[index]; }
            set { List[index] = (Segment)value; }
        }
    }//FieldCollection
}
