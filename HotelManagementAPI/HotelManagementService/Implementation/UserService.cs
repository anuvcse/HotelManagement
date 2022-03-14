using HotelManagementCommon.Entities;
using HotelManagementCommon.Models;
using HotelManagementRepository.Interface;
using HotelManagementService.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementService.Implementation
{
    public class UserService : IUserService
    {
        private readonly ICustomerDetailsRepository _customerDetailsRepository;
        private readonly IBookingDetailsRepository _bookingDetailsRepository;
        private readonly ILogger<UserService> _logger;
        public UserService(ICustomerDetailsRepository customerDetailsRepository,IBookingDetailsRepository bookingDetailsRepository, ILogger<UserService> logger)
        {
            _customerDetailsRepository = customerDetailsRepository ?? throw new ArgumentNullException(nameof(customerDetailsRepository));
            _bookingDetailsRepository = bookingDetailsRepository ?? throw new ArgumentNullException(nameof(bookingDetailsRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> BookRoom(BookUserRoom bookUserRoom)
        {

            int customerID =await _customerDetailsRepository.IsCustomerExsist(bookUserRoom.Email);
            if(customerID == 0)
            {
                CustomerDetailsEntity customerDetailsEntity = new CustomerDetailsEntity { CustomerName = bookUserRoom.UserName, Email = bookUserRoom.Email };
                customerID=await _customerDetailsRepository.AddCustomerDetails(customerDetailsEntity);
            }
            var result = await _bookingDetailsRepository.DisplayBookedRooms(bookUserRoom.StartDate, bookUserRoom.EndDate).ConfigureAwait(false);
            int? RoomID = result.Where(x => x.Status == "Not Booked" && x.RoomType== bookUserRoom.RoomType).Select(x => x.RoomNo).FirstOrDefault();
            if(RoomID ==null || RoomID==0)
            {
                return false;
            }
            BookingDetailsEntity bookingDetailsEntity = new BookingDetailsEntity
                                                       {RoomNo=(int)RoomID,
                                                        CustomerID= (int)customerID, 
                                                        StartDate= bookUserRoom.StartDate,
                                                        EndDate= bookUserRoom.EndDate
                                                       };
            int BookDetailsID = await _bookingDetailsRepository.BookRoom(bookingDetailsEntity);
            if(BookDetailsID==0)
            {
                return false;
            }
            return true;
        }

        public async Task<List<History>> GetHistory(string Email)
        {
            int CustomerID= await _customerDetailsRepository.IsCustomerExsist(Email);
            return await _bookingDetailsRepository.GetHistoryBookingByUser(CustomerID);
        }
    }
}
