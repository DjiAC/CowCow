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
            int nombreOrdinateur = 0; // C'est le nombre d'IA
            int nombreDeMouches = 0; 
            int nombreUtilisateur = 0; // C'est le nombre d'utilisateur
            while (action != "quitter")
            {
                Console.WriteLine("    solo");
                Console.WriteLine("    multi");
                Console.WriteLine("    quitter");
                action = Console.ReadLine();
                if (action == "solo") // L'utilsateur est seul
                {
                    bool erreur = true; // Permet de voir si tout ce passe bien 
                    bool erreur2 = true;


                    while (erreur == true)
                    {
                        try
                        {

                            Console.WriteLine("Combien de joueurs en plus ? (entre 1 à 4)");
                            nombreOrdinateur = int.Parse(Console.ReadLine()); // Le joueur entre le nombre d'IA avec qui il veut jouer

                            if (nombreOrdinateur >= 1 && nombreOrdinateur <= 4)
                            {
                                erreur = false;

                            }
                            else
                            {
                                Console.WriteLine("Le nombre de participants doit être en 1 et 4");
                            }
                        }
                        catch (ArgumentNullException exception)
                        {
                            Console.WriteLine(exception.Message);
                            Console.WriteLine("Merci de ne pas mettre une chaine vide");
                        }
                        catch (FormatException exception)
                        {
                            Console.WriteLine(exception.Message);
                            Console.WriteLine("Veillez entrer un nombre entre 1 et 4.");
                        }
                        catch (OverflowException exception)
                        {
                            Console.WriteLine(exception.Message);
                            Console.WriteLine("Veuillez entrer un nombre possible.");
                        }
                    }

                    while (erreur2 == true)
                    {
                        try
                        {
                            Console.WriteLine("Combiens de mouches maximum  ?");
                            nombreDeMouches = int.Parse(Console.ReadLine()); // L'utilisateur détermine le nombre limite de mouche

                            erreur2 = false;

                        }
                        catch (ArgumentNullException exception)
                        {
                            Console.WriteLine(exception.Message);
                            Console.WriteLine("Merci de ne pas mettre une chaine vide");
                        }
                        catch (ArgumentOutOfRangeException exception)
                        {
                            Console.WriteLine(exception.Message);
                            Console.WriteLine("Entrer un nombre qui est dans l'intervalle.");
                        }
                        catch (FormatException exception)
                        {
                            Console.WriteLine(exception.Message);
                            Console.WriteLine("Veillez entrer un nombre.");
                        }
                        catch (OverflowException exception)
                        {
                            Console.WriteLine(exception.Message);
                            Console.WriteLine("Veuillez entrer un nombre possible.");
                        }
                    }

                    Console.WriteLine("Entrer votre pseudo");
                    string NomJoueur = Console.ReadLine();

                    Partie MaPartie = new Partie(action, nombreOrdinateur, NomJoueur, nombreDeMouches);

                    MaPartie.JouerPartie(); // On lance le jeu

                    Console.ReadLine();

                    action = "quitter";

                }

                else if (action == "multi") // Il y a plusieurs utilisateurs
                {

                    bool erreur = true;

                    while (erreur == true)
                    {
                        try
                        {

                            Console.WriteLine("Combien de joueurs en plus ? (entre 1 à 4)");
                            nombreUtilisateur = int.Parse(Console.ReadLine()); // Le joueur entre le nombre d'IA avec qui il veut jouer

                            if (nombreUtilisateur >= 1 && nombreUtilisateur <= 4)
                            {
                                erreur = false;

                            }
                            else
                            {
                                Console.WriteLine("Le nombre de participants doit être en 1 et 4");
                            }
                        }
                        catch (ArgumentNullException exception)
                        {
                            Console.WriteLine(exception.Message);
                            Console.WriteLine("Merci de ne pas mettre une chaine vide");
                        }
                        catch (FormatException exception)
                        {
                            Console.WriteLine(exception.Message);
                            Console.WriteLine("Veillez entrer un nombre entre 1 et 4.");
                        }
                        catch (OverflowException exception)
                        {
                            Console.WriteLine(exception.Message);
                            Console.WriteLine("Veuillez entrer un nombre possible.");
                        }
                    }

                    erreur = true;

                    while (erreur == true)
                    {
                        try
                        {

                            Console.WriteLine("Combien de joueurs en plus ? (entre 1 à 4)");
                            nombreOrdinateur = int.Parse(Console.ReadLine()); // Le joueur entre le nombre d'IA avec qui il veut jouer

                            if (nombreOrdinateur >= 1 && nombreOrdinateur <= 4)
                            {
                                erreur = false;

                            }
                            else
                            {
                                Console.WriteLine("Le nombre de participants doit être en 1 et 4");
                            }
                        }
                        catch (ArgumentNullException exception)
                        {
                            Console.WriteLine(exception.Message);
                            Console.WriteLine("Merci de ne pas mettre une chaine vide");
                        }
                        catch (FormatException exception)
                        {
                            Console.WriteLine(exception.Message);
                            Console.WriteLine("Veillez entrer un nombre entre 1 et 4.");
                        }
                        catch (OverflowException exception)
                        {
                            Console.WriteLine(exception.Message);
                            Console.WriteLine("Veuillez entrer un nombre possible.");
                        }
                    }



                    erreur = true;
                    while (erreur == true)
                    {
                        try
                        {
                            Console.WriteLine("Combiens de mouches maximum  ?");
                            nombreDeMouches = int.Parse(Console.ReadLine()); // L'utilisateur détermine le nombre limite de mouche

                            erreur = false;

                        }
                        catch (ArgumentNullException exception)
                        {
                            Console.WriteLine(exception.Message);
                            Console.WriteLine("Merci de ne pas mettre une chaine vide");
                        }
                        catch (ArgumentOutOfRangeException exception)
                        {
                            Console.WriteLine(exception.Message);
                            Console.WriteLine("Entrer un nombre qui est dans l'intervalle.");
                        }
                        catch (FormatException exception)
                        {
                            Console.WriteLine(exception.Message);
                            Console.WriteLine("Veillez entrer un nombre.");
                        }
                        catch (OverflowException exception)
                        {
                            Console.WriteLine(exception.Message);
                            Console.WriteLine("Veuillez entrer un nombre possible.");
                        }
                    }


                    Console.WriteLine("Entrer votre pseudo");
                    string NomJoueur = Console.ReadLine();

                    Partie MaPartie = new Partie(action, nombreOrdinateur, NomJoueur, nombreDeMouches);

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
