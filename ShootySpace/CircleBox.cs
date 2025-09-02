using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShootySpace
{
    // Joshua Smith
    // 09/02/2025
    //
    // This is the code for a circular hitbox, used for, well, hitboxes that aren't rectangles.
    internal class CircleBox
    {
        public int Radius;

        private Vector2 center;

        // DELETE WHEN POLISHING FINISHED PRODUCT
        private Texture2D testure;
        private Rectangle drawLocation;
        private Color standard;
        private Color collision = Color.Aqua;
        // DELETE WHEN POLISHING FINISHED PRODUCT

        /// <summary>
        /// The constructor for a circular hitbox.
        /// </summary>
        /// <param name="radius"> The radius of a circular hitbox. </param>
        /// <param name="center"> The center of the circular hitbox. </param>
        public CircleBox(int radius, Vector2 center, Texture2D testure, Color standard)
        {
            Radius = radius;
            this.center = center;

            // DELETE WHEN POLISHING FINISHED PRODUCT
            this.testure = testure;
            this.standard = standard;
            drawLocation = new Rectangle((int)center.X - radius, (int)center.Y - radius, radius * 2, radius * 2);
            // DELETE WHEN POLISHING FINISHED PRODUCT
        }

        /// <summary>
        /// Checks to see if the CircleBox is colliding with another CircleBox.
        /// </summary>
        /// <param name="other"> The CircleBox we're checking collisions against. </param>
        /// <returns> Whether or not there is a collision. </returns>
        public bool CheckCollision(CircleBox other)
        {
            return Math.Sqrt(Math.Pow(other.center.X - center.X, 2) + Math.Pow(other.center.Y - center.Y, 2)) < Radius + other.Radius;
        }

        /// <summary>
        /// Updates the center of the CircleBox.
        /// </summary>
        /// <param name="center"> The new center of the CircleBox. </param>
        public void Update(Vector2 center)
        {
            this.center = center;

            // DELETE WHEN POLISHING FINISHED PRODUCT
            drawLocation.X = (int)this.center.X - Radius;
            drawLocation.Y = (int)this.center.Y - Radius;
            // DELETE WHEN POLISHING FINISHED PRODUCT
        }

        // DELETE WHEN POLISHING FINISHED PRODUCT
        public void Draw(SpriteBatch sb, bool colliding)
        {
            if (colliding)
            {
                sb.Draw(testure, drawLocation, collision);
            }
            else
            {
                sb.Draw(testure, drawLocation, standard);
            }
        }
    }
}
