using System;
using System.Collections.Generic;

namespace Raytracer.HW4;

/// <summary>
/// Represents a scene containing a collection of shapes for ray tracing.
/// </summary>
public class Scene
{
    private List<Shape> shapes;

    /// <summary>
    /// Initializes a new instance of the Scene class with an empty collection of shapes.
    /// </summary>
    public Scene()
    {
        shapes = new List<Shape>();
    }

    /// <summary>
    /// Adds a shape to the scene.
    /// </summary>
    /// <param name="shape">The shape to add to the scene.</param>
    public void AddShape(ref Shape shape)
    {
        shapes.Add(shape);
    }

    /// <summary>
    /// Gets all shapes in the scene for iteration.
    /// </summary>
    /// <returns>An enumerable collection of shapes in the scene.</returns>
    public IEnumerable<Shape> GetShapes()
    {
        return shapes;
    }

    /// <summary>
    /// Gets the number of shapes currently in the scene.
    /// </summary>
    public int Count => shapes.Count;
}