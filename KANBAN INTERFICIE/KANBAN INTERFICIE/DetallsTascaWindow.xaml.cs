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
        private Tasca instanciaTiquet;

        //Bool per saber si s'ha modificat algun paràmetre.
        private bool descripcio_Alterada = false;
        private bool status_Alterat = false;
        private bool prioritat_Alterada = false;
        private bool responsable_Alterat = false;

        private string originalDescription;
        private int originalStatus;
        private int originalPriority;
        //private int originalResponsable

        internal DetallsTascaWindow(Tasca t, PissarraKanban_MainWindow mainWindow)
        {
            InitializeComponent();
            /// fes que l'atribut d'aquesta classe apunti al parametre rebut.
            instanciaTiquet = t;
            ConnectorDeFinestraPrincipal = mainWindow;
            /// Mostra les dades del tiquet rebut
            CarregarTiquet();
        }

        private void CarregarTiquet()
        {
            //Aqui es defineixen tots els camps del formulari, amb els valors del tiquet que hem obert.
            //Si hi ha algun camp nul, llavors NO es recupera, o es substitueix per un string.

            idText.Text = instanciaTiquet.ObtenirCodi().ToString(); // ID

            // Primer es guarda als strings original_ i després es mostra. 
            if (instanciaTiquet.descripcio != null)
                TitolText.Text = instanciaTiquet.ObtenirTitol();
                if (instanciaTiquet.descripcio != null)
                DescriptionText.Text = originalDescription = instanciaTiquet.ObtenirDescripcio(); // DESCRIPCIÓ
            else
                DescriptionText.Text = "<{Sense Descripció}>";

                creationDate.SelectedDate = instanciaTiquet.ObtenirDataCreacio(); // DATA CREACIO
                expectedDate.SelectedDate = instanciaTiquet.ObtenirDataEstimadaFinalitzacio();

            if (instanciaTiquet.codi_responsable != null)
                ResponsibleText.Text = instanciaTiquet.ObtenirResponsable().ToString(); // RESPONSABLE
            else
                ResponsibleText.Text = "<{Responsable Indefinit}>";
        }


        private void GuardarCanvisModificats(object sender, RoutedEventArgs e)
        {
            if (DescriptionText.Text != originalDescription)
                instanciaTiquet.CanviarDescripcio(DescriptionText.Text);
            if (PriorityBox.SelectedIndex != originalPriority)
                instanciaTiquet.CanviarPrioritat(PriorityBox.SelectedIndex);
            if (StatusBox.SelectedIndex != originalStatus)
                instanciaTiquet.CanviarEstat(StatusBox.SelectedIndex);
            Close();
        }


        private async Task DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult eleccioMessageBox_Si_No = MessageBox.Show("Estas segur de que vols borrar aquesta tasca?", "Borrar Tasca", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            switch (eleccioMessageBox_Si_No)
            {
                case MessageBoxResult.Yes:
                    ConnectorDeFinestraPrincipal.BorrarTiquet(instanciaTiquet);
                    await API_REST.Instance.DeleteTascaAsync(instanciaTiquet.ObtenirCodi());
                    Close();
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
