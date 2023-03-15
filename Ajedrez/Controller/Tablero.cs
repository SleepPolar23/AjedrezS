#nullable enable
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Ajedrez;

namespace Ajedrez.Controller;

public class Tablero
{
    public static Point ultimaPosicion;
    public static List<Point> listaPuedeMover = new List<Point>();
    public static int turno = -1;

    public Casilla[,] Casillas { get; set; }

    public Tablero(int size, int rows, int columns, Panel PanelTablero)
    {
        // crea las casillas
        Casillas = new Casilla[rows, columns];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                var color = (i + j) % 2;
                Casillas[i, j] = new Casilla(color, size, i, j, Casillas);
                PanelTablero.Controls.Add(Casillas[i, j].pb);
            }
        }

        SetPiezasToCasillas();
    }

    private void SetPiezasToCasillas()
    {
        new TableroDefault(TipoCasilla.Blanco, Casillas).SetPiezasToCasillas();
        new TableroDefault(TipoCasilla.Negro, Casillas).SetPiezasToCasillas();
    }
}