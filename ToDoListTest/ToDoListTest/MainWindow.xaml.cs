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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ToDoListTest
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public void AddTask()
        {
            ListaTIckets.Items.Add(new ButtonData
            {
                Title = IntroTitle.Text,
                Description = IntroDescription.Text
            });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddTask();
        }

        private void AccesoNuevaVentana(object sender, RoutedEventArgs e)
        {
            new Eliminar().ShowDialog();
            ButtonData button = sender as ButtonData;
        }
    }
    public class ButtonData
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Codigo { get; set; }
    }
}