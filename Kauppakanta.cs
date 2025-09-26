using Microsoft.AspNetCore.Http.HttpResults;
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
    //Metodi, jolla listaöön asiakas tietokantan

    public void LisaaAsiakas(string nimi)
    {
        using (var connection = new SqliteConnection(_connectionstring))
        {
            connection.Open();
            //Lisäätän asiakas tietokantan

            var insertcmd = connection.CreateCommand();
            insertcmd.CommandText = @"INSERT INTO Asiakaat (nimi) VALUES ($nimi)";
            insertcmd.Parameters.AddWithValue($"nimi", nimi);
            insertcmd.ExecuteNonQuery();
        }
    }
    public Dictionary<int, string> HaeAsiakaat()
    {
        using (var connection = new SqliteConnection(_connectionstring))
        {
            connection.Open();

            //Haetaan kaikki asiakaat tietokannasta

            var selectcmd = connection.CreateCommand();
            selectcmd.CommandText = "SELECT * FROM Asiakaat";
            using (var reader = selectcmd.ExecuteReader())
            {
                var asiakaat = new Dictionary<int, string>();
                while (reader.Read())
                {
                    asiakaat.Add(reader.GetInt32(0), reader.GetString(1));
                }
                return asiakaat;
            }
        }
    }
}