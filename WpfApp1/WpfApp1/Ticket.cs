using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public enum Estado
    {
        ToDo,
        Doing,
        Done
    }

    public class Ticket
    {
        private int codigo;
        private string responsable;
        private string descripcion;
        private Estado estado;
        private string fechaCreacion;
        private string fechaEstimadaFinalizacion;
        private int prioridad;

        // Constructor
        public Ticket(int codigo, string responsable, string descripcion, Estado estado,
                      string fechaCreacion, string fechaEstimadaFinalizacion, int prioridad)
        {
            this.codigo = codigo;
            this.responsable = responsable;
            this.descripcion = descripcion;
            this.estado = estado;
            this.fechaCreacion = fechaCreacion;
            this.fechaEstimadaFinalizacion = fechaEstimadaFinalizacion;
            this.prioridad = prioridad;
        }

        // Métodos getters
        public int ObtenerCodigo() => codigo;
        public string ObtenerResponsable() => responsable;
        public string ObtenerDescripcion() => descripcion;
        public Estado ObtenerEstado() => estado;
        public string ObtenerFechaCreacion() => fechaCreacion;
        public string ObtenerFechaEstimadaFinalizacion() => fechaEstimadaFinalizacion;
        public int ObtenerPrioridad() => prioridad;

        // Métodos setters
        public void CambiarResponsable(string nuevoResponsable)
        {
            responsable = nuevoResponsable;
        }

        public void CambiarDescripcion(string nuevaDescripcion)
        {
            descripcion = nuevaDescripcion;
        }

        public void CambiarEstado(int nuevoEstado)
        {
            estado = (Estado)nuevoEstado;
        }

        public void CambiarFechaEstimadaFinalizacion(string nuevaFecha)
        {
            fechaEstimadaFinalizacion = nuevaFecha;
        }

        public void CambiarPrioridad(int nuevaPrioridad)
        {
            prioridad = nuevaPrioridad;
        }
    }

}
