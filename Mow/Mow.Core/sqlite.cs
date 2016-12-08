using SQLite.Net;
using SQLite.Net.Platform.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mow.Core
{
    class sqlite
    {
            var db = new SQLiteConnection(new SQLitePlatformWin32(), "c://temp//mydb.sqlite");
            db.CreateTable<Statscore>();
            db.CreateTable<Statjoueur>();
    }
}
