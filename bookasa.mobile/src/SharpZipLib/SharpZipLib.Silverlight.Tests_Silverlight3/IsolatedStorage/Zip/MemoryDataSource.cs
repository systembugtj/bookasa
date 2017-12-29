using System.IO;
using ICSharpCode.SharpZipLib.Zip;

namespace ICSharpCode.SharpZipLib.Tests.FileSystem.Zip
{
    class MemoryDataSource : IStaticDataSource
    {
        #region Constructors
        /// <summary>
        /// Initialise a new instance.
        /// </summary>
        /// <param name="data">The data to provide.</param>
        public MemoryDataSource(byte[] data)
        {
            data_ = data;
        }
        #endregion

        #region IDataSource Members

        /// <summary>
        /// Get a Stream for this <see cref="IStaticDataSource"/>
        /// </summary>
        /// <returns>Returns a <see cref="Stream"/></returns>
        public Stream GetSource()
        {
            return new MemoryStream(data_);
        }
        #endregion

        #region Instance Fields
        readonly byte[] data_;
        #endregion
    }
}