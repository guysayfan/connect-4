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
        PlayerID activePlayer;

        PlayerID winner = PlayerID.None;

        public GameEngine()
        {
            board = new Board();
            rnd = new Random();
            activePlayer = rnd.Next(2) == 0 ? PlayerID.One : PlayerID.Two;
        }
        public PlayerID GetActivePlayer()
        {
            return activePlayer;
        }

        public PlayerID GetWinner()
        {
            return winner;
        }

        public Board GetBoard()
        {
            return new Board(board);
        }


        public void Run(IPlayer p1, IPlayer p2, IGameEngineEvents sink)
        {
             var players = new Dictionary<PlayerID, IPlayer> { 
                 { PlayerID.One, p1 }, 
                 { PlayerID.Two, p2 } 
             };

            Location location = new Location(0, 0);
            while (!IsGameOver(location.Col)) {
                activePlayer = activePlayer.Other();
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

        private bool IsGameOver(uint col)
        {
            int counter = 0;
            for (uint i = 0; i < 7; i++)
            {
                if (board.IsColFull(i))
                {
                    counter++;
                }
            }

            if (board.CheckWin(col, activePlayer))
            {
                winner = activePlayer;
                return true;
            }

            return counter == 7;
        }
    }
}
