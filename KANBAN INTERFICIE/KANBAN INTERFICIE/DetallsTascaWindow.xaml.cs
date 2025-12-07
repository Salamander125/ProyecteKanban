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

        private Tiquet instanciaTiquet;

        internal DetallsTascaWindow(Tiquet t) //La classe rep un parametre tiquet des de la MainWindow.
        {
            InitializeComponent(); // Executa la finestra.
            instanciaTiquet = t; 
            CarregarTiquet(); // Carrega el tiquet a l'iniciar la finestra.
        }


        private void CarregarTiquet()
        {
            //Aqui es defineixen tots els camps del formulari, amb els valors del tiquet que hem obert.
            //Si hi ha algun camp nul, llavors NO es recupera, o es substitueix per un string.

            idText.Text = instanciaTiquet.codi_id.ToString(); // ID


            if (instanciaTiquet.Description != null)
                DescriptionText.Text = instanciaTiquet.Description; // DESCRIPCIÓ
            else
                DescriptionText.Text = "<{Sense Descripció}>";

            if (instanciaTiquet.DataCreacio != null)
                creationDate.SelectedDate = DateTime.Parse(instanciaTiquet.DataCreacio); // DATA CREACIO

            if (instanciaTiquet.DataEstimadaFinalitzacio != null) // DATA ESTIMADA FINALITZACIO
                expectedDate.SelectedDate = DateTime.Parse(instanciaTiquet.DataEstimadaFinalitzacio);

            if (instanciaTiquet.Responsable != null)
                ResponsibleText.Text = instanciaTiquet.Responsable; // RESPONSABLE
            else
                ResponsibleText.Text = "<{Responsable Indefinit}>";

                PriorityLabel.Text = instanciaTiquet.PrioritatTiquet.ToString(); //PRIORITAT (TEXT)


            //Switch que depenent de la prioritat, canvia el led de color.
            //Si no té prioritat, es torna Gris.
            switch (instanciaTiquet.PrioritatTiquet)
            {
                case Prioritat.Alta:
                    PriorityLED.Fill = Brushes.Red;
                    break;

                case Prioritat.Mitja:
                    PriorityLED.Fill = Brushes.Orange;
                    break;

                case Prioritat.Baixa:
                    PriorityLED.Fill = Brushes.Green;
                    break;

                default:
                    PriorityLED.Fill = Brushes.Gray;
                    break;
            }
        }


        //Debug
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
