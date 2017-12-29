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
using Crimson.Catalog.Services.Interfaces;
using Crimson.Catalog.Services.Library;

namespace Crimson.Catalog.Services.NewYorkTimes
{
    /// <summary>
    /// Access to the New York Times Api's
    /// </summary>
    public class NewYorkTimes : INewYorkTimes
    {
        #region The NewYork time bestseller lists
        /// <summary>
        /// The best sellers lists from the New York Times
        /// </summary>
        /// <returns>The list of best seller lists</returns>
        public List<string> BestSellerLists()
        {
            try 
            {
                List<string> lists = Utilities.GetApiKey("http://www.nytimes.com", "BestSellerKey");

                if (lists.Count == 1)
                {
                    XDocument xd = XDocument.Load(string.Format("http://api.nytimes.com/svc/books/v2/lists/names.xml?api-key={0}", lists[0]));
                    return (from list in xd.Descendants("list_name")
                            select list.Value).ToList();
                }
                else return null;
            }
            catch (Exception e)
            {
                throw new Exception("The New York Times Best Seller Lists are not Available at this time", e);
            }
        }

        /// <summary>
        /// Return the BestSeller list whose name is listname and was published on the publicationdate.  
        /// </summary>
        /// <param name="listname">The name of the best seller list to retrieve</param>
        /// <param name="publicationdate">The publcation date of the best seller list</param>
        /// <param name="offset">The offsett in the list (must be a multiple of 20)</param>
        /// <returns>The next block of bestsellers for this list starting at the offsett</returns>
        public BestSellerList BestSellers(string listname, DateTime publicationdate, int offset)
        {
            try
            {
                List<string> lists = Utilities.GetApiKey("http://www.nytimes.com", "BestSellerKey");

                if (lists.Count == 1)
                {
                    string uri = string.Format("http://api.nytimes.com/svc/books/v2/lists/{0:yyyy-MM-dd}/{1}.xml?offset={2}&api-key={3}",
                        publicationdate, listname, offset, lists[0]);
                    XDocument xd = XDocument.Load(uri);

                    // Load the best seller data to a bestseller list
                    //return (from list in xd.Descendants("Results")
                    //        select new BestSellerList
                    //        {
                    //            ListName = list.Descendants("list_name").First().Value.Trim(),
                    //            BestSellersDate = DateTime.Parse(list.Descendants("bestsellers_date").First().Value.Trim()),
                    //            PublishedDate = DateTime.Parse(list.Descendants("published_date").First().Value.Trim()),
                    //            BestSellers = (
                    //                from books in xd.Descendants("book")
                    //                select new BestSeller
                    //                {
                    //                    Rank = Convert.ToInt16(books.Element("rank").Value.Trim()),
                    //                    WeeksOnList = books.Element("weeks_on_list").Value.Trim(),
                    //                    RankLastWeek = books.Element("rank_last_week").Value.Trim(),
                    //                    Asterisk = books.Element("asterisk").Value.Trim(),
                    //                    Dagger = books.Element("dagger").Value.Trim(),
                    //                    BookDetails = (from book in books.Descendants("book_detail")
                    //                                   select new BookDetail
                    //                                   {
                    //                                       Title = book.Element("title").Value,
                    //                                       Description = book.Element("description").Value.Trim(),
                    //                                       Contributor = book.Element("contributor").Value.Trim(),
                    //                                       Author = book.Element("author").Value.Trim(),
                    //                                       ContributorNote = book.Element("contributor_note").Value.Trim(),
                    //                                       Price = book.Element("price").Value.Trim(),
                    //                                       AgeGroup = book.Element("age_group").Value.Trim(),
                    //                                       Publisher = book.Element("publisher").Value.Trim(),
                    //                                   }).ToList(),
                    //                    ISBN13 = (from isbn in list.Descendants("isbn13")
                    //                              select isbn.Value.Trim()).ToList(),
                    //                    ISBN10 = (from isbn in list.Descendants("isbn10")
                    //                              select isbn.Value.Trim()).ToList(),
                    //                    BookReviews = (from bookreview in list.Descendants("review")
                    //                                   select new BookReview
                    //                                   {
                    //                                       BookReviewLink = bookreview.Element("book_review_link").Value.Trim(),
                    //                                       FirstChapterLink = bookreview.Element("first_chapter_link").Value.Trim(),
                    //                                       SundayReviewLink = bookreview.Element("sunday_review_link").Value.Trim(),
                    //                                       ArticleChapterLink = bookreview.Element("article_chapter_link").Value.Trim(),
                    //                                   }).ToList(),
                    //                }),
                    //        }).First();
                    return null;
                }
                else return null;
            }
            catch (Exception e)
            {
                throw new Exception("The New York Times Best Seller Lists are not Available at this time", e);
            }
        }
        #endregion

