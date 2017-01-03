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
        public bool Sens { get; set; } // Sens du déroulement - True = horaire / False = anti-horaire

        Stack<Carte> Pioche { get; set; } // Notre objet pioche est une stack
        List<Carte> TroupeauDeVache { get; set; } // C'est l'endroit où les joueurs posent leurs cartes

        public List<Joueur> Joueurs = new List<Joueur>(); 

        public int IndexJoueur = 0; // Le premier joueur dans la liste commence la partie
        public int LimiteDeMouche { get; set; } // Détermine la condition d'arrêt d'une partie

        public string TypeDePartie { get; set; }
        public string NomJoueur { get; set; }
        public int NbJoueursPartie { get; set; }

        public int NbManche { get; set; }

        public Partie(string TypePartie, int NbJoueurs, string nomJoueur, int NbMouche)
        {
            Pioche = new Stack<Carte>();
            TroupeauDeVache = new List<Carte>();

            Sens = true; // On inialise le sens
            LimiteDeMouche = NbMouche;
            NomJoueur = nomJoueur;
            NbJoueursPartie = NbJoueurs;
            TypeDePartie = TypePartie;

            // Console.WriteLine("===== La partie commence =====");

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

        public void JouerPartie()
        {
            CreerListeDeJoueur(TypeDePartie, 4, 1); // On créé la liste de joueur participant

            NbManche = 0;

            while (VerifierMouche() != true) // Une partie s'arrête quand la limite de mouche est atteinte par un joueur
            {
                CreerPioche(); // Création de la pioche

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
            NbManche++;
            DistribuerCarte(); // Au début d'une manche, on distribue les cartes
            string choix = ""; // Variable qui contient le choix d'un joueur
            IndexJoueur = 0;

            while (Pioche.Count != 0 || choix != "A") // Une manche s'arrête lorsque la pioche est vide et qu'un joueur ne peux plus jouer de vache
            {
                if (TroupeauDeVache.Count != 0) 
                    DeterminerJoueurActuel(); // On détermine le joueur qui va jouer à chaque tour selon le sens 

                Console.WriteLine("Troupeau");
                foreach (Carte carte in TroupeauDeVache)
                {

                    Console.WriteLine(carte.TypeDeCarte + " " + carte.NumeroDeCarte + " " + carte.NombreDeMouche); // On affiche le troupeau

                }

                Console.WriteLine(IndexJoueur);
                if (Joueurs.ElementAt(IndexJoueur).Type == "Humain") // Pour le cas d'un joueur humain
                {

                    bool erreur = true;

                    while (erreur == true)
                    {
                        try
                        {
                            do
                            {


                                Console.WriteLine("Le joueur " + Joueurs.ElementAt(IndexJoueur).Pseudo);
                                Console.WriteLine("Choississez la carte à jouer, (taper entre un chiffre entre 0 et 4 ou passer votre tour en tapent A)");
                                foreach (Carte carte in Joueurs.ElementAt(IndexJoueur).Main)
                                {
                                    Console.WriteLine(carte.TypeDeCarte + " " + carte.NumeroDeCarte + " " + carte.NombreDeMouche); // On affiche ses cartes
                                }
                                choix = Console.ReadLine(); // Il choisit la carte à jouer ou de ne pas jouer



                            } while (choix != "A" && JouerCarte(Joueurs.ElementAt(IndexJoueur), Joueurs.ElementAt(IndexJoueur).Main.ElementAt(int.Parse(choix))) == false); // S'il tape autre chose que demander ou qu'il ne peut pas jouer la carte qu'il a choisi, il doit recommencer
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
                            Console.WriteLine("Veillez entrer A ou un nombre entre 0 et 4.");
                        }
                        catch (IndexOutOfRangeException exception)
                        {
                            Console.WriteLine(exception.Message);
                            Console.WriteLine("Merci de saisir un nombre dans la bonne range.");
                        }
                    }
                }
                else if (Joueurs.ElementAt(IndexJoueur).Type == "Ordinateur") // Pour le cas de l'IA
                {
                    do
                    {
                        choix = JouerOrdinateurFaible(Joueurs.ElementAt(IndexJoueur)); // L'IA choisit une carte ou de ne pas jouer
                        
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
            int IndexCarte = 0; // Variable qui désigne l'index de la carte à jouer

            if (TroupeauDeVache.Count == 0 && (CarteJouee.TypeDeCarte != "VacheAcrobate" && CarteJouee.TypeDeCarte != "VacheRetardataire")) // Si le troupeau est vide et que la carte à jouer n'est pas une vache acrobate ou retardataire
            {
                TroupeauDeVache.Add(CarteJouee); // On pose la carte 
                JoueurActuelle.Main.Remove(CarteJouee); // On l'enlève de la main du joueur

                if (CarteJouee.TypeDeCarte == "VacheSerreFile")
                {
                    if (JoueurActuelle.Type == "Humain")
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

                    if (JoueurActuelle.Type == "Humain")
                        ChangerSens(); // On demande à changer le sens car c'est une carte spéciale

                    else Sens = !Sens;
                    return true;
                }

                else if (CarteJouee.NumeroDeCarte > MaximumDuTroupeau) // Si le numéro de la carte jouée est supérieur au maximum
                {
                    TroupeauDeVache.Add(CarteJouee); // On met la carte à la fin du troupeau
                    JoueurActuelle.Main.Remove(CarteJouee); // On l'enlève de la main du joueur

                    if (JoueurActuelle.Type == "Humain")
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
                if (JoueurActuelle.Type == "Humain")
                {
                    bool erreur = true;
                    while (erreur == true)
                    {
                        try
                        {



                            Console.WriteLine("Choississez où vous voulez poser votre carte : (Taper entre 0 et" + (TroupeauDeVache.Count - 1) + ")");
                            IndexCarte = int.Parse(Console.ReadLine()); // On récupère l'index de la carte où la vache acrobate doit être posée

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
                            Console.WriteLine("Veillez entrer A ou un nombre entre 0 et " + (TroupeauDeVache.Count - 1) + ".");
                        }
                        catch (IndexOutOfRangeException exception)
                        {
                            Console.WriteLine(exception.Message);
                            Console.WriteLine("Merci de saisir un nombre dans la bonne range.");
                        }
                    }
                }
                else
                {
                    IndexCarte = JouerCarteSpecialeOrdinateurFaible(JoueurActuelle, CarteJouee);
                }

                if (CarteJouee.NumeroDeCarte == TroupeauDeVache.ElementAt(IndexCarte).NumeroDeCarte) // Si les numéros sont les même
                {
                    TroupeauDeVache.Insert(IndexCarte, CarteJouee); // On la pose par dessus
                    JoueurActuelle.Main.Remove(CarteJouee); // On l'enlève de la main du joueur

                    if (JoueurActuelle.Type == "Humain")
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

            else if (CarteJouee.TypeDeCarte == "VacheRetardataire") // Pour le cas de la vache retardataire
            {
                if (TroupeauDeVache.Count >= 2) // Il faut qu'il y a ait 2 vaches au minimum
                {
                        if (JoueurActuelle.Type == "Humain")
                        {

                            bool erreur = true;
                            while (erreur == true)
                            {
                                try
                                {

                                    Console.WriteLine("Choississez où vous voulez poser votre carte : (Taper entre 0 et" + (TroupeauDeVache.Count - 2) + ")");
                                    IndexCarte = int.Parse(Console.ReadLine()); // On récupère l'index de la carte où la vache retardataire doit être posée

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
                                    Console.WriteLine("Veillez entrer A ou un nombre entre 0 et " + (TroupeauDeVache.Count - 2) + ".");
                                }
                                catch (IndexOutOfRangeException exception)
                                {
                                    Console.WriteLine(exception.Message);
                                    Console.WriteLine("Merci de saisir un nombre dans la bonne range.");
                                }
                            }
                        }
                        else
                        {
                            IndexCarte = JouerCarteSpecialeOrdinateurFaible(JoueurActuelle, CarteJouee);
                        }

                        if (TroupeauDeVache.ElementAt(IndexCarte + 1).NumeroDeCarte - TroupeauDeVache.ElementAt(IndexCarte).NumeroDeCarte >= 2) // Si l'écart entre les 2 cartes est supérieur à 2
                        {
                            TroupeauDeVache.Insert(IndexCarte, CarteJouee); // On insère la carte entre les deux autres concernées
                            JoueurActuelle.Main.Remove(CarteJouee); // On l'enlève de la main du joueur

                            if (JoueurActuelle.Type == "Humain")
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
        /// <param name="JoueurActuelle">Le joueur artificielle en cours</param>
        /// <returns>Retourne son choix de jeu</returns>
        public string JouerOrdinateurFaible(Joueur JoueurActuelle)
        {

            if (TroupeauDeVache.Count == 0)
            {
                foreach (Carte carte in JoueurActuelle.Main)
                {
                    if (carte.TypeDeCarte == "VacheNormale") 
                    {
                        return JoueurActuelle.Main.IndexOf(carte).ToString(); // Lorsque que le troupeau est vide, il posera une vache normale quelconque
                    }

                }

                foreach (Carte carte in JoueurActuelle.Main)
                {
                    if (carte.TypeDeCarte == "VacheSerreFile")
                    {
                        return JoueurActuelle.Main.IndexOf(carte).ToString(); // Pose une vache serre file s'il n'a pas d'autre choix (très rare)
                    }

                }

            }

            else
            {
                foreach (Carte carte in JoueurActuelle.Main)
                {
                    if (carte.TypeDeCarte == "VacheNormale") // L'ordinateur jouera en priorité un vache normale
                    {
                        int MinimumDuTroupeau = TroupeauDeVache.ElementAt(0).NumeroDeCarte; // On crée la variable qui correspond au minimum du troupeau
                        int MaximumDuTroupeau = TroupeauDeVache.ElementAt(TroupeauDeVache.Count - 1).NumeroDeCarte; // On crée la variable qui correspond au maximum du troupeau

                        if (JoueurActuelle.Main.ElementAt(JoueurActuelle.Main.IndexOf(carte)).NumeroDeCarte < MinimumDuTroupeau) // Si le numéro de la carte jouée est inférieur au minimum
                        {
                            


                            return JoueurActuelle.Main.IndexOf(carte).ToString(); // Retourne l'index d'une carte vache normale qui respecte la condition inférieure au troupeau 
                        }

                        else if (JoueurActuelle.Main.ElementAt(JoueurActuelle.Main.IndexOf(carte)).NumeroDeCarte > MaximumDuTroupeau) // Si le numéro de la carte jouée est supérieur au maximum
                        {

                            return JoueurActuelle.Main.IndexOf(carte).ToString(); // Retourne l'index d'une carte vache normale qui respecte la condition supérieure au troupeau
                        }
                    }
                }

                foreach (Carte carte in JoueurActuelle.Main)
                {
                    if (carte.TypeDeCarte == "VacheSerrefile") // Sinon il joue une serre file
                    {
                        return JoueurActuelle.Main.IndexOf(carte).ToString();
                    }
                    else if (carte.TypeDeCarte == "VacheAcrobate") // Sinon une acrobate
                    {
                        foreach (Carte cartevache in TroupeauDeVache)
                        {
                            if (JoueurActuelle.Main.ElementAt(JoueurActuelle.Main.IndexOf(carte)).NumeroDeCarte == cartevache.NumeroDeCarte)
                            {
                                return JoueurActuelle.Main.IndexOf(carte).ToString(); // Retourne l'index de la vache acrobate si la condition qu'il y a ait une carte du même numéro dans le troupeau
                            }
                        }
                    }
                    else if (carte.TypeDeCarte == "VacheRetardataire")
                    {
                        foreach (Carte cartevache in TroupeauDeVache)
                        {
                            int limite = (TroupeauDeVache.Count - 2);
                            if (TroupeauDeVache.IndexOf(cartevache) < limite && TroupeauDeVache.Count >=2)
                            {
                                if (TroupeauDeVache.ElementAt(TroupeauDeVache.IndexOf(cartevache) + 1).NumeroDeCarte - cartevache.NumeroDeCarte >= 2)
                                {
                                    return JoueurActuelle.Main.IndexOf(carte).ToString(); // Retourne l'index de la vache retardataire si la condition qu'il y a ait 2 cartes dans le troupeau et que l'écarte entre 2 cartes et >2 
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

                CreerProfil(NomJoueur); // On crée son profile puis on l'ajoute à la liste des joueurs


                for (int i = 0; i < nombreOrdinateur; i++) 
                    CreerProfilOrdinateur(i);  // On crée autant de joueurs artificielles que le nombre demandé


            }
            else if (typePartie == "multi") // Il y a plusieurs utilisateurs
            {
                for (int i = 0; i < nombreUtilisateur; i++) 
                    CreerProfil(NomJoueur); // On crée autant de profile que le nombre demandé

                for (int i = 0; i < nombreOrdinateur; i++)
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
            string choix = "";
            bool erreur = true;
            while (erreur == true)
            {


                Console.WriteLine("Voulez-vous changer de sens? Taper Oui ou Non");
                choix = Console.ReadLine();
                if (choix == "Oui" || choix == "Non")
                {
                    erreur = false;
                }
                else
                {
                    Console.WriteLine("Veuillez Répondre par Oui ou par Non !");
                }
            }

            if (choix == "Oui")
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
