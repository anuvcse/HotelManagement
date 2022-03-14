using HotelManagementCommon.Entities;
using HotelManagementRepository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementRepository.Implementation
{
    public class CustomerDetailsRepository : ICustomerDetailsRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public CustomerDetailsRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<int> AddCustomerDetails(CustomerDetailsEntity customerDetailsEntity)
        {
             _applicationDbContext.CustomerDetails.Add(customerDetailsEntity);
             await _applicationDbContext.SaveChangesAsync();
            return customerDetailsEntity.CustomerID;
        }

        public async Task<int> IsCustomerExsist(string Email)
        {
          return await _applicationDbContext.CustomerDetails.Where(x => x.Email == Email).Select(x => x.CustomerID).FirstOrDefaultAsync().ConfigureAwait(false);
        }
    }
}
