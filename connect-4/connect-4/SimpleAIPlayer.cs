using connect_4_core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace connect_4
{
    public class SimpleAIPlayer : IPlayer
    {
        Random rnd = new Random();
        public int Play(IBoard board)
        {
            Thread.Sleep(1000);
            var col = rnd.Next(7);
            while (board.IsColFull(col)) 
            {
                col = rnd.Next(7);
            }
            return col;
        }
    }
}
