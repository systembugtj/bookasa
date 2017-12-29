using System;
using System.IO;
using System.IO.Compression;
using System.IO.Packaging;

namespace Arcadia.Bookasa.Format.ePUB
{
    public abstract class Component
    {
        protected Uri _partUri;
        protected string _uriString;
        protected string _content;

        public string UriString
        {
            get
            {
                return _uriString;
            }
            set
            {
                _uriString = value;
                _partUri = PackUriHelper.CreatePartUri(new Uri(UriString, UriKind.Relative));
            }
        }
        public Uri PartUri
        {
            get
            {
                return _partUri;
            }
        }

        public abstract Stream ToStream();
    }
}
