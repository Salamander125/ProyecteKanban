using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KANBAN_INTERFICIE
{
    public enum Status
    {
        ToDo,
        Doing,
        Done
    }
    public enum Prioritat
    {
        Baja,
        Media,
        Alta
    }
    class Tiquet
    {
        private int codi;
        private string responsable;
        private string descripcio;
        private Status estat;
        private string dataCreacio;
        private string dataEstimada_Finalitzacio;
        private Prioritat prioritat;

        // Constructor
        public Tiquet(int codi, string responsable, string descripcio, Status estat,
                      string dataCreacio, string dataEstimada_Finalitzacio, Prioritat prioritat)
        {
            this.codi = codi;
            this.responsable = responsable;
            this.descripcio = descripcio;
            this.estat = estat;
            this.dataCreacio = dataCreacio;
            this.dataEstimada_Finalitzacio = dataEstimada_Finalitzacio;
            this.prioritat = prioritat;
        }

        // Métodos getters
        public int ObtenerCodigo() => codi;
        public string ObtenerResponsable() => responsable;
        public string ObtenerDescripcion() => descripcio;
        public Status ObtenerEstado() => estat;
        public string ObtenerFechaCreacion() => dataCreacio;
        public string ObtenerFechaEstimadaFinalizacion() => dataEstimada_Finalitzacio;
        public Prioritat ObtenerPrioridad() => prioritat;

        // Métodos setters
        public void CanviarResponsable(string nuevoResponsable)
        {
            responsable = nuevoResponsable;
        }

        public void CanviarDescripcio(string nuevaDescripcion)
        {
            descripcio = nuevaDescripcion;
        }

        public void CanviarEstat(int nuevoEstado)
        {
            estat = (Status)nuevoEstado;
        }

        public void CambiarFechaEstimadaFinalizacion(string nuevaFecha)
        {
            dataEstimada_Finalitzacio = nuevaFecha;
        }

        public void CambiarPrioridad(Prioritat nuevaPrioridad)
        {
            prioritat = nuevaPrioridad;
        }
    }
}
