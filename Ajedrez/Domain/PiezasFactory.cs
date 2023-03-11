using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Ajedrez.Controller;

namespace Ajedrez.Domain;

public class PiezasFactory
{
    private readonly IEnumerable<Image> _piezasImage;
    private Equipo _equipo;

    public PiezasFactory(string folder, Equipo equipo)
    {
        _piezasImage = new LoaderImage(folder).GetPiezas();
        _equipo = equipo;
    }


    public Piezas Caballo => new Piezas(_piezasImage.ElementAt(1), TipoDePieza.Caballo, _equipo);
    public Piezas Peones => new Piezas(_piezasImage.ElementAt(0), TipoDePieza.Peon, _equipo);
    public Piezas Torres => new Piezas(_piezasImage.ElementAt(3),TipoDePieza.Torre, _equipo);
    public Piezas Alfil => new Piezas(_piezasImage.ElementAt(2), TipoDePieza.Alfil, _equipo);
    public Piezas Reina => new Piezas(_piezasImage.ElementAt(4), TipoDePieza.Reina, _equipo);
    public Piezas Rey => new Piezas(_piezasImage.ElementAt(5), TipoDePieza.Rey, _equipo);


    public class Builder
    {
        private string _pathPiezas;
        private Equipo _equipo;

        public static Builder Create()
        {
            return new Builder();
        }

        public Builder PiezaBlanca()
        {
            _pathPiezas = LoaderImage.CarpetaB;
            _equipo = Equipo.Blanco;
            return this;
        }

        public Builder PiezaNegra()
        {
            _pathPiezas = LoaderImage.CarpetaN;
            _equipo = Equipo.Negro;
            return this;
        }

        public PiezasFactory Build()
        {
            return new PiezasFactory(_pathPiezas, _equipo);
        }
    }
}