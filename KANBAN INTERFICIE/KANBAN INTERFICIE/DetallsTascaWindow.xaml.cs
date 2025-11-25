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

namespace KANBAN_INTERFICIE
{
    /// <summary>
    /// Interaction logic for DetallsTascaWindow.xaml
    /// </summary>
    public partial class DetallsTascaWindow : Window
    {

        public DetallsTascaWindow()
        {
            InitializeComponent();
        }
        private void PlaceHolders_DeProva(object sender, RoutedEventArgs e)
        {
            string prioritat = "Alta";
            idText.Text = "111";
            DescriptionText.Text = "Description";
            creationDate.SelectedDate = DateTime.Now;
            //DateTime.Parse("2025-05-28");
            expectedDate.SelectedDate = DateTime.ParseExact("2026-01-02", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            ResponsibleText.Text = "John Lemmon";
            
            PriorityLabel.Text = prioritat;
            switch (PriorityLabel.Text)
            {
                case "Alta":
                    PriorityLED.Fill = Brushes.Red;
                    break;

                case "Mitjana":
                    PriorityLED.Fill = Brushes.Orange;
                    break;

                case "Baixa":
                    PriorityLED.Fill = Brushes.Green;
                    break;

                default:
                    PriorityLED.Fill = Brushes.Gray;
                    break;
            }



        }

    }
}
