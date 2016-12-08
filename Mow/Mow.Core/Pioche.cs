using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mow.Core
{
    class Pioche
    {
        public Pioche(){
            Console.WriteLine("=========== Mon royaume pour une pioche ===========");
            List<Carte> TasDeCartes = new List<Carte>();
            // ajout de cartes meubles dans TasDeCartes
            TasDeCartes.Add(new Carte("truc"));
            TasDeCartes.Add(new Carte("trac"));
            TasDeCartes.Add(new Carte("troc"));
            ShowDetails();
        }

        public void ShowDetails(){
            foreach (Carte MaCarte in TasDeCartes) {
                MaCarte.getNom();
            }
        }
        public List<Carte> TasDeCartes;
    }
}
