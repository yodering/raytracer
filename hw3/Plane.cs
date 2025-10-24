using System;
using System.Collections.Generic;

namespace Raytracer.HW3;

/// <summary>
/// Represents a plane in 3D space for ray tracing.
/// A plane is defined by a normal vector and a point on the plane.
/// </summary>
public class Plane : Shape
{
    private Vector _normal;
    private Vector _point;

    /// <summary>
    /// Gets or sets the normal vector of the plane.
    /// </summary>
    public Vector PlaneNormal
    {
        get { return _normal; }
        set { _normal = value; }
    }

    /// <summary>
    /// Gets or sets a reference point on the plane.
    /// </summary>
    public Vector Point
    {
        get { return _point; }
        set { _point = value; }
    }

    /// <summary>
    /// Initializes a new instance of the Plane class with default values.
    /// Creates a plane on the x-z axis with normal pointing in the positive y direction.
    /// </summary>
    public Plane()
    {
        PlaneNormal = new Vector(0f, 1f, 0f);
        Point = new Vector(0f, 0f, 0f);
        Center = Point;
        DiffuseColor = new Vector(255f, 255f, 255f);
    }

    /// <summary>
    /// Initializes a new instance of the Plane class with specified normal and point.
    /// </summary>
    /// <param name="normal">The normal vector of the plane.</param>
    /// <param name="point">A reference point on the plane.</param>
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