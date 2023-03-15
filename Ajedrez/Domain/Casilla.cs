using Ajedrez.Controller.Enums;
using System;
<<<<<<< HEAD:Ajedrez/Controller/Casilla.cs
using System.Drawing;
using System.Windows.Forms;
=======
using Ajedrez.Controller;
>>>>>>> 0d0ef351401b3d530e17fbe7829bb990070fc233:Ajedrez/Domain/Casilla.cs

namespace Ajedrez.Domain;

public class Casilla
{
<<<<<<< HEAD:Ajedrez/Controller/Casilla.cs
    private bool _puedeMover;
    public TipoCasilla ColorCasilla;
    private Piezas? pieza;
    public PictureBox pb;

    

    public Casilla(int color, int size, int i, int j, Casilla[,] casillas)
=======
    public ColorCasilla ColorCasilla { get; set; }
    public Coordenada Coordenada { get; set; }
    public Piezas? Pieza { get; set; }
    public EstadoCasilla Estado { get; set; }

	public Casilla(int color, int fila, int columna)
>>>>>>> 0d0ef351401b3d530e17fbe7829bb990070fc233:Ajedrez/Domain/Casilla.cs
    {
        if (Enum.TryParse<TipoCasilla>(color.ToString(), out var colorEnum))
        {
            ColorCasilla = colorEnum;
            pb = BuilderPictureBox.Create().WithSize(size).WithLocation(i, j).WithColorCasilla(colorEnum).Build();
            pb.Click += new EventHandler((sender, e) => Ajedrez.PictureBox_Click(sender, e, casillas));
        }
<<<<<<< HEAD:Ajedrez/Controller/Casilla.cs
    }

    #region CambiarPuedeMover
    public void CambiarPuedeMover(bool puedeMover)
    {
        _puedeMover = puedeMover;

        switch (ColorCasilla)
        {
            case TipoCasilla.Blanco:
                pb.BackColor = puedeMover ? Colores.BlancoMover : Colores.BlancoNormal;
                break;
            case TipoCasilla.Negro:
                pb.BackColor = puedeMover ? Colores.NegroMover : Colores.NegroNormal;
                break;
        }
    }
    #endregion


    #region Getter y Setter
    // Este codigo sirve para que al cambiar el Image de la Pieza tambien se cambie el Image del PictureBox
    public Piezas? Pieza
    {
        get { return pieza; }
        set
        {
            pieza = value;
            if (pb != null)
            {
                if (pieza != null)
                {
                    pb.Image = pieza._image;
                }
                else
                {
                    pb.Image = null;
                }
            }
        }
    }

    public bool PuedeMover
    {
        get => _puedeMover;
        set => _puedeMover = value;
    }
    #endregion
=======
        else
        {
            throw new Exception("No se pudo convertir el color");
        }

        Coordenada = new Coordenada(fila, columna);
    }
}

public enum EstadoCasilla
{
    Normal,
    Seleccionada,
    PosibleMovimiento,
    PosbileComer,
>>>>>>> 0d0ef351401b3d530e17fbe7829bb990070fc233:Ajedrez/Domain/Casilla.cs
}