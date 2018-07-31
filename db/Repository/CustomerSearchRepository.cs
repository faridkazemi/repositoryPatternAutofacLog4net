using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Fiveways.Insight.Model;
using Fiveways.Insight.Model.Repository;
using Fiveways.Insight.Model.Repository.Interface;
using Fiveways.Logging;

public class CustomerSearchRepository: GenericRepository<Customer>, ICustomerSearchRepository
{
	public CustomerSearchRepository(DbContext context, ILogger logger) : base(context, logger)
    {
	}

    public async Task<List<Customer>> GetCustomerSearchAsync()
    {
        var result = (await GetAsync(c => c.Id > 0, "AccountManager, CustomerAddress")).ToList();
        return result;
    }
}
