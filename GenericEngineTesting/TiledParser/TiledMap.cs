using System.Xml.Serialization;

namespace TiledParser;

[XmlRoot("map")]
public class TiledMap
{
  [XmlAttribute("width")]
  public int Width { get; set; }

  [XmlAttribute("height")]
  public int Height { get; set; }

  [XmlAttribute("tileheight")]
  public int TileHeight { get; set; }

  [XmlAttribute("tilewidth")]
  public int TileWidth { get; set; }

  [XmlElement("tileset")]
  public List<TiledTileset> Tilesets { get; set; }

  [XmlElement("layer")]
  public List<TiledLayer> Layers { get; set; }

  /// <summary>
  /// The total amount of single tiles available in the map
  /// </summary>
  [XmlIgnore]
  public int TilesCount { get; private set; }

  /// <summary>
  /// The path to the tmx file
  /// </summary>
  [XmlIgnore]
  public string TmxFilename { get; private set; }

  [XmlIgnore]
  public string TmxFolder => Path.GetDirectoryName(TmxFilename);

  /// <summary>
  /// Loads, deserializes and prepares a Tiled map, including tilsets data.
  /// </summary>
  /// <typeparam name="T">The type of Tiledmap to load</typeparam>
  /// <param name="tmxFilename">Yje</param>
  /// <returns></returns>
  public static T Load<T>(string tmxFilename) where T : TiledMap
  {
    XmlSerializer tmxSerializer = new(typeof(T));

    using FileStream tmxFile = File.Open(tmxFilename, FileMode.Open);

    string workingDirectory = Directory.GetCurrentDirectory();
    string tmxFolder = Path.GetDirectoryName(tmxFile.Name);

    // Temporarily sets current dir to Tmx file's. Needed for auto-loading of
    //  tsx tileset files
    Directory.SetCurrentDirectory(tmxFolder);
    T map = (T)tmxSerializer.Deserialize(tmxFile);

    // Restore current dir
    Directory.SetCurrentDirectory(workingDirectory);

    // Count map's total number of tiles.
    map.TilesCount = map.Tilesets.Sum(tileset => tileset.Tilecount);

    // Save the filename of the deserialized file
    map.TmxFilename = tmxFile.Name;

    return map;
  }

  public TiledTileset GetTilesetOf(int tileId)
  {
    foreach (TiledTileset tileset in Tilesets)
    {
      if (tileset.ContainsId(tileId))
      {
        return tileset;
      }
    }

    return Tilesets[0];
  }

  public TiledLayer GetLayer(int layerId)
  {
    return Layers.Find(l => l.Id == layerId);
  }

  public TiledLayer GetLayer(string layerName)
  {
    return Layers.Find(l => l.Name == layerName);
  }

  public string GetBlockClass(TiledLayer layer, int x, int y)
  {
    int tileId = layer.GetValue(x, y);
    return GetTilesetOf(tileId).GetTileClass(tileId);
  }

  public T GetTileProperty<T>(TiledLayer layer, int x, int y, string propertyName)
  {
    int tileId = layer.GetValue(x, y);
    return GetTilesetOf(tileId).GetTileProperty<T>(tileId, propertyName);
  }

  public virtual TiledTileset.TileInfo GetTileInfo(TiledLayer layer, int x, int y)
  {
    int tileId = layer.GetValue(x, y);
    TiledTileset.TileInfo tileInfo = GetTilesetOf(tileId).GetTileInfo(tileId);

    return tileInfo;
  }

  // Make/contain Grid instance?

  public class TiledLayer
  {
    [XmlAttribute("id")]
    public int Id { get; set; }

    [XmlAttribute("name")]
    public string Name { get; set; }

    /// <summary>
    /// Width in blocks
    /// </summary>
    [XmlAttribute("width")]
    public int Width { get; set; }

    /// <summary>
    /// Height in blocks
    /// </summary>
    [XmlAttribute("height")]
    public int Height { get; set; }

    private string csvData;

    [XmlElement("data")]
    public string CsvData
    {
      get
      {
        return csvData;
      }
      set
      {
        csvData = value;

        string[] rows = value.Trim().Split("\n");
        string[] row = rows[0].Split(",");
        int width = row.Count(a => a.Length > 0);
        int height = rows.Length;

        Data = new int[width, height];

        for (int y = 0; y < height; y++)
        {
          row = rows[y].Split(",");

          for (int x = 0; x < width; x++)
          {
            if (int.TryParse(row[x], out int tileId))
            {
              Data[x, y] = tileId;
            }
          }
        }
      }
    }

    [XmlIgnore]
    public int[,] Data { get; set; }

    public int GetValue(int x, int y)
    {
      if (x < 0 || x >= Data.GetLength(0) || y < 0 || y >= Data.GetLength(1))
      {
        return 0;
      }

      return Data[x, y];
    }
  }
}
