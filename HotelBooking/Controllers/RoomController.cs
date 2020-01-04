using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Interfaces;

namespace HotelBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : Controller
    {
        private IRoomService roomService;
        public RoomController(IRoomService bookingService)
        {
            this.roomService = bookingService;
        }

        // GET api/room
        [HttpGet]
        public ActionResult<string> Get()
        {
            return  "FRONT PAGE OF ROOM CONTROLLER. ENDPOINTS: /{id} /checkAvailablity/{id} /checkAllAvailableRooms/..." ;
        }

        // example: GET api/room/5
        [HttpGet("{id}")]
        public ActionResult<bool> Get(int id)
        {
            return roomService.IsBooked(id);
        }

        // example: GET api/room/checkAvailablity/5
        [HttpGet("checkAvailablity/{id}")]
        public ActionResult<Room> CheckAvailability(int id)
        {
            return roomService.GetRoom(id);
        }

        // example: GET api/room/checkAllAvaibleRooms/...
        [HttpGet("checkAllAvailableRooms/{id}")]
        public ActionResult<List<Room>> CheckAllAvailableRooms([FromBody] DateTime start, [FromBody] DateTime end)
        {
            return roomService.GetAllAvailableRooms(start, end);
        }
        
    }
}