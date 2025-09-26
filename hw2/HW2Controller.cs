using System;

namespace Raytracer.HW2;

public class HW2Controller
{
    public static void Main(string[] args)
    {
        Camera c = new Camera();

        c.RenderIMage("test.bmp");

        Camera c2 = new Camera(Camera.Projection.Perspective,
                    new Vector(0.0f, 0.0f, 50.0f),
                    new Vector(0.0f, 0.0f, 0.0f),
                    new Vector(0.0f, 0.0f, 0.0f)
                    );

        c2.RenderIMage("test.bmp");
    }
}
