using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.ADO.Interfaces
{
    public interface IBookingRepo
    {
        bool IsBooked(int roomId);
        Room GetRoomDetails(int roomId);
        bool BookRoom(int roomId);
        bool UnBookRoom(int roomId);
        List<Room> GetAllAvailableRooms(DateTime start, DateTime end);

    }
}
