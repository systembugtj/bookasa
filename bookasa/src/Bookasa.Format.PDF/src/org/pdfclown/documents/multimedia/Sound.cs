/*
  Copyright 2008-2010 Stefano Chizzolini. http://www.pdfclown.org

  Contributors:
    * Stefano Chizzolini (original code developer, http://www.stefanochizzolini.it)

  This file should be part of the source code distribution of "PDF Clown library" (the
  Program): see the accompanying README files for more info.

  This Program is free software; you can redistribute it and/or modify it under the terms
  of the GNU Lesser General Public License as published by the Free Software Foundation;
  either version 3 of the License, or (at your option) any later version.

  This Program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY,
  either expressed or implied; without even the implied warranty of MERCHANTABILITY or
  FITNESS FOR A PARTICULAR PURPOSE. See the License for more details.

  You should have received a copy of the GNU Lesser General Public License along with this
  Program (see README files); if not, go to the GNU website (http://www.gnu.org/licenses/).

  Redistribution and use, with or without modification, are permitted provided that such
  redistributions retain the above copyright notice, license and disclaimer, along with
  this list of conditions.
*/

using org.pdfclown.bytes;
using org.pdfclown.documents;
using org.pdfclown.objects;

using System;

namespace org.pdfclown.documents.multimedia
{
//TODO: this is just a stub.
  /**
    <summary>Sound object [PDF:1.6:9.2].</summary>
  */
  [PDF(VersionEnum.PDF12)]
  public sealed class Sound
    : PdfObjectWrapper<PdfStream>
  {
    #region dynamic
    #region constructors
    /**
      <summary>Creates a new sound within the given document context.</summary>
    */
    public Sound(
      Document context,
      IInputStream stream
      ) : base(
        context.File,
        new PdfStream(
          new PdfDictionary(
            new PdfName[]{PdfName.Type},
            new PdfDirectObject[]{PdfName.Sound}
            )
          )
        )
    {throw new NotImplementedException("Process the sound stream!");}

    public Sound(
      PdfDirectObject baseObject
      ) : base(
        baseObject,
        null // NO container (streams are self-contained).
        )
    {}
    #endregion

    #region interface
    #region public
    public override object Clone(
      Document context
      )
    {throw new NotImplementedException();}
    #endregion
    #endregion
    #endregion
  }
}