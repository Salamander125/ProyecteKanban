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

        private PissarraKanban_MainWindow ConnectorDeFinestraPrincipal;
        private Tiquet instanciaTiquet;

        //Bool per saber si s'ha modificat algun paràmetre.
        private bool descripcio_Alterada = false;
        private bool status_Alterat = false;
        private bool prioritat_Alterada = false;
        private bool responsable_Alterat = false;

        private string originalDescription;
        private Status originalStatus;
        private Prioritat originalPriority;
        //private int originalResponsable

        internal DetallsTascaWindow(Tiquet t, PissarraKanban_MainWindow mainWindow)
        {
            InitializeComponent();
            /// fes que l'atribut d'aquesta classe apunti al parametre rebut.
            instanciaTiquet = t;
            ConnectorDeFinestraPrincipal = mainWindow;
            /// Mostra les dades del tiquet rebut
            CarregarTiquet();
        }


        private void InicialitzarPriorityComboBox(Tiquet tiquet)
        {
            foreach (ComboBoxItem item in PriorityBox.Items)
            {
                if ((Prioritat)item.Tag == instanciaTiquet.PrioritatTiquet)
                {
                    PriorityBox.SelectedItem = item;
                    break;
                }
            }
        }


        private void CarregarTiquet()
        {
            //Aqui es defineixen tots els camps del formulari, amb els valors del tiquet que hem obert.
            //Si hi ha algun camp nul, llavors NO es recupera, o es substitueix per un string.

            idText.Text = instanciaTiquet.ObtenirCodi().ToString(); // ID

            // Primer es guarda als strings original_ i després es mostra. 
            if (instanciaTiquet.Description != null)
                TitolText.Text = instanciaTiquet.ObtenirTitol();
                if (instanciaTiquet.Description != null)
                DescriptionText.Text = originalDescription = instanciaTiquet.ObtenirDescripcio(); // DESCRIPCIÓ
            else
                DescriptionText.Text = "<{Sense Descripció}>";

            if (instanciaTiquet.DataCreacio != null)
                creationDate.SelectedDate = DateTime.Parse(instanciaTiquet.ObtenirDataCreacio()); // DATA CREACIO

            if (instanciaTiquet.DataEstimadaFinalitzacio != null) // DATA ESTIMADA FINALITZACIO
                expectedDate.SelectedDate = DateTime.Parse(instanciaTiquet.ObtenirDataEstimadaFinalitzacio());

            if (instanciaTiquet.Responsable != null)
                ResponsibleText.Text = instanciaTiquet.ObtenirResponsable(); // RESPONSABLE
            else
                ResponsibleText.Text = "<{Responsable Indefinit}>";

            //Tenir seleccionada la prioritat
            foreach (ComboBoxItem item in PriorityBox.Items)
            {
                if ((Prioritat)item.Tag == instanciaTiquet.PrioritatTiquet)
                {
                    PriorityBox.SelectedItem = item;
                    break;
                }
            }

            //Tenir seleccionat l'estat
            foreach (ComboBoxItem item in StatusBox.Items)
            {
                if ((Status)item.Tag == instanciaTiquet.Estat)
                {
                    StatusBox.SelectedItem = item;
                    break;
                }
            }

            //StatusBox.SelectedIndex = instanciaTiquet.ObtenirPrioritat();
            //PriorityBox.SelectedIndex = instanciaTiquet.ObtenirEstat();

            originalStatus = instanciaTiquet.Estat;
            originalPriority = instanciaTiquet.PrioritatTiquet;

            //Switch que depenent de la prioritat, canvia el led de color.
            //Si no té prioritat, es torna Gris.
            switch (instanciaTiquet.PrioritatTiquet)
            {
                case Prioritat.Alta:
                    PriorityBox.Background = Brushes.Tomato;
                    break;

                case Prioritat.Mitja:
                    PriorityBox.Background = Brushes.Gold;
                    break;

                case Prioritat.Baixa:
                    PriorityBox.Background = Brushes.LightGreen;
                    break;
                case Prioritat.SensePrioritat:
                    PriorityBox.Background = Brushes.LightGreen;
                    break;
            }
        }


        private void GuardarCanvisModificats(object sender, RoutedEventArgs e)
        {
            if (DescriptionText.Text != originalDescription)
                instanciaTiquet.CanviarDescripcio(DescriptionText.Text);
            if (PriorityBox.SelectedIndex != (int)originalPriority)
                instanciaTiquet.CanviarPrioritat((Prioritat)PriorityBox.SelectedIndex);
            if (StatusBox.SelectedIndex != (int)originalStatus)
                instanciaTiquet.CanviarEstat((Status)StatusBox.SelectedIndex);
            this.Close();
        }


        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult eleccioMessageBox_Si_No = MessageBox.Show("Estas segur de que vols borrar aquesta tasca?", "Borrar Tasca", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            switch (eleccioMessageBox_Si_No)
            {
                case MessageBoxResult.Yes:
                    ConnectorDeFinestraPrincipal.BorrarTiquet(instanciaTiquet as Tiquet);
                    this.Close();
                    break;
                case MessageBoxResult.No:
                    break;
            }

        }


        private void DescriptionText_TextChanged(object sender, TextChangedEventArgs e)
        {
            descripcio_Alterada = true;
        }
        private void StatusBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            status_Alterat = true;
        }


        // Canviar el color del ComboBox quan es canvia la prioritat
        private void PriorityBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            prioritat_Alterada = true;
            switch ((Prioritat)PriorityBox.SelectedIndex)
            {
                case Prioritat.Alta:
                    PriorityBox.Background = Brushes.Tomato;
                    break;

                case Prioritat.Mitja:
                    PriorityBox.Background = Brushes.Gold;
                    break;

                case Prioritat.Baixa:
                    PriorityBox.Background = Brushes.LightGreen;
                    break;
                case Prioritat.SensePrioritat:
                    PriorityBox.Background = Brushes.LightGray;
                    break;
                default:
                    PriorityBox.Background = Brushes.LightGray;
                    break;
            }
        }



        // Si l'usuari intenta tancar la finestra i te canvis sense guardar...
        /// Quan es crida "this.Close()", tambe es crida aquesta funcio:
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (status_Alterat || responsable_Alterat || descripcio_Alterada)
            {
                //Si descripcio no es la original / Status no és l'original / Prioritat no és la original...
                if ((DescriptionText.Text != originalDescription) || (StatusBox.SelectedIndex != (int)originalStatus) || (PriorityBox.SelectedIndex != (int)originalPriority))
                {
                    MessageBoxResult eleccioMessageBox_Si_No = MessageBox.Show("Has fet canvis. Els vols guardar?", "Canvis sense guardar!", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    switch (eleccioMessageBox_Si_No)
                    {
                        case MessageBoxResult.Yes:
                            //GuardarCanvisModificats();
                            break;
                        case MessageBoxResult.No:
                            break;
                    }
                }
            }


        }

    }
}
