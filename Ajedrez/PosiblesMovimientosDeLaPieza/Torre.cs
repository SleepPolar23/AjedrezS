using Ajedrez.Controller;
using System.Drawing;
using static Ajedrez.Controller.Tablero;

namespace Ajedrez.PosiblesMovimientosDeLaPieza
{
    public class Torre : Piezas
    {
        public Torre() { }
        public Torre(Image image, MovimientoEspecial movimientoEspecial) : base(image, movimientoEspecial)
        {
        }

        public override void ObtenerMovimientosPosibles(Casilla[,] casillas, Point coordenada)
        {
            DireccionesDeLaTorre(casillas, coordenada.Y, coordenada.X,  0, -1, -1,  0); //Arriba
            DireccionesDeLaTorre(casillas, coordenada.Y, coordenada.X,  7, -1,  1,  0); //Abajo
            DireccionesDeLaTorre(casillas, coordenada.Y, coordenada.X, -1,  0,  0, -1); //Izquierda
            DireccionesDeLaTorre(casillas, coordenada.Y, coordenada.X, -1,  7,  0,  1); //Derecha
        }


        #region DireccionesDeLaTorre
        private void DireccionesDeLaTorre(Casilla[,] casillas, int fila, int columna, int limitFila, int limitColumna, int sumaFila, int sumaColumna)
        {
            while (fila != limitFila && columna != limitColumna)
            {
                fila += sumaFila;
                columna += sumaColumna;
                if (casillas[fila, columna].Pieza != null)
                {
                    if (casillas[fila, columna].Pieza.Color == Color) return;
                    casillas[fila, columna].CambiarPuedeMover(true);
                    listaPuedeMover.Add(new Point(columna, fila));
                    return;
                }
                casillas[fila, columna].CambiarPuedeMover(true);
                listaPuedeMover.Add(new Point(columna, fila));
            }
        }
        #endregion
    }
}
