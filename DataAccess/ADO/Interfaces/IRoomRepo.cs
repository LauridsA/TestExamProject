using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.ADO.Interfaces
{
    public interface IRoomRepo
    {
        bool IsBooked(int roomId);
        Room GetRoomDetails(int roomId);
        List<Room> GetAllAvailableRooms(DateTime start, DateTime end);

    }
}
