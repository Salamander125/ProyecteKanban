using System.ComponentModel;

namespace KANBAN_INTERFICIE
{
    public class Responsable : INotifyPropertyChanged
    {
        private int codi;
        private string nom;
        private string cognoms;
        private bool esAdmin;

        public int Codi
        {
            get => codi;
            set { codi = value; OnPropertyChanged(nameof(Codi)); }
        }

        public string Nom
        {
            get => nom;
            set { nom = value; OnPropertyChanged(nameof(Nom)); }
        }

        public string Cognoms
        {
            get => cognoms;
            set { cognoms = value; OnPropertyChanged(nameof(Cognoms)); }
        }

        public bool AdminPrivilegi
        {
            get => esAdmin;
            set { esAdmin = value; OnPropertyChanged(nameof(AdminPrivilegi)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }


    }
}
