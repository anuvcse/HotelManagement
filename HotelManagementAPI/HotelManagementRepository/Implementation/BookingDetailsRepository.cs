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
    public class BookingDetailsRepository : IBookingDetailsRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public BookingDetailsRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<int> BookRoom(BookingDetailsEntity bookkingDetailsEntity)
        {
             _applicationDbContext.BookingDetails.Add(bookkingDetailsEntity);
              return await _applicationDbContext.SaveChangesAsync();
        }

        public async Task<List<RoomStatus>> DisplayBookedRooms(DateTime date, DateTime endDate)
        {
            return await (from c in _applicationDbContext.Rooms.Where(r=> r.CreatedAt.Date<=date.Date)
                          join p in _applicationDbContext.BookingDetails.Where(p => (date.Date >= p.StartDate.Date && date.Date <= p.EndDate.Date) ||
                                (endDate.Date >= p.StartDate.Date && endDate.Date <= p.EndDate.Date)) on c.RoomNo equals p.RoomNo into ps
                          from p in ps.DefaultIfEmpty()                     
                          select new RoomStatus {  RoomNo= c.RoomNo, RoomType= c.RoomType,
                          Status = p.BookingID>0 ? "Booked":"Not Booked",
                          }).ToListAsync();
            
        }

        public async Task<List<History>> GetHistoryBookingByUser(int CustomerID)
        {
            return await _applicationDbContext.BookingDetails.Where(x => x.CustomerID == CustomerID)
                         .Join(_applicationDbContext.Rooms, booking=> booking.RoomNo,rooms=> rooms.RoomNo,
                         (booking,rooms)=> new History{ 
                             RoomNo=booking.RoomNo,
                             RoomType=rooms.RoomType,
                             StartDate=booking.StartDate,
                             EndDate=booking.EndDate}).ToListAsync();
        }
    }
}
