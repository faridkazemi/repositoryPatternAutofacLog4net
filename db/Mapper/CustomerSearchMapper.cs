using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fiveways.Insight.Model.DTO;
using Fiveways.Insight.Model.Mapper.Interface;
using Fiveways.Insight.Model.UnitOfWork;
using Fiveways.Logging;

namespace Fiveways.Insight.Model.Mapper
{
    public class CustomerSearchMapper : BaseMapper, ICustomerSearchMapper
    {
        public CustomerSearchMapper(IUnitOfWork unitOfWork, ILogger logger):base(unitOfWork, logger)
        {
        }
        public async Task<IEnumerable<CustomerSearchDTO>> ToDTOAsync(List<Customer> entity)
        {
            try
            {
                var result = await Task.Run(() => entity.Select(e => new CustomerSearchDTO()
                {
                    Id = e.Id,
                    AccountManagerName = e.AccountManager.Name,
                    AccountOpened = e.AccountOpened,
                    //CustomerAddressDto = e.CustomerAddress.Where(a => a.AddressType == AddressType.Delivery).Select(ad => ad.ToDto()).ToList(),
                    CustomerIdInSupplierSystem = e.CustomerIdInSupplierSystem,
                    CustomerIndustryName = e.CustomerIndustry.Name,
                    CustomerTradingName = e.CustomerTradingName,
                    CustomerName = e.CustomerTradingName,
                    CustomerTypeName = e.CustomerType.Name,
                    LastSale = GetLastSale(e.Id).Result,
                    State = e.CustomerAddress.Where(a => a.AddressType == AddressType.Delivery).Select(ad => ad.ToDto()).FirstOrDefault()?.State,
                    Suburb = e.CustomerAddress.Where(a => a.AddressType == AddressType.Delivery).Select(ad => ad.ToDto()).FirstOrDefault()?.Suburb,
                    IndustryCode = e.CustomerIndustry.ProntoCode,
                    OperationGroups = e.OperationsGroups.Select(og => new CustomerOperationsGroupDTO()
                    {
                        AwesomeIcon = og.AwesomeIcon,
                        Color = og.Color,
                        OperationGroupName = og.GroupName,
                        ParentId = og.ParentId,
                        ShowInUI = og.ShowInUI,
                        UiSortNumber = og.UiSortNumber
                    }).ToList()
                }).ToList());

                return result;
            }
            catch (Exception e)
            {
                Logger.Error(e,"");
                throw e;
            }
            
        }

        public Task<List<Customer>> ToEntityAsync(IEnumerable<CustomerSearchDTO> entity)
        {
            throw new NotImplementedException();
        }

        private async Task<DateTime?> GetLastSale(int customerId)
        {
            try
            {
                var transactionHeaders = await ((UnitOfWork.CustomerTransactionHeaderRepository).GetCustomerTransactionHeaderListAsync());
                DateTime? result = transactionHeaders
                    .Where(x => (customerId == 0 || x.Id == customerId))
                    .GroupBy(x => x.StoreId)
                    .Select(c => new
                    {
                        InvoiceDate = c.Max(a => a.InvoiceDate),
                        Id = c.Select(b => b.StoreId).FirstOrDefault()
                    }).Where(g => g.Id == customerId).Select(r => r.InvoiceDate).FirstOrDefault();
                return result;
            }
            catch (Exception e)
            {
                Logger.Error(e,"");
                throw;
            }
            
        }
    }
}
