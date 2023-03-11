using System.Linq;
using Ajedrez.Domain;

namespace Ajedrez.Controller;

public class LogicGame
{
    public Coordenada[] GetPosibleMovs(Piezas peon, Coordenada coordenada)
    {
        var movs = peon.Movimientos;
        return movs.Select(mov => new Coordenada(coordenada.X + mov.X, coordenada.Y + mov.Y)).ToArray();
    }

    public Coordenada[] GetPosiblesMovsDondeCome(Piezas peon, Coordenada coordenada)
    {
        var movs = peon.MovimientosDondeCome;
        return movs.Select(mov => new Coordenada(coordenada.X + mov.X, coordenada.Y + mov.Y)).ToArray();
    }

    public bool PuedoMoverme(Coordenada movSelected, Tablero tablero)
    {
        return !tablero.Casillas.Any(j => j.Coordenada.X == movSelected.X && j.Coordenada.Y == movSelected.Y);
    }

    public Coordenada[] GetPosiblesMovimientosParaComer(Tablero tablero, Coordenada coordenadaPeonAMover)
    {
        // si en los posibles movimientos existe una fisha, entonces se lo puede comer
        var posiblesMovs = GetPosiblesMovsDondeCome(tablero.GetPieza(coordenadaPeonAMover), coordenadaPeonAMover);
        return posiblesMovs.Where(mov =>
        {
            var piezaDondeIntersectara = tablero.GetPieza(mov);
            var piezaSeleccionadaAMover = tablero.GetPieza(coordenadaPeonAMover);
            return piezaDondeIntersectara != null && piezaDondeIntersectara._equipo != piezaSeleccionadaAMover?._equipo;
        }).ToArray();
    }
}