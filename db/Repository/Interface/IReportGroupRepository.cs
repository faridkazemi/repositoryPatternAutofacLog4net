using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fiveways.Insight.Model.DTO;
using Fiveways.Insight.Model.Entities;

namespace Fiveways.Insight.Model.Repository.Interface
{
    public interface IReportGroupRepository
    {
       Task<List<ReportGroup>> GetGroupsAsync();
    }
}
