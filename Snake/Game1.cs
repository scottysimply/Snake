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
        private GameState _gameState;

        private Point _playerPosition;
        private bool _hasGraceBeenGiven;
        private LogicIDs _headDirection;
        private int _score;

        private LogicIDs _bufferedInput;

        DialogBox _currentDialog;
        private Rectangle _dialogDimensions;


        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private InputHandler _inputHandler;
        private int _stepTimer;
        private int _framesPerStep;

        Random _rand;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // Initializing the game's properties
            SetupGame();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Set Dimensions of the window and game. This can't be done in initialize for some reason.
            _graphics.PreferredBackBufferHeight = 768;
            _graphics.PreferredBackBufferWidth = 1024;
            _graphics.ApplyChanges();
            Dimensions = Window.ClientBounds;
            _gameGrid = new(20, 15, Dimensions.Size);

            // Set dialog box dimensions
            _dialogDimensions = new((int)(Dimensions.Width / 2 - 0.25f * Dimensions.Width),
                                    (int)(Dimensions.Height / 2 - 0.25f * Dimensions.Height),
                                    Dimensions.Width / 2,
                                    Dimensions.Height / 4);

            // Setting the dialog box.
            _currentDialog = new DialogBox(_dialogDimensions, "Press space to begin.\nUse WASD or arrow keys to move.");

            // Create the initial snake.
            _playerPosition = _gameGrid.SpawnSnake();

            // Spawn first apple.
            _gameGrid.SpawnAnApple(_rand);

            // Initialize the SpriteBatch object (does the rendering)
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load sprites
            AssetList.TBlankCell = Content.Load<Texture2D>("BlankCell");
            AssetList.TApple = Content.Load<Texture2D>("Apple");
            AssetList.TSnakeBody = Content.Load<Texture2D>("SnakeBody");
            AssetList.TSnakeHeadNorth = Content.Load<Texture2D>("SnakeHeadNorth");
            AssetList.TSnakeHeadSouth = Content.Load<Texture2D>("SnakeHeadSouth");
            AssetList.TSnakeHeadEast = Content.Load<Texture2D>("SnakeHeadEast");
            AssetList.TSnakeHeadWest = Content.Load<Texture2D>("SnakeHeadWest");
            AssetList.BlankSquare = Content.Load<Texture2D>("EmptySquare");
            AssetList.ArialLarge = Content.Load<SpriteFont>("ArialLarge");
            AssetList.ArialSmall = Content.Load<SpriteFont>("ArialSmall");
        }

        protected override void Update(GameTime gameTime)
        {
            // Order of operations for game logic:
            // Completed: Get the current state of the keyboard.
            _inputHandler.QueryInput();

            if (_gameState == GameState.Inactive)
            {
                InactiveLogic();
            }
            else
            {
                // Run all movement related checks
                HandleMovement();

                // Run game logic if there is a valid time step for it.
                if (_stepTimer > _framesPerStep)
                {
                    _gameGrid.SetIDAtPosition(_playerPosition, _bufferedInput);
                    _headDirection = _bufferedInput;
                    GameLogic(gameTime);
                    _stepTimer = 0;
                }
                _stepTimer++;
            }
        }
        private void InactiveLogic()
        {
            if (_inputHandler.IsKeyPressed(Keys.Space))
            {
                _gameState = GameState.Playing;
                _currentDialog = null;
            }
        }
        private void HandleMovement()
        {
            // Add killswitch activated via escape:
            if (_inputHandler.IsKeyHeld(Keys.Escape))
            {
                Exit();
            }

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
                    _bufferedInput = LogicIDs.SnakeHeadNorth;
                }
            }
            // If down is pressed
            if (_inputHandler.IsKeyHeld(Keys.S) || _inputHandler.IsKeyPressed(Keys.Down))
            {
                // If snake is facing left or right, make the snake face down.
                if (_headDirection == LogicIDs.SnakeHeadWest || _headDirection == LogicIDs.SnakeHeadEast)
                {
                    _bufferedInput = LogicIDs.SnakeHeadSouth;
                }
            }
            // If left is pressed
            if (_inputHandler.IsKeyHeld(Keys.A) || _inputHandler.IsKeyPressed(Keys.Left))
            {
                // If snake is facing up or down, make the snake face left.
                if (_headDirection == LogicIDs.SnakeHeadSouth || _headDirection == LogicIDs.SnakeHeadNorth)
                {
                    _bufferedInput = LogicIDs.SnakeHeadWest;
                }
            }
            // If right is pressed
            if (_inputHandler.IsKeyHeld(Keys.D) || _inputHandler.IsKeyPressed(Keys.Right))
            {
                // If snake is facing up or down, make the snake face right.
                if (_headDirection == LogicIDs.SnakeHeadSouth || _headDirection == LogicIDs.SnakeHeadNorth)
                {
                    _bufferedInput = LogicIDs.SnakeHeadEast;
                }
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

            GraphicsDevice.Clear(Color.CornflowerBlue);

            _gameGrid.DrawGrid(_spriteBatch);
            if (_currentDialog is not null)
            {
                _currentDialog.Draw(_spriteBatch);
            }

            // Draw current score:
            Vector2 text_offset = AssetList.ArialSmall.MeasureString($"Score: {_score}");
            Vector2 draw_position = new Vector2(Dimensions.Width - 12 - text_offset.X, 4 + text_offset.Y / 2);
            _spriteBatch.DrawString(AssetList.ArialSmall, $"Score: {_score}", draw_position, Color.WhiteSmoke);

            _spriteBatch.End();
        }
        private void SetupGame()
        {
            _bufferedInput = LogicIDs.SnakeHeadEast;
            _gameState = GameState.Inactive;
            _inputHandler = new();
            _stepTimer = 0;
            _rand = new();
            _score = 0;
            _framesPerStep = 12;
        }
        private void Crashed()
        {
            _currentDialog = new DialogBox(_dialogDimensions, $"Game over. Your score was {_score}.\nPress space to restart.");

            // Prepare the game board.
            _gameGrid.ClearGrid();
            _playerPosition = _gameGrid.SpawnSnake();
            _gameGrid.SpawnAnApple(_rand);
            SetupGame();
        }
        private void GameLogic(GameTime gameTime)
        {
            // Get the direction of the snake
            _headDirection = _gameGrid.GetIDAtPosition(_playerPosition);

            // Get the position of the cell in front of the snake
            Point facing_position = (_headDirection) switch
            {
                LogicIDs.SnakeHeadNorth => new Point(_playerPosition.X, _playerPosition.Y - 1),
                LogicIDs.SnakeHeadSouth => new Point(_playerPosition.X, _playerPosition.Y + 1),
                LogicIDs.SnakeHeadEast => new Point(_playerPosition.X + 1, _playerPosition.Y),
                LogicIDs.SnakeHeadWest => new Point(_playerPosition.X - 1, _playerPosition.Y),
                _ => _playerPosition,
            };
            // If snake is outside the game.
            if (facing_position.X < 0 || facing_position.X >= _gameGrid.Size.X || facing_position.Y < 0 || facing_position.Y >= _gameGrid.Size.Y)
            {
                // In snake, you usually have grace frames when you're about to crash. To simulate this, I will allow 1 extra game step to re-adjust. If the input still isn't given, the game is lost.
                if (!_hasGraceBeenGiven)
                {
                    _hasGraceBeenGiven = true;
                    return;
                }
                Crashed();
                return;
            }

            // Get the ID of the cell in front of the snake
            LogicIDs facing_ID = _gameGrid.GetIDAtPosition(facing_position);
            
            // Also have to check for this crash.
            if (facing_ID == LogicIDs.SnakeBody)
            {
                if (!_hasGraceBeenGiven)
                {
                    _hasGraceBeenGiven = true;
                    return;
                }
                Crashed();
                return;
            }

            // Move the snake head forward.
            _gameGrid.SetIDAtPosition(facing_position, _headDirection);
            _gameGrid.SetWhoAmIAtPosition(facing_position, 0);

            // Pseudocode for what needs to be done:
            // Set _playerPosition (which is the old snake position) to be a LogicIDs.SnakeBody. Do this with _gameGrid.SetIDAtPosition().
            _gameGrid.SetIDAtPosition(_playerPosition, LogicIDs.SnakeBody);
            // Correct the snake's order and find the tail.
            Point tail_location = _gameGrid.PathFind(facing_position);
            // If the snake ate an apple, spawn a new one. Increase the score. Else, remove the tail of the snake (the snake moved!)
            if (facing_ID == LogicIDs.Apple)
            {
                _gameGrid.SpawnAnApple(_rand);
                _score++;
                // Do frame/step timer calculations to speed up the game.
                if ((_score + 1) % 10 == 0 && _framesPerStep > 4)
                {
                    _framesPerStep -= 1;
                }
            }
            else
            {
                _gameGrid.SetIDAtPosition(tail_location, LogicIDs.Empty);
                _gameGrid.SetWhoAmIAtPosition(tail_location, -1);
            }

            // Update the player's position:
            _playerPosition = facing_position;
            _hasGraceBeenGiven = false;
            
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