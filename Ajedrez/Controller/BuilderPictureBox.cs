using System;
using System.Drawing;
using System.Windows.Forms;

namespace Ajedrez.Controller;

public class BuilderPictureBox
{
    private int size;
    private Casilla _casilla;
    private PictureBox _pictureBox;

    private BuilderPictureBox()
    {
        _pictureBox = new PictureBox();
    }

    public static BuilderPictureBox Create()
    {
        return new BuilderPictureBox();
    }

    public BuilderPictureBox WithSize(int size)
    {
        this.size = size;
        return this;
    }

    public BuilderPictureBox ToCasilla(Casilla casilla)
    {
        _casilla = casilla;
        return this;
    }

    public BuilderPictureBox WithAction(EventHandler action)
    {
        _pictureBox.Click += action;
        return this;
    }

    public PictureBox Build()
    {
        _pictureBox.Size = new Size(size, size);
        _pictureBox.Location = new Point(_casilla.Columna * size, _casilla.Fila * size);

        if (_casilla.ColorCasilla == ColorCasilla.Blanco) SetColorBlanco();
        else SetColorNegro();

        _pictureBox.Image = _casilla.Pieza?._image;

        return _pictureBox;
    }

    private void SetColorBlanco()
    {
        _pictureBox.BackColor = Color.FromArgb(106, 117, 136); // Asignar el color negro
        _pictureBox.Name = "Blanco";
    }

    private void SetColorNegro()
    {
        _pictureBox.BackColor = Color.FromArgb(42, 49, 62); // Asignar el color blanco
        _pictureBox.Name = "Negro";
    }
}