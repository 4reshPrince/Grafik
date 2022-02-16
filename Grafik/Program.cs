using System;
using System.Collections.Generic;
using System.Numerics;
using Raylib_cs;

Raylib.InitWindow(800, 600, "2D-topdown game");
Raylib.SetTargetFPS(60);

float speed = 3f;

Texture2D playerImage = Raylib.LoadTexture("george.png");
Rectangle playerRect = new Rectangle(100, 70, playerImage.width, playerImage.height);

Texture2D backImage = Raylib.LoadTexture("map.png");

Texture2D back1Image = Raylib.LoadTexture("map1.png");

Rectangle invRect = new Rectangle(300, 320, 120, 60);

Rectangle wallRect = new Rectangle(248, 0, 5, 240);

List<Rectangle> wallRects = new List<Rectangle>();

// Height väggar

wallRects.Add(new Rectangle(248, 0, 5, 240));
wallRects.Add(new Rectangle(364, 0, 7, 240));
wallRects.Add(new Rectangle(477, 0, 7, 240));
wallRects.Add(new Rectangle(637, 0, 7, 240));
wallRects.Add(new Rectangle(111, 181, 5, 180));
wallRects.Add(new Rectangle(157, 360, 4, 44));
wallRects.Add(new Rectangle(157, 467, 4, 133));
wallRects.Add(new Rectangle(249, 360, 4, 600));
wallRects.Add(new Rectangle(363, 360, 4, 600));
wallRects.Add(new Rectangle(477, 360, 7, 600));
wallRects.Add(new Rectangle(592, 360, 7, 600));
wallRects.Add(new Rectangle(705, 360, 7, 600));

// Width väggar

wallRects.Add(new Rectangle(248, 238, 35, 5));
wallRects.Add(new Rectangle(311, 238, 87, 5));
wallRects.Add(new Rectangle(424, 238, 88, 5));
wallRects.Add(new Rectangle(539, 238, 202, 5));
wallRects.Add(new Rectangle(768, 238, 32, 5));
wallRects.Add(new Rectangle(0, 360, 160, 4));
wallRects.Add(new Rectangle(249, 358, 35, 4));
wallRects.Add(new Rectangle(310, 358, 88, 4));
wallRects.Add(new Rectangle(425, 358, 87, 4));
wallRects.Add(new Rectangle(539, 358, 88, 4));
wallRects.Add(new Rectangle(653, 358, 88, 4));
wallRects.Add(new Rectangle(767, 358, 33, 4));


List<Rectangle> wall1Rects = new List<Rectangle>();

wall1Rects.Add(new Rectangle(90, 107, 710, 7));


wall1Rects.Add(new Rectangle(100, 0, 5, 100));



Rectangle doorRect = new Rectangle(0, 355, 110, 5);

Rectangle door2Rect = new Rectangle(338, 114, 80, 4);

Texture2D key1Image = Raylib.LoadTexture("Key1.png");
Rectangle key1Rect = new Rectangle(300, 320, 0, 0);

int points = 0;

Texture2D pointImage = Raylib.LoadTexture("Key.png");
Rectangle pointRect = new Rectangle(268, 36, 16, 16);
bool pointTaken = false;

bool undoX = false;
bool undoY = false;
Vector2 movement = new Vector2();

string level = "start";



