using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fiveways.Insight.Model.DTO;
using Fiveways.Insight.Model.Repository.Interface;
using Fiveways.Logging;

namespace Fiveways.Insight.Model.Repository.Interface
{
    public class ItOptionRepository : GenericRepository<ITOOptionDTO>,IItOptionRepository
    {
        public ItOptionRepository(DbContext context, ILogger logger) : base(context, logger)
        {
        }

        public async Task<List<ITOOptionDTO>> GetLatestItOptionsAsync()
        {
           var result = await ExecuteQryAsync("exec GetLatestITOPoints");
            return result.ToList();
        }
    }
}
