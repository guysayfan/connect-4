using System.Numerics;

namespace connect_4_core
{
    public class Board : IBoard
    {
        VictoryChecker victoryChecker = new VictoryChecker();


        const int INVALID_ROW = 999;

        uint?[,] board = new uint?[7, 6];

        public Board()
        {
        }

        public Board(IBoard b)
        {
            Board bb = (Board)b;

            for (uint col = 0; col < 7; col++)
            {
                for (int row = 0; row < 6; row++) 
                {
                    board[col, row] = bb.board[col, row];
                }
            }            
        }

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

    }
}