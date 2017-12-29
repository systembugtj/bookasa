//using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;

namespace ICSharpCode.SharpZipLib.Tests.FileSystem.Zip
{
    class StringMemoryDataSource : MemoryDataSource
    {
        public StringMemoryDataSource(string data)
            : base(Encoding.UTF8.GetBytes(data))
        {
        }
    }
}
