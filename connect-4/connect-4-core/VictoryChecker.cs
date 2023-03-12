using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace connect_4_core
{
    public class VictoryChecker
    {
        private bool CheckVerticalWin(IBoard board, uint col, uint player)
        {
            uint topRow = board.FindTopRow(col);

            if (topRow > 2)
            {
                return false;
            }

            for (uint i = topRow + 1; i < 4 + topRow; i++)
            {
                if (board.GetPlayer(new Location(col, i)) != player) {
                    return false;
                }
            }
            return true;
        }

        private bool CheckHorizontalWin(IBoard board, uint col, uint player)
        {
            uint counter = 1;

            // Left check
            for (uint i = 1; i < 4; i++)
            {
                if (col - i < 0)
                {
                    break;
                }
                if (board.GetPlayer(new Location(col - i, board.FindTopRow(col))) != player)
                {
                    break;
                }

                counter++;
            }

            if (counter == 4) {
                return true;
            }

            // Right check
            for (uint i = 1; i < 4; i++)
            {
                if (col + i > 6)
                {
                    break;
                }
                if (board.GetPlayer(new Location(col + i, board.FindTopRow(col))) != player)
                {
                    break;
                }

                counter++;
            }

            return counter == 4;
        }

        private bool CheckTopLeftBotRightWin(IBoard board, uint col)
        {
            return false;
        }

        private bool CheckBotLeftTopRightWin(IBoard board, uint col)
        {
            return false;
        }
    }
}
