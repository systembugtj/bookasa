//using System;
//using System.Collections.Generic;
//using Amazon.ECS;
//using Amazon.ECS.Model;
//using Amazon.ECS.Query;
//using Crimson.Catalog;

//namespace Crimson.Catalog
//{
//    /// <summary>
//    /// Query Amazon for information on a particular book and title.
//    /// </summary>
//    public class AmazonCatalog
//    {
//        /// <summary>
//        /// The Amazon.com Associates Web Server
//        /// </summary>
//        public AmazonECS WebService { get; set; }

//        public List<Book> BookSearch(string author, string title)
//        {
//            Book book;
//            ItemSearchResponse response;
//            ItemSearchRequest request = new ItemSearchRequest();

//            try
//            {
//                request.SearchIndex = "Books";
//                request.Author = author;
//                request.Title = title;
//                response = WebService.ItemSearch(request);
//            }
//            catch (Exception e)
//            {
//                throw e;
//            }

//            //Check for null response
//            if (response == null)
//                throw new Exception("Server Error - no response recieved!");

//            List<Book> BookList = new List<Book>();
//            foreach (var items in response.Items)
//            {
//                foreach (var item in items.Item)
//                {
//                    book = new Book();
//                    //book.CatalogId = 4;
//                    book.BookCatalogId = item.ASIN;
//                    book.Title = item.ItemAttributes.Title;
//                    //book.Publisher = item.ItemAttributes.Publisher;
//                    //book.Type = item.ItemAttributes.Binding;
//                    BookList.Add(book);
//                }
//            }
//            return BookList;
//        }

//        public List<Book> BookTitleSearch(string title)
//        {
//            Book book;
//            ItemSearchResponse response;
//            ItemSearchRequest request = new ItemSearchRequest();

//            try
//            {
//                request.SearchIndex = "Books";
//                request.Title = title;
//                response = WebService.ItemSearch(request);
//            }
//            catch (Exception e)
//            {
//                throw e;
//            }

//            //Check for null response
//            if (response == null)
//                throw new Exception("Server Error - no response recieved!");

//            List<Book> BookList = new List<Book>();
//            foreach (var items in response.Items)
//            {
//                foreach (var item in items.Item)
//                {
//                    book = new Book();
//                    //book.CatalogId = 4;
//                    book.BookCatalogId = item.ASIN;
//                    book.Title = item.ItemAttributes.Title;
//                    //book.Publisher = item.ItemAttributes.Manufacturer ?? item.ItemAttributes.Publisher;
//                    //book.Type = item.ItemAttributes.ProductGroup;
//                    BookList.Add(book);
//                }
//            }
//            return BookList;
//        }

//        public AmazonCatalog()
//        {
//            WebService = new AmazonECSQuery(Resources.Amazon.AccessKeyId, Resources.Amazon.AssociateTag);
//        }
//    }
//}
