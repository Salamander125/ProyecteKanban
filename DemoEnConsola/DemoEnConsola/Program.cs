using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoEnConsola
{
    using System;
    using System.Collections.Generic;

    public class Program
    {
        static List<Ticket> tickets = new List<Ticket>();
        static List<Responsable> responsables = new List<Responsable>();

        static void Main(string[] args)
        {
            int opcion = 0;

            do
            {
                Console.WriteLine("\n=== MAIN MENU ===");
                Console.WriteLine("1 - Listar");
                Console.WriteLine("2 - Seleccionar");
                Console.WriteLine("3 - Añadir Tarea");
                Console.WriteLine("4 - Añadir Responsable");
                Console.WriteLine("5 - Salir");
                Console.Write("Seleccione una opción: ");

                opcion = Convert.ToInt32(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        ListarTickets();
                        break;

                    case 2:
                        SeleccionarTicket();
                        break;

                    case 3:
                        AñadirTarea();
                        break;

                    case 4:
                        MenuResponsable();
                        break;

                    case 5:
                        Console.WriteLine("Saliendo...");
                        break;

                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }

            } while (opcion != 5);
        }

        // ====================== MENU TICKET ======================

        static void SeleccionarTicket()
        {
            Console.Write("Ingrese código de ticket: ");
            int codigo = int.Parse(Console.ReadLine());

            Ticket ticket = tickets.Find(t => t.ObtenerCodigo() == codigo);

            if (ticket == null)
            {
                Console.WriteLine("Ticket no encontrado.");
                return;
            }

            int opcion;
            do
            {
                Console.WriteLine("\n=== MENU TAREA ===");
                Console.WriteLine("1 - Cambiar estado");
                Console.WriteLine("2 - Cambiar prioridad");
                Console.WriteLine("3 - Seleccionar responsable");
                Console.WriteLine("4 - Volver");
                opcion = Convert.ToInt32(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        Console.WriteLine("Ingrese nuevo estado (0=ToDo, 1=Doing, 2=Done): ");
                        int est = int.Parse(Console.ReadLine());
                        ticket.CambiarEstado(est);
                        break;

                    case 2:
                        Console.Write("Nueva prioridad: ");
                        ticket.CambiarPrioridad(int.Parse(Console.ReadLine()));
                        break;

                    case 3:
                        ListarResponsables();
                        Console.Write("Ingrese código de responsable: ");
                        int codResp = int.Parse(Console.ReadLine());
                        Responsable resp = responsables.Find(r => r.ObtenerCodigo() == codResp);
                        if (resp != null) ticket.CambiarResponsable(resp.ObtenerNombre());
                        break;
                }

            } while (opcion != 4);
        }

        // ====================== MENU RESPONSABLES ======================

        static void MenuResponsable()
        {
            int opcion;
            do
            {
                Console.WriteLine("\n=== MENU RESPONSABLE ===");
                Console.WriteLine("1 - Añadir responsable");
                Console.WriteLine("2 - Eliminar responsable");
                Console.WriteLine("3 - Listar responsables");
                Console.WriteLine("4 - Volver");
                Console.Write("Opción: ");
                opcion = Convert.ToInt32(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        AñadirResponsable();
                        break;
                    case 2:
                        EliminarResponsable();
                        break;
                    case 3:
                        ListarResponsables();
                        break;
                }

            } while (opcion != 4);
        }

        // ====================== MÉTODOS AUXILIARES ======================

        static void ListarTickets()
        {
            foreach (Ticket t in tickets)
                Console.WriteLine($"{t.ObtenerCodigo()} - {t.ObtenerDescripcion()} - {t.ObtenerEstado()}");
        }

        static void AñadirTarea()
        {
            Console.Write("Código: ");
            int codigo = int.Parse(Console.ReadLine());
            Console.Write("Responsable: ");
            string resp = Console.ReadLine();
            Console.Write("Descripción: ");
            string desc = Console.ReadLine();

            Ticket nuevo = new Ticket(codigo, resp, desc, Estado.ToDo, "Hoy", "Sin fecha", 1);
            tickets.Add(nuevo);
        }

        static void AñadirResponsable()
        {
            Console.Write("Código: ");
            int cod = int.Parse(Console.ReadLine());
            Console.Write("Nombre: ");
            string nombre = Console.ReadLine();
            Console.Write("Apellidos: ");
            string apellidos = Console.ReadLine();

            responsables.Add(new Responsable(cod, nombre, apellidos));
        }

        static void EliminarResponsable()
        {
            Console.Write("Código a eliminar: ");
            int cod = int.Parse(Console.ReadLine());
            responsables.RemoveAll(r => r.ObtenerCodigo() == cod);
        }

        static void ListarResponsables()
        {
            foreach (Responsable r in responsables)
                Console.WriteLine($"{r.ObtenerCodigo()} - {r.ObtenerNombre()} {r.ObtenerApellidos()}");
        }
    }

}
