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
        Rectangle currentTile; // Where the current tile is being drawn.
        int width; // The width of the tiles array.
        int height; // The height of the tiles array.

        /// <summary>
        /// The constructor for a Backdrop object. It contains all of the information for the background of the game.
        /// </summary>
        /// <param name="width"> How wide the inner 2D array is. </param>
        /// <param name="height"> How tall the inner 2D array is. </param>
        /// <param name="sprites"> All of the sprites that can appear in the backdrop. </param>
        public Backdrop(int width, int height, params Texture2D[] sprites)
        {
            this.width = width;
            this.height = height;
            tiles = new int[width, height];
            this.sprites = sprites;
            currentTile = new Rectangle(0, 0, 120, 120);

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

        /// <summary>
        /// Draws the backdrop of the game to the screen.
        /// </summary>
        /// <param name="sb"> The SpriteBatch being used to draw the backdrop. </param>
        public void Draw(SpriteBatch sb)
        {
            // Loop through the inner 2D array.
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    // Move the drawing rectangle accordingly.
                    currentTile.X = i * 120;
                    currentTile.Y = j * 120;

                    // Draw the tile at the location in the array, multiplied by the necessary amount of pixels.
                    sb.Draw(sprites[tiles[i, j]], currentTile, Color.White);
                }
            }
        }
    }
}
