using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arcadia.Bookasa.Persistence.Entity
{
    public class Folders
    {
        public Folders() { }
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Path { get; set; }
    }

}
