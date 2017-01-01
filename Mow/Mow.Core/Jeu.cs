using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mow.Core
{
    public class Jeu
    {

    // test
        /// <summary>
        ///   The main entry point for the application
        /// </summary>
        public static void Main(){
            Console.WriteLine("===== Le jeu commence =====");

            Partie MaPartie = new Partie();
            MaPartie.LimiteDeMouche = 30;


            MaPartie.JouerPartie();

            Console.ReadLine();


        }

    }
}
