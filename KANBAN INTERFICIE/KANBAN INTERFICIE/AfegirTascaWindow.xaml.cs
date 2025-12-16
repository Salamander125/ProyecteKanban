using System.Windows;
using System.Windows.Controls;

namespace KANBAN_INTERFICIE
{
    /// <summary>
    /// Interaction logic for AfegirTascaWindow.xaml
    /// </summary>
    public partial class AfegirTascaWindow : Window
    {
        private PissarraKanban_MainWindow ConnectorDeFinestraPrincipal;

        public AfegirTascaWindow(PissarraKanban_MainWindow instanciaMainWindow)
        {
            InitializeComponent();
            ConnectorDeFinestraPrincipal = instanciaMainWindow;
            DescriptionBox.Focus(); //inmediatament permet escriure sense fer clic.
        }


        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            int nouCodi = new Random().Next(1000, 9999); // Genera un ID aleatori.

            // Estructura que valida que no hi hagin camps Buits

            if (string.IsNullOrWhiteSpace(DescriptionBox.Text) || string.IsNullOrWhiteSpace(TitolBox.Text))
            {
                MessageBox.Show("HAS DE POSAR LA DESCRIPCIÓ I EL TITOL ABANS DE CREAR EL TIQUET", "Omplir la descripció/titol és obligatori", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            /// Comprova que no hi hagi PRIORITAT/STATUS Null. omple automaticament lo que estigui buit.
            else if (StatusBox.SelectedIndex == -1 || PriorityBox.SelectedIndex == -1) //Si no hi ha res seleccionat...
            {
                MessageBoxResult eleccioMessageBox_Si_No = MessageBox.Show("No has establert un d'aquests dos camps: PRIORITAT/STATUS. " +
                    "Si vols puc definir el camp que estigui buit per tu", "Pregunta", MessageBoxButton.YesNo, MessageBoxImage.Question);

                switch (eleccioMessageBox_Si_No)
                {
                    case MessageBoxResult.Yes: //Fes que Sigui status ToDo
                        if (StatusBox.SelectedIndex == -1)
                        { StatusBox.SelectedIndex = 0; }
                        if (PriorityBox.SelectedIndex == -1)
                        { PriorityBox.SelectedIndex = 1; }
                        //DESPRES GUARDA I TANCA LA FINESTRA AUTOMATICAMENT:
                        {
                            Tiquet tiquet_que_sEnvia = new Tiquet(
                                codi: nouCodi,
                                responsable: (ResponsibleBox.SelectedItem as ComboBoxItem)?.Content.ToString(),
                                titol: TitolBox.Text,
                                descripcio: DescriptionBox.Text,
                                estat: (Status)StatusBox.SelectedIndex,
                                dataCreacio: DateTime.Now.ToString("dd/MM/yyyy"),
                                dataEstimada_Finalitzacio: DataFinalitzacioPicker.SelectedDate?.ToString("dd/MM/yyyy"),
                                prioritat: (Prioritat)PriorityBox.SelectedIndex);

                            ConnectorDeFinestraPrincipal.AfegirTiquet(tiquet_que_sEnvia);
                            this.Close();
                        }
                        break;
                    case MessageBoxResult.No:
                        MessageBox.Show("Pues omple-ho tot, troç de suroooo", "No vols?", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                }
            }
            else
            {

                Tiquet tiquet_que_sEnvia = new Tiquet(
                    codi: nouCodi,
                    responsable: (ResponsibleBox.SelectedItem as ComboBoxItem)?.Content.ToString(),
                    titol: TitolBox.Text,
                    descripcio: DescriptionBox.Text,
                    estat: (Status)StatusBox.SelectedIndex,
                    dataCreacio: DateTime.Now.ToString("dd/MM/yyyy"),
                    dataEstimada_Finalitzacio: DataFinalitzacioPicker.SelectedDate?.ToString("dd/MM/yyyy"),
                    prioritat: (Prioritat)PriorityBox.SelectedIndex
                );

                ConnectorDeFinestraPrincipal.AfegirTiquet(tiquet_que_sEnvia);
                this.Close();
            }
        }

        //TANCA LA FINESTRA AL CLICAR CANCELAR:
        private void CancelarOperacio(object sender, RoutedEventArgs e) => this.Close();

    }
}