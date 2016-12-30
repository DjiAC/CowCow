using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Mow.Core
{
    class Partie
    {
        public bool Sens { get; set; } // Sens du déroulement - True = horaire / False = anti-horaire

        Stack<Carte> Pioche { get; set; } // Notre objet pioche est une stack
        List<Carte> TroupeauDeVache { get; set; }

        public List<Joueur> Joueurs = new List<Joueur>();

        public int Index = -1;
        public string CarteIndex { get; set; }
        public int LimiteDeMouche { get; set; }

        public Partie(){

            Pioche = new Stack<Carte>();
            TroupeauDeVache = new List<Carte>();

            Console.WriteLine("===== La partie commence =====");
            for (int i=0; i<3; i++) {
                // var MaManche = new Manche(i);


            }
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

                            NouvelleCarte.NumeroDeCarte = int.Parse(Numero);  //  On instancie son numéro

                        NouvelleCarte.NombreDeMouche = int.Parse(Mouche);   // On instancie son nombre de mouche
                        Pioche.Push(NouvelleCarte);                         // On ajoute la carte dans la pioche
                    }

                    else
                    {
                        CarteSpeciale NouvelleCarteSpeciale = new CarteSpeciale();                  // On crée une nouvelle carte spéciale

                        NouvelleCarteSpeciale.TypeDeCarte = Type;                 //  On instancie son type

                        if (Numero != "")                                   // Certaines cartes n'ont pas de numéro
                            NouvelleCarteSpeciale.NumeroDeCarte = int.Parse(Numero);  //  On instancie son numéro

                        NouvelleCarteSpeciale.NombreDeMouche = int.Parse(Mouche);   // On instancie son nombre de mouche
                        Pioche.Push(NouvelleCarteSpeciale);                         // On ajoute la carte dans la pioche
                    }

                }
            }
                Pioche.OrderBy(a => Guid.NewGuid()); // On mélange la pioche à la fin
           
        }

        public void JouerPartie()
        {
            while (VerifierMouche(LimiteDeMouche))
            {
                CreerPioche();

                JouerManche();             

                ViderCartes();

            }
        }

        public void JouerManche()
        {
            DistribuerCarte();

            while (Pioche.Count != 0 && CarteIndex != "A")
            {
                DeterminerJoueurActuelle();

                if (Joueurs.ElementAt(Index).Type == "Humain")
                {
                    Console.WriteLine("Choississez la carte à jouer, (taper entre un chiffre entre 0 et 4 ou passer votre tour en tapent A)");
                    foreach (Carte carte in Joueurs.ElementAt(Index).Main)
                    {
                        Console.WriteLine(carte.TypeDeCarte);
                        Console.WriteLine(carte.NumeroDeCarte);
                        Console.WriteLine(carte.NombreDeMouche);
                    }
                    CarteIndex = Console.ReadLine();
                }
                if (CarteIndex != "A")
                {
                    while (JouerCarte(Joueurs.ElementAt(Index), Joueurs.ElementAt(Index).Main.ElementAt(int.Parse(CarteIndex))) == false) ;
                    if (Pioche.Count != 0)
                    Joueurs.ElementAt(Index).Main.Add(Pioche.Pop());
                }
                else
                {
                    AjouterDansEtable(Joueurs.ElementAt(Index));
                }

                CompterMouches();
            }
        }

        public bool JouerCarte(Joueur JoueurActuelle, Carte CarteJouee)
        {
            if (TroupeauDeVache.Count != 0)
            {
                TroupeauDeVache.Add(CarteJouee);
                JoueurActuelle.Main.Remove(CarteJouee);
            }

            else if (CarteJouee.TypeDeCarte == "VacheNormale" || CarteJouee.TypeDeCarte == "VacheSerreFile")
            {
                int MinimumDuTroupeau = TroupeauDeVache.ElementAt(0).NumeroDeCarte;
                int MaximumDuTroupeau = TroupeauDeVache.ElementAt(TroupeauDeVache.Count - 1).NumeroDeCarte;

                if (CarteJouee.NumeroDeCarte < MinimumDuTroupeau)
                {
                    TroupeauDeVache.Insert(0, CarteJouee);
                    JoueurActuelle.Main.Remove(CarteJouee);
                }

                else if (CarteJouee.NumeroDeCarte > MaximumDuTroupeau)
                {
                    TroupeauDeVache.Add(CarteJouee);
                    JoueurActuelle.Main.Remove(CarteJouee);
                }
                else
                {
                    Console.WriteLine("Erreur votre carte doit être placé en dehors de l'intervalle !");
                    return false;
                }

         
            }

            else if (CarteJouee.TypeDeCarte == "VacheAcrobate")
            {
               //TODO

            }

            else if (CarteJouee.TypeDeCarte == "VacheRetardataire")
            {
                //TODO

            }
            return true;
        }

        public void DistribuerCarte()
        {
            for (int i = 0; i < 5; i++)
            {
                foreach (Joueur joueur in Joueurs)
                {
                    joueur.Main.Add(Pioche.Pop());
                }
            }
        }

        public void DeterminerJoueurActuelle()
        {
            if (Sens)
            {
                
                Index++;
                if (Index == Joueurs.Count)
                    Index = 0;
            }
            
            else
            {
                Index--;
                if (Index == -1)
                    Index = Joueurs.Count - 1;
            } 
        }

        public void AjouterDansEtable(Joueur JoueurActuelle)
        {
            foreach (Carte carte in TroupeauDeVache)
            {
                JoueurActuelle.Etable.Add(carte);
            }
        }

        public void CompterMouches()
        {
            foreach (Joueur joueur in Joueurs)
            {
                foreach (Carte carte in joueur.Main)
                {
                    joueur.NombreDeMouche += carte.NombreDeMouche;
                }
                foreach (Carte carte in joueur.Etable)
                {
                    joueur.NombreDeMouche += carte.NombreDeMouche;
                }
            }
        }

        public void ViderCartes()
        {
            TroupeauDeVache.Clear();

            foreach (Joueur joueur in Joueurs)
            {
                joueur.Main.Clear();
                joueur.Etable.Clear();

            }
        }

        public bool VerifierMouche()
        {
            foreach (Joueur joueur in Joueurs)
            {
                if (joueur.NombreDeMouche >= LimiteDeMouche)
                    return true;
            }
            return false;
        }
    }
}
