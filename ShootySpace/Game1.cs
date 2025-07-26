using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using LiteNetLib;
using LiteNetLib.Utils;
using System.Runtime.CompilerServices;
using System.Threading;
using System;

namespace ShootySpace
{
    // Joshua Smith
    // 07/26/2025
    //
    // I decided I was going to make a game called Shooty Space for my portfolio, but also to play with friends. This project tackles quite a few things
    // I'm not super comfortable implementing, but I think I can figure it out. The biggest thing is multiplayer...
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        #region Utilities

        KeyboardState kb; // Current state of keyboard.
        KeyboardState pkb; // Previous state of keyboard.
        MouseState ms; // Current state of mouse.
        MouseState pms; // Previous state of mouse.
        Point mScale; // Scales the mouse based on the resolution to allow users with smaller resolutions to play the game.
        float xScale; // The X scale of the screen for resizing. (1920)
        float yScale; // The Y scale of the screen for resizing. (1080)
        Matrix windowScaler; // The matrix used to resize the screen.

        #endregion

        #region Textures

        Texture2D tempAsset;

        #endregion

        #region Game Objects

        Backdrop soloBackdrop; // This is the backdrop of the game while in single player, and also the backdrop of the main menu screen.

        #endregion

        #region User Interface

        Button solo;
        Button versus;
        Button settings;
        Button quit;

        #endregion

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

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
            soloBackdrop = new Backdrop(16, 9, Content.Load<Texture2D>($"SpaceTiles/SS_Stars1"), Content.Load<Texture2D>($"SpaceTiles/SS_Stars2"), Content.Load<Texture2D>($"SpaceTiles/SS_Stars3"), Content.Load<Texture2D>($"SpaceTiles/SS_Stars4"), Content.Load<Texture2D>($"SpaceTiles/SS_Stars5"), Content.Load<Texture2D>($"SpaceTiles/SS_Stars6"), Content.Load<Texture2D>($"SpaceTiles/SS_Stars7"), Content.Load<Texture2D>($"SpaceTiles/SS_Stars8"), Content.Load<Texture2D>($"SpaceTiles/SS_Stars9"), Content.Load<Texture2D>($"SpaceTiles/SS_Stars10"), Content.Load<Texture2D>($"SpaceTiles/SS_Stars11"), Content.Load<Texture2D>($"SpaceTiles/SS_Stars12"), Content.Load<Texture2D>($"SpaceTiles/SS_Stars13"), Content.Load<Texture2D>($"SpaceTiles/SS_Stars14"), Content.Load<Texture2D>($"SpaceTiles/SS_Stars15"));
            tempAsset = Content.Load<Texture2D>($"tempAsset");

            #region User Interface

            solo = new Button(Content.Load<Texture2D>($"MainMenuButtons/SoloButton"), new Rectangle(1000, 800, 360, 120), Color.Yellow);

            #endregion

            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            kb = Keyboard.GetState(); // Check current state of keyboard.
            ms = Mouse.GetState(); // Check current state of mouse.
            mScale = new Point((int)(ms.X * (1920.0 / _graphics.PreferredBackBufferWidth)), (int)(ms.Y * (1080.0 / _graphics.PreferredBackBufferHeight))); // Scale the mouse position based on the resolution.

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

            solo.Update(ms, pms, mScale);

            pkb = kb; // Update previous keyboard state.
            pms = ms; // Update previous mouse state.
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: windowScaler);

            soloBackdrop.Draw(_spriteBatch);
            solo.Draw(_spriteBatch);

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
