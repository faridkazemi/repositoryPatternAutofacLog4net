using Fiveways.Insight.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiveways.Insight.Model.Repository.Interface
{
    public interface ICustomerActivityRepository
    {
        Task<List<CustomerActivityHistory>> GetByStatusAsync(int statusId);

        Task<List<CustomerActivityHistory>> GetActivityListAsync();
    }
}
