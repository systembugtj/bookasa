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
using System.Drawing;

namespace org.pdfclown.documents.interaction.annotations
{
  /**
    <summary>Pop-up annotation [PDF:1.6:8.4.5].</summary>
    <remarks>It displays text in a pop-up window for entry and editing.
    It typically does not appear alone but is associated with a markup annotation,
    its parent annotation, and is used for editing the parent's text.</remarks>
  */
  [PDF(VersionEnum.PDF13)]
  public sealed class Popup
    : Annotation
  {
    #region dynamic
    #region constructors
    public Popup(
      Page page,
      RectangleF box
      ) : base(
        page.Document,
        PdfName.Popup,
        box,
        page
        )
    {}

    public Popup(
      PdfDirectObject baseObject,
      PdfIndirectObject container
      ) : base(baseObject,container)
    {}
    #endregion

    #region interface
    #region public
    public override object Clone(
      Document context
      )
    {throw new NotImplementedException();}

    /**
      <summary>Gets/Sets whether the annotation should initially be displayed open.</summary>
    */
    public bool IsOpen
    {
      get
      {
        /*
          NOTE: 'Open' entry may be undefined.
        */
        PdfBoolean openObject = (PdfBoolean)BaseDataObject[PdfName.Open];
        if(openObject == null)
          return false;

        return openObject.RawValue;
      }
      set
      {BaseDataObject[PdfName.Open] = PdfBoolean.Get(value);}
    }

    /**
      <summary>Gets/Sets the parent annotation.</summary>
    */
    public Annotation Parent
    {
      get
      {
        /*
          NOTE: 'Parent' entry may be undefined.
        */
        PdfReference parentObject = (PdfReference)BaseDataObject[PdfName.Parent];
        if(parentObject == null)
          return null;

        return Annotation.Wrap(parentObject);
      }
      set
      {BaseDataObject[PdfName.Parent] = value.BaseObject;}
    }
    #endregion
    #endregion
    #endregion
  }
}