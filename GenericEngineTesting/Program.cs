﻿using System.Numerics;
using Raylib_cs;

using GridSystem;
using TiledParser;

Raylib.InitWindow(800, 600, "Generic 2d engine");

string tiledMapFile = @"TiledData\map.tmx";

// Load the map
TiledMapRaylib map = TiledMapRaylib.Load(tiledMapFile);

// Get the texture of the first layer
TiledMap.TiledLayer layer = map.GetLayer(1);
Texture2D tx = map.layerTextures[layer];

// Generate grid from map
Grid grid = TiledMapGridGenerator.Generate(map, layer, 2);

// Create a renderer for the grid
GridRaylib.RaylibGridRenderer gridRenderer = new (grid, new Color(255, 0, 0, 128));

// GridRaylib.GetHitboxRectangles(grid);

grid.GetLargestSolidArea(0, 0);


Raylib.SetTargetFPS(60);

while (!Raylib.WindowShouldClose())
{
  Raylib.BeginDrawing();

  Raylib.ClearBackground(Color.WHITE);

  Raylib.DrawTextureEx(tx, Vector2.One, 0, 2, Color.WHITE);
  gridRenderer.Render();

  Raylib.EndDrawing();
}


// TODO: Generating an efficient Raylib collision grid from a grid
// TODO: Make a Level class, containing a Grid and a TiledMap and maybe other things

// TODO: General collider handler class? Containing "self" list of rectangles 
//          Returning object whose collider was collided with (level, monster etc)

// TODO: Grid collision checking based on proximity
// TODO: Grid collision checking against all collider rectangles
// TODO: Stopping/negating collisions