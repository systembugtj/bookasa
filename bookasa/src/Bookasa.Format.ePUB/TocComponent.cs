using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.IO.Packaging;
using System.Text;

namespace Arcadia.Bookasa.Format.ePUB
{
    class TocComponent : Component
    {

        private List<ContentComponent> _contentFiles;
        private Metadata _metadata;

        public List<ContentComponent> ContentFiles
        {
            get { return _contentFiles; }
            set { _contentFiles = value; }
        }

        public Metadata Metadata
        {
            get { return _metadata; }
            set { _metadata = value; }
        }

        public TocComponent()
        {
            _uriString = @"toc.ncx";
            _partUri = PackUriHelper.CreatePartUri(new Uri(UriString, UriKind.Relative));

        }

        private void BuildContent()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<?xml version=\"1.0\"?>");
            sb.AppendLine("<!DOCTYPE ncx PUBLIC \"-//NISO//DTD ncx 2005-1//EN\"   \"http://www.daisy.org/z3986/2005/ncx-2005-1.dtd\">");
            sb.AppendLine("");
            sb.AppendLine("<ncx xmlns=\"http://www.daisy.org/z3986/2005/ncx/\" version=\"2005-1\">");
            sb.AppendLine("");
            sb.AppendLine("   <head>");
            sb.AppendLine("      <meta name=\"dtb:uid\" content=\"http://www.hxa.name/articles/content/epup-guide_hxa7241_2007_1.epub\"/>");
            sb.AppendLine("      <meta name=\"dtb:depth\" content=\"1\"/>");
            sb.AppendLine("      <meta name=\"dtb:totalPageCount\" content=\"0\"/>");
            sb.AppendLine("      <meta name=\"dtb:maxPageNumber\" content=\"0\"/>");
            sb.AppendLine("   </head>");
            sb.AppendLine("");
            sb.AppendLine("   <docTitle>");
            sb.Append("      <text>");
            sb.Append(_metadata.Title);
            sb.AppendLine("</text>");
            sb.AppendLine("   </docTitle>");
            sb.AppendLine("");
            sb.AppendLine("   <navMap>");

            int iPartCount = 1;
            foreach (ContentComponent cc in _contentFiles)
            {
                if (cc.MediaMimeType.Contains("xhtml"))
                {
                    sb.Append("      <navPoint id=\"navPoint-");
                    sb.Append(iPartCount.ToString());
                    sb.Append("\" playOrder=\"");
                    sb.Append(iPartCount.ToString());
                    sb.AppendLine("\">");
                    sb.AppendLine("         <navLabel>");
                    sb.Append("            <text>");
                    sb.Append(cc.Title);
                    sb.AppendLine("</text>");
                    sb.AppendLine("         </navLabel>");
                    sb.Append("         <content src=\"");
                    sb.Append(cc.PartUri.ToString().TrimStart("/".ToCharArray()));
                    sb.AppendLine("\"/>");
                    sb.AppendLine("      </navPoint>");
                }
            }

            sb.AppendLine("   </navMap>");
            sb.AppendLine("");
            sb.AppendLine("</ncx>");

            _content = sb.ToString();
        }

        public override System.IO.Stream ToStream()
        {
            BuildContent();

            return new MemoryStream(Util.StringToByteArray(_content));
        }

    }
}
