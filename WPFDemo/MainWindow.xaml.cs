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
                    // Filtrar los productos por el nombre ingresado
                    var productosFiltrados = _todosLosProductos.FindAll(p => p.Name.Contains(nombreProducto, StringComparison.OrdinalIgnoreCase));

                    // Mostrar los productos filtrados en el DataGrid
                    dgProductos.ItemsSource = productosFiltrados;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al buscar los productos: " + ex.Message);
            }
        }
    }
}
