using connect_4_core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace connect_4
{
    public class NormalAIPlayer
    {
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

        private uint FindWinningCol(IBoard board)
        {
            uint col = 0;
            for (uint i = 0; i < 7; i++)
            {

            }
            return col;
        }
    }
}
