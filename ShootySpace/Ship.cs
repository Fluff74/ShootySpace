using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShootySpace
{
    // Joshua Smith
    // 07/28/2025
    //
    // The ship class is just the things that players control. They're space ships. They fly and shoot lasers.
    internal class Ship
    {
        private int currentShip; // Which ship from the array of spritesheets is being used.
        private Texture2D[] shipSprites; // Spritesheets for all of the different ships the player can use.
        private Texture2D explosionSheet; // The spritesheet for the explosion that happens when the player dies.

        private Rectangle drawLocation; // Where we're drawing the ship.
        private Rectangle shipSource; // Which ship sprite from the sheet is being used currently.
        private Rectangle explosionSource; // Which explosion sprite from the sheet is being used currently.

        private Vector2 origin; // The center of the shipSource sprite.
        private Vector2 facing; // The direction that the ship is facing.
        private Vector2 velocity; // The velocity of the ship, given a direction and a speed.

        private double timeMoving; // The amount of time spent moving forward. Enables faster mode.
        private double rotation; // The current rotation of the ship.

        /// <summary>
        /// The color of the ship. This will be different when playing in Versus mode.
        /// </summary>
        public Color ShipColor { get; set; } // In Versus, use Red, Cyan, Yellow, and Lime.

        /// <summary>
        /// The constructor for a ship object.
        /// </summary>
        /// <param name="shipSprites"> All of the possible sprites for a ship. There will be unlockable ones. </param>
        /// <param name="explosionSheet"> The spritesheet for when a ship explodes. </param>
        /// <param name="currentShip"> The index of the current ship that the user has selected. </param>
        public Ship(Texture2D[] shipSprites, Texture2D explosionSheet, int currentShip)
        {
            this.shipSprites = shipSprites;
            this.explosionSheet = explosionSheet;
            this.currentShip = currentShip;

            drawLocation = new Rectangle(960, 540, 90, 90);
            shipSource = new Rectangle(0, 0, 30, 30);
            origin = shipSource.Center.ToVector2();

            ShipColor = Color.White;

            DetermineDirection();
        }

        /// <summary>
        /// Allows the user to control the ship.
        /// </summary>
        /// <param name="kb"> The current state of the user's keyboard. </param>
        /// <param name="pkb"> The previous state of the user's keyboard. </param>
        /// <param name="gameTime"> How much time has elapsed in seconds. </param>
        public void Update(KeyboardState kb, KeyboardState pkb, GameTime gameTime)
        {
            DetermineDirection(); //  Determine direction before any movements.

            if(kb.IsKeyDown(Keys.W))
            {
                timeMoving += gameTime.ElapsedGameTime.TotalSeconds;
                if(timeMoving >= 3.5)
                {
                    Move(12);
                }
                else
                {
                    Move(7);
                }
            } //  Move forward based on how long we've been moving for.
            else
            {
                timeMoving = 0;
            } // If not moving forward, reset the timeMoving variable.
            if(kb.IsKeyDown(Keys.A))
            {
                rotation -= 0.1f; // Rotate slightly to the left.
                DetermineDirection();
            } // Rotate left.
            if(kb.IsKeyDown(Keys.D))
            {
                rotation += 0.1f; // Rotate slightly to the right.
                DetermineDirection();
            } // Rotate right.
            if(kb.IsKeyDown(Keys.S))
            {
                Move(-5);
            } // Move slowly backwards.
        }

        /// <summary>
        /// Draws the ship based on how fast it is moving, and at the correct orientation.
        /// </summary>
        /// <param name="sb"> The SpriteBatch we're using to draw the ship. </param>
        public void Draw(SpriteBatch sb)
        {
            // Move shipSource rectangle according to speed.
            if(timeMoving >= 3.5)
            {
                shipSource.X = 60;
            } // Full throttle.
            else if(timeMoving <= 0)
            {
                shipSource.X = 0;
            } // Stationary.
            else
            {
                shipSource.X = 30;
            } // Moving normal speed.

            sb.Draw(shipSprites[currentShip], drawLocation, shipSource, ShipColor, (float)rotation, origin, SpriteEffects.None, 0.0f);
        }

        /// <summary>
        /// Moves the ship at an appropriate speed, and in the right direction.
        /// </summary>
        /// <param name="speed"> The speed at which the ship is moving. </param>
        public void Move(int speed)
        {
            // Calculates the distance from our source's center to where we're facing.
            float distance = Vector2.Distance(facing, shipSource.Center.ToVector2());

            // Calculates the velocity.
            velocity = (facing.ToPoint() - shipSource.Center).ToVector2() / distance * speed;

            // Update our location accordingly.
            drawLocation.X += (int)velocity.X;
            drawLocation.Y += (int)velocity.Y;
        }

        /// <summary>
        /// Crunches the numbers to determine what direction the ship is currently facing.
        /// </summary>
        public void DetermineDirection()
        {
            // 25 is how far in front of the ship's center the facing point is. This is used for lasers.
            // 1.5708 is half of PI.
            facing.X = (float)(shipSource.Center.X + (25 * Math.Cos(rotation - 1.5708)));
            facing.Y = (float)(shipSource.Center.Y + (25 * Math.Sin(rotation - 1.5708)));
        }
    }
}
