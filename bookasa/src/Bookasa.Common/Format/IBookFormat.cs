using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Arcadia.Bookasa.Data;

namespace Arcadia.Bookasa.Common.Format
{
    public interface IBookFormat
    {
        /// <summary>
        /// Whether the format is supported.
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        bool IsSupported(Book book);
    }
}
