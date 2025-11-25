using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KANBAN_INTERFICIE
{
    class Responsable
    {
        private int codi;
        private string nom;
        private string cognoms;

        public Responsable(int codi, string nom, string cognoms)
        {
            this.codi = codi;
            this.nom = nom;
            this.cognoms = cognoms;
        }

        public int ObtenerCodigo() => codi;
        public string ObtenerNombre() => nom;
        public string ObtenerApellidos() => cognoms;

        public void CambiarNombre(string nuevoNombre) => nom = nuevoNombre;
        public void CambiarApellidos(string nuevosApellidos) => cognoms = nuevosApellidos;
    }
}
