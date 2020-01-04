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
            return repo.GetRoomDetails(1);
        }

        public List<Room> GetAllAvailableRooms(DateTime start, DateTime end)
        {
            if (start >= end) throw new ArgumentException("start should be larger or equal to end");
            return repo.GetAllAvailableRooms(start, end);
        }

        public bool IsBooked(int roomId)
        {
            return repo.IsBooked(roomId);
        }
        
    }
}
