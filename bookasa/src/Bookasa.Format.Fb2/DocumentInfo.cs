using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Arcadia.Bookasa.Format.Fb2
{
    public class DocumentInfo
    {
        private XmlNode theNode;
        private XmlNode nodeId;
        private XmlNode nodeVersion;

        internal DocumentInfo(XmlNode node)
        {
            if (node == null)
                throw new ArgumentNullException("node");

            theNode = node;

            this.Authors = new List<AuthorType>();
            this.SourceUrl = new List<string>();

            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "author": this.Authors.Add(new AuthorType(childNode));
                        break;
                    case "program-used": this.ProgramUsed = new TextFieldType(childNode);
                        break;
                    case "date": this.Date = new DateType(childNode);
                        break;
                    case "src-url": this.SourceUrl.Add(childNode.InnerText);
                        break;
                    case "src-ocr": this.SourceOcr = new TextFieldType(childNode);
                        break;
                    case "id": this.nodeId = childNode;
                        break;
                    case "version": this.nodeVersion = childNode;
                        break;
                    case "history": this.History = new AnnotationType(childNode);
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Author(s) of this particular document.
        /// </summary>
        public IList<AuthorType> Authors { get; private set; }

        /// <summary>
        /// Any software used in preparation of this document, in free format.
        /// </summary>
        public TextFieldType ProgramUsed { get; set; }

        /// <summary>
        /// Date this document was created, same guidelines as in the TitleInfo section apply.
        /// </summary>
        public DateType Date { get; set; }

        /// <summary>
        /// Source URL if this document is a conversion of of some other (online) document.
        /// </summary>
        public IList<string> SourceUrl { get; private set; }

        /// <summary>
        /// Author of the original (online) document if this is a conversion.
        /// </summary>
        public TextFieldType SourceOcr { get; set; }

        /// <summary>
        /// This is a unique identifier for a document. This must not change unless you make substantial updates to the document.
        /// </summary>
        public string Id {
            get
            {
                if (nodeId != null)
                    return nodeId.InnerText;
                return null;
            }
            set
            {
                if (nodeId != null)
                    nodeId.InnerText = value;
            }
        }

        /// <summary>
        /// Document version, in free format should be incremented if the document is changed and re-leased to the public.
        /// </summary>
        public float Version 
        {
            get
            {
                if (nodeVersion != null)
                    return float.Parse(nodeVersion.InnerText);
                return 0.0f;
            }
            set
            {
                if (nodeVersion != null)
                    nodeVersion.InnerText = value.ToString();
            }
        }

        public AnnotationType History { get; set; }

    }
}
