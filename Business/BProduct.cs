using System;
using System.Collections.Generic;
using Data;
using Entity;

namespace Business
{
    public class BProduct
    {
        private ProductoData _productoData;

        public BProduct()
        {
            _productoData = new ProductoData();
        }

        // Método para obtener la lista de productos
        public List<Product> ObtenerProductos()
        {
            try
            {
                return _productoData.ListarProductos();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los productos", ex);
            }
        }

        // Nuevo método para insertar un producto
        public bool InsertarProducto(Product producto)
        {
            try
            {
                return _productoData.InsertarProducto(producto);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar el producto", ex);
            }
        }

        public bool EliminarLogicoProducto(int productId)
        {
            try
            {
                return _productoData.EliminarLogicoProducto(productId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar lógicamente el producto", ex);
            }
        }


    }
}
