//--------------------------------------------------------------
// <CreatedBy>
//     Anthony R Yates
//	   Feb. 26, 2005
//     Runtime Version: 1.1.4322.2032
//	   
//     Contains Fields Classes
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
    /// A field represents a single piece of information in a segment.</summary>
    public class Field
    {
        /// <summary>
        /// The field value.</summary>
        public string Value
        {
            get { return fldValue; }
            set { fldValue = value; }
        }

        private string fldValue;

        /// <summary>
        /// Field Contructor</summary>
        /// <param name="Value">The field value.</param>
        public Field(string Value)
        {
            fldValue = Value;
        }
    }//Field

    /// <summary>
    /// A collection of field elements.</summary>
    public class FieldCollection : System.Collections.CollectionBase
    {
        /// <summary>
        /// Add increments the number of fields in the collection.</summary>
        public void Add(Field aField)
        {
            List.Add(aField);
        }
        /// <summary>
        /// Remove deletes an index in the collection.</summary>
        public void Remove(int index)
        {
            // Check to see if there is a field at the supplied index.
            if (index > Count - 1 || index < 0) return;
            else
                List.RemoveAt(index);
        }
        /// <summary>
        /// Item accesses a field in the collection by its index value.</summary>
        public Field Item(int Index)
        {
            return (Field)List[Index];
        }
    }//FieldCollection
}
