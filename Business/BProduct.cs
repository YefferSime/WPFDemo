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
    }
}
