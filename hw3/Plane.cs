using System;
using System.Collections.Generic;

namespace Raytracer.HW3;

public class Plane : Shape
{
    private Vector _normal;
    private Vector _point;

     public Vector PlaneNormal
    {
        get { return _normal; }
        set { _normal = value; }
    }

    public Vector Point
    {
        get { return _point; }
        set { _point = value; }
    }

    public Plane()
    {
        PlaneNormal = new Vector(0f, 1f, 0f);
        Point = new Vector(0f, 0f, 0f);
        Center = Point;
        DiffuseColor = new Vector(255f, 255f, 255f);
    }

    public Plane(Vector normal, Vector point)
    {
        PlaneNormal = normal;
        Point = point;
        Center = Point;
        DiffuseColor = new Vector(255f, 255f, 255f);
    }

    public override float Hit(Ray r)
    {
        Vector o = r.Origin;
        Vector d = r.Direction;
        Vector a = Point;
        Vector n = PlaneNormal;

        float denominator = Vector.Dot(d, n);

        if (denominator == 0)
        {
            return float.PositiveInfinity;
        }
            
        float t = Vector.Dot(a - o, n) / denominator;
        return t;
    }
    
    public override Vector Normal(Vector p)
    {
        return PlaneNormal;
    }
}