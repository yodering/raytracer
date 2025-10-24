using System;


namespace Raytracer.HW3;

public class Ray
{
    private Vector _origin;
    private Vector _direction;

    public Vector Origin
    {
        get { return _origin; }
        set { _origin = value; }
    }

    public Vector Direction
    {
        get { return _direction; }
        set
        {
            Vector normalizedValue = new Vector(value.X, value.Y, value.Z);
            Vector.Normalize(ref normalizedValue);
            _direction = normalizedValue;
        }
    }

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
        Vector normalizedDirection = new Vector(direction.X, direction.Y, direction.Z);
        Vector.Normalize(ref normalizedDirection);
        Direction = normalizedDirection;
    }

    public Vector At(float t)
    {
        return Origin + Direction * t;
    }
}