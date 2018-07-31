using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fiveways.Insight.Model.Entities;

namespace Fiveways.Insight.Model.Repository.Interface
{
   public interface ICustomerReportGroupRepository
   {
       Task<List<CustomerReportGroup>> GetCustomerReportGroupsAsync();
       Task<List<CustomerReportGroup>> GetReportGroupsByCustomerIdAsync(int id);
       Task<bool> SaveListAsync(List<CustomerReportGroup> customerReportGroups);
       Task<bool> SaveAsync(CustomerReportGroup customerReportGroups);
    }
}