        #region Newswire articles
        /// <summary>
        /// Return the articles from the new york times for the last 24 hours
        /// </summary>
        /// <param name="offset">The offset into the newsfeed, should be a multiple of the count</param>
        /// <param name="count">The count of articles to return.  This can not be greater then 20</param>
        /// <returns>A list of articles from the last 24 hours</returns>
        public List<NewsWire> TodaysNewsWire(int offset, int count)
        {
            try
            {
                List<string> lists = Utilities.GetApiKey("http://www.nytimes.com", "NewsWireKey");
                List<NewsWire> articlelist = new List<NewsWire>(60);

                if (lists.Count == 1)
                {
                    XDocument xd = XDocument.Load(string.Format("http://api.nytimes.com/svc/news/v2/all/last24hours.xml?limit={0}&offset={1}&api-key={2}", count, offset, lists[0]));

                    var summary = (from results in xd.Descendants("result_set")
                                   select new
                                    {
                                        articlecount = Convert.ToInt16(results.Element("num_results").Value),
                                        copyright = results.Element("copyright").Value,
                                        status = results.Element("status").Value
                                    }).Single();

                    if ((summary.status != "OK") || (summary.articlecount == 0))
                        return null;

                    foreach (NewsWire a in ArticleList(xd, summary.copyright))
                        articlelist.Add(a);
                    return articlelist;
                }
                else return null;
            }
            catch (Exception e)
            {
                throw new Exception("The New York Times NewsWire is not Available at this time", e);
            }
        }

        /// <summary>
        /// Return the articles from the new york times for a recent period
        /// </summary>
        /// <param name="offset">The offset into the newsfeed, should be a multiple of the count</param>
        /// <param name="count">The count of articles to return.  This can not be greater then 20</param>
        /// <returns>A list of recent articles</returns>
        public List<NewsWire> RecentNewsWire(int offset, int count)
        {
            try
            {
                List<string> lists = Utilities.GetApiKey("http://www.nytimes.com", "NewsWireKey");
                List<NewsWire> articlelist = new List<NewsWire>(count);

                if (lists.Count == 1)
                {
                    XDocument xd = XDocument.Load(string.Format("http://api.nytimes.com/svc/news/v2/all/recent.xml?limit=10&offset={0}&api-key={1}", count, offset, lists[0]));

                    var summary = (from results in xd.Descendants("result_set")
                                   select new
                                   {
                                       articlecount = Convert.ToInt16(results.Element("num_results").Value),
                                       copyright = results.Element("copyright").Value,
                                       status = results.Element("status").Value
                                   }).Single();

                    if ((summary.status != "OK") || (summary.articlecount == 0))
                        return null;

                    foreach (NewsWire a in ArticleList(xd, summary.copyright))
                        articlelist.Add(a);
                    return articlelist;
                }
                else return null;
            }
            catch (Exception e)
            {
                throw new Exception("The New York Times NewsWire is not Available at this time", e);
            }
        }

