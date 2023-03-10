using Ajedrez;
using Ajedrez.Domain;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

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

			// en la vida real antes de que una persona mueva una pieza primero selecciona 
			// la pieza que quiero mover.

			var coordenada = new Coordenada(1, 1); // coordenada de peon

			// en game le digo que me diga los posibles movimientos para una pieza en la coordenada
			var posiblesMovimientos = game.GetPosiblesMov(coordenada);

			// como es un peón en la coordenada 1, 1, entonces debería poder moverse
			// un espacio a la izquierda y arriba, y un espacio a la derecha arriba

			posiblesMovimientos[0].X.Should().Be(1);
			posiblesMovimientos[0].Y.Should().Be(2);

			posiblesMovimientos.Count().Should().Be(1);
		}

		[TestMethod]
		public void PeonSeMueveCorrectamente()
		{
			var peon = new Piezas(null, TipoDePieza.Peon);

			peon.Movimientos[0].X.Should().Be(0);
			peon.Movimientos[0].Y.Should().Be(1);

			peon.Movimientos[1].X.Should().Be(0);
			peon.Movimientos[1].Y.Should().Be(2);
		}
	}
}
