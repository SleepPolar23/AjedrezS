using System;
using System.Drawing;
using System.Windows.Forms;
using Ajedrez.Controller;
using Ajedrez.Domain;


namespace Ajedrez;

public partial class Ajedrez : Form, ITableroObserver
{
    private bool PiezaClickeada;
    Point tempPiezaClick;
    int jugadorActual = 0;
    private Tablero _tablero;
    private Game _game;


    public Ajedrez()
    {
        InitializeComponent();
    }

    private void Form1_Load(object sender, EventArgs e) => CrearTablero(100, 8, 8);


    private void CrearTablero(int size, int rows, int cols)
    {
        _tablero = new Tablero(rows, rows);
        _tablero.SetPiezasToCasillasDefault();
        _game = new Game(_tablero);

        foreach (var tableroCasilla in _tablero.Casillas)
        {
            var pb = BuilderPictureBox.Create()
                .WithSize(size)
                .WithAction((sender, args) => { _game.SetCasillaSeleccionada(tableroCasilla.Coordenada); })
                .ToCasilla(tableroCasilla)
                .Build(label1);

            Tablero.Controls.Add(pb); // Añadir el PictureBox al panel
        }

        _game.AddObserverTablero(this);

        Tablero.Location = new Point((Width - Tablero.Width) / 2, (Height - Tablero.Height) / 2);
        Tablero.Visible = true;
    }

    public void TableroCambio(Tablero tablero)
    {
        Tablero.Controls.Clear();
        foreach (var casilla in _game.Tablero.Casillas)
        {
            var pb = BuilderPictureBox.Create()
                .WithSize(100)
                //.WithAction(PictureBox_Click)
                .WithAction((sender, args) => { _game.SetCasillaSeleccionada(casilla.Coordenada); })
                .ToCasilla(casilla)
                .Build(label1);

            Tablero.Controls.Add(pb);
        }
    }
}