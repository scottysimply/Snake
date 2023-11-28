using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Snake.Components;
using Snake.Images;

namespace Snake
{
    public class Game1 : Game
    {
        public Rectangle Dimensions { get; set; }
        public Grid GameGrid { get; set; }






        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public InputHandler GameInput { get; set; }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Dimensions = Window.ClientBounds;

            // Load sprites
            TextureList.TBlankCell = Content.Load<Texture2D>("Images/BlankCell.png");
            TextureList.TApple = Content.Load<Texture2D>("Images/Apple.png");
            TextureList.TSnake = Content.Load<Texture2D>("Images/Snake.png");
        }

        protected override void Update(GameTime gameTime)
        {
            // Read keyboard input:
            GameInput.QueryInput();

            // Add killswitch activated via escape:
            if (GameInput.IsKeyHeld(Keys.Escape))
            {
                Exit();
            }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}