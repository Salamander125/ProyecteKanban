using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ToDoListTest.Model;

namespace ToDoListTest
{
    public partial class MainWindow : Window
    {
        public Task<List<Model.Tasca>> AllTickets { get; set; }
        public ObservableCollection<Tasca> TicketsPendientes { get; set; }
        public ObservableCollection<Tasca> TicketsEnCurso { get; set; }
        public ObservableCollection<Tasca> TicketsFinalizados { get; set; }
        public ObservableCollection<Responsable> ListaResp { get; set; }

        private Tasca ticketArrastrado = null;
        private Point puntoInicialArrastre;
        private const string FormatoDatosTicket = "TicketDataFormat";

        public MainWindow()
        {
            InitializeComponent();
            VerificarPermisos();
            TicketsPendientes = new ObservableCollection<Tasca>();
            TicketsEnCurso = new ObservableCollection<Tasca>();
            TicketsFinalizados = new ObservableCollection<Tasca>();
            ListaResp = new ObservableCollection<Responsable>();

            ListaTickets.ItemsSource = TicketsPendientes;
            ListaEnCurso.ItemsSource = TicketsEnCurso;
            ListaFinalizados.ItemsSource = TicketsFinalizados;
            ListaCompañeros.ItemsSource = ListaResp;

            CargarYClasificarTickets();
            Closed += (s, e) => Environment.Exit(0);
        }

        private void VerificarPermisos()
        {
            // Si el usuario NO es administrador, ocultamos o deshabilitamos el botón
            if (SessionManager.UsuarioActual != null && !SessionManager.UsuarioActual.admin)
            {
                // Suponiendo que tu botón se llama btnAddResponsable en el XAML
                // btnAddResponsable.Visibility = Visibility.Collapsed; 

                // O si prefieres dejarlo visible pero que no haga nada:
                RespBtn.IsEnabled = false;
                EliminarUsuariobtn.IsEnabled = false;
            }
        }

        private async void CargarYClasificarTickets()
        {
            var allTickets = await API_REST.Instance.GetAllTascaAsync();
            var allusers = await API_REST.Instance.GetAllResponsableAsync();
            TicketsPendientes.Clear();
            TicketsEnCurso.Clear();
            TicketsFinalizados.Clear();
            ListaResp.Clear();

            foreach (Tasca t in allTickets)
            {
                switch (t.Estat)
                {
                    case 0: TicketsPendientes.Add(t); break;
                    case 1: TicketsEnCurso.Add(t); break;
                    case 2: TicketsFinalizados.Add(t); break;
                }
            }

            foreach (Responsable r in allusers)
            {
                ListaResp.Add(r);
            }
        }

        private void AccesoNuevaVentana(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement elemento = sender as FrameworkElement;

            if (elemento?.DataContext is Tasca tascaSeleccionada)
            {
                Eliminar ventanaEdicion = new Eliminar(tascaSeleccionada);
                ventanaEdicion.Owner = this;

                bool? resultado = ventanaEdicion.ShowDialog();

                if (resultado == true)
                {
                    CargarYClasificarTickets();
                }
            }
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
                        ticketArrastrado = (Tasca)lista.ItemContainerGenerator.ItemFromContainer(itemContenedor);

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
        private ObservableCollection<Tasca> BuscarListaOrigen(Tasca item)
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

        private async void HandleDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(FormatoDatosTicket))
            {
                // Obtiene el ticket arrastrado
                Tasca ticketSoltado = e.Data.GetData(FormatoDatosTicket) as Tasca;

                if (ticketSoltado == null) return;

                // Identifica la lista de destino
                ListView listaDestinoView = sender as ListView;
                ObservableCollection<Tasca> listaDestino = listaDestinoView?.ItemsSource as ObservableCollection<Tasca>;

                if (listaDestino == null) return;

                // Identifica la lista de origen
                ObservableCollection<Tasca> listaOrigen = BuscarListaOrigen(ticketSoltado);

                if (listaOrigen == null || listaOrigen == listaDestino) return; // Ya está allí o no existe

                // Quitar del origen y añadir al destino
                listaOrigen.Remove(ticketSoltado);
                listaDestino.Add(ticketSoltado);

                if (BuscarListaOrigen(ticketSoltado) == TicketsPendientes)
                {
                    await API_REST.Instance.UpdateEstatTascaAsync(ticketSoltado.Codi, 0);
                }
                if (BuscarListaOrigen(ticketSoltado) == TicketsEnCurso)
                {
                    await API_REST.Instance.UpdateEstatTascaAsync(ticketSoltado.Codi, 1);
                }
                if (BuscarListaOrigen(ticketSoltado) == TicketsFinalizados)
                {
                    await API_REST.Instance.UpdateEstatTascaAsync(ticketSoltado.Codi, 2);
                }

                CargarYClasificarTickets();

                e.Handled = true;
            }
        }
        private async void Responsables(object sender, RoutedEventArgs e)
        {
            NuevoResponsable ventana = new NuevoResponsable();
            ventana.Owner = this;

            if (ventana.ShowDialog() == true)
            {
                CargarYClasificarTickets();
            }
        }

        private void NuevaTasca(object sender, RoutedEventArgs e)
        {
            NuevaTicket ventanaNuevo = new NuevaTicket();
            ventanaNuevo.Owner = this;

            if (ventanaNuevo.ShowDialog() == true)
            {
                CargarYClasificarTickets();
            }
        }

        private async void EliminarUsuario(object sender, RoutedEventArgs e)
        {
            if (ListaCompañeros.SelectedItem == null)
            {
                MessageBox.Show("Por favor, selecciona un usuario para eliminar.");
            }
            else
            {
                await API_REST.Instance.DeleteResponsableAsync((int)ListaCompañeros.SelectedValue);
            }
            CargarYClasificarTickets();
        }
    }
}