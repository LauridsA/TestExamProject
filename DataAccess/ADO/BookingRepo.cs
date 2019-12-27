using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.ADO.Interfaces;
using Models;

namespace DataAccess.ADO
{
    public class BookingRepo : IBookingRepo
    {
        private string connectionString;
        
        public BookingRepo(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public bool BookRoom(int roomId)
        {
            throw new NotImplementedException();
        }

        public List<Room> GetAllAvailableRooms(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        public Room GetRoomDetails(int roomId)
        {
            SqlCommand cmd;
            Room res = null;
            string sql = "SELECT * FROM Room WHERE id = '1'";
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
            throw new NotImplementedException();
        }

        public bool UnBookRoom(int roomId)
        {
            throw new NotImplementedException();
        }
    }
}
