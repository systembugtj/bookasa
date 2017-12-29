using System;
using System.IO;
using System.IO.Packaging;
using System.Text;

namespace EpubSharp
{
    public class ContainerComponent : Component
    {
        private string _opfContentFile;

        public ContainerComponent() : this("content.opf") { }

        public ContainerComponent(string OpfContentFile)
        {
            _opfContentFile = OpfContentFile;
            _uriString = @"META-INF\container.xml";
            _partUri = PackUriHelper.CreatePartUri(new Uri(UriString, UriKind.Relative));

            BuildContent();
        }

        private void BuildContent()
        {
            StringBuilder sbContent = new StringBuilder();
            sbContent.AppendLine("<?xml version=\"1.0\"?>");
            sbContent.AppendLine("<container version=\"1.0\" xmlns=\"urn:oasis:names:tc:opendocument:xmlns:container\">");
            sbContent.AppendLine("   <rootfiles>");
            sbContent.Append("      <rootfile full-path=\"");
            sbContent.Append(_opfContentFile);
            sbContent.AppendLine("\" media-type=\"application/oebps-package+xml\"/>");
            sbContent.AppendLine("   </rootfiles>");
            sbContent.AppendLine("</container>");
            _content = sbContent.ToString();
        }

        public override Stream ToStream()
        {
            return new MemoryStream(Util.StringToByteArray(_content));
        }
    }
}
