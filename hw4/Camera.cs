using System;


namespace Raytracer.HW4;

public class Camera
{
    // enum for projection type
    public enum Projection
    {
        Perspective,
        Orthographic
    }

    // instance variables
    private Projection _projection;
    private Vector _eye;
    private Vector _lookAt;
    private Vector _up;
    private float _near;
    private float _far;
    private int _width;
    private int _height;
    private float _left;
    private float _right;
    private float _bottom;
    private float _top;

    // Computed basis vectors for camera coordinate system
    private Vector _u;
    private Vector _v;
    private Vector _w;

    /// <summary>
    /// Creates a new generic
    /// orthographic <c>Camera</c> centered at the origin. The generated image
    /// plane is 512 x 512 pixels and the viewing frustum is 2 x 2 units.
    /// </summary>
    public Camera()
    {
        // initialize default orthographic camera
        _projection = Projection.Orthographic;
        _eye = new Vector(0, 0, 0);
        _lookAt = new Vector(0, 0, -1);
        _up = new Vector(0, 1, 0);
        _near = 0.1f;
        _far = 10.0f;
        _width = 512;
        _height = 512;
        _left = -1.0f;
        _right = 1.0f;
        _bottom = -1.0f;
        _top = 1.0f;

        ComputeBasis();
    }

    /// <summary>
    /// Creates a new <c>Camera</c> object with the specified parameters.
    /// </summary>
    /// <param name="projection">The projection type for the camera (e.g., Perspective, Orthographic).</param>
    /// <param name="eye">The position of the camera's eye point in world coordinates.</param>
    /// <param name="lookAt">The location the camera is looking at in world coordinates.</param>
    /// <param name="up">The camera's up vector.</param>
    /// <param name="near">The distance from the camera's eye point to the near clipping plane.(default (.1). </param>
    /// <param name="far">The distance from the camera's eye point to the far clipping plane.(default: 10).</param>
    /// <param name="width">The width of the camera's viewport in pixels (default: 512).</param>
    /// <param name="height">The height of the camera's viewport in pixels (default: 512).</param>
    /// <param name="left">The left boundary of the camera's viewing frustum (default: -1.0).</param>
    /// <param name="right">The right boundary of the camera's viewing frustum (default: 1.0).</param>
    /// <param name="bottom">The bottom boundary of the camera's viewing frustum (default: -1.0).</param>
    /// <param name="top">The top boundary of the camera's viewing frustum (default: 1.0).</param>
    public Camera(Projection projection, Vector eye, Vector lookAt, Vector up,
                  float near = 0.1f, float far = 10.0f,
                  int width = 512, int height = 512,
                  float left = -1.0f, float right = 1.0f,
                  float bottom = -1.0f, float top = 1.0f
                  )
    {
        _projection = projection;
        _eye = eye;
        _lookAt = lookAt;
        _up = up;
        _near = near;
        _far = far;
        _width = width;
        _height = height;
        _left = left;
        _right = right;
        _bottom = bottom;
        _top = top;

        ComputeBasis();
    }

    /// <summary>
    /// Computes the orthonormal basis vectors (u, v, w) for the camera coordinate system.
    /// </summary>
    private void ComputeBasis()
    {
        _w = _eye - _lookAt;
        Vector.Normalize(ref _w);

        _u = Vector.Cross(_up, _w);
        Vector.Normalize(ref _u);

        _v = Vector.Cross(_w, _u);
    }

    /// <summary>
    /// Renders and saves a ray-traced image to the specified file.
    /// For each pixel, casts a ray through the scene and performs intersection testing
    /// with all shapes. Colors pixels based on the closest intersection using distance-based
    /// shading where closer objects appear brighter than distant ones.
    /// </summary>
    /// <param name="filename">The name of the .bmp file to save.</param>
    /// <param name="scene">The scene containing shapes to render.</param>
    public void RenderImage(string filename, Scene scene)
    {

        Image image = new Image(_width, _height, 0.8f);

        // each pixel
        if (_projection == Projection.Orthographic)
        {
            for (int j = 0; j < _height; j++)
            {
                for (int i = 0; i < _width; i++)
                {
                    // create ray through pixel (i, j)
                    Ray ray = GetOrthographicRay(i, j);

                    float closestT = float.PositiveInfinity;
                    Shape closestShape = null;

                    foreach (Shape shape in scene.GetShapes())
                    {
                        float t = shape.Hit(ray);
                        if (t > 0 && t < closestT && t <= _far)
                        {
                            closestT = t;
                            closestShape = shape;
                        }
                    }
                    Vector color;
                    if (closestShape != null)
                    {
                        color = closestShape.DiffuseColor * ((_far - closestT) / _far);
                    }
                    else
                    {
                        color = new Vector(0, 0, 0);
                    }
                    Vector normalizedColor = new Vector(color.X / 255.0f, color.Y / 255.0f, color.Z / 255.0f);
                    image.Paint(i, j, normalizedColor);
                }
            }
        }
        else
        {
            for (int j = 0; j < _height; j++)
            {
                for (int i = 0; i < _width; i++)
                {
                    // create ray through pixel (i, j)
                    Ray ray = GetPerspectiveRay(i, j);

                    float closestT = float.PositiveInfinity;
                    Shape closestShape = null;

                    foreach (Shape shape in scene.GetShapes())
                    {
                        float t = shape.Hit(ray);
                        if (t > 0 && t < closestT && t <= _far)
                        {
                            closestT = t;
                            closestShape = shape;
                        }
                    }
                    Vector color;
                    if (closestShape != null)
                    {
                        color = closestShape.DiffuseColor * ((_far - closestT) / _far);
                    }
                    else
                    {
                        color = new Vector(0, 0, 0);
                    }
                    Vector normalizedColor = new Vector(color.X / 255.0f, color.Y / 255.0f, color.Z / 255.0f);
                    image.Paint(i, j, normalizedColor);
                }
            }
        }
        image.SaveImage(filename);
    }

    /// <summary>
    /// Creates a ray for orthographic projection through pixel (i, j).
    /// </summary>
    /// <param name="i">Pixel x-coordinate.</param>
    /// <param name="j">Pixel y-coordinate.</param>
    /// <returns>A Ray object with origin and direction for the pixel.</returns>
    private Ray GetOrthographicRay(int i, int j)
    {
        // map horizontal pixel coordinate to camera space [-left, right]
        float u_coord = _left + (_right - _left) * i / _width;

        // map vertical pixel coordinate to camera space [bottom, top]
        float v_coord = _bottom + (_top - _bottom) * j / _height;

        Vector origin = _eye + u_coord * _u + v_coord * _v;
        Vector direction = -_w;

        return new Ray(origin, direction);
    }

    /// <summary>
    /// Creates a ray for perspective projection through pixel (i, j).
    /// </summary>
    /// <param name="i">Pixel x-coordinate.</param>
    /// <param name="j">Pixel y-coordinate.</param>
    /// <returns>A Ray object with origin at the eye and direction through the pixel.</returns>
    private Ray GetPerspectiveRay(int i, int j)
    {
        // map horizontal pixel coordinate to camera space [left, right]
        float u_coord = _left + (_right - _left) * i / _width;

        // map vertical pixel coordinate to camera space [bottom, top]
        float v_coord = _bottom + (_top - _bottom) * j / _height;

        // direction from eye through the pixel on the near plane
        Vector direction = u_coord * _u + v_coord * _v - _w;

        return new Ray(_eye, direction);
    }
}