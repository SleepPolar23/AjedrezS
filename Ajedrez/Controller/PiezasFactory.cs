using Ajedrez.PosiblesMovimientosDeLaPieza;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Ajedrez.Controller;

public class PiezasFactory
{
    private readonly IEnumerable<Image> _piezasImage;

    public PiezasFactory(string folder)
    {
        _piezasImage = new LoaderImage(folder).GetPiezas();
    }


    public Piezas Peones => new Peon(_piezasImage.ElementAt(0), MovimientoEspecial.True);
    public Piezas Caballo => new Caballo(_piezasImage.ElementAt(1), MovimientoEspecial.False);
    public Piezas Alfil => new Alfil(_piezasImage.ElementAt(2), MovimientoEspecial.False);
    public Piezas Torres => new Torre(_piezasImage.ElementAt(3), MovimientoEspecial.True);
    public Piezas Reina => new Reina(_piezasImage.ElementAt(4), MovimientoEspecial.False);
    public Piezas Rey => new Rey(_piezasImage.ElementAt(5), MovimientoEspecial.True);


    public class Builder
    {
        private string _pathPiezas;

        public static Builder Create()
        {
            return new Builder();
        }

        public Builder PiezaBlanca()
        {
            _pathPiezas = LoaderImage.CarpetaB;
            return this;
        }

        public Builder PiezaNegra()
        {
            _pathPiezas = LoaderImage.CarpetaN;
            return this;
        }

        public PiezasFactory Build()
        {
            return new PiezasFactory(_pathPiezas);
        }
    }
}