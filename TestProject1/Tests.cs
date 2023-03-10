using Ajedrez.Domain;
using NUnit.Framework;

namespace TestProject1;

[TestFixture]
public class Tests
{
    [Test]
    public void Tablero_VerPosiblesMovimientos()
    {
        var game = new Game();
        var mov = new Mov().PosiblesMovimientos(1, 1);
        var res = game.MakeMov(mov);

        res.PositionMov[0].X.Should().Be(0);
        res.PositionMov[0].Y.Should().Be(2);
        
        res.PositionMov[1].X.Should().Be(2);
        res.PositionMov[1].Y.Should().Be(2);
    }
}