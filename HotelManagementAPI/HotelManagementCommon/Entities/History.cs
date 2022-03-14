using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManagementCommon.Entities
{
    public class History:RoomsEntity
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
