using System.Numerics;

namespace connect_4_core
{
    public class Board : IBoard
    {
        const int INVALID_ROW = 999;

        uint?[,] board = new uint?[7, 6];

        public uint DropPiece(uint col, uint player)
        {
            var topRow = FindTopRow(col);
            if (topRow == INVALID_ROW)
            {
                throw new Exception("Col full");
            }
            board[col, topRow] = player;

            return topRow;
        }

        public bool IsColFull(uint col)
        {
            return FindTopRow(col) == INVALID_ROW;
        }
        
        public uint? GetPlayer(Location location)
        {
            return board[location.Col, location.Row];
        }

        //public bool CheckVictory(int player, int lastPieceRow, int lastPieceCol)
        //{

        //}

        // Finds next available row to place piece

        public uint FindTopRow(uint col)
        {
            for (uint i = 0; i < 6; i++)
            {
                var row = 5 - i;
                if (board[col, row] == null)
                {
                    return row;
                }
            }
            return INVALID_ROW;
        }

        public bool CheckRowWin(uint row, uint player)
        {
            var count = 0;
            for (int col = 0; col < 7; col++)
            {
                if (board[col, row] == player)
                {
                    count++;
                    if (count == 4)
                    {
                        return true;
                    }
                }
                else
                {
                    count = 0;
                }
            }
            return false;
        }

        public bool CheckColWin(uint col, uint player)
        {
            var count = 0;
            for (int row = 0; row < 6; row++)
            {
                if (board[col, row] == player)
                {
                    count++;
                    if (count == 4)
                    {
                        return true;
                    }
                }
                else
                {
                    count = 0;
                }
            }
            return false;
        }

        public uint CalcTopLeftBotRightSteps(Location location)
        {
            uint steps = 0;
            if (location.Col <= location.Row)
            {
                steps = 6 - location.Row;
            } else
            {
                steps = 7 - location.Col;
            }

            return steps;
        }

        public bool CheckTopLeftBotRightWin(Location location, uint player)
        {
            var initCol = location.Col;
            var initRow = location.Row;

            if (initCol > 3 || initRow > 3)
            {
                return false;
            }
            var count = 0;
            var steps = initRow == 0 ? 6 - initCol : 6 - initRow;
            for (int i = 0; i < steps; i++)
            {
                try
                {
                    if (board[initCol + i, initRow + i] == player)
                    {
                        count++;
                        if (count == 4)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        count = 0;
                    }
                }
                catch (Exception e) {
                    if (board[initCol + i, initRow + i] == player)
                    {
                        count++;
                        if (count == 4)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        count = 0;
                    }
                }
            }
            return false;
        }

        public bool CheckBotLeftTopRightWin(Location initLocation, Location clickLocation, uint player)
        {
            var initCol = initLocation.Col;
            var initRow = initLocation.Row;

            if ((clickLocation.Row + clickLocation.Col < 3) || (clickLocation.Row + clickLocation.Col > 8))
            {
                return false;
            }

            var count = 0;
            var steps = initCol == 0 ? initCol + 1 : 7 - initCol;

            if (initRow != 5 && initCol != 0) {
                throw new Exception("initRow must equal 5 or initCol must equal 0");
            }

            for (int i = 0; i < steps; i++)
            {
                var row = initRow - i;
                var col = initCol + i;
                if (board[col, row] == player)
                {
                    count++;
                    if (count == 4)
                    {
                        return true;
                    }
                }
                else
                {
                    count = 0;
                }
            }
            return false;
        }

        /// <summary>
        /// Populate the board with some pieces for testing
        /// </summary>
        public void Populate(List<int[]> player1Pieces, List<int[]> player2Pieces)
        {
            player1Pieces.ForEach(item =>
            {
                var row = item[1];
                var col = item[0];
                board[col, row] = 0;
            });

            player2Pieces.ForEach(item =>
            {
                var row = item[1];
                var col = item[0];
                board[col, row] = 1;
            });

        }

        public uint?[,] GetBoard()
        {
            return board;
        }

        //static bool hasPlayerRow(int step, int player)
        //{

        //}

        //private bool GenericCheck(Predicate<KeyValuePair <int, int>> p, int player, int steps)
        //{
        //    var count = 0;
        //    for (int i = 0; i < steps; i++)
        //    {
        //        var pair = new KeyValuePair<int, int>(i, player);
        //        if (p(pair))
        //        {
        //            count++;
        //            if (count == 4)
        //            {
        //                return true;
        //            }
        //        }
        //        else
        //        {
        //            count = 0;
        //        }
        //    }
        //    return false;
        //}
    }
}