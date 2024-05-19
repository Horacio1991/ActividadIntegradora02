using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ActividadIntegradora01
{
    public partial class MainWindow : Window
    {
        // Lista para guardar las instancias de Personas
        private List<Persona> listaDePersonas = new List<Persona>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Verificar si todos los campos están completos
            if (string.IsNullOrWhiteSpace(txtDNI.Text) || string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtApellido.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Campos Incompletos", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Verificar si el DNI contiene solo números
            if (!txtDNI.Text.All(char.IsDigit))
            {
                MessageBox.Show("El campo DNI solo puede contener números.", "DNI Inválido", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Verificar si el nombre contiene solo letras
            if (!txtNombre.Text.All(char.IsLetter))
            {
                MessageBox.Show("El campo Nombre solo puede contener letras.", "Nombre Inválido", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Verificar si el apellido contiene solo letras
            if (!txtApellido.Text.All(char.IsLetter))
            {
                MessageBox.Show("El campo Apellido solo puede contener letras.", "Apellido Inválido", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DateTime fechaAlta = datePickerFechaAlta.SelectedDate ?? DateTime.Now;

            // Instancia del objeto Persona pasando al constructor los datos ingresados en los inputs
            Persona nuevaPersona = new Persona(txtDNI.Text, txtNombre.Text, txtApellido.Text, fechaAlta);

            // Agregar la persona a la lista
            Persona.AgregarPersona(listaDePersonas, nuevaPersona);

            // Actualizar la vista
            LlenarDataGridConLista();

            // Limpiar los inputs para poder agregar la siguiente persona
            LimpiarCampos();
        }

        private void LimpiarCampos()
        {
            txtDNI.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
            datePickerFechaAlta.SelectedDate = null;
            txtAntiguedad.Text = string.Empty;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // Obtener el DNI ingresado en el campo txtDNI
            string dniABuscar = txtDNI.Text;

            // Verificar que se ingrese un dni válido
            if (!txtDNI.Text.All(char.IsDigit))
            {
                MessageBox.Show("El campo DNI solo puede contener números.", "DNI Inválido", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Buscar la persona en la lista, al ser un metodo estatico lo puedo llamar sin hacer una instancia del objeto persona
            Persona personaEncontrada = Persona.BuscarPersonaPorDNI(dniABuscar, listaDePersonas);

            // Verificar si se encontró la persona
            if (personaEncontrada != null)
            {
                // Mostrar los datos de la persona encontrada
                txtNombre.Text = personaEncontrada.Nombre;
                txtApellido.Text = personaEncontrada.Apellido;
                datePickerFechaAlta.SelectedDate = personaEncontrada.FechaIngreso;
                txtAntiguedad.Text = personaEncontrada.Antiguedad.ToString();
            }
            else
            {
                // Mostrar un mensaje indicando que no se encontró la persona
                MessageBox.Show("No se encontró la persona con el DNI proporcionado.", "Búsqueda", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void DatePickerFechaAlta_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            // Verificar si se ha seleccionado una fecha
            if (datePickerFechaAlta.SelectedDate.HasValue)
            {
                // Actualizar el campo de antigüedad basado en la fecha seleccionada
                DateTime fechaSeleccionada = datePickerFechaAlta.SelectedDate.Value;

                // Crear una instancia temporal de Persona para calcular la antigüedad
                Persona personaTemporal = new Persona("", "", "", fechaSeleccionada);
                int antiguedad = personaTemporal.CalcularAntiguedad();

                // Actualizar el campo de antigüedad
                txtAntiguedad.Text = antiguedad.ToString();
            }
        }


        private void LlenarDataGridConLista()
        {
            // Limpiar la DataGrid
            dgPersonas.ItemsSource = null;

            // Llenar el DataGrid con los datos de la lista de personas
            dgPersonas.ItemsSource = listaDePersonas;
        }
    }
}
