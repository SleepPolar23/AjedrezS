using Ajedrez.Controller;
using System.Drawing;
using static Ajedrez.Controller.Tablero;

namespace Ajedrez.PosiblesMovimientosDeLaPieza
{
    public class Rey : Piezas
    {
        public Rey() { }
        public Rey(Image image, MovimientoEspecial movimientoEspecial) : base(image, movimientoEspecial)
        {
        }

        public override void ObtenerMovimientosPosibles(Casilla[,] casillas, Point coordenada)
        {
            DireccionesDelRey(casillas, coordenada.Y, coordenada.X, 0,  0, -1, -1); //Ariba Izquierda
            DireccionesDelRey(casillas, coordenada.Y, coordenada.X, 0, -1, -1,  0); //Arriba
            DireccionesDelRey(casillas, coordenada.Y, coordenada.X, 0,  7, -1,  1); //Arriba Derecha
            DireccionesDelRey(casillas, coordenada.Y, coordenada.X,-1,  0,  0, -1); //Izquierda
            DireccionesDelRey(casillas, coordenada.Y, coordenada.X,-1,  7,  0,  1); //Derecha
            DireccionesDelRey(casillas, coordenada.Y, coordenada.X, 7,  0,  1, -1); //Abajo Izquierda
            DireccionesDelRey(casillas, coordenada.Y, coordenada.X, 7, -1,  1,  0); //Abajo
            DireccionesDelRey(casillas, coordenada.Y, coordenada.X, 7,  7,  1,  1); //Abajo Derecha
        }

        #region DireccionesDelRey
        private void DireccionesDelRey(Casilla[,] casillas, int fila, int columna, int limitFila, int limitColumna, int sumaFila, int sumaColumna)
        {
            if(fila != limitFila && columna != limitColumna)
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
