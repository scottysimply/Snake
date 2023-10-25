﻿using Microsoft.Xna.Framework;
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

        private GameBoard _gameBoard;

        public bool IsYellowsTurn { get; set; }

        public ConnectFourGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            // Initialize bounds.
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferMultiSampling = false;
        }
        const int NUM_ROWS = 6;
        const int NUM_COLUMNS = 7;
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            _gameBoard = new GameBoard(NUM_COLUMNS, NUM_ROWS, Window.ClientBounds.Size, out PIECE_SIZE);

            oldState = new();
            IsYellowsTurn = true;


            base.Initialize();
        }
        internal static Texture2D EmptySpace;
        internal static Texture2D PlayerToken;
        internal static Texture2D RedToken;
        internal static Texture2D YellowToken;
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            
            // My images.
            EmptySpace = Content.Load<Texture2D>("EmptySpace");
            PlayerToken = Content.Load<Texture2D>("PlayerToken");
            RedToken = Content.Load<Texture2D>("RedPiece");
            YellowToken = Content.Load<Texture2D>("YellowPiece");
        }
        MouseState oldState;
        /// <summary>
        /// Width and height of the piece, in pixels.
        /// </summary>
        public int PIECE_SIZE;
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();



            // // // // BEGIN MOUSE LOGIC
            
            // Prelim: Capture mouse state
            var mouseState = Mouse.GetState();

            // Check if mouse is pressed
            if (mouseState.LeftButton == ButtonState.Pressed && oldState.LeftButton != ButtonState.Pressed)
            {
                // If this returns true, spawn a piece! Otherwise... don't.
                if (FindSuitableColumn(mouseState.Position, out Point location))
                {
                    _gameBoard.GameState[location.X, location.Y].State = IsYellowsTurn ? 1 : 2;
                    int game_state = TestWinCondition(location);
                    switch (game_state)
                    {
                        case 1:
                            Exit();
                            break;

                        case 2:
                            Exit();
                            break;

                        case 3:
                            break;

                        default:
                            IsYellowsTurn = !IsYellowsTurn;
                            break;
                    }

                }
            }


            // // // // END MOUSE LOGIC

            // Update old mouse state.
            oldState = mouseState;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            _gameBoard.Draw(_spriteBatch);

            // TODO: Add your drawing code here

            _spriteBatch.End();

            base.Draw(gameTime);
        }
        /// <summary>
        /// Finds the column the piece should be dropped in. Only executes code if the column wasn't empty, and <see cref="FindEmptyRow"/> returns true.
        /// </summary>
        /// <param name="mousePos">Window coordinates of the mouse on click.</param>
        /// <param name="new_piece">Array coordinates of the new piece to be dropped.</param>
        /// <returns>Whether the piece should be dropped. <see cref="FindEmptyRow"/> must also return true for this function to return true.</returns>
        protected bool FindSuitableColumn(Point mousePos, out Point new_piece)
        {
            for (int col = 0; col < _gameBoard.GameState.GetLength(0); col++)
            {
                Rectangle bounds = _gameBoard.GameState[col, 0].Bounds;
                if (bounds.Left <= mousePos.X && mousePos.X <= bounds.Right)
                {
                    // FindEmptyRow returns true if a piece was spawned. Otherwise, it returns false.
                    if (FindEmptyRow(col, out new_piece)) return true;
                }
            }
            new_piece = new(0, 0);
            return false;
        }
        /// <summary>
        /// Determines if the given column <paramref name="new_piece"/> is empty. If it is empty, then a piece will be dropped.
        /// </summary>
        /// <param name="this_column">The column to check.</param>
        /// <param name="new_piece">Array coordinates of the new piece to be dropped.</param>
        /// <returns>Whether the piece should be dropped.</returns>
        protected bool FindEmptyRow(int this_column, out Point new_piece)
        {
            for (int row = _gameBoard.GameState.GetLength(1) - 1; row >= 0; row--)
            {
                if (_gameBoard.GameState[this_column, row].State == 0)
                {
                    new_piece = new(this_column, row);
                    return true;
                }
            }
            new_piece = new(0, 0);
            return false;
        }
        /// <summary>
        /// Determines if the game has been one, and by whom.
        /// </summary>
        /// <param name="location_to_test"></param>
        /// <returns>Returns if the game has been won. <br/>0: Game is ongoing. <br/>1: Yellow (player 1) has won. <br/>2: Red (player 2) has won. <br/>3: Neither player has won (draw).</returns>
        protected int TestWinCondition(Point location_to_test)
        {
            int player = IsYellowsTurn ? 1 : 2;
            for (int i = 0; i < 4; i++)
            {
                if (TestHorizontal(location_to_test - new Point(i, 0), player))
                {
                    return player;
                }
                if (TestVertical(location_to_test - new Point(0, i), player))
                {
                    return player;
                }
                if (TestPositiveSlope(location_to_test - new Point(i, i), player))
                {
                    return player;
                }
                if (TestNegativeSlope(location_to_test - new Point(i, -i), player))
                {
                    return player;
                }
            }
            return 0;
        }
        protected bool TestHorizontal(Point start_location, int player)
        {
            
            for (int i = 0; i < 4; i++)
            {
                Point test_location = new(start_location.X + i, start_location.Y);
                // If point is outside the array, quit everything.
                if (!(0 <= test_location.X && test_location.X < NUM_COLUMNS))
                {
                    return false;
                }
                if (_gameBoard.GameState[test_location.X, test_location.Y].State != player)
                {
                    return false;
                }
            }
            return true;
        }
        protected bool TestVertical(Point start_location, int player)
        {
            for (int i = 0; i < 4; i++)
            {
                Point test_location = new(start_location.X, start_location.Y + i);
                // If point is outside the array, quit everything.
                if (!(0 <= test_location.Y && test_location.Y < NUM_ROWS))
                {
                    return false;
                }
                if (_gameBoard.GameState[test_location.X, test_location.Y].State != player)
                {
                    return false;
                }
            }
            return true;
        }
        protected bool TestPositiveSlope(Point start_location, int player)
        {
            for (int i = 0; i < 4; i++)
            {
                Point test_location = new(start_location.X + i, start_location.Y + i);
                // If point is outside the array, quit everything.
                if (!(0 <= test_location.X && test_location.X < NUM_COLUMNS) || !(0 <= test_location.Y && test_location.Y < NUM_ROWS))
                {
                    return false;
                }
                if (_gameBoard.GameState[test_location.X, test_location.Y].State != player)
                {
                    return false;
                }
            }
            return true;
        }
        protected bool TestNegativeSlope(Point start_location, int player)
        {
            for (int i = 0; i < 4; i++)
            {
                Point test_location = new(start_location.X + i, start_location.Y - i);
                // If point is outside the array, quit everything.
                if (!(0 <= test_location.X && test_location.X < NUM_COLUMNS) || !(0 <= test_location.Y && test_location.Y < NUM_ROWS))
                {
                    return false;
                }
                if (_gameBoard.GameState[test_location.X, test_location.Y].State != player)
                {
                    return false;
                }
            }
            return true;
        }
    }
}