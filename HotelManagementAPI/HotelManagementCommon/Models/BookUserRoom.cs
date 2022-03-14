using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManagementCommon.Models
{
    public class BookUserRoom
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string RoomType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
