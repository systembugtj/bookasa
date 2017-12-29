﻿using System;
using System.IO;
using System.IO.Compression;
using System.IO.Packaging;

namespace Arcadia.Bookasa.Format.ePUB
{
    class MimetypeComponent : Component
    {
        public MimetypeComponent()
        {
            _uriString = "mimetype";
            _partUri = PackUriHelper.CreatePartUri(new Uri(UriString, UriKind.Relative));

            BuildContent();
        }

        private void BuildContent()
        {
            _content = "application/epub+zip";
        }

        public override Stream ToStream()
        {
            return new MemoryStream(Util.StringToByteArray(_content));
        }
    }
}