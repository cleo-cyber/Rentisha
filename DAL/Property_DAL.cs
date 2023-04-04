using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Rentisha.Models;
namespace Rentisha.DAL
{
    public class Property_DAL
    {
        string connString = ConfigurationManager.ConnectionStrings["myConnectionstring"].ToString();
        // Get All listings
        public List<Properties> GetAllProperties()
        {

            List<Properties> propertiesList = new List<Properties>();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = conn.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_GetAllProducts";
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dtProperties = new DataTable();

                conn.Open();
                adapter.Fill(dtProperties);
                conn.Close();

                foreach ( DataRow dr in dtProperties.Rows)
                {
                    propertiesList.Add(new Properties
                    {
                        listing_id = Convert.ToInt32( dr["listing_id"]),
                        County = dr["County"].ToString(),
                        Town = dr["Town"].ToString(),
                        Rooms = Convert.ToInt32(dr["Rooms"]),
                        Price = Convert.ToInt32(dr["Price"]),
                        PropertyName = dr["PropertyName"].ToString(),
                        PropertyType = dr["PropertyType"].ToString(),
                        Rent = Convert.ToInt32(dr["Rent"])
                    });
                }
            }

            return propertiesList;
        }

        //Insert listing
        public bool InsertProperty(Properties property)
        {
            int Id = 0;
            using (SqlConnection connection = new SqlConnection(connString))
            {
                //Get store procedure for insert

                SqlCommand command = new SqlCommand("sp_InsertProperties",connection);
                command.CommandType = CommandType.StoredProcedure;

                //Insert
                command.Parameters.AddWithValue("@County", property.County);
                command.Parameters.AddWithValue("@Town", property.Town);
                command.Parameters.AddWithValue("@Rooms", property.Rooms);
                command.Parameters.AddWithValue("@Price", property.Price);
                command.Parameters.AddWithValue("@PropertyName", property.PropertyName);
                command.Parameters.AddWithValue("@PropertyType", property.PropertyType);
                command.Parameters.AddWithValue("@Rent", property.Rent);

                connection.Open();
                Id=command.ExecuteNonQuery();
                connection.Close();


            }
            if (Id > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}