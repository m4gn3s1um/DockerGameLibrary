USE GameLib;

CREATE TABLE Game (
                      GameId VARCHAR(50) PRIMARY KEY,
                      GameName VARCHAR(50) NOT NULL,
                      ConsoleId VARCHAR(50),
                      GenreId VARCHAR(50)
);