using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public enum LogicIDs
    {
        Empty,
        Apple,
        SnakeHeadNorth,
        SnakeHeadSouth,
        SnakeHeadEast,
        SnakeHeadWest,
        SnakeBody,
        // I am 100% aware that there is a much better way to make Snake as a game (by using objects). However, not only am I lazy, but functional programming is much easier to grasp as a beginner.
        // It would be wise to keep the game logic functional to help ease my partner into programming with MonoGame. A lot of calls to random objects is confusing.
    }
}
