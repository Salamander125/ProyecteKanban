using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ListBoxSESSIO6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            List<Poblacions> llistaPob = new();
            llistaPob.Add(new Poblacions()
            {
                poblacio1 = "Girona",
                temp1 = 12,
                poblacio2 = "Barcelona",
                temp2 = 29,
                DiferenciaTemp = 29-12
            });
            llistaPob.Add(new Poblacions()
            {
                poblacio1 = "Lleida",
                temp1 = 16,
                poblacio2 = "Andorra",
                temp2 = 4,
                DiferenciaTemp = 16-4
            });
            llistaPob.Add(new Poblacions()
            {
                poblacio1 = "Valencia",
                temp1 = 28,
                poblacio2 = "Alacant",
                temp2 = 18,
                DiferenciaTemp = 28-18
            });
            llistaPob.Add(new Poblacions()
            {
                poblacio1 = "Cordoba",
                temp1 = 35,
                poblacio2 = "Paris",
                temp2 = 10,
                DiferenciaTemp = 35-10
            });
            llistaPoblacions.ItemsSource = llistaPob;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (llistaPoblacions.SelectedItem != null)  //EVITA QUE PETI EL PROGRAMA
            {
                MessageBox.Show((llistaPoblacions.SelectedItem as Poblacions).poblacio1 + " " +
                    (llistaPoblacions.SelectedItem as Poblacions).temp1 + " ºC,\n" +
                    (llistaPoblacions.SelectedItem as Poblacions).poblacio2 + " " +
                    (llistaPoblacions.SelectedItem as Poblacions).temp2 + " ºC"
                    );
            }
            else
                MessageBox.Show("Primer has de seleccionar un element de la LISTBOX", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);

        }

        private void TextBlock_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show((llistaPoblacions.SelectedItem as Poblacions).poblacio1 + " " +
                    (llistaPoblacions.SelectedItem as Poblacions).temp1 + " ºC,\n" +
                    (llistaPoblacions.SelectedItem as Poblacions).poblacio2 + " " +
                    (llistaPoblacions.SelectedItem as Poblacions).temp2 + " ºC"
                    );
        }
    }

    public class Poblacions
    {
        public string poblacio1 {  get; set; }
        public string poblacio2 { get; set; }
        public int temp1 { get; set; }
        public int temp2 { get; set; }
        public int DiferenciaTemp { get; set; }
    }
}