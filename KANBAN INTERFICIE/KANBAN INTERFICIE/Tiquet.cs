using System.Windows.Media;

namespace KANBAN_INTERFICIE
{
    public enum Status
    {
        toDo = 0,
        enProgres = 1,
        acabat = 2,
    }

    public enum Prioritat
    {
        Baixa = 0,
        Mitja = 1,
        Alta = 2,
        SensePrioritat = 3,
    }

    class Tiquet
    {
        private int codi;
        private string responsable;
        private string descripcio;
        public Status estat;
        private string dataCreacio;
        private string dataEstimada_Finalitzacio;
        private Prioritat prioritat;


        //Constructor que s'utilitza per fer tiquets nous.
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

        // Propietats públiques per facilitar binding i accés
        public int codi_id => codi;
        public string Description => descripcio;
        public string Responsable => responsable;
        public Status Estat => estat;
        public string DataCreacio => dataCreacio;
        public string DataEstimadaFinalitzacio => dataEstimada_Finalitzacio;
        public Prioritat PrioritatTiquet => prioritat;

        // Color segons prioritat (binding al XAML)
        public SolidColorBrush ColorPrioritat
        {
            get
            {
                switch (prioritat)
                {
                    case Prioritat.Alta: return new SolidColorBrush(Colors.Red);
                    case Prioritat.Mitja: return new SolidColorBrush(Colors.Orange);
                    case Prioritat.Baixa: return new SolidColorBrush(Colors.LightGreen);
                    default: return new SolidColorBrush(Colors.LightGray);
                }
            }
        }

        // Getters existents
        public int ObtenirCodi() => codi;
        public string ObtenirResponsable() => responsable;
        public string ObtenirDescripcio() => descripcio;
        public int ObtenirEstat() => (int)estat;
        public string ObtenirDataCreacio() => dataCreacio;
        public string ObtenirDataEstimadaFinalitzacio() => dataEstimada_Finalitzacio;
        public int ObtenirPrioritat() => (int)prioritat;

        // Setters
        public void CanviarResponsable(string nuevoResponsable) => responsable = nuevoResponsable;
        public void CanviarDescripcio(string nuevaDescripcion) => descripcio = nuevaDescripcion;
        public void CanviarEstat(int nuevoEstado) => estat = (Status)nuevoEstado;
        public void CanviarDataEstimadaFinalizacio(string nuevaFecha) => dataEstimada_Finalitzacio = nuevaFecha;
        public void CanviarPrioridad(Prioritat nuevaPrioridad) => prioritat = nuevaPrioridad;
    }
}