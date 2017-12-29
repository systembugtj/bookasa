

using ICSharpCode.SharpZipLib.Core;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace ICSharpCode.SharpZipLib.Tests.Core
{
	[TestClass]
	public class Core
	{

		[TestMethod]
		public void FilterQuoting()
		{
			string[] filters = NameFilter.SplitQuoted("");
			Assert.AreEqual(0, filters.Length);
			
			filters = NameFilter.SplitQuoted(";;;");
			Assert.AreEqual(4, filters.Length);
			foreach(string filter in filters) {
				Assert.AreEqual("", filter);
			}

			filters = NameFilter.SplitQuoted("a;a;a;a;a");
			Assert.AreEqual(5, filters.Length);
			foreach (string filter in filters) {
				Assert.AreEqual("a", filter);
			}

			filters = NameFilter.SplitQuoted(@"a\;;a\;;a\;;a\;;a\;");
			Assert.AreEqual(5, filters.Length);
			foreach (string filter in filters) {
				Assert.AreEqual("a;", filter);
			}
		}

		[TestMethod]
		public void ValidFilter()
		{
			Assert.IsTrue(NameFilter.IsValidFilterExpression("a"));
			Assert.IsFalse(NameFilter.IsValidFilterExpression(@"\,)"));
		}
	}
}
