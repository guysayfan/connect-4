using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace connect_4_core
{
    public class GameEngine : IGameEngine
    {
        const uint NO_WINNER = 999;

        Board board;

        Random rnd;
        uint activePlayer;

        uint winner = NO_WINNER;

        public GameEngine()
        {
            board = new Board();
            rnd = new Random();
            activePlayer = (uint)rnd.Next(2);
        }
        public uint GetActivePlayer()
        {
            return activePlayer;
        }

        public uint GetWinner()
        {
            return winner;
        }

        public IBoard GetBoard()
        {
            return board.Clone();
        }


        public void Run(IPlayer p1, IPlayer p2, IGameEngineEvents sink)
        {
            IPlayer[] players = {p1, p2};

            Location location = new Location(0, 0);
            while (!IsGameOver(location)) {
                activePlayer = activePlayer == 0U ? 1U : 0U;
                location.Col = players[activePlayer].Play(GetBoard());
                location.Row = board.DropPiece(location.Col, activePlayer);
                sink.OnDropPiece(location);
            }
            sink.OnGameOver(GetWinner());
        }

        public Location GetTopLeftBotRightInit(Location location)
        {
            Location initials = new Location(0, 0);
            if (location.Col > location.Row)
            {
                initials.Col = location.Col - location.Row;
                initials.Row = 0;
            } else if (location.Col < location.Row)
            {
                initials.Col = 0;
                initials.Row = location.Row - location.Col;
            }

            return initials;
        }

        public Location GetBotLeftTopRightInit(Location location)
        {
            Location initials = new Location(0, 5);
            if (location.Col > 5 - location.Row)
            {
                initials.Col = location.Col - (5 - location.Row);
                initials.Row = 5;
            } else if (location.Col + location.Row == 5)
            {
                initials.Col = 0;
                initials.Row = 5;
            } else if (location.Col < location.Row)
            {
                initials.Row += initials.Col;
                initials.Col = 0;
            } 

            return initials;
        }

        private bool IsGameOver(Location location)
        {
            int counter = 0;
            for (uint i = 0; i < 7; i++)
            {
                if (board.IsColFull(i))
                {
                    counter++;
                }
            }

            Location initialsTopLeft = GetTopLeftBotRightInit(location);
            Location initialsBotLeft = GetBotLeftTopRightInit(location);

            if (board.CheckRowWin(location.Row, activePlayer))
            {
                winner = activePlayer;
                return true;
            } else if (board.CheckColWin(location.Col, activePlayer))
            {
                winner = activePlayer;
                return true;
            } else if (board.CheckTopLeftBotRightWin(initialsTopLeft, activePlayer))
            {
                winner = activePlayer;
                return true;
            } else if (board.CheckBotLeftTopRightWin(initialsBotLeft, location, activePlayer))
            {
                winner = activePlayer;
                return true;
            }

            return counter == 7;
        }
    }
}
