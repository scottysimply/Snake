using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace TrickOrTreat
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private DialogueBox _trickOrTreatDialogue;
        public Point WindowSize { get; set; }
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            oldState = new();

            Random rand = new();
            haunting = rand.Next(0, 2) == 0;

            WindowSize = new(1920, 1080);
            door = new(new Point(WindowSize.X / 2, WindowSize.Y / 2), 10f);

            _graphics.PreferredBackBufferHeight = WindowSize.Y;
            _graphics.PreferredBackBufferWidth = WindowSize.X;
            _graphics.ApplyChanges();

            // Set size of the game

            base.Initialize();
        }
        bool haunting = false;
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            // Load door!
            DoorTexture = Content.Load<Texture2D>("Door");
            EyeTexture = Content.Load<Texture2D>("Eyes");
            EmptyTexture = Content.Load<Texture2D>("Empty");
            Arial = Content.Load<SpriteFont>("Arial");
            _trickOrTreatDialogue = new(haunting ? "You received a trick..." : "You received a treat!", WindowSize);
        }
        Door door;
        public static Texture2D DoorTexture { get; set; }
        public static Texture2D EyeTexture { get; set; }
        public static Texture2D EmptyTexture { get; set; }
        public static SpriteFont Arial { get; set; }
        MouseState oldState;
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            MouseState currentState = Mouse.GetState();
            if (currentState.LeftButton == ButtonState.Pressed && currentState.LeftButton != oldState.LeftButton)
            {
                if (door.Bounds.Contains(currentState.Position))
                {
                    door.OnClick();
                }
            }


            oldState = currentState;
            base.Update(gameTime);
        }
        Color backgroundColor = new(20, 30, 60);
        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp, sortMode: SpriteSortMode.Immediate, blendState: BlendState.AlphaBlend);
            GraphicsDevice.Clear(backgroundColor);
            if (haunting)
            {
                _spriteBatch.Draw(EyeTexture, new Rectangle(door.Bounds.Center - new Point(60, 50), new Point(100, 20)), null, Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0);
            }
            door.Draw(_spriteBatch);
            // TODO: Add your drawing code here

            if (door.openingTimer > 75)
            {
                _trickOrTreatDialogue.Draw(_spriteBatch, door.openingTimer);
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}