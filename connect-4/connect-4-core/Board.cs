using System.Numerics;

namespace connect_4_core
{
    public class Board : IBoard
    {
        VictoryChecker victoryChecker = new VictoryChecker();


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

        public void RemoveTopPiece(uint col)
        {
            var topRow = FindTopRow(col);
            if (topRow != INVALID_ROW)
            {
                board[col, topRow] = null;
            }
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
            if (board[col, 0] != null) {
                return INVALID_ROW;
            }

            uint i;
            for (i = 1; i < 6; i++)
            {                
                if (board[col, i] != null)
                {
                    break;
                }
            }
            return i - 1;
        }

        public bool CheckRowWin(uint col, uint player)
        {
            return victoryChecker.CheckHorizontalWin(this, col, player);
        }

        public bool CheckColWin(uint col, uint player)
        {
            return victoryChecker.CheckVerticalWin(this, col, player);
        }

        public bool CheckTopLeftBotRightWin(uint col, uint player)
        {
            return victoryChecker.CheckTopLeftBotRightWin(this, col, player);

        }

        public bool CheckBotLeftTopRightWin(uint col, uint player)
        {
            return victoryChecker.CheckBotLeftTopRightWin(this, col, player);

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

        public void Set(uint col, uint row, uint player)
        {
            board[col, row] = player;
        }

        public uint?[,] GetBoard()
        {
            return board;
        }

        public IBoard Clone()
        {
            return (IBoard)this.MemberwiseClone();
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

/************************

0, 0 : 1
0, 1 : 2
0, 2 : 3
0, 3 : 4
0, 4 : 5
0, 5 : 6
1, 5 : 6
2, 5 : 5
3, 5 : 4
4, 5 : 3
5, 5 : 2
6, 5 : 1

*/