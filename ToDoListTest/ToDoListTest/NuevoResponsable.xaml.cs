using System;
using System.Windows;
using ToDoListTest.Model;

namespace ToDoListTest
{
    public partial class NuevoResponsable : Window
    {
        public NuevoResponsable()
        {
            InitializeComponent();
        }

        private async void GuardarResponsable_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Responsable nuevo = new Responsable
                {
                    usuari = txtUsername.Text,
                    contrasenya = txtPassword.Password,
                    admin = chkIsAdmin.IsChecked ?? false
                };

                await API_REST.Instance.AddResponsableAsync(nuevo);

                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}