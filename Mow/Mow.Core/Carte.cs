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
            get { return this.typedecarte; }
            set
            {
                if (this.typedecarte != value)
                {
                    this.typedecarte = value;
                    this.NotifyPropertyChanged("TypeDeCarte");
                }
            }
        }

        private string numerodecarte;
        public string NumeroDeCarte
        {
            get { return this.numerodecarte; }
            set
            {
                if (this.numerodecarte != value)
                {
                    this.numerodecarte = value;
                    this.NotifyPropertyChanged("NumeroDeCarte");
                }
            }
        }

        private string nombredemouche;
        public string NombreDeMouche
        {
            get { return this.nombredemouche; }
            set
            {
                if (this.nombredemouche != value)
                {
                    this.nombredemouche = value;
                    this.NotifyPropertyChanged("NombreDeMouche");
                }
            }
        }

        public string Adresse; // adresse de la carte   

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
