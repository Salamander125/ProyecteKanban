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

namespace KANBAN_INTERFICIE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) // Mostra el text sencer quan es clica.
        {
            if (sender is Button btn)
            {
                MessageBox.Show($"Button pressed: {btn.Content}");
            }
        }


        private void Button_MouseMove(object sender, MouseEventArgs  e) //Mostra la prioritat segons el color del botó, quan el mouse es a sobre del botó
        {
            var btn = (Button)sender;

            // The hex color you want to detect
            Color targetColor = (Color)ColorConverter.ConvertFromString("#7C2BE3E3"); //COLOR EN HEXADECIMAL

            // Check mouse drag + color
            if (e.LeftButton == MouseButtonState.Pressed &&
                btn.Background is SolidColorBrush brush &&
                brush.Color == targetColor)
            {
                btn.ToolTip = "You are dragging over a red button!";
                ToolTipService.SetIsEnabled(btn, true);
            }
        }

    }
}