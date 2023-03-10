using Ajedrez.Domain;
using System.Collections.Generic;
using System.Drawing;

namespace Ajedrez
{
    public class Piezas
    {
        private TipoDePieza _tipoPieza;
        private int _color;
        private bool _movimientoEspecial;
        public Image _image;
		public List<Coordenada> Movimientos { 
            get
            {
                switch (_tipoPieza)
                {
                    case (TipoDePieza.Peon):
						return new List<Coordenada>(){
							new Coordenada(0, 1),
							new Coordenada(0, 2)
						};
                    default:
                        throw new System.Exception();
                }
            }
        }

        public Piezas(Image pictureBox, TipoDePieza tipoDePieza)
        {
            _image = pictureBox;
            _tipoPieza = tipoDePieza;
            _color = -1;
        }

	}
}