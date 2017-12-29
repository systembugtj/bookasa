using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.IO.Packaging;
using System.Net;
//using System.ServiceModel.Syndication;
using System.Text;

namespace Arcadia.Bookasa.Format.ePUB
{
    public class ImageContentComponent : ContentComponent
    {
        private new byte[] _content;
        private string _originalUri;

        public string OriginalUri
        {
            get { return _originalUri; }
            set { _originalUri = value; }
        }

        public new byte[] Content
        {
            get { return _content; }
            set { _content = value; }
        }

        public ImageContentComponent(string ImageUrl)
        {
            _originalUri = ImageUrl;
            Guid imgguid = Guid.NewGuid(); 
            string imagename = imgguid.ToString("N");
            _uriString = @"images/" + imagename.ToString();
            _partUri = PackUriHelper.CreatePartUri(new Uri(UriString, UriKind.Relative));

            try
            {
                WebClient wc = new WebClient();
                _content = wc.DownloadData(ImageUrl);
                _mediaMimeType = wc.ResponseHeaders["Content-type"];

                if (!(_content.Length > 0))
                {
                    throw new NotSupportedException("No image data found");
                }
            }
            catch (Exception ex)
            {
                // Unable to retrieve image
                Console.WriteLine(ex.Message);
                _content = File.ReadAllBytes("not-found.gif");
                _mediaMimeType = System.Net.Mime.MediaTypeNames.Image.Gif;
            }
        }

        public override Stream ToStream()
        {
            return new MemoryStream(_content);
        }
    }
}

