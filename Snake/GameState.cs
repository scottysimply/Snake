using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public enum GameState
    {
        Inactive, // In this state, the user can press any key and the game will begin.
        Playing, // In this state, the user is actively playing the game
    }
}
