using Ajedrez.Controller;
using Ajedrez.Controller.Enums.ParaPieza;
using System;
using System.Drawing;

namespace Ajedrez
{
    public abstract class Piezas
    {
        private int _color;
        private bool _movimientoEspecial;
        public Image _image;

        public Piezas()
        {
            _color = 0;
            _movimientoEspecial = false;
            _image = null;
        }

        public Piezas(Image image, MovimientoEspecial movimientoEspecial)
        {
            _image = image;
            _movimientoEspecial = (movimientoEspecial == Controller.MovimientoEspecial.True);
        }

        public abstract void ObtenerMovimientosPosibles(Casilla[,] casillas, Point coordenada);

        public int Color
        {
            get => _color;
            set => _color = value;
        }

        public bool MovimientoEspecial
        {
            get => _movimientoEspecial;
            set => _movimientoEspecial = value;
        }
    }
}