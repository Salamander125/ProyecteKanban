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

        public object ConnectorFinestraPrincipal { get; private set; }

        public AfegirTascaWindow(PissarraKanban_MainWindow instanciaMainWindow)
        {
            InitializeComponent();
            ConnectorDeFinestraPrincipal = instanciaMainWindow;
            DescriptionBox.Focus(); //inmediatament permet escriure sense fer clic.
        }


        private async Task SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Estructura que valida que no hi hagin camps Buits

            if (string.IsNullOrWhiteSpace(DescriptionBox.Text) || string.IsNullOrWhiteSpace(TitolBox.Text))
            {
                MessageBox.Show("HAS DE POSAR LA DESCRIPCIÓ I EL TITOL ABANS DE CREAR EL TIQUET", "Omplir la descripció/titol és obligatori", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            /// Comprova que no hi hagi PRIORITAT/STATUS Null. omple automaticament lo que estigui buit.
            else if (StatusBox.SelectedIndex == -1 || PriorityBox.SelectedIndex == -1) //Si no hi ha res seleccionat...
            {
                MessageBoxResult eleccioMessageBox_Si_No = MessageBox.Show("No has establert un d'aquests dos camps: PRIORITAT/STATUS. ");
            }
            else
            {
                Tasca tiquet_que_sEnvia = new Tasca(
                    responsable: (long?)(ResponsibleBox.SelectedItem as ComboBoxItem)?.Tag,
                    titol: TitolBox.Text,
                    descripcio: DescriptionBox.Text,
                    // IMPORTANTE: Usa el Tag si ahora usas números en el XAML, no el Index
                    estat: Convert.ToInt32((StatusBox.SelectedItem as ComboBoxItem)?.Tag ?? "1"),
                    data_creacio: DateTime.Now,
                    data_finalitzacio: DataFinalitzacioPicker.SelectedDate?.Date ?? DateTime.Today,
                    prioritat: Convert.ToInt32((PriorityBox.SelectedItem as ComboBoxItem)?.Tag ?? "0")
                );

                // AQUÍ ESTÁ EL TRUCO: Esperar a que la API termine antes de cerrar
                await ConnectorFinestraPrincipal.AfegirTiquet(tiquet_que_sEnvia);

                this.Close(); // Ahora sí se cierra solo cuando la API confirma recepción
            }
        }

        //TANCA LA FINESTRA AL CLICAR CANCELAR:
        private void CancelarOperacio(object sender, RoutedEventArgs e) => this.Close();

    }
}