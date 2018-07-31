using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Fiveways.Insight.Model.DTO;
using Fiveways.Insight.Model.Entities;
using Fiveways.Insight.Model.Mapper.Interface;
using Fiveways.Insight.Model.UnitOfWork;
using Fiveways.Logging;

namespace Fiveways.Insight.Model.Mapper
{
    public class ReportGroupMapper : BaseMapper, IReportGroupMapper
    {
        public ReportGroupMapper(IUnitOfWork uinOfWork, ILogger logger) : base(uinOfWork, logger)
        {
            
        }

        public async Task<IEnumerable<ReportGroupDTO>> ToDTOAsync(List<ReportGroup> entity)
        {
            try
            {
                var result = await BuildTreeAndGetRoots(entity);
                return result;
            }
            catch (Exception e)
            {
                Logger.Error(e,"");
                throw;
            }
            
        }

        public Task<List<ReportGroup>> ToEntityAsync(IEnumerable<ReportGroupDTO> entity)
        {
            throw new NotImplementedException();
        }

        /// /////////////////////////////

        async Task<IEnumerable<ReportGroupDTO>> BuildTreeAndGetRoots(List<ReportGroup> actualObjects)
        {
            Dictionary<int, ReportGroupDTO> lookup = new Dictionary<int, ReportGroupDTO>();
            actualObjects.ForEach(x => lookup.Add(x.Id, new ReportGroupDTO { AssociatedObject = x }));
            try
            {
                ReportGroupDTO proposedParent = new ReportGroupDTO();
                foreach (var item in lookup.Values)
                {
                    if (lookup.TryGetValue(item.AssociatedObject.ParentId, out proposedParent))
                    {
                        item.Parent = proposedParent;
                        proposedParent?.Childs?.Add(item);
                    }
                }
                var result = await Task.Run(() => lookup.Values.Where(x => x.AssociatedObject.ParentId == -1));
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
