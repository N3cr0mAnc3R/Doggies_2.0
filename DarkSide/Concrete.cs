﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkSide
{
    public class Concrete : IDisposable
    {


        public void UseConnection(Action<DbConnection> action)
        {
            using (DbConnection cnt = OpenConnection())
            {
                action(cnt);
            }
        }

        public async Task UseConnectionAsync(Action<DbConnection> action)
        {
            using (DbConnection cnt = await OpenConnectionAsync())
            {
                action(cnt);
            }
        }

        public DbConnection OpenConnection()
        {
            DbConnection cnt = Factory.CreateConnection();
            cnt.ConnectionString = ConnectionString;
            cnt.Open();

            ConnectionHistory.Add(cnt);

            return cnt;
        }

        public async Task<DbConnection> OpenConnectionAsync()
        {
            DbConnection cnt = Factory.CreateConnection();
            cnt.ConnectionString = ConnectionString;
            await cnt.OpenAsync();

            ConnectionHistory.Add(cnt);

            return cnt;
        }
        protected List<DbConnection> ConnectionHistory = new List<DbConnection>();

        protected string ConnectionString { get; set; }
        protected DbProviderFactory Factory { get; set; }


        public Concrete(ConnectionStringSettings settings) : this(settings.ConnectionString, settings.ProviderName) { }
        public Concrete(string connectionString, string providerName)
    : this(connectionString, DbProviderFactories.GetFactory(providerName)) { }
        public Concrete(string connectionString, DbProviderFactory factory)
        {
            ConnectionString = connectionString;
            Factory = factory;
        }
        public void Dispose()
        {
            ConnectionHistory.ForEach(cnt =>
            {
                if (cnt.State != System.Data.ConnectionState.Closed) cnt.Close();
            });
        }
    }
}
