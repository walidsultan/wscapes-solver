using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WS.Wscapes.DataTypes
{
    public enum GameState
    {
        Initializing,
        Menu,
        Puzzle,
        LevelSolved,
        PiggyBank,
        ReOrderLevel,
        Transitioning,
        ClickTeam,
        Chat,
        ClickHelp,
        ClickChatBack
    }
}
