using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace connect_4_core
{
    public interface IGameEngine
    {
        int GetActivePlayer();

        int GetWinner();

        IBoard GetBoard();
    }
}
