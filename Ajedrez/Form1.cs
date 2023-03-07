using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Ajedrez.Controller;


namespace Ajedrez;

public partial class Ajedrez : Form
{
    Color ColorNegro = Color.FromArgb(64, 95, 118);
    Color ColorBlanco = Color.FromArgb(98, 131, 157);
    Color ColorNegroSuave = Color.FromArgb(42, 49, 62);
    Color ColorBlancoSuave = Color.FromArgb(106, 117, 136);

    Piezas[,] mapaDelTablero = new Piezas[8, 8];
    List<Image> blancas;
    List<Image> negras;
    private bool PiezaClickeada;
    List<Point> temporalDeClick = new List<Point>();
    List<Point> ultimoMovimiento = new List<Point>();
    Point tempPiezaClick;
    int jugadorActual = 0;


    public Ajedrez()
    {
        InitializeComponent();
        ImagenesDeLasPiezas();
    }

    private void Form1_Load(object sender, EventArgs e) => CrearTablero(100, 8, 8);

    private void ImagenesDeLasPiezas()
    {
        var loaderImage = new LoaderImage();
        //Blancas
        blancas = loaderImage.GetBlancas().ToList();
        //Negras
        negras = loaderImage.GetNegras().ToList();
    }

    #region CrearTablero

    private void CrearTablero(int size, int rows, int cols)
    {
        var tablero = new Tablero(rows, rows);
        foreach (var tableroCasilla in tablero.Casillas)
        {
            PictureBox pb = new PictureBox(); // Crear un nuevo PictureBox
            pb.Size = new Size(size, size); // Asignar el tamaño
            pb.Location = new Point(tableroCasilla.Columna * size, tableroCasilla.Fila * size); // Asignar la ubicación
            if (tableroCasilla.ColorCasilla is ColorCasilla.Blanco)
            {
                pb.BackColor = Color.FromArgb(106, 117, 136); // Asignar el color negro
                pb.Name = "Blanco";
            }
            else
            {
                pb.BackColor = Color.FromArgb(42, 49, 62); // Asignar el color blanco
                pb.Name = "Negro";
            }

            pb.Click += PictureBox_Click;
            Tablero.Controls.Add(pb); // Añadir el PictureBox al panel
            mapaDelTablero[tableroCasilla.Fila, tableroCasilla.Columna] = new Piezas(pb);
        }

        OrdenarPiezas();
        Tablero.Location = new Point((Width - Tablero.Width) / 2, (Height - Tablero.Height) / 2);
        Tablero.Visible = true;
    }

    #endregion


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

    #endregion


    #region OrdenarPiezas

    private void OrdenarPiezas()
    {
        //Peones
        for (int j = 0; j < 8; j++)
        {
            mapaDelTablero[1, j].PictureBox.Image = blancas[0];
            mapaDelTablero[1, j].TipoPieza = 0;
            mapaDelTablero[1, j].Color = 1;
            mapaDelTablero[1, j].MovimientoEspecial = true;
            mapaDelTablero[6, j].PictureBox.Image = negras[0];
            mapaDelTablero[6, j].TipoPieza = 0;
            mapaDelTablero[6, j].Color = 0;
            mapaDelTablero[6, j].MovimientoEspecial = true;
        }

        //Torres
        mapaDelTablero[0, 0].PictureBox.Image = blancas[3];
        mapaDelTablero[0, 0].TipoPieza = 3;
        mapaDelTablero[0, 0].Color = 1;
        mapaDelTablero[0, 7].PictureBox.Image = blancas[3];
        mapaDelTablero[0, 7].TipoPieza = 3;
        mapaDelTablero[0, 7].Color = 1;

        mapaDelTablero[7, 0].PictureBox.Image = negras[3];
        mapaDelTablero[7, 0].TipoPieza = 3;
        mapaDelTablero[7, 0].Color = 0;
        mapaDelTablero[7, 7].PictureBox.Image = negras[3];
        mapaDelTablero[7, 7].TipoPieza = 3;
        mapaDelTablero[7, 7].Color = 0;
        //Caballos
        mapaDelTablero[0, 1].PictureBox.Image = blancas[1];
        mapaDelTablero[0, 1].TipoPieza = 1;
        mapaDelTablero[0, 1].Color = 1;
        mapaDelTablero[0, 6].PictureBox.Image = blancas[1];
        mapaDelTablero[0, 6].TipoPieza = 1;
        mapaDelTablero[0, 6].Color = 1;

        mapaDelTablero[7, 1].PictureBox.Image = negras[1];
        mapaDelTablero[7, 6].PictureBox.Image = negras[1];
        mapaDelTablero[7, 1].TipoPieza = 1;
        mapaDelTablero[7, 6].TipoPieza = 1;
        mapaDelTablero[7, 1].Color = 0;
        mapaDelTablero[7, 6].Color = 0;
        //Alfiles
        mapaDelTablero[0, 2].PictureBox.Image = blancas[2];
        mapaDelTablero[0, 2].TipoPieza = 2;
        mapaDelTablero[0, 2].Color = 1;
        mapaDelTablero[0, 5].PictureBox.Image = blancas[2];
        mapaDelTablero[0, 5].TipoPieza = 2;
        mapaDelTablero[0, 5].Color = 1;

        mapaDelTablero[7, 2].PictureBox.Image = negras[2];
        mapaDelTablero[7, 2].TipoPieza = 2;
        mapaDelTablero[7, 2].Color = 0;
        mapaDelTablero[7, 5].PictureBox.Image = negras[2];
        mapaDelTablero[7, 5].TipoPieza = 2;
        mapaDelTablero[7, 5].Color = 0;
        //Reinas
        mapaDelTablero[0, 3].PictureBox.Image = blancas[4];
        mapaDelTablero[0, 3].TipoPieza = 3;
        mapaDelTablero[0, 3].Color = 1;

        mapaDelTablero[7, 3].PictureBox.Image = negras[4];
        mapaDelTablero[7, 3].TipoPieza = 3;
        mapaDelTablero[7, 3].Color = 0;
        //Reyes
        mapaDelTablero[0, 4].PictureBox.Image = blancas[5];
        mapaDelTablero[0, 4].TipoPieza = 4;
        mapaDelTablero[0, 4].Color = 1;

        mapaDelTablero[7, 4].PictureBox.Image = negras[5];
        mapaDelTablero[7, 4].TipoPieza = 4;
        mapaDelTablero[7, 4].Color = 0;
    }

    #endregion
}