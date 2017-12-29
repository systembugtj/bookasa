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
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using ICSharpCode.SharpZipLib.Zip;

namespace Crimson.Reader.EPub.OpenContainerFormat
{
    /// <summary>
    /// All valid OCF Containers MUST include a file called “container.xml” within the “META-INF” directory at 
    /// the root level of the container file system. The container.xml file MUST identify the MIME type of, 
    /// and path to, the rootfile for the OEBPS version of the publication and any OPTIONAL alternate renditions 
    /// included within the container.  
    /// <para>
    /// The container.xml file MUST NOT be encrypted.
    /// </para>
    /// The container.xml file contains XML that uses the “urn:oasis:names:tc:opendocument:xmlns:container” 
    /// namespace for all of its elements and attributes. The “version="1.0"” attribute MUST be included for 
    /// all containers that conform to this version of the specification. 
    /// </summary>
    public class Container : EPubBase
    {
        /// <summary>
        /// Private property fr the Manifest for this epub
        /// </summary>
        private OpenPackagingFormat.Manifest _Manifest { get; set; }

        /// <summary>
        /// The version of the container.xml file
        /// </summary>
        public Version Version { get; set; }

        /// <summary>
        /// The name space for the container.xml file
        /// </summary>
        public string NameSpace { get; set; }

        /// <summary>
        /// The root files for the container.xml file
        /// </summary>
        public List<RootFile> RootFiles { get; set; }

        /// <summary>
        /// The public constructor just sets up the default property values
        /// </summary>
        public Container()
        {
            NameSpace = "urn:oasis:names:tc:opendocument:xmlns:container";
            Version = new Version(1, 0);
            RootFiles = new List<RootFile>();
        }

        /// <summary>
        /// Return the object as a XML string
        /// </summary>
        /// <returns>The object in as a OCF container.xml string</returns>
        public override string ToString()
        {
            XNamespace xn = NameSpace;
            return new XDocument(
                new XDeclaration("1.0", "UTF-8", "yes"),
                new XElement(xn + "container",
                    new XAttribute("version", Version.ToString()),
                    new XElement("rootfiles",
                        from rf in RootFiles
                        select new XElement(
                            "rootfile",
                            new XAttribute("full-path", rf.Name),
                            new XAttribute("media-type", rf.MediaType)
                        )
                    )
                )
            ).ToString();
        }
    }
}
