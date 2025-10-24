using System;

// what about GetHashCode ? 

/// <summary>
/// Represents a 3D vector with single-precision components and common operations.
/// </summary>
namespace Raytracer.HW3;

public class Vector
{
    private float _x;
    private float _y;
    private float _z;

    /// <summary>
    /// X component.
    /// </summary>
    public float X
    {
        get { return _x; }
        set { _x = value; }
    }

    /// <summary>
    /// Y component.
    /// </summary>
    public float Y
    {
        get { return _y; }
        set { _y = value; }
    }

    /// <summary>
    /// Z component.
    /// </summary>
    public float Z
    {
        get { return _z; }
        set { _z = value; }
    }

    /// <summary>
    /// Initializes a zero vector (0, 0, 0).
    /// </summary>
    public Vector()
    {
        X = 0;
        Y = 0;
        Z = 0;
    }

    /// <summary>
    /// Initializes a vector with specified components.
    /// </summary>
    /// <param name="x">X component.</param>
    /// <param name="y">Y component.</param>
    /// <param name="z">Z component.</param>
    public Vector(float x, float y, float z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    // unary 
    /// <summary>
    /// Unary plus; returns a copy of the vector.
    /// </summary>
    public static Vector operator +(Vector vec)
    {
        return new Vector(vec.X, vec.Y, vec.Z);
    }

    // unary
    /// <summary>
    /// Unary negation; returns the negated vector.
    /// </summary>
    public static Vector operator -(Vector vec)
    {
        return new Vector(-vec.X, -vec.Y, -vec.Z);
    }

    // binary
    /// <summary>
    /// Vector addition.
    /// </summary>
    public static Vector operator +(Vector vec1, Vector vec2)
    {
        return new Vector(vec1.X + vec2.X, vec1.Y + vec2.Y, vec1.Z + vec2.Z);
    }

    // binary
    /// <summary>
    /// Vector subtraction.
    /// </summary>
    public static Vector operator -(Vector vec1, Vector vec2)
    {
        return new Vector(vec1.X - vec2.X, vec1.Y - vec2.Y, vec1.Z - vec2.Z);
    }

    // binary
    /// <summary>
    /// Scalar multiplication (scalar on the left).
    /// </summary>
    /// <param name="k">Scalar multiplier.</param>
    public static Vector operator *(float k, Vector vec)
    {
        return new Vector(k * vec.X, k * vec.Y, k * vec.Z);
    }

    // binary
    /// <summary>
    /// Scalar multiplication (scalar on the right).
    /// </summary>
    /// <param name="k">Scalar multiplier.</param>
    public static Vector operator *(Vector vec, float k)
    {
        return new Vector(vec.X * k, vec.Y * k, vec.Z * k);
    }

    // unary - for magnitude
    /// <summary>
    /// Magnitude (length) of the vector.
    /// </summary>
    public static double operator ~(Vector vec)
    {
        return Math.Sqrt(vec.X * vec.X + vec.Y * vec.Y + vec.Z * vec.Z);
    }

    /// <summary>
    /// Dot product of two vectors.
    /// </summary>
    /// <returns>Scalar dot product.</returns>
    public static float Dot(Vector vec1, Vector vec2)
    {
        return vec1.X * vec2.X + vec1.Y * vec2.Y + vec1.Z * vec2.Z;
    }

    /// <summary>
    /// Cross product of two vectors.
    /// </summary>
    /// <returns>Vector perpendicular to both inputs.</returns>
    public static Vector Cross(Vector vec1, Vector vec2)
    {
        return new Vector(
            vec1.Y * vec2.Z - vec1.Z * vec2.Y,
            vec1.Z * vec2.X - vec1.X * vec2.Z,
            vec1.X * vec2.Y - vec1.Y * vec2.X
        );
    }

    /// <summary>
    /// Returns the angle between two vectors in radians.
    /// </summary>
    /// <remarks>Clamps cosine to [-1, 1] to avoid NaN due to rounding.</remarks>
    public static double GetAngle(Vector vec1, Vector vec2) // need to check if dividing by 0
    {
        double dot = Dot(vec1, vec2);
        double magnitudeVec1 = ~vec1;
        double magnitudeVec2 = ~vec2;
        double cosTheta = dot / (magnitudeVec1 * magnitudeVec2);
        cosTheta = Math.Max(-1.0, Math.Min(1.0, cosTheta)); // prevent rounding error issues
        double angle = Math.Acos(cosTheta);

        return angle;
    }

    /// <summary>
    /// Converts each component to its absolute value in place.
    /// </summary>
    /// <param name="vec">Vector to modify.</param>
    public static void Abs(ref Vector vec)
    {
        vec.X = Math.Abs(vec.X);
        vec.Y = Math.Abs(vec.Y);
        vec.Z = Math.Abs(vec.Z);
    }

    /// <summary>
    /// Normalizes the vector in place to unit length if non-zero.
    /// </summary>
    /// <param name="vec">Vector to normalize.</param>
    public static void Normalize(ref Vector vec)
    {
        double mag = ~vec;
        if (mag > 0) // no division by 0
        {
            vec.X = (float)(vec.X / mag);
            vec.Y = (float)(vec.Y / mag);
            vec.Z = (float)(vec.Z / mag);
        }
    }

    /// <summary>
    /// Returns a string in the form (X, Y, Z).
    /// </summary>
    public override string ToString()
    {
        return $"({X}, {Y}, {Z})";
    }
}
