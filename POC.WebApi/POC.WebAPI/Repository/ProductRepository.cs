using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using POC.WebAPI.Controllers;
using POC.WebAPI.Data;
using POC.WebAPI.Models;

namespace POC.WebAPI.Repository
{
    public class ProductRepository : IRepository<Product>
    {
        private readonly DbContext _context = null;
        private readonly ILogger<ProductRepository> _logger;

        public ProductRepository(IOptions<Settings> settings, ILogger<ProductRepository> logger)
        {
            _logger = logger;
            _context = new DbContext(settings);
        }

        public async Task Add(Product product)
        {
            try
            {
                await _context.Products.InsertOneAsync(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        public async Task<IEnumerable<Product>> Get()
        {
            try
            {
                return await _context.Products.Find(x => true).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<Product> Get(string id)
        {
            try
            {
                var product = Builders<Product>.Filter.Eq("Id", id);
                return await _context.Products.Find(product).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public Task<bool> Get(Product entity)
        {
            throw new NotImplementedException();
        }

        public async Task<DeleteResult> Remove(string id)
        {
            try
            {
                return await _context.Products.DeleteOneAsync(Builders<Product>.Filter.Eq("Id", id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<Product> Update(string id, Product product)
        {
            try
            {
                var filter = Builders<Product>.Filter.Eq("Id",id);
                var update = Builders<Product>.Update
                    .Set("Title", product.Title)
                    .Set("Price", product.Price)
                    .Set("ModifiedDate", DateTime.Now);
                    
               return await _context.Products.FindOneAndUpdateAsync(filter, update);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
