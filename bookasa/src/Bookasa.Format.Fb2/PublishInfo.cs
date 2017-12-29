using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Arcadia.Bookasa.Format.Fb2
{
    public class PublishInfo
    {
        XmlNode theNode;
        XmlNode nodeYear;

        internal PublishInfo(XmlNode node)
        {
            if (node == null)
                throw new ArgumentNullException("node");

            theNode = node;
            this.Sequence = new List<SequenceType>();

            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "book-name": this.BookName = new TextFieldType(childNode);
                        break;
                    case "publisher": this.Publisher = new TextFieldType(childNode);
                        break;
                    case "city": this.City = new TextFieldType(childNode);
                        break;
                    case "year": this.nodeYear = childNode;
                        break;
                    case "isbn": this.Isbn = new TextFieldType(childNode);
                        break;
                    case "sequence": this.Sequence.Add(new SequenceType(childNode));
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Original (paper) book name.
        /// </summary>
        public TextFieldType BookName { get; set; }

        /// <summary>
        /// Original (paper) book publisher name.
        /// </summary>
        public TextFieldType Publisher { get; set; }

        /// <summary>
        /// City where the original (paper) book was published.
        /// </summary>
        public TextFieldType City { get; set; }

        /// <summary>
        /// Year of the (original) publication.
        /// </summary>
        public int Year 
        {
            get
            {
                if (nodeYear != null)
                    return int.Parse(nodeYear.InnerText);
                return 0;
            }
            set
            {
                if (nodeYear != null)
                    nodeYear.InnerText = value.ToString();
            }
        }

        public TextFieldType Isbn { get; set; }

        public IList<SequenceType> Sequence { get; private set; }

    }
}
