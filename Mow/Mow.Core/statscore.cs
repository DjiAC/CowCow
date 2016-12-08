using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mow.Core
{
    public class Statscore
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(11)]
        public string name { get; set; }
    }
}
