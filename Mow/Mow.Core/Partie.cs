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
        public Partie(){
            Console.WriteLine("===== La partie commence =====");
            for (int i=0; i<3; i++) {
                var MaManche = new Manche(i);
            }
        }

        public void Json() { 
        var json = System.IO.File.ReadAllText(@"C:\Users\Charles\Downloads\cartes.json"); // à modifier mettre la source

        var objects = JArray.Parse(json); // parse as array  
            foreach (JObject root in objects)
            {
                foreach (KeyValuePair<String, JToken> app in root)
                {
                    var appName = app.Key;
                    var type = (String)app.Value["Type"];
                    var nombre = (String)app.Value["Nombre"];
                    var mouche = (String)app.Value["Mouches"];

                    Console.WriteLine(appName);
                    Console.WriteLine(type);
                    Console.WriteLine(nombre);
                    Console.WriteLine(mouche);
                    Console.WriteLine("\n");

                }
            }
}

public List<Joueur> Joueurs = new List<Joueur>();
        
    }
}
