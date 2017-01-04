using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Mow.Core
{
    public class Partie // Tout fait par C
    {
        public bool Sens { get; set; } // Sens du déroulement du jeu - True = horaire / False = anti-horaire
        public string SensImage { get; set; } // Sens du déroulement du jeu - Image
        public string ChoixSens { get; set; } // Choix changement sens ou non

        Stack<Carte> Pioche { get; set; } // Notre objet pioche est une stack
        List<Carte> TroupeauDeVache { get; set; } // C'est l'endroit où les joueurs posent leurs cartes

        public List<Joueur> Joueurs = new List<Joueur>();

        public int IndexJoueur = 0; // Le premier joueur dans la liste commence la partie
        public int LimiteDeMouche { get; set; } // Détermine la condition d'arrêt d'une partie

        public string TypeDePartie { get; set; } // Type de partie
        public string NomJoueur { get; set; } // Pseudo du joueur
        public int NbJoueursPartie { get; set; } // Nb de joueurs de la partie

        public int NbManche { get; set; } // Numéro de la manche en cours
        public bool AJoueeCarte { get; set; } // Test à jouer ou non carte

        public string Choix { get; set; }  // Variable qui contient le Choix d'un joueur        
        public int IndexCarte { get; set; } // Variable qui désigne l'index de la carte à jouer

        public string MessageBox { get; set; } // Message box

        public int ScoreJoueur { get; set; } // Score joueur
        public int ScoreDaenerys { get; set; } // Score Daenerys
        public int ScoreNegan { get; set; } // Score Negan
        public int ScoreSavitar { get; set; } // Score Savitar
        public int ScoreRobert { get; set; } // Score Robert

        public string CarteMain1 { get; set; } // Lien image Carte 1 de la main
        public string CarteMain2 { get; set; } // Lien image Carte 2 de la main
        public string CarteMain3 { get; set; } // Lien image Carte 3 de la main
        public string CarteMain4 { get; set; } // Lien image Carte 4 de la main
        public string CarteMain5 { get; set; } // Lien image Carte 5 de la main

        public Partie(string TypePartie, int NbJoueurs, string nomJoueur, int NbMouche)
        {
            Pioche = new Stack<Carte>();
            TroupeauDeVache = new List<Carte>();

            Sens = true; // On inialise le sens
            LimiteDeMouche = NbMouche;
            NomJoueur = nomJoueur;
            NbJoueursPartie = NbJoueurs;
            TypeDePartie = TypePartie;
            MessageBox = "Welcome in Mow Jow !";
            NbManche = 1;
            AJoueeCarte = false;
            SensImage = "Images/SensHoraire.png";            

            // Initialisation Score Joueurs
            ScoreJoueur = ScoreDaenerys = ScoreNegan = ScoreSavitar = ScoreRobert = 0;
        }
        /// <summary>
        /// Création de la pioche à l'aide d'un fichier Json
        /// </summary>
        public void CreerPioche()   // Fait par C
        {
            var Json = System.IO.File.ReadAllText(@"..\..\..\cartes.json"); // On cherche les cartes 

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

        public void Test()
        {
            CreerPioche(); // Création de la pioche

            CreerListeDeJoueur(TypeDePartie, 4, 1); // On créé la liste de joueur participant

            NbManche = 1; // initialisation numero manche
        }

        public void JouerPartie()
        {
            CreerListeDeJoueur(TypeDePartie, 4, 1); // On créé la liste de joueur participant

            if (VerifierMouche() != true) // Une partie s'arrête quand la limite de mouche est atteinte par un joueur
            {
                CreerPioche(); // Création de la pioche

                DistribuerCarte(); // Au début d'une manche, on distribue les cartes

                JouerManche(); // On joue une manche

                CompterMouches(); // A la fin d'une manche on compte les mouches accumulées par les joueurs

                ViderCartes(); // On vide toutes les cartes présentes (dans les mains, étables et le troupeau)
            }
            Console.WriteLine("Le gagnant de la partie est le joueur :" + Joueurs.ElementAt(DesignerGagnant()).Pseudo + ".");
        }

        /// <summary>
        /// C'est le déroulement d'une manche
        /// </summary>
        public void JouerManche()
        {
            if (Pioche.Count != 0 || Choix != "A") // Une manche s'arrête lorsque la pioche est vide et qu'un joueur ne peux plus jouer de vache
            {
                if (TroupeauDeVache.Count != 0 && AJoueeCarte == true)
                    DeterminerJoueurActuel(); // On détermine le joueur qui va jouer à chaque tour selon le sens 

                MessageBox = "Troupeau";
                foreach (Carte carte in TroupeauDeVache)
                {
                    MessageBox = (carte.TypeDeCarte + " " + carte.NumeroDeCarte + " " + carte.NombreDeMouche); // On affiche le troupeau
                }
                
                if (Joueurs.ElementAt(IndexJoueur).Type == "Humain") // Pour le cas d'un joueur humain
                {
                    AJoueeCarte = (Choix != "A" && JouerCarte(Joueurs.ElementAt(IndexJoueur), Joueurs.ElementAt(IndexJoueur).Main.ElementAt(int.Parse(Choix))) == false); // S'il tape autre chose que demander ou qu'il ne peut pas jouer la carte qu'il a choisi, il doit recommencer
                }
                else if (Joueurs.ElementAt(IndexJoueur).Type == "Ordinateur") // Pour le cas de l'IA
                {
                    System.Threading.Thread.Sleep(3000); // Temps d'attente simulation Humaine

                    Choix = JouerOrdinateurFaible(Joueurs.ElementAt(IndexJoueur)); // L'IA choisit une carte ou de ne pas jouer

                    AJoueeCarte = (Choix != "A" && JouerCarte(Joueurs.ElementAt(IndexJoueur), Joueurs.ElementAt(IndexJoueur).Main.ElementAt(int.Parse(Choix))) == false); // S'il tape autre chose que demander ou qu'il ne peut pas jouer la carte qu'il a choisi, il doit recommencer
                }

                if (Choix != "A") // Si le joueur a joué une carte
                {
                    if (Pioche.Count != 0)
                        Joueurs.ElementAt(IndexJoueur).Main.Add(Pioche.Pop()); // Le joueur pioche une carte à la fin de son tour
                    // Actualisation de la main du joueur
                    CarteMain1 = "Images/" + Joueurs.ElementAt(0).Main.ElementAt(0).TypeDeCarte + "-" + Joueurs.ElementAt(0).Main.ElementAt(0).NumeroDeCarte + "-" + Joueurs.ElementAt(0).Main.ElementAt(0).NombreDeMouche + ".png"; // Récupération lien image Carte 1
                    CarteMain2 = "Images/" + Joueurs.ElementAt(0).Main.ElementAt(1).TypeDeCarte + "-" + Joueurs.ElementAt(0).Main.ElementAt(1).NumeroDeCarte + "-" + Joueurs.ElementAt(0).Main.ElementAt(1).NombreDeMouche + ".png"; // Récupération lien image Carte 2
                    CarteMain3 = "Images/" + Joueurs.ElementAt(0).Main.ElementAt(2).TypeDeCarte + "-" + Joueurs.ElementAt(0).Main.ElementAt(2).NumeroDeCarte + "-" + Joueurs.ElementAt(0).Main.ElementAt(2).NombreDeMouche + ".png"; // Récupération lien image Carte 3
                    CarteMain4 = "Images/" + Joueurs.ElementAt(0).Main.ElementAt(3).TypeDeCarte + "-" + Joueurs.ElementAt(0).Main.ElementAt(3).NumeroDeCarte + "-" + Joueurs.ElementAt(0).Main.ElementAt(3).NombreDeMouche + ".png"; // Récupération lien image Carte 4
                    CarteMain5 = "Images/" + Joueurs.ElementAt(0).Main.ElementAt(4).TypeDeCarte + "-" + Joueurs.ElementAt(0).Main.ElementAt(4).NumeroDeCarte + "-" + Joueurs.ElementAt(0).Main.ElementAt(4).NombreDeMouche + ".png"; // Récupération lien image Carte 5
                }

                if (Choix == "A") // Si le joueur ne joue pas de vache
                {
                    AjouterDansEtable(); // Il récupère les cartes du troupeau dans son étable
                    TroupeauDeVache.Clear(); // On vide le troupeau
                }
            }
        }

        public void FinManche(int IndexJoueur)
        {
            // Récupération score des joueurs
            ScoreJoueur = Joueurs.ElementAt(0).NombreDeMouche;
            ScoreDaenerys = Joueurs.ElementAt(1).NombreDeMouche;
            ScoreNegan = Joueurs.ElementAt(2).NombreDeMouche;
            ScoreSavitar = Joueurs.ElementAt(3).NombreDeMouche;
            ScoreRobert = Joueurs.ElementAt(4).NombreDeMouche;
        }

        /// <summary>
        /// Méthode qui permet de jouer une vache
        /// </summary>
        /// <param name="JoueurActuel">Joueur qui joue</param>
        /// <param name="CarteJouee">Carte choisie par le joueur</param>
        /// <returns></returns>
        public bool JouerCarte(Joueur JoueurActuel, Carte CarteJouee)
        {
            if (TroupeauDeVache.Count == 0 && (CarteJouee.TypeDeCarte != "VacheAcrobate" && CarteJouee.TypeDeCarte != "VacheRetardataire")) // Si le troupeau est vide et que la carte à jouer n'est pas une vache acrobate ou retardataire
            {
                TroupeauDeVache.Add(CarteJouee); // On pose la carte 
                JoueurActuel.Main.Remove(CarteJouee); // On l'enlève de la main du joueur

                if (CarteJouee.TypeDeCarte == "VacheSerreFile")
                {
                    if (JoueurActuel.Type == "Humain")
                        ChangerSens(); // On demande à changer le sens car c'est une carte spéciale
                    else Sens = !Sens;
                }
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
                    JoueurActuel.Main.Remove(CarteJouee); // On l'enlève de la main du joueur

                    return true;
                }

                else if (CarteJouee.NumeroDeCarte > MaximumDuTroupeau) // Si le numéro de la carte jouée est supérieur au maximum
                {
                    TroupeauDeVache.Add(CarteJouee); // On met la carte à la fin du troupeau
                    JoueurActuel.Main.Remove(CarteJouee); // On l'enlève de la main du joueur
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
                    JoueurActuel.Main.Remove(CarteJouee); // On l'enlève de la main du joueur

                    if (JoueurActuel.Type == "Humain")
                        ChangerSens(); // On demande à changer le sens car c'est une carte spéciale

                    else Sens = !Sens;
                    return true;
                }

                else if (CarteJouee.NumeroDeCarte > MaximumDuTroupeau) // Si le numéro de la carte jouée est supérieur au maximum
                {
                    TroupeauDeVache.Add(CarteJouee); // On met la carte à la fin du troupeau
                    JoueurActuel.Main.Remove(CarteJouee); // On l'enlève de la main du joueur

                    if (JoueurActuel.Type == "Humain")
                        ChangerSens(); // On demande à changer le sens car c'est une carte spéciale

                    else Sens = !Sens;
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
                if (JoueurActuel.Type == "Humain")
                {

                }
                else
                {
                    IndexCarte = JouerCarteSpecialeOrdinateurFaible(JoueurActuel, CarteJouee);
                }

                if (CarteJouee.NumeroDeCarte == TroupeauDeVache.ElementAt(IndexCarte).NumeroDeCarte) // Si les numéros sont les même
                {
                    TroupeauDeVache.Insert(IndexCarte, CarteJouee); // On la pose par dessus
                    JoueurActuel.Main.Remove(CarteJouee); // On l'enlève de la main du joueur

                    if (JoueurActuel.Type == "Humain")
                        ChangerSens(); // On demande à changer le sens car c'est une carte spéciale

                    else Sens = !Sens;
                    return true;
                }
                else
                {
                    Console.WriteLine("Il faut que les vaches aient le même numéro");
                    return false;
                }
            }
            return true;
        }

        public bool JouerCarteUnpeuSpéciale(Joueur JoueurActuel, Carte CarteJouee)
        {
            if (CarteJouee.TypeDeCarte == "VacheRetardataire") // Pour le cas de la vache retardataire
            {
                if (TroupeauDeVache.Count >= 2) // Il faut qu'il y a ait 2 vaches au minimum
                {
                    if (JoueurActuel.Type == "Humain")
                    {

                    }
                    else
                    {
                        IndexCarte = JouerCarteSpecialeOrdinateurFaible(JoueurActuel, CarteJouee);
                    }

                    if (TroupeauDeVache.ElementAt(IndexCarte + 1).NumeroDeCarte - TroupeauDeVache.ElementAt(IndexCarte).NumeroDeCarte >= 2) // Si l'écart entre les 2 cartes est supérieur à 2
                    {
                        TroupeauDeVache.Insert(IndexCarte, CarteJouee); // On insère la carte entre les deux autres concernées
                        JoueurActuel.Main.Remove(CarteJouee); // On l'enlève de la main du joueur

                        if (JoueurActuel.Type == "Humain")
                            ChangerSens(); // On demande à changer le sens car c'est une carte spéciale

                        else Sens = !Sens;

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
        /// Méthode qui permet à l'ordinateur de jouer une carte de façon autonome
        /// </summary>
        /// <param name="JoueurActuel">Le joueur artificielle en cours</param>
        /// <returns>Retourne son Choix de jeu</returns>
        public string JouerOrdinateurFaible(Joueur JoueurActuel)
        {
            if (TroupeauDeVache.Count == 0)
            {
                foreach (Carte carte in JoueurActuel.Main)
                {
                    if (carte.TypeDeCarte == "VacheNormale")
                    {
                        return JoueurActuel.Main.IndexOf(carte).ToString(); // Lorsque que le troupeau est vide, il posera une vache normale quelconque
                    }
                }

                foreach (Carte carte in JoueurActuel.Main)
                {
                    if (carte.TypeDeCarte == "VacheSerreFile")
                    {
                        return JoueurActuel.Main.IndexOf(carte).ToString(); // Pose une vache serre file s'il n'a pas d'autre Choix (très rare)
                    }
                }
            }
            else
            {
                foreach (Carte carte in JoueurActuel.Main)
                {
                    if (carte.TypeDeCarte == "VacheNormale") // L'ordinateur jouera en priorité un vache normale
                    {
                        int MinimumDuTroupeau = TroupeauDeVache.ElementAt(0).NumeroDeCarte; // On crée la variable qui correspond au minimum du troupeau
                        int MaximumDuTroupeau = TroupeauDeVache.ElementAt(TroupeauDeVache.Count - 1).NumeroDeCarte; // On crée la variable qui correspond au maximum du troupeau

                        if (JoueurActuel.Main.ElementAt(JoueurActuel.Main.IndexOf(carte)).NumeroDeCarte < MinimumDuTroupeau) // Si le numéro de la carte jouée est inférieur au minimum
                        {
                            return JoueurActuel.Main.IndexOf(carte).ToString(); // Retourne l'index d'une carte vache normale qui respecte la condition inférieure au troupeau 
                        }
                        else if (JoueurActuel.Main.ElementAt(JoueurActuel.Main.IndexOf(carte)).NumeroDeCarte > MaximumDuTroupeau) // Si le numéro de la carte jouée est supérieur au maximum
                        {
                            return JoueurActuel.Main.IndexOf(carte).ToString(); // Retourne l'index d'une carte vache normale qui respecte la condition supérieure au troupeau
                        }
                    }
                }

                foreach (Carte carte in JoueurActuel.Main)
                {
                    if (carte.TypeDeCarte == "VacheSerrefile") // Sinon il joue une serre file
                    {
                        return JoueurActuel.Main.IndexOf(carte).ToString();
                    }
                    else if (carte.TypeDeCarte == "VacheAcrobate") // Sinon une acrobate
                    {
                        foreach (Carte cartevache in TroupeauDeVache)
                        {
                            if (JoueurActuel.Main.ElementAt(JoueurActuel.Main.IndexOf(carte)).NumeroDeCarte == cartevache.NumeroDeCarte)
                            {
                                return JoueurActuel.Main.IndexOf(carte).ToString(); // Retourne l'index de la vache acrobate si la condition qu'il y a ait une carte du même numéro dans le troupeau
                            }
                        }
                    }
                    else if (carte.TypeDeCarte == "VacheRetardataire")
                    {
                        foreach (Carte cartevache in TroupeauDeVache)
                        {
                            int limite = (TroupeauDeVache.Count - 2);
                            if (TroupeauDeVache.IndexOf(cartevache) < limite && TroupeauDeVache.Count >= 2)
                            {
                                if (TroupeauDeVache.ElementAt(TroupeauDeVache.IndexOf(cartevache) + 1).NumeroDeCarte - cartevache.NumeroDeCarte >= 2)
                                {
                                    return JoueurActuel.Main.IndexOf(carte).ToString(); // Retourne l'index de la vache retardataire si la condition qu'il y a ait 2 cartes dans le troupeau et que l'écarte entre 2 cartes et >2 
                                }
                            }
                        }
                    }

                }
            }
            return "A";
        }

        /// <summary>
        /// Permet à l'ordinateur de placer sa carte spéciale
        /// </summary>
        /// <param name="JoueuActuelle">Le joueur ordinateur qui joue</param>
        /// <param name="CarteJouee">La carte précemment choisi à l'aide de JouerOrdinateurFaible</param>
        /// <returns></returns>
        public int JouerCarteSpecialeOrdinateurFaible(Joueur JoueuActuelle, Carte CarteJouee)
        {
            if (CarteJouee.TypeDeCarte == "VacheAcrobate")
            {
                foreach (Carte carte in TroupeauDeVache)
                {
                    if (CarteJouee.NumeroDeCarte == carte.NumeroDeCarte)
                    {
                        return TroupeauDeVache.IndexOf(carte); // Retourne l'index de la carte du troupeau qui lui permettra de poser sa carte vache acrobate
                    }
                }
            }
            else
            {
                foreach (Carte carte in TroupeauDeVache)
                {
                    if (TroupeauDeVache.IndexOf(carte) < (TroupeauDeVache.Count - 1))
                    {
                        if (TroupeauDeVache.ElementAt(TroupeauDeVache.IndexOf(carte) + 1).NumeroDeCarte - TroupeauDeVache.ElementAt(TroupeauDeVache.IndexOf(carte)).NumeroDeCarte >= 2)
                        {
                            return TroupeauDeVache.IndexOf(carte);  // Retourne l'index de la carte du troupeau qui lui permettra de poser sa carte vache retardataire
                        }
                    }
                }
            }
            return 0;
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
            CarteMain1 = "Images/" + Joueurs.ElementAt(0).Main.ElementAt(0).TypeDeCarte + "-" + Joueurs.ElementAt(0).Main.ElementAt(0).NumeroDeCarte + "-" + Joueurs.ElementAt(0).Main.ElementAt(0).NombreDeMouche + ".png"; // Récupération lien image Carte 1
            CarteMain2 = "Images/" + Joueurs.ElementAt(0).Main.ElementAt(1).TypeDeCarte + "-" + Joueurs.ElementAt(0).Main.ElementAt(1).NumeroDeCarte + "-" + Joueurs.ElementAt(0).Main.ElementAt(1).NombreDeMouche + ".png"; // Récupération lien image Carte 2
            CarteMain3 = "Images/" + Joueurs.ElementAt(0).Main.ElementAt(2).TypeDeCarte + "-" + Joueurs.ElementAt(0).Main.ElementAt(2).NumeroDeCarte + "-" + Joueurs.ElementAt(0).Main.ElementAt(2).NombreDeMouche + ".png"; // Récupération lien image Carte 3
            CarteMain4 = "Images/" + Joueurs.ElementAt(0).Main.ElementAt(3).TypeDeCarte + "-" + Joueurs.ElementAt(0).Main.ElementAt(3).NumeroDeCarte + "-" + Joueurs.ElementAt(0).Main.ElementAt(3).NombreDeMouche + ".png"; // Récupération lien image Carte 4
            CarteMain5 = "Images/" + Joueurs.ElementAt(0).Main.ElementAt(4).TypeDeCarte + "-" + Joueurs.ElementAt(0).Main.ElementAt(4).NumeroDeCarte + "-" + Joueurs.ElementAt(0).Main.ElementAt(4).NombreDeMouche + ".png"; // Récupération lien image Carte 5
        }

        /// <summary>
        /// Créer la liste de joueur qui vont jouer la partie
        /// </summary>
        /// <param name="option">On détermine le mode de jeu, en solo ou en multi</param>
        /// <param name="nombreOrdinateur">Le nombre d'IA à mettre</param>
        /// <param name="nombreUtilisateur">Le nombre d'utilisateur à mettre</param>
        public void CreerListeDeJoueur(string typePartie, int nombreOrdinateur, int nombreUtilisateur)
        {
            if (typePartie == "solo") // Il y a un seul utilisateur
            {
                CreerProfil(NomJoueur); // On crée son profil puis on l'ajoute à la liste des joueurs
                
                for (int i = 0; i < nombreOrdinateur - 1; i++)
                    CreerProfilOrdinateur(i);  // On crée autant de joueurs artificielles que le nombre demandé
                
            }
            else if (typePartie == "multi") // Il y a plusieurs utilisateurs
            {
                for (int i = 0; i < nombreUtilisateur; i++)
                    CreerProfil(NomJoueur); // On crée autant de profile que le nombre demandé

                for (int i = 0; i < nombreOrdinateur - 1; i++)
                    CreerProfilOrdinateur(i); // On crée autant de joueurs artificielles que le nombre demandé
                
            }
        }

        /// <summary>
        /// Permet à un utilisateur de créer son profile
        /// </summary>
        public void CreerProfil(string NomJoueur)
        {
            Joueur joueur = new Joueur(); // On initialise l'objet joueur
            joueur.Etable = new List<Carte>(); // On initialise l'objet Etable du joueur
            joueur.Main = new List<Carte>(); // On initialise l'objet Main du joueur

            joueur.Pseudo = NomJoueur; // L'utilisateur entre son pseudo
            joueur.Type = "Humain"; // On définit le type de joueur
            Joueurs.Add(joueur); // On ajoute ensuite le joueur dans la liste
        }

        /// <summary>
        /// Crée un joueur arficielle
        /// </summary>
        /// <param name="i">Index pour la liste de nom</param>
        public void CreerProfilOrdinateur(int i)
        {
            Joueur joueur = new Joueur(); // On initialise l'objet joueur
            joueur.Etable = new List<Carte>(); // On initialise l'objet Etable du joueur
            joueur.Main = new List<Carte>(); // On initialise l'objet Main du joueur

            joueur.Pseudo = joueur.NomAleatoire.ElementAt(i); //  choisit un nom dans la liste proposé
                                                              // TODO rendre ça aléatoire et éviter les doublons lors de l'aléatoire

            joueur.Type = "Ordinateur"; // On définit le type
            Joueurs.Add(joueur); // On ajoute ensuite le joueur dans la liste
        }

        /// <summary>
        /// Détermine le joueur qui doit jouer selon le sens
        /// </summary>
        public void DeterminerJoueurActuel()
        {
            if (Sens == true)
            {
                IndexJoueur++; // On passe au joueur suivant 
                if (IndexJoueur == Joueurs.Count)
                    IndexJoueur = 0;
                SensImage = "Images/SensHoraire.png"; // Changement sens --> Indication carte
            }
            else
            {
                IndexJoueur--; // On passe au joueur suivant 
                if (IndexJoueur == -1) 
                    IndexJoueur = Joueurs.Count - 1;
                SensImage = "Images/SensAntiHoraire.png"; // Changement sens --> Indication carte
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
            bool erreur = true;
            while (erreur == true)
            {
                Console.WriteLine("Voulez-vous changer de sens? Taper Oui ou Non");

                if (ChoixSens == "Oui" || ChoixSens == "Non")
                {
                    erreur = false;
                }
                else
                {
                    Console.WriteLine("Veuillez Répondre par Oui ou par Non !");
                }
            }
            if (ChoixSens == "Oui")
                Sens = !Sens;
            Console.WriteLine(Sens);
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
            int nombreDeMoucheMin = 100;
            foreach (Joueur joueur in Joueurs)
            {
                if (joueur.NombreDeMouche < nombreDeMoucheMin)
                {
                    nombreDeMoucheMin = joueur.NombreDeMouche;
                    IndexJoueurGagnant = Joueurs.IndexOf(joueur);
                }
            }
            return IndexJoueurGagnant;
        }

        public void EcrireStat()
        {
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@"..\..\..\Statistique.txt"))
            {
                foreach (Joueur joueur in Joueurs)
                {
                    file.Write(joueur.Pseudo + " " + joueur.NombreDeMouche);
                }
            }
        }
    }
}
