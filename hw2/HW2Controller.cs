using System;

namespace Raytracer.HW2;

public class HW2Controller
{
    public static void Main(string[] args)
    {
        var img = new Image(64, 64, 2.2f);
        img.Paint(32, 32, new Vector(1f, 0f, 0f), 255);
        img.SaveImage("output.png");
    }
}
