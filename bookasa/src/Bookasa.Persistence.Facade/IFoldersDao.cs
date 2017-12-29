using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Arcadia.Bookasa.Persistence.Entity;

namespace Arcadia.Bookasa.Persistence.Facade
{
    public interface IFoldersDao
    {
        IList<Folders> GetAllFolders();
    }
}
