using Microsoft.Data.Sqlite;

class Kauppakanta
{
    //Tieto kantayhteyttä _connectionstring
    private static string _connectionstring = "Data Source=kauppa.db";

    //constructor

    public Kauppakanta()
    {
        //Luoda tietokantayhteys

        using (var connection = new SqliteConnection(_connectionstring))
        {
            connection.Open();

            //Luodaan asiakaat  -taulu jos sitä ei ole

            var tablecmd = connection.CreateCommand();
            tablecmd.CommandText = @"Create TABLE IF NOT EXISTS
            Asiakaat (id INTEGER PRIMARY KEY, nimi TEXT)";
            tablecmd.ExecuteNonQuery();
        }
    }
}