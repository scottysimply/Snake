using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ConnectFour
{
    public enum Team
    {
        Red,
        Yellow,
    }
    public class ConnectFourGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public ConnectFourGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            // Initialize bounds.
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.PreferredBackBufferWidth = 1280;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }
        protected Texture2D EmptySpace;
        protected Texture2D PlayerToken;
        protected Texture2D RedToken;
        protected Texture2D YellowToken;
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
            EmptySpace = Content.Load<Texture2D>("Content/EmptySpace");
            PlayerToken = Content.Load<Texture2D>("Content/PlayerToken");
            RedToken = Content.Load<Texture2D>("Content/RedPiece");
            YellowToken = Content.Load<Texture2D>("Content/YellowPiece");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here




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