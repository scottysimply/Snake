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
        private int _snakeLength;


        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private InputHandler _inputHandler;
        private int _stepTimer;

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
            _stepTimer = 0;

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
            _snakeLength = 3;

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

            // Run game logic if there is a valid time step for it.
            if (_stepTimer > 30)
            {
                GameLogic(gameTime);
                _stepTimer = 0;
            }
            _stepTimer++;
        }

        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();

            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _gameGrid.DrawGrid(_spriteBatch);


            _spriteBatch.End();
        }
        private void GameLogic(GameTime gameTime)
        {
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
                _ => _playerPosition,
            };
            // Get the ID of the cell in front of the snake
            LogicIDs facing_ID = _gameGrid.GetIDAtPosition(facing_position);

            // If the snake is going to crash
            if (facing_ID == LogicIDs.SnakeBody)
            {
                // TODO: End the game on a loss.
            }

            // Move the snake head forward.
            _gameGrid.SetIDAtPosition(facing_position, _headDirection);

            // Pseudocode for what needs to be done:
            // Set _playerPosition (which is the old snake position) to be a LogicIDs.SnakeBody. Do this with _gameGrid.SetIDAtPosition().
            _gameGrid.SetIDAtPosition(_playerPosition, LogicIDs.SnakeBody);
            // If the facing_id == LogicIDs.Apple
            //      Increment _snakeLength by 1.
            if (facing_ID == LogicIDs.Apple)
            {
                _snakeLength += 1;
            }
            
            // Finally, i can talk about the "movement pathfinding" thing I've been hyping up.
            // Each body segment of the snake has something called "WhoAmI." This is an integer that tracks what body segment this is from the snake.
            // The count starts at 0 (following the concept of indices starting at 0), and goes up by one for each body piece the snake extends close to the end.
            // Since each segment of the snake gets a higher WhoAmI count closer to the tail, we can find the end of the snake by just looking for higher and higher counts.
            // All we do to move the snake is just delete the one closest to the end.

            // What would this algorithm entail?
            // Firstly, it would be recursive. Meaning, we would start from one point (the head of the snake), and continue searching if we found a snake segment.
            // Start from a given position (the snake head).
            // Check the cell to the North of the current position. Is it a snake body segment with WhoAmI greater than or equal to* the current cell's WhoAmI?
            //      If yes, begin the search from that cell.
            // If no, check the cell to the East. Is it a snake body with WhoAmI >= the current cell's WhoAmI?
            //      If yes, begin the search from that cell.
            // This repeats for all four cardinal directions. This will *ALWAYS* find the end of the snake. This is why I referred to it as pathfinding: This is the backbone of all pathing algorithms.
            // Once we reach the end of the snake, we then check the length of the snake, stored as _snakeLength.
            // If we did more operations than the _snakeLength, then that means the snake moved instead of growing (from eating an apple).
            // Since the snake moved, we should turn the final segment into an empty space.
            // * I said greater than or equal to here. This is not a mistake: We are checking for a body segment with equal to, or +1 WhoAmI of the current cell.
            //      This is done because there may end up being duplicate WhoAmI's, especially near the front of the snake.

            // While the pathfinding algorithm is being done, we can store the positions of the cells that were checked. We can then invert this for the creation of a new apple.
            // This gives us all positions of empty cells at *no* additional cost to the player's computing power.




            //      Detect if the facing ID was an apple
            //          Extend the snake's length if the snake did eat an apple.
            //      Move the rest of the snake via pathfinding (talk to Scott about this)
            //          In order to perform this, all non-empty cells will have to be investigated. This will be done in tandem with creating an apple.
            // If an apple no longer exists, generate a new one.

            // TODO: Make the given amount of time be variable.
            //
            // END GAME LOGIC!
        }
    }
}