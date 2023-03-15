using Ajedrez.Controller;
using System.Drawing;
using static Ajedrez.Controller.Tablero;

namespace Ajedrez.PosiblesMovimientosDeLaPieza
{
    public class Peon : Piezas
    {
        public Peon(Image image, MovimientoEspecial movimientoEspecial) : base(image, movimientoEspecial)
        {
        }


        #region ObtenerMovimientosPosibles
        public override void ObtenerMovimientosPosibles(Casilla[,] casillas, Point coordenada)
        {
            if (MovimientoEspecial)
            {
                ObtenerMovimientoEspecial(casillas, coordenada);
            }
            else
            {
                if (coordenada.Y == 0 || coordenada.Y == 7) return;
                ObtenerMovimientoRecto(casillas, coordenada);
            }
            ObtenerMovimientoComer(casillas, coordenada);
        }
        #endregion


        #region ObtenerMovimientoRecto
        private void ObtenerMovimientoRecto(Casilla[,] casillas, Point coordenada)
        {
            if (casillas[coordenada.Y + Color, coordenada.X].Pieza != null) return;
            casillas[coordenada.Y + Color, coordenada.X].CambiarPuedeMover(true);
            listaPuedeMover.Add(new Point(coordenada.X, coordenada.Y + Color));
        }
        #endregion


        #region ObtenerMovimientoComer
        private void ObtenerMovimientoComer(Casilla[,] casillas, Point coordenada)
        {
            int fila = coordenada.Y;
            int columna = coordenada.X;
            
            if (columna > 0)
            {
                MovimientoComer(casillas, fila, columna, -1);
            }
            if (columna < 7)
            {
                MovimientoComer(casillas, fila, columna, 1);
            }
        }

        private void MovimientoComer(Casilla[,] casillas, int fila, int columna, int i)
        {
            if (casillas[fila + Color, columna + i].Pieza == null) return;
            if (casillas[fila + Color, columna + i].Pieza.Color == Color) return;
            casillas[fila + Color, columna + i].CambiarPuedeMover(true);
            listaPuedeMover.Add(new Point(columna + i, fila + Color));
        }
        #endregion


        #region ObtenerMovimientoEspecial
        private void ObtenerMovimientoEspecial(Casilla[,] casillas, Point coordenada)
        {
            int fila = coordenada.Y;

            for (int i = 0; i < 2; i++)
            {
                fila += Color;
                
                if (casillas[fila, coordenada.X].Pieza != null) return;
                
                casillas[fila, coordenada.X].CambiarPuedeMover(true);
                listaPuedeMover.Add(new Point(coordenada.X, fila));
            }
        }
        #endregion

    }
}