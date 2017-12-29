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
using System.Collections.Generic;
using Crimson.Reader.EPub.OpenPackagingFormat;

namespace Crimson.Reader.EPub.OpenPackagingFormat
{
    /// <summary>
    /// The Navigation Control file for XML applications (NCX) exposes the hierarchical structure of a Publication 
    /// to allow the user to navigate through it. The NCX is similar to a table of contents in that it enables 
    /// the reader to jump directly to any of the major structural elements of the document, i.e. part, chapter, 
    /// or section, but it will often contain more elements of the document than the publisher chooses to include 
    /// in the original print table of contents. It can be visualized as a collapsible tree familiar to PC users. 
    /// Its development was motivated by the need to provide quick access to the main structural elements of a 
    /// document without the need to parse the entire documents. Other elements such as pages, footnotes, figures, 
    /// tables, etc. can be included in separate, nonhierarchical lists and can be accessed by the user as well. 
    /// <para>
    /// It is important to emphasize that these navigation features are intended as a convenience for users who 
    /// want them, and not a burden to those who do not. The alternative guide to the book may be provided for 
    /// those users not requiring the navigation features of the NCX.
    /// </para>
    /// </summary>
    public class NCX
    {
    }
}
