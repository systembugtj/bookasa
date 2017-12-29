using ICSharpCode.SharpZipLib.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ICSharpCode.SharpZipLib.Tests.FileSystem.Zip
{
    public class TransformBase : ZipBase
    {
        protected void TestFile(INameTransform t, string original, string expected)
        {
            string transformed = t.TransformFile(original);
            Assert.AreEqual(expected, transformed, "Should be equal");
        }

        protected void TestDirectory(INameTransform t, string original, string expected)
        {
            string transformed = t.TransformDirectory(original);
            Assert.AreEqual(expected, transformed, "Should be equal");
        }
    }
}