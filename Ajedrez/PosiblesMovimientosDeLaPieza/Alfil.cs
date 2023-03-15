using Ajedrez.Controller;
using System.Drawing;
using static Ajedrez.Controller.Tablero;

namespace Ajedrez.PosiblesMovimientosDeLaPieza
{
    public class Alfil : Piezas
    {
        public Alfil() { }
        public Alfil(Image image, MovimientoEspecial movimientoEspecial) : base(image, movimientoEspecial)
        {
        }

        public override void ObtenerMovimientosPosibles(Casilla[,] casillas, Point coordenada)
        {
            DireccionesDelAlfil(casillas, coordenada.Y, coordenada.X, 0, 0, -1, -1); //Arriba Izquierda
            DireccionesDelAlfil(casillas, coordenada.Y, coordenada.X, 0, 7, -1,  1); //Arriba Derecha
            DireccionesDelAlfil(casillas, coordenada.Y, coordenada.X, 7, 0,  1, -1); //Abajo Izquierda
            DireccionesDelAlfil(casillas, coordenada.Y, coordenada.X, 7, 7,  1,  1); //Abajo Derecha
        }


        #region DireccionesDelAlfil
        private void DireccionesDelAlfil(Casilla[,] casillas, int fila, int columna, int limitFila, int limitColumna, int sumaFila, int sumaColumna)
        {
            while(fila != limitFila && columna != limitColumna)
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

        /*if (casillas[coordenada.Y + Color, coordenada.X].Pieza != null) return;
            casillas[coordenada.Y + Color, coordenada.X].CambiarPuedeMover(true);
            listaPuedeMover.Add(new Point(coordenada.X, coordenada.Y + Color));*/
    }
}