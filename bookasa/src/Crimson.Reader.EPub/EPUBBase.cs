#region Microsoft Public License (Ms-PL)
//  Microsoft Public License (Ms-PL)
// 
// This license governs use of the accompanying software. If you use the 
// software, you accept this license. If you do not accept the license, do not 
// use the software.
// 
// 1. Definitions
// 
// The terms "reproduce," "reproduction," "derivative works," and "distribution" 
// have the same meaning here as under U.S. copyright law.
// 
// A "contribution" is the original software, or any additions or changes to 
// the software.
// 
// A "contributor" is any person that distributes its contribution under this 
// license.
// 
// "Licensed patents" are a contributor's patent claims that read directly on 
// its contribution.
// 
// 2. Grant of Rights
// 
// (A) Copyright Grant- Subject to the terms of this license, including the 
// license conditions and limitations in section 3, each contributor grants you 
// a non-exclusive, worldwide, royalty-free copyright license to reproduce its 
// contribution, prepare derivative works of its contribution, and distribute its 
// contribution or any derivative works that you create.
// 
// (B) Patent Grant- Subject to the terms of this license, including the 
// license conditions and limitations in section 3, each contributor grants 
// you a non-exclusive, worldwide, royalty-free license under its licensed 
// patents to make, have made, use, sell, offer for sale, import, and/or 
// otherwise dispose of its contribution in the software or derivative works of 
// the contribution in the software.
// 
// 3. Conditions and Limitations
// 
// (A) No Trademark License- This license does not grant you rights to use 
// any contributors' name, logo, or trademarks.
// 
// (B) If you bring a patent claim against any contributor over patents that 
// you claim are infringed by the software, your patent license from such 
// contributor to the software ends automatically.
// 
// (C) If you distribute any portion of the software, you must retain all 
// copyright, patent, trademark, and attribution notices that are present in 
// the software.
// 
// (D) If you distribute any portion of the software in source code form, 
// you may do so only under this license by including a complete copy of 
// this license with your distribution. If you distribute any portion of the 
// software in compiled or object code form, you may only do so under a license
// that complies with this license.
// 
// (E) The software is licensed "as-is." You bear the risk of using it. The 
// contributors give no express warranties, guarantees or conditions. You may 
// have additional consumer rights under your local laws which this license 
// cannot change. To the extent permitted under your local laws, the 
// contributors exclude the implied warranties of merchantability, fitness 
// for a particular purpose and non-infringement.  
// 
// This Software is Copyright (c)2009 by LigoSoftware.com
//
#endregion
using System;
using System.IO;
using System.Text;
using ICSharpCode.SharpZipLib.Zip;

namespace Crimson.Reader.EPub
{
    /// <summary>
    /// The Base class for all access to the EPUB file
    /// </summary>
    public class EPubBase
    {
        /// <summary>The EPub namespaces</summary>
        protected const string XNLNSOPF="{http://www.idpf.org/2007/opf}";
        protected const string XMLNSDC="{http://purl.org/dc/elements/1.1/}";
        protected const string XMLNSDCTERMS="{http://purl.org/dc/terms/}";
        protected const string XMLNSXSI = "{http://www.w3.org/2001/XMLSchema-instance}";
        protected const string XMLNSOPF="{http://www.idpf.org/2007/opf}";

        /// <summary>
        /// The OCF File (EPUB) for the book
        /// </summary>
        public ZipFile OCF { get; set; }

        /// <summary>
        /// The Zip entry for this file in the EPUB.
        /// </summary>
        public ZipEntry ZipEntry { get; set; }

        #region get a entry from the OCF zip archive and return it as a string
        /// <summary>
        /// Read a file from OCF
        /// </summary>
        /// <param name="entry">The entry in the OCF</param>
        /// <returns>The contents of the file as a string</returns>
        protected string GetEntryAsString(ZipEntry entry)
        {
            StringBuilder sb = null;

            using (Stream st = OCF.GetInputStream(entry))
            {
                try
                {
                    byte[] buffer = new byte[8192];
                    int bytesRead;

                    sb = new StringBuilder(8192);
                    while ((bytesRead = st.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        sb.Append(ASCIIEncoding.ASCII.GetString(buffer, 0, bytesRead));
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

            if (sb != null)
                return sb.ToString().Trim();
            else return null;
        }
        #endregion
    }
}