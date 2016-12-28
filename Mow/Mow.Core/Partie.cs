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

public List<Joueur> Joueurs = new List<Joueur>();
        
    }
}
