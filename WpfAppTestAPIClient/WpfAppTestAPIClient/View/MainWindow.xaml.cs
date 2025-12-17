using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfAppTestAPIClient.Model;
using WpfAppTestAPIClient.APIClient;
using WpfAppTestAPIClient.View;

namespace WpfAppTestAPIClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UsersApiClient api = new UsersApiClient();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnListBox_Click(object sender, RoutedEventArgs e)
        {
            WindowListBox form = new WindowListBox();
            form.ShowDialog();
        }

        private void btnDataGrid_Click(object sender, RoutedEventArgs e)
        {
            WindowDataGrid form = new WindowDataGrid();
            form.ShowDialog();
        }





        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            Responsable user = await api.GetResponsableAsyncbyid(1);
            List<Responsable> users = await api.GetAllResponsableAsync();
            DateTime birthday = new DateTime(2000, 12, 01);
            user = new Responsable() { usuari = "pepe", contrasenya = "marathom", admin = true 
            };
            await api.AddResponsableAsync(user);
        //    user.id = 2;
        //    user.usuari = "xxx";
        //    await api.UpdateAsync(user);
        //    await api.DeleteAsync(2);
        }
   


    }
}
