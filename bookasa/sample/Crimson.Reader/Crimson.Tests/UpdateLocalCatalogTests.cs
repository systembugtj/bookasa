using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Crimson.Catalog;

namespace Crimson.Catalog.Tests
{
    [TestFixture]
    public class UpdateCatalogTests
    {
        private Library local { get; set; }
        private Library SqlExpress { get; set; }

        [TestFixtureSetUp]
        public void OpenDataBase()
        {
            local = new Library("MyLibrary.sdf");
        }

        [Test]
        public void AUpdateCatalogTests()
        {
            Crimson.Catalog.Gutenberg.Catalog Gutenburg = new Crimson.Catalog.Gutenberg.Catalog() { LocalCatalog = local };
            Gutenburg.UpdateCatalogs();
        }

        [Test]
        public void AuthorSearchTest()
        {
            var TwainBooks = (from books in local.BookCreators
                              where books.Creator.Fullname.Contains("Twain")
                                &&  books.Contributor == false
                              select books.Book);

            Assert.AreEqual(TwainBooks.Count(), 206);
        }

        [Test]
        public void TitleSearchTest()
        {
            var SkylarkBook = (from books in local.Books
                               where books.Title.ToUpper().Contains("SkyLark Three".ToUpper())
                               select books);

            Assert.AreEqual(SkylarkBook.Count(), 1);
            Assert.AreEqual(SkylarkBook.First().BookCatalogId.ToUpper(), "etext21051".ToUpper());
            Console.WriteLine(SkylarkBook.First().BookId);
        }

        [Test]
        public void AuthorTitleSearchTest()
        {
            var SkylarkBook = (from ebook in local.Books
                               join bookcreator in local.BookCreators on
                                        ebook.BookId equals bookcreator.BookId
                                where ebook.Title.ToUpper().Contains("SkyLark".ToUpper())
                                   && bookcreator.Creator.Fullname.ToUpper().Contains("Smith".ToUpper())
                                select new
                                {
                                    ebook.BookId,
                                    ebook.BookCatalogId,
                                    ebook.FriendlyTitle,
                                    bookcreator.Creator.CreatorId,
                                    bookcreator.Creator.DisplayName
                                }).First();

            Assert.AreEqual(SkylarkBook.BookCatalogId.ToUpper(), "etext21051".ToUpper());
        }

        //[Test]
        //public void AuthorSubjectSearchTest()
        //{
        //    var BurroughsScienceFictions = (from books in Gutenburg.Catalog
        //                       where books.Value.Creators.First().FullName.ToUpper().Contains("burroughs".ToUpper()) &&
        //                             books.Value.LibraryofCongressSubjectHeadings.Contains("Science fiction")
        //                       select books);

        //    Assert.AreEqual(BurroughsScienceFictions.Count(), 13);
        //}

        [Test]
        public void CreatedIn2007Test()
        {
            var Created2007 = (from books in local.Books
                               where books.Created.Year == 2007
                               select books).ToList();

            Assert.AreEqual(Created2007.Count, 3898);
        }

        //[Test]
        //public void FilesModifiedIn2007Test()
        //{
        //    var FilesModified2007 = (from books in local.ETexts
        //                             where books.Value.Files.First().Value.Modified.Year == 2007
        //                             select books);

        //    Assert.AreEqual(FilesModified2007.Count(), 4099);
        //}

        //[Test]
        //public void TableOfContentsTest()
        //{
        //    string TOC = "The Descent of Man -- The Other Two -- Expiation -- The Lady's Maid's Bell -- The Mission of Jane -- The Reckoning -- The Letter -- The Dilettante -- The Quicksand -- A Venetian Night's Entertainment.";
        //    Assert.AreEqual(Gutenburg.Catalog["etext4519"].TableOfContents, TOC.Replace(" -- ", "\n"));
        //}

        //[Test]
        //public void AlternativeTitleTest()
        //{
        //    Assert.AreEqual(local.ETexts.F["etext3006"].AlternativeTitle, "Stalky and Company");
        //}

        //[Test]
        //public void SubjectTest()
        //{
        //    var SubjectValid = (from books in Gutenburg.Catalog
        //                             where books.Value.LibraryofCongressSubjectHeadings.Count() > 0
        //                             select books);

