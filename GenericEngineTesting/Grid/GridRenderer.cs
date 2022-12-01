
namespace GridSystem;

public abstract class GridRenderer
{
  protected Grid grid;

  public GridRenderer(Grid grid)
  {
    this.grid = grid;
  }

  public void Render()
  {
    EachCoord(RenderBlockRectangle);
  }

  public abstract void RenderBlockRectangle(int x, int y);

  public void EachSquare(Action<Block> action)
  {
    for (int y = 0; y < grid.Height; y++)
    {
      for (int x = 0; x < grid.Width; x++)
      {
        action(grid.GetBlock(x, y));
      }
    }
  }

  public void EachCoord(Action<int, int> action)
  {
    for (int y = 0; y < grid.Height; y++)
    {
      for (int x = 0; x < grid.Width; x++)
      {
        action(x, y);
      }
    }
  }
}
