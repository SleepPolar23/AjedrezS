using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Ajedrez.Controller;
using static Ajedrez.Controller.Tablero;

namespace Ajedrez;

public partial class Ajedrez : Form
{
    public Ajedrez()
    {
        InitializeComponent();
    }

    private void Form1_Load(object sender, EventArgs e) => CrearTablero(100, 8, 8);


    private void CrearTablero(int size, int rows, int columns)
    {
        var tablero = new Tablero(size, rows, columns, PanelTablero);

        PanelTablero.Location = new Point((Width - PanelTablero.Width) / 2, (Height - PanelTablero.Height) / 2);
        PanelTablero.Visible = true;
    }

    public static void PictureBox_Click(object sender, EventArgs e, Casilla[,] casillas)
    {
        PictureBox pb = (PictureBox)sender;

        Point coordenada = (Point)pb.Tag;

        bool PuedoMover = casillas[coordenada.Y, coordenada.X].PuedeMover;

        if (!PuedoMover && listaPuedeMover.Count != 0)
        {
            DevolverColorCasilla(casillas);
        }
        if(pb.Image != null && !PuedoMover && casillas[coordenada.Y, coordenada.X].Pieza.Color == turno)
        {
            casillas[coordenada.Y, coordenada.X].Pieza.ObtenerMovimientosPosibles(casillas, coordenada);
            ultimaPosicion = new Point(coordenada.X, coordenada.Y);
            return;
        }
        if (PuedoMover)
        {
            DevolverColorCasilla(casillas);
            turno *= -1;
            casillas[ultimaPosicion.Y, ultimaPosicion.X].Pieza.MovimientoEspecial = false;
            casillas[coordenada.Y, coordenada.X].Pieza = casillas[ultimaPosicion.Y, ultimaPosicion.X].Pieza;
            casillas[ultimaPosicion.Y, ultimaPosicion.X].Pieza = null;
        }
    }

    public static void DevolverColorCasilla(Casilla[,] casillas)
    {
        foreach (Point mover in listaPuedeMover)
        {
            casillas[mover.Y, mover.X].CambiarPuedeMover(false);
        }
        listaPuedeMover.Clear();
    }
}