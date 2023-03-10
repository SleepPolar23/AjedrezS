#nullable enable
using System;
using Ajedrez.Controller;

namespace Ajedrez.Domain;

public class Casilla
{
    public ColorCasilla ColorCasilla { get; set; }
    public int Fila { get; set; }
    public int Columna { get; set; }
    public Piezas? Pieza { get; set; }

    public Casilla(int color, int fila, int columna)
    {
        if (Enum.TryParse<ColorCasilla>(color.ToString(), out var colorEnum))
        {
            ColorCasilla = colorEnum;
        }
        else
        {
            throw new Exception("No se pudo convertir el color");
        }

        Fila = fila;
        Columna = columna;
    }
}