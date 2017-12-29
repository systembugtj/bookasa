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
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Crimson.Catalog;
using Crimson.Reader.EPub.OpenContainerFormat;
using ICSharpCode.SharpZipLib.Zip;

namespace Crimson.Reader.EPub
{
    /// <summary>
    /// The OEBPS Container Format (OCF) is a general-purpose container technology
    /// <seealso cref="http://www.idpf.org/ocf/ocf1.0/download/ocf10.htm"/>
    /// </summary>
    public class ZipOCF : EPubBase, IOCF
    {
        #region IOCF Members

        #region Public Properties
        /// <summary>
        /// The mimetype file must contain the string 'application/epub+zip' as an ASCII string; no padding, 
        /// white-space or case change. The file MUST be neither compressed nor encrypted.
        /// </summary>
        public string MimeType { get { return "application/epub+zip"; } }

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
        public Container Container { get; set; }

        /// <summary>
        /// An OPTIONAL file with the reserved name "manifest.xml" within the "META-INF" directory at the root level 
        /// of the container may appear in a valid OCF container. If present, the file’s content MUST be as defined 
        /// in the ODF 1.0 manifest schema 
        /// (http://www.oasis-open.org/committees/download.php/12570/OpenDocument-manifest-schema-v1.0-os.rng).
        /// <para>
        /// The manifest.xml file, if present, MUST NOT be encrypted.
        /// </para>
        /// </summary>
        public Manifest Manifest { get; set; }

