using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using GameLibraryAPI.Data;
using MySql.Data.MySqlClient;

namespace GameLibraryAPI.Controllers;


[ApiController]
[Route("[controller]")]
public class GameController : ControllerBase
{
    private readonly DatabaseActions _db;

        public GameController(DatabaseActions db)
        {
            _db = db;
        }

        [HttpPost]
        public IActionResult AddGame([FromBody] Game game)
        {
            try
            {
                using (MySqlConnection sqlConnection = new MySqlConnection(_db.getDBString()))
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "INSERT INTO Game VALUES (@GameId, @GameTitle, @ConsoleId, @GenreId)";
                        cmd.Parameters.AddWithValue("@GameId", game.gameId);
                        cmd.Parameters.AddWithValue("@GameTitle", game.gameTitle);
                        cmd.Parameters.AddWithValue("@ConsoleId", game.consoleId);
                        cmd.Parameters.AddWithValue("@GenreId", game.genreId);
                        cmd.Connection = sqlConnection;
                        
                        sqlConnection.Open();
                        MySqlDataReader reader = cmd.ExecuteReader();

                        var addedGame = new Game
                        {
                            gameId = reader["GameId"].ToString(),
                            gameTitle = reader["GameTitle"].ToString(),
                            consoleId = reader["ConsoleId"].ToString(),   
                            genreId = reader["GenreId"].ToString()
                        };

                        return Ok(addedGame);
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
            
        }

        [HttpGet]
        [Route("GetGames")]
        public IActionResult GetGames()
        {
            try
            {
                using (MySqlConnection sqlConnection = new MySqlConnection(_db.getDBString()))
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "SELECT * FROM Game";
                        cmd.Connection = sqlConnection;

                        sqlConnection.Open();
                        MySqlDataReader reader = cmd.ExecuteReader();

                        var games = new List<Game>();
                        while (reader.Read())
                        {
                            var game = new Game
                            {
                                gameId = reader["GameId"].ToString(),
                                gameTitle = reader["GameTitle"].ToString(),
                                consoleId = reader["ConsoleId"].ToString(),   
                                genreId = reader["GenreId"].ToString()
                            };
                            games.Add(game);
                        }

                        return Ok(games);
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        
        [HttpGet("{id}")]
        public IActionResult GetGame(Guid gameId)
        {
            try
            {
                using (MySqlConnection sqlConnection = new MySqlConnection(_db.getDBString()))
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "SELECT * FROM Game WHERE GameId = @GameId";
                        cmd.Parameters.AddWithValue("@GameId", gameId);
                        cmd.Connection = sqlConnection;

                        sqlConnection.Open();
                        MySqlDataReader reader = cmd.ExecuteReader();

                        var games = new List<Game>();
                        while (reader.Read())
                        {
                            var game = new Game
                            {
                                gameId = reader["GameId"].ToString(),
                                gameTitle = reader["GameTitle"].ToString(),
                                consoleId = reader["ConsoleId"].ToString(),   
                                genreId = reader["GenreId"].ToString()
                            };
                            games.Add(game);
                        }

                        return Ok(games);
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    
        [HttpGet("GetGames/{id}")]
        public IActionResult GetGameFromConsole(Guid consoleId)
        {
            try
            {
                using (MySqlConnection sqlConnection = new MySqlConnection(_db.getDBString()))
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "SELECT * FROM Game WHERE ConsoleId = @ConsoleId";
                        cmd.Parameters.AddWithValue("@ConsoleId", consoleId);
                        cmd.Connection = sqlConnection;

                        sqlConnection.Open();
                        MySqlDataReader reader = cmd.ExecuteReader();

                        var games = new List<Game>();
                        while (reader.Read())
                        {
                            var game = new Game
                            {
                                gameId = reader["GameId"].ToString(),
                                gameTitle = reader["GameTitle"].ToString(),
                                consoleId = reader["ConsoleId"].ToString(),   
                                genreId = reader["GenreId"].ToString()
                            };
                            games.Add(game);
                        }

                        return Ok(games);
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    
}

public class Game
{
    public String gameId { get; set; }
    
    public String gameTitle { get; set; }
    
    public String consoleId { get; set; }
    
    public String genreId { get; set; }
}