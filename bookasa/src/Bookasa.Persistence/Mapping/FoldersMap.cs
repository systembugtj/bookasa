using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Arcadia.Bookasa.Persistence.Entity;

namespace Arcadia.Bookasa.Persistence.Mapping
{
    public class FoldersMap : ClassMap<Folders>
    {

        public FoldersMap()
        {
            Table("Folders");
            LazyLoad();
            Id(x => x.Id).GeneratedBy.Identity().Column("Id");
            Map(x => x.UserName).Column("UserName").Length(256);
            Map(x => x.PasswordMD5).Column("PasswordMD5").Length(128);
            Map(x => x.Role).Column("Role").Length(50);
        }
    }

}
