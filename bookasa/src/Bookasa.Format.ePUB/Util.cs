using System;
using System.Collections.Generic;
using System.IO;

namespace Arcadia.Bookasa.Format.ePUB
{
    public class Util
    {
        /// <summary>
        ///   Copies data from a source stream to a target stream.</summary>
        /// <param name="source">
        ///   The source stream to copy from.</param>
        /// <param name="target">
        ///   The destination stream to copy to.</param>
        public static void CopyStream(Stream source, Stream target)
        {
            const int bufSize = 0x1000;
            byte[] buf = new byte[bufSize];
            int bytesRead = 0;
            while ((bytesRead = source.Read(buf, 0, bufSize)) > 0)
                target.Write(buf, 0, bytesRead);
        }

        public static string RemoveImgTags(string InputHtml)
        {
            string output = InputHtml;
            while (output.Contains("<img"))
            {
                int iStart = output.IndexOf("<img");
                int iEnd = output.IndexOf(">", iStart);
                string killString = output.Substring(iStart, (iEnd + 1) - iStart);
                output = output.Replace(killString, "");
            }
            return output;
        }

        public static List<string> ParseImageTags(string InputHtml)
        {
            List<string> imageUrls = new List<string>();
            string output = InputHtml;
            while (output.Contains("<img"))
            {
                int iTagStart = output.IndexOf("<img");
                int iTagEnd = output.IndexOf(">", iTagStart);
                int iStart = output.IndexOf("src=", iTagStart);
                int iEnd = output.IndexOf(" ", iStart);

                // If the SRC element is smooshed against the end of the IMG tag...
                if (iEnd > iTagEnd)
                {
                    iEnd = output.IndexOf("/>", iStart);
                }

                // Poorly formed. Not XHTML compliant, last gasp at trying to match...
                if (iEnd > iTagEnd)
                {
                    iEnd = iTagEnd;
                }

                string imageLocation = output.Substring(iStart + 4, (iEnd + 1) - (iStart + 4));
                imageLocation = imageLocation.Replace("\"", "");
                imageLocation = imageLocation.Replace("'", "");
                imageLocation = imageLocation.Trim();
                imageLocation = imageLocation.TrimEnd("/".ToCharArray());

                if (!imageUrls.Contains(imageLocation))
                {
                    imageUrls.Add(imageLocation);
                }
                string killString = output.Substring(iTagStart, (iTagEnd + 1) - iTagStart);
                output = output.Replace(killString, "");
            }
            return imageUrls;
            
        }

        public static byte[] ReadWholeArray(Stream stream, byte[] data)
        {
            int offset = 0;
            int remaining = data.Length;
            while (remaining > 0)
            {
                int read = stream.Read(data, offset, remaining);
                if (read <= 0)
                    throw new EndOfStreamException
                        (String.Format("End of stream reached with {0} bytes left to read", remaining));
                remaining -= read;
                offset += read;
            }
            return new byte[5];
        }


        public static byte[] StringToByteArray(string input)
        {
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            return encoding.GetBytes(input);
        }

        public static string ByteArrayToString(byte[] input)
        {
            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
            return enc.GetString(input);
        }
    }
}
