using System;
using System.Collections.Generic;
using System.Linq;
//using System.ServiceModel.Syndication;
using System.Text;

namespace Arcadia.Bookasa.Format.ePUB
{
    public class Metadata
    {
        private string _title;
        private string _language;
        private string _identifier;
        private string _creator;
        private string _contributor;
        private string _publisher;
        private string _subject;
        private string _description;
        private DateTime _date;
        private string _type;
        private string _format;
        private string _source;
        private string _relation;
        private string _coverage;
        private string _rights;

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public string Language
        {
            get { return _language; }
            set { _language = value; }
        }

        public string Identifier
        {
            get { return _identifier; }
            set { _identifier = value; }
        }

        public string Creator
        {
            get { return _creator; }
            set { _creator = value; }
        }

        public string Contributor
        {
            get { return _contributor; }
            set { _contributor = value; }
        }

        public string Publisher
        {
            get { return _publisher; }
            set { _publisher = value; }
        }

        public string Subject
        {
            get { return _subject; }
            set { _subject = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public string Format
        {
            get { return _format; }
            set { _format = value; }
        }

        public string Source
        {
            get { return _source; }
            set { _source = value; }
        }

        public string Relation
        {
            get { return _relation; }
            set { _relation = value; }
        }

        public string Coverage
        {
            get { return _coverage; }
            set { _coverage = value; }
        }

        public string Rights
        {
            get { return _rights; }
            set { _rights = value; }
        }

        public Metadata()
        {
        }

        public string ToXmlShard()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("   <metadata xmlns:dc=\"http://purl.org/dc/elements/1.1/\" xmlns:dcterms=\"http://purl.org/dc/terms/\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:opf=\"http://www.idpf.org/2007/opf\">");

            // Required Headers
            sb.Append("      <dc:title>");
            sb.Append(_title);
            sb.AppendLine("</dc:title>");

            sb.Append("      <dc:language xsi:type=\"dcterms:RFC3066\">");
            sb.Append(_language);
            sb.AppendLine("</dc:language>");

            sb.Append("      <dc:identifier id=\"dcidid\">");
            sb.Append(_identifier);
            sb.AppendLine("</dc:identifier>");

            //Optional Headers
            sb.Append("      <dc:creator>");
            sb.Append(_creator);
            sb.AppendLine("</dc:creator>");

            sb.Append("      <dc:contributor>");
            sb.Append(_contributor);
            sb.AppendLine("</dc:contributor>");

            sb.Append("      <dc:publisher>");
            sb.Append(_publisher);
            sb.AppendLine("</dc:publisher>");

            sb.Append("      <dc:subject>");
            sb.Append(_subject);
            sb.AppendLine("</dc:subject>");

            sb.Append("      <dc:description>");
            sb.Append(_description);
            sb.AppendLine("</dc:description>");

            sb.Append("      <dc:date xsi:type=\"dcterms:W3CDTF\">");
            sb.Append(_date.ToString("yyyy-MM-dd"));
            sb.AppendLine("</dc:date>");

            sb.Append("      <dc:type>");
            sb.Append(_type);
            sb.AppendLine("</dc:type>");

            sb.Append("      <dc:format>");
            sb.Append(_format);
            sb.AppendLine("</dc:format>");

            sb.Append("      <dc:source>");
            sb.Append(_source);
            sb.AppendLine("</dc:source>");

            sb.Append("      <dc:relation>");
            sb.Append(_relation);
            sb.AppendLine("</dc:relation>");

            sb.Append("      <dc:coverage>");
            sb.Append(_coverage);
            sb.AppendLine("</dc:coverage>");

            sb.Append("      <dc:rights>");
            sb.Append(_rights);
            sb.AppendLine("</dc:rights>");

            sb.AppendLine("   </metadata>");

            return sb.ToString();
        }
    }
}
