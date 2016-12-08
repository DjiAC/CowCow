using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mow.Core
{

    /// <summary>
    /// Une carte spéciale est défini par son type et les méthodes associé à son type.
    /// </summary>

    class CarteSpeciale : Carte
    {

        //TODO Méethodes des cartes spéciales



        /// <summary>
        /// Une vache acrobate se met par dessus d'une autre qui porte le même numéro.
        /// </summary>
        /// <param name="Type"></param>
        public void Acrobate(TypeDeCarte Type)
        {

        }

        /// <summary>
        /// Une vache retardataire se met entre deux vaches dont l'écart des numéro est de 2 minimum.
        /// </summary>
        /// <param name="Type"></param>
        public void Retardataire(TypeDeCarte Type)
        {

        }

    }
}
