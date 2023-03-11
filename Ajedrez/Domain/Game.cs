using System.Linq;
using Ajedrez.Controller;

namespace Ajedrez.Domain;

public class Game
{
    private readonly LogicGame _logicGame;
    private ITableroObserver? _observer;
    public Tablero Tablero { get; set; }
    public Casilla CasillaSelected { get; set; }

    public Game(Tablero tablero)
    {
        Tablero = tablero;
        _logicGame = new LogicGame();
    }

    public Coordenada[] GetPosiblesMov(Coordenada coordenada)
    {
        // busco la pieza
        var casilla = Tablero.Casillas.First(j => j.Coordenada.X == coordenada.X && j.Coordenada.Y == coordenada.Y);

        return _logicGame.GetPosibleMovs(casilla.Pieza, coordenada);
    }

    public void SetCasillaSeleccionada(Coordenada coordenadaPeonSeleccionado)
    {
        var casilla = Tablero.Casillas.First(j =>
            j.Coordenada.X == coordenadaPeonSeleccionado.X && j.Coordenada.Y == coordenadaPeonSeleccionado.Y);
        CasillaSelected = casilla;
        RefreshTablero();
    }

    public void RefreshTablero()
    {
        var posiblesMovs = _logicGame.GetPosibleMovs(CasillaSelected.Pieza, CasillaSelected.Coordenada);
        var posiblesMovsComer = _logicGame.GetPosiblesMovimientosParaComer(Tablero, CasillaSelected.Coordenada);

        // las coordenadas seleccionada si coincide con una casilla del tablero, se pone en estado seleccionada
        foreach (var posiblesMov in posiblesMovs)
        {
            var casilla = Tablero.Casillas.First(j =>
                j.Coordenada.X == posiblesMov.X && j.Coordenada.Y == posiblesMov.Y);
            casilla.Estado = EstadoCasilla.PosibleMovimiento;
        }

        // las coordenadas seleccionada si coincide con una casilla del tablero, se pone en estado seleccionada
        foreach (var posiblesMovComer in posiblesMovsComer)
        {
            var casilla = Tablero.Casillas.First(j =>
                j.Coordenada.X == posiblesMovComer.X && j.Coordenada.Y == posiblesMovComer.Y);
            casilla.Estado = EstadoCasilla.PosbileComer;
        }

        // set casilla seleccionada
        CasillaSelected.Estado = EstadoCasilla.Seleccionada;

        _observer?.TableroCambio(Tablero);
    }

    public void AddObserverTablero(ITableroObserver mockObject)
    {
        _observer = mockObject;
    }

    public void MoverPieza(Coordenada coordenadaDestino)
    {
        Tablero.MovPieza(CasillaSelected.Coordenada, coordenadaDestino);
        _observer?.TableroCambio(Tablero);
    }
}

public interface ITableroObserver
{
    void TableroCambio(Tablero tablero);
}