        #region load the newswire articles to a local list
        /// <summary>
        /// Load the article list from the XDocument received from the New Yorktimes News Wire
        /// </summary>
        /// <param name="xd">The XDocument that contains the article list</param>
        /// <param name="copyright">The copyright string for the article list</param>
        /// <returns>The list of articles</returns>
        private List<NewsWire> ArticleList(XDocument xd, string copyright)
        {
            return (from article in xd.Descendants("news_item")
                    let IsMissingUrl = article.Attribute("url")
                    let IsMissingSection = article.Element("section")
                    let IsMissingBlogName = article.Element("blog_name")
                    let IsMissingByLine = article.Element("byline")
                    let IsMissingCategoriesTags = article.Element("categories_tags")
                    let IsMissingCreated = article.Element("created")
                    let IsMissingHeadline = article.Element("headline")
                    let IsMissingIndexingTerms = article.Element("indexing_terms")
                    let IsMissingKicker = article.Element("kicker")
                    let IsMissingLocations = article.Element("locations")
                    let IsMissingOrganizations = article.Element("organizations")
                    let IsMissingPeople = article.Element("people")
                    let IsMissingPlatform = article.Element("platform")
                    let IsMissingPubDate = article.Element("pubdate")
                    let IsMissingRelatedUrl = article.Element("relatedurl")
                    let IsMissingSource = article.Element("source")
                    let IsMissingSubHeadline = article.Element("subheadline")
                    let IsMissingSubSection = article.Element("subsection")
                    let IsMissingSubType = article.Element("subtype")
                    let IsMissingSummary = article.Element("summary")
                    let IsMissingTerms = article.Element("terms")
                    let IsMissingType = article.Element("type")
                    let IsMissingUpdated = article.Element("updated")
                    select new NewsWire
                    {
                        BlogName = (IsMissingBlogName == null) ? null : article.Element("blog_name").Value,
                        ByLine = (IsMissingByLine == null) ? null : article.Element("byline").Value,
                        CategoriesTags = (IsMissingCategoriesTags == null) ? null : article.Element("categories_tags").Value,
                        CopyRight = copyright,
                        Created = (IsMissingCreated == null) ? DateTime.Now : DateTime.Parse(article.Element("created").Value),
                        Headline = (IsMissingHeadline == null) ? null : article.Element("headline").Value,
                        IndexingTerms = (IsMissingIndexingTerms == null) ? null : article.Element("indexing_terms").Value,
                        Kicker = (IsMissingKicker == null) ? null : article.Element("kicker").Value,
                        Locations = (IsMissingLocations == null) ? null : article.Element("locations").Value,
                        Organizations = (IsMissingOrganizations == null) ? null : article.Element("organizations").Value,
                        People = (IsMissingPeople == null) ? null : article.Element("people").Value,
                        Platform = (IsMissingPlatform == null) ? null : article.Element("platform").Value,
                        PubDate = (IsMissingPubDate == null) ? DateTime.Now : DateTime.Parse(article.Element("pubdate").Value),
                        RelatedUrl = (IsMissingRelatedUrl == null) ? null : article.Element("relatedurl").Value,
                        Section = (IsMissingSection == null) ? null : article.Element("section").Value,
                        Source = (IsMissingSource == null) ? null : article.Element("source").Value,
                        SubHeadline = (IsMissingSubHeadline == null) ? null : article.Element("subheadline").Value,
                        SubSection = (IsMissingSubSection == null) ? null : article.Element("subsection").Value,
                        SubType = (IsMissingSubType == null) ? null : article.Element("subtype").Value,
                        Summary = (IsMissingSummary == null) ? null : article.Element("summary").Value,
                        Terms = (IsMissingTerms == null) ? null : article.Element("terms").Value,
                        Type = (IsMissingType == null) ? null : article.Element("type").Value,
                        Updated = (IsMissingUpdated == null) ? DateTime.Now : DateTime.Parse(article.Element("updated").Value),
                        Uri = (IsMissingUrl == null) ? null : article.Attribute("url").Value,
                        // The media items are optional and usually photos
                        MediaItems = (from m in article.Descendants("media_item")
                                      let IsMissingCaption = article.Element("caption")
                                      let IsMissingMediaCopyRight = article.Element("copyright")
                                      let IsMissingMediaSubType = article.Element("subtype")
                                      let IsMissingMediaType = article.Element("type")
                                      let IsMissingMediaUri = article.Element("uri")
                                      select new Media
                                      {
                                          Caption = (IsMissingCaption == null) ? null : article.Element("caption").Value,
                                          CopyRight = (IsMissingMediaCopyRight == null) ? null : article.Element("copyright").Value,
                                          SubType = (IsMissingMediaSubType == null) ? null : article.Element("subtype").Value,
                                          Type = (IsMissingMediaType == null) ? null : article.Element("type").Value,
                                          Uri = (IsMissingMediaUri == null) ? null : article.Element("url").Value,
                                      }).ToList()
                    }).ToList();
        }
        #endregion

        #endregion

        public void MovieReviews()
        {
            //WebClient reviews = new WebClient();
            //XDocument xd = XDocument.Load(
            //    new StringReader(reviews.DownloadString(Resources.NewYorkTimes.MovieReviews +
            //        "?api-key=" + Resources.NewYorkTimes.MovieReviewsAPI)));
        }
    }
}
