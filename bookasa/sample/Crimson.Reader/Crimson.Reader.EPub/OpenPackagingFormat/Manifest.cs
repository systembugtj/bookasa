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
using System.Xml.Linq;
using ICSharpCode.SharpZipLib.Zip;

namespace Crimson.Reader.EPub.OpenPackagingFormat
{
    /// <summary>
    /// The required manifest must provides a list of all the files that are parts of the publication 
    /// (e.g. Content Documents, style sheets, image files, any embedded font files, any included schemas). 
    /// The manifest element must contain one or more item elements. 
    /// </summary>
    public class Manifest : List<Item>
    {
        /// <summary>
        /// The OCF Container for the EPUB
        /// </summary>
        public Manifest Load(XElement xd)
        {
            return null;
        }
    }

    /// <summary>
    /// Each item describes a document, an image file, a style sheet, or other component that is considered 
    /// part of the publication. The manifest must not include item elements referring to the file or files 
    /// that make up the OPF Package Document. 
    /// </summary>
    public class Item : EPubBase
    {
        /// <summary>
        /// The unique id for this item
        /// </summary>
        public Guid ManifestItemId { get; set; }

        /// <summary>
        /// The ID of the item
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The relative URI to the item.  This is relative to the root of the epub
        /// </summary>
        public Uri HRef { get; set; }

        /// <summary>
        /// The media type of the item
        /// </summary>
        public string MediaType { get; set; }

        /// <summary>
        /// The OPS specification defines a set of OPS Core Media Types that all conforming Reading Systems
        /// must support (e.g. XHTML, PNG, SVG). For a publication that uses only those media types, the 
        /// manifest merely lists the publication's component files directly. However, content providers 
        /// may construct publications that reference items of additional media types. In order for such 
        /// publications to be read by all conforming Reading Systems, content providers must provide 
        /// alternative "fallback" items for each such item. For every item that is not an OPS Core Media 
        /// Type, at least one of its associated fallback items must either be of a type drawn from the set 
        /// of OPS Core Media Types or, in some cases, CSS styling may be provided for documents containing 
        /// non-preferred XML vocabularies. 
        /// </summary>
        public string FallBack { get; set; }
    }
}
