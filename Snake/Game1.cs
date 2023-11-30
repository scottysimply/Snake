using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Snake.Components;
using System;

namespace Snake
{
    public class Game1 : Game
    {
        public Rectangle Dimensions { get; set; }
        private Grid _gameGrid;




        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private InputHandler _inputHandler;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            _inputHandler = new();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.ApplyChanges();


            _spriteBatch = new SpriteBatch(GraphicsDevice);

            

            Dimensions = Window.ClientBounds;
            _gameGrid = new(20, 15, Dimensions.Size);


            // Load sprites
            TextureList.TBlankCell = Content.Load<Texture2D>("BlankCell");
            TextureList.TApple = Content.Load<Texture2D>("Apple");
            TextureList.TSnake = Content.Load<Texture2D>("Snake");
        }

        protected override void Update(GameTime gameTime)
        {
            // Read keyboard input:
            _inputHandler.QueryInput();

            // Add killswitch activated via escape:
            if (_inputHandler.IsKeyHeld(Keys.Escape))
            {
                Exit();
            }
            // GAME LOGIC GOES HERE!!!

            // Order of operations for game logic:
            // Get the currently pressed key.
            // Compare the pressed key to the snake's direction
            //      If applicable, change the snake head's direction.
            // Move the snake head forward.
            //      Detect if snake overwrote an apple.
            //          Extend the snake's length if the snake did eat an apple.
            //      Detect if the snake has crashed.
            //          End game if the snake has crashed.
            //      Extend the snake's length via pathfinding (ask Scott about this)
            //      In order to perform this, all non-empty cells will have to be investigated. This will be done in tandem with creating an apple.
            // If an apple no longer exists, generate a new one.
            // Wait a given amount of time
            // TODO: Make the given amount of time be variable.
            //



            // Create a new apple.
            AppleGenerator generator = new(ref _gameGrid);

            // END GAME LOGIC!


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();

            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _gameGrid.DrawGrid(_spriteBatch);


            _spriteBatch.End();
        }
    }
}