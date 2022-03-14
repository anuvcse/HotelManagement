using HotelManagementCommon.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementRepository.Interface
{
    public interface ICustomerDetailsRepository
    {
        Task<int> AddCustomerDetails(CustomerDetailsEntity customerDetailsEntity);
        Task<int> IsCustomerExsist(string Email);

    }
}
