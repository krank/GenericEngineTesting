using System.Numerics;
using Raylib_cs;

using GridSystem;
using TiledParser;


using System.Xml.Serialization;

TmxMap map = new(@"TiledData\map.tmx");



Console.ReadLine();

// Raylib.InitWindow(800, 600, "Generic 2d engine");

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