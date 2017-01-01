using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mow.Core
{
    public class Jeu
    {

        public static void Main()
        {
            Console.WriteLine("===== Welcome to MowJow ! =====");
            string action = ""; // On définit la variable qui contient l'action que l'on veut effectuer
            while (action != "quitter")
            { // Le menu principal
                Console.WriteLine("\n\nQue voulez-vous faire ?");
                Console.WriteLine("    - nouvelle partie");
                Console.WriteLine("    - charger partie");
                Console.WriteLine("    - credits");
                Console.WriteLine("    - regles");
                Console.WriteLine("    - options");
                Console.WriteLine("    - quitter");

                action = Console.ReadLine();
                if (action == "nouvelle partie")
                {
                    nouvellePartie();
                }
                if (action == "credits")
                {
                    credits();
                }
            }
        }
        public static void nouvellePartie()
        {
            string action = "";
            while (action != "quitter")
            {
                Console.WriteLine("    solo");
                Console.WriteLine("    multi");
                Console.WriteLine("    quitter");
                action = Console.ReadLine();
                if (action == "solo") // L'utilsateur est seul
                {

                    Partie MaPartie = new Partie(); // on commence la partie

                    Console.WriteLine("Combien de joueurs en plus ? (entre 1 à 4)");

                    int nombreOrdinateur = int.Parse(Console.ReadLine()); // Le joueur entre le nombre d'IA avec qui il veut jouer
                    MaPartie.CreerListeDeJoueur(action, nombreOrdinateur, 0); // On créer la liste de joueur participant

                    Console.WriteLine("Combiens de mouches maximum  ?");
                    MaPartie.LimiteDeMouche = int.Parse(Console.ReadLine()); // L'utilisateur détermine le nombre limite de mouche
                    MaPartie.JouerPartie(); // On lance le jeu

                    Console.ReadLine();

                    action = "quitter";
                }
                else if (action == "multi") // Il y a plusieurs utilisateurs
                {
                    string nbParticipantsHumain = "", nbMouches = "", nbParticipantsOrdinateurs = "";   // nombre de joueurs participants à la partie
                                                                                                        // et limite de mouche d'une partie
                    int nbP, nbM, nbO;
                    while (!Int32.TryParse(nbParticipantsHumain, out nbP))
                    {
                        Console.WriteLine("Combients de participants humains?");
                        nbParticipantsHumain = Console.ReadLine(); // On demande le nombre de joueurs humains
                    }
                    if (nbP < 1) { nbP = 1; }
                    Console.WriteLine("La partie comporteras " + nbP + " participants");

                    while (!Int32.TryParse(nbParticipantsOrdinateurs, out nbO)) // 
                    {
                        Console.WriteLine("Combients de participants Ordinateurs?");
                        nbParticipantsOrdinateurs = Console.ReadLine(); // On demande le nombre de joueurs artificielles
                    }


                    Partie MaPartie = new Partie(); 


                    while (!Int32.TryParse(nbMouches, out nbM))
                    {
                        Console.WriteLine("Combients de mouches maximum  ?");
                        nbMouches = Console.ReadLine(); // On demande le nombre limite de mouche
                    }
                    if (nbM < 0) { nbM = 0; }

                    Console.WriteLine("La partie comporteras " + nbM + " mouches au maximum");
                    MaPartie.LimiteDeMouche = int.Parse(nbMouches); // On instancie la variable LimiteDeMouche

                    MaPartie.CreerListeDeJoueur(action, nbO, nbP); // On créerla liste de joueurs participants
                    MaPartie.JouerPartie(); // On lance le jeu

                    Console.ReadLine();

                    action = "quitter";
                }
            }

        }
        public static void chargerPartie()
        {
            string savePath; // le chemin d'accès à la sauvgarde
            Console.WriteLine("veuillez importer une sauvgarde :");
            savePath = Console.ReadLine();
        }
        public static void credits()
        {
            Console.WriteLine("\n\nCrédits :");
            Console.WriteLine("    MowJow adapté du jeux de société Mow de Bruno Cathala");
            Console.WriteLine("    Dévelopé par les Coding Farmers !");
            Console.WriteLine("    Charles Douangdara, Nicolas Bouyssounouse, Adrien Ceccaldi");
            Console.WriteLine("\nAppuyer sur \"ENTRER\" pour continuer");
            Console.ReadLine();

        }

    }
}
