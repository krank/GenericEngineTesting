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
    return null;
  }

  private bool BlockWithinBounds(int x, int y)
  {
    return x > 0 && x < Width && y > 0 && y < Height;
  }
}
