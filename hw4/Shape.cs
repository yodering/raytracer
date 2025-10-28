using System;
using System.Collections.Generic;

namespace Raytracer.HW4;

public abstract class Shape
{

    private Vector _diffuseColor;
    private Vector _center;

     public Vector DiffuseColor
    {
        get { return _diffuseColor; }
        set { _diffuseColor = value; }
    }

    public Vector Center
    {
        get { return _center; }
        set { _center = value; }
    }

    /// <summary>
    /// Determines if the shape object has been hit by the ray input.
    /// </summary>
    /// <param name="r">The ray.</param>
    /// <returns>The intersection distance from the ray origin. Return infinity if there is no intersection.</returns>
    public abstract float Hit(Ray r);

    /// <summary>
    /// Calculates the normal of the object at the given point on the object.
    /// </summary>
    /// <param name="p">A point on the object</param>
    /// <returns>A vector of the normal of the object at that point.</returns>
    public abstract Vector Normal(Vector p);
}