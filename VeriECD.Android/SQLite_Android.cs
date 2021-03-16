using System.IO;
using VeriECD.Droid.Data;
using VeriECD.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLite_Android))]
namespace VeriECD.Droid.Data
{
    public class SQLite_Android : ISQlite
    {
        public SQLite_Android() { }
        public SQLite.SQLiteConnection GetConnection()
        {
            var sqliteFilename = "TestDB.db3";
            string documentPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentPath, sqliteFilename);
            var conn = new SQLite.SQLiteConnection(path);
            return conn;
        }
    }
}
