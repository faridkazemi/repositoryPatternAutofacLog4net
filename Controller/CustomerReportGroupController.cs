using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Fiveways.External.API.Helper.Security;
using Fiveways.Insight.Model;
using Fiveways.Insight.Model.DTO;
using Fiveways.Insight.Model.Mapper.Interface;
using Fiveways.Insight.Model.Request;

namespace Fiveways.External.API.Controllers
{
    //[AuthorizeApiKey]
    public class CustomerReportGroupController : ApiController
    {
        ICustomerReportGroupMapper _mapper;
        Insight.Model.UnitOfWork.IUnitOfWork _unitOfWork;
        public CustomerReportGroupController(ICustomerReportGroupMapper mapper, Insight.Model.UnitOfWork.IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<Response> GetByCustomerId(int id)
        {
            var entityResult = await _unitOfWork.CustomerReportGroupRepository.GetReportGroupsByCustomerIdAsync(id);
            var dtoResult =  await _mapper.ToDTOAsync(entityResult);
            var response = Response.CreateSuccess(dtoResult);
            return response;
        }

        [HttpPost]
        public async Task<Response> Save(GenericBaseRequest<List<CustomerReportGroupDTO>> reportGroup)
        {
           var entity = await _mapper.ToEntityAsync(reportGroup.Entity);
            await _unitOfWork.CustomerReportGroupRepository.SaveListAsync(entity);
            await _unitOfWork.SaveAsync();
            var response = await Task.Run(()=> Response.CreateSuccess(true)) ;
            return response;
        }
    }
}
