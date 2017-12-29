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
using System.Linq;
using System.Xml.Linq;
using Crimson.Reader.EPub.OpenContainerFormat;
using Crimson.Reader.EPub.OpenPackagingFormat;

namespace Crimson.Reader.EPub
{
    /// <summary>
    /// The Open Packaging Format (OPF) Specification, defines the mechanism by which the various components of an 
    /// OPS publication are tied together and provides additional structure and semantics to the electronic publication. 
    /// Specifically, OPF:
    /// <list type="bullet">
    /// <item>
    /// Describes and references all components of the electronic publication (e.g. markup files, images, navigation 
    /// structures). 
    /// </item>
    /// <item>
    /// Provides publication-level metadata. 
    /// </item>
    /// <item>
    /// Specifies the linear reading-order of the publication. 
    /// </item>
    /// <item>
    /// Provides fallback information to use when unsupported extensions to OPS are employed. 
    /// </item>
    /// <item>
    /// Provides a mechanism to specify a declarative table of contents (the NCX). 
    /// </item>
    /// </list>
    /// <seealso cref="http://www.openebook.org/2007/opf/OPF_2.0_final_spec.html"/>
    /// </summary>
    public class OPF : EPubBase
    {
        /// <summary>
        /// The package element must specify a value for its unique-identifier attribute. The unique-identifier 
        /// attribute's value specifies which Dublin Core identifier element, described in Section 2.2.10, provides 
        /// the package's preferred, or primary, identifier. The OPF Package Document's author is responsible for 
        /// choosing a primary identifier that is unique to one and only one particular package (i.e., the set of 
        /// files referenced from the package document's manifest). 
        /// </summary>
        public string UniqueIdentifier { get; set; }
 
        /// <summary>
        /// The required metadata for the EPUB. 
        /// </summary>
        public MetaData MetaData { get; set; }

        /// <summary>
        /// The required Manifest for the EPub.
        /// </summary>
        public OpenPackagingFormat.Manifest Manifest { get; set; }

        /// <summary>
        /// Following the manifest there must be one spine element.        
        /// </summary>
        public Spine Spine { get; set; }

        /// <summary>
        /// Within the package there may be one guide element. 
        /// </summary>
        public Guide Guide { get; set; }

        /// <summary>
        /// In order to enable ease of navigation and provide greater accessibility, the OPF Package Document 
        /// supports a "Navigation Center eXtended.
        /// </summary>
        public NCX NavigationCenterExtended { get; set; }

        #region Methods to load this from the package file
        /// <summary>
        /// Load the OPF object from the XDocument.  This document is presumed to be a OPF Package file
        /// </summary>
        /// <param name="xd">The OCF container to load from.</param>
        public bool Load(Container con)
        {
            // Get the name of the package file
            var pub = (from p in con.RootFiles
                       where p.MediaType == "application/oebps-package+xml"
                       select p.Name).First();

            // Set the OCF and the ZipEntry and parse the class members from the package file
            if (con.OCF != null)
            {
                OCF = con.OCF;
                if (OCF.FindEntry(pub, false) > -1)
                {
                    ZipEntry = OCF[OCF.FindEntry(pub, false)];

                    // Now parse the package file and load to the child objects 
                    Load(XDocument.Parse(GetEntryAsString(ZipEntry)));
                }
            }
            return true;
        }

        /// <summary>
        /// Load the OPF object from a XDocument Package
        /// </summary>
        /// <param name="xd">The XDocument for the Package</param>
        public bool Load(XDocument xd)
        {
            var epub = (from p in xd.Descendants(XMLNSOPF + "package")
                        let IsMissingMetaData = p.Element(XMLNSOPF + "metadata")
                        select
                            this.MetaData = (IsMissingMetaData == null) ? null :
                                                new OpenPackagingFormat.MetaData().Load(p.Element(XMLNSOPF + "metadata"))
                        );
            return epub.Count() >= 1;
        }

        /// <summary>
        /// Load the OPF object from the XML string for the Package
        /// </summary>
        /// <param name="uri">The uri for the file to load for the package</param>
        public bool Load(string uri)
        {
            return Load(XDocument.Load(uri));
        }

        /// <summary>
        /// Parse the OPF object from the XML string for the Package
        /// </summary>
        /// <param name="xml">The XML string for the package</param>
        public bool Parse(string xml)
        {
            return Load(XDocument.Parse(xml));
        }
        #endregion
    }
}
