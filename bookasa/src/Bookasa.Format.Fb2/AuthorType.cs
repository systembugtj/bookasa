using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Arcadia.Bookasa.Format.Fb2
{
    public class AuthorType
    {
        private XmlNode theNode;

        internal AuthorType(XmlNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }
            theNode = node;
            this.Emails = new List<string>();
            this.Homepages = new List<string>();
            this.CreateFields();
        }

        public TextFieldType FirstName { get; private set; }

        public TextFieldType MiddleName { get; private set; }

        public TextFieldType LastName { get; private set; }

        public TextFieldType NickName { get; private set; }

        public IList<string> Homepages { get; private set; }

        public IList<string> Emails { get; private set; }

        private void CreateFields()
        {
            foreach (XmlNode node in this.theNode.ChildNodes)
            {
                switch (node.Name)
                {
                    case "first-name":
                        this.FirstName = new TextFieldType(node);
                        break;
                    case "middle-name":
                        this.MiddleName = new TextFieldType(node);
                        break;
                    case "last-name":
                        this.LastName = new TextFieldType(node);
                        break;
                    case "nickname":
                        this.NickName = new TextFieldType(node);
                        break;
                    case "home-page": this.Homepages.Add(node.InnerText);
                        break;
                    case "email": this.Emails.Add(node.InnerText);
                        break;
                    default:
                        break;
                }
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            if (this.FirstName != null)
            {
                sb.Append(this.FirstName.ToString());
                sb.Append(" ");
            }
            if (this.MiddleName != null)
            {
                sb.Append(this.MiddleName.ToString());
                sb.Append(" ");
            }
            if (this.LastName != null)
            {
                sb.Append(this.LastName.ToString());
                sb.Append(" ");
            }
            if (this.NickName != null)
            {
                sb.Append(this.NickName.ToString());
            }
            return sb.ToString();
        }
    }
}
