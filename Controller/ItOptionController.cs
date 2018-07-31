using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Fiveways.Insight.Model;
using Fiveways.Insight.Model.DTO;

namespace Fiveways.External.API.Controllers
{
    public class ItOptionController : ApiController
    {
        Insight.Model.UnitOfWork.IUnitOfWork _unitOfWork;
        public ItOptionController(Insight.Model.UnitOfWork.IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]   
        public async Task<Response> Get()
        {
           List<ITOOptionDTO> result = await _unitOfWork.ItOptionRepository.GetLatestItOptionsAsync();
           var response = await Task.Run(() => Response.CreateSuccess(result));
            return response;
        }
    }
}
