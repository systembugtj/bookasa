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
using System.Collections.Generic;
using System.Drawing;

namespace org.pdfclown.documents.interaction.annotations
{
  /**
    <summary>Free text annotation [PDF:1.6:8.4.5].</summary>
    <remarks>It displays text directly on the page. Unlike an ordinary text annotation,
    a free text annotation has no open or closed state;
    instead of being displayed in a pop-up window, the text is always visible.</remarks>
  */
  [PDF(VersionEnum.PDF13)]
  public sealed class CalloutNote
    : Annotation
  {
    #region types
    /**
      <summary>Callout line [PDF:1.6:8.4.5].</summary>
    */
    public class LineObject
      : PdfObjectWrapper<PdfArray>
    {
      #region dynamic
      #region fields
      private Page page;
      #endregion

      #region constructors
      public LineObject(
        Page page,
        PointF start,
        PointF end
        ) : base(
          page.File,
          new PdfArray()
          )
      {
        this.page = page;

        PdfArray baseDataObject = BaseDataObject;
        double pageHeight = page.Box.Height;
        baseDataObject.Add(new PdfReal(start.X));
        baseDataObject.Add(new PdfReal(pageHeight - start.Y));
        baseDataObject.Add(new PdfReal(end.X));
        baseDataObject.Add(new PdfReal(pageHeight - end.Y));
      }

      public LineObject(
        Page page,
        PointF start,
        PointF knee,
        PointF end
        ) : base(
          page.File,
          new PdfArray()
          )
      {
        this.page = page;

        PdfArray baseDataObject = BaseDataObject;
        double pageHeight = page.Box.Height;
        baseDataObject.Add(new PdfReal(start.X));
        baseDataObject.Add(new PdfReal(pageHeight - start.Y));
        baseDataObject.Add(new PdfReal(knee.X));
        baseDataObject.Add(new PdfReal(pageHeight - knee.Y));
        baseDataObject.Add(new PdfReal(end.X));
        baseDataObject.Add(new PdfReal(pageHeight - end.Y));
      }

      internal LineObject(
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

      public PointF End
      {
        get
        {
          PdfArray coordinates = BaseDataObject;
          if(coordinates.Count < 6)
            return new PointF(
              (float)((IPdfNumber)coordinates[2]).RawValue,
              (float)(page.Box.Height - ((IPdfNumber)coordinates[3]).RawValue)
              );
          else
            return new PointF(
              (float)((IPdfNumber)coordinates[4]).RawValue,
              (float)(page.Box.Height - ((IPdfNumber)coordinates[5]).RawValue)
              );
        }
      }

      public PointF? Knee
      {
        get
        {
          PdfArray coordinates = BaseDataObject;
          if(coordinates.Count < 6)
            return null;

          return new PointF(
            (float)((IPdfNumber)coordinates[2]).RawValue,
            (float)(page.Box.Height - ((IPdfNumber)coordinates[3]).RawValue)
            );
        }
      }

      public PointF Start
      {
        get
        {
          PdfArray coordinates = BaseDataObject;

          return new PointF(
            (float)((IPdfNumber)coordinates[0]).RawValue,
            (float)(page.Box.Height - ((IPdfNumber)coordinates[1]).RawValue)
            );
        }
      }
      #endregion
      #endregion
      #endregion
    }

    /**
      <summary>Text justification [PDF:1.6:8.4.5].</summary>
    */
    public enum JustificationEnum
    {
      /**
        <summary>Left.</summary>
      */
      Left,
      /**
        <summary>Center.</summary>
      */
      Center,
      /**
        <summary>Right.</summary>
      */
      Right
    };
    #endregion

    #region static
    #region fields
    private static readonly Dictionary<JustificationEnum,PdfInteger> JustificationEnumCodes;
    #endregion

    #region constructors
    static CalloutNote()
    {
      JustificationEnumCodes = new Dictionary<JustificationEnum,PdfInteger>();
      JustificationEnumCodes[JustificationEnum.Left] = new PdfInteger(0);
      JustificationEnumCodes[JustificationEnum.Center] = new PdfInteger(1);
      JustificationEnumCodes[JustificationEnum.Right] = new PdfInteger(2);
    }
    #endregion

    #region interface
    #region private
    /**
      <summary>Gets the code corresponding to the given value.</summary>
    */
    private static PdfInteger ToCode(
      JustificationEnum value
      )
    {return JustificationEnumCodes[value];}

    /**
      <summary>Gets the justification corresponding to the given value.</summary>
    */
    private static JustificationEnum ToJustificationEnum(
      PdfInteger value
      )
    {
      foreach(KeyValuePair<JustificationEnum,PdfInteger> justification in JustificationEnumCodes)
      {
        if(justification.Value.Equals(value))
          return justification.Key;
      }
      return JustificationEnum.Left;
    }
    #endregion
    #endregion
    #endregion

    #region dynamic
    #region constructors
    public CalloutNote(
      Page page,
      RectangleF box,
      string text
      ) : base(
        page.Document,
        PdfName.FreeText,
        box,
        page
        )
    {Text = text;}

    public CalloutNote(
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
      <summary>Gets/Sets the justification to be used in displaying the annotation's text.</summary>
    */
    public JustificationEnum Justification
    {
      get
      {return ToJustificationEnum((PdfInteger)BaseDataObject[PdfName.Q]);}
      set
      {BaseDataObject[PdfName.Q] = ToCode(value);}
    }

    /**
      <summary>Gets/Sets the callout line attached to the free text annotation.</summary>
    */
    public LineObject Line
    {
      get
      {
        /*
          NOTE: 'CL' entry may be undefined.
        */
        PdfArray calloutLineObject = (PdfArray)BaseDataObject[PdfName.CL];
        if(calloutLineObject == null)
          return null;

        return new LineObject(calloutLineObject,Container);
      }
      set
      {BaseDataObject[PdfName.CL] = value.BaseObject;}
    }
    #endregion
    #endregion
    #endregion
  }
}