using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace connect_4_core
{
    public class GameEngine : IGameEngine
    {
        IBoard board = new Board();

        int activePlayer = 1;
        public int GetActivePlayer()
        {
            if (activePlayer == 0)
            {
                activePlayer = 1;
            } else if (activePlayer == 1)
            {
                activePlayer = 0;
            }

            return activePlayer;
        }

        public int GetWinner()
        {
            return -1;
        }

        public IBoard GetBoard()
        {
            return board;
        }


        public void Run(IPlayer p1, IPlayer p2, IGameEngineEvents sink)
        {
            IPlayer[] players = {p1, p2};
            int col;
            int activePlayer = 0;

            while (!IsGameOver()) {
                col = players[activePlayer].Play(board);
                var row = board.DropPiece(col, activePlayer);
                sink.OnDropPiece(row, col);

                activePlayer = activePlayer == 0 ? 1 : 0;
            }
            sink.OnGameOver(GetWinner());
        }

        private bool IsGameOver()
        {
            int counter = 0;
            for (int i = 0; i < 7; i++)
            {
                if (board.IsColFull(i))
                {
                    counter++;
                }
            }

            if (counter == 7) return true;

            return false;
        }
    }
}
