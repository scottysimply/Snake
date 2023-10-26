using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ConnectFour
{
    public enum GameStateSettings
    {
        YellowWin,
        RedWin,
        Draw,
        Ongoing,
    }
    public class GameEndForm
    {
        public GameStateSettings CurrentState {  get; set; }
        public Rectangle dimensions;
        public GameEndForm(GameStateSettings game_state, Point size, Point center) 
        {
            if (game_state == GameStateSettings.Ongoing)
            {
                return;
            }
        }
    }
}
