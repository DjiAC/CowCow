using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mow.Core
{

    /// <summary>
    /// Une Carte est définie par un type, un numéro et un nombre de mouche.
    /// </summary>
    class Carte 
    {
        // propriété des cartes par C

        public enum TypeDeCarte { VacheNormale, VacheAcrobate, VacheRetardataire, VacheSerreFile}   // type de carte par C

        public int NumeroDeCarte { get; set; }        // numéro de la carte par C

        public int NombreDeMouche { get; set; }      // nombre de mouche par C

      

    }
}
