using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Arcadia.Bookasa.Data;

namespace Arcadia.Bookasa.Common.Format
{
    public class BookFormatBase : IBookFormat
    {
        protected virtual string ExtensionName { get; set; }

        public bool IsSupported(Book book)
        {
            return this.ExtensionName == GetExtension(book.FullPath);
        }
 
        #region helper method
        /// <summary>
        /// Return the suffix from the path.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        protected string GetExtension (string path)
        {
            return Path.GetExtension(path).ToUpper();
        }
        #endregion
    }
}
