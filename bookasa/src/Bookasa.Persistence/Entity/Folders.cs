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
        public virtual string UserName { get; set; }
        public virtual string PasswordMD5 { get; set; }
        public virtual string Role { get; set; }
    }

}
