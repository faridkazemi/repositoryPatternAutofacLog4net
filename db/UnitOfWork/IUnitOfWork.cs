using Fiveways.Insight.Model.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiveways.Insight.Model.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task SaveAsync();
        ICustomerActivityRepository CustomerActivityRepository { get ;}
        ICustomerPurchasingInfoRepository CustomerPurchasingInfoRepository { get ;}
        IReportGroupRepository ReportGroupRepository { get; }
        ICustomerReportGroupRepository CustomerReportGroupRepository { get; }
        IItOptionRepository ItOptionRepository { get; }
    }
}
