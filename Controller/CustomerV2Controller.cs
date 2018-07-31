using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Fiveways.External.API.Helper.Security;
using Fiveways.Insight.Model;

namespace Fiveways.External.API.Controllers
{
    [AuthorizeApiKey]
    public class CustomerV2Controller : ApiController
    {
        Insight.Model.UnitOfWork.IUnitOfWork _unitOfWork;
        public CustomerV2Controller(Insight.Model.UnitOfWork.IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

       [HttpGet]
       [Route("api/v1/newCustomer/CustomerSearchList")]
        public async Task<Response> GetCustomerSearchList()
       {
          var result = await _unitOfWork.CustomerSearchRepository.GetCustomerSearchAsync();
          return Response.CreateSuccess(result);
       }
    }
}
