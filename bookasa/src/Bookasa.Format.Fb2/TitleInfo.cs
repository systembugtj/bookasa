using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Arcadia.Bookasa.Format.Fb2
{
    public class TitleInfo
    {
        private XmlNode theNode;
        private XmlNode langNode;
        private XmlNode srclangNode;

        internal TitleInfo(XmlNode node) 
        {
            if (node == null)
            {
                throw new ArgumentNullException();
            }

            this.theNode = node;
            this.Genres = new List<GenreType>();
            this.Authors = new List<AuthorType>();
            this.Translators = new List<AuthorType>();
            this.Sequence = new List<SequenceType>();
            this.Coverpage = new List<ImageType>();

            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "genre": this.Genres.Add(new GenreType(childNode));
                        break;
                    case "author": this.Authors.Add(new AuthorType(childNode));
                        break;
                    case "book-title": this.BookTitle = new TextFieldType(childNode);
                        break;
                    case "annotation": this.Annotation = new AnnotationType(childNode);
                        break;
                    case "keywords": this.Keywords = new TextFieldType(childNode);
                        break;
                    case "date": this.Date = new DateType(childNode);
                        break;
                    case "coverpage": this.ReadImages(childNode); 
                        break;
                    case "lang": this.langNode = childNode;
                        break;
                    case "src-lang": this.srclangNode = childNode;
                        break;
                    case "translator": this.Translators.Add(new AuthorType(childNode));
                        break;
                    case "sequence": this.Sequence.Add(new SequenceType(childNode));
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Read images sequence for the Cover
        /// </summary>
        /// <param name="childNode">A XmlNode</param>
        private void ReadImages(XmlNode node)
        {
            foreach (XmlNode childNode in node.ChildNodes)
            {
                if (childNode.Name == "image")
                {
                    this.Coverpage.Add(new ImageType(childNode));
                }
            }
        }

        /// <summary>
        /// Genres of this book with the optional match percentage.
        /// </summary>
        public IList<GenreType> Genres { get; private set; }

        /// <summary>
        /// Author(s) for this book.
        /// </summary>
        public IList<AuthorType> Authors { get; private set; }

        /// <summary>
        /// Book title.
        /// </summary>
        public TextFieldType BookTitle { get; private set; }

        /// <summary>
        /// Annotation for this book.
        /// </summary>
        public AnnotationType Annotation { get; private set; }

        /// <summary>
        /// Any keywords for this book intended for use in search engines.
        /// </summary>
        public TextFieldType Keywords { get; private set; }

        /// <summary>
        /// Date this book was written. Can be not exact. e.g. 1863-1867. 
        /// If an optional attribute is present the it should contain some computer readable date from the interval use by search and indexing engines.
        /// </summary>
        public DateType Date { get; private set; }

        /// <summary>
        /// Any coverage items. Only images.
        /// </summary>
        public IList<ImageType> Coverpage { get; private set; }

        public string Language 
        {
            get
            {
                if (langNode != null) return langNode.InnerText;
                return null;
            }
            set 
            {
                if (langNode != null)
                {
                    langNode.InnerText = value;
                }
            }
        }

        /// <summary>
        /// Book's source language if this is a translation.
        /// </summary>
        public string SourceLanguage 
        {
            get
            {
                if (srclangNode != null) return srclangNode.InnerText;
                return null;
            }
            set
            {
                if (srclangNode != null)
                {
                    srclangNode.InnerText = value;
                }
            }
        }

        /// <summary>
        /// Translators if this book is a translation.
        /// </summary>
        public IList<AuthorType> Translators { get; private set; }

        /// <summary>
        /// Any sequences of this book might be part of.
        /// </summary>
        public IList<SequenceType> Sequence { get; private set; }

    }
}
