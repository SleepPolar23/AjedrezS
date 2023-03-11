using System;
using System.Drawing;
using System.Windows.Forms;
using Ajedrez.Domain;

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

    public PictureBox Build(Label label)
    {
        _pictureBox.Size = new Size(size, size);
        _pictureBox.Location = new Point(_casilla.Coordenada.X * size, _casilla.Coordenada.Y * size);

        if (_casilla.ColorCasilla == ColorCasilla.Blanco) SetColorBlanco();
        else SetColorNegro();

        if (_casilla.Estado == EstadoCasilla.Seleccionada)
            SetColorSelected();

        if (_casilla.Estado == EstadoCasilla.PosibleMovimiento)
            _pictureBox.BackColor = Color.Blue;
        
        if (_casilla.Estado == EstadoCasilla.PosbileComer)
            _pictureBox.BackColor = Color.Red;

        _pictureBox.Image = _casilla.Pieza?._image;
        _pictureBox.MouseHover += (sender, args) =>
        {
            label.Text = $"X = {_casilla.Coordenada.X}, Y = {_casilla.Coordenada.Y}";
        };

        return _pictureBox;
    }

    private void SetColorBlanco()
    {
        _pictureBox.BackColor = Color.FromArgb(106, 117, 136); // Asignar el color negro
        _pictureBox.Name = "Blanco";
    }

    private void SetColorSelected()
    {
        _pictureBox.BackColor = Color.Aquamarine;
        _pictureBox.Name = "Blanco Seleccionado";
    }

    private void SetColorNegro()
    {
        _pictureBox.BackColor = Color.FromArgb(42, 49, 62); // Asignar el color blanco
        _pictureBox.Name = "Negro";
    }
}