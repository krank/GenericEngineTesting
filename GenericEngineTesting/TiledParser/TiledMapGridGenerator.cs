using GridSystem;
using System.Numerics;

namespace TiledParser;

public class TiledMapGridGenerator
{
  public static Grid Generate(TiledMap tiledMap, TiledMap.TiledLayer tiledLayer, float scaling)
  {
    Vector2 blockSize = new Vector2(tiledMap.TileWidth, tiledMap.TileHeight) * scaling;

    Grid grid = new (tiledLayer.Width, tiledLayer.Height, Vector2.Zero, blockSize);

    // Prepare all the blocks
    for (int y = 0; y < grid.Height; y++)
    {
      for (int x = 0; x < grid.Width; x++)
      {
        Block b = new()
        {
          Class = tiledMap.GetBlockClass(tiledLayer, x, y),
          Cost = 1,
          IsSolid = tiledMap.GetTileProperty<bool>(tiledLayer, x, y, "solid")
        };
        grid.SetBlock(x, y, b);
      }
    }

    return grid;
  }
}
