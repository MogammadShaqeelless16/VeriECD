using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using SQLitePCL;

namespace VeriECD.Services
{
    public interface ISQlite
    {
        SqliteConnection GetConnection();
    }
}
