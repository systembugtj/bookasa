using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ICSharpCode.SharpZipLib.Tests.FileSystem.Zip
{

    /// <summary>
    /// Need to implement IsolatedStorage aware classes in main lib
    /// </summary>
    //[TestClass]
    public class WindowsNameTransformHandling_IsolatedStorage : TransformBase
    {

        [TestMethod]
        public void BasicDirectories()
        {
            WindowsNameTransform wnt = new WindowsNameTransform();
            wnt.TrimIncomingPaths = false;

            string tutu = Path.GetDirectoryName("\\bogan\\ping.txt");
            TestDirectory(wnt, "d/", "d");
            TestDirectory(wnt, "d", "d");
            TestDirectory(wnt, "absolute/file2", @"absolute\file2");

            const string BaseDir1 = @"C:\Dir";
            wnt.BaseDirectory = BaseDir1;

            TestDirectory(wnt, "talofa", Path.Combine(BaseDir1, "talofa"));

            const string BaseDir2 = @"C:\Dir\";
            wnt.BaseDirectory = BaseDir2;

            TestDirectory(wnt, "talofa", Path.Combine(BaseDir2, "talofa"));
        }
    }
}