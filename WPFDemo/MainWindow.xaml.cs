using System;
using System.Collections.Generic;
using System.Windows;
using Business;
using Entity;

namespace WPFDemo
{
    public partial class MainWindow : Window
    {
        private BProduct _bProduct;
        private List<Product> _todosLosProductos;

        public MainWindow()
        {
            InitializeComponent();
            _bProduct = new BProduct();
            CargarTodosLosProductos();
        }

        // Método para cargar todos los productos y mostrarlos en el DataGrid
        private void CargarTodosLosProductos()
        {
            try
            {
                _todosLosProductos = _bProduct.ObtenerProductos();
                dgProductos.ItemsSource = _todosLosProductos;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al cargar los productos: " + ex.Message);
            }
        }

        // Método para buscar productos por nombre
        private void BuscarProducto_Click(object sender, RoutedEventArgs e)
        {
            string nombreProducto = txtNombreProducto.Text;

            try
            {
                if (string.IsNullOrWhiteSpace(nombreProducto))
                {
                    dgProductos.ItemsSource = _todosLosProductos;
                }
                else
                {
                    var productosFiltrados = _todosLosProductos.FindAll(p => p.Name.Contains(nombreProducto, StringComparison.OrdinalIgnoreCase));
                    dgProductos.ItemsSource = productosFiltrados;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al buscar los productos: " + ex.Message);
            }
        }

        // Método para agregar un nuevo producto
        private void AgregarProducto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Product nuevoProducto = new Product
                {
                    Name = txtNombreNuevo.Text,
                    Price = decimal.Parse(txtPrecioNuevo.Text),
                    Stock = int.Parse(txtStockNuevo.Text)
                };

                // Insertar el producto a través de la capa de negocio
                bool productoInsertado = _bProduct.InsertarProducto(nuevoProducto);

                if (productoInsertado)
                {
                    MessageBox.Show("Producto insertado correctamente.");
                    CargarTodosLosProductos();  // Recargar los productos para reflejar el nuevo producto
                }
                else
                {
                    MessageBox.Show("Error al insertar el producto.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al agregar el producto: " + ex.Message);
            }
        }

        // Método para eliminar lógicamente un producto
        private void EliminarProducto_Click(object sender, RoutedEventArgs e)
        {
            if (dgProductos.SelectedItem is Product productoSeleccionado)
            {
                if (MessageBox.Show("¿Está seguro de que desea eliminar este producto?", "Confirmar eliminación", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    try
                    {
                        bool eliminado = _bProduct.EliminarLogicoProducto(productoSeleccionado.ProductId);
                        if (eliminado)
                        {
                            MessageBox.Show("Producto eliminado correctamente.");
                            CargarTodosLosProductos();  // Recargar la lista de productos
                        }
                        else
                        {
                            MessageBox.Show("Error al eliminar el producto.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ocurrió un error al eliminar el producto: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Seleccione un producto para eliminar.");
            }
        }
    }
}
