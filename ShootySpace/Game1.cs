using System.Runtime.InteropServices;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using LiteNetLib;
using LiteNetLib.Utils;
using System.Runtime.CompilerServices;

namespace ShootySpace
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        #region Utilities

        KeyboardState kb; // Current state of keyboard.
        KeyboardState pkb; // Previous state of keyboard.
        float xScale;
        float yScale;
        Matrix windowScaler;

        #endregion

        #region Textures

        Texture2D stars1;

        #endregion

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;

            #region Window Settings

            // Make the window borderless, and resize to match the player's computer.
            _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            _graphics.ApplyChanges();

            Window.IsBorderless = true;

            #endregion
        }

        protected override void Initialize()
        {
            xScale = (float)_graphics.PreferredBackBufferWidth / 1920;
            yScale = (float)_graphics.PreferredBackBufferHeight / 1080;
            windowScaler = Matrix.CreateScale(xScale, yScale, 1.0f);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            stars1 = Content.Load<Texture2D>($"SpaceTiles/SS_Stars1");

            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            kb = Keyboard.GetState(); // Check current state of keyboard.

            #region State Independent Controls

            // Allows players to exit whenever they want by pressing Escape.
            if (SingleKeyPress(Keys.Escape)) Exit();

            // Allows players to toggle the borders on the game window with F11.
            if (SingleKeyPress(Keys.F11))
            {
                if (Window.IsBorderless)
                {
                    Window.IsBorderless = false;
                    Window.Position = new Point(50, 50);
                }
                else
                {
                    Window.IsBorderless = true;
                    Window.Position = Point.Zero;
                }
            }

            #endregion



            pkb = kb; // Update previous keyboard state.
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: windowScaler);



            _spriteBatch.End();

            base.Draw(gameTime);
        }

        #region Utility Functions

        /// <summary>
        /// This function checks to see if a key has been pressed one time.
        /// </summary>
        /// <param name="key"> The key we're checking. </param>
        /// <returns> Whether or not it was pressed once. </returns>
        bool SingleKeyPress(Keys key)
        {
            return kb.IsKeyDown(key) && pkb.IsKeyUp(key);
        }

        #endregion
    }
}
