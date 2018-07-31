using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fiveways.Insight.Model.Repository.Interface;
using Fiveways.Logging;

namespace Fiveways.Insight.Model.Repository
{
    public class CustomerTransactionHeaderRepository : GenericRepository<CombinedCustomerTransactionHeader>, ICustomerTransactionHeaderRepository
    {
        public CustomerTransactionHeaderRepository(DbContext db, ILogger logger) : base(db, logger)
        {
            
        }
        public async Task<List<CombinedCustomerTransactionHeader>> GetCustomerTransactionHeaderListAsync()
        {
            var result = (await GetAsync(a => a.Id > 0, "")).ToList();
            return result;
        }
    }
}
