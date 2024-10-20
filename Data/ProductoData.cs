using Entity;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Data
{
    public class ProductoData
    {
        private readonly string _connectionString = DataAccess.cadena;

        // Método para listar productos (ya lo tienes)
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

        // Método para agregar un producto nuevo
        public bool InsertarProducto(Product producto)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("USP_InsertarProducto", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                // Agregamos los parámetros del procedimiento almacenado
                command.Parameters.AddWithValue("@name", producto.Name);
                command.Parameters.AddWithValue("@price", producto.Price);
                command.Parameters.AddWithValue("@stock", producto.Stock);
                command.Parameters.AddWithValue("@active", 1);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();  // Ejecuta la inserción

                    return rowsAffected > 0;  // Retorna true si se insertó correctamente
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al insertar producto", ex);
                }
            }
        }

        public bool EliminarLogicoProducto(int productId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("USP_EliminarProducto", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                // Agregar el parámetro del ID del producto
                command.Parameters.AddWithValue("@product_id", productId);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al eliminar lógicamente el producto", ex);
                }
            }
        }

    }
}
