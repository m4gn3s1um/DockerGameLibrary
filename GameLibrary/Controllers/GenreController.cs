using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using GameLibraryAPI.Data;
using GameLibraryAPI.Models;
using MySql.Data.MySqlClient;

namespace GameLibraryAPI.Controllers;


[ApiController]
[Route("[controller]")]
public class GenreController : ControllerBase
{
    private DatabaseActions db;

    private readonly DatabaseActions _db;

        public GenreController(DatabaseActions db)
        {
            _db = db;
        }

        [HttpGet]
        [Route("GetGenres")]
        public IActionResult GetGenres()
        {
            try
            {
                using (MySqlConnection sqlConnection = new MySqlConnection(_db.getDBString()))
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "SELECT * FROM Genre";
                        cmd.Connection = sqlConnection;

                        sqlConnection.Open();
                        MySqlDataReader reader = cmd.ExecuteReader();

                        var genres = new List<Genre>();
                        while (reader.Read())
                        {
                            var genre = new Genre
                            {
                                GenreId = reader["GenreId"].ToString(),
                                GenreName = reader["GenreName"].ToString()
                            };
                            genres.Add(genre);
                        }

                        return Ok(genres);
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        
        
        [HttpGet("{id}")]
        public IActionResult GetGenre(Guid genreId)
        {
            try
            {
                using (MySqlConnection sqlConnection = new MySqlConnection(_db.getDBString()))
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "SELECT * FROM Genre WHERE GenreId = @GenreId";
                        cmd.Parameters.AddWithValue("@GenreId", genreId);
                        cmd.Connection = sqlConnection;

                        sqlConnection.Open();
                        MySqlDataReader reader = cmd.ExecuteReader();

                        var genres = new List<Genre>();
                        while (reader.Read())
                        {
                            var genre = new Genre
                            {
                                GenreId = reader["GenreId"].ToString(),
                                GenreName = reader["GenreName"].ToString()
                            };
                            genres.Add(genre);
                        }

                        return Ok(genres);
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    
        public class Genre
        {
            public string GenreId { get; set; }
            public string GenreName { get; set; }
        }
}