using System.Xml.Serialization;


namespace TiledParser;

public class TmxMap : TiledMap
{
  private static readonly XmlSerializer tmxSerializer = new(typeof(TiledMap));

  public TmxMap(string tmxFilename)
  {
    using FileStream tmxFile = File.Open(tmxFilename, FileMode.Open);

    string workingDirectory = Directory.GetCurrentDirectory();
    string tmxFolder = Path.GetDirectoryName(tmxFile.Name);

    Directory.SetCurrentDirectory(tmxFolder);
    TiledMap map = (TiledMap)tmxSerializer.Deserialize(tmxFile);
    Directory.SetCurrentDirectory(workingDirectory);

    Height = map.Height;
    Width = map.Width;
    TileHeight = map.TileHeight;
    TileWidth = map.TileWidth;
    Tileset = map.Tileset;
  }



  // TODO: Layers
}
