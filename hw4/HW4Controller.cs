using System;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Raytracer.HW4;

public class HW4Controller
{
    static void Main(string[] args)
    {
        Camera c2 = new Camera(Camera.Projection.Perspective,
        new Vector(0.0f, 20.0f, 100.0f),
        new Vector(0.0f, 0f, 0f),
        new Vector(0.0f, 1f, 0f),
        0.1f, 150f, 512, 512, -10f, 10f, -10f, 10f);

        // Scene
        Scene scene = new Scene();

        // Set light position
        scene.Light = new Vector(100f, 100f, 100f);

        Shape s = new Sphere(new Vector(0.0f, 10.0f, 50.0f), 20f);
        Shape s2 = new Sphere(new Vector(50.0f, 15.0f, 10.0f), 30f);
        Shape s3 = new Sphere(new Vector(-60f, 30f, -10.0f), 60f);

        // Red sphere (s)
        s.DiffuseColor = new Vector(255f, 0.0f, 0.0f);
        s.A = new Vector(20, 0, 0);           // dark red ambient
        s.D = new Vector(255f, 0.0f, 0.0f);   // red diffuse
        s.S = new Vector(255f, 255f, 255f);   // white specular
        s.Shiny = 32f;

        // Purple sphere (s2)
        s2.DiffuseColor = new Vector(200f, 0.0f, 255f);
        s2.A = new Vector(20, 0, 20);         // dark purple ambient
        s2.D = new Vector(200f, 0.0f, 255f);  // purple diffuse
        s2.S = new Vector(255f, 255f, 255f);  // white specular
        s2.Shiny = 64f;

        // Green sphere (s3)
        s3.DiffuseColor = new Vector(0.0f, 255f, 0.0f);
        s3.A = new Vector(0, 20, 0);          // dark green ambient
        s3.D = new Vector(0.0f, 255f, 0.0f);  // green diffuse
        s3.S = new Vector(255f, 255f, 255f);  // white specular
        s3.Shiny = 16f;

        // Blue plane (p1)
        Shape p1 = new Plane();
        p1.DiffuseColor = new Vector(0.0f, 0.0f, 255f);
        p1.A = new Vector(0, 0, 20);          // dark blue ambient
        p1.D = new Vector(0.0f, 0.0f, 255f);  // blue diffuse
        p1.S = new Vector(100f, 100f, 100f);  // dimmer specular for plane
        p1.Shiny = 8f;

        scene.AddShape(ref p1);
        scene.AddShape(ref s3);
        scene.AddShape(ref s2);
        scene.AddShape(ref s);
        c2.RenderImage("test.bmp", scene);

    }
}
