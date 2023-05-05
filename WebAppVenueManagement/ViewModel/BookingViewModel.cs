using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebAppVenueManagement.ViewModel
{
    public class BookingViewModel
    {
        [Display(Name = "Customer Name")]
        [Required(ErrorMessage = "Customer Name is required.")]

        public string CustomerName { get; set; }


        [Display(Name = "Customer Address")]
        [Required(ErrorMessage = "Customer Address is required.")]

        public string CustomerAddress { get; set; }

        [Display(Name = "Customer Phone")]
        [Required(ErrorMessage = "Customer Phone is required.")]

        public string CustomerPhone { get; set; }

        [Display(Name = "Booking From")]
        [Required(ErrorMessage = "Booking From Date is required.")]
        [DisplayFormat(DataFormatString ="{0:dd-MMM-yyyy}", ApplyFormatInEditMode =true)]

        [DataType(DataType.Date)]
        public DateTime BookingFrom { get; set; }

        [Display(Name = "Booking To")]
        [Required(ErrorMessage = "Booking To Date is required.")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]

        public DateTime BookingTo { get; set; }
        [DataType(DataType.Date)]

        [Display(Name = "Assign Room ID")]
        [Required(ErrorMessage = "Assign Room ID is required.")]
        public int AssignRoomId { get; set; }

        [Display(Name = "Number of Guests")]
        [Required(ErrorMessage = "Number of Guests is required.")]
        public int NumberOfMembers { get; set; }

        public IEnumerable<SelectListItem> ListOfRooms { get; set; }









    }
}