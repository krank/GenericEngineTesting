using System.Globalization;
using System.Xml.Serialization;

namespace TiledParser;

[XmlRoot("tileset")]
public class TiledTileset
{
  [XmlAttribute("name")]
  public string Name { get; set; }

  [XmlAttribute("tilewidth")]
  public int TileWidth { get; set; }

  [XmlAttribute("tileheight")]
  public int TileHeight { get; set; }

  [XmlAttribute("spacing")]
  public int Spacing { get; set; }

  [XmlAttribute("margin")]
  public int Margin { get; set; }

  [XmlAttribute("tilecount")]
  public int Tilecount { get; set; }

  [XmlAttribute("columns")]
  public int Columns { get; set; }

  [XmlElement("image")]
  public TsxImage Image { get; set; }

  [XmlElement("tile")]
  public List<TsxTile> Tiles { get; set; }

  private static readonly XmlSerializer tsxSerializer = new(typeof(TiledTileset));

  private string source = "";

  [XmlAttribute("firstgid")]
  public int FirstGid { get; set; }

  [XmlAttribute("source")]
  public string Source
  {
    get => source;
    set
    {
      source = value;

      // Read tsx file
      using FileStream tsxFile = File.Open(source, FileMode.Open);
      TiledTileset tilesetData = (TiledTileset)tsxSerializer.Deserialize(tsxFile);

      // Transfer data
      Name = tilesetData.Name;
      TileWidth = tilesetData.TileWidth;
      TileHeight = tilesetData.TileHeight;
      Tilecount = tilesetData.Tilecount;
      Columns = tilesetData.Columns;
      Margin = tilesetData.Margin;
      Spacing = tilesetData.Spacing;
      Image = tilesetData.Image;
      Tiles = tilesetData.Tiles;
    }
  }

  public bool ContainsId(int gid)
  {
    return gid >= FirstGid && gid < FirstGid + Tilecount;
  }

  public TileInfo GetTileInfo(int gid)
  {
    gid -= FirstGid;

    TileInfo tileInfo = new();

    int y = gid / Columns;
    int x = gid % Columns;

    tileInfo.image = Image.Source;

    tileInfo.x = Margin + x * (TileWidth + Spacing);
    tileInfo.y = Margin + y * (TileHeight + Spacing);

    tileInfo.width = TileWidth;
    tileInfo.height = TileHeight;

    return tileInfo;
  }

  public string GetTileClass(int gid)
  {
    gid -= FirstGid;
    TsxTile tile = Tiles.Find(t => t.Id == gid);
    
    return tile == null ? "" : tile.Class;
  }

  public T GetTileProperty<T>(int gid, string name)
  {
    gid -= FirstGid;
    TsxTile tile = Tiles.Find(t => t.Id == gid);

    return tile == null ? default : tile.GetProperty<T>(name);
  }

  public struct TileInfo
  {
    public string image;
    public int x;
    public int y;
    public int width;
    public int height;
  }

  public class TsxImage
  {
    [XmlAttribute("source")]
    public string Source { get; set; }
  }

  public class TsxTile
  {
    [XmlAttribute("id")]
    public int Id { get; set; }

    [XmlAttribute("class")]
    public string Class { get; set; }

    [XmlIgnore]
    public List<TsxProperty> Properties { get; set; }

    [XmlElement("properties")]
    public TsxProperties TsxFileProperties {
      get => new() { Properties = Properties };
      set => Properties = value.Properties;
    }

    public T GetProperty<T>(string name)
    {
      if (Properties == null)
        return default;
      
      TsxProperty property = Properties.Find(p => p.Name == name);

      if (property == null)
        return default;

      return (T)Convert.ChangeType(property.Value, typeof(T), CultureInfo.InvariantCulture);
    }
  }

  public class TsxProperties
  {
    [XmlElement("property")]
    public List<TsxProperty> Properties { get; set; }
  }

  public class TsxProperty
  {
    [XmlAttribute("name")]
    public string Name { get; set; }

    [XmlAttribute("value")]
    public string Value { get; set; }
  }
}

