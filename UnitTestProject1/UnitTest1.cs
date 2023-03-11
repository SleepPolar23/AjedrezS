using Ajedrez;
using Ajedrez.Domain;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Ajedrez.Controller;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void VerDondePuedoMoverLaPieza()
        {
            // necesto un game para poder mover la pieza
            var game = new Game();
            game.Tablero.SetPiezasToCasillasDefault();

            // en la vida real antes de que una persona mueva una pieza primero selecciona 
            // la pieza que quiero mover.

            var coordenada = new Coordenada(1, 1); // coordenada de peon

            // en game le digo que me diga los posibles movimientos para una pieza en la coordenada
            var posiblesMovimientos = game.GetPosiblesMov(coordenada);

            // como es un peón en la coordenada 1, 1, entonces debería poder moverse
            // un espacio a la izquierda y arriba, y un espacio a la derecha arriba

            posiblesMovimientos[0].X.Should().Be(1);
            posiblesMovimientos[0].Y.Should().Be(2);

            posiblesMovimientos[1].X.Should().Be(1);
            posiblesMovimientos[1].Y.Should().Be(3);

            posiblesMovimientos.Count().Should().Be(2);
        }

        [TestMethod]
        public void MoverPeon11()
        {
            var logicGame = new LogicGame();
            var peon = new Piezas(null, TipoDePieza.Peon);
            var tablero = new Tablero(8, 8);
            var coordenada = new Coordenada(1, 1);

            // al tablero se le inserta una pieza a la coordenada de la casilla
            tablero.SetPieza(peon, coordenada);

            // entonces muevo la pieza a la coordenada 1, 2
            var posiblesMovs = logicGame.GetPosibleMovs(peon, coordenada);

            var movSelected = posiblesMovs[0];

            // entonces la pieza se mueve a la coordenada 1, 2
            var action = new Action(() => tablero.MovPieza(coordenada, movSelected));

            // cuando se ejecute la funcion mover pieza, no debe dar error
            action.Should().NotThrow();

            // compruebo que la pieza se haiga movido
            var casilla =
                tablero.Casillas.First(j => j.Coordenada.X == movSelected.X && j.Coordenada.Y == movSelected.Y);
            casilla.Pieza.Should().Be(peon);

            // compruebo que de donde se saco la pieza ya no este
            var casilla2 =
                tablero.Casillas.First(j => j.Coordenada.X == coordenada.X && j.Coordenada.Y == coordenada.Y);
            casilla2.Pieza.Should().BeNull();
        }

        [TestMethod]
        public void PeonSeMueveCorrectamente()
        {
            var peon = new Piezas(null, TipoDePieza.Peon);
            peon.Movimientos.Should().ContainEquivalentOf(new Coordenada(0, 1));
            peon.Movimientos.Should().ContainEquivalentOf(new Coordenada(0, 2));
        }

        [TestMethod]
        public void PeonNoSePuedeMoverSiExisteUnPeonDelante()
        {
            var logicGame = new LogicGame();
            var tablero = new Tablero(8, 8);
            var peonAMover = new Piezas(null, TipoDePieza.Peon);
            var peonEnemigo = new Piezas(null, TipoDePieza.Peon);
            var coordenadaPeonAMover = new Coordenada(1, 1);
            var coordenadaPeonEnemigo = new Coordenada(1, 2);

            tablero.SetPieza(peonAMover, coordenadaPeonAMover);
            tablero.SetPieza(peonEnemigo, coordenadaPeonEnemigo);

            var posiblesMovs = logicGame.GetPosibleMovs(peonAMover, coordenadaPeonAMover);
            // le consulto al logic game que me diga si puedo moverme a la coordenada 1, 2
            var movSelected = posiblesMovs[0];

            var puedo = logicGame.PuedoMoverme(movSelected, tablero);
            puedo.Should().BeFalse();
        }

        [TestMethod]
        public void PeonGetMovsDondeCome()
        {
            var peon = new Piezas(null, TipoDePieza.Peon);
            
            peon.MovimientosDondeCome.Should().ContainEquivalentOf(new Coordenada(1, 1));
            peon.MovimientosDondeCome.Should().ContainEquivalentOf(new Coordenada(-1, 1));
        }

        [TestMethod]
        public void PeonDebeComerSiPuede()
        {
            var logicGame = new LogicGame();
            var tablero = new Tablero(8, 8);
            var peonAMover = new Piezas(null, TipoDePieza.Peon);
            var peonEnemigo = new Piezas(null, TipoDePieza.Peon);
            var coordenadaPeonAMover = new Coordenada(1, 1);
            var coordenadaPeonEnemigo = new Coordenada(2, 2);

            tablero.SetPieza(peonAMover, coordenadaPeonAMover);
            tablero.SetPieza(peonEnemigo, coordenadaPeonEnemigo);

            Coordenada[] coordenadasDondecomer = logicGame.GetPosiblesMovimientosParaComer(tablero, coordenadaPeonAMover);

            coordenadasDondecomer.Count().Should().Be(1);
            coordenadasDondecomer.Should().ContainEquivalentOf(new Coordenada(2, 2));
        }

        [TestMethod]
        public void PeonNoDebeComerSiPuede2Posibilidades()
        {
            var logicGame = new LogicGame();
            var tablero = new Tablero(8, 8);

            var peonAMover = new Piezas(null, TipoDePieza.Peon);
            var peonEnemigo = new Piezas(null, TipoDePieza.Peon);
            var peonEnemigo2 = new Piezas(null, TipoDePieza.Peon);
            var coordenadaPeonAMover = new Coordenada(1, 1);
            var coordenadaPeonEnemigo = new Coordenada(2, 2);
            var coordenadaPeonEnemigo2 = new Coordenada(0, 2);

            tablero.SetPieza(peonAMover, coordenadaPeonAMover);
            tablero.SetPieza(peonEnemigo, coordenadaPeonEnemigo);
            tablero.SetPieza(peonEnemigo2, coordenadaPeonEnemigo2);

            var posiblesMovs = logicGame.GetPosibleMovs(peonAMover, coordenadaPeonAMover);
            // le consulto al logic game que me diga si puedo moverme a la coordenada 1, 2
            var movSelected = posiblesMovs[0];

            Coordenada[] coordenadasDondecomer =
                logicGame.GetPosiblesMovimientosParaComer(tablero, coordenadaPeonAMover);

            coordenadasDondecomer.Should().ContainEquivalentOf(new Coordenada(2, 2));
            coordenadasDondecomer.Should().ContainEquivalentOf(new Coordenada(0, 2));
        }
    }
}