using Fiveways.Insight.Model.Mapper.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fiveways.Insight.Model.DTO;
using Fiveways.Insight.Model.Entities;
using Fiveways.Insight.Model.UnitOfWork;
using Fiveways.Logging;

namespace Fiveways.Insight.Model.Mapper
{
    public class CustomerActivityHistoryMapper : BaseMapper,ICustomerActivityHistoryMapper
    {
       
       public CustomerActivityHistoryMapper(IUnitOfWork unitOfWork, ILogger logger):base(unitOfWork, logger)
       {
       }

        public async Task<List<CustomerActivityHistoryDTO>> ToDTOAsync(List<CustomerActivityHistory> entity)
        {
            try
            {
                var result = await Task.Run(() => entity.Select(e => new CustomerActivityHistoryDTO()
                {
                    ActivityId = e.ActivityId,
                    CreationDate = e.CreationDate,
                    CreatorUserId = e.CreatorUserId,
                    Customer = e.Customer.ToDto(),
                    CustomerActivity = e.CustomerActivity,
                    CustomerActivityReason = e.CustomerActivityReason.ToDTO(),
                    CustomerActivityReasonId = e.CustomerActivityReasonId,
                    CustomerId = e.CustomerId,
                    Description = e.Description,
                    FollowupActivity = e.FollowupActivity,
                    FollowupActivityId = e.FollowupActivityId,
                    Id = e.Id,
                    Status = e.Status,
                    StatusId = e.StatusId,
                    User = e.User,
                    ActivityDate = e.ActivityDate,
                    PurchasingStatusWarning = false,//GetPurchasingStatusWarningAsync(e).Result
                }).ToList());

                return result;
            }
            catch (Exception e)
            {
                Logger.Error(e, "");
                throw e;
            }
        }

        private async Task<bool> GetPurchasingStatusWarningAsync(CustomerActivityHistory e)
        {
            var reslut = await UnitOfWork.CustomerPurchasingInfoRepository
                .GetPurchasingListAsync(e.Customer.CustomerIdInSupplierSystem);
            return reslut.Any(x => x.MarketSharePercentage >= 40);
        }

        public async Task<List<CustomerActivityHistory>> ToEntityAsync(List<CustomerActivityHistoryDTO> entity)
        {
            throw new NotImplementedException();
        }
    }
}
