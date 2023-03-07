#nullable enable
using System.Collections.Generic;
using System.Linq;

namespace Ajedrez.Controller;

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

        SetPiezasToCasillas();
    }

    private void SetPiezasToCasillas()
    {
        new TableroDefault(ColorCasilla.Blanco, Casillas).SetPiezasToCasillas();
        new TableroDefault(ColorCasilla.Negro, Casillas).SetPiezasToCasillas();
    }
}