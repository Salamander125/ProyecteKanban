using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KANBAN_INTERFICIE.Model;

namespace KANBAN_INTERFICIE
{
    public class ControlDeDatos
    {
        static public ControlDeDatos Instance = new ControlDeDatos();

        public async Task NuevaTasca(Tasca tasca)
        {
            if (tasca == null)
            {
                throw new ArgumentNullException(nameof(tasca), "La tasca no pot ser nul·la.");
            }

            if (string.IsNullOrWhiteSpace(tasca.titol) || string.IsNullOrWhiteSpace(tasca.descripcio))
            {
                throw new ArgumentException("La tasca té camps obligatoris buits.");
            }

            await API_REST.Instance.AddTascaAsync(tasca);
        }
    }
}
