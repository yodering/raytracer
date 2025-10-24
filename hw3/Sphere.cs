using System;
using System.Collections.Generic;

namespace Raytracer.HW3;

public class Sphere : Shape
{
    private float _radius;


    public float Radius
    {
        get { return _radius; }
        set { _radius = value; }
    }

    public Sphere()
    {
        Center = new Vector(0f, 0f, 0f);
        Radius = 1f;   
        DiffuseColor = new Vector(255f, 255f, 255f);
    }

    public Sphere(Vector center, float radius)
    {
        Center = center;
        Radius = radius;
        DiffuseColor = new Vector(255f, 255f, 255f);
    }

    public override float Hit(Ray r)
    {
        Vector c = Center;
        Vector o = r.Origin;
        Vector d = r.Direction;

        Vector o_minus_c = o - c;
        float d_dot_d = Vector.Dot(d, d);

        float discriminant = (float)Math.Pow(Vector.Dot(d, o_minus_c), 2) - d_dot_d * (Vector.Dot(o_minus_c, o_minus_c) - (float)Math.Pow(Radius, 2));

        if (discriminant < 0)
        {
            return float.PositiveInfinity;
        }

        float sqrt_discriminant = (float)Math.Sqrt(discriminant);

        float t_plus = (-Vector.Dot(d, o_minus_c) + sqrt_discriminant) / d_dot_d;
        float t_minus = (-Vector.Dot(d, o_minus_c) - sqrt_discriminant) / d_dot_d;
        float t = 0;



        if (t_minus > 0)
        {
            t = Math.Min(t_minus, t_plus);
        }
        else
        {
            if (t_plus > 0)
            {
                t = t_plus;
            }
        }

        return t;


    }
    
    public override Vector Normal(Vector p)
    {
        Vector normal = p - Center;
        Vector.Normalize(ref normal);
        return normal;
    }
}