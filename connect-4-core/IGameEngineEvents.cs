using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace connect_4_core
{
    public interface IGameEngineEvents
    {
        void OnDropPiece(Location location);

        void OnGameOver(uint winner);
    }
}