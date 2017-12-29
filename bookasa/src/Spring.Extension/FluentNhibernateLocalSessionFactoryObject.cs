using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using FluentNHibernate;
using Spring.Data.NHibernate;
using NHibernate.Cfg;

namespace Spring.Extension
{
    public class FluentNhibernateLocalSessionFactoryObject : LocalSessionFactoryObject
    {
        /// <summary>     
        /// Sets the assemblies to load that contain fluent NHibernate mappings.     
        /// </summary>     
        /// <value>The mapping assemblies.</value>     
        public string[] FluentNhibernateMappingAssemblies { get; set; }
        /// <summary>     
        /// This method will be called after the configuration is processed      
        /// but before the session factory is created.       
        /// Adding the assembly mappings for the      
        /// Fluent NHibernate mapping assemblies to the config object.     
        /// This is done so that later when the session factory is created     
        /// Using the updated configuration it will have      
        /// Fluent NHibernate mappings registered in it.     
        /// </summary>     
        /// <param name="config">     
        /// The configuration object that holds the NHibernate configuration.     
        /// </param>     
        protected override void PostProcessConfiguration(Configuration config)
        {
            base.PostProcessConfiguration(config);
            if (FluentNhibernateMappingAssemblies == null)
                return;
            foreach (string assemblyName in FluentNhibernateMappingAssemblies)
            {
                // Loading the assembly by name and              
                // then adding it as the Mapping assembly.             
                config.AddMappingsFromAssembly(Assembly.Load(assemblyName));
            }
        }
    }
}
