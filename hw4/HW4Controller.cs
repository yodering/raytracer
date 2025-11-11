using System;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Raytracer.HW4;

public class HW4Controller
{
    static void Main(string[] args)
    {
        GenerateSphereScene();
        GenerateSphereArray();
    }

    static void GenerateSphereScene()
    {
        Camera c2 = new Camera(Camera.Projection.Perspective,
        new Vector(0.0f, 20.0f, 100.0f),
        new Vector(0.0f, 0f, 0f),
        new Vector(0.0f, 1f, 0f),
        0.1f, 150f, 512, 512, -10f, 10f, -10f, 10f);

        Scene scene = new Scene();

        scene.Light = new Vector(0f, 100f, 150f);

        Shape s = new Sphere(new Vector(0.0f, 10.0f, 50.0f), 20f);
        Shape s2 = new Sphere(new Vector(50.0f, 15.0f, 10.0f), 30f);
        Shape s3 = new Sphere(new Vector(-60f, 30f, -10.0f), 60f);

        s.DiffuseColor = new Vector(255f, 0.0f, 0.0f);
        s.A = new Vector(20, 0, 0);
        s.D = new Vector(255f, 0.0f, 0.0f);
        s.S = new Vector(255f, 255f, 255f);
        s.Shiny = 32f;

        s2.DiffuseColor = new Vector(200f, 0.0f, 255f);
        s2.A = new Vector(20, 0, 20);
        s2.D = new Vector(200f, 0.0f, 255f);
        s2.S = new Vector(255f, 255f, 255f);
        s2.Shiny = 64f;

        s3.DiffuseColor = new Vector(0.0f, 255f, 0.0f);
        s3.A = new Vector(0, 20, 0);
        s3.D = new Vector(0.0f, 255f, 0.0f);
        s3.S = new Vector(255f, 255f, 255f);
        s3.Shiny = 16f;

        Shape p1 = new Plane();
        p1.DiffuseColor = new Vector(0.0f, 0.0f, 255f);
        p1.A = new Vector(0, 0, 20);
        p1.D = new Vector(0.0f, 0.0f, 255f);
        p1.S = new Vector(100f, 100f, 100f);
        p1.Shiny = 8f;

        scene.AddShape(ref p1);
        scene.AddShape(ref s3);
        scene.AddShape(ref s2);
        scene.AddShape(ref s);
        c2.RenderImage("SphereScene.bmp", scene);
    }

    static void GenerateSphereArray()
    {
        Camera c = new Camera(Camera.Projection.Orthographic,
        new Vector(0.0f, 0.0f, 100.0f),
        new Vector(0.0f, 0f, -1f),
        new Vector(0.0f, 1f, 0f),
        0.1f, 200f, 512, 512, -50f, 50f, -50f, 50f);

        Scene scene = new Scene();
        scene.Light = new Vector(0f, 100f, 200f);

        Random rand = new Random(42);

        float spacing = 10f;
        float radius = 4.5f;
        float startX = -45f;
        float startY = -45f;

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                float x = startX + i * spacing;
                float y = startY + j * spacing;
                float z = 0f;

                Shape sphere = new Sphere(new Vector(x, y, z), radius);

                float r = (float)(rand.NextDouble() * 255);
                float g = (float)(rand.NextDouble() * 255);
                float b = (float)(rand.NextDouble() * 255);

                float shiny = (float)(rand.NextDouble() * 127) + 1f;

                sphere.DiffuseColor = new Vector(r, g, b);
                sphere.A = new Vector(r * 0.1f, g * 0.1f, b * 0.1f);
                sphere.D = new Vector(r, g, b);
                sphere.S = new Vector(255f, 255f, 255f);
                sphere.Shiny = shiny;

                scene.AddShape(ref sphere);
            }
        }

        c.RenderImage("SphereArray.bmp", scene);
    }
}
