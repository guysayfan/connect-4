using System.Numerics;
using System.Text;

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

        public bool CheckWin(uint col, uint player)
        {
            return victoryChecker.CheckVictory(this, col, player);
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

        public uint? Get(uint col, uint row) => board[col, row];

        public HashSet<uint> FindAvailableCols()
        {
            var cols = new HashSet<uint>();

            for (uint i = 0; i < 7; i++)
            {
                if (!IsColFull(i))
                {
                    cols.Add(i);
                }
            }

            return cols;
        }

        public uint CountPieces()
        {
            uint counter = 0;
            for (uint i = 0; i < 6; i++)
            {
                for (uint j = 0; j < 7; j++)
                {
                    if (board[j, i] != null)
                    {
                        counter++;
                    }
                }
            }
            return counter;
        }

        public override string ToString()
        {
            char c;
            var result = new StringBuilder("", 48);
            for (uint row = 0; row < 6; row++)
            {
                var ln = new StringBuilder("", 7);
                for (uint col = 0; col < 7; col++)
                {
                    var player = Get(col, row);
                    switch (player)
                    {
                        case 0:
                            c = 'o';
                            break;
                        case 1:
                            c = 'x';
                            break;
                        default:
                            c = '.';
                            break;
                    }
                    ln.Append(c);
                }
                Console.WriteLine(ln.ToString());
                result.Append(ln + Environment.NewLine);
            }
            return result.ToString();
        }
    }
}