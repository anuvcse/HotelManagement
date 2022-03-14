using System;
using System.ComponentModel.DataAnnotations;

namespace HotelManagement_User.Models
{
    public class BookRoom
    {
        [Required]
        [Display(Name = "Room Type")]
        public string RoomType { get; set; }
        [Required]
        [Display(Name = "From Date")]
        [DisplayFormat(DataFormatString = "{MM-dd-yyyy}", ApplyFormatInEditMode = true)]    
        public DateTime StartDate { get; set; }
        [DisplayFormat(DataFormatString = "{MM-dd-yyyy}", ApplyFormatInEditMode = true)]

        [Display(Name = "To Date")]
        [Required]
        public DateTime EndDate { get; set; }

        public string CustomerName { get; set; }
        public string Email { get; set; }
    }

    
  
}
