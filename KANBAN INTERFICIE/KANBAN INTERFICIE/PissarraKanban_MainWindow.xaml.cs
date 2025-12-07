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
    public partial class PissarraKanban_MainWindow : Window
    {
        public PissarraKanban_MainWindow()
        {
            InitializeComponent();
        }



        private void TiquetButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                Tiquet tiquetQue_sEnvia = btn.DataContext as Tiquet;
                if (tiquetQue_sEnvia != null)
                {
                    new DetallsTascaWindow(tiquetQue_sEnvia).ShowDialog();
                }
            }
        }
        

        private void AfegirTascaButton_Click(object sender, RoutedEventArgs e)
        {
            AfegirTascaWindow wnd = new AfegirTascaWindow(this);
            wnd.ShowDialog();
        }

        /// <summary>
        /// Afegeix un tiquet al panell on pertany (ToDo/InProgress/Finished).
        /// </summary>
        /// <param name="tiquet"></param>
        internal void AfegirTiquet(Tiquet tiquet)
        {
            switch (tiquet.estat)
            {
                case Status.toDo:
                    ListaToDo.Items.Add(tiquet);
                    break;
                case Status.enProgres:
                    InProgressItems.Items.Add(tiquet);
                    break;
                case Status.acabat:
                    DoneItems.Items.Add(tiquet);
                    break;
            }
        }
    }
}