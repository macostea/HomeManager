using System;
using System.Data;
using Npgsql;
using Dapper;

namespace Common.Repository
{
    public class DBController
    {
        public NpgsqlConnection Connection { get; }
        public string ConnectionString { get; }

        public DBController(string connectionString)
        {
            this.ConnectionString = connectionString;
            this.Connection = new NpgsqlConnection(connectionString);
        }
    }
}
