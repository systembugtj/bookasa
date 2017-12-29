using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Arcadia.Bookasa.Format.Fb2
{
    public class GenreType
    {
        private XmlNode theNode = null;

        internal GenreType(XmlNode node)
        {
            theNode = node;
        }

        public string Value 
        {
            get 
            { 
                if (theNode != null) 
                {
                    return theNode.InnerText;
                }
                return null;
            }
            set 
            {
                if (theNode != null)
                {
                    theNode.InnerText = value;
                }
            }
        }

        public int Match 
        {
            get
            {
                if (theNode.Attributes.Count > 0)
                {
                    return int.Parse(theNode.Attributes[0].Value);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                if (theNode.Attributes.Count > 0)
                {
                    theNode.Attributes[0].Value = value.ToString();
                }
                else
                {
                    //theNode.Attributes.Append(new XmlAttribute() { Value = value.ToString() });
                }
            }
        }
    }
}
