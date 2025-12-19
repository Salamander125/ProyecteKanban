using System;
using System.Collections.Generic;
using System.Windows;
using ToDoListTest.Model;

namespace ToDoListTest
{
    public partial class NuevaTicket : Window
    {
        public NuevaTicket()
        {
            InitializeComponent();

            dpCreacion.Text = DateTime.Now.ToString("dd/MM/yyyy");

            InicializarDatos();
        }

        private async void InicializarDatos()
        {
            try
            {
                List<Responsable> responsables = await API_REST.Instance.GetAllResponsableAsync();

                cbResponsable.ItemsSource = responsables;

                if (cbResponsable.Items.Count > 0)
                {
                    cbResponsable.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar responsables: {ex.Message}");
            }
        }

        private void Cancelar(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            Close();
        }

        private async void AddTask(object sender, RoutedEventArgs e)
        {
            long? resp = null;

            if (cbResponsable.SelectedValue != null)
                resp = Convert.ToInt64(cbResponsable.SelectedValue);

            if (txtTitulo.Text == "")
            {
                MessageBox.Show("El título no puede estar vacío.");
                return;
            }
            if (dpFinalizacion.SelectedDate == null)
            {
                MessageBox.Show("Debe seleccionar una fecha de finalización.");
                return;
            }

            try
            {
                Tasca nuevaTasca = new Tasca
                {
                    Titol = txtTitulo.Text,
                    Descripcio = txtDescripcion.Text,
                    Data_creacio = DateTime.Now,
                    Data_finalitzacio = dpFinalizacion.SelectedDate ?? DateTime.Now.AddDays(1),
                    Prioritat = cbPrioridad.SelectedIndex,
                    Estat = 0,
                    Codi_responsable = resp
                };

                await API_REST.Instance.AddTascaAsync(nuevaTasca);

                this.DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al crear la tasca: {ex.Message}");
            }
        }
    }
}