        //    Assert.AreEqual(13290, SubjectValid.Count());
        //    Assert.AreEqual(Gutenburg.Catalog["etext3012"].LibraryofCongressSubjectHeadings.Count(), 3);
        //    Assert.AreEqual(Gutenburg.Catalog["etext3012"].LibraryofCongressSubjectHeadings[0], "Comedies");
        //    Assert.AreEqual(Gutenburg.Catalog["etext3012"].LibraryofCongressSubjectHeadings[1], "Greek drama (Comedy) -- Translations into English");
        //    Assert.AreEqual(Gutenburg.Catalog["etext3012"].LibraryofCongressSubjectHeadings[2], "Peace treaties -- Drama");
        //    Assert.AreEqual(Gutenburg.Catalog["etext3012"].LCC.Length, 1);
        //    Assert.AreEqual(Gutenburg.Catalog["etext3012"].LCC[0], "PA");
        //}

        //[Test]
        //public void CreatorsNeverBornTest()
        //{
        //    var NeverBorn = (from books in Gutenburg.Catalog
        //                     where (books.Value.Creators.First().Died.Year > 1) && (books.Value.Creators.First().Born.Year == 1)
        //                     select books);

        //    Assert.AreEqual(NeverBorn.Count(), 325);
        //}

        //[Test]
        //public void CreatorsNeverDiedTest()
        //{
        //    var NeverDied = (from books in Gutenburg.Catalog
        //                     where (books.Value.Creators.First().Died.Year == 1) && (books.Value.Creators.First().Born.Year > 1)
        //                     select books);

        //    Assert.AreEqual(NeverDied.Count(), 652);
        //}

        //[Test]
        //public void CreatorsTest()
        //{
        //    Assert.AreEqual(Gutenburg.Catalog["etext27585"].Creators.Count, 2);
        //    Assert.AreEqual(Gutenburg.Catalog["etext27585"].Creators[0].FullName, "C. Creighton Mandell");
        //    Assert.AreEqual(Gutenburg.Catalog["etext27585"].Creators[1].FullName, "Edward Shanks");
        //}

        //[Test]
        //public void ContributorsTest()
        //{
        //    Assert.AreEqual(Gutenburg.Catalog["etext15022"].Contributors.Count, 3);
        //    Assert.AreEqual(Gutenburg.Catalog["etext15022"].Contributors[0], "Robertson, James Alexander, 1873-1939 [Editor]");
        //    Assert.AreEqual(Gutenburg.Catalog["etext15022"].Contributors[1], "Blair, Emma Helen, -1911 [Editor]");
        //}

        //[Test]
        //public void SubjectsTest()
        //{
        //    Assert.AreEqual(Gutenburg.Subjects.Count, 8521);
        //}

        //[Test]
        //public void LanguageTest()
        //{
        //    var Languages = (from books in Gutenburg.Catalog
        //                     where books.Value.Culture != null
        //                     orderby books.Value.Culture.EnglishName
        //                     select books.Value.Culture.EnglishName).Distinct();

        //    Assert.AreEqual(Languages.Count(), 29);
        //}

        //[Test]
        //public void SpanishBoooksTest()
        //{
        //    var SpanishBooks = (from books in Gutenburg.Catalog
        //                        where books.Value.Culture != null && books.Value.Culture.EnglishName == "Spanish"
        //                        select books.Value.Culture.EnglishName);

        //    Assert.AreEqual(SpanishBooks.Count(), 218);
        //}

        //[Test]
        //public void ScienceFictionBooksTest()
        //{
        //    var ScienceFictionBooks = (from books in Gutenburg.Catalog
        //                        where books.Value.LibraryofCongressSubjectHeadings != null &&
        //                                books.Value.LibraryofCongressSubjectHeadings.Contains("Science fiction")
        //                        select books.Value.Title);

        //    Assert.AreEqual(ScienceFictionBooks.Count(), 282);
        //}

        [TestFixtureTearDown]
        public void CloseDatabase()
        {
            local.Dispose();
            local = null;
        }
    }
}
