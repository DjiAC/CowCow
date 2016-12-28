﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mow.Core
{

    /// <summary>
    /// Une carte spéciale est défini par son type et les méthodes associé à son type.
    /// </summary>

    class CarteSpeciale : Carte     // Fait par C
    {

        //TODO Méethodes des cartes spéciales



        /// <summary>
        /// Une vache acrobate se met par dessus d'une autre qui porte le même numéro.
        /// </summary>
        /// <param name="Type"></param>
        public void Acrobate(String Type, ref Troupeau TroupeauDeVache, int Index,int Numero, Carte CarteActuelle) // un peu trop de paramètre je pense
        {
            if (Type == "VacheAcrobate" && Numero == CarteActuelle.NumeroDeCarte)
            {
                TroupeauDeVache.TroupeauDeVache.Insert(Index, CarteActuelle);
            }
        }

        /// <summary>
        /// Une vache retardataire se met entre deux vaches dont l'écart des numéros est de 2 minimum.
        /// </summary>
        /// <param name="Type"></param>
        public void Retardataire(String Type, ref Troupeau TroupeauDeVache, int Index,Carte CarteActuelle) // de même
        {
            if (Type == "VacheRetardataire")
            {
                TroupeauDeVache.TroupeauDeVache.Insert(Index, CarteActuelle);
            }
        }

    }
}
