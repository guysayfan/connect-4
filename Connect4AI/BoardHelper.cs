using connect_4_core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4AI
{

    public static class BoardExtensions
    {
        public static HashSet<uint> FindWinningCols(this Board board, PlayerID player)
        {
            var cols = new HashSet<uint>();
            for (uint i = 0; i < 7; i++)
            {
                //check if column is full
                if (board.IsColFull(i))
                {
                    continue;
                }

                //clone board and drop piece in current col
                var b = new Board(board);
                b.DropPiece(i, player);

                //if top row is replaced with ai piece checks if the ai will win
                if (VictoryChecker.CheckVictory(b, i, player))
                {
                    cols.Add(i);
                }
            }
            return cols;
        }
    }
}
