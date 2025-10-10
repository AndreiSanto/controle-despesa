using Dapper;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace controleDespesa.Infrastructure.Migration
{
    public static class DatabaseMigration
    {
        public static void Migrate(string connecConnectionStrings)
        {

            Database(connecConnectionStrings);
        }
        private static void Database(string connecConnectionStrings) {
             var conneConnectionStringBuilder = new MySqlConnectionStringBuilder(connecConnectionStrings);

            var databaseNome = conneConnectionStringBuilder.Database;
            conneConnectionStringBuilder.Remove("Database");

            using var dbConexao = new MySqlConnection(conneConnectionStringBuilder.ConnectionString);

            var parametro = new DynamicParameters();
            parametro.Add("nome", databaseNome);
           var resultado =  dbConexao.Query("SELECT * FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = @nome", parametro);

            if (!resultado.Any())
            {
                dbConexao.Execute($"CREATE DATABASE {databaseNome}");
            }

        } 
    }
}
