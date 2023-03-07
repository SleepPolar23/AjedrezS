using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Ajedrez.Controller;

public class LoaderImage
{
    private string _carpetaB = "Piezas/Piezas Blancas";
    private string _carpetaN = "Piezas/Piezas Negras";

    public IEnumerable<Image> GetBlancas() => GetPiezas(_carpetaB);

    public IEnumerable<Image> GetNegras() => GetPiezas(_carpetaN);

    private IEnumerable<Image> GetPiezas(string nameFolder)
    {
        var files = Directory.GetFiles(nameFolder);
        foreach (var file in files)
        {
            yield return Image.FromFile(file);
        }
    }
}