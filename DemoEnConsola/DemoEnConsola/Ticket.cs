using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        Baixa,
        Mitja,
        Alta
    }
    class Tiquet
    {
        // string connectionString = "Server=tu_servidor;Database=tu_base_de_datos;User ID=tu_usuario;Password=tu_contraseña;";

        //using (SqlConnection connection = new SqlConnection(connectionString))
        //{
        //    connection.Open();
        //}


        //string query = "SELECT * FROM MiTabla;";
        //using (SqlCommand command = new SqlCommand(query, connectionString))
        //{
        //    using (SqlDataReader reader = command.ExecuteReader())
        //    {
        //        while (reader.Read())
        //        {
        //            // Procesa los datos del reader
        //        }
        //    }
        //}


        //Usa ExecuteNonQuery() para comandos que no devuelven un conjunto de resultados(como INSERT, UPDATE, DELETE).
        //Usa ExecuteReader() para ejecutar una consulta SELECT y obtener los resultados en un objeto SqlDataReader.
        //Usa ExecuteScalar() para obtener el valor de la primera columna de la primera fila de un resultado.

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
        public int ObtenirCodi() => codi;
        public string ObtenirResponsable() => responsable;
        public string ObtenirDescripcio() => descripcio;
        public Status ObtenirEstat() => estat;
        public string ObtenirDataCreacio() => dataCreacio;
        public string ObtenirDataEstimadaFinalitzacio() => dataEstimada_Finalitzacio;
        public Prioritat ObtenirPrioritat() => prioritat;

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
        public void CanviarDataEstimadaFinalizacio(string nuevaFecha)
        {
            dataEstimada_Finalitzacio = nuevaFecha;
        }
        public void CanviarPrioridad(Prioritat nuevaPrioridad)
        {
            prioritat = nuevaPrioridad;
        }
    }
}