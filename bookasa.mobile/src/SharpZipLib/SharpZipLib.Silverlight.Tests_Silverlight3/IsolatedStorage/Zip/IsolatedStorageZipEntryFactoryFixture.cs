using System;
using System.IO;
using System.IO.IsolatedStorage;
using ICSharpCode.SharpZipLib.Zip;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Salient.SharpZipLib.Zip;


namespace ICSharpCode.SharpZipLib.Tests.IsolatedStorage
{
    [TestClass]
    public class IsolatedStorageZipEntryFactoryFixture
    {
        [TestMethod]
        public void Noid()
        {
            using (IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (IsolatedStorageFileStream fs = store.CreateFile("foo.txt"))
                {
                    using (var writer = new StreamWriter(fs))
                    {
                        writer.Write("Foo Bar FU!");
                    }
                }

                store.CreateDirectory("foobarfu");
                string fooText = Guid.NewGuid().ToString();
                using (var createdFile = store.CreateFile("foo.txt"))
                {
                    using (var writer = new StreamWriter(createdFile))
                    {

                        writer.Write(fooText);
                    }
                }

                using (var createdFile = store.CreateFile("foobarfu\\foo.txt"))
                {
                    using (var writer = new StreamWriter(createdFile))
                    {

                        writer.Write(fooText);
                    }
                }


                var factory = new IsolatedZipEntryFactory();

                ZipEntry fentry = factory.MakeFileEntry("foo.txt");

                ZipEntry dentry = factory.MakeDirectoryEntry("foobarfu");


                using (var f = IsolatedZipFile.Create("foo.zip"))
                {
                    f.BeginUpdate();
                    f.Add("foo.txt");
                    f.CommitUpdate();
                }

                using (var f = new IsolatedZipFile("foo.zip"))
                {
                    var entry = f.GetEntry("foo.txt");
                    string content = new StreamReader(f.GetInputStream(entry)).ReadToEnd();
                    Assert.AreEqual(fooText, content);
                }

                var fz = new IsolatedFastZip();
                fz.CreateZip("fz.zip", "foobarfu", true, "");
            }
        }

    }
}
