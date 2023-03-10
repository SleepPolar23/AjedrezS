using System.Collections.Generic;
using System.Linq;

namespace Ajedrez.Domain;

public class TableroDefault
{
    public int filaPeones;
    int filaNoPeones;

    private readonly PiezasFactory _builderPiezaBlanca;
    private readonly IEnumerable<Casilla> _casillas;

    public TableroDefault(ColorCasilla colorPlayer, IEnumerable<Casilla> casillas)
    {
        filaPeones = colorPlayer == ColorCasilla.Blanco ? 1 : 6;
        filaNoPeones = colorPlayer == ColorCasilla.Blanco ? 0 : 7;

        if (colorPlayer is ColorCasilla.Blanco)
        {
            _builderPiezaBlanca = PiezasFactory.Builder.Create().PiezaBlanca().Build();
        }

        if (colorPlayer is ColorCasilla.Negro)
        {
            _builderPiezaBlanca = PiezasFactory.Builder.Create().PiezaNegra().Build();
        }

        _casillas = casillas;
    }

    private void SetPeones()
        => _casillas.Where(j => j.Coordenada.Y == filaPeones).ToList().ForEach(j => j.Pieza = _builderPiezaBlanca.Peones);

    private void SetTorres()
        => _casillas.Where(j => j.Coordenada.Y == filaNoPeones && j.Coordenada.X == 0 || j.Coordenada.Y == filaNoPeones && j.Coordenada.X == 7)
            .ToList()
            .ForEach(j => j.Pieza = _builderPiezaBlanca.Torres);

    private void SetCaballo()
        => _casillas.Where(j => j.Coordenada.Y == filaNoPeones && j.Coordenada.X == 1 || j.Coordenada.Y == filaNoPeones && j.Coordenada.X == 6)
            .ToList()
            .ForEach(j => j.Pieza = _builderPiezaBlanca.Caballo);

    private void SetAlfil()
        => _casillas.Where(j => j.Coordenada.Y == filaNoPeones && j.Coordenada.X == 2 || j.Coordenada.Y == filaNoPeones && j.Coordenada.X == 5)
            .ToList()
            .ForEach(j => j.Pieza = _builderPiezaBlanca.Alfil);

    private void SetReina()
        => _casillas.Where(j => j.Coordenada.Y == filaNoPeones && j.Coordenada.X == 3).ToList()
            .ForEach(j => j.Pieza = _builderPiezaBlanca.Reina);

    private void SetRey()
        => _casillas.Where(j => j.Coordenada.Y == filaNoPeones && j.Coordenada.X == 4).ToList()
            .ForEach(j => j.Pieza = _builderPiezaBlanca.Rey);

    public void SetPiezasToCasillas()
    {
        SetPeones();
        SetTorres();
        SetCaballo();
        SetAlfil();
        SetReina();
        SetRey();
    }
}