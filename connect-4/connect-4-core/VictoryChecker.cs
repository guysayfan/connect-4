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
        private uint getRow(IBoard board, uint col)
        {
            var row = board.FindTopRow(col) + 1;
            return row > 6 ? 0 : row;
        }

        public bool CheckVerticalWin(IBoard board, uint col, uint player)
        {
            var row = board.FindTopRow(col) + 1;
            if (row > 2)
            {
                return false;
            }

            for (uint i = row + 1; i < 4 + row; i++)
            {
                if (board.GetPlayer(new Location(col, i)) != player) {
                    return false;
                }
            }
            return true;
        }

        public bool CheckHorizontalWin(IBoard board, uint col, uint player)
        {
            uint counter = 1;
            var row = getRow(board, col);
            if (row > 5) {
                return false;
            }

            // Left check
            for (uint i = 1; i < 4; i++)
            {
                if (col < i)
                {
                    break;
                }
                if (board.GetPlayer(new Location(col - i, row)) != player)
                {
                    break;
                }

                counter++;
            }

            if (counter >= 4) {
                return true;
            }

            // Right check
            for (uint i = 1; i < 4; i++)
            {
                if (col + i > 6)
                {
                    break;
                }
                if (board.GetPlayer(new Location(col + i, row)) != player)
                {
                    break;
                }

                counter++;
            }

            return counter >= 4;
        }

        public bool CheckTopLeftBotRightWin(IBoard board, uint col, uint player)
        {
            uint counter = 1;
            var row = getRow(board, col);
            // Top left check
            for (uint i = 1; i < 4; i++)
            {
                if (col < i || row < i)
                {
                    break;
                }
                if (board.GetPlayer(new Location(col - i, row - i)) != player)
                {
                    break;
                }

                counter++;
            }

            if (counter >= 4) {
                return true;
            }

            // Bottom right check
            for (uint i = 1; i < 4; i++)
            {
                if (col + i > 6 || row + i > 5)
                {
                    break;
                }
                if (board.GetPlayer(new Location(col + i, row + i)) != player)
                {
                    break;
                }

                counter++;
            }

            return counter >= 4;
        }

        public bool CheckBotLeftTopRightWin(IBoard board, uint col, uint player)
        {
            uint counter = 1;
            var row = getRow(board, col);

            // Top right check
            for (uint i = 1; i < 4; i++)
            {
                if (col + i > 6 || row < i)
                {
                    break;
                }
                if (board.GetPlayer(new Location(col + i, row - i)) != player)
                {
                    break;
                }

                counter++;
            }

            if (counter >= 4)
            {
                return true;
            }

            // Bottom left check
            for (uint i = 1; i < 4; i++)
            {
                if (col < i || row + i > 5)
                {
                    break;
                }
                if (board.GetPlayer(new Location(col - i, row + i)) != player)
                {
                    break;
                }

                counter++;
            }

            return counter >= 4;
        }
    }
}
