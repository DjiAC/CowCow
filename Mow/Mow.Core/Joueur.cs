using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mow.Core
{
    class Joueur
    {
        public Joueur(){
            Console.WriteLine("===== Instanciation du joueur =====");
        }

        public string Type { get; set; }

        public string Nom {get; set;}

        public string Prenom { get; set; }

        public string Pseudo { get; set; }

        public int NombreDeMouche { get; set; }

        public List<Carte> Main { get; set; }

        public List<Carte> Etable { get; set; }

    }
}
