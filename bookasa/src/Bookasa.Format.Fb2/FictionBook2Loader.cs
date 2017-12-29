using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Fb2Format
{
    public class FictionBook2Loader
    {
        public FictionBook2 Load(string filename)
        {
            FictionBook2 book; 

            using (var reader = new System.IO.StreamReader(filename))
            {
                XmlReader xmlreader = XmlReader.Create(reader);
                if (!xmlreader.ReadToFollowing("FictionBook", "http://www.gribuser.ru/xml/fictionbook/2.0"))
                {
                    throw new FormatException("File is not a valid FB2 format file.");
                }

                // Go to description branch
                if (xmlreader.ReadToDescendant("description"))
                {
                    this.UpdateBook(book.Description, xmlreader);
                }

                // TODO Read Bodies

                // TODO Read Binaries
            }

            return book;
        }

        private void UpdateBook(Description description, XmlReader xmlreader)
        {
            if (xmlreader.ReadToFollowing("title-info"))
            {
                this.UpdateBook(description.TitleInfo, xmlreader);
            }

            if (xmlreader.ReadToFollowing("document-info"))
            {
            }

            if (xmlreader.ReadToFollowing("publish-info"))
            {
            }

            if (xmlreader.ReadToFollowing("custom-info"))
            {
            }

        }

        private void UpdateBook(TitleInfo titleInfo, XmlReader xmlreader)
        {
            // Read Genres [1..*]
            if (xmlreader.ReadToDescendant("genre"))
            {
                titleInfo.Genres.Add(new GenreType() { Value = xmlreader.ReadString() } );

                while (xmlreader.ReadToNextSibling("genre"))
                {
                    titleInfo.Genres.Add(new GenreType() { Value = xmlreader.ReadString() });
                }
            }
        }
    }
}
