using System;
using System.Collections.Generic;

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
    }
}

public class Casilla
{
    public ColorCasilla ColorCasilla { get; set; }
    public int Fila { get; set; }
    public int Columna { get; set; }

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

public enum ColorCasilla
{
    Blanco,
    Negro,
}