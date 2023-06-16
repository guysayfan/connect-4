using connect_4_core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace connect_4
{
    public class NormalAIPlayer : IPlayer
    {
        uint aiPlayer;
        uint human;
        public NormalAIPlayer(uint player, uint human)
        {
            aiPlayer = player;
            this.human = human;
        }

        VictoryChecker victoryChecker = new VictoryChecker();

        Random rnd = new Random();
        public uint Play(Board board)
        {
            Thread.Sleep(1000);

            // Check if AI wins
            var cols = FindWinningCols(board, aiPlayer);
            if (cols.Count > 0) {
                return cols.First();
            }

            // Check if human wins
            cols = FindWinningCols(board, human);
            if (cols.Count > 0)
            {
                return cols.First();
            }

            uint col = (uint)rnd.Next(7);
            while (board.IsColFull(col))
            {
                col = (uint)rnd.Next(7);
            }
            return col;
        }

        private HashSet<uint> FindWinningCols(Board board, uint player)
        {
            var cols = new HashSet<uint>();
            for (uint i = 0; i < 7; i++)
            {
                //check if column is full
                if (board.IsColFull(i)) {
                    continue;
                }

                //clone board and drop piece in current col
                var b = new Board(board);
                b.DropPiece(i, player);

                //if top row is replaced with ai piece checks if the ai will win
                if (victoryChecker.CheckVictory(b, i, player))
                {
                    cols.Add(i);
                }
            }
            return cols;
        }
    }
}