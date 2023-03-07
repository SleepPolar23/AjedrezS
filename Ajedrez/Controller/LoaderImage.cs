using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Ajedrez.Controller;

public class LoaderImage
{
    private string _carpetaB = "Piezas/Piezas Blancas";
    private string _carpetaN = "Piezas/Piezas Negras";

    public IEnumerable<Image> GetBlancas()
    {
        // get the names of files in _carpetaB
        var files = Directory.GetFiles(_carpetaB);
        foreach (var file in files)
        {
            yield return Image.FromFile(file);
        }
    }

    public IEnumerable<Image> GetNegras()
    {
        // get the names of files in _carpetaN
        var files = Directory.GetFiles(_carpetaN);
        foreach (var file in files)
        {
            yield return Image.FromFile(file);
        }
    }
}