using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Ajedrez.Controller;

namespace Ajedrez.Domain;

public class Game
{
    private readonly LogicGame _logicGame;
    public Tablero Tablero { get; set; }

    public Game()
    {
        Tablero = new Tablero(8, 8);
        _logicGame = new LogicGame();
    }

    public Coordenada[] GetPosiblesMov(Coordenada coordenada)
    {
        // busco la pieza
        var casilla = Tablero.Casillas.First(j => j.Coordenada.X == coordenada.X && j.Coordenada.Y == coordenada.Y);

        return _logicGame.GetPosibleMovs(casilla.Pieza, coordenada);
    }
}