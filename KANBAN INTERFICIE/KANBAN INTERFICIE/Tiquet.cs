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
        SensePrioritat = 0,
        Baixa = 1,
        Mitja = 2,
        Alta = 3,
    }

    public class Tiquet
    {
        private int codi;
        private string responsable;
        private string titol;
        private string descripcio;
        public Status estat;
        private string dataCreacio;
        private string dataEstimada_Finalitzacio;
        private Prioritat prioritat;


        //Constructor que s'utilitza per fer tiquets nous.
        public Tiquet(int codi, string responsable, string titol, string descripcio, Status estat,
                      string dataCreacio, string dataEstimada_Finalitzacio, Prioritat prioritat)
        {
            this.codi = codi;
            this.responsable = responsable;
            this.titol = titol;
            this.descripcio = descripcio;
            this.estat = estat;
            this.dataCreacio = dataCreacio;
            this.dataEstimada_Finalitzacio = dataEstimada_Finalitzacio;
            this.prioritat = prioritat;
        }

        // Propietats públiques per facilitar binding i accés
        public int Codi_id => codi;
        public string Titol => titol;
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
                    case Prioritat.Alta: return new SolidColorBrush(Colors.Tomato);
                    case Prioritat.Mitja: return new SolidColorBrush(Colors.Gold);
                    case Prioritat.Baixa: return new SolidColorBrush(Colors.LightGreen);
                    case Prioritat.SensePrioritat: return new SolidColorBrush(Colors.LightGray);
                    default: return new SolidColorBrush(Colors.LightGray);
                }
            }
        }

        // Getters existents
        public int ObtenirCodi() => codi;
        public string ObtenirResponsable() => responsable;
        public string ObtenirTitol() => Titol;
        public string ObtenirDescripcio() => descripcio;
        public int ObtenirEstat() => (int)estat;
        public string ObtenirDataCreacio() => dataCreacio;
        public string ObtenirDataEstimadaFinalitzacio() => dataEstimada_Finalitzacio;
        public int ObtenirPrioritat() => (int)prioritat;

        // Setters
        public void CanviarResponsable(string nouResponsable) => responsable = nouResponsable;
        public void CanviarDescripcio(string novaDescripció) => descripcio = novaDescripció;
        public void CanviarEstat(Status nouEstat) => estat = nouEstat;
        public void CanviarDataEstimadaFinalizacio(string novaData) => dataEstimada_Finalitzacio = novaData;
        public void CanviarPrioritat(Prioritat novaPrioritat) => prioritat = novaPrioritat;
    }
}