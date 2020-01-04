using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.ADO.Interfaces;
using Models;

namespace DataAccess.ADO
{
    public class RoomRepo : IRoomRepo
    {
        private string connectionString;
        
        public RoomRepo(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Room> GetAllAvailableRooms(DateTime start, DateTime end)
        {
            SqlCommand cmd;
            List<Room> res = new List<Room>();
            //i wonder if i gotta convert this.
            string sql = "SELECT * FROM [HotelBooking].[dbo].[Room] as A WHERE A.[Id] NOT IN(SELECT room.[Id] FROM [HotelBooking].[dbo].[Room] room JOIN [HotelBooking].[dbo].[Booking] booking ON booking.[RoomId] = room.[Id] WHERE booking.StartDate < "+ start+ " AND booking.EndDate < " +end+ ")";
            using (SqlConnection con = new SqlConnection(connectionString))
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
                                var room = new Room();
                                room.Id = (int)read["id"];
                                room.status = read["status"].ToString();
                                room.PricePerDay = (float)read["PricePerDay"];
                                room.available = (bool)read["Available"];
                                switch ((int)read["RoomTypeId"])
                                {
                                    case 1:
                                        room.roomType = Room.RoomType.Basic;
                                        break;
                                    case 2:
                                        room.roomType = Room.RoomType.Standard;
                                        break;
                                    case 3:
                                        room.roomType = Room.RoomType.Deluxe;
                                        break;
                                    case 4:
                                        room.roomType = Room.RoomType.Penthouse;
                                        break;
                                    default:
                                        throw new Exception("unknown room type");
                                }
                                res.Add(room);
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

        public Room GetRoomDetails(int roomId)
        {
            SqlCommand cmd;
            Room res = null;
            string sql = "SELECT * FROM Room WHERE id = "+roomId;
            using (SqlConnection con = new SqlConnection(connectionString))
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
                            res = new Room();
                            res.Id = (int)read["id"];
                            res.status = read["status"].ToString();
                            res.PricePerDay = (float)read["PricePerDay"];
                            res.available = (bool)read["Available"];
                            switch ((int)read["RoomTypeId"])
                            {
                                case 1:
                                    res.roomType = Room.RoomType.Basic;
                                    break;
                                case 2:
                                    res.roomType = Room.RoomType.Standard;
                                    break;
                                case 3:
                                    res.roomType = Room.RoomType.Deluxe;
                                    break;
                                case 4:
                                    res.roomType = Room.RoomType.Penthouse;
                                    break;
                                default:
                                    throw new Exception("unknown room type");
                            }
                        } catch (Exception e)
                        {
                            throw e;
                        }
                    }
                } catch (SqlException e)
                {
                    throw e;
                } finally
                {
                    con.Close();
                }
            }
            return res;
        }

        public bool IsBooked(int roomId)
        {
            SqlCommand cmd;
            bool res = false;
            string sql = "SELECT * FROM Room WHERE id = " + roomId;
            using (SqlConnection con = new SqlConnection(connectionString))
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
                            res = (bool)read["Available"];
                            
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
