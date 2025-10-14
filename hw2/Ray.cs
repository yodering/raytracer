using System;


namespace Raytracer.HW2;

public class Ray
{

    public Vector Origin { get; set; }
    public Vector Direction { get; set; }

    public Ray()
    {
        Origin = new Vector();
        Direction = new Vector(0f, 0f, 1f);
    }

    public Ray(Vector origin, Vector direction)
    {
        // checks for validity
        Origin = origin ?? throw new ArgumentNullException(nameof(origin));

        if (direction == null)
            throw new ArgumentNullException(nameof(direction));
        // normalize
        Vector.Normalize(ref direction);
        Direction = direction;
    }

    public Vector At(float t)
    {
        return Origin + Direction * t;
    }
}