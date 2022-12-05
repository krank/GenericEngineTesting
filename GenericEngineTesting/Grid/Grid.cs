using System;
using System.Numerics;

namespace GridSystem;

public class Grid
{
  private Block[,] blocks;
  public Vector2 WorldOffset;
  public Vector2 BlockSize { get; set; }

  public int Width
  {
    get => blocks.GetLength(0);
  }

  public int Height
  {
    get => blocks.GetLength(1);
  }

  public Grid(int width, int height, Vector2 worldOffset, Vector2 blockSize)
  {
    WorldOffset = worldOffset;
    BlockSize = blockSize;

    blocks = new Block[width, height];
  }

  public void SetBlock(int x, int y, Block block)
  {
    if (BlockWithinBounds(x, y))
    {
      blocks[x, y] = block;
    }
  }

  public Block GetBlock(int x, int y)
  {
    return BlockWithinBounds(x, y) ? blocks[x, y] : null;
  }

  public Block GetBlock(Vector2 worldCoord)
  {
    (int x, int y) = GetBlockCoords(worldCoord);

    return GetBlock(x, y);
  }

  public Vector2 GetBlockWorldCoord(int x, int y)
  {
    return WorldOffset + new Vector2(x * BlockSize.X, y * BlockSize.Y);
  }

  public (int x, int y) GetBlockCoords(Vector2 worldCoord)
  {
    int x = (int)((worldCoord.X - WorldOffset.X) / BlockSize.X);
    int y = (int)((worldCoord.Y - WorldOffset.Y) / BlockSize.Y);

    Console.WriteLine($"x:{x}, y:{y}");

    return (x, y);
  }

  public Block[] GetNeighbors(int x, int y)
  {
    // TODO: Implement
    return null;
  }

  public bool BlockWithinBounds(int x, int y)
  {
    return x >= 0 && x < Width && y >= 0 && y < Height;
  }

  public (int, int) GetLargestSolidArea(int startX, int startY)
  {
    List<int> columnAreaHeights = new();

    for (int x = startX; x < Width; x++)
    {
      // Console.WriteLine($"--- Checking column {x} ---");

      // För varje rad...
      int y;
      bool spanIsSolid = true;
      for (y = startY; spanIsSolid && y < Height; y++)
      {
        // Check if rowspan from startx to this column, @ this row, is 100% solid
        spanIsSolid = true;
        for (int lX = startX; lX <= x && spanIsSolid; lX++)
        {
          // Check if rowspan is still solid if it includes this block
          Block b = GetBlock(lX, y);
          spanIsSolid = b != null && b.IsSolid && !b.IsDirty;
        }
      }
      y--;
      // Console.WriteLine($" Last fully solid row was y: {y}");

      y -= startY;

      if (y != 0)
      {
        columnAreaHeights.Add(y);
      }
    }

    // Find the biggest area
    int bestAreaIndex = 0;
    int bestArea = 0;
    for (int i = 0; i < columnAreaHeights.Count; i++)
    {
      int area = (i + 1) * columnAreaHeights[i];
      if (area > bestArea)
      {
        bestArea = area;
        bestAreaIndex = i;
      }
    }

    // Console.WriteLine($"Best area index was {bestAreaIndex} ({(bestAreaIndex+1) * columnAreaHeights[bestAreaIndex]})");
    if (columnAreaHeights.Count == 0)
    {
      return (0, 0);
    }

    return (
      bestAreaIndex + 1,
      columnAreaHeights[bestAreaIndex]
      );
  }

  public void MarkDirty(int x, int y)
  {
    if (BlockWithinBounds(x, y))
    {
      blocks[x, y].IsDirty = true;
    }
  }

  public void MarkClean(int x, int y)
  {
    if (BlockWithinBounds(x, y))
    {
      blocks[x, y].IsDirty = false;
    }
  }

  public void MarkCleanAll()
  {
    for (int x = 0; x < Width; x++)
    {
      for (int y = 0; y < Height; y++)
      {
        MarkClean(x, y);
      }
    }
  }

  public void MarkDirtyArea(int x, int y, int width, int height)
  {
    for (int lX = x; lX < x + width; lX++)
    {
      for (int lY = y; lY < y + height; lY++)
      {
        MarkDirty(lX, lY);
      }
    }
  }
}
