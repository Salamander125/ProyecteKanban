using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ToDoListTest.Model;

namespace ToDoListTest
{
    enum EstatTasca
    {
        Pendiente = 0,
        En_curso = 1,
        Finalizado = 2
    }
    /// <summary>
    /// Lógica de interacción para Eliminar.xaml
    /// </summary>
    public partial class Eliminar : Window
    {
        private Tasca _tasca;

        // Modificamos el constructor para recibir el objeto
        public Eliminar(Tasca tascaRecibida)
        {
            InitializeComponent();
            this._tasca = tascaRecibida;
            CargarDatosEnPantalla();
            InicializarDatos();
        }
        private async void InicializarDatos()
        {

            var responsables = await API_REST.Instance.GetAllResponsableAsync();

            cbResponsable.ItemsSource = responsables;

            if (_tasca.Codi_responsable != null)
            {
                cbResponsable.SelectedValue = _tasca.Codi_responsable;
            }
        }

        private void CargarDatosEnPantalla()
        {
            lblTitol.Text = _tasca.Titol;
            lblEstado.Text = $"Estado: {(EstatTasca)_tasca.Estat}";
            txtFechaCreacion.Text = _tasca.Data_creacio.ToString("dd/MM/yyyy");
            txtFechaFinalizacion.Text = _tasca.Data_finalitzacio.ToString("dd/MM/yyyy");
            lblIdTasca.Text = $"ID: {_tasca.Codi}";

            txtDescripcio.Text = _tasca.Descripcio;
            cbPrioridad.SelectedIndex = _tasca.Prioritat;
        }

        private void Cancelar(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            Close();
        }

        private async void Eliminarbtn(object sender, RoutedEventArgs e)
        {
            await API_REST.Instance.DeleteTascaAsync(_tasca.Codi);
            this.DialogResult = true;
            Close();
        }

        private async void Modificar(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbResponsable.SelectedValue == null)
                {
                    MessageBox.Show("Por favor, selecciona un responsable.");
                    return;
                }

                _tasca.Prioritat = cbPrioridad.SelectedIndex;
                _tasca.Descripcio = txtDescripcio.Text;

                _tasca.Codi_responsable = Convert.ToInt64(cbResponsable.SelectedValue);

                await API_REST.Instance.UpdateTascaAsync(_tasca);

                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar: {ex.Message}");
            }
        }
    }
}
