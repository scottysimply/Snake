using Microsoft.Xna.Framework.Input;

namespace Snake.Components
{
    public class InputHandler
    {
        /// <summary>
        /// The current keyboard state
        /// </summary>
        private KeyboardState _currentState;

        /// <summary>
        /// The old keyboard state
        /// </summary>
        private KeyboardState _oldState;

        public InputHandler()
        {
            _currentState = _oldState = Keyboard.GetState();
        }

        /// <summary>
        /// Gets whether the specified <see cref="Keys"/> is pressed.
        /// </summary>
        /// <param name="keyToCheck"></param>
        /// <returns>Returns true <i>on the tick the key was pressed</i>.</returns>
        public bool IsKeyPressed(Keys keyToCheck)
        {
            if (_currentState.IsKeyDown(keyToCheck) && !_oldState.IsKeyDown(keyToCheck))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Gets whether the specified <see cref="Keys"/> is held down.
        /// </summary>
        /// <param name="keyToCheck"></param>
        /// <returns>Returns true on <i>every tick</i> the key is held down.</returns>
        public bool IsKeyHeld(Keys keyToCheck)
        {
            if (_currentState.IsKeyDown(keyToCheck))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Reads the current state of the <see cref="Keyboard"/>.
        /// </summary>
        public void QueryInput()
        {
            _oldState = _currentState;
            _currentState = Keyboard.GetState();
        }

    }
}
