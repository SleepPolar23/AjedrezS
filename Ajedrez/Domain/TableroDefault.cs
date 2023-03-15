using Ajedrez.Controller.Enums.ParaPieza;
using Ajedrez.PosiblesMovimientosDeLaPieza;
using System;

namespace Ajedrez.Domain;

public class TableroDefault
{
    private int filaPeones, filaNoPeones;
    private ColorDePieza colorPieza;

    private readonly PiezasFactory _builderPieza;
    private readonly Casilla[,] _casillas;

    public TableroDefault(TipoCasilla colorPlayer, Casilla[,] casillas)
    {
        filaPeones = colorPlayer == TipoCasilla.Blanco ? 1 : 6;
        filaNoPeones = colorPlayer == TipoCasilla.Blanco ? 0 : 7;

        if (colorPlayer is TipoCasilla.Blanco)
        {
            _builderPieza = PiezasFactory.Builder.Create().PiezaBlanca().Build();
            colorPieza = ColorDePieza.Blanco;
        }

        if (colorPlayer is TipoCasilla.Negro)
        {
            _builderPieza = PiezasFactory.Builder.Create().PiezaNegra().Build();
            colorPieza = ColorDePieza.Negro;
        }
        
        _casillas = casillas;
    }

    private void SetPeones()
<<<<<<< HEAD:Ajedrez/Controller/TableroDefault.cs
    {
        for(int i = 0; i < _casillas.GetLength(0); i++)
        {
            if (i != filaPeones) continue;

            for(int j = 0; j < _casillas.GetLength(1); j++)
            {
                _casillas[i, j].Pieza = _builderPieza.Peones;
                _casillas[i, j].Pieza.Color = (int)colorPieza;
            }
            return;
        }
    }

    private void SetCaballo()
        => ColocarPieza(_builderPieza.Caballo, 1, 6);

    private void SetAlfil()
        => ColocarPieza(_builderPieza.Alfil, 2, 5);

    private void SetTorres()
        => ColocarPieza(_builderPieza.Torres, 0, 7);

    private void SetReina()
        => ColocarPieza(_builderPieza.Reina, 4, 4);

    private void SetRey()
        => ColocarPieza(_builderPieza.Rey, 3, 3);
=======
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
>>>>>>> 0d0ef351401b3d530e17fbe7829bb990070fc233:Ajedrez/Domain/TableroDefault.cs


    #region ColocarPieza
    private void ColocarPieza(Piezas builderPieza, int columna1, int columna2)
    {
        for (int i = 0; i < _casillas.GetLength(0); i++)
        {
            if (i != filaNoPeones) continue;

            for (int j = 0; j < _casillas.GetLength(1); j++)
            {
                if (j == columna1 || j == columna2) 
                { 
                    _casillas[i, j].Pieza = builderPieza;
                    _casillas[i, j].Pieza.Color = (int)colorPieza;
                }
            }
            return;
        }
    }
    #endregion


    #region SetPiezasToCasillas
    public void SetPiezasToCasillas()
    {
        SetPeones();
<<<<<<< HEAD:Ajedrez/Controller/TableroDefault.cs
=======
        /*
        SetTorres();
>>>>>>> 0d0ef351401b3d530e17fbe7829bb990070fc233:Ajedrez/Domain/TableroDefault.cs
        SetCaballo();
        SetAlfil();
        SetTorres();
        SetReina();
        SetRey();
        */
    }
    #endregion
}