using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Ajedrez.Controller;

namespace Ajedrez.Domain;

public class PiezasFactory
{
    private readonly IEnumerable<Image> _piezasImage;

    public PiezasFactory(string folder)
    {
        _piezasImage = new LoaderImage(folder).GetPiezas();
    }


    public Piezas Caballo => new Piezas(_piezasImage.ElementAt(1), TipoDePieza.Caballo);
    public Piezas Peones => new Piezas(_piezasImage.ElementAt(0), TipoDePieza.Peon);
    public Piezas Torres => new Piezas(_piezasImage.ElementAt(3),TipoDePieza.Torre);
    public Piezas Alfil => new Piezas(_piezasImage.ElementAt(2), TipoDePieza.Alfil);
    public Piezas Reina => new Piezas(_piezasImage.ElementAt(4), TipoDePieza.Reina);
    public Piezas Rey => new Piezas(_piezasImage.ElementAt(5), TipoDePieza.Rey);


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