using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Ajedrez.Domain;

public class LoaderImage
{
    public static string CarpetaB = "Piezas/Piezas Blancas";
    public static string CarpetaN = "Piezas/Piezas Negras";
    private readonly string _folder;

    public LoaderImage(string folder)
    {
        _folder = folder;
    }

    public IEnumerable<Image> GetPiezas()
    {
        var files = Directory.GetFiles(_folder);
        foreach (var file in files)
        {
            yield return Image.FromFile(file);
        }
    }
}