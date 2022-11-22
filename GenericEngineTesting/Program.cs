using System.Numerics;
using System.Xml.Serialization;
using Raylib_cs;

using GridSystem;
using TiledParser;

Raylib.InitWindow(800, 600, "Generic 2d engine");

string tiledMapFile = @"TiledData\map.tmx";
string baseTiledDir = Path.GetDirectoryName(tiledMapFile);

TiledMapRaylib map = TiledMapRaylib.Load(tiledMapFile);


// TiledMap map = TiledMap.Load<TiledMapRaylib>(tiledMapFile);
// TiledMapRaylib map = TiledMap.Load<TiledMapRaylib>(tiledMapFile);

Console.WriteLine("hej");



// TiledTileset.TileInfo info = map.Tilesets[0].GetTileInfo(2);

// string tilesetImagePath = Path.Combine(baseTiledDir, info.image);

// Rectangle rect = new Rectangle(
//   info.x,
//   info.y,
//   info.width,
//   info.height
// );

// Rectangle destRect = new Rectangle(0, 0, 16, 16);

// // Texture2D tileImage = new Texture2D();

// Image tilesetImage = new Image();

// if (File.Exists(tilesetImagePath))
// {
//   tilesetImage = Raylib.LoadImage(tilesetImagePath);
// }

// Image tileImage = Raylib.GenImageColor(16, 16, Color.WHITE);

// Raylib.ImageDraw(ref tileImage, tilesetImage, rect, destRect, Color.WHITE);

// Texture2D tx = Raylib.LoadTextureFromImage(tileImage);

Raylib.SetTargetFPS(60);


while (!Raylib.WindowShouldClose())
{
  Raylib.BeginDrawing();

  Raylib.ClearBackground(Color.WHITE);

  // Raylib.DrawTexture(tx, 0, 0, Color.WHITE);

  Raylib.EndDrawing();
}



// int blockSize = 32;

// Grid grid = new Grid(Raylib.GetScreenWidth() / blockSize,
//                      Raylib.GetScreenHeight() / blockSize,
//                      Vector2.Zero, Vector2.One * blockSize);

// grid.SetBlock(4, 5, new Block());


// GridRenderer renderer = new RaylibGridRenderer(grid, Color.GREEN);

// while (!Raylib.WindowShouldClose())
// {
//   if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT))
//   {
//     (int x, int y) = grid.GetBlockCoords(Raylib.GetMousePosition());
//     Vector2 wPos = grid.GetBlockWorldCoord(x, y);
//     Console.WriteLine(wPos);
//   }

//   Raylib.BeginDrawing();

//   Raylib.ClearBackground(Color.WHITE);

//   renderer.RenderRectangles();

//   Raylib.EndDrawing();
// }