        /// <summary>
        /// A file with the reserved name “metadata.xml” within the “META-INF” directory at the root level of the 
        /// container file system may appear in a valid OCF container. This file, if present, MUST be used for 
        /// container-level metadata. In version 1.0 of OCF, no such container-level metadata is specified.  
        /// It is in this file that future innovation and extension SHOULD occur.
        /// <para>
        /// If the “META-INF/metadata.xml” file exists, its contents MUST be valid XML with namespace-qualified 
        /// elements to avoid collision with future versions of OCF that MAY specify a particular grammar and 
        /// namespace for elements and attributes within this file.
        /// </para>
        /// <para>
        /// The metadata.xml file, if present, MUST NOT be encrypted.
        /// </para>
        /// </summary>
        public Metadata Metadata { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Signatures Signatures { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Encryption Encryption { get; set; }

        /// <summary>
        /// 
        /// </summary>
        //public Rights Rights { get; set; }
        #endregion

        #region Create a new container
        /// <summary>
        /// Create a OCF Container in the correct format
        /// </summary>
        /// <param name="filePath">The path to the EPub file to be created.</param>
        public void CreateContainer(string filePath)
        {
            ZipFile zip = new ZipFile(new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite));
            zip.Close();
        }
        #endregion

        #region Open an existing container
        /// <summary>
        /// Open and process the epub file.
        /// </summary>
        /// <param name="filePath">The path to the epub file</param>
        public void OpenContainer(string filePath)
        {
            try
            {
                CheckMagic(filePath);
                using (OCF = new ZipFile(filePath))
                {
                    if (OCF.FindEntry("META-INF/container.xml", false) != -1)
                    {
                        Container = ParseContainer(OCF[OCF.FindEntry("META-INF/container.xml", false)]);
                        Container.ZipEntry = OCF[OCF.FindEntry("META-INF/container.xml", false)];
                        Container.OCF = OCF;
                    }
                    if (OCF.FindEntry("META-INF/manifest.xml", false) != -1)
                    {
                        Manifest = ParseManifest(OCF[OCF.FindEntry("META-INF/manifest.xml", false)]);
                        Manifest.ZipEntry = OCF[OCF.FindEntry("META-INF/manifest.xml", false)];
                        Manifest.OCF = OCF;
                    }
                    if (OCF.FindEntry("META-INF/metadata.xml", false) != -1)
                    {
                        Metadata = ParseMetadata(OCF[OCF.FindEntry("META-INF/metadata.xml", false)]);
                        Metadata.ZipEntry = OCF[OCF.FindEntry("META-INF/metadata.xml", false)];
                        Metadata.OCF = OCF;
                    }
                    if (OCF.FindEntry("META-INF/signatures.xml", false) != -1)
                    {
                        Signatures = ParseSignatures(OCF[OCF.FindEntry("META-INF/signatures.xml", false)]);
                        Signatures.ZipEntry = OCF[OCF.FindEntry("META-INF/signatures.xml", false)];
                        Signatures.OCF = OCF;
                    }
                    if (OCF.FindEntry("META-INF/encryption.xml", false) != -1)
                    {
                        Encryption = ParseEncryption(OCF[OCF.FindEntry("META-INF/encryption.xml", false)]);
                        Encryption.ZipEntry = OCF[OCF.FindEntry("META-INF/encryption.xml", false)];
                        Encryption.OCF = OCF;
                    }
                    if (OCF.FindEntry("META-INF/rights.xml", false) != -1)
                    {
                       // Rights = ParseRights(OCF[OCF.FindEntry("META-INF/rights.xml", false)]);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region Check the Magic number of a EPUB zip archive
        /// <summary>
        /// The first file in the ZIP Container MUST be a file by the ASCII name of ‘mimetype’ which holds the MIME 
        /// type for the ZIP Container (i.e., “application/epub+zip” as an ASCII string; no padding, white-space or 
        /// case change). The file MUST be neither compressed nor encrypted and there MUST NOT be an extra field in
        /// its ZIP header. If this is done, then the ZIP Container offers convenient “magic number” support as 
        /// described in RFC 2048 and the following will hold true:
        /// <list>
        /// <item>The bytes “PK” will be at the beginning of the file</item>
        /// <item>The bytes “mimetype” will be at position 30</item>
        /// <item>The actual MIME type (i.e., the ASCII string “application/epub+zip”) will begin at position 38</item>
        /// </list>
        /// </summary>
        /// <param name="filePath">The Path to the EPub file.</param>
        public bool CheckMagic(string filePath)
        {
            bool MagicValid = false;
            byte[] Magic = new byte[38 + MimeType.Length];
            FileStream epub = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite); ;

            try
            {
                // Get the magic number
                epub.Read(Magic, 0, 38 + MimeType.Length);

                // verify the magic number
                if (ASCIIEncoding.ASCII.GetString(Magic, 0, 2) != "PK")
                    throw new ApplicationException(string.Format("{0}. In file {1} {2}", Resources.Exceptions.InvalidEPubFileException, filePath,
                        "the value of the first two bytes is not 'PK'"));

                if (ASCIIEncoding.ASCII.GetString(Magic, 30, 8) != "mimetype")
                    throw new ApplicationException(string.Format("{0}. In file {1} {2}", Resources.Exceptions.InvalidEPubFileException, filePath,
                        "the name of the first file in the archive is not 'mimetype'"));

                if (ASCIIEncoding.ASCII.GetString(Magic, 38, MimeType.Length) != MimeType)
                    throw new ApplicationException(string.Format("{0}. In file {1} {2}", Resources.Exceptions.InvalidEPubFileException, filePath,
                        "the contents of the first file in the archive is not ", MimeType));

                // This is a valid epub archive
                MagicValid = true;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (epub != null)
                    epub.Close();
            }
            return MagicValid;
        }
        #endregion

        #endregion

        #region Private routines for parsing a EPUB container
        /// <summary>
        /// Return the container information from the container file
        /// </summary>
        /// <param name="entry">The zip entry for the container</param>
        /// <returns>The Container object built from the container xml string</returns>
        private Container ParseContainer(ZipEntry entry)
        {
            return (from con in XDocument.Parse(GetEntryAsString(entry)).Descendants("{urn:oasis:names:tc:opendocument:xmlns:container}container")
                    let IsMissingVersion = con.Attribute("version")
                    let IsMissingNameSpace = con.Attribute("xmlns")
                    select new Container
                      {
                          Version =   (IsMissingVersion == null) ? null : new Version(con.Attribute("version").Value.Trim()),
                          NameSpace = (IsMissingNameSpace == null) ? null : con.Attribute("xmlns").Value.Trim(),
                          RootFiles = (from rf in con.Descendants("{urn:oasis:names:tc:opendocument:xmlns:container}rootfiles")
                                       let IsMissingFullPath = rf.Element("{urn:oasis:names:tc:opendocument:xmlns:container}rootfile").Attribute("full-path")
                                       let IsMissingMediaType = rf.Element("{urn:oasis:names:tc:opendocument:xmlns:container}rootfile").Attribute("media-type")
                                       select new RootFile
                                       {
                                           Name = (IsMissingFullPath == null) ? null : rf.Element("{urn:oasis:names:tc:opendocument:xmlns:container}rootfile").Attribute("full-path").Value.Trim(),
                                           MediaType = (IsMissingMediaType == null) ? null : rf.Element("{urn:oasis:names:tc:opendocument:xmlns:container}rootfile").Attribute("media-type").Value.Trim()
                                       }).ToList(),
                      }).Single();
        }

        /// <summary>
        /// Parse the signatures Xml file
        /// </summary>
        /// <param name="entry">The zip entry for the signatures</param>
        /// <returns>The signatures object created from the signatures xml</returns>
        private Signatures ParseSignatures(ZipEntry entry)
        {
            return null;
        }

        /// <summary>
        /// Parse the encryption Xml file
        /// </summary>
        /// <param name="entry">The zip entry for the encryption</param>
        /// <returns>The Encryption object created from the encryption xml</returns>
        private Encryption ParseEncryption(ZipEntry entry)
        {
            return null;
        }

        /// <summary>
        /// Parse the manifest Xml file
        /// </summary>
        /// <param name="entry">The zip entry for the manifest</param>
        /// <returns>The manifest object created from the manifest xml</returns>
        private Manifest ParseManifest(ZipEntry entry)
        {
            return null;
        }

        /// <summary>
        /// Parse the rights Xml file
        /// </summary>
        /// <param name="entry">The zip entry for the rights</param>
        /// <returns>The rights object created from the rights xml</returns>
        //private Rights ParseRights(ZipEntry entry)
        //{
        //    return null;
        //}

        /// <summary>
        /// At the moment this routine is undefined.  It is avaliable for future exapnsion
        /// </summary>
        /// <param name="entry">The zip entry for the metadata</param>
        /// <returns>always null</returns>
        private Metadata ParseMetadata(ZipEntry entry)
        {
            return null;
        }
        #endregion
    }
}