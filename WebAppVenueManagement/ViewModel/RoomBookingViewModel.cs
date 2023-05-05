using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebAppVenueManagement.ViewModel
{
    public class RoomBookingViewModel
    {
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerPhone { get; set; }
        public DateTime? BookingFrom { get; set; }
        public DateTime? BookingTo { get; set; }
        public int AssignRoomId { get; set; }
        public int NumberOfMembers { get; set; }
        public IEnumerable<SelectListItem> ListOfRooms { get; set; }
    }
}