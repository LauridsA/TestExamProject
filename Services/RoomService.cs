using DataAccess.ADO;
using DataAccess.ADO.Interfaces;
using Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class RoomService : IRoomService
    {
        private IRoomRepo repo;
        public RoomService(IRoomRepo repo)
        {
            this.repo = repo;
        }
        public Room GetRoom(int i)
        {
            return repo.GetRoomDetails(i);
        }

        public List<Room> GetAllAvailableRooms(DateTime start, DateTime end)
        {
            if (start.Date > end.Date) throw new ArgumentException("start should be before end");
            return repo.GetAllAvailableRooms(start, end);
        }

        public bool IsBooked(int roomId)
        {
            return repo.IsBooked(roomId);
        }
        
    }
}
