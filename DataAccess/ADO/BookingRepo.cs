using DataAccess.ADO.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DataAccess.ADO
{
    public class BookingRepo : IBookingRepo
    {
        private readonly string connectionstring;
        private IRoomRepo roomRepo;
        public BookingRepo(string connectionstring, IRoomRepo roomRepo)
        {
            this.connectionstring = connectionstring;
            this.roomRepo = roomRepo;
        }

        public bool BookRoom(Room room, DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        public bool DeleteBooking(int bookingId)
        {
            throw new NotImplementedException();
        }

        public List<Booking> GetAllBookings()
        {
            SqlCommand cmd;
            List<Booking> res = new List<Booking>() ;
            int? roomId = null;
            string sql = "SELECT * FROM [HotelBooking].[dbo].[Booking]";
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                cmd = new SqlCommand(sql, con);
                try
                {
                    con.Open();
                    var read = cmd.ExecuteReader();
                    if (read.HasRows)
                    {
                        try
                        {
                            while(read.Read())
                            {
                                //get the stuff
                                var booking = new Booking();
                                booking.Id = (int)read["Id"];
                                booking.customerComment = read["customerComment"].ToString();
                                booking.dateOfOrder = (DateTime)read["DateOfOrder"];
                                booking.startDate = (DateTime)read["StartDate"];
                                booking.endDate = (DateTime)read["EndDate"];
                                booking.totalPrice = (float)read["totalPrice"];
                                roomId = (int)read["RoomId"];
                            }
                        }
                        catch (Exception e)
                        {
                            throw e;
                        }
                    }
                }
                catch (SqlException e)
                {
                    throw e;
                }
                finally
                {
                    con.Close();
                }
            }
            //if (roomId != null)
            //    res.room = roomRepo.GetRoomDetails(roomId);
            return res;
        }

        public Booking GetBookingDetails(int bookingId)
        {
            SqlCommand cmd;
            Booking res = null;
            int? roomId = null;
            string sql = "SELECT * FROM [HotelBooking].[dbo].[Booking] WHERE Id = " + bookingId;
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                cmd = new SqlCommand(sql, con);
                try
                {
                    con.Open();
                    var read = cmd.ExecuteReader();
                    if (read.HasRows)
                    {
                        try
                        {
                            read.Read();
                            //get the stuff
                            res = new Booking();
                            res.Id = (int)read["Id"];
                            res.customerComment = read["customerComment"].ToString();
                            res.dateOfOrder = (DateTime)read["DateOfOrder"];
                            res.startDate = (DateTime)read["StartDate"];
                            res.endDate = (DateTime)read["EndDate"];
                            res.totalPrice = (float)read["totalPrice"];
                            roomId = (int)read["RoomId"];
                        }
                        catch (Exception e)
                        {
                            throw e;
                        }
                    }
                }
                catch (SqlException e)
                {
                    throw e;
                }
                finally
                {
                    con.Close();
                }
            }
            if (roomId != null)
                res.room = roomRepo.GetRoomDetails((int)roomId);
            return res;
        }
    } 
}
