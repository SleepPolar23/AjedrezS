namespace Ajedrez.Domain;

public class Game
{
    public Tablero Tablero { get; set; }

    public Game()
    {
        Tablero = new Tablero(8, 8);
    }
}