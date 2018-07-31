using Fiveways.Insight.Model.Context;
using Fiveways.Insight.Model.Repository;
using Fiveways.Insight.Model.Repository.Interface;
using System;
using System.Threading.Tasks;

namespace Fiveways.Insight.Model.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDispsable
    {
        InsightContext context;
        ICustomerActivityRepository _customerAcivityRepository;
        ICustomerPurchasingInfoRepository _customerPurchasingInfoRepository;
        IReportGroupRepository _reportGroupRepository;
        ICustomerReportGroupRepository _customerReportGroupRepository;
        IItOptionRepository _itOptionRepository;

        private bool disposed = false;

       public UnitOfWork()
        {
            context = new InsightContext();
        }

        public ICustomerActivityRepository CustomerActivityRepository
        {
            get
            {
                if (_customerAcivityRepository == null)
                    _customerAcivityRepository = new CustomerActivityRepository(this.context);
                return _customerAcivityRepository;
            }
        }
        public ICustomerPurchasingInfoRepository CustomerPurchasingInfoRepository
        {
            get
            {
                if (_customerPurchasingInfoRepository == null)
                    _customerPurchasingInfoRepository = new CustomerPurchasingInfoRepository(this.context);
                return _customerPurchasingInfoRepository;
            }
        }
        public IReportGroupRepository ReportGroupRepository
        {
            get
            {
                if (_reportGroupRepository == null)
                    _reportGroupRepository = new ReportGroupRepository(this.context);
                return _reportGroupRepository;
            }
        }

        public ICustomerReportGroupRepository CustomerReportGroupRepository
        {
            get
            {
                if (_customerReportGroupRepository == null)
                    _customerReportGroupRepository = new CustomerReportGroupRepository(this.context);
                return _customerReportGroupRepository;
            }
        }

        public IItOptionRepository ItOptionRepository
        {
            get
            {
                if (_itOptionRepository == null)
                    _itOptionRepository = new ItOptionRepository(this.context);
                return _itOptionRepository;
            }
        }

        public async Task SaveAsync()
        {
            try
            {
                await context.SaveChangesAsync();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
