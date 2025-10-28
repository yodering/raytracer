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
        Shape s = new Sphere(new Vector(0.0f, 10.0f, 50.0f), 20f);
        Shape s2 = new Sphere(new Vector(50.0f, 15.0f, 10.0f), 30f);
        Shape s3 = new Sphere(new Vector(-60f, 30f, -10.0f), 60f);
        s3.DiffuseColor = new Vector(0.0f, 255f, 0.0f);
        s2.DiffuseColor = new Vector(200f, 0.0f, 255f);
        s.DiffuseColor = new Vector(255f, 0.0f, 0.0f);
        Shape p1 = new Plane();
        p1.DiffuseColor = new Vector(0.0f, 0.0f, 255f); // adding colors to plane
        scene.AddShape(ref p1);
        scene.AddShape(ref s3);
        scene.AddShape(ref s2);
        scene.AddShape(ref s);
        c2.RenderImage("test.bmp", scene);

    }
}
