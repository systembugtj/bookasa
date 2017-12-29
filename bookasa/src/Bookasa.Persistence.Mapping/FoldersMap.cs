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
            Map(x => x.Name).Column("Name").Length(128);
            Map(x => x.Path).Column("Path").Length(256);
        }
    }

}
