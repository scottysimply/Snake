using Microsoft.Xna.Framework;

namespace ConnectFour
{
    public class GamePiece
    {
        public Vector2 Position {  get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Team Team { get; set; }
        public GamePiece(int width, int height, Vector2 position, Team team)
        {
            Width = width;
            Height = height;
            Position = position;
        }
        

    }
}
