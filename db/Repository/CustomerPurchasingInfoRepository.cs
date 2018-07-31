using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fiveways.Insight.Model.DTO;
using Fiveways.Insight.Model.Repository.Interface;
using Fiveways.Logging;

namespace Fiveways.Insight.Model.Repository
{
    public class CustomerPurchasingInfoRepository : GenericRepository<CustomerPurchaseInfoDto>,
        ICustomerPurchasingInfoRepository
    {
        public CustomerPurchasingInfoRepository(DbContext db, ILogger logger) : base(db, logger)
        {

        }

        public async Task<List<CustomerPurchaseInfoDto>> GetPurchasingListAsync(string customerCode)
        {
            var result = await ExecuteQryAsync("exec GetCustomerPurchaseHistory @p0", new[] {customerCode});
            return result.ToList();
        }
    }
}
