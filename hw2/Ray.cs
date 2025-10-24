using System;


namespace Raytracer.HW2;

/// <summary>
/// Represents a ray in 3D space with an origin point and direction vector.
/// Used for ray tracing calculations to determine intersections with objects.
/// </summary>
public class Ray
{
    private Vector _origin;
    private Vector _direction;

    /// <summary>
    /// Gets or sets the origin point of the ray.
    /// </summary>
    public Vector Origin
    {
        get { return _origin; }
        set { _origin = value; }
    }

    /// <summary>
    /// Gets or sets the direction vector of the ray. The direction is automatically normalized when set.
    /// </summary>
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

    /// <summary>
    /// Initializes a new instance of the Ray class with default origin and direction pointing in the positive Z direction.
    /// </summary>
    public Ray()
    {
        Origin = new Vector();
        Direction = new Vector(0f, 0f, 1f);
    }

    /// <summary>
    /// Initializes a new instance of the Ray class with specified origin and direction vectors.
    /// The direction vector is automatically normalized.
    /// </summary>
    /// <param name="origin">The origin point of the ray.</param>
    /// <param name="direction">The direction vector of the ray.</param>
    /// <exception cref="ArgumentNullException">Thrown when origin or direction is null.</exception>
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

    /// <summary>
    /// Calculates a point along the ray at the specified parameter value.
    /// </summary>
    /// <param name="t">The parameter value (distance along the ray).</param>
    /// <returns>A point on the ray at parameter t, calculated as Origin + Direction * t.</returns>
    public Vector At(float t)
    {
        return Origin + Direction * t;
    }
}