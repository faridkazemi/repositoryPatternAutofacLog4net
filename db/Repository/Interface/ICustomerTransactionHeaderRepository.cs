using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiveways.Insight.Model.Repository.Interface
{
    public interface ICustomerTransactionHeaderRepository
    {
        Task<List<CombinedCustomerTransactionHeader>> GetCustomerTransactionHeaderListAsync();
    }
}
