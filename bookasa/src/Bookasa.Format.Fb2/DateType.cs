using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Arcadia.Bookasa.Format.Fb2
{
    public class DateType
    {
        private XmlNode theNode;

        internal DateType(XmlNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }

            theNode = node;
        }

        public DateTime Value
        {
            get
            {
                if (theNode.Attributes.Count > 0)
                {
                    return DateTime.Parse(theNode.Attributes[0].InnerText);
                }
                return DateTime.MinValue;
            }
            set
            {
                if (theNode.Attributes.Count > 0)
                {
                    theNode.Attributes[0].InnerText = value.ToString();
                }
            }
        }

        public string Text
        {
            get
            {
                return theNode.InnerText;
            }
            set
            {
                theNode.InnerText = value;
            }
        }
    }
}
