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
  }
}