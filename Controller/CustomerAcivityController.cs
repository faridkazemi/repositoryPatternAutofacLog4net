using Fiveways.External.API.Helper.Security;
using Fiveways.Insight.Model;
using Fiveways.Insight.Model.Entities;
using Fiveways.Insight.Model.Mapper.Interface;
using Fiveways.Insight.Model.UnitOfWork;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Collections;
using System.Linq;
using Fiveways.Insight.Model.DTO;

namespace Fiveways.External.API.Controllers
{
    [AuthorizeApiKey]
    public class CustomerAcivityController : ApiController
    {
        IUnitOfWork unitOfWork;
        ICustomerActivityHistoryMapper mapper;
        public CustomerAcivityController(IUnitOfWork unitOfWork, ICustomerActivityHistoryMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("api/v1/CustomerAcivity/GetByStatusAsync/{id}")]
        public async Task<Response> GetByStatusAsync(int id)
        {
           List<CustomerActivityHistory> activityHistoryList = await unitOfWork.CustomerActivityRepository.GetByStatusAsync(id);
            var dto = await mapper.ToDTOAsync(activityHistoryList);
           var response =  Response.CreateSuccess(dto);
            return response;
        }

    }
}
