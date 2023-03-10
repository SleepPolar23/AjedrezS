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

    public Coordenada[] GetPosiblesMovimientosParaComer(Coordenada coordenadaPeonAMover)
    {
        Coordenada[] movs = new Coordenada[2];
        movs[0] = new Coordenada(coordenadaPeonAMover.X + 1, coordenadaPeonAMover.Y + 1);
        movs[1] = new Coordenada(coordenadaPeonAMover.X + 1, coordenadaPeonAMover.Y - 1);
        return movs;
    }
}