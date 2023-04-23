using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace connect_4_core
{
    public interface IBoard
    {
        uint DropPiece(uint col, uint player);

        void RemoveTopPiece(uint col);

        bool IsColFull(uint col);

        uint? GetPlayer(Location location);

        uint FindTopRow(uint col);

        bool CheckWin(uint row, uint player);

        HashSet<uint> FindAvailableCols();
    }
}
