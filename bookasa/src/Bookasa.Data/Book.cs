using System;
using System.IO;

namespace Arcadia.Bookasa.Data
{
    public class Book
    {
        string name;
        DateTime dateTime;
        string size;
        string path;

        public Book(string filename)
        {
            FileInfo info = new FileInfo(filename);
            size = (info.Length / 1024).ToString("N0") + " KB";
            dateTime = info.LastWriteTime;
            name = info.Name;
            path = info.DirectoryName;
        }

        public string Name
        {
            get { return name; }
        }

        public DateTime DateTime
        {
            get { return dateTime; }
        }

        public string Size
        {
            get { return size; }
        }

        public string Path
        {
            get { return path; }
        }

        public string FullPath
        {
            get { return System.IO.Path.Combine(Path, Name); }
        }

        public override string ToString()
        {
            return FullPath;
        }
    }
}
