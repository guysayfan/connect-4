using System.Numerics;
using System.Text;

namespace connect_4_core
{
    public class Board
    {
        public const int INVALID_ROW = 999;
        PlayerID[,] board = new PlayerID[7, 6];

        public Board()
        {
        }

        public uint LastPiece
        {
            get; set;
        }
        public Board(Board b)
        {
            LastPiece = b.LastPiece;
            for (uint col = 0; col < 7; col++)
            {
                for (int row = 0; row < 6; row++) 
                {
                    board[col, row] = b.board[col, row];
                }
            }            
        }

        public uint DropPiece(uint col, PlayerID player)
        {
            var topRow = FindTopRow(col);
            if (topRow == INVALID_ROW)
            {
                throw new Exception("Col full");
            }
            board[col, topRow] = player;
            LastPiece = col;

            return topRow;
        }

        public PlayerID LastPlayer => CountPieces() % 2 == 0 ? PlayerID.Two : PlayerID.One;

        public void RemoveTopPiece(uint col)
        {
            var topRow = FindTopRow(col);
            if (topRow != INVALID_ROW)
            {
                board[col, topRow] = PlayerID.None;
            }
        }

        public bool IsColFull(uint col)
        {
            return FindTopRow(col) == INVALID_ROW;
        }
        
        public PlayerID GetPlayer(Location location)
        {
            return board[location.Col, location.Row];
        }

        // returns top occupied row
        public uint FindTopRow(uint col)
        {
            if (board[col, 0] != PlayerID.None) {
                return INVALID_ROW;
            }

            uint i;
            for (i = 1; i < 6; i++)
            {                
                if (board[col, i] != PlayerID.None)
                {
                    break;
                }
            }
            return i - 1;
        }

        public bool CheckWin(uint col, PlayerID player)
        {
            return VictoryChecker.CheckVictory(this, col, player);
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
                board[col, row] = PlayerID.One;
            });

            player2Pieces.ForEach(item =>
            {
                var row = item[1];
                var col = item[0];
                board[col, row] = PlayerID.Two;
            });
        }

        public void Set(uint col, uint row, PlayerID player)
        {
            board[col, row] = player;
        }

        public PlayerID Get(uint col, uint row) => board[col, row];

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
                    if (board[j, i] != PlayerID.None)
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
                    c = player switch
                    {
                        PlayerID.One => 'x',
                        PlayerID.Two => 'o',
                        _ => '.',
                    };
                    ln.Append(c);
                }
                Console.WriteLine(ln.ToString());
                result.Append(ln + Environment.NewLine);
            }
            return result.ToString();
        }
    }
}