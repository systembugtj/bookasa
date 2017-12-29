using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;

namespace EpubSharp
{
    public class Document
    {
        //Container ContainerDefinition { get; set; }

        public void CreateDocument(string Filename, Metadata BookMetadata, List<ContentComponent> ContentFiles)
        {
            using (Package zp = ZipPackage.Open(Filename, System.IO.FileMode.Create))
            {
                MimetypeComponent mtc = new MimetypeComponent();
                PackagePart partMimeType = zp.CreatePart(mtc.PartUri, System.Net.Mime.MediaTypeNames.Text.Plain);
                Util.CopyStream(mtc.ToStream(), partMimeType.GetStream());

                ContainerComponent cc = new ContainerComponent();
                PackagePart partContainer = zp.CreatePart(cc.PartUri, System.Net.Mime.MediaTypeNames.Text.Xml);
                Util.CopyStream(cc.ToStream(), partContainer.GetStream());

                ContentOpfComponent opf = new ContentOpfComponent();
                opf.Metadata = BookMetadata;
                opf.ContentFiles = ContentFiles;
                PackagePart partOpf = zp.CreatePart(opf.PartUri, System.Net.Mime.MediaTypeNames.Text.Xml);
                Util.CopyStream(opf.ToStream(), partOpf.GetStream());

                TocComponent toc = new TocComponent();
                toc.Metadata = BookMetadata;
                toc.ContentFiles = ContentFiles;
                PackagePart partToc = zp.CreatePart(toc.PartUri, System.Net.Mime.MediaTypeNames.Text.Xml);
                Util.CopyStream(toc.ToStream(), partToc.GetStream());

                foreach (ContentComponent content in opf.ContentFiles)
                {
                    PackagePart partContent = zp.CreatePart(content.PartUri, content.MediaMimeType);
                    Util.CopyStream(content.ToStream(), partContent.GetStream());
                }

            } /* close and dispose of Package */
        }

    }
}
