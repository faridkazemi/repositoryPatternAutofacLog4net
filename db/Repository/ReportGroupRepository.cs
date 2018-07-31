using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fiveways.Insight.Model.DTO;
using Fiveways.Insight.Model.Entities;
using Fiveways.Insight.Model.Repository.Interface;
using Fiveways.Logging;

namespace Fiveways.Insight.Model.Repository
{
   public class ReportGroupRepository:GenericRepository<ReportGroup>, IReportGroupRepository
    {
        public ReportGroupRepository(DbContext context, ILogger logger) : base(context, logger)
        {
        }

        public async Task<List<ReportGroup>> GetGroupsAsync()
        {
            var result = await  GetAsync(e => e.Id != 0);
            return result.ToList();
        }
    }
}
