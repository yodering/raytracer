using System;
using IronSoftware.Drawing;

// dotnet add package IronSoftware.System.Drawing --version 2025.9.3

/// <summary>
/// Simple RGBA image buffer with gamma-correct saving.
/// </summary>
public class Image
{
    private int _width;
    private int _height;
    private float _gamma;
    private Vector[,] _pixels;
    private int[,] _alpha;

    /// <summary>
    /// Image width in pixels.
    /// </summary>
    public int Width
    {
        get { return _width; }
        set { _width = value; }
    }
    /// <summary>
    /// Image height in pixels.
    /// </summary>
    public int Height
    {
        get { return _height; }
        set { _height = value; }
    }
    /// <summary>
    /// Gamma value used for output correction when saving.
    /// </summary>
    public float Gamma
    {
        get { return _gamma; }
        set { _gamma = value; }
    }

    /// <summary>
    /// 2D array of linear RGB color vectors indexed as [row, column].
    /// </summary>
    public Vector[,] Pixels
    {
        get { return _pixels; }
        set { _pixels = value; }
    }

    /// <summary>
    /// 2D array of per-pixel alpha values (0–255) indexed as [row, column].
    /// </summary>
    public int[,] Alpha
    {
        get { return _alpha; }
        set { _alpha = value; }
    }

    /// <summary>
    /// Creates a new image and initializes all pixels to black and fully opaque.
    /// </summary>
    /// <param name="width">Image width in pixels (default 512).</param>
    /// <param name="height">Image height in pixels (default 512).</param>
    /// <param name="gamma">Gamma used for output correction (default 1.8).</param>
    public Image(int width = 512, int height = 512, float gamma = 1.8f)
    {
        Width = width;
        Height = height;
        Gamma = gamma;

        Pixels = new Vector[Height, Width];
        Alpha = new int[Height, Width];

        for (int j = 0; j < Height; j++)
        {
            for (int i = 0; i < Width; i++)
            {
                Pixels[j, i] = new Vector(0, 0, 0);
                Alpha[j, i] = 255;
            }
        }
    }

    /// <summary>
    /// Sets the pixel at (i, j) to a color and alpha if within bounds.
    /// Interprets j as world-space Y with origin at bottom-left.
    /// </summary>
    /// <param name="i">Column index (x).</param>
    /// <param name="j">World-space row index (y), origin at bottom.</param>
    /// <param name="color">Linear RGB color vector.</param>
    /// <param name="alpha">Alpha value 0–255 (default 255).</param>
    public void Paint(int i, int j, Vector color, int alpha = 255)
    {
        // bounds check
        if (i < 0 || i >= Width || j < 0 || j >= Height) return;

        Pixels[j, i] = color;
        Alpha[j, i] = alpha;
    }

    /// <summary>
    /// Saves the image to a file, applying gamma correction to RGB channels.
    /// </summary>
    /// <param name="name">Output file path (e.g., .png).</param>
    public void SaveImage(string name)
    { // actually writing image to file...
        var solution = new AnyBitmap(Width, Height); // using ironsoftware drawing instead

        for (int j = 0; j < Height; j++)
        { // go through each pixel and apply gamma correction and write to file

            int row = Height - 1 - j; // flipping the origin thing

            for (int i = 0; i < Width; i++)
            {
                Vector current = Pixels[row, i];
                int alpha = Alpha[row, i];
                int red = (int)(255 * Math.Pow(current.X, 1 / Gamma));
                int green = (int)(255 * Math.Pow(current.Y, 1 / Gamma));
                int blue = (int)(255 * Math.Pow(current.Z, 1 / Gamma));

                // implement clamping

                solution.SetPixel(i, j, Color.FromArgb(alpha, red, green, blue));
            }
        }
        solution.SaveAs(name); // different save for ironsoftware
    }


}
