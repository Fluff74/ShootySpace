using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShootySpace
{
    // Joshua Smith
    // 07/26/2025
    //
    // Ah yes, the button class... I wonder what this mythical tool might do.
    // It's a button that is clickable on screen by using a mouse.
    internal class Button
    {
        Texture2D texture;
        Rectangle hitbox;
        Color hoverColor;
        bool hover;
        bool holding;

        /// <summary>
        /// The constructor for a button object. Takes in the texture of the button, and it's hitbox.
        /// </summary>
        /// <param name="texture"> The sprite of the button. </param>
        /// <param name="hitbox"> The hitbox of the button. </param>
        /// <param name="hoverColor"> The hue shift of the button while it's being hovered over. </param>
        public Button(Texture2D texture, Rectangle hitbox, Color hoverColor)
        {
            this.texture = texture;
            this.hitbox = hitbox;
            this.hoverColor = hoverColor;
        }

        /// <summary>
        /// Allows the button to be hovered over, and checks if it has been clicked or not.
        /// </summary>
        /// <param name="ms"> The current state of the user's mouse. </param>
        /// <param name="pms"> The previous state of the user's mouse. </param>
        /// <returns> Whether or not the user clicked the button. </returns>
        public bool Update(MouseState ms, MouseState pms, Point test)
        {
            hover = hitbox.Contains(test);
            holding = ms.LeftButton == ButtonState.Pressed;
            return hover && !holding && pms.LeftButton == ButtonState.Pressed;
        }

        /// <summary>
        /// Draws the button to the screen, and applies a hue change if it's being hovered over.
        /// </summary>
        /// <param name="sb"> The SpriteBatch being used to draw the button. </param>
        public void Draw(SpriteBatch sb)
        {
            if(hover)
            {
                if(holding)
                {
                    sb.Draw(texture, hitbox, Color.DarkSlateGray);
                }
                else
                {
                    sb.Draw(texture, hitbox, hoverColor);
                }
            }
            else
            {
                sb.Draw(texture, hitbox, Color.White);
            }
        }
    }
}
