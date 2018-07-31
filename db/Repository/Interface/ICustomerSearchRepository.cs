using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fiveways.Insight.Model;

public interface ICustomerSearchRepository
{       
	    Task<List<Customer>> GetCustomerSearchAsync();
}
