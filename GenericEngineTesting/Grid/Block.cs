using System;

namespace GridSystem;

public class Block
{
  public string Class {get; set;} = "";
  public int Cost {get; set;}
  public bool IsSolid { get; set; }

  // Passability – x, y, directions
}
