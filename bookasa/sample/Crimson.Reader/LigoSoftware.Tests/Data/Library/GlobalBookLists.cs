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
using System.Linq;

namespace Crimson.Catalog.WebSite.Tests.Data.Library
{
    public class GlobalBookLists
    {
        /// <summary>
        /// Selected by the Guardian's Review team and a panel of expert judges, this list includes only novels – 
        /// no memoirs, no short stories, no long poems – from any decade and in any language.
        /// </summary>
        /// <returns>The 1000 Novels every must read as a BookList</returns>
        public Services.Library.BookList OneThousandNovelsEveryMustRead()
        {
            Services.Library.BookList OneThousand = new Services.Library.BookList()
                {
                    ListId = Guid.NewGuid(),
                    Name = "1000 Novels Everyone Must Read",
                    Source = "http://www.guardian.co.uk",
                    InternetUri = new Uri("http://www.guardian.co.uk/books/series/1000novels"),
                    Published = DateTime.Parse("01/23/2009"),
                    Description = "Selected by the Guardian's Review team and a panel of expert judges, this list includes only novels – no memoirs, no short stories, no long poems – from any decade and in any language. Originally published in thematic supplements – love, crime, comedy, family and self, state of the nation, science fiction and fantasy, war and travel – they appear here for the first time in a single list.",
                    Created = DateTime.Parse("01/23/2009"),
                    Modified = DateTime.Now
                };

            Services.Library.Creator c = new Services.Library.Creator();
            for (int i = 0; i < 1000; i++)
            {
                string[] book = Resources._1000NovelsEveryOneMustRead.ResourceManager.GetString(String.Format("String{0}", i), Resources._1000NovelsEveryOneMustRead.Culture).Split('|');

                // Create the book and set the title and the id
                Services.Library.Book thisBook = new Services.Library.Book { Title=book[0].Trim(), BookId=Guid.NewGuid() };
                OneThousand.Books.Add(thisBook);

                // Add the Author (if one exists)
                if (book.Length == 2)
                {
                    var creator = (from cr in OneThousand.Creators
                                   where cr.Fullname.ToUpper().Contains(book[1].ToUpper())
                                   select cr).ToList();

                    if (creator.Count == 0)
                    {
                        c = new Services.Library.Creator { Fullname = book[1].Trim(), CreatorId = Guid.NewGuid() };
                        OneThousand.Creators.Add(c);
                        OneThousand.BookCreators.Add(new Services.Library.BookCreator { BookId = thisBook.BookId, CreatorId = c.CreatorId });
                    }
                    else OneThousand.BookCreators.Add(new Services.Library.BookCreator { BookId=thisBook.BookId, CreatorId=creator.First().CreatorId });
                }
            }

            return OneThousand;
        }
    }
}
