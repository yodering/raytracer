using System;

namespace Raytracer.HW1;

public class HW1Controller
{
    /// <summary>
    /// Application entry point; creates an image, paints a pixel, and saves it.
    /// </summary>
    /// <param name="args">Command-line arguments (unused).</param>
    public static void Main(string[] args)
    {
        var img = new Image(64, 64, 2.2f);
        img.Paint(32, 32, new Vector(1f, 0f, 0f), 255);
        img.SaveImage("output.png");
    }
}
