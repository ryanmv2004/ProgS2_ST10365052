﻿using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace CLDVapp.Models
{
    public class productTable
    {
        public int productID { get; set; }
        public string name { get; set; }
        public string price { get; set; }
        public string description { get; set; }
        public string availability { get; set; }
        public string category { get; set; }

        public static string con_string = "Server=tcp:st10365052cldva1.database.windows.net,1433;Initial Catalog=st10365052cdlbdatabse;Persist Security Info=False;User ID=ryanv2304;Password=AceVents12;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        public static SqlConnection con = new SqlConnection(con_string);

        public int insert_Product(productTable m)
        {
            try
            {
                string sql = "INSERT INTO productTable (Name, Price, Description, Availability, Category) VALUES (@name, @price, @description, @availability, @category)";

                SqlCommand cmd = new SqlCommand(sql, con);

                cmd.Parameters.AddWithValue("@name", m.name);
                cmd.Parameters.AddWithValue("@price", m.price);
                cmd.Parameters.AddWithValue("@description", m.description);
                cmd.Parameters.AddWithValue("@availability", m.availability);
                cmd.Parameters.AddWithValue("@category", m.category);
                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                con.Close();
                return rowsAffected;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return -1;
            }
        }

        public List<productTable> GetAllProducts()
        {
            List<productTable> products = new List<productTable>();

            try
            {
                string sql = "SELECT * FROM productTable";

                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    products.Add(new productTable
                    {
                        name = reader["name"].ToString(),
                        price = reader["price"].ToString(),
                        description = reader["description"].ToString(),
                        availability = reader["availability"].ToString(),
                        category = reader["category"].ToString()
                    });
                }

                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return products;
        }

    }
}
