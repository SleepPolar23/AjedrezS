using System.Linq;
using Ajedrez.Domain;

namespace Ajedrez.Controller;

public class LogicGame
{
    public Coordenada[] GetPosibleMovs(Piezas peon, Coordenada coordenada)
    {
        var movs = peon.Movimientos;
        return movs.Select(mov =>
            new Coordenada(coordenada.X + mov.X, coordenada.Y + mov.Y)
        ).ToArray();
    }

    public bool PuedoMoverme(Coordenada movSelected, Tablero tablero)
    {
        return !tablero.Casillas.Any(j => j.Coordenada.X == movSelected.X && j.Coordenada.Y == movSelected.Y);
    }
}