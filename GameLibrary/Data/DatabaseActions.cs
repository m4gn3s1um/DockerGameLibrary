namespace GameLibraryAPI.Data;

using MySql.Data.MySqlClient;
using System.Data.SqlClient;

public class DatabaseActions
{
    private static string connectionString;

    public DatabaseActions()
    {
        connectionString = "Server=localhost;Port=63306;Database=GameLib;Uid=root;Pwd=MY_S€cr€t_P@ssw0rD;";
    }

    public String getDBString()
    {
        return connectionString;
    }
}