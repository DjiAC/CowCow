using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mow.Core
{
    class Carte
    {
        public Carte(string n){
            nom = n;
        }
        public string nom = "Generic";

        public void getNom(){
            Console.WriteLine(nom);
        }
        
    }
}
