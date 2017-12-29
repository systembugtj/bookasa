using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Arcadia.Bookasa.Data.Watching;
using Arcadia.Bookasa.Scanner;

namespace Bookasa.Test
{
    [TestClass]
    public class ScannerTests
    {
        [TestMethod]
        public void TestDirectoryScanning()
        {

            LocalStorageScanner scanner = new LocalStorageScanner();

            WatchSource source =  new WatchSource();
            source.SourceUri = new Uri ("C:\\娱乐\\书籍");
            scanner.Scanning(source, null);
            
        }
    }
}
