using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShootySpace
{
    // Joshua Smith
    // 07/26/2025
    //
    // The backdrop class will handle generating, drawing, and animating the background for the game.
    // Tiles are drawn at 40x40, but rendered at 120x120.
    internal class Backdrop
    {
        Random rng = new Random(); // For randomizing tile selection.
        int[,] tiles; // The 2D array of tiles.
        Texture2D[] sprites; // The sprites used for the tiles.
        int width; // The width of the tiles array.
        int height; // The height of the tiles array.

        public Backdrop(int width, int height, params Texture2D[] sprites)
        {
            this.width = width;
            this.height = height;
            tiles = new int[width, height];
            this.sprites = sprites;

            Generate();
        }

        public void Generate()
        {
            for(int i = 0; i < width; i++)
            {
                for(int j = 0; j < height; j++)
                {
                    tiles[i, j] = rng.Next(0, 15);
                }
            }
        }

        public void Draw(SpriteBatch sb)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    sb.Draw(sprites[tiles[i, j]], new Rectangle(i * 120, j * 120, 120, 120), Color.White);
                }
            }
        }
    }
}
