using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Text;
using System.IO;
using System.Windows.Documents;
using System.Windows;
using System.Windows.Media;

namespace Silver.Documents
{
    public class ImportHtml
    {
        public FlowDocument Import(string path)
        {
            // Create the flow document
            FlowDocument fd = new FlowDocument();
            fd.IsHyphenationEnabled = true;
            fd.IsOptimalParagraphEnabled = true;
            fd.ColumnRuleWidth = 5;
            fd.FontSize = 12;
            fd.FontFamily = new FontFamily("Times New Roman");

            using (StreamReader reader = new StreamReader(path))
            {
                // setup the SgmlReader and load it into a XDocument
                Sgml.SgmlReader sgmlReader = new Sgml.SgmlReader();   
                sgmlReader.DocType = "HTML";   
                sgmlReader.WhitespaceHandling = WhitespaceHandling.All;   
                sgmlReader.CaseFolding = Sgml.CaseFolding.ToLower;   
                sgmlReader.InputStream = reader;   
                XDocument xd = XDocument.Load(sgmlReader);

                // Read the html page using linq
                var htmlpage = (from nodes in xd.Descendants()
                               select nodes);

                Section s;
                foreach(XElement p in htmlpage)
                {
                    switch (p.Name.LocalName)
                    {
                        case "body":
                            s = new Section();
                            ParseBody(s.Blocks, p);
                            break;
                        case "head":
                            s = new Section();
                            ParseHead(s.Blocks, p);
                            break;
                    }
                }
            }
            return fd;
        }

        public void ParseHead(BlockCollection b, XElement header)
        {
            // Read the html header using linq
            var htmlheader = (from nodes in header.Descendants()
                            select nodes);

            foreach (XElement headers in htmlheader)
            {
                switch (headers.Name.LocalName)
                {
                    case "title":
                        b.Add(new Paragraph(new Run(headers.Value.Trim())));
                        break;
                    default:
                        break;
                }
            }
        }

        public void ParseBody(BlockCollection b, XElement body)
        {
            // Read the html header using linq
            var htmlbody = (from nodes in body.Nodes()
                              select nodes);

            foreach (var element in htmlbody)
            {
                switch (element.NodeType)
                {
                    case XmlNodeType.Element:
                        XElement xelement = element as XElement;
                        switch (xelement.Name.LocalName)
                        {
                            case "a":
                                AddHyperlink(xelement, b);
                                break;
                            case "h1": case "h2": case "h3": 
                            case "h4": case "h5": case "h6":
                                b.Add(new Paragraph(new Run(xelement.Value.Trim())));
                                break;
                            case "p":
                                b.Add(new Paragraph(new Run(xelement.Value.Trim())));
                                break;
                            case "b":
                                b.Add(new Paragraph(new Bold(new Run(xelement.Value.Trim()))));
                                break;
                            case "br":
                                b.Add(new Paragraph(new LineBreak()));
                                break;
                            default:
                                Console.Error.Write(xelement.Name.LocalName + " ");
                                foreach (var att in xelement.Attributes())
                                    Console.Error.Write(att.Name + "=" + att.Value.Trim() + " ");
                                Console.Error.WriteLine();
                                break;
                        }
                        break;
                    case XmlNodeType.Text:
                        if (element.ToString().Trim().Length > 0)
                            Console.WriteLine(element.ToString().Trim());
                        break;
                    default:
                        break;
                }
            }
        }

        public void AddHyperlink(XElement e, BlockCollection b)
        {
            if (e.DescendantNodes().Count() == 0)
            {
                LinkTarget l = new LinkTarget();
                l.Name = e.Value.Trim();
                //b.Add(l);
            }
            else
            {
                Hyperlink h = new Hyperlink();
                //fd.Blocks.Add(new Hyperlink
                //Console.Write("<hyperlink>", xelement.Value.Trim());
                //foreach (var att in xelement.Attributes())
                //    Console.Write(att.Name + "=" + att.Value.Trim() + " ");
                //foreach (var child in xelement.Descendants())
                //    ParseBody(child);
                //Console.WriteLine("</hyperlink>", xelement.Value.Trim());
            }
        }
    }
}
