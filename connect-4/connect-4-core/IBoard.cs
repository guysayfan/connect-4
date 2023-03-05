﻿using System;
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

        bool CheckRowWin(uint row, uint player);

        bool CheckColWin(uint col, uint player);

        bool CheckTopLeftBotRightWin(Location location, uint player);

        bool CheckBotLeftTopRightWin(Location initLocation, Location clickLocation, uint player);
    }
}
