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



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                Tiquet t = btn.DataContext as Tiquet;
                if (t != null)
                {
                    new DetallsTascaWindow(t).ShowDialog();
                }
            }
        }
        

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AfegirTascaWindow wnd = new AfegirTascaWindow(this);
            wnd.ShowDialog();
        }

        internal void AfegirTiquet(Tiquet t)
        {
            switch (t.estat)
            {
                case Status.toDo:
                    ListaToDo.Items.Add(t);
                    break;
                case Status.enProgres:
                    InProgressItems.Items.Add(t);
                    break;
                case Status.acabat:
                    DoneItems.Items.Add(t);
                    break;
            }
        }
    }
}