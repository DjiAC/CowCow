using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite.Net.Attributes;
using System.Threading.Tasks;

namespace Mow.Core
{
    public class Statjoueur
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(11)]
        public int score { get; set; }
        [MaxLength(11)]
        public int Id { get; set; }
        [MaxLength(11)]
    }
}
