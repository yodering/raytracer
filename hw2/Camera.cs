using System;


namespace Raytracer.HW2;

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
    /// Renders and saves an image to the specified file.
    /// Casts a ray through each pixel and colors it accordingly.
    /// </summary>
    /// <param name="filename">The name of the .bmp file to save.</param>
    public void RenderImage(string filename)
    {

        // change to iron software?
        Image image = new Image(_width, _height, 0.8f);

        Vector white = new Vector(255, 255, 255);
        Vector blue = new Vector(128, 200, 255);

        // each pixel
        for (int j = 0; j < _height; j++)
        {
            for (int i = 0; i < _width; i++)
            {
                Vector color;

                if (_projection == Projection.Orthographic)
                {
                    // for orthographic: get the ray origin through pixel (i, j)
                    Vector origin = GetOrthographicRayOrigin(i, j);

                    Vector.Normalize(ref origin);
                    float x = origin.X;

                    x = Math.Max(0.0f, x);

                    // linear interpolation: (1 - t) * color1 + t * color2
                    color = (1.0f - x) * white + x * blue;
                }
                else
                {
                    // perspective projection
                    // get the ray direction through pixel (i, j)
                    Vector direction = GetPerspectiveRayDirection(i, j);

                    float y = direction.Y;

                    y = Math.Max(0.0f, y);

                    color = (1.0f - y) * white + y * blue;
                }

                Vector normalizedColor = new Vector(
                    color.X / 255.0f,
                    color.Y / 255.0f,
                    color.Z / 255.0f
                );

                // set the pixel color in the image buffer
                image.Paint(i, j, normalizedColor);
            }
        }
        image.SaveImage(filename);
    }

    /// <summary>
    /// Computes the ray origin for orthographic projection through pixel (i, j).
    /// </summary>
    /// <param name="i">Pixel x-coordinate.</param>
    /// <param name="j">Pixel y-coordinate.</param>
    /// <returns>The ray origin in world coordinates.</returns>
    private Vector GetOrthographicRayOrigin(int i, int j)
    {

        // nap horizontal pixel coordinate to camera space [-left, right]
        float u_coord = _left + (_right - _left) * (i + 0.5f) / _width;

        // map vertical pixel coordinate to camera space [bottom, top]
        float v_coord = _bottom + (_top - _bottom) * (j + 0.5f) / _height;
        Vector origin = _eye + u_coord * _u + v_coord * _v - _near * _w;

        return origin;
    }

    /// <summary>
    /// Computes the ray direction for perspective projection through pixel (i, j).
    /// </summary>
    /// <param name="i">Pixel x-coordinate.</param>
    /// <param name="j">Pixel y-coordinate.</param>
    /// <returns>The normalized ray direction.</returns>
    private Vector GetPerspectiveRayDirection(int i, int j)
    {
        // map pixel (i, j) to a direction vector from the eye through the pixel

        // map horizontal pixel coordinate to camera space [left, right]
        float u_coord = _left + (_right - _left) * (i + 0.5f) / _width;

        // map vertical pixel coordinate to camera space [bottom, top]
        float v_coord = _bottom + (_top - _bottom) * (j + 0.5f) / _height;
        Vector direction = -_near * _w + u_coord * _u + v_coord * _v;

        return direction;
    }
}