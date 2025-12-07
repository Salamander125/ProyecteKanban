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

        public int ObtenirCodi() => codi;
        public string ObtenirNom() => nom;
        public string ObtenirCognoms() => cognoms;

        public void CanviarNom(string nouNom) => nom = nouNom;
        public void CanviarCognoms(string NouCognom) => cognoms = NouCognom;
    }
}
