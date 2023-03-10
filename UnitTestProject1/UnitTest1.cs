using Ajedrez;
using Ajedrez.Domain;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using Ajedrez.Controller;
using Moq;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void VerDondePuedoMoverLaPieza()
        {
            // necesto un game para poder mover la pieza
            var game = new Game(new Tablero(8, 8));
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
            var peon = new Piezas(null, TipoDePieza.Peon, default);
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
            var peon = new Piezas(null, TipoDePieza.Peon, default);
            peon.Movimientos.Should().ContainEquivalentOf(new Coordenada(0, 1));
            peon.Movimientos.Should().ContainEquivalentOf(new Coordenada(0, 2));
        }

        [TestMethod]
        public void PeonNoSePuedeMoverSiExisteUnPeonDelante()
        {
            var logicGame = new LogicGame();
            var tablero = new Tablero(8, 8);
            var peonAMover = new Piezas(null, TipoDePieza.Peon, default);
            var peonEnemigo = new Piezas(null, TipoDePieza.Peon, default);
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
            var peon = new Piezas(null, TipoDePieza.Peon, default);

            peon.MovimientosDondeCome.Should().ContainEquivalentOf(new Coordenada(1, 1));
            peon.MovimientosDondeCome.Should().ContainEquivalentOf(new Coordenada(-1, 1));
        }

        [TestMethod]
        public void PeonMovsDondeCome()
        {
            var logicGame = new LogicGame();
            var tablero = new Tablero(8, 8);
            var peonAMover = new Piezas(null, TipoDePieza.Peon, Equipo.Blanco);
            var peonEnemigo = new Piezas(null, TipoDePieza.Peon, Equipo.Negro);
            var coordenadaPeonAMover = new Coordenada(1, 1);
            var coordenadaPeonEnemigo = new Coordenada(2, 2);

            tablero.SetPieza(peonAMover, coordenadaPeonAMover);
            tablero.SetPieza(peonEnemigo, coordenadaPeonEnemigo);

            Coordenada[] coordenadasDondecomer =
                logicGame.GetPosiblesMovimientosParaComer(tablero, coordenadaPeonAMover);

            coordenadasDondecomer.Count().Should().Be(1);
            coordenadasDondecomer.Should().ContainEquivalentOf(new Coordenada(2, 2));
        }

        [TestMethod]
        public void PeonMovsDondeCome2Posibilidades()
        {
            var logicGame = new LogicGame();
            var tablero = new Tablero(8, 8);

            var peonAMover = new Piezas(null, TipoDePieza.Peon, Equipo.Blanco);
            var peonEnemigo = new Piezas(null, TipoDePieza.Peon, Equipo.Negro);
            var peonEnemigo2 = new Piezas(null, TipoDePieza.Peon, Equipo.Negro);
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

        [TestMethod]
        public void PeonNoPuedeComerUnaPiezaSiEsAliada()
        {
            var logicGame = new LogicGame();
            var tablero = new Tablero(8, 8);

            var peonAMover = new Piezas(null, TipoDePieza.Peon, Equipo.Blanco);
            var peonAliado = new Piezas(null, TipoDePieza.Peon, Equipo.Blanco);
            var coordenadaPeonAMover = new Coordenada(1, 1);
            var coordenadaPeonAliado = new Coordenada(2, 2);

            tablero.SetPieza(peonAMover, coordenadaPeonAMover);
            tablero.SetPieza(peonAliado, coordenadaPeonAliado);

            Coordenada[] coordenadasDondecomer =
                logicGame.GetPosiblesMovimientosParaComer(tablero, coordenadaPeonAMover);

            coordenadasDondecomer.Length.Should().Be(0);
            coordenadasDondecomer.Should().NotContainEquivalentOf(new Coordenada(2, 2));
        }

        [TestMethod]
        public void SiSeSeleccionaUnaPieza_CambiarElEstadoDeLasCasillasIndicandoLasPosibilidadesDeMovimiento()
        {
            var tablero = new Tablero(8, 8);

            var peonAMover = new Piezas(null, TipoDePieza.Peon, Equipo.Blanco);
            var coordenadaPeonSeleccionado = new Coordenada(1, 1);
            tablero.SetPieza(peonAMover, coordenadaPeonSeleccionado);

            var game = new Game(tablero);
            game.SetCasillaSeleccionada(coordenadaPeonSeleccionado);

            tablero.Casillas.Where(j => j.Coordenada.X == 1 && j.Coordenada.Y == 1).First()
                .Estado.Should().Be(EstadoCasilla.Seleccionada);

            tablero.Casillas.Where(j => j.Coordenada.X == 1 && j.Coordenada.Y == 2).First()
                .Estado.Should().Be(EstadoCasilla.PosibleMovimiento);

            tablero.Casillas.Where(j => j.Coordenada.X == 1 && j.Coordenada.Y == 3).First()
                .Estado.Should().Be(EstadoCasilla.PosibleMovimiento);
        }

        [TestMethod]
        public void NotificarAInterfazQueElEstadoDelTableroHaCambiadoParaPintar()
        {
            var tablero = new Tablero(8, 8);

            var peonAMover = new Piezas(null, TipoDePieza.Peon, Equipo.Blanco);
            var coordenadaPeonSeleccionado = new Coordenada(1, 1);
            tablero.SetPieza(peonAMover, coordenadaPeonSeleccionado);

            var game = new Game(tablero);
            game.SetCasillaSeleccionada(coordenadaPeonSeleccionado);

            var mock = new Mock<ITableroObserver>();
            game.AddObserverTablero(mock.Object);

            game.SetCasillaSeleccionada(coordenadaPeonSeleccionado);

            mock.Verify(j => j.TableroCambio(It.IsAny<Tablero>()), Times.Once);
        }

        [TestMethod]
        public void LosMovimientosDebenEstarLimitadosPorElRangoDeMovimiento()
        {
            var logicGame = new LogicGame();
            var tablero = new Tablero(8, 8);

            var peon = new Piezas(null, TipoDePieza.Peon, Equipo.Blanco);
            var coordenadaPeonSeleccionado = new Coordenada(7, 7);
            tablero.SetPieza(peon, coordenadaPeonSeleccionado);

            var posiblesMovs = logicGame.GetPosibleMovs(peon, coordenadaPeonSeleccionado);

            posiblesMovs.Should().NotContainEquivalentOf(new Coordenada(7, 8));
            posiblesMovs.Should().NotContainEquivalentOf(new Coordenada(7, 9));
            posiblesMovs.Length.Should().Be(0);
        }

        [TestMethod]
        public void LosBlancosSeMuevenEnPositivo()
        {
            var logicGame = new LogicGame();
            var tablero = new Tablero(8, 8);

            var peon = new Piezas(null, TipoDePieza.Peon, Equipo.Blanco);
            var coordenadaPeonSeleccionado = new Coordenada(1, 1);
            tablero.SetPieza(peon, coordenadaPeonSeleccionado);

            var posiblesMovs = logicGame.GetPosibleMovs(peon, coordenadaPeonSeleccionado);

            posiblesMovs.Should().ContainEquivalentOf(new Coordenada(1, 2));
            posiblesMovs.Should().ContainEquivalentOf(new Coordenada(1, 3));
            posiblesMovs.Length.Should().Be(2);
        }

        [TestMethod]
        public void LosNegrosSeMuevenEnNegativo()
        {
            var logicGame = new LogicGame();
            var tablero = new Tablero(8, 8);

            var peon = new Piezas(null, TipoDePieza.Peon, Equipo.Negro);
            var coordenadaPeonSeleccionado = new Coordenada(3, 3);
            tablero.SetPieza(peon, coordenadaPeonSeleccionado);

            var posiblesMovs = logicGame.GetPosibleMovs(peon, coordenadaPeonSeleccionado);

            posiblesMovs.Should().ContainEquivalentOf(new Coordenada(3, 2));
            posiblesMovs.Should().ContainEquivalentOf(new Coordenada(3, 1));
            posiblesMovs.Length.Should().Be(2);
        }

        [TestMethod]
        public void PeonSeMueveParaComer()
        {
            var logicGame = new LogicGame();
            var peon = new Piezas(null, TipoDePieza.Peon, Equipo.Blanco);
            var peonEnemigo = new Piezas(null, TipoDePieza.Peon, Equipo.Negro);
            var tablero = new Tablero(8, 8);
            var coordenadaSeleccionado = new Coordenada(1, 1);
            var coordenadaEnemigo = new Coordenada(2, 2);

            // al tablero se le inserta una pieza a la coordenada de la casilla
            tablero.SetPieza(peon, coordenadaSeleccionado);
            tablero.SetPieza(peonEnemigo, coordenadaEnemigo);

            // entonces muevo la pieza a la coordenada 1, 2
            var posiblesMovs = logicGame.GetPosiblesMovsDondeCome(peon, coordenadaSeleccionado);

            var movSelected = posiblesMovs[0];

            // entonces la pieza se mueve a la coordenada 1, 2
            var action = new Action(() => tablero.ComerPieza(coordenadaSeleccionado, movSelected));

            // cuando se ejecute la funcion mover pieza, no debe dar error
            action.Should().NotThrow();

            // compruebo que la pieza se haiga comido
            var casilla =
                tablero.Casillas.Where(j => j.Coordenada.X == movSelected.X && j.Coordenada.Y == movSelected.Y).First();

            casilla.Pieza.Should().Be(peon);

            // comprueba que la coordenada origen no tiene pieza
            var casillaOrigen =
                tablero.Casillas.Where(j =>
                    j.Coordenada.X == coordenadaSeleccionado.X && j.Coordenada.Y == coordenadaSeleccionado.Y).First();

            casillaOrigen.Pieza.Should().BeNull();
        }

        [TestMethod]
        public void LosPosiblesMovimientosDondeCome_ParaELBlancoSonPositivos()
        {
            var logicGame = new LogicGame();
            var peon = new Piezas(null, TipoDePieza.Peon, Equipo.Blanco);

            var tablero = new Tablero(8, 8);
            var coordenadaSeleccionado = new Coordenada(1, 1);

            // al tablero se le inserta una pieza a la coordenada de la casilla
            tablero.SetPieza(peon, coordenadaSeleccionado);

            // entonces muevo la pieza a la coordenada 1, 2
            var posiblesMovs = logicGame.GetPosiblesMovsDondeCome(peon, coordenadaSeleccionado);

            posiblesMovs.Should().ContainEquivalentOf(new Coordenada(2, 2));
            posiblesMovs.Should().ContainEquivalentOf(new Coordenada(0, 2));
        }

        [TestMethod]
        public void LosPosiblesMovimientosDondeCome_ParaELNegroSonPositivos()
        {
            var logicGame = new LogicGame();
            var peon = new Piezas(null, TipoDePieza.Peon, Equipo.Negro);

            var tablero = new Tablero(8, 8);
            var coordenadaSeleccionado = new Coordenada(3, 3);

            // al tablero se le inserta una pieza a la coordenada de la casilla
            tablero.SetPieza(peon, coordenadaSeleccionado);

            // entonces muevo la pieza a la coordenada 1, 2
            var posiblesMovs = logicGame.GetPosiblesMovsDondeCome(peon, coordenadaSeleccionado);

            posiblesMovs.Should().ContainEquivalentOf(new Coordenada(4, 2));
            posiblesMovs.Should().ContainEquivalentOf(new Coordenada(2, 2));
        }

        [TestMethod]
        public void LosPosiblesMovimientosCuandoCome_NoPuedenEstarFueraDeLosLimites()
        {
            var logicGame = new LogicGame();
            var peon = new Piezas(null, TipoDePieza.Peon, Equipo.Negro);

            var tablero = new Tablero(8, 8);
            var coordenadaSeleccionado = new Coordenada(0, 0);

            // al tablero se le inserta una pieza a la coordenada de la casilla
            tablero.SetPieza(peon, coordenadaSeleccionado);

            // entonces muevo la pieza a la coordenada 1, 2
            var posiblesMovs = logicGame.GetPosiblesMovsDondeCome(peon, coordenadaSeleccionado);

            posiblesMovs.Length.Should().Be(0);
        }
    }
}