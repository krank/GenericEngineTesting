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
  public List<TsxTileset> Tileset { get; set; }

  private static readonly XmlSerializer tmxSerializer = new(typeof(TiledMap));

  public static TiledMap Load(string tmxFilename)
  {
    using FileStream tmxFile = File.Open(tmxFilename, FileMode.Open);

    string workingDirectory = Directory.GetCurrentDirectory();
    string tmxFolder = Path.GetDirectoryName(tmxFile.Name);

    Directory.SetCurrentDirectory(tmxFolder);
    TiledMap map = (TiledMap)tmxSerializer.Deserialize(tmxFile);
    Directory.SetCurrentDirectory(workingDirectory);

    return map;
  }
}
