using System.Collections.ObjectModel;
using System.Net.Sockets;
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
        //Llistes de cada columna KANBAN. 
        public ObservableCollection<Tiquet> ColeccioTiquets_ToDo { get; set; }
        public ObservableCollection<Tiquet> ColeccioTiquets_InProgress { get; set; }
        public ObservableCollection<Tiquet> ColeccioTiquets_Finished { get; set; }

        //Variables per el Drag n' Drop
        private Tiquet TiquetArrossegat = null;
        private Point puntInicialArrossegament;
        private const string FormatDadesTiquet = "KANBAN_TICKET";

        private bool _isDragging;
        private Point _startPoint;
        private Tiquet _draggedTiquet;



        public PissarraKanban_MainWindow()
        {
            InitializeComponent();


            ColeccioTiquets_ToDo = new ObservableCollection<Tiquet>();
            ColeccioTiquets_InProgress = new ObservableCollection<Tiquet>();
            ColeccioTiquets_Finished = new ObservableCollection<Tiquet>();

            //Fes que els tiquets es guardin a la coleccio, NO al ItemSource del ListView.
            LlistaToDo.ItemsSource = ColeccioTiquets_ToDo;
            InProgressItems.ItemsSource = ColeccioTiquets_InProgress;
            DoneItems.ItemsSource = ColeccioTiquets_Finished;

        }
        /////////////////////////////////////////////////////////////////////
        private void Card_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _startPoint = e.GetPosition(null);
            _draggedTiquet = (sender as Border)?.DataContext as Tiquet;
            _isDragging = false;
        }

        private void Card_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed || _draggedTiquet == null)
                return;

            Point currentPos = e.GetPosition(null);

            if (!_isDragging &&
                (Math.Abs(currentPos.X - _startPoint.X) > SystemParameters.MinimumHorizontalDragDistance ||
                 Math.Abs(currentPos.Y - _startPoint.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                _isDragging = true;

                DragDrop.DoDragDrop(
                    sender as DependencyObject,
                    new DataObject(FormatDadesTiquet, _draggedTiquet),
                    DragDropEffects.Move
                );

            }
        }
        private void Card_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_isDragging) return;

            var tiquet = (sender as Border)?.DataContext as Tiquet;
            if (tiquet != null)
            {
                new DetallsTascaWindow(tiquet, this).ShowDialog();
            }
            RefreshKanban();
        }



        // Obrir panell afegir tasca
        internal void AfegirTascaButton_Click(object sender, RoutedEventArgs e)
        {
            AfegirTascaWindow wnd = new AfegirTascaWindow(this);
            wnd.ShowDialog();
            RefreshKanban();
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
                    ColeccioTiquets_ToDo.Add(tiquet);
                    break;
                case Status.enProgres:
                    ColeccioTiquets_InProgress.Add(tiquet);
                    break;
                case Status.acabat:
                    ColeccioTiquets_Finished.Add(tiquet);
                    break;
            }
            RefreshKanban();
        }

        internal void BorrarTiquet(Tiquet tiquet)
        {
            switch (tiquet.estat)
            {
                case Status.toDo:
                    ColeccioTiquets_ToDo.Remove(tiquet);
                    break;
                case Status.enProgres:
                    ColeccioTiquets_InProgress.Remove(tiquet);
                    break;
                case Status.acabat:
                    ColeccioTiquets_Finished.Remove(tiquet);
                    break;
            }
            RefreshKanban();
        }
        internal void GestioResponsablesButton_Click(object sender, RoutedEventArgs e)
        {
            GestioResponsables wnd = new GestioResponsables();
            wnd.ShowDialog();
            RefreshKanban();
        }

        /// <summary>
        /// Actualitza la pissarra kanban.
        /// </summary>
        internal void RefreshKanban()
        {
            LlistaToDo.Items.Refresh();
            InProgressItems.Items.Refresh();
            DoneItems.Items.Refresh();
        }


        /////////////////////
        /// Drag And Drop \\\

     


        // 3. Valida el destino (Cursor de feedback)
        internal void ListView_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(FormatDadesTiquet))
            {
                e.Effects = DragDropEffects.Move; // Permitir soltar (Mostrar cursor de movimiento)
            }
            else
            {
                e.Effects = DragDropEffects.None; // Prohibir soltar
            }
            e.Handled = true;
        }

        // 4. Busca la colección de origen del Tiquet que se está arrastrando
        private ObservableCollection<Tiquet> BuscarListaOrigen(Tiquet el_tiquet)
        {
            if (ColeccioTiquets_ToDo.Contains(el_tiquet))
            {
                return ColeccioTiquets_ToDo;
            }
            if (ColeccioTiquets_InProgress.Contains(el_tiquet))
            {
                return ColeccioTiquets_InProgress;
            }
            if (ColeccioTiquets_Finished.Contains(el_tiquet))
            {
                return ColeccioTiquets_Finished;
            }
            return null;
        }

        internal void HandleDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(FormatDadesTiquet))
            {
                // Obtiene el Tiquet arrastrado
                Tiquet TiquetSoltado = e.Data.GetData(FormatDadesTiquet) as Tiquet;
                if (TiquetSoltado == null) return;

                // Identifica la lista de destino
                ListView listaDestinoView = sender as ListView;
                ObservableCollection<Tiquet> listaDestino = listaDestinoView?.ItemsSource as ObservableCollection<Tiquet>;
                if (listaDestino == null) return;

                // Identifica la lista de origen
                ObservableCollection<Tiquet> listaOrigen = BuscarListaOrigen(TiquetSoltado);
                if (listaOrigen == null || listaOrigen == listaDestino) return;

                // Quitar del origen y añadir al destino
                listaOrigen.Remove(TiquetSoltado);
                listaDestino.Add(TiquetSoltado);

                // 🔹 Actualiza l'estat segons la columna de destinació
                if (listaDestino == ColeccioTiquets_ToDo) TiquetSoltado.CanviarEstat(Status.toDo);
                else if (listaDestino == ColeccioTiquets_InProgress) TiquetSoltado.CanviarEstat(Status.enProgres);
                else if (listaDestino == ColeccioTiquets_Finished) TiquetSoltado.CanviarEstat(Status.acabat);

                e.Handled = true;
            }
            RefreshKanban();
        }



    }
}