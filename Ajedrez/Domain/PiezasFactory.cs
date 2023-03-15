using Ajedrez.PosiblesMovimientosDeLaPieza;
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


<<<<<<< HEAD:Ajedrez/Controller/PiezasFactory.cs
    public Piezas Peones => new Peon(_piezasImage.ElementAt(0), MovimientoEspecial.True);
    public Piezas Caballo => new Caballo(_piezasImage.ElementAt(1), MovimientoEspecial.False);
    public Piezas Alfil => new Alfil(_piezasImage.ElementAt(2), MovimientoEspecial.False);
    public Piezas Torres => new Torre(_piezasImage.ElementAt(3), MovimientoEspecial.True);
    public Piezas Reina => new Reina(_piezasImage.ElementAt(4), MovimientoEspecial.False);
    public Piezas Rey => new Rey(_piezasImage.ElementAt(5), MovimientoEspecial.True);
=======
    public Piezas Caballo => new Piezas(_piezasImage.ElementAt(1), TipoDePieza.Caballo, _equipo);
    public Piezas Peones => new Piezas(_piezasImage.ElementAt(0), TipoDePieza.Peon, _equipo);
    public Piezas Torres => new Piezas(_piezasImage.ElementAt(3),TipoDePieza.Torre, _equipo);
    public Piezas Alfil => new Piezas(_piezasImage.ElementAt(2), TipoDePieza.Alfil, _equipo);
    public Piezas Reina => new Piezas(_piezasImage.ElementAt(4), TipoDePieza.Reina, _equipo);
    public Piezas Rey => new Piezas(_piezasImage.ElementAt(5), TipoDePieza.Rey, _equipo);
>>>>>>> 0d0ef351401b3d530e17fbe7829bb990070fc233:Ajedrez/Domain/PiezasFactory.cs


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