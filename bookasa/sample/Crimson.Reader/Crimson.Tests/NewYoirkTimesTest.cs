using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Crimson.Catalog;

namespace Crimson.Catalog.Tests
{
    [TestFixture]
    public class NewYoirkTimesTest
    {
        [Test]
        public void BestSellerListsTest()
        {
            NewYorkTimes nyt = new NewYorkTimes();
            nyt.BestSellerLists();
        }

        [Test]
        public void NewsWireTest()
        {
            NewYorkTimes nyt = new NewYorkTimes();
            nyt.NewsWire();
        }

        [Test]
        public void MovieReviewsTest()
        {
            NewYorkTimes nyt = new NewYorkTimes();
            nyt.MovieReviews();
        }
    }
}
