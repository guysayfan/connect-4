using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace connect_4_core
{
    public enum PlayerID : int
    {
        None = 0,
        One,
        Two
    }
    public static class Extensions
    {
        public static PlayerID Other(this PlayerID p)
        {
            if (p == PlayerID.None)
            {
                throw new Exception("There is no alternate player for None");
            }
            return p == PlayerID.One ? PlayerID.Two : PlayerID.One;
        }
    }

    public interface IGameEngine
    {
        PlayerID GetActivePlayer();

        PlayerID GetWinner();

        Board GetBoard();

        void Run(IPlayer player1, IPlayer player2, IGameEngineEvents sink);
    }
}