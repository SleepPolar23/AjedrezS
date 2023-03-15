using Ajedrez.Controller;
using System.Drawing;

namespace Ajedrez.PosiblesMovimientosDeLaPieza
{
    public class Reina : Piezas
    {
        Rey movRey = new Rey();
        Torre movTorre = new Torre();
        Alfil movAlfil = new Alfil();
        public Reina(Image image, MovimientoEspecial movimientoEspecial) : base(image, movimientoEspecial)
        {
        }

        public override void ObtenerMovimientosPosibles(Casilla[,] casillas, Point coordenada)
        {
            movRey.Color = Color;
            movTorre.Color = Color;
            movAlfil.Color = Color;
            movRey.ObtenerMovimientosPosibles(casillas, coordenada);
            movTorre.ObtenerMovimientosPosibles(casillas, coordenada);
            movAlfil.ObtenerMovimientosPosibles(casillas, coordenada);
        }
    }
}
