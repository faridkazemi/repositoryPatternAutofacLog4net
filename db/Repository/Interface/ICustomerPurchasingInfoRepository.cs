using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fiveways.Insight.Model.DTO;

namespace Fiveways.Insight.Model.Repository.Interface
{
    public interface ICustomerPurchasingInfoRepository
    {
        Task<List<CustomerPurchaseInfoDto>> GetPurchasingListAsync(string customerCode);
    }
}
