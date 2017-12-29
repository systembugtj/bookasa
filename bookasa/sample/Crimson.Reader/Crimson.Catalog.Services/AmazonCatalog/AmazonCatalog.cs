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

namespace Crimson.Catalog.Services.AmazonCatalog
{
    /// <summary>
    /// Search the Amazon Catalog By Author, Author and Title, Title, ASIN or ISBN
    /// </summary>
    public class AmazonCatalog : IAmazonCatalog
    {
        private const string AmazonUri = "http://webservices.amazon.com/onca/xml/2009-02-01?Service=AWSECommerceService&AWSAccessKeyId={0}&tag={1}Operation=ItemSearch&SearchIndex={2}&ResponseGroup=Medium&";
        private const string ns = "{http://webservices.amazon.com/AWSECommerceService/2009-02-01}";

        /// <summary>
        /// Return the list of books given a search criteria
        /// </summary>
        /// <param name="searchCriteria">The search criteria for the amazon catalog</param>
        /// <returns>A list of books matching the search criteria</returns>
        private List<AmazonCatalogEntry> ItemSearch(string searchCriteria, string searchIndex)
        {
            try
            {
                List<string> accesskey = Utilities.GetApiKey("http://www.amazon.com", "AccessKey");
                List<string> associateTag = Utilities.GetApiKey("http://www.amazon.com", "AssociateTag");

                if ((accesskey.Count == 1) && (associateTag.Count == 1))
                {
                    XDocument xd = XDocument.Load(string.Format(AmazonUri, accesskey[0], associateTag[0], searchIndex, searchCriteria));
                    var AmazonBooks = (from n in xd.Descendants(ns + "Item")
                                       let IsMissingAuthor = n.Element(ns + "Author")
                                       let IsMissingTitle = n.Element(ns + "Title")
                                       let IsMissingDetailedUrl = n.Element(ns + "DetailedUrl")
                                       select new AmazonCatalogEntry
                                       {
                                           Title = (IsMissingTitle == null) ? null : n.Element(ns + "Title").Value.ToString().Trim(),
                                       }).ToList();
                    return AmazonBooks;
                }
                else return null;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        #region IAmazonCatalog Members
        /// <summary>
        /// Return a MP3 Download List from amazon.com by title
        /// </summary>
        /// <param name="title">The title of the MP3 to search for</param>
        /// <returns>A list of MP3's with the given title</returns>
        public List<AmazonCatalogEntry> GetMP3DownloadsByTitle(string title)
        {
            return ItemSearch(string.Format("Title={0}", title), "MP3Downloads");
        }

        /// <summary>
        /// Return a MP3 Download List from amazon.com by artist
        /// </summary>
        /// <param name="artist">The artist of the MP3 to search for</param>
        /// <returns>A list of MP3's with the given title</returns>
        public List<AmazonCatalogEntry> GetMP3DownloadsByArtist(string artist)
        {
            return ItemSearch(string.Format("&Creator={0}", artist), "MP3Downloads");
        }

        /// <summary>
        /// Return a MP3 Download List from amazon.com by artist and title
        /// </summary>
        /// <param name="artist">The artist of the MP3 to search for</param>
        /// <param name="title">The title of the MP3 to search for</param>
        /// <returns>A list of MP3's with the given title</returns>
        public List<AmazonCatalogEntry> GetMP3DownloadsByArtist(string artist, string title)
        {
            return ItemSearch(string.Format("&Creator={0}&Title={1}", artist, title), "MP3Downloads");
        }

        /// <summary>
        /// Return a DVD list from amazon.com by title
        /// </summary>
        /// <param name="title">The title of the DVD to search for</param>
        /// <returns>A list of dvd's with the given title</returns>
        public List<AmazonCatalogEntry> GetVideoOnDemandByTitle(string title)
        {
            return ItemSearch(string.Format("Title={0}", title), "UnboxVideo");
        }

        /// <summary>
        /// Return a DVD list from amazon.com by title
        /// </summary>
        /// <param name="title">The title of the DVD to search for</param>
        /// <returns>A list of dvd's with the given title</returns>
        public List<AmazonCatalogEntry> GetDVDSByTitle(string title)
        {
            return ItemSearch(string.Format("Title={0}", title), "DVD");
        }

        /// <summary>
        /// Return a Book list from amazon.com by title
        /// </summary>
        /// <param name="title">The title of the book to search for</param>
        /// <returns>A list of books with the given title</returns>
        public List<AmazonCatalogEntry> GetBooksByTitle(string title)
        {
            return ItemSearch(string.Format("Title={0}", title), "Books");
        }

        /// <summary>
        /// Return books by author
        /// </summary>
        /// <param name="author">The author (creator of the book</param>
        /// <returns>Books by the author</returns>
        public List<AmazonCatalogEntry> GetBooksByAuthor(string author)
        {
            return ItemSearch(string.Format("Author={0}", author), "Books");
        }

        /// <summary>
        /// Get Books by Author and Title
        /// </summary>
        /// <param name="title">The title of the book</param>
        /// <param name="author">The author of the book</param>
        /// <returns>A list of books with the given title and author</returns>
        public List<AmazonCatalogEntry> GetBooksByTitleAndAuthor(string title, string author)
        {
            return ItemSearch(string.Format("Title={0}&Author={1}", title, author), "Books");
        }
        
        /// <summary>
        /// Get the Detailed page for the Amazon catalog
        /// </summary>
        /// <param name="isbn">The ASIN or isbn for the page</param>
        /// <returns>The detailed catalog data for the page</returns>
        public List<AmazonCatalogEntry> GetDetailsPage(string key)
        {
            try
            {
                List<string> accesskey = Utilities.GetApiKey("http://www.amazon.com", "AccessKey");
                List<string> associateTag = Utilities.GetApiKey("http://www.amazon.com", "AssociateTag");

                if ((accesskey.Count == 1) && (associateTag.Count == 1))
                {
                    //ItemPage=4&Sort=titlerank&
                    XDocument xd = XDocument.Load(string.Format("http://www.amazon.com/dp/{0}/?tag={1}", key, associateTag[0]));
                    var AmazonBooks = (from n in xd.Descendants(ns + "Item")
                                       let IsMissingAuthor = n.Element(ns + "Author")
                                       let IsMissingTitle = n.Element(ns + "Title")
                                       let IsMissingDetailedUrl = n.Element(ns + "DetailedUrl")
                                       select new AmazonCatalogEntry
                                       {
                                           Title = (IsMissingTitle == null) ? null : n.Element(ns + "Title").Value.ToString().Trim(),
                                       }).ToList();
                    return AmazonBooks;
                }
                else return null;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        #endregion
    }
}
