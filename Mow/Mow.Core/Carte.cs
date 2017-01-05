using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mow.Core
{

    /// <summary>
    /// Une Carte est définie par un type, un numéro et un nombre de mouche.
    /// </summary>
    public class Carte : INotifyPropertyChanged
    {
        private string typedecarte;
        public string TypeDeCarte
        {
            get { return typedecarte; }
            set
            {                
                typedecarte = value;
                NotifyPropertyChanged("TypeDeCarte");                
            }
        }

        private int numerodecarte;
        public int NumeroDeCarte
        {
            get { return numerodecarte; }
            set
            {
                numerodecarte = value;
                NotifyPropertyChanged("NumeroDeCarte");
            }
        }

        private int nombredemouche;
        public int NombreDeMouche
        {
            get { return nombredemouche; }
            set
            {
                nombredemouche = value;
                NotifyPropertyChanged("NombreDeMouche");
            }
        }

        public string Adresse; // adresse de la carte   

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
