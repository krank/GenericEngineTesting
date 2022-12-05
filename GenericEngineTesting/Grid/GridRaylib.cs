using Raylib_cs;

namespace GridSystem;

public class GridRaylib
{
  public static Rectangle GetRectangle(int x, int y, Grid grid)
  {
    return new(
            grid.WorldOffset.X + (grid.BlockSize.X * x),
            grid.WorldOffset.Y + (grid.BlockSize.Y * y),
            grid.BlockSize.X,
            grid.BlockSize.Y
          );
  }

  public static List<Rectangle> GetHitboxRectangles(Grid grid)
  {
    List<Rectangle> rectangles = new();

    grid.MarkCleanAll();

    for (int x = 0; x < grid.Width; x++)
    {
      for (int y = 0; y < grid.Height; y++)
      {
        (int width, int height) area = grid.GetLargestSolidArea(x, y);

        if (area.width > 0 && area.height > 0)
        {
          Rectangle rect = GetRectangle(x, y, grid);
          rect.width *= area.width;
          rect.height *= area.height;

          rectangles.Add(rect);

          grid.MarkDirtyArea(x, y, area.width, area.height);
        }

      }
    }

    return rectangles;
  }


  public static List<Rectangle> GetSolidRectangles(Grid grid)
  {
    List<Rectangle> rectangles = new();

    for (int x = 0; x < grid.Width; x++)
    {
      for (int y = 0; y < grid.Height; y++)
      {
        if (grid.GetBlock(x, y) != null && grid.GetBlock(x, y).IsSolid)
        {
          rectangles.Add(GetRectangle(x, y, grid));
        }
      }
    }

    return rectangles;
  }

  public class RaylibGridRenderer : GridRenderer
  {
    // Todo: Rectangle cache?

    public Color RectangleColor { get; set; }

    public RaylibGridRenderer(Grid grid, Color rectangleColor) : base(grid)
    {
      RectangleColor = rectangleColor;
    }

    public override void RenderBlockRectangle(int x, int y)
    {
      Rectangle r = GetRectangle(x, y, grid);

      if (grid.GetBlock(x, y) != null && grid.GetBlock(x, y).IsSolid)
      {
        Raylib.DrawRectangleRec(r, RectangleColor);
        Raylib.DrawRectangleLinesEx(r, 1, Color.WHITE);
      }
      else
      {
        Raylib.DrawRectangleLinesEx(r, 1, RectangleColor);
      }
    }
  }
}
