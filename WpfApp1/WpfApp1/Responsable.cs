using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Responsable
    {
        private int codigo;
        private string nombre;
        private string apellidos;

        public Responsable(int codigo, string nombre, string apellidos)
        {
            this.codigo = codigo;
            this.nombre = nombre;
            this.apellidos = apellidos;
        }

        public int ObtenerCodigo() => codigo;
        public string ObtenerNombre() => nombre;
        public string ObtenerApellidos() => apellidos;

        public void CambiarNombre(string nuevoNombre) => nombre = nuevoNombre;
        public void CambiarApellidos(string nuevosApellidos) => apellidos = nuevosApellidos;
    }

}
