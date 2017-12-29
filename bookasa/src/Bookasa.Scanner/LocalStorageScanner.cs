using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using Arcadia.Bookasa.Data.Watching;
using Arcadia.Bookasa.Common.Scan;
using Arcadia.Bookasa.Common.Exception;

namespace Arcadia.Bookasa.Scanner
{
    public class LocalStorageScanner : IBookScanner
    {

        protected WatchSource Target { set; get; }


        protected event FindBookEventHandler findBookEvents;
        public bool IsAcceptedSource(WatchSource target)
        {
            return true;
        }


        public void Scanning(WatchSource target, FindBookEventHandler handler)
        {
            this.Target = target;

            findBookEvents += handler;

            FindFile(Target.SourceUri.OriginalString);

            findBookEvents -= handler;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dir">参数为指定的目录</param>
        protected void FindFile(string dir)                           
        {     
            //在指定目录及子目录下查找文件,在listBox1中列出子目录及文件 
            DirectoryInfo   Dir=new   DirectoryInfo(dir); 
            try 
            { 
                foreach(DirectoryInfo   d   in   Dir.GetDirectories())     //查找子目录   
                { 
                    FindFile(Dir + d.ToString() + "\\ "); 
                } 
                foreach(FileInfo   f   in   Dir.GetFiles( "*.* "))             //查找文件 
                {

                    findBookEvents(this, f);
                } 
            } 
            catch(Exception   e) 
            {
                throw new FindingBookException (e.Message);
            }
        }
        protected void FireEvent(object sender, object args)
        {
            if (findBookEvents != null) findBookEvents(sender, args);
        }
    }
}
