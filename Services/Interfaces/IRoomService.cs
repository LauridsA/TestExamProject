using Models;
using System;
using System.Collections.Generic;

namespace Services.Interfaces
{
    public interface IRoomService
    {
        Room GetRoom(int i);
        bool IsBooked(int roomId);
        List<Room> GetAllAvailableRooms(DateTime start, DateTime end);
    }
}