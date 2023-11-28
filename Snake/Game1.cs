using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Snake.Components;

namespace Snake
{
    public class Game1 : Game
    {
        public Rectangle Dimensions { get; set; }
        public Grid GameGrid { get; set; }






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
            GameGrid = new(20, 15, Dimensions.Size);

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


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();

            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            GameGrid.DrawGrid(_spriteBatch);


            _spriteBatch.End();
        }
    }
}