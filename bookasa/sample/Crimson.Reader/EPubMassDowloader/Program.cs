using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace EPubMassDowloader
{
    class Program
    {
        static void Main(string[] args)
        {
            WebClient wc = new WebClient();
            DateTime start;
            for (int i = 0; i < 29000; i++)
            {
                if (! File.Exists(string.Format("pg{0}.epub", i)))
                {
                    try
                    {
                        start = DateTime.Now;
                        wc.DownloadFile(string.Format("http://www.gutenberg.org/cache/epub/{0}/pg{0}.epub", i),
                            string.Format(@"pg{0}.epub", i));
                        Console.WriteLine("Downloaded Etext{0} at {1} (in {2} seconds", i, DateTime.Now, DateTime.Now-start);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Exception: {0} {1}", string.Format(@"pg{0}.epub", i), e.Message);
                    }
                }

                if (! File.Exists(string.Format(@"pg{0}-images.epub", i)))
                {
                    try
                    {
                        start = DateTime.Now;
                        wc.DownloadFile(string.Format("http://www.gutenberg.org/cache/epub/{0}/pg{0}-images.epub", i),
                            string.Format(@"pg{0}-images.epub", i));
                        Console.WriteLine("Downloaded Etext{0} (with images) at {1} (in {2} seconds", i, DateTime.Now, DateTime.Now - start);
                    }
                    catch (Exception e)
                    {
                        if (e.GetType().ToString() != "System.Net.WebException")
                            Console.WriteLine("Exception: {0} {1}:{2}", string.Format(@"pg{0}-images.epub", i), 
                                e.GetType().ToString(), e.Message);
                    }
                }
            }
            Console.ReadLine();
        }
    }
}
