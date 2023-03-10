using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Ajedrez.Domain;

public class Game
{
    public Tablero Tablero { get; set; }

    public Game()
    {
        Tablero = new Tablero(8, 8);
    }

    public Coordenada[] GetPosiblesMov(Coordenada coordenada)
    {
        // busco la pieza
        var casilla = Tablero.Casillas.First(j => j.Coordenada == coordenada);

        // la pieza conoce su propio movimiento
		// si es un peón retorna x = 0, y = 1; o x = 0 , y = 2 si es movimiento especial
        var movs = casilla.Pieza.Movimientos;

		return movs.Select(mov => 
            new Coordenada(coordenada.X + mov.X, coordenada.Y + mov.Y)
        ).ToArray();
	}
}