namespace GameLibraryAPI.Models;

public class Game
{
    public Guid gameId { get; set; }
    
    public String gameTitle { get; set; }
    
    public Guid consoleId { get; set; }
    
    public Guid genreId { get; set; }
}