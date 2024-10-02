using Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Data
{
    public class ProductoData
    {

        private readonly string _connectionString = DataAccess.cadena;

        public List<Product> ListarProductos()
        {
            List<Product> productos = new List<Product>();


            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
               
                SqlCommand command = new SqlCommand("USP_ListProducts", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                   
                    while (reader.Read())
                    {
                        Product producto = new Product
                        {
                            ProductId = (int)reader["product_id"],
                            Name = reader["name"].ToString(),
                            Price = (decimal)reader["price"],
                            Stock = (int)reader["stock"],
                            Active = (bool)reader["active"]
                        };

                        productos.Add(producto);
                    }

                    reader.Close();
                }
                catch (SqlException ex)
                {
                    
                    throw new Exception("Error al listar productos", ex);
                }
            }

            return productos;
        }
    }
}
