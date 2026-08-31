using System;
using System.Collections.Generic;
using System.Text;
using PosApp.Models;

namespace PosApp.Data
{
    public interface IProductRepository
    {
        Task<int> AddAsync (Product p);
        Task<List<Product>> GetAllAsync ();
        Task<Product?> GetByIdAsync(int id);
        Task UpdateAsync (Product p);
        Task DeleteAsync (int id);

    }
}
