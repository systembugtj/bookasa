using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Arcadia.Bookasa.Format.Fb2
{
    public class Description
    {
        private XmlNode theNode;

        internal Description(XmlNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException();
            }

            this.theNode = node;

            this.CustomInfo = new List<CustomInfoType>();

            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "title-info":
                        this.TitleInfo = new TitleInfo(childNode);
                        break;
                    case "document-info":
                        this.DocumentInfo = new DocumentInfo(childNode);
                        break;
                    case "publish-info":
                        this.PublishInfo = new PublishInfo(childNode);
                        break;
                    case "custom-info":
                        this.CustomInfo.Add(new CustomInfoType(childNode));
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Generic information about the book.
        /// </summary>
        public TitleInfo TitleInfo { get; private set; }

        /// <summary>
        /// Information about this particular (xml) document.
        /// </summary>
        public DocumentInfo DocumentInfo { get; private set; }

        /// <summary>
        /// Information about some paper/outher published document. that was used as a source of this xml document.
        /// </summary>
        public PublishInfo PublishInfo { get; private set; }

        /// <summary>
        /// Any other information about the book/documentthat didn't fit in the above groups.
        /// </summary>
        public IList<CustomInfoType> CustomInfo { get; private set; }
    }
}
