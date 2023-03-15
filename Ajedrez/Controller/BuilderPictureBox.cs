using System;
using System.Drawing;
using System.Windows.Forms;
using Ajedrez.Controller.Enums;

namespace Ajedrez.Controller;

public class BuilderPictureBox
{
    private int size;
    private PictureBox _pictureBox;
    private Color color;
    private int i, j;

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

    public BuilderPictureBox WithLocation(int i, int j)
    {
        this.i = i;
        this.j = j;
        return this;
    }

    public BuilderPictureBox WithAction(EventHandler action)
    {
        _pictureBox.Click += action;
        return this;
    }
    
    public BuilderPictureBox WithColorCasilla(TipoCasilla colorCasilla)
    {
        color = SetColorCasilla(colorCasilla);
        return this;
    }

    public PictureBox Build()
    {
        _pictureBox.Size = new Size(size, size);
        _pictureBox.Location = new Point(j * size, i * size);

        _pictureBox.BackColor = color;
        _pictureBox.Tag = new Point(j, i);

        return _pictureBox;
    }

    public static Color SetColorCasilla(TipoCasilla colorCasilla)
    {
        if (colorCasilla == TipoCasilla.Blanco)
            return Colores.BlancoNormal;

        return Colores.NegroNormal;
    }
}