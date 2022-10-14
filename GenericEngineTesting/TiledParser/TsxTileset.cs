using System.Xml.Serialization;

namespace TiledParser;

public class TsxTileset : TiledTileset
{
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
}

