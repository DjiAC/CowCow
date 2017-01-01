using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Mow.Core
{
    class Partie // Tout fait par C
    {
        public bool Sens { get; set; } // Sens du déroulement - True = horaire / False = anti-horaire

        Stack<Carte> Pioche { get; set; } // Notre objet pioche est une stack
        List<Carte> TroupeauDeVache { get; set; } // C'est l'endroit où les joueurs posent leurs cartes

        public List<Joueur> Joueurs = new List<Joueur>(); 

        public int IndexJoueur = 0; // Le premier joueur dans la liste commence la partie
        public int LimiteDeMouche { get; set; } // Détermine la condition d'arrêt d'une partie

        public Partie()
        {
            Pioche = new Stack<Carte>();
            TroupeauDeVache = new List<Carte>();



            Joueur Seul = new Joueur();
            Seul.Type = "Humain";
            Seul.Main = new List<Carte>();
            Seul.Etable = new List<Carte>();

            Joueur Deux = new Joueur();
            Deux.Type = "Humain";
            Deux.Main = new List<Carte>();
            Deux.Etable = new List<Carte>();

            Joueur Trois = new Joueur();
            Trois.Type = "Humain";
            Trois.Main = new List<Carte>();
            Trois.Etable = new List<Carte>();

            Sens = true; // On inialise le sens

            Joueurs.Add(Seul);
            Joueurs.Add(Deux);
            Joueurs.Add(Trois);


            Console.WriteLine("===== La partie commence =====");

        }
        /// <summary>
        /// Création de la pioche à l'aide d'un fichier Json
        /// </summary>
        public void CreerPioche()   // Fait par C
        {

            var Json = System.IO.File.ReadAllText(@"..\..\..\Ressources\cartes.json"); // On cherche les cartes 

            var Objets = JArray.Parse(Json);                            // On parse le Json en Array
            foreach (JObject root in Objets)                            // On parcourt chaque carte dans l'array
            {
                foreach (KeyValuePair<String, JToken> app in root)      // On parcourt les différentes propriétés d'une carte dans l'array
                {

                    var Type = (String)app.Value["Type"];               // On récupère le type de la carte dans l'array
                    var Numero = (String)app.Value["Nombre"];           // On récupère le nuémro de la carte dans l'array
                    var Mouche = (String)app.Value["Mouches"];          // On récupère le nombre de mouches de la carte dans l'array

                    if (Type == "VacheNormale")                             // on vérifie le type de carte
                    {
                        Carte NouvelleCarte = new Carte();                  // On crée une nouvelle carte

                        NouvelleCarte.TypeDeCarte = Type;                 //  On instancie son type

                        if (Numero != "")
                            NouvelleCarte.NumeroDeCarte = int.Parse(Numero);  //  On instancie son numéro

                        NouvelleCarte.NombreDeMouche = int.Parse(Mouche);   // On instancie son nombre de mouche
                        Pioche.Push(NouvelleCarte);                         // On ajoute la carte dans la pioche
                    }

                    else
                    {
                        Carte NouvelleCarteSpeciale = new Carte();                  // On crée une nouvelle carte spéciale

                        NouvelleCarteSpeciale.TypeDeCarte = Type;                 //  On instancie son type

                        if (Numero != "")                                   // Certaines cartes n'ont pas de numéro
                            NouvelleCarteSpeciale.NumeroDeCarte = int.Parse(Numero);  //  On instancie son numéro

                        NouvelleCarteSpeciale.NombreDeMouche = int.Parse(Mouche);   // On instancie son nombre de mouche
                        Pioche.Push(NouvelleCarteSpeciale);                         // On ajoute la carte dans la pioche
                    }

                }
            }
            var cartemelanger = Pioche.OrderBy(a => Guid.NewGuid()); // On mélange les cartes dans une variable cartemelanger

            var maPile = new Stack<Carte>(cartemelanger.ToList()); // On transforme la variable IOrderedEnumerable en List puis on met ce résultat dans une variable Stack
            Pioche = maPile; // On récupère le résultat de la variable locale dans la varible globale Pioche


        }

        public void JouerPartie()
        {
            while (VerifierMouche() != true) // Une partie s'arrête quand la limite de mouche est atteinte par un joueur
            {
                CreerPioche(); // Création de la pioche

                JouerManche(); // On joue une manche

                CompterMouches(); // A la fin d'une manche on compte les mouches accumulées par les joueurs

                ViderCartes(); // On vide toutes les cartes présentes (dans les mains, étables et le troupeau)
            }
        }

        /// <summary>
        /// C'est le déroulement d'une manche
        /// </summary>
        public void JouerManche()
        {

            DistribuerCarte(); // Au début d'une manche, on distribue les cartes
            string choix = ""; // Variable qui contient le choix d'un joueur

            while (Pioche.Count != 0 || choix != "A") // Une manche s'arrête lorsque la pioche est vide et qu'un joueur ne plus jouer de vache
            {
                if (TroupeauDeVache.Count != 0) 
                    DeterminerJoueurActuelle(); // On détermine le joueur qui va jouer à chaque tour selon le sens 

                Console.WriteLine("C'est le Troupeau");
                foreach (Carte carte in TroupeauDeVache)
                {

                    Console.WriteLine(carte.TypeDeCarte + " " + carte.NumeroDeCarte + " " + carte.NombreDeMouche); // On affiche le troupeau

                }

                if (Joueurs.ElementAt(IndexJoueur).Type == "Humain") // Pour le cas d'un joueur humain
                { 

                    do
                    {

                        
                            Console.WriteLine("Le joueur " + IndexJoueur);
                            Console.WriteLine("Choississez la carte à jouer, (taper entre un chiffre entre 0 et 4 ou passer votre tour en tapent A)");
                            foreach (Carte carte in Joueurs.ElementAt(IndexJoueur).Main)
                            {
                                Console.WriteLine(carte.TypeDeCarte + " " + carte.NumeroDeCarte + " " + carte.NombreDeMouche); // On affiche ses cartes
                            }
                            choix = Console.ReadLine(); // Il choisit la carte à jouer ou de ne pas jouer



                        } while (choix != "A" && JouerCarte(Joueurs.ElementAt(IndexJoueur), Joueurs.ElementAt(IndexJoueur).Main.ElementAt(int.Parse(choix))) == false) ; // S'il tape autre chose que demander ou qu'il ne peut pas jouer la carte qu'il a choisi, il doit recommencer
                    }
                else if (Joueurs.ElementAt(IndexJoueur).Type == "Ordinateur") // Pour le cas de l'IA
                {
                    do
                    {

                        JouerOrdinateur(); // L'IA choisit une carte ou de ne pas jouer
                       



                    } while (choix != "A" && JouerCarte(Joueurs.ElementAt(IndexJoueur), Joueurs.ElementAt(IndexJoueur).Main.ElementAt(int.Parse(choix))) == false); // S'il tape autre chose que demander ou qu'il ne peut pas jouer la carte qu'il a choisi, il doit recommencer
                }


                    if (choix != "A") // Si le joueur a joué une carte
                {

                    if (Pioche.Count != 0)
                        Joueurs.ElementAt(IndexJoueur).Main.Add(Pioche.Pop()); // Le joueur pioche une carte à la fin de son tour
                }

                if (choix == "A") // Si le joueur ne joue pas de vache
                {                  
                    AjouterDansEtable(); // Il récupère les cartes du troupeau dans son étable
                    TroupeauDeVache.Clear(); // On vide le troupeau
                }

            }

        }


        /// <summary>
        /// Méthode qui permet de jouer une vache
        /// </summary>
        /// <param name="JoueurActuelle">Joueur qui joue</param>
        /// <param name="CarteJouee">Carte choisie par le joueur</param>
        /// <returns></returns>
        public bool JouerCarte(Joueur JoueurActuelle, Carte CarteJouee)
        {
            int IndexCarte; // Variable qui désigne l'index de la carte à jouer

            if (TroupeauDeVache.Count == 0 && (CarteJouee.TypeDeCarte != "VacheAcrobate" && CarteJouee.TypeDeCarte != "VacheRetardataire")) // Si le troupeau est vide et que la carte à jouer n'est pas une vache acrobate ou retardataire
            {
                TroupeauDeVache.Add(CarteJouee); // On pose la carte 
                JoueurActuelle.Main.Remove(CarteJouee); // On l'enlève de la main du joueur
                return true;
            }
            else if (TroupeauDeVache.Count == 0 && (CarteJouee.TypeDeCarte == "VacheAcrobate" || CarteJouee.TypeDeCarte == "VacheRetardataire")) // Si le troupeau est vide et que la carte à jouer est une vache acrobate ou retardataire
            {
                Console.WriteLine("Vous ne pouvez pas jouer cette vache spéciale en début de partie");
                return false;
            }

            else if (CarteJouee.TypeDeCarte == "VacheNormale") // Pour le cas d'une vache normale
            {
                int MinimumDuTroupeau = TroupeauDeVache.ElementAt(0).NumeroDeCarte; // On crée la variable qui correspond au minimum du troupeau
                int MaximumDuTroupeau = TroupeauDeVache.ElementAt(TroupeauDeVache.Count - 1).NumeroDeCarte; // On crée la variable qui correspond au maximum du troupeau

                if (CarteJouee.NumeroDeCarte < MinimumDuTroupeau) // Si le numéro de la carte jouée est inférieur au minimum
                {
                    TroupeauDeVache.Insert(0, CarteJouee); // On met la carte au début du troupeau
                    JoueurActuelle.Main.Remove(CarteJouee); // On l'enlève de la main du joueur

                    return true;
                }

                else if (CarteJouee.NumeroDeCarte > MaximumDuTroupeau) // Si le numéro de la carte jouée est supérieur au maximum
                {
                    TroupeauDeVache.Add(CarteJouee); // On met la carte à la fin du troupeau
                    JoueurActuelle.Main.Remove(CarteJouee); // On l'enlève de la main du joueur
                    return true;
                }
                else
                {
                    Console.WriteLine("Erreur votre carte doit être placé en dehors de l'intervalle !");
                    return false;
                }


            }

            else if (CarteJouee.TypeDeCarte == "VacheSerreFile") // Pour le cas de la vache serre file
            {
                int MinimumDuTroupeau = TroupeauDeVache.ElementAt(0).NumeroDeCarte; // On crée la variable qui correspond au minimum du troupeau
                int MaximumDuTroupeau = TroupeauDeVache.ElementAt(TroupeauDeVache.Count - 1).NumeroDeCarte; // On crée la variable qui correspond au maximum du troupeau

                if (CarteJouee.NumeroDeCarte < MinimumDuTroupeau) // Si le numéro de la carte jouée est inférieur au minimum
                {
                    TroupeauDeVache.Insert(0, CarteJouee); // On met la carte au début du troupeau
                    JoueurActuelle.Main.Remove(CarteJouee); // On l'enlève de la main du joueur

                    ChangerSens(); // On demande à changer le sens car c'est une carte spéciale
                    return true;
                }

                else if (CarteJouee.NumeroDeCarte > MaximumDuTroupeau) // Si le numéro de la carte jouée est supérieur au maximum
                {
                    TroupeauDeVache.Add(CarteJouee); // On met la carte à la fin du troupeau
                    JoueurActuelle.Main.Remove(CarteJouee); // On l'enlève de la main du joueur

                    ChangerSens(); // On demande à changer le sens car c'est une carte spéciale
                    return true;
                }
                else
                {
                    Console.WriteLine("Erreur votre carte doit être placé en dehors de l'intervalle !");
                    return false;
                }


            }

            else if (CarteJouee.TypeDeCarte == "VacheAcrobate") // Pour le cas de la vache acrobate
            {

                Console.WriteLine("Choississez où vous voulez poser votre carte : (Taper entre 0 et" + (TroupeauDeVache.Count -1) + ")");
                IndexCarte = int.Parse(Console.ReadLine()); // On récupère l'index de la carte où la vache acrobate doit être posée

                if (CarteJouee.NumeroDeCarte == TroupeauDeVache.ElementAt(IndexCarte).NumeroDeCarte) // Si les numéros sont les même
                {
                    TroupeauDeVache.Insert(IndexCarte, CarteJouee); // On la pose par dessus
                    JoueurActuelle.Main.Remove(CarteJouee); // On l'enlève de la main du joueur

                    ChangerSens(); // On demande à changer le sens car c'est une carte spéciale
                    return true;

                }
                else
                {
                    Console.WriteLine("Il faut que les vaches aient le même numéro");
                    return false;
                }

            }

            else if (CarteJouee.TypeDeCarte == "VacheRetardataire")
            {
                if (TroupeauDeVache.Count >= 2) // Il faut qu'il y a ait 2 vaches au minimum
                {
                    Console.WriteLine("Choississez où vous voulez poser votre carte : (Taper entre 0 et" + (TroupeauDeVache.Count - 1) + ")");
                    IndexCarte = int.Parse(Console.ReadLine()); // On récupère l'index de la carte où la vache retardataire doit être posée

                    if (TroupeauDeVache.ElementAt(IndexCarte + 1).NumeroDeCarte - TroupeauDeVache.ElementAt(IndexCarte).NumeroDeCarte >= 2) // Si l'écart entre les 2 cartes est supérieur à 2
                    {
                        TroupeauDeVache.Insert(IndexCarte, CarteJouee); // On insère la carte entre les deux autres concernées
                        JoueurActuelle.Main.Remove(CarteJouee); // On l'enlève de la main du joueur

                        ChangerSens(); // On demande à changer le sens car c'est une carte spéciale
                        return true;
                    }

                    else
                    {
                        Console.WriteLine("L'écart entre deux vaches doit être de deux minimum.");
                        return false;
                    }



                }
                else
                {
                    Console.WriteLine("Il faut minimum 2 vaches dans le troupeau pour jouer la retardataire");
                }
                return false;

            }
            return true;
        }

        /// <summary>
        /// Distribue les cartes à tous les joueurs présents dans la partie
        /// </summary>
        public void DistribuerCarte()
        {
            for (int i = 0; i < 5; i++) // On le fait 5 fois pour avoir une main de 5 cartes 
            {
                foreach (Joueur joueur in Joueurs) 
                {

                    joueur.Main.Add(Pioche.Pop()); // Pour chaque joueur, on lui donne une carte dans samain
                }
            }
        }

      
        /// <summary>
        /// Détermine le joueur qui doit jouer selon le sens
        /// </summary>
        public void DeterminerJoueurActuelle()
        {
            if (Sens == true)
            {

                IndexJoueur++;
                if (IndexJoueur == Joueurs.Count)
                    IndexJoueur = 0;
            }

            else
            {
                IndexJoueur--;
                if (IndexJoueur == -1)
                    IndexJoueur = Joueurs.Count - 1;
            }
        }

        /// <summary>
        /// Ajoute toutes les cartes du troupeau dans l'étable d'un joueur
        /// </summary>
        public void AjouterDansEtable()
        {
            foreach (Carte carte in TroupeauDeVache)
            {
                Joueurs.ElementAt(IndexJoueur).Etable.Add(carte);
            }
        }


        /// <summary>
        /// Compte les mouches accumulées par chaque joueur
        /// </summary>
        public void CompterMouches()
        {
            foreach (Joueur joueur in Joueurs)
            {
                foreach (Carte carte in joueur.Main)
                {
                    joueur.NombreDeMouche += carte.NombreDeMouche; // On compte les mouches dans la main d'un joueur
                }
                foreach (Carte carte in joueur.Etable)
                {
                    joueur.NombreDeMouche += carte.NombreDeMouche; // On compte les mouches dans les étables
                }
                Console.WriteLine(joueur.NombreDeMouche);
            }

        }


        /// <summary>
        /// Vide les cartes qui sont dans le jeu
        /// </summary>
        public void ViderCartes()
        {
            TroupeauDeVache.Clear(); // On enlève les cartes dans le troupeau

            foreach (Joueur joueur in Joueurs)
            {
                joueur.Main.Clear(); // On enlève les cartes dans la main des joueurs
                joueur.Etable.Clear(); // On enlève les cartes dans l'étable des joueurs

            }
        }

        /// <summary>
        /// Demande si on doit changer le sens
        /// </summary>
        public void ChangerSens()
        {
            Console.WriteLine("Voulez-vous changer de sens? Taper Oui ou Non");
            string choix = Console.ReadLine();

            if (choix == "Oui")
                Sens = !Sens;

            Console.WriteLine(Sens);
        }

        public void JouerOrdinateur()
        {
            //TODO
        }


        /// <summary>
        /// Vérifie si la limite de mouche a été atteinte par un joueur
        /// </summary>
        /// <returns>La partie s'arrête si c'est vrai</returns>
        public bool VerifierMouche()
        {
            foreach (Joueur joueur in Joueurs)
            {
                if (joueur.NombreDeMouche >= LimiteDeMouche)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Désigne le gagnant de la partie, celui qui a le moin de mouche à la fin
        /// </summary>
        /// <returns>Retourne l'index du gagnant</returns>
        public int DesignerGagnant()
        {
            int IndexJoueurGagnant = 0;
            foreach (Joueur joueur in Joueurs)
            {
                int nombreDeMoucheMin = 100;

                if (joueur.NombreDeMouche < nombreDeMoucheMin)
                {
                    nombreDeMoucheMin = joueur.NombreDeMouche;
                    IndexJoueurGagnant = Joueurs.IndexOf(joueur);
                }
            }
            return IndexJoueurGagnant;
        }
    }
}
