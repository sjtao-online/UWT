using LinqToDB.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UWT.Templates.Models.Database
{
    class ConnectionStringSettings : IConnectionStringSettings
    {
        public string ConnectionString { get; set; }

        public string Name { get; set; }

        public string ProviderName { get; set; }

        public bool IsGlobal => false;
    }
    class DbCreator
    {
        public static Func<LinqToDB.Data.DataConnection> CreateDb { get; set; }
    }
    class LinqDbSettings : LinqToDB.Configuration.ILinqToDBSettings
    {
        public IEnumerable<IDataProviderSettings> DataProviders => Enumerable.Empty<IDataProviderSettings>();
        public string DefaultConfiguration { get; set; }
        public string DefaultDataProvider { get; set; }
        IConnectionStringSettings ConnectionStringSettings;
        public LinqDbSettings(string name, string connection)
        {
            DefaultConfiguration = name;
            DefaultDataProvider = name;
            ConnectionStringSettings = new ConnectionStringSettings()
            {
                Name = name,
                ProviderName = name,
                ConnectionString = connection
            };
        }

        public IEnumerable<IConnectionStringSettings> ConnectionStrings
        {
            get
            {
                yield return ConnectionStringSettings;
            }
        }
    }
}
