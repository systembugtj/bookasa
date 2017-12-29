using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows;
using ICSharpCode.SharpZipLib.GZip;
using Salient.SharpZipLib;
using Salient.SharpZipLib.Zip;

namespace QuickStart
{
    public partial class MainPage
    {


        #region FastZip

        /// <summary>
        /// </summary>
        private void FastZipToISO(object sender, RoutedEventArgs e)
        {
            DeleteSampleText();
            DeleteSampleZip();

            CreateLocalSampleFile(SampleTextFileName);

            /*
             *  Create  an archive using IsolatedFastZip
             *************************************************************
             */

            new IsolatedFastZip()
                .CreateZip(SampleZipFileName, "", false, SampleTextFileName);

            /*************************************************************
             * 
             */

            DisplayMetrics("FastZipToISO", false);
            DeleteSampleText();
            UnzipFromISOButton.IsEnabled = true;
            ZipFileFromISOButton.IsEnabled = true;
            ZipStreamFromISOButton.IsEnabled = false;
        }



        /// <summary>
        /// </summary>
        private void FastZipFromISO(object sender, RoutedEventArgs e)
        {
            DeleteSampleText();

            /*
             *  Extract an archive using IsolatedFastZip
             *************************************************************
             */

            new IsolatedFastZip()
                .ExtractZip(SampleZipFileName, "", null);

            /*************************************************************
             * 
             */

            DisplayMetrics("FastZipFromISO", false);
        }

        #endregion


        #region ZipFile

        /// <summary>
        /// </summary>
        private void ZipFileToISO(object sender, RoutedEventArgs e)
        {
            DeleteSampleText();
            DeleteSampleZip();

            CreateLocalSampleFile(SampleTextFileName);

            /*
             *  Create an archive with ZipFile
             *************************************************************
             */

            IsolatedZipFile zip = IsolatedZipFile.Create(SampleZipFileName);

            zip.BeginUpdate();
            zip.Add(SampleTextFileName);
            zip.CommitUpdate();
            zip.Close();

            /*************************************************************
             * 
             */

            DisplayMetrics("CreateIsoZipFile", false);
            DeleteSampleText();
            UnzipFromISOButton.IsEnabled = true;
            ZipFileFromISOButton.IsEnabled = true;
            ZipStreamFromISOButton.IsEnabled = false;
        }


        /// <summary>
        /// </summary>
        private void ZipFileFromISO(object sender, RoutedEventArgs e)
        {
            DeleteSampleText();

            /*
             *  Extract an archive using IsolatedZipFile
             *************************************************************
             */

            using (var zip = new IsolatedZipFile(SampleZipFileName))
            {
                var entry = zip.GetEntry(SampleTextFileName);
                using (var streamIn = zip.GetInputStream(entry))
                {
                    using (var store = IsolatedStorageFile.GetUserStoreForApplication())
                    {
                        using (var streamOut = store.OpenFile(SampleTextFileName, FileMode.Create))
                        {
                            streamIn.CopyTo(streamOut);
                        }
                    }
                }
            }

            /*************************************************************
             * 
             */

            DisplayMetrics("ZipFileFromISO", false);
        }

        #endregion
   

        #region gzip stream

        /// <summary>
        /// </summary>
        private void ZipStreamToISO(object sender, RoutedEventArgs e)
        {
            DeleteSampleText();
            DeleteSampleZip();

            /*
             *  Compress a stream to ISO
             *************************************************************
             */

            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (var fileStreamOut = store.CreateFile(SampleZipFileName))
                {
                    using (var zipStreamOut = new GZipOutputStream(fileStreamOut))
                    {
                        using (var streamIn = typeof(App).Assembly
                            .GetManifestResourceStream("QuickStart.SampleText.txt"))
                        {
                            streamIn.CopyTo(zipStreamOut);
                        }
                    }
                }
            }

            /*************************************************************
             * 
             */

            DisplayMetrics("ZipStreamToISO", true);
            DeleteSampleText();
            UnzipFromISOButton.IsEnabled = false;
            ZipFileFromISOButton.IsEnabled = false;
            ZipStreamFromISOButton.IsEnabled = true;
        }



        /// <summary>
        /// </summary>
        private void ZipStreamFromISO(object sender, RoutedEventArgs e)
        {
            DeleteSampleText();

            /*
             *  Extract a file from ISO using stream
             *************************************************************
             */
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (var fileStreamIn = store.OpenFile(SampleZipFileName, FileMode.Open))
                {
                    using (var zipStreamIn = new GZipInputStream(fileStreamIn))
                    {
                        using (var fileStreamOut = store.OpenFile(SampleTextFileName, FileMode.Create))
                        {
                            zipStreamIn.CopyTo(fileStreamOut);
                        }
                    }
                }
            }

            /*************************************************************
             * 
             */

            DisplayMetrics("ZipStreamFromISO", false);
        }
        
        #endregion
     


        /// <summary>
        /// </summary>
        private void ProcessGzippedWebResponse(object sender, RoutedEventArgs e)
        {
            // this is a json service endpoint that ALWAYS serves gzipped content
            WebUtil.GetResponseText("http://stackauth.com/sites/", s => OutputTextBlock.Text = s);

            // the code you should be looking at is in WebUtil.
        }


        #region Support

        private static void CreateLocalSampleFile(string filename)
        {
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (var streamOut = store.OpenFile(filename, FileMode.Create))
                using (var streamIn = typeof(App).Assembly.GetManifestResourceStream("QuickStart.SampleText.txt"))
                {
                    streamIn.CopyTo(streamOut);
                }
            }
        }

        private static void DeleteSampleText()
        {
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (store.FileExists(SampleTextFileName))
                    store.DeleteFile(SampleTextFileName);
            }
        }

        private static void DeleteSampleZip()
        {
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (store.FileExists(SampleZipFileName))
                    store.DeleteFile(SampleZipFileName);
            }
        }

        private void DisplayMetrics(string title, bool inputFromStream)
        {
            long originalSize = 0;
            long compressedSize;
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (!inputFromStream)
                {
                    originalSize = store.GetFileSize(SampleTextFileName);
                }

                compressedSize = store.GetFileSize(SampleZipFileName);
            }

            const string outFmt = "{2}\r\n---------------------------------\r\nOriginal Size:{0}\r\nCompressed Size:{1}";
            OutputTextBlock.Text = String.Format(outFmt, originalSize, compressedSize, title);
        }

        #endregion

        #region Constants

        private const string SampleTextFileName = "SampleText.txt";

        private const string SampleZipFileName = "SampleText.zip";

        #endregion

        #region Constructors

        public MainPage()
        {
            InitializeComponent();
        }

        #endregion
    }

    /// <summary>
    /// </summary>
    public static class WebUtil
    {

        public static void GetResponseText(string url, Action<string> action)
        {
            var request = WebRequest.CreateHttp(url);

            request.BeginGetResponse(ar =>
                {
                    string responseText;
                    using (var response = request.EndGetResponse(ar))
                        responseText = response.GetResponseText();
                    Deployment.Current.Dispatcher.BeginInvoke(() => action(responseText));
                }, null);
        }

        public static string GetResponseText(this WebResponse response)
        {
            using (Stream stream = response.IsCompressed()
                                       ? new GZipInputStream(response.GetResponseStream())
                                       : response.GetResponseStream())
            {
                return new StreamReader(stream).ReadToEnd();
            }
        }

        public static bool IsCompressed(this WebResponse response)
        {
            return Regex.IsMatch((response.Headers["Content-Encoding"] ?? "")
                                     .ToLower(), "(gzip|deflate)");
        }


    }
}
