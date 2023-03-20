using connect_4_core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace connect_4
{
    public class NormalAIPlayer
    {
        uint player;
        public NormalAIPlayer(uint player)
        {
            this.player = player;
        }

        VictoryChecker victoryChecker = new VictoryChecker();

        Random rnd = new Random();
        public uint Play(IBoard board)
        {
            Thread.Sleep(1000);
            int col = rnd.Next(7);
            while (board.IsColFull((uint)col))
            {
                col = rnd.Next(7);
            }
            return (uint)col;
        }

        private HashSet<uint> FindWinningCols(IBoard board)
        {
            var cols = new HashSet<uint>();
            for (uint i = 0; i < 7; i++)
            {
                //check if column is full
                //if top row is replaced with ai piece checks if the ai will win
                if (victoryChecker.CheckVerticalWin(board, i, player))
                {
                    cols.Add(i);
                }
                else if (victoryChecker.CheckHorizontalWin(board, i, player))
                {
                    cols.Add(i);
                }
                else if (victoryChecker.CheckTopLeftBotRightWin(board, i, player))
                {
                    cols.Add(i);
                }
                else if (victoryChecker.CheckBotLeftTopRightWin(board, i, player))
                {
                    cols.Add(i);
                }
            }
            return cols;
        }
    }
}
