using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Arcadia.Bookasa.Persistence.Facade;
using Arcadia.Bookasa.Persistence.Entity;

namespace Arcadia.Bookasa.Persistence.Dao
{
    public class FoldersDaoImpl : BaseDao, IFoldersDao
    {
        public IList<Folders> GetAllFolders()
        {
            return GetAll<Folders>();
        }
    }
}
