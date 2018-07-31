using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fiveways.Insight.Model.DTO;
using Fiveways.Insight.Model.Entities;
using Fiveways.Insight.Model.UnitOfWork;
using Fiveways.Insight.Model.Mapper.Interface;
using Fiveways.Logging;

namespace Fiveways.Insight.Model.Mapper
{
    public class CustomerReportGroupMapper : BaseMapper, ICustomerReportGroupMapper
    {
        public CustomerReportGroupMapper(IUnitOfWork unitOfWork, ILogger logger) : base(unitOfWork, logger)
        {
        }

        public Task<List<CustomerReportGroupDTO>> ToDTOAsync(List<CustomerReportGroup> entity)
        {
            try
            {
                var result = Task.Run(() => entity.Select(x => new CustomerReportGroupDTO()
                    {
                        Id = x.Id,
                        Customer = x.Customer,
                        CustomerId = x.CustomerId,
                        ReportGroup = x.ReportGroup,
                        ReportGroupId = x.ReportGroupId
                    }
                ).ToList());
                return result;
            }
            catch (Exception e)
            {
                Logger.Error(e,"");
                throw;
            }
        }

        public async Task<List<CustomerReportGroup>> ToEntityAsync(List<CustomerReportGroupDTO> entity)
        {
            try
            {
                var result = await Task.Run(() => entity.Select(e => new CustomerReportGroup()
                    {
                        Id = e.Id,
                        Customer = e.Customer,
                        ReportGroup = e.ReportGroup,
                        CustomerId = e.CustomerId,
                        ReportGroupId = e.ReportGroupId
                    }
                ).ToList());

                return result;
            }
            catch (Exception e)
            {
                Logger.Error(e, "");
                throw;
            }
        }
    }
}
