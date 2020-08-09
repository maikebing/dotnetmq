using System;
using DotNetMQ.Storage.MemoryStorage;
using DotNetMQ.Storage.MsSqlStorage;
using DotNetMQ.Storage.MySqlStorage;
using DotNetMQ.Settings;
using DotNetMQ.Storage.SQLiteStorage;
using MDS.Storage.PgSqlStorage;
using DotNetMQ.Storage.MySqlConnectorStorage;

namespace DotNetMQ.Storage
{
    public static class StorageManagerFactory
    {
        /// <summary>
        /// Creates Storage Manager according to StorageType value in Settings file.
        /// </summary>
        /// <returns>Storame Manager</returns>
        public static IStorageManager CreateStorageManager()
        {
            //Get a reference to the settings
            var settings = MDSSettings.Instance;

            //Create storage manager according to the settings
            var storageType = settings["StorageType"];
            IStorageManager storageManager;
            if (storageType.Equals("MySQL-ODBC", StringComparison.OrdinalIgnoreCase))
            {
                storageManager = new MySqlOdbcStorageManager { ConnectionString = settings["ConnectionString"] };
            }
            else if (storageType.Equals("MySQL-Net", StringComparison.OrdinalIgnoreCase))
            {
                storageManager = new MySqlNetStorageManager { ConnectionString = settings["ConnectionString"] };
            }
            else if (storageType.Equals("MySqlConnector", StringComparison.OrdinalIgnoreCase))
            {
                storageManager = new   MySqlConnectorStorageManager()  { ConnectionString = settings["ConnectionString"] };
            }
            else if (storageType.Equals("MSSQL", StringComparison.OrdinalIgnoreCase))
            {
                storageManager = new MsSqlStorageManager { ConnectionString = settings["ConnectionString"] };
            }
            else if (storageType.Equals("SQLite", StringComparison.OrdinalIgnoreCase))
            {
                storageManager = new SqliteStorageManager();
            }
            else if (storageType.Equals("PgSQL", StringComparison.OrdinalIgnoreCase))
            {
                storageManager = new PgSqlStorageManager() { ConnectionString = settings["ConnectionString"] };
            }
            else //Default storage manager
            {
                storageManager = new MemoryStorageManager();
            }

            //Wrap storageManager with FaultToleratedStorageManagerWrapper and return it
            return new FaultToleratedStorageManagerWrapper(storageManager);
        }
    }
}
