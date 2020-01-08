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

        public bool BookRoom(Booking booking)
        {
            SqlCommand cmd;
            bool res = false;
            string sql = "INSERT INTO [HotelBooking].[dbo].[Booking] (Id, RoomId, StartDate, EndDate, DateOfOrder, customerComment, totalPrice) VALUES (" + booking.Id+ ", "+booking.room.Id + ", " +booking.startDate + ", " +booking.endDate + ", " +booking.dateOfOrder + ", " +booking.customerComment + ", " +booking.totalPrice +");";
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                cmd = new SqlCommand(sql, con);
                try
                {
                    con.Open();
                    var read = cmd.ExecuteNonQuery();
                    if (read > 1) 
                        throw new Exception("Seems like too many rows were inserted");
                    else if (read == 1) 
                        res = true;
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
            return res;
        }

        public bool DeleteBooking(int bookingId)
        {
            SqlCommand cmd;
            bool res = false;
            string sql = "DELETE * FROM [HotelBooking].[dbo].[Booking] WHERE Id = " + bookingId;
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                cmd = new SqlCommand(sql, con);
                try
                {
                    con.Open();
                    var read = cmd.ExecuteNonQuery();
                    if (read > 1) throw new Exception("Seems like too many rows were deleted");
                    else res = true;
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
            return res;
        }

        public List<Booking> GetAllBookings()
        {
            SqlCommand cmd;
            List<Booking> res = new List<Booking>() ;
            int[] roomId = new int[50];
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
                            int counter = 0;
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
                                roomId[counter] = (int)read["RoomId"];
                                res.Add(booking);
                                counter++;
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
            for (var i = 0; i <= res.Count; i++)
            {
                res[i].room = roomRepo.GetRoomDetails(roomId[i]);
            }
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

        public List<Booking> GetBookingsByRoom(Room room)
        {
            SqlCommand cmd;
            List<Booking> res = new List<Booking>();
            string sql = "SELECT * FROM [HotelBooking].[dbo].[Booking] booking where booking.RoomId = " + room.Id;
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
                            while (read.Read())
                            {
                                //get the stuff
                                var booking = new Booking();
                                booking.Id = (int)read["Id"];
                                booking.customerComment = read["customerComment"].ToString();
                                booking.dateOfOrder = (DateTime)read["DateOfOrder"];
                                booking.startDate = (DateTime)read["StartDate"];
                                booking.endDate = (DateTime)read["EndDate"];
                                booking.totalPrice = (float)read["totalPrice"];
                                res.Add(booking);
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
            return res;
        }
    } 
}
