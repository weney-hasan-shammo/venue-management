using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppVenueManagement.Controllers;
using WebAppVenueManagement.ViewModel;
using WebAppVenueManagement.Models;



namespace WebAppVenueManagement.Controllers
{
    public class BookingController : Controller
    {
        
        private VenueDBEntities objHotelDBEntities;
        public BookingController()
        {
            objHotelDBEntities = new VenueDBEntities();

        }
        // GET: Booking
        public ActionResult Index()
        {
            BookingViewModel objBookingViewModel = new BookingViewModel();
            objBookingViewModel.ListOfRooms = (from objRoom in objHotelDBEntities.Rooms
                                            where objRoom.BookingStatusId==2
                                            select new SelectListItem()
                                            {
                                                Text = objRoom.RoomNumber,
                                                Value = objRoom.RoomId.ToString()

                                            }).ToList();

            objBookingViewModel.BookingFrom = DateTime.Now;
            objBookingViewModel.BookingTo = DateTime.Now;


            return View(objBookingViewModel);
        }
        [HttpPost]
        public ActionResult Index(BookingViewModel objBookingViewModel)
        {
            int numberOfDays = Convert.ToInt32((objBookingViewModel.BookingTo-objBookingViewModel.BookingFrom).TotalDays); 
            Room objRoom = objHotelDBEntities.Rooms.Single(model => model.RoomId == objBookingViewModel.AssignRoomId);

            decimal RoomPrice = objRoom.RoomPrice;

            decimal TotalAmount =RoomPrice * numberOfDays;

            RoomBooking roomBooking = new RoomBooking() {

                BookingFrom = objBookingViewModel.BookingFrom,
                BookingTo = objBookingViewModel.BookingTo,

                AssignRoomId = objBookingViewModel.AssignRoomId,

                CustomerAddress = objBookingViewModel.CustomerAddress,

                CustomerName = objBookingViewModel.CustomerName,

                NoOfMembers = objBookingViewModel.NumberOfMembers,

                TotalAmount = TotalAmount
            };

                    objHotelDBEntities.RoomBookings.Add(roomBooking);

                    objHotelDBEntities.SaveChanges();
                    return Json(new { message = "Booking successfully Done.", success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}