using PosApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PosApp.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly DatabaseService _databaseService;
        public ProductRepository(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<int> AddAsync(Product p)
        {
            await using var conn = _databaseService.GetConnection();
            await conn.OpenAsync();

            var cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO Products (name, category, price, quantity, unit) VALUES (@name, @category, @price, @quantity, @unit)";

            cmd.Parameters.AddWithValue("@name", p.Name);
            cmd.Parameters.AddWithValue("@category", p.Category);
            cmd.Parameters.AddWithValue("@price", p.Price);
            cmd.Parameters.AddWithValue("@quantity", p.Quantity);
            cmd.Parameters.AddWithValue("@unit", p.Unit);

            await cmd.ExecuteNonQueryAsync();

            // this method promises Task<int> — what should it return? (hint: an id would be useful, but that's actually a later task. For now, what's the simplest valid int to return so it compiles and makes sense?)
            return 0;
        }
        public async Task<List<Product>> GetAllAsync()
        {
            var products = new List<Product>();

            await using var conn = _databaseService.GetConnection();
            await conn.OpenAsync();

            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT ID, Name, Category, Price, Quantity, Unit FROM Products ORDER BY name";

            await using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync()) {

                products.Add(new Product
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Category = reader.GetString(2),
                    Price = reader.GetDecimal(3),
                    Quantity = reader.GetInt32(4),
                    Unit = reader.GetString(5)

                });
            }

            return products;
        }
        public async Task<Product?> GetByIdAsync(int id)
        {

            await using var conn = _databaseService.GetConnection();
            await conn.OpenAsync();

            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT ID, Name, Category, Price, Quantity, Unit FROM Products WHERE ID = @ID";
            cmd.Parameters.AddWithValue("@ID", id);
            await using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Product
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Category = reader.GetString(2),
                    Price = reader.GetDecimal(3),
                    Quantity = reader.GetInt32(4),
                    Unit = reader.GetString(5)

                };
            }

            return null;
        }
        public Task UpdateAsync(Product p) => throw new NotImplementedException();
        public Task DeleteAsync(int id) => throw new NotImplementedException();
    }
}
