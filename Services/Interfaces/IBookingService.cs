using Models;
using System;
using System.Collections.Generic;

namespace Services.Interfaces
{
    public interface IBookingService
    {
        bool BookRoom();
        Room GetRoom(int i);
        bool UnBookRoom(int roomId);
        bool IsBooked(int roomId);
        List<Room> GetAllAvailableRooms(DateTime start, DateTime end);
    }
}