using Ajedrez.Controller;
using System.Drawing;
using System.Windows.Forms;
using static Ajedrez.Controller.Tablero;

namespace Ajedrez.PosiblesMovimientosDeLaPieza
{
    public class Caballo : Piezas
    {
        public Caballo(Image image, MovimientoEspecial movimientoEspecial) : base(image, movimientoEspecial)
        {
        }

        public override void ObtenerMovimientosPosibles(Casilla[,] casillas, Point coordenada)
        {
            DireccionesDelCaballo(casillas, coordenada.Y, coordenada.X, -2, 1); //2 Arriba
            DireccionesDelCaballo(casillas, coordenada.Y, coordenada.X,  2, 1); //2 Abajo
            DireccionesDelCaballo(casillas, coordenada.Y, coordenada.X, -1, 2); //2 Izquierda
            DireccionesDelCaballo(casillas, coordenada.Y, coordenada.X,  1, 2); //2 Derecha
        }


        #region DireccionesDelCaballo
        private void DireccionesDelCaballo(Casilla[,] casillas, int fila, int columna, int sumaFila, int sumaColumna)
        {
            if(fila + sumaFila > -1 && fila + sumaFila < 8)
            {
                if (columna - sumaColumna > -1)
                {
                    AgregarPosibleMovimiento(casillas, fila + sumaFila, columna - sumaColumna);
                }
                if (columna + sumaColumna < 8)
                {
                    AgregarPosibleMovimiento(casillas, fila + sumaFila, columna + sumaColumna);
                }
            }
        }
        #endregion


        private void AgregarPosibleMovimiento(Casilla[,] casillas, int fila, int columna)
        {
            if (casillas[fila, columna].Pieza != null)
                if (casillas[fila, columna].Pieza.Color == Color) return;

            casillas[fila, columna].CambiarPuedeMover(true);
            listaPuedeMover.Add(new Point(columna, fila));
        }

    }
}
