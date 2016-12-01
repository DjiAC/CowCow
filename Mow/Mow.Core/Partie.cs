using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mow.Core
{
    class Partie
    {
        public Partie(){
            Console.WriteLine("===== La partie commence =====");
            for (int i=0; i<3; i++) {
                var MaManche = new Manche(i);
            }
        }
        public List<Joueur> Joueurs = new List<Joueur>();
        
    }
}
