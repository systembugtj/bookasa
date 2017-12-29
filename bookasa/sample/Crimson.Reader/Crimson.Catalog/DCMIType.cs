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

namespace Crimson.Catalog
{
    /// <summary>
    /// The DCMI Type Vocabulary provides a general, cross-domain list of approved terms that may be used as values for 
    /// the Resource Type element to identify the genre of a resource. The terms documented here are also included in 
    /// the more comprehensive document "DCMI Metadata Terms" at http://dublincore.org/documents/dcmi-terms/.
    /// </summary>
    public enum DCMIType
    {
        /// <summary>
        /// An aggregation of resources.
        /// <remarks>
        /// A collection is described as a group; its parts may also be separately described.
        /// </remarks>
        /// <seealso cref="http://purl.org/dc/dcmitype/Collection"/>
        /// <seealso cref="http://purl.org/dc/terms/DCMIType"/>
        /// <seealso cref="http://dublincore.org/usage/terms/history/#Collection-003"/>
        /// </summary>
        Collection,

        /// <summary>
        /// Data encoded in a defined structure.
        /// <remarks>
        /// Examples include lists, tables, and databases. A dataset may be useful for direct machine processing.
        /// </remarks>
        /// <seealso cref="http://purl.org/dc/dcmitype/Dataset"/>
        /// <seealso cref="http://purl.org/dc/terms/DCMIType"/>
        /// <seealso cref="http://dublincore.org/usage/terms/history/#Dataset-003"/>
        /// </summary>
        Dataset,

        /// <summary>
        /// A non-persistent, time-based occurrence.
        /// <remarks>
        /// Metadata for an event provides descriptive information that is the basis for discovery of the purpose, 
        /// location, duration, and responsible agents associated with an event. Examples include an exhibition, 
        /// webcast, conference, workshop, open day, performance, battle, trial, wedding, tea party, conflagration.
        /// </remarks>
        /// <seealso cref="http://purl.org/dc/dcmitype/Event"/>
        /// <seealso cref="http://purl.org/dc/terms/DCMIType"/>
        /// <seealso cref="http://dublincore.org/usage/terms/history/#Event-003"/>
        /// </summary>
        Event,

        /// <summary>
        /// A visual representation other than text.
        /// <remarks>
        /// Examples include images and photographs of physical objects, paintings, prints, drawings, other images 
        /// and graphics, animations and moving pictures, film, diagrams, maps, musical notation. Note that Image 
        /// may include both electronic and physical representations.
        /// </remarks>
        /// <seealso cref="http://purl.org/dc/dcmitype/Image"/>
        /// <seealso cref="http://purl.org/dc/terms/DCMIType"/>
        /// <seealso cref="http://purl.org/dc/dcmitype/StillImage"/>
        /// <seealso cref="http://purl.org/dc/dcmitype/MovingImage"/>
        /// <seealso cref="http://dublincore.org/usage/terms/history/#Image-004"/>
        /// </summary>
        Image,

        /// <summary>
        /// A resource requiring interaction from the user to be understood, executed, or experienced.
        /// <remarks>
        /// Examples include forms on Web pages, applets, multimedia learning objects, chat services, or virtual 
        /// reality environments.
        /// </remarks>
        /// <seealso cref="http://purl.org/dc/dcmitype/InteractiveResource"/>
        /// <seealso cref="http://purl.org/dc/terms/DCMIType"/>
        /// <seealso cref="http://dublincore.org/usage/terms/history/#InteractiveResource-003"/>
        /// </summary>
        InteractiveResource,

        /// <summary>
        /// A series of visual representations imparting an impression of motion when shown in succession.
        /// <remarks>
        /// Examples include animations, movies, television programs, videos, zoetropes, or visual output from 
        /// a simulation. Instances of the type Moving Image must also be describable as instances of the broader 
        /// type Image.
        /// </remarks>
        /// <seealso cref="http://purl.org/dc/dcmitype/MovingImage"/>
        /// <seealso cref="http://purl.org/dc/dcmitype/Image"/>
        /// <seealso cref="http://purl.org/dc/terms/DCMIType"/>
        /// <seealso cref="http://dublincore.org/usage/terms/history/#MovingImage-003"/>
        /// </summary>
        MovingImage,

        /// <summary>
        /// An inanimate, three-dimensional object or substance.
        /// <remarks>
        /// Note that digital representations of, or surrogates for, these objects should use Image, Text or one of the 
        /// other types.
        /// </remarks>
        /// <seealso cref="http://purl.org/dc/dcmitype/PhysicalObject"/>
        /// <seealso cref="http://purl.org/dc/terms/DCMIType"/>
        /// <seealso cref="http://dublincore.org/usage/terms/history/#PhysicalObject-003"/>
        /// </summary>
        PhysicalObject,

        /// <summary>
        /// A system that provides one or more functions.
        /// <remarks>
        /// Examples include a photocopying service, a banking service, an authentication service, interlibrary loans, 
        /// a Z39.50 or Web server.
        /// </remarks>
        /// <seealso cref="http://purl.org/dc/dcmitype/Service"/>
        /// <seealso cref="http://purl.org/dc/terms/DCMIType"/>
        /// <seealso cref="http://dublincore.org/usage/terms/history/#Service-003"/>
        /// </summary>
        Service,

        /// <summary>
        /// A computer program in source or compiled form.
        /// <remarks>
        /// Examples include a C source file, MS-Windows .exe executable, or Perl script.
        /// </remarks>
        /// <seealso cref="http://purl.org/dc/dcmitype/Software"/>
        /// <seealso cref="http://purl.org/dc/terms/DCMIType"/>
        /// <seealso cref="http://dublincore.org/usage/terms/history/#Software-003"/>
        /// </summary>
        Software,

        /// <summary>
        /// A resource primarily intended to be heard.
        /// <remarks>
        /// Examples include a music playback file format, an audio compact disc, and recorded speech or sounds.
        /// </remarks>
        /// <seealso cref="http://purl.org/dc/dcmitype/Sound"/>
        /// <seealso cref="http://purl.org/dc/terms/DCMIType"/>
        /// <seealso cref="http://dublincore.org/usage/terms/history/#Sound-003"/>
        /// </summary>
        Sound,

        /// <summary>
        /// A static visual representation.
        /// <remarks>
        /// Examples include paintings, drawings, graphic designs, plans and maps. Recommended best practice is to 
        /// assign the type Text to images of textual materials. Instances of the type Still Image must also be 
        /// describable as instances of the broader type Image.
        /// </remarks>
        /// <seealso cref="http://purl.org/dc/dcmitype/StillImage"/>
        /// <seealso cref="http://purl.org/dc/dcmitype/Image"/>
        /// <seealso cref="http://purl.org/dc/terms/DCMIType"/>
        /// <seealso cref="http://dublincore.org/usage/terms/history/#StillImage-003"/>
        /// </summary>
        StillImage,

        /// <summary>
        /// A resource consisting primarily of words for reading.
        /// <remarks>
        /// Examples include books, letters, dissertations, poems, newspapers, articles, archives of mailing lists. 
        /// Note that facsimiles or images of texts are still of the genre Text.
        /// </remarks>
        /// <seealso cref="http://purl.org/dc/dcmitype/Text"/>
        /// <seealso cref="http://purl.org/dc/terms/DCMIType"/>
        /// <seealso cref="http://dublincore.org/usage/terms/history/#Text-003"/>
        /// </summary>
        Text
    }
}
