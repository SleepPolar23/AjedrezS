#nullable enable
using System;
using Ajedrez.Controller;

namespace Ajedrez.Domain;

public class Casilla
{
    public ColorCasilla ColorCasilla { get; set; }
    public Coordenada Coordenada { get; set; }
    public Piezas? Pieza { get; set; }
    public EstadoCasilla Estado { get; set; }

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

        Coordenada = new Coordenada(fila, columna);
    }
}

public enum EstadoCasilla
{
    Normal,
    Seleccionada,
    PosibleMovimiento,
    PosbileComer,
}