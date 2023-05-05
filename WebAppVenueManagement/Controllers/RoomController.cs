using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppVenueManagement.Controllers;
using WebAppVenueManagement.ViewModel;
using WebAppVenueManagement.Models;


namespace WebAppVenueManagement.Controllers
{
    public class RoomController : Controller
    {
        private VenueDBEntities objHotelDBEntities;
        public RoomController()
        {
            objHotelDBEntities = new VenueDBEntities();

        }

        // GET: Room
        public ActionResult Index()
        {
            RoomViewModel objRoomViewModel = new RoomViewModel();
            objRoomViewModel.ListOfBookingStatus = (from obj in objHotelDBEntities.BookingStatus
                                                    select new SelectListItem()
                                                    {
                                                        Text = obj.BookingStatus,
                                                        Value = obj.BookingStatusId.ToString()

                                                    }).ToList();

            objRoomViewModel.ListOfRoomType = (from obj in objHotelDBEntities.RoomTypes
                                               select new SelectListItem()
                                               {
                                                   Text = obj.RoomTypeName,
                                                   Value = obj.RoomTypeId.ToString()

                                               }).ToList();

            return View(objRoomViewModel);
        }



        [HttpPost]
        public ActionResult Index(RoomViewModel objRoomViewModel)
        {
            string message = string.Empty;
            string ImageUniqueName = string.Empty;
            string ActualImageName = string.Empty;



            if (objRoomViewModel.RoomId == 0)
            {
                 ImageUniqueName = Guid.NewGuid().ToString();
                 ActualImageName = ImageUniqueName + Path.GetExtension(objRoomViewModel.Image.FileName);
                 objRoomViewModel.Image.SaveAs(Server.MapPath("~/RoomImages/" + ActualImageName));

                
                Room objRoom = new Room()
                {
                    RoomNumber = objRoomViewModel.RoomNumber,
                    RoomDescription = objRoomViewModel.RoomDescription,
                    RoomPrice = objRoomViewModel.RoomPrice,
                    RoomCapacity = objRoomViewModel.RoomCapacity,
                    RoomTypeId = objRoomViewModel.RoomTypeId,
                    RoomImage = ActualImageName,
                    BookingStatusId = objRoomViewModel.BookingStatusId,
                    IsActive = true,
                   

                };
                objHotelDBEntities.Rooms.Add(objRoom);
                message = "Added";

            }
            else
            {
                if (objRoomViewModel.Image != null)
                {
                     ImageUniqueName = Guid.NewGuid().ToString();
                     ActualImageName = ImageUniqueName + Path.GetExtension(objRoomViewModel.Image.FileName);
                     objRoomViewModel.Image.SaveAs(Server.MapPath("~/RoomImages/" + ActualImageName));
                     objRoomViewModel.RoomImage = ActualImageName;

                }



                Room objRoom = objHotelDBEntities.Rooms.Single(model => model.RoomId ==objRoomViewModel.RoomId);

                objRoom.RoomNumber = objRoomViewModel.RoomNumber;
                objRoom.RoomDescription = objRoomViewModel.RoomDescription;
                objRoom.RoomPrice = objRoomViewModel.RoomPrice;
                objRoom.RoomCapacity = objRoomViewModel.RoomCapacity;
                objRoom.RoomTypeId = objRoomViewModel.RoomTypeId;
                objRoom.BookingStatusId = objRoomViewModel.BookingStatusId;
                objRoom.IsActive = true;
                message = "Updated";
            }

            
            objHotelDBEntities.SaveChanges();
            return Json(new { message = "Room successfully Added.", success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllRooms()
        {
            IEnumerable<RoomDetailsViewModel> listOfRoomDetailsViewModels = (
                from objRoom in objHotelDBEntities.Rooms
                join objBooking in objHotelDBEntities.BookingStatus on objRoom.BookingStatusId equals objBooking.BookingStatusId
                join objRoomType in objHotelDBEntities.RoomTypes on objRoom.RoomTypeId equals objRoomType.RoomTypeId
                where objRoom.IsActive==true
                select new RoomDetailsViewModel()
                {
                    RoomNumber = objRoom.RoomNumber,
                    RoomDescription = objRoom.RoomDescription,
                    RoomCapacity = objRoom.RoomCapacity,
                    RoomPrice = objRoom.RoomPrice,
                    BookingStatus = objBooking.BookingStatus,
                    RoomType = objRoomType.RoomTypeName,
                    RoomImage = objRoom.RoomImage,
                    RoomId = objRoom.RoomId,
                    RoomTypeId =objRoom.RoomTypeId

                }




                );

            return PartialView("_RoomDetailsPartial", listOfRoomDetailsViewModels);

        }
        [HttpGet]
        public JsonResult EditRoomDetails(int roomId)
        {
            var result = objHotelDBEntities.Rooms.Single(model => model.RoomId== roomId);
            return Json(new { message = "Room successfully Added.", success = true }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult DeleteRoomDetails(int roomId)
        {
            Room objRoom = objHotelDBEntities.Rooms.Single(model => model.RoomId == roomId);
            objRoom.IsActive = false;
            objHotelDBEntities.SaveChanges();

            return Json(new { message = "Record successfully Deleted.", success = true }, JsonRequestBehavior.AllowGet);
        }
    }    
}