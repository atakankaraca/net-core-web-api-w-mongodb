using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using NLog;
using POC.WebAPI.Data;
using POC.WebAPI.Models;

namespace POC.WebAPI.Repository
{
    public class AccountRepository : IRepository<Account>
    {
        private readonly DbContext _context = null;
        private readonly ILogger<AccountRepository> _logger;

        public AccountRepository(IOptions<Settings> settings, ILogger<AccountRepository> logger)
        {
            _logger = logger;
            _context = new DbContext(settings);
        }

        public async Task Add(Account account)
        {
            try
            {
                await _context.Accounts.InsertOneAsync(account);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<Account>> Get()
        {
            try
            {
                return await _context.Accounts.Find(x => true).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public Task<Account> Get(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Get(Account entity)
        {
            try
            {
                var account = Builders<Account>.Filter.Eq("Username", entity.Username) & Builders<Account>.Filter.Eq("Password", entity.Password);
                var result = await _context.Accounts.Find(account).FirstOrDefaultAsync();

                return result != null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<DeleteResult> Remove(string id)
        {
            try
            {
                return await _context.Accounts.DeleteOneAsync(Builders<Account>.Filter.Eq("Id", id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public Task<Account> Update(string id, Account entity)
        {
            throw new NotImplementedException();
        }
    }
}
