using connect_4_core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace connect_4
{
    public class SuperAIPlayer : IPlayer
    {
        uint aiPlayer;
        uint human;
        uint lookAhead;
        public SuperAIPlayer(uint player, uint human, uint lookAhead)
        {
            aiPlayer = player;
            this.human = human;
            this.lookAhead = lookAhead;
        }

        VictoryChecker victoryChecker = new VictoryChecker();

        Random rnd = new Random();
        public uint Play(IBoard board)
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

            var pCols = FindPotentialCols(board);

            uint index = (uint)rnd.Next(pCols.Length);
            return pCols[index];
        }


        private uint[] FindPotentialCols(IBoard board)
        {
            var cols = new HashSet<uint>();



            return cols.ToArray();
        }

        // Finds columns that lead to a sure victory for AI player or human player on their next turn
        private Dictionary<uint, IBoard> CheckBoards(HashSet<IBoard> boards, uint player)
        {
            var result = new Dictionary<uint, IBoard>();
            foreach(IBoard b in boards)
            {
                var cols = FindWinningCols(b, player);
            }

            return result;
        }

        private HashSet<uint> FindWinningCols(IBoard board, uint player)
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
                if (victoryChecker.CheckVerticalWin(b, i, player))
                {
                    cols.Add(i);
                }
                else if (victoryChecker.CheckHorizontalWin(b, i, player))
                {
                    cols.Add(i);
                }
                else if (victoryChecker.CheckTopLeftBotRightWin(b, i, player))
                {
                    cols.Add(i);
                }
                else if (victoryChecker.CheckBotLeftTopRightWin(b, i, player))
                {
                    cols.Add(i);
                }
            }
            return cols;
        }
    }
}