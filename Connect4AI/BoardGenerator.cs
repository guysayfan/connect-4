using connect_4_core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BoardDict = System.Collections.Generic.Dictionary<connect_4_core.IBoard, System.Collections.Generic.HashSet<System.Collections.Generic.List<uint>>>;

namespace Connect4AI
{
    public class BoardGenerator
    {
        public BoardDict GenerateAllBoards(IBoard initBoard, uint lookAhead, uint player)
        {
            var boards = new BoardDict();

            for (uint i = 0; i < lookAhead; i++)
            {

            }

            return boards;
        }

        /// <summary>
        /// .......
        /// .......
        /// .......
        /// .......
        /// .......
        /// .......
        /// 
        /// 
        /// .......
        /// .......
        /// .......
        /// .......
        /// ...x...
        /// ...xo..
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public IBoard BuildBoard(string bb)
        {
            var b = UnindentString(bb);
            var newBoard = new Board();
            var lines = b.Split(Environment.NewLine).Where(line => line.Length > 0).ToArray();
            if (lines.Length != 6)
            {
                throw new Exception("Invalid # of lines.");
            }

            for (uint i = 0; i < 6; i++)
            {
                var line = lines[i];

                if (line.Length != 7)
                {
                    throw new Exception("Invalid # of characters in line: " + line);
                }

                for (uint j = 0; j < line.Length; j++)
                {
                    var c = char.ToLower(line[(int)j]);

                    if (c == 'o')
                    {
                        newBoard.Set(j, i, 0);
                    }
                    if (c == 'x')
                    {
                        newBoard.Set(j, i, 1);
                    }
                }
            }

            return newBoard;
        }

        public string DisplayBoard(IBoard b)
        {
            var bb = (Board)b;
            char c;
            var result = new StringBuilder("", 48);
            for (uint row = 0; row < 6; row++)
            {
                var ln = new StringBuilder("", 7);
                for (uint col = 0; col < 7; col++)
                {
                    var player = bb.Get(col, row);
                    switch (player) {
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

        public string UnindentString(string s)
        {
            var lines = s.Split(Environment.NewLine).Where(line => line.Length > 0).ToArray();
            var result = new StringBuilder("");
            for (uint i = 0; i < lines.Length; i++)
            {
                var line = lines[i].Trim();
                result.Append(line + Environment.NewLine);
            }
            
            return result.ToString();
        }

        public BoardDict GenerateBoards(IBoard board, uint player, HashSet<List<uint>> playSequences)
        {
            var boards = new BoardDict();

            for (uint i = 0; i < 7; i++)
            {
                if (board.IsColFull(i))
                {
                    continue;
                }

                var b = new Board(board);
                var cloneSequences = new HashSet<List<uint>>(playSequences);

                if (cloneSequences.Count == 0)
                {
                    cloneSequences.Add(new List<uint>());
                }
                b.DropPiece(i, player);
                foreach (var seq in cloneSequences)
                {
                    seq.Add(player);
                }
                boards.Add(b, cloneSequences);
            }

            return boards;
        }
    }
}
