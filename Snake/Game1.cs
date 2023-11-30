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

        private Point _playerPosition;
        private LogicIDs _headDirection;


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
            // Set Dimensions of the window and game
            _graphics.PreferredBackBufferHeight = 768;
            _graphics.PreferredBackBufferWidth = 1024;
            _graphics.ApplyChanges();
            Dimensions = Window.ClientBounds;
            _gameGrid = new(20, 15, Dimensions.Size);

            // Create the initial snake.
            _playerPosition = _gameGrid.SpawnSnake();

            // Initialize the SpriteBatch object (does the rendering)
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load sprites
            TextureList.TBlankCell = Content.Load<Texture2D>("BlankCell");
            TextureList.TApple = Content.Load<Texture2D>("Apple");
            TextureList.TSnake = Content.Load<Texture2D>("Snake");
        }

        protected override void Update(GameTime gameTime)
        {
            // Order of operations for game logic:
            // Completed: Get the current state of the keyboard.
            _inputHandler.QueryInput();

            // Add killswitch activated via escape:
            if (_inputHandler.IsKeyHeld(Keys.Escape))
            {
                Exit();
            }
            // Get the direction of the snake
            _headDirection = _gameGrid.GetIDAtPosition(_playerPosition);

            // IMPORTANT NOTE:
            // I know that object oriented programming is great, and shrimplifies the creation of games. However, it leads to a lot of calls to code that may not be fully understood.
            // For the sake of this project, all the update logic should be mostly barebones in this update loop. Make sure it's commented to make clear what it does.

            // Check if WASD or Arrow Keys were pressed.
            //      If applicable, change the snake head's direction.

            // If up is pressed
            if (_inputHandler.IsKeyHeld(Keys.W) || _inputHandler.IsKeyPressed(Keys.Up))
            {
                // If snake is facing left or right, make the snake face up.
                // To get the direction the snake head is facing, use _headDirection.
                // To change the direction of the snake, call _gameGrid.SetIDAtPosition(_playerPosition, LogicIDs.[id here]).
                if (_headDirection == LogicIDs.SnakeHeadWest || _headDirection == LogicIDs.SnakeHeadEast)
                {
                    _gameGrid.SetIDAtPosition(_playerPosition, LogicIDs.SnakeHeadNorth);
                    _headDirection = LogicIDs.SnakeHeadNorth;
                }
            }
            // If down is pressed
            if (_inputHandler.IsKeyHeld(Keys.S) || _inputHandler.IsKeyPressed(Keys.Down))
            {
                // If snake is facing left or right, make the snake face down.
                if (_headDirection == LogicIDs.SnakeHeadWest || _headDirection == LogicIDs.SnakeHeadEast)
                {
                    _gameGrid.SetIDAtPosition(_playerPosition, LogicIDs.SnakeHeadSouth);
                    _headDirection = LogicIDs.SnakeHeadSouth;
                }
            }
            // If left is pressed
            if (_inputHandler.IsKeyHeld(Keys.A) || _inputHandler.IsKeyPressed(Keys.Left))
            {
                // If snake is facing up or down, make the snake face left.
                if (_headDirection == LogicIDs.SnakeHeadSouth || _headDirection == LogicIDs.SnakeHeadNorth)
                {
                    _gameGrid.SetIDAtPosition(_playerPosition, LogicIDs.SnakeHeadWest);
                    _headDirection = LogicIDs.SnakeHeadWest;
                }
            }
            // If right is pressed
            if (_inputHandler.IsKeyHeld(Keys.D) || _inputHandler.IsKeyPressed(Keys.Right))
            {
                // If snake is facing up or down, make the snake face right.
                if (_headDirection == LogicIDs.SnakeHeadSouth || _headDirection == LogicIDs.SnakeHeadNorth)
                {
                    _gameGrid.SetIDAtPosition(_playerPosition, LogicIDs.SnakeHeadEast);
                    _headDirection = LogicIDs.SnakeHeadEast;
                }
            }
            // Get the position of the cell in front of the snake
            Point facing_position = (_headDirection) switch
            {
                LogicIDs.SnakeHeadNorth => new Point(_playerPosition.X, _playerPosition.Y - 1),
                LogicIDs.SnakeHeadSouth => new Point(_playerPosition.X, _playerPosition.Y + 1),
                LogicIDs.SnakeHeadEast => new Point(_playerPosition.X + 1, _playerPosition.Y),
                LogicIDs.SnakeHeadWest => new Point(_playerPosition.X - 1, _playerPosition.Y),
            };
            // Get the ID of the cell in front of the snake
            LogicIDs facing_ID = _gameGrid.GetIDAtPosition(facing_position);

            // If the snake is going to crash
            if (facing_ID == LogicIDs.SnakeBody)
            {
                // TODO: Kill the snake.
                
            }

            // Move the snake head forward.
            _gameGrid.SetIDAtPosition(facing_position, _headDirection);
            //      Detect if the previous cell ID was an apple.
            //          Extend the snake's length if the snake did eat an apple.
            //      Move the rest of the snake via pathfinding (talk to Scott about this)
            //          In order to perform this, all non-empty cells will have to be investigated. This will be done in tandem with creating an apple.
            // If an apple no longer exists, generate a new one.
            // Wait a given amount of time
            // TODO: Make the given amount of time be variable.
            //
            // END GAME LOGIC!
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