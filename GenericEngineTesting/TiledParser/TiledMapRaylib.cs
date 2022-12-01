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
  [XmlIgnore]
  public readonly Dictionary<TiledLayer, Texture2D> layerTextures = new();

  /// <summary>
  /// A dictionary of generated ready-to-render textures for each layer
  /// </summary>
  private readonly Dictionary<TiledLayer, Image> layerImages = new();

  /// <summary>
  /// A dictionary of preloaded tileset images
  /// </summary>
  private readonly Dictionary<string, Image> tilesetImages = new();

  /// <summary>
  /// A list of tiles - containing basic info on where to find each tile
  /// (source rectangle, image)
  /// </summary>
  private readonly Dictionary<int, RaylibTileInfo> tileInfos = new();

  /// <summary>
  /// Loads, deserializes and prepares a Tiled map, including tilsets data.
  /// Also loads all tileset images & generates layer images
  /// </summary>
  /// <param name="tmxFilename">The .tmx file to load</param>
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
    foreach (TiledTileset tileset in Tilesets)
    {
      Image tilesetImage = Raylib.LoadImage(Path.Combine(TmxFolder, tileset.Image.Source));
      tilesetImages.Add(tileset.Image.Source, tilesetImage);
    }

    // Prepare all RaylibTileInfo for all tilemaps
    foreach (TiledTileset tileset in Tilesets)
    {
      for (int gid = tileset.FirstGid; gid < tileset.FirstGid + tileset.Tilecount; gid++)
      {
        TiledTileset.TileInfo info = tileset.GetTileInfo(gid);
        RaylibTileInfo rInfo = new(info);

        tileInfos.Add(gid, rInfo);
      }
    }

    // Make layer visuals
    UpdateAllLayerVisuals();
  }

  public void BlitTileImage(int gid, ref Image target, int xOffset = 0, int yOffset = 0)
  {
    if (!tileInfos.ContainsKey(gid)) return;

    RaylibTileInfo tileInfo = tileInfos[gid];

    Image sourceImage = tilesetImages[tileInfo.image];
    Rectangle destination = new(xOffset, yOffset, TileWidth, TileHeight);

    Raylib.ImageDraw(ref target, sourceImage, tileInfo.rect, destination, Color.WHITE);
  }

  public void BlitTileImage(TiledLayer layer, int x, int y, ref Image target)
  {
    int gid = layer.GetValue(x, y);
    BlitTileImage(gid, ref target, x * TileWidth, y * TileHeight);
  }

  private void UpdateAllLayerVisuals()
  {
    foreach (TiledLayer layer in Layers)
    {
      UpdateLayerVisual(layer);
    }
  }

  /// <summary>
  /// Update the cached image & texture of specified layer
  /// </summary>
  /// <param name="layer">The source layer</param>
  private void UpdateLayerVisual(TiledLayer layer)
  {
    UpdateLayerImage(layer);
    UpdateLayerTexture(layer);
  }

  private void UpdateLayerTile(TiledLayer layer, int x, int y)
  {
    Image layerImage = layerImages[layer];
    BlitTileImage(layer, x, y, ref layerImage);
    layerImages[layer] = layerImage;
    UpdateLayerTexture(layer);
  }

  private void UpdateLayerTexture(TiledLayer layer)
  {
    Texture2D layerTexture = Raylib.LoadTextureFromImage(layerImages[layer]);
    Raylib.SetTextureFilter(layerTexture, TextureFilter.TEXTURE_FILTER_POINT);
    layerTextures[layer] = layerTexture;
  }

  private void UpdateLayerImage(TiledLayer layer)
  {
    if (layer == null) return;

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
        BlitTileImage(layer, x, y, ref image);
      }
    }

    layerImages[layer] = image;
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

}

// TODO: Drawing
// TODO: Changing single tile to something else
// TODO: Optional chunking
// TODO: Parallax values for layers?