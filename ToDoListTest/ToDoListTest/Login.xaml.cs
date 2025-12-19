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

namespace ToDoListTest
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }
        private async void Aceptar(object sender, RoutedEventArgs e)
        {
            string user = txtUsuario.Text.Trim();
            string passInput = txtPassword.Password;

            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(passInput))
            {
                MessageBox.Show("Rellena todos los campos.");
                return;
            }

            try
            {
                string passDB = await API_REST.Instance.GetPasswordAsyncbyusername(txtUsuario.Text);


                if (passDB != null)
                {
                    // Esto elimina comillas extra si la API devuelve "password" con comillas
                    passDB = passDB.Trim('"').Trim();
                }

                if (passDB != null && passDB == passInput)
                {
                    var todos = await API_REST.Instance.GetAllResponsableAsync();
                    SessionManager.UsuarioActual = todos.FirstOrDefault(u => u.usuari == user);

                    DialogResult = true;
                    Close();
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos.", "Error de acceso",
                                    MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                // Esto te dirá finalmente por qué se cierra en el modo Debug
                MessageBox.Show($"Error crítico: {ex.Message}\n{ex.InnerException?.Message}");
            }
        }
    }
}
