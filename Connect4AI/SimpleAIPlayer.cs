using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using connect_4_core;

namespace Connect4AI
{
    public class SimpleAIPlayer : IPlayer
    {
        Random rnd = new Random();
        public uint Play(Board board)
        {
            Thread.Sleep(1000);
            int col = rnd.Next(7);
            while (board.IsColFull((uint)col)) 
            {
                col = rnd.Next(7);
            }
            return (uint)col;
        }
    }
}
