using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mow.Core
{
    class Manche
    {
        public Manche(int i=0){
            nb = i+1;
            Console.WriteLine("===== Instanciation de la manche n° " + nb +" =====");
            var MaPioche = new Pioche();
        }


        private int nb;



    }
}
