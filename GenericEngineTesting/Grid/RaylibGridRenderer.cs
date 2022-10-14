using System.Numerics;
using Raylib_cs;

namespace GridSystem;

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
    Rectangle r = new(
      grid.WorldOffset.X + (grid.BlockSize.X * x),
      grid.WorldOffset.Y + (grid.BlockSize.Y * y),
      grid.BlockSize.X,
      grid.BlockSize.Y
    );

    if (grid.GetBlock(x, y) != null)
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