while (!Raylib.WindowShouldClose())
{
    undoX = false;
    undoY = false;

    if (level == "start")
    {

        Raylib.DrawTexture(backImage, 0, 0, Color.WHITE);

        movement = ReadMovement(speed);

        playerRect.x += movement.X;

        // Gör så att det finns väggar med collision i X-axeln

      for (int i = 0; i < wallRects.Count; i++)
      {
        if (Raylib.CheckCollisionRecs(playerRect, wallRects[i]))
        {
          playerRect.x -= movement.X;
        }
      }

        playerRect.y += movement.Y;

        // Gör så att det finns väggar med collision i Y-axeln

        for (int i = 0; i < wallRects.Count; i++)
        {
          if (Raylib.CheckCollisionRecs(playerRect, wallRects[i]))
          {
            playerRect.y -= movement.Y;
          }
        }

        // Gör så att man inte kan gå utanför bilden i både X och Y axeln

        if (playerRect.x < 0 || playerRect.x + playerRect.width > Raylib.GetScreenWidth())
        {
            undoX = true;
        }
        if (playerRect.y < 0 || playerRect.y + playerRect.height > Raylib.GetScreenHeight())
        {
            undoY = true;
        }
    }

    // Gör så att playerRect och playerImage kan flytta sig i både X och Y axeln

    if (level == "end")
    {
        Raylib.DrawTexture(back1Image, 0, 0, Color.WHITE);

        movement = ReadMovement(speed);


        playerRect.x += movement.X;

        for (int i = 0; i < wall1Rects.Count; i++)
      {
        if (Raylib.CheckCollisionRecs(playerRect, wall1Rects[i]))
        {
          playerRect.x -= movement.X;
        }
      }

        
        playerRect.y += movement.Y;

        for (int i = 0; i < wall1Rects.Count; i++)
        {
          if (Raylib.CheckCollisionRecs(playerRect, wall1Rects[i]))
          {
            playerRect.y -= movement.Y;
          }
        }


        if (playerRect.x < 0 || playerRect.x + playerRect.width > Raylib.GetScreenWidth())
        {
            undoX = true;
        }
        if (playerRect.y < 0 || playerRect.y + playerRect.height > Raylib.GetScreenHeight())
        {
            undoY = true;
        }
    }


    if (level == "start")
    {
        if (Raylib.CheckCollisionRecs(playerRect, doorRect) && points == 1)
        {
            level = "end";
            playerRect.x = 370;
            playerRect.y = 120;
        }
        if (Raylib.CheckCollisionRecs(playerRect, pointRect) && pointTaken == false)
        {
            points++;
            pointTaken = true;
        }
    }

    if (level == "end")
    {
        if (Raylib.CheckCollisionRecs(playerRect, door2Rect))
        {
            level = "start";
            playerRect.x = 45;
            playerRect.y = 330;
        }
    }


    if (undoX == true) playerRect.x -= movement.X;
    if (undoY == true) playerRect.y -= movement.Y;

    Raylib.BeginDrawing();

    if (Raylib.IsKeyDown(KeyboardKey.KEY_I))
    {
      Raylib.DrawRectangleRec(invRect, Color.WHITE);
      if (points == 1)
        {
          Raylib.DrawTexture(key1Image, (int)key1Rect.x, (int)key1Rect.y, Color.WHITE);
        }
    }

    if (level == "start")
    {
        for (int i = 0; i < wallRects.Count; i++)
        {
            // Raylib.DrawRectangleRec(wallRects[i], Color.WHITE);

        }

        if (points == 1)
        {
            Raylib.DrawRectangleRec(doorRect, Color.BLACK);
        }
        if (pointTaken == false)
        {
            Raylib.DrawTexture(pointImage, (int)pointRect.x, (int)pointRect.y, Color.WHITE);
            // Raylib.DrawRectangleRec(pointRect, Color.WHITE);
        }
    }

    if (level == "end")
    {
        for (int i = 0; i < wall1Rects.Count; i++)
        {
            Raylib.DrawRectangleRec(wall1Rects[i], Color.WHITE);

        }

        Raylib.DrawRectangleRec(door2Rect, Color.BLACK);
    }

    if (level == "start" || level == "end")
    {
        Raylib.DrawTexture(playerImage, (int)playerRect.x, (int)playerRect.y, Color.WHITE);
        // Raylib.DrawText(points.ToString(), 10, 10, 40, Color.BLACK);
    }

    Raylib.EndDrawing();
}

static Vector2 ReadMovement(float speed)
{
    Vector2 movement = new Vector2();
    if (Raylib.IsKeyDown(KeyboardKey.KEY_W)) movement.Y = -speed;
    if (Raylib.IsKeyDown(KeyboardKey.KEY_S)) movement.Y = speed;
    if (Raylib.IsKeyDown(KeyboardKey.KEY_A)) movement.X = -speed;
    if (Raylib.IsKeyDown(KeyboardKey.KEY_D)) movement.X = speed;

    return movement;
}