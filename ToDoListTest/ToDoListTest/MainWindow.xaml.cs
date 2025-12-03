using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ToDoListTest
{
    public class Ticket
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Codigo { get; set; }
    }

    public partial class MainWindow : Window
    {
        public ObservableCollection<Ticket> TicketsPendientes { get; set; }
        public ObservableCollection<Ticket> TicketsEnCurso { get; set; }
        public ObservableCollection<Ticket> TicketsFinalizados { get; set; }

        private Ticket ticketArrastrado = null;
        private Point puntoInicialArrastre;
        private const string FormatoDatosTicket = "TicketDataFormat";

        public MainWindow()
        {
            InitializeComponent();

            TicketsPendientes = new ObservableCollection<Ticket>();
            TicketsEnCurso = new ObservableCollection<Ticket>();
            TicketsFinalizados = new ObservableCollection<Ticket>();

            ListaTickets.ItemsSource = TicketsPendientes;
            ListaEnCurso.ItemsSource = TicketsEnCurso;
            ListaFinalizados.ItemsSource = TicketsFinalizados;
        }

        public void AddTask()
        {
            if (IntroTitle != null && IntroTitle.Text != "")
            {
                TicketsPendientes.Add(new Ticket
                {
                    Title = IntroTitle.Text,
                    Description = IntroDescription.Text,
                });
            }
            else
            {
                MessageBox.Show("Error: El campo de titulo no puede estar vacio");
            }
            // Limpiar los campos de entrada después de agregar
            IntroTitle.Text = "Nuevo";
            IntroDescription.Text = "";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddTask();
        }

        private void AccesoNuevaVentana(object sender, MouseButtonEventArgs e)
        {
            new Eliminar().ShowDialog();
        }

        // Funcion para encontrar el padre del Ticket arrastrado
        private static T BuscarContenedor<T>(DependencyObject actual) where T : DependencyObject
        {
            do
            {
                if (actual is T contenedor) return contenedor;
                actual = VisualTreeHelper.GetParent(actual);
            }
            while (actual != null);
            return null;
        }

        // 1. Captura la posición inicial del ratón
        private void ListView_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            puntoInicialArrastre = e.GetPosition(null);
        }

        // 2. Inicia la operación de arrastre si hay movimiento
        private void ListView_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point posicionActual = e.GetPosition(null);
                Vector diferencia = puntoInicialArrastre - posicionActual;

                // Deteccion de movimiento suficiente para iniciar arrastre
                if (Math.Abs(diferencia.X) > SystemParameters.MinimumHorizontalDragDistance ||
                    Math.Abs(diferencia.Y) > SystemParameters.MinimumVerticalDragDistance)
                {
                    ListView lista = sender as ListView;
                    ListViewItem itemContenedor = BuscarContenedor<ListViewItem>((DependencyObject)e.OriginalSource);

                    if (itemContenedor != null)
                    {
                        ticketArrastrado = (Ticket)lista.ItemContainerGenerator.ItemFromContainer(itemContenedor);

                        if (ticketArrastrado != null)
                        {
                            // Iniciar el Drag and Drop con el objeto de datos
                            DataObject objetoArrastre = new DataObject(FormatoDatosTicket, ticketArrastrado);
                            DragDrop.DoDragDrop(itemContenedor, objetoArrastre, DragDropEffects.Move);
                        }
                    }
                }
            }
        }

        // 3. Valida el destino (Cursor de feedback)
        private void ListView_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(FormatoDatosTicket))
            {
                e.Effects = DragDropEffects.Move; // Permitir soltar (Mostrar cursor de movimiento)
            }
            else
            {
                e.Effects = DragDropEffects.None; // Prohibir soltar
            }
            e.Handled = true;
        }

        // 4. Busca la colección de origen del ticket que se está arrastrando
        private ObservableCollection<Ticket> BuscarListaOrigen(Ticket item)
        {
            if (TicketsPendientes.Contains(item))
            {
                return TicketsPendientes;
            }
            if (TicketsEnCurso.Contains(item))
            {
                return TicketsEnCurso;
            }
            if (TicketsFinalizados.Contains(item))
            {
                return TicketsFinalizados;
            }
            return null;
        }

        private void HandleDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(FormatoDatosTicket))
            {
                // Obtiene el ticket arrastrado
                Ticket ticketSoltado = e.Data.GetData(FormatoDatosTicket) as Ticket;

                if (ticketSoltado == null) return;

                // Identifica la lista de destino
                ListView listaDestinoView = sender as ListView;
                ObservableCollection<Ticket> listaDestino = listaDestinoView?.ItemsSource as ObservableCollection<Ticket>;

                if (listaDestino == null) return;

                // Identifica la lista de origen
                ObservableCollection<Ticket> listaOrigen = BuscarListaOrigen(ticketSoltado);

                if (listaOrigen == null || listaOrigen == listaDestino) return; // Ya está allí o no existe

                // Quitar del origen y añadir al destino
                listaOrigen.Remove(ticketSoltado);
                listaDestino.Add(ticketSoltado);

                e.Handled = true;
            }
        }
    }
}