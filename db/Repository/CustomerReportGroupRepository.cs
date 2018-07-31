using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fiveways.Insight.Model.Entities;
using Fiveways.Insight.Model.Repository.Interface;
using Fiveways.Logging;

namespace Fiveways.Insight.Model.Repository
{
    class CustomerReportGroupRepository : GenericRepository<CustomerReportGroup>, ICustomerReportGroupRepository
    {
        public CustomerReportGroupRepository(DbContext context, ILogger logger) : base(context, logger)
        {
        }

        public async Task<List<CustomerReportGroup>> GetCustomerReportGroupsAsync()
        {
           var result = await GetAsync(x => x.Id != 0,"ReportGroup");
            return result.ToList();
        }

        public async Task<List<CustomerReportGroup>> GetReportGroupsByCustomerIdAsync(int id)
        {
            var result = await GetAsync(x => x.CustomerId == id, "ReportGroup");
            return result.ToList();
        }

        public Task<bool> SaveAsync(CustomerReportGroup customerReportGroups)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SaveListAsync(List<CustomerReportGroup> customerReportGroups)
        {
           var result = await Task.Run(()=> SaveList(customerReportGroups));
            return result;
        }

        bool SaveList(List<CustomerReportGroup> customerReportGroups)
        {
            try
            {
                List<CustomerReportGroup> customerReportGroupList = dbSet.ToList();
                var currentCustomerGroups = customerReportGroupList.Where(x => x.CustomerId == customerReportGroups.FirstOrDefault().CustomerId).ToList();
                dbSet.RemoveRange(currentCustomerGroups);
                dbSet.AddRange(customerReportGroups);
                return true;
            }
            catch (Exception e)
            {
                logger.Error(e,"");
                throw;
            }
            
        }
    }
}
