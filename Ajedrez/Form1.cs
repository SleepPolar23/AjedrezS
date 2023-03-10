using System;
using System.Drawing;
using System.Windows.Forms;
using Ajedrez.Controller;
using Ajedrez.Domain;


namespace Ajedrez;

public partial class Ajedrez : Form
{
    private bool PiezaClickeada;
    Point tempPiezaClick;
    int jugadorActual = 0;
    private Tablero _tablero;


    public Ajedrez()
    {
        InitializeComponent();
    }

    private void Form1_Load(object sender, EventArgs e) => CrearTablero(100, 8, 8);


    private void CrearTablero(int size, int rows, int cols)
    {
        _tablero = new Tablero(rows, rows);
        foreach (var tableroCasilla in _tablero.Casillas)
        {
            var pb = BuilderPictureBox.Create()
                .WithSize(size)
                //.WithAction(PictureBox_Click)
                .ToCasilla(tableroCasilla)
                .Build();

            Tablero.Controls.Add(pb); // Añadir el PictureBox al panel
        }

        Tablero.Location = new Point((Width - Tablero.Width) / 2, (Height - Tablero.Height) / 2);
        Tablero.Visible = true;
    }


    /*
    #region PictureBoxClick

    private void PictureBox_Click(object sender, EventArgs e)
    {
        PictureBox pb = (PictureBox)sender;

        int i = pb.Location.Y;
        int j = pb.Location.X;
        i /= 100;
        j /= 100;

        if (mapaDelTablero[i, j].Color != jugadorActual && PiezaClickeada == false) return;

        if (PiezaClickeada)
        {
            DevolverColorALosPictureBox();
            if (MoverPieza(i, j))
            {
                CambiarColor(ultimoMovimiento, ColorNegro,
                    ColorBlanco); //Si se mueve una pieza colorea el movimiento hecho
                return;
            }
        }

        if (mapaDelTablero[i, j].PictureBox.Image == null) return;

        switch (mapaDelTablero[i, j].TipoPieza)
        {
            case 0:
                PosiblesMovimientosDelPeon(i, j);
                break;
        }

        PiezaClickeada = true;
    }

    #endregion


    #region MoverPieza

    private bool MoverPieza(int i, int j)
    {
        foreach (Point temporal in temporalDeClick)
        {
            if (i == temporal.X && j == temporal.Y)
            {
                Piezas tempPieza = mapaDelTablero[i, j];
                PictureBox tempPictureBox = mapaDelTablero[i, j].PictureBox;
                Image tempImage = tempPictureBox.Image;
                if (mapaDelTablero[i, j].TipoPieza != -1)
                {
                    tempImage = null;
                    tempPieza.TipoPieza = -1;
                }

                mapaDelTablero[i, j].PictureBox.Image =
                    mapaDelTablero[tempPiezaClick.X, tempPiezaClick.Y].PictureBox.Image;
                mapaDelTablero[i, j].PictureBox = mapaDelTablero[tempPiezaClick.X, tempPiezaClick.Y].PictureBox;
                mapaDelTablero[i, j] = mapaDelTablero[tempPiezaClick.X, tempPiezaClick.Y];

                mapaDelTablero[tempPiezaClick.X, tempPiezaClick.Y].PictureBox.Image = tempImage;
                mapaDelTablero[tempPiezaClick.X, tempPiezaClick.Y].PictureBox = tempPictureBox;
                mapaDelTablero[tempPiezaClick.X, tempPiezaClick.Y] = tempPieza;

                mapaDelTablero[i, j].MovimientoEspecial = false;
                temporalDeClick.Clear();
                jugadorActual = jugadorActual == 0 ? 1 : 0;

                CambiarMovimientoAColorear(i, j);

                return true;
            }
        }

        temporalDeClick.Clear();
        return false;
    }

    #endregion


    #region CambiarMovimientoAColores

    private void CambiarMovimientoAColorear(int i, int j)
    {
        CambiarColor(ultimoMovimiento, ColorNegroSuave, ColorBlancoSuave);
        ultimoMovimiento.Clear();
        ultimoMovimiento.Add(new Point(i, j));
        ultimoMovimiento.Add(new Point(tempPiezaClick.X, tempPiezaClick.Y));
    }

    #endregion


    #region CambiarColor

    private void CambiarColor(List<Point> lista, Color negro, Color blanco)
    {
        foreach (Point temporal in lista)
        {
            if (mapaDelTablero[temporal.X, temporal.Y].PictureBox.Name.Equals("Negro"))
            {
                mapaDelTablero[temporal.X, temporal.Y].PictureBox.BackColor = negro;
            }
            else
            {
                mapaDelTablero[temporal.X, temporal.Y].PictureBox.BackColor = blanco;
            }
        }
    }

    #endregion


    #region DevolverColorALosPictureBox

    private void DevolverColorALosPictureBox()
    {
        CambiarColor(temporalDeClick, ColorNegroSuave, ColorBlancoSuave);
        PiezaClickeada = false;
    }

    #endregion


    #region PosiblesMovimientosDelPeon

    private void PosiblesMovimientosDelPeon(int i, int j)
    {
        if (i == 0 || i == 7) return;
        int condicion = mapaDelTablero[i, j].Color == 1 ? 1 : -1;
        int k = mapaDelTablero[i, j].MovimientoEspecial == true ? 3 : 2;
        for (int p = 1; p < k; p++)
        {
            if (mapaDelTablero[i + p * condicion, j].TipoPieza != -1) break;

            if (mapaDelTablero[i + p * condicion, j].PictureBox.Name == "Negro")
            {
                mapaDelTablero[i + p * condicion, j].PictureBox.BackColor = ColorNegro;
                temporalDeClick.Add(new Point(i + p * condicion, j));
            }
            else
            {
                mapaDelTablero[i + p * condicion, j].PictureBox.BackColor = ColorBlanco;
                temporalDeClick.Add(new Point(i + p * condicion, j));
            }
        }

        tempPiezaClick = new Point(i, j);
        ComprobarSiPeonPuedeComer(i, j, condicion);
    }

    #endregion


    #region ComprobarSiPeonPuedeComer

    private void ComprobarSiPeonPuedeComer(int i, int j, int condicion) // Este metodo decide que opcion usar.
    {
        if (i == 0 || i == 7) return;
        if (j == 0)
        {
            ComprobarSiHayPiezasComestibles(i, j + 1, j, condicion);
            return;
        }

        if (j == 7)
        {
            ComprobarSiHayPiezasComestibles(i, j - 1, j, condicion);
            return;
        }

        ComprobarSiHayPiezasComestibles(i, j + 1, j, condicion);
        ComprobarSiHayPiezasComestibles(i, j - 1, j, condicion);
    }

    #endregion


    #region ComprobarSiHayPiezasComestibles

    private void ComprobarSiHayPiezasComestibles(int i, int x, int j, int condicion)
    {
        if (mapaDelTablero[i + condicion, x].TipoPieza == -1) return;
        if (mapaDelTablero[i + condicion, x].Color == mapaDelTablero[i, j].Color) return;

        var pictureBox = mapaDelTablero[i + condicion, x].PictureBox;
        pictureBox.BackColor = pictureBox.Name == "Negro" ? ColorNegro : ColorBlanco;
        temporalDeClick.Add(new Point(i + condicion, x));
    }

*/
}