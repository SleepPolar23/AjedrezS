using System;
using System.Windows.Forms;

namespace Ajedrez
{
    public class Piezas
    {
        private int _tipoPieza;
        private int _color;
        private bool _movimientoEspecial;
        private PictureBox _pictureBox;

        public Piezas(PictureBox pictureBox)
        {
            _pictureBox = pictureBox;
            _tipoPieza = -1;
            _color = -1;
        }

        public int Color { get => _color; set => _color = value; }
        public PictureBox PictureBox { get => _pictureBox; set => _pictureBox = value; }
        public int TipoPieza { get => _tipoPieza; set => _tipoPieza = value; }
        public bool MovimientoEspecial { get => _movimientoEspecial; set => _movimientoEspecial = value; }
    }
}