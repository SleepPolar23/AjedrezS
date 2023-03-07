using System;
using System.Drawing;
using System.Windows.Forms;

namespace Ajedrez
{
    public class Piezas
    {
        private int _tipoPieza;
        private int _color;
        private bool _movimientoEspecial;
        public Image _image;

        public Piezas(Image pictureBox)
        {
            _image = pictureBox;
            _tipoPieza = -1;
            _color = -1;
        }

        public int Color
        {
            get => _color;
            set => _color = value;
        }

        public int TipoPieza
        {
            get => _tipoPieza;
            set => _tipoPieza = value;
        }

        public bool MovimientoEspecial
        {
            get => _movimientoEspecial;
            set => _movimientoEspecial = value;
        }
    }
}