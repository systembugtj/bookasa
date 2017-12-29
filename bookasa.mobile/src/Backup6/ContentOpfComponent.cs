using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Text;

namespace EpubSharp
{
    public class ContentOpfComponent : Component
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

        public ContentOpfComponent()
        {
            _uriString = @"content.opf";
            _partUri = PackUriHelper.CreatePartUri(new Uri(UriString, UriKind.Relative));
        }

        private void BuildContent()
        {
            StringBuilder sbContentOpf = new StringBuilder();

            sbContentOpf.AppendLine("<?xml version=\"1.0\"?>");
            sbContentOpf.AppendLine("<package xmlns=\"http://www.idpf.org/2007/opf\" unique-identifier=\"dcidid\" version=\"2.0\">");
            
            sbContentOpf.AppendLine(Metadata.ToXmlShard());

            sbContentOpf.AppendLine("   <manifest>");

            foreach (ContentComponent cc in _contentFiles)
            {
                sbContentOpf.Append("      <item id=\"");
                sbContentOpf.Append(cc.ItemId);
                sbContentOpf.Append("\"      href=\"");
                sbContentOpf.Append(cc.PartUri.ToString().TrimStart("/".ToCharArray()));
                sbContentOpf.Append("\" media-type=\"");
                sbContentOpf.Append(cc.MediaMimeType);
                sbContentOpf.AppendLine("\" />");
            }

            sbContentOpf.AppendLine("   </manifest>");

            sbContentOpf.AppendLine("");

            sbContentOpf.AppendLine("   <spine toc=\"ncx\">");

            foreach (ContentComponent cc in _contentFiles)
            {
                if (cc.MediaMimeType == "application/xhtml+xml")
                {
                    sbContentOpf.Append("      <itemref idref=\"");
                    sbContentOpf.Append(cc.ItemId);
                    sbContentOpf.AppendLine("\" />");
                }
            }

            sbContentOpf.AppendLine("   </spine>");

            sbContentOpf.AppendLine("");

            sbContentOpf.AppendLine("</package>");

            _content = sbContentOpf.ToString();

        }

        public override System.IO.Stream ToStream()
        {
            BuildContent();

            return new MemoryStream(Util.StringToByteArray(_content));
        }
    }
}
