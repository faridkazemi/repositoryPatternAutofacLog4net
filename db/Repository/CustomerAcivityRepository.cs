using Fiveways.Insight.Model.Entities;
using Fiveways.Insight.Model.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fiveways.Logging;

namespace Fiveways.Insight.Model.Repository
{
    public class CustomerActivityRepository : GenericRepository<CustomerActivityHistory>,
                                            ICustomerActivityRepository
    {
        public CustomerActivityRepository(DbContext db, ILogger logger) : base(db, logger)
        {
        }
        public async Task<List<CustomerActivityHistory>> GetActivityListAsync()
        {
             var result = await GetAsync(e => e.Id > 0);
            return result.ToList();
        }

        public async Task<List<CustomerActivityHistory>> GetByStatusAsync(int statusId)
        {

            var result = await GetAsync(e => e.StatusId == statusId,
                "Customer,Customer.AccountManager,Customer.AccountsReceivable, CustomerActivity,CustomerActivityReason,Status,FollowupActivity,User");
            return result.ToList();
        }
    }
}
