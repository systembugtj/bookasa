using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace Arcadia.Bookasa.Persistence.Dao
{
    public abstract class BaseDao
    {
        private ISessionFactory sessionFactory;

        /// <summary>
        /// Session factory for sub-classes.
        /// </summary>
        public ISessionFactory SessionFactory
        {
            protected get { return sessionFactory; }
            set { sessionFactory = value; }
        }

        /// <summary>
        /// Get's the current active session. Will retrieve session as managed by the 
        /// Open Session In View module if enabled.
        /// </summary>
        protected ISession CurrentSession
        {
            get { return sessionFactory.GetCurrentSession(); }
        }

        protected void Update<T>(T obj)
        {
            CurrentSession.SaveOrUpdate(obj);
        }

        protected object Save<T>(T obj)
        {
            return CurrentSession.Save(obj);
        }

        protected void Delete<T>(T obj)
        {
            CurrentSession.Delete(obj);
        }

        protected IList<T> GetAll<T>() where T : class
        {
            ICriteria criteria = CurrentSession.CreateCriteria<T>();
            return criteria.List<T>();
        }
    }
}
