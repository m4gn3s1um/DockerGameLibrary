using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using GameLibraryAPI.Data;
using System.Collections.Generic;

namespace GameLibraryAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConsoleController : ControllerBase
    {
        private readonly DatabaseActions _db;

        public ConsoleController(DatabaseActions db)
        {
            _db = db;
        }

        [HttpGet]
        [Route("GetConsoles")]
        public IActionResult GetConsoles()
        {
            try
            {
                using (MySqlConnection sqlConnection = new MySqlConnection(_db.getDBString()))
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "SELECT * FROM Console";
                        cmd.Connection = sqlConnection;

                        sqlConnection.Open();
                        MySqlDataReader reader = cmd.ExecuteReader();

                        var consoles = new List<Console>();
                        while (reader.Read())
                        {
                            var console = new Console
                            {
                                ConsoleId = reader["ConsoleId"].ToString(),
                                ConsoleName = reader["ConsoleName"].ToString()
                            };
                            consoles.Add(console);
                        }

                        return Ok(consoles);
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        
        [HttpGet("{id}")]
        public IActionResult GetConsole(Guid consoleId)
        {
            try
            {
                using (MySqlConnection sqlConnection = new MySqlConnection(_db.getDBString()))
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "SELECT * FROM Console WHERE ConsoleId = @ConsoleId";
                        cmd.Parameters.AddWithValue("@ConsoleId", consoleId);
                        cmd.Connection = sqlConnection;

                        sqlConnection.Open();
                        MySqlDataReader reader = cmd.ExecuteReader();

                        var consoles = new List<Console>();
                        while (reader.Read())
                        {
                            var console = new Console
                            {
                                ConsoleId = reader["ConsoleId"].ToString(),
                                ConsoleName = reader["ConsoleName"].ToString()
                            };
                            consoles.Add(console);
                        }

                        return Ok(consoles);
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
    
    
    

    public class Console
    {
        public string ConsoleId { get; set; }
        public string ConsoleName { get; set; }
    }
}