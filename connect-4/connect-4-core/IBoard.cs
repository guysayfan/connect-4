using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace connect_4_core
{
    public interface IBoard
    {
        int DropPiece(uint col, int player);

        bool IsColFull(uint col);

        int? GetPlayer(Location location);

        int FindTopRow(uint col);

        bool CheckRowWin(uint row, int player);

        bool CheckColWin(uint col, int player);

        bool CheckTopLeftBotRightWin(Location location, int player);

        bool CheckTopRightBotLeftWin(Location location, int player);
    }
}
