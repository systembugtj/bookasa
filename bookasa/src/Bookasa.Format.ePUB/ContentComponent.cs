using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
//using System.ServiceModel.Syndication;
using System.Text;

namespace Arcadia.Bookasa.Format.ePUB
{
    public class ContentComponent : Component
    {
        protected string _itemId;
        protected string _mediaMimeType;
        protected string _title;

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public string Content
        {
            get { return _content; }
            set { _content = value; }
        }

        public string ItemId
        {
            get { return _itemId; }
            set { _itemId = value; }
        }

        public string MediaMimeType
        {
            get { return _mediaMimeType; }
            set { _mediaMimeType = value; }
        }

        public ContentComponent()
        {
        }

        private void BuildContent()
        {
            StringBuilder sb = new StringBuilder();

            if (_mediaMimeType.Contains("css"))
            {
                sb.AppendLine("body { background-color: white; }");
            }
            else
            {
                sb.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                sb.AppendLine("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.1//EN\" \"http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd\">");
                sb.AppendLine("<html xmlns=\"http://www.w3.org/1999/xhtml\" xml:lang=\"en\">");
                sb.AppendLine("<head>");
                sb.Append("   <title>");
                sb.Append(_title);
                sb.Append("</title>");
                sb.AppendLine("   <link rel=\"stylesheet\" type=\"text/css\" href=\"style.css\" />");
                sb.AppendLine("</head>");
                sb.Append("<body>");
                sb.Append(_content);
                sb.Append("</body>");
                sb.AppendLine("</html>");
            }

            _content = sb.ToString();
        }

        public override System.IO.Stream ToStream()
        {
            BuildContent();

            return new MemoryStream(Util.StringToByteArray(_content));
        }
    }
}
