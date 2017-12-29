/*
  Copyright 2006-2010 Stefano Chizzolini. http://www.pdfclown.org

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

using org.pdfclown;
using org.pdfclown.documents;
using org.pdfclown.files;
using org.pdfclown.objects;

using System;
using System.Collections;
using System.Collections.Generic;

namespace org.pdfclown.documents.interaction.navigation.document
{
  /**
    <summary>Collection of bookmarks [PDF:1.6:8.2.2].</summary>
  */
  [PDF(VersionEnum.PDF10)]
  public sealed class Bookmarks
    : PdfObjectWrapper<PdfDictionary>,
      IList<Bookmark>
  {
    #region dynamic
    #region constructors
    public Bookmarks(
      Document context
      ) : base(
        context.File,
        new PdfDictionary(
          new PdfName[2]
          {
            PdfName.Type,
            PdfName.Count
          },
          new PdfDirectObject[2]
          {
            PdfName.Outlines,
            new PdfInteger(0)
          }
          )
        )
    {}

    internal Bookmarks(
      PdfDirectObject baseObject
      ) : base(
        baseObject,
        null // NO container (bookmark MUST be an indirect object [PDF:1.6:8.2.2]).
        )
    {}
    #endregion

    #region interface
    #region public
    public override object Clone(
      Document context
      )
    {throw new NotImplementedException();}

    #region IList
    public int IndexOf(
      Bookmark bookmark
      )
    {throw new NotImplementedException();}

    public void Insert(
      int index,
      Bookmark bookmark
      )
    {throw new NotImplementedException();}

    public void RemoveAt(
      int index
      )
    {throw new NotImplementedException();}

    public Bookmark this[
      int index
      ]
    {
      get
      {
        PdfReference item = (PdfReference)BaseDataObject[PdfName.First];
        while(index > 0)
        {
          item = (PdfReference)((PdfDictionary)File.Resolve(item))[PdfName.Next];
          // Did we go past the collection range?
          if(item == null)
            throw new ArgumentOutOfRangeException();

          index--;
        }

        return new Bookmark(item);
      }
      set
      {
        throw new NotImplementedException();
      }
    }

    #region ICollection
    public void Add(
      Bookmark bookmark
      )
    {
      /*
        NOTE: Bookmarks imported from alien PDF files MUST be cloned
        before being added.
      */
      bookmark.BaseDataObject[PdfName.Parent] = BaseObject;

      PdfInteger countObject = EnsureCountObject();
      // Is it the first bookmark?
      if((int)countObject.Value == 0) // First bookmark.
      {
        BaseDataObject[PdfName.Last]
          = BaseDataObject[PdfName.First]
          = bookmark.BaseObject;

        countObject.Value = ((int)countObject.Value)+1;
      }
      else // Non-first bookmark.
      {
        PdfReference oldLastBookmarkReference = (PdfReference)BaseDataObject[PdfName.Last];
        BaseDataObject[PdfName.Last] // Added bookmark is the last in the collection...
          = ((PdfDictionary)File.Resolve(oldLastBookmarkReference))[PdfName.Next] // ...and the next of the previously-last bookmark.
          = bookmark.BaseObject;
        bookmark.BaseDataObject[PdfName.Prev] = oldLastBookmarkReference;

        /*
          NOTE: The Count entry is a relative number (whose sign represents
          the node open state).
        */
        countObject.Value = (int)countObject.Value + Math.Sign((int)countObject.Value);
      }
    }

    public void Clear(
      )
    {throw new NotImplementedException();}

    public bool Contains(
      Bookmark bookmark
      )
    {throw new NotImplementedException();}

    public void CopyTo(
      Bookmark[] bookmarks,
      int index
      )
    {throw new NotImplementedException();}

    public int Count
    {
      get
      {
        /*
          NOTE: The Count entry may be absent [PDF:1.6:8.2.2].
        */
        PdfInteger countObject = (PdfInteger)BaseDataObject[PdfName.Count];
        if(countObject == null)
          return 0;

        return countObject.RawValue;
      }
    }

    public bool IsReadOnly
    {get{return false;}}

    public bool Remove(
      Bookmark bookmark
      )
    {throw new NotImplementedException();}

    #region IEnumerable<ContentStream>
    IEnumerator<Bookmark> IEnumerable<Bookmark>.GetEnumerator(
      )
    {
      PdfDirectObject item = BaseDataObject[PdfName.First];
      if(item == null)
        yield break;

      do
      {
        yield return new Bookmark(item);

        item = ((PdfDictionary)File.Resolve(item))[PdfName.Next];
      }while(item != null);
    }

    #region IEnumerable
    IEnumerator IEnumerable.GetEnumerator(
      )
    {return ((IEnumerable<Bookmark>)this).GetEnumerator();}
    #endregion
    #endregion
    #endregion
    #endregion
    #endregion

    #region private
    /**
      <summary>Gets the count object, forcing its creation if it doesn't
      exist.</summary>
    */
    private PdfInteger EnsureCountObject(
      )
    {
      /*
        NOTE: The Count entry may be absent [PDF:1.6:8.2.2].
      */
      PdfInteger countObject = (PdfInteger)BaseDataObject[PdfName.Count];
      if(countObject == null)
        BaseDataObject[PdfName.Count] = countObject = new PdfInteger(0);

      return countObject;
    }
    #endregion
    #endregion
    #endregion
  }
}