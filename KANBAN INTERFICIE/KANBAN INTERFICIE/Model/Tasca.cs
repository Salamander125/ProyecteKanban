using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KANBAN_INTERFICIE
{
    public class Tasca
    {
        public long? codi { get; set; }
        public string titol { get; set; }
        public string descripcio { get; set; }
        public DateTime data_creacio { get; set; }
        public DateTime data_finalitzacio { get; set; }
        public int prioritat { get; set; }
        public int estat { get; set; }
        public long? codi_responsable { get; set; }

        public Tasca(long? responsable, string titol, string descripcio, int estat,
              DateTime data_creacio, DateTime data_finalitzacio, int prioritat)
        {
            this.codi_responsable = responsable;
            this.titol = titol;
            this.descripcio = descripcio;
            this.estat = estat;
            this.data_creacio = data_creacio;
            this.data_finalitzacio = data_finalitzacio;
            this.prioritat = prioritat;
        }

        // Getters existents
        public long? ObtenirCodi() => codi;
        public long? ObtenirResponsable() => codi_responsable;
        public string ObtenirTitol() => titol;
        public string ObtenirDescripcio() => descripcio;
        public int ObtenirEstat() => (int)estat;
        public DateTime ObtenirDataCreacio() => data_creacio;
        public DateTime ObtenirDataEstimadaFinalitzacio() => data_finalitzacio;
        public int ObtenirPrioritat() => (int)prioritat;

        // Setters
        public void CanviarResponsable(long? nouResponsable) => codi_responsable = nouResponsable;
        public void CanviarDescripcio(string novaDescripció) => descripcio = novaDescripció;
        public void CanviarEstat(int nouEstat) => estat = nouEstat;
        public void CanviarDataEstimadaFinalizacio(DateTime novaData) => data_finalitzacio = novaData;
        public void CanviarPrioritat(int novaPrioritat) => prioritat = novaPrioritat;
    }
}

