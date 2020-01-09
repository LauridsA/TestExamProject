using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;
using Services.Interfaces;

namespace HotelBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : Controller
    {
        private IBookingService bookingService;
        public BookingController(IBookingService bookingService)
        {
            this.bookingService = bookingService;
        }

        // GET api/booking
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "FRONT PAGE OF BOOKING CONTROLLER. ENDPOINTS: /{id} /delete/{id} /getAllBookings /BookRoom/{id}";
        }

        // EXAMPLE: GET api/booking/5
        [HttpGet("{id}")]
        public ActionResult<Booking> Get(int id)
        {
            return bookingService.GetBookingDetails(id);
        }

        // EXAMPLE: GET api/booking/getAllBookings
        [HttpGet("getAllBookings")]
        public ActionResult<List<Booking>> GetAllBookings()
        {
            return bookingService.GetAllBookings();
        }

        // EXAMPLE: DELETE api/booking/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            bookingService.DeleteBooking(id);
        }

        // EXAMPLE: POST api/booking/5
        [HttpPost]
        [Route("bookRoom/{id}")]
        public ActionResult<bool> BookRoom(int id, [FromBody] BookingRequest requestObject)
        {
            if (requestObject.start.Date < DateTime.Today)
                return BadRequest("start date cannot be in the past");
            if (requestObject.end.Date < DateTime.Today)
                return BadRequest("end date is in the past");
            return bookingService.BookRoom(id, requestObject.start, requestObject.end, requestObject.customerComment);
        }
    }
}