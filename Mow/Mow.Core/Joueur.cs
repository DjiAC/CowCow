using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mow.Core
{
    public class Joueur
    {
        public Joueur(){
            NomAleatoire = new List<string>();
            NomAleatoire.Add("Daenerys");
            NomAleatoire.Add("Negan");
            NomAleatoire.Add("Savitar");
            NomAleatoire.Add("Robert");
           
        }

        public string Type { get; set; } // Pour savoir si c'est un utilisateur ou IA par C

        public string Nom {get; set;} 

        public string Prenom { get; set; } 

        public string Pseudo { get; set; } 

        public int NombreDeMouche { get; set; } // Un joueur a un nombre de mouche qu'il accumule au fur de la partie

        public List<Carte> Main { get; set; } // Main d'un joueur

        public List<Carte> Etable { get; set; } // Les cartes qu'il récupère lorqu'il ne joue pas de vache

        public List<String> NomAleatoire; // Liste de nom aléatoire pour les joueurs artificielles
    }
}
