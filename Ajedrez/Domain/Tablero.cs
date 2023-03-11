#nullable enable
using System.Collections.Generic;
using System.Linq;
using Ajedrez.Controller;

namespace Ajedrez.Domain;

public class Tablero
{
    public List<Casilla> Casillas { get; set; }

    public Tablero(int rows, int columns)
    {
        // crea las casillas
        Casillas = new List<Casilla>();

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                var color = (i + j) % 2;
                Casillas.Add(new Casilla(color, i, j));
            }
        }

        // eliminaré esto porque el tablero por defecto se debe construir vacío
        //SetPiezasToCasillas();
    }

    public void SetPiezasToCasillasDefault()
    {
        new TableroDefault(ColorCasilla.Blanco, Casillas).SetPiezasToCasillas();
        new TableroDefault(ColorCasilla.Negro, Casillas).SetPiezasToCasillas();
    }

    public void SetPieza(Piezas peon, Coordenada coordenada)
    {
        // busca la casilla que coincida con la coordenada
        var casilla = Casillas.First(j => j.Coordenada.X == coordenada.X && j.Coordenada.Y == coordenada.Y);
        // asigna la pieza
        casilla.Pieza = peon;
    }

    public void MovPieza(Coordenada coordenada, Coordenada movSelected)
    {
        // busca la casilla que coincida con la coordenada
        var casilla = Casillas.First(j => j.Coordenada.X == coordenada.X && j.Coordenada.Y == coordenada.Y);
        // asigna la pieza
        var pieza = casilla.Pieza;
        casilla.Pieza = null;

        // busca la casilla que coincida con la coordenada
        var casillaMov = Casillas.First(j => j.Coordenada.X == movSelected.X && j.Coordenada.Y == movSelected.Y);
        // asigna la pieza
        casillaMov.Pieza = pieza;
    }

    public Piezas? GetPieza(Coordenada coordenadaPeonAMover)
    {
        // busca la casilla que coincida con la coordenada
        var casilla = Casillas.First(j => j.Coordenada.X == coordenadaPeonAMover.X && j.Coordenada.Y == coordenadaPeonAMover.Y);
        return casilla.Pieza;
    }
}