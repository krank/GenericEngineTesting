using System;
using System.Xml.Serialization;
using Raylib_cs;

namespace TiledParser;

[XmlRoot("map")]
public class TiledMapRaylib : TiledMap
{
  /// <summary>
  /// A dictionary of generated images for each layer
  /// </summary>
  private Dictionary<TiledLayer, Image> layerImages = new();

  /// <summary>
  /// A dictionary of preloaded tileset images
  /// </summary>
  private Dictionary<string, Image> tilesetImages = new();

  /// <summary>
  /// A list of tiles - containing basic info on where to find each tile
  /// </summary>
  private List<RaylibTileInfo> tileInfos = new();

  /// <summary>
  /// Loads, deserializes and prepares a Tiled map, including tilsets data.
  /// Also loads all tileset images & generates layer images
  /// </summary>
  /// <param name="tmxFilename"></param>
  /// <returns></returns>
  public static TiledMapRaylib Load(string tmxFilename)
  {
    TiledMapRaylib map = TiledMap.Load<TiledMapRaylib>(tmxFilename);
    map.Prepare();

    return map;
  }

  public void Prepare()
  {
    // Load tileset images
    foreach(TiledTileset tileset in Tilesets)
    {
      Image tilesetImage = Raylib.LoadImage(tileset.Image.Source);
      tilesetImages.Add(tileset.Image.Source, tilesetImage);
    }

    // Prepare all RaylibTileInfo for all tilemaps

    // Make layer images?
  }

  private void MakeLayerImages()
  {
    foreach (TiledLayer layer in Layers)
    {
      layerImages.Add(layer, MakeLayerImage(layer));
    }
  }

  private Image MakeLayerImage(TiledLayer layer)
  {
    // Scaling?

    // Create empty image for layer
    Image image = Raylib.GenImageColor(
      layer.Width * TileWidth,
      layer.Height * TileHeight,
      new Color(0, 0, 0, 0)
      );
      
    for (int y = 0; y < layer.Height; y++)
    {
      for (int x = 0; x < layer.Width; x++)
      {
        // Get value
        // Get RaylibTileInfo
        // Make destRect
        // DrawImage
      }
    }

    return image;
  }

  private struct RaylibTileInfo
  {
    public string image;
    public Rectangle rect;

    public RaylibTileInfo(TiledTileset.TileInfo tileInfo)
    {
      image = tileInfo.image;
      rect = new Rectangle(tileInfo.x, tileInfo.y, tileInfo.width, tileInfo.height);
    }
  }

  /*
    DrawLayer(offsetX, offsetY)
      framtid: använd chunking automatiskt?


  */


  // private struct RaylibLayer
  // {
  //   public string image;
  //   public List<RaylibTileInfo> tileInfos = new();
  // }

  // MakeMapImage(layer: int)
  // GetMapTexture
  // Chunking
}

// TODO: Optional caching of layer images
// TODO: Integer-based scaling
// TODO: Changing single tile to something else
// TODO: Optional chunking
// TODO: Parallax values for layers?