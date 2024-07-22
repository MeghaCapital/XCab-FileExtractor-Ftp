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

using System;
using System.IO;

namespace EDIFACT
{
    /// <summary>
    /// Contains various IO helper routines for working with files.</summary>
    public class FileUtility
    {
        public FileUtility() { }

        public static string Read(string filename)
        {
            try
            {
                string data;

                FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                StreamReader sr = new StreamReader(fs, System.Text.Encoding.Default);
                data = sr.ReadToEnd();
                sr.Close();
                fs.Close();
                return data;
            }
            catch (Exception)
            {
                //System.Windows.Forms.MessageBox.Show(e.Message,"Error",
                //	System.Windows.Forms.MessageBoxButtons.OK,
                //	System.Windows.Forms.MessageBoxIcon.Error);
                return "";
            }
        }

        private const string DEFAULTEXT = ".xml";
        public static bool CheckExt(ref string filename)
        {
            int i = filename.LastIndexOf('.');
            if (i == -1)
            {
                filename += DEFAULTEXT;
                return true;
            }
            else if ((i + 4) != filename.Length)
            {
                filename += DEFAULTEXT;
                return true;
            }
            else if (filename.Substring(filename.Length - 4) == ".txt")
            {
                filename = filename.Replace(".txt", DEFAULTEXT);
                return true;
            }

            return false; //Should never get here.
        }
        private bool IsAllowedExt(FileInfo filename)
        {
            string[] allowedExtensions = { ".txt", ".xml", ".xsl", ".xslt", ".edi" };
            foreach (string s in allowedExtensions)
            {
                if (s == filename.Extension || string.Compare(s, filename.Extension, true) == 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
