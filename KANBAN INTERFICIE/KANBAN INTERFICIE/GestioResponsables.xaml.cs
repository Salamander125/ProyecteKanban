using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KANBAN_INTERFICIE
{
    /// <summary>
    /// Interaction logic for GestioResponsables.xaml
    /// </summary>
    public partial class GestioResponsables : Window
    {
        public GestioResponsables()
        {
            try
            {
                InitializeComponent();
                DataContext = new GestioResponsablesViewModel();
            }
            catch (Exception ex )
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void IsAdminCheckbox_Checked(object sender, RoutedEventArgs e)
        {

        }
    }

    /// <nota>
    /// TOT VA AMB BINDINGS!!
    /// </nota>
    public class GestioResponsablesViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string prop)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        public ObservableCollection<Responsable> Responsables { get; set; }

        private Responsable responsableSeleccionat;
        public Responsable ResponsableSeleccionat
        {
            get => responsableSeleccionat;
            set
            {
                responsableSeleccionat = value;
                OnPropertyChanged(nameof(ResponsableSeleccionat));

                if (value != null)
                {
                    NouNom = value.Nom;
                    NouCognom = value.Cognoms;
                    NouValorPrivilegi = value.AdminPrivilegi;

                    OnPropertyChanged(nameof(NouNom));
                    OnPropertyChanged(nameof(NouCognom));
                    OnPropertyChanged(nameof(NouValorPrivilegi));
                }

                (EliminarCommand as RelayCommand)?.RaiseCanExecuteChanged();
                (ModificarCommand as RelayCommand)?.RaiseCanExecuteChanged();
            }
        }


        public string NouNom { get; set; }
        public string NouCognom { get; set; }
        public bool NouValorPrivilegi { get; set; }

        //RoutedUICommand es millor que Button_Click...
        public ICommand AfegirCommand { get; }
        public ICommand EliminarCommand { get; }
        public ICommand ModificarCommand { get; }

        public GestioResponsablesViewModel()
        {
            //Dades d'exemple..
            Responsables = new ObservableCollection<Responsable>
            {
                new Responsable { Codi=1, Nom="Anna", Cognoms="Garcia", AdminPrivilegi=false },
                new Responsable { Codi=2, Nom="Marc", Cognoms="López", AdminPrivilegi=false }
            };

            AfegirCommand = new RelayCommand(Afegir);
            EliminarCommand = new RelayCommand(Eliminar, () => ResponsableSeleccionat != null);
        }

        private void Afegir()
        {
            Responsables.Add(new Responsable
            {
                Codi = Responsables.Count + 1,
                Nom = NouNom,
                Cognoms = NouCognom,
                AdminPrivilegi = NouValorPrivilegi
            });
        }
        private void Modificar()
        {
            if (ResponsableSeleccionat == null) return;

            ResponsableSeleccionat.Nom = NouNom;
            ResponsableSeleccionat.Cognoms = NouCognom;
            ResponsableSeleccionat.AdminPrivilegi = NouValorPrivilegi;
        }

        private void Eliminar()
        {
            Responsables.Remove(ResponsableSeleccionat);
        }
    }
    class RelayCommand : ICommand
    {
        private readonly Action execute;
        private readonly Func<bool> canExecute;

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
            => canExecute == null || canExecute();

        public void Execute(object parameter)
            => execute();

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
            => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }

}
