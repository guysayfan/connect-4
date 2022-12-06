using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace connect_4_core
{
    public interface IBoard
    {
        int DropPiece(int col, int player);

        bool IsColFull(int col);

        int? GetPlayer(int col, int row);

        int FindTopRow(int col);

        bool CheckRowWin(int row, int player);

        bool CheckColWin(int col, int player);

        bool CheckTopLeftBotRightWin(int initCol, int initRow, int player);

        bool CheckTopRightBotLeftWin(int initCol, int initRow, int player);
    }
}
