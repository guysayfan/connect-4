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
            throw new NotImplementedException();
        }

        public IBoard GetBoard()
        {
            return board;
        }


        public void Run(IPlayer p1, IPlayer p2)
        {
            IPlayer[] players = {p1, p2};
            int col;
            int activePlayer = 0;

            while (!IsGameOver()) {
                col = players[activePlayer].Play(board);
                board.DropPiece(col, activePlayer + 1);

                activePlayer = activePlayer == 0 ? 0 : 1;
            }
        }

        private bool IsGameOver()
        {
            return false;
        }
    }
}
