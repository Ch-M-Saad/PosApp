using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.Sqlite;


namespace PosApp.Data
{
    public class DatabaseService
    {
        private readonly string _dbPath;
        public string DbPath => _dbPath;
        public DatabaseService() 
        {
            // step 2 goes here — build _dbPath using Path.Combine + FileSystem.AppDataDirectory
            _dbPath = Path.Combine(FileSystem.AppDataDirectory, "pos.db3");
        }

        public SqliteConnection GetConnection()
        {
            // step 3 — return a new SqliteConnection using _dbPath
            return new SqliteConnection($"Data Source={_dbPath}");
        }

        public async Task InitializeAsync()
        {
            // step 4 — open a connection, run CREATE TABLE IF NOT EXISTS Products
            await using var conn = GetConnection();
            await conn.OpenAsync();

            var cmd = conn.CreateCommand();
            cmd.CommandText = @"CREATE TABLE IF NOT EXISTS Products(
                ID INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT NOT NULL,
                Category TEXT,
                Price REAL NOT NULL,
                Quantity INTEGER NOT NULL,
                Unit TEXT
                )";
            await cmd.ExecuteNonQueryAsync();
         
            cmd.CommandText = @"CREATE TABLE IF NOT EXISTS Sales(
                ID INTEGER PRIMARY KEY AUTOINCREMENT,
                Date TEXT NOT NULL,
                Total_Amount REAL NOT NULL        
                )";
            await cmd.ExecuteNonQueryAsync();

            cmd.CommandText = @"CREATE TABLE IF NOT EXISTS SaleItems(
                ID INTEGER PRIMARY KEY AUTOINCREMENT,
                Sale_ID INTEGER NOT NULL,
                Product_ID INTEGER NOT NULL,
                Quantity INTEGER NOT NULL, 
                Price_At_Sale REAL NOT NULL,
                FOREIGN KEY (Sale_ID) REFERENCES Sales(ID),
                FOREIGN KEY (Product_ID) REFERENCES Products(ID)
                )";
            await cmd.ExecuteNonQueryAsync();

        }

    }
}
