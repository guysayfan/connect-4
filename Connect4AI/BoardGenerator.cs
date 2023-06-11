using connect_4_core;
using System.Text;

using BoardDict = System.Collections.Generic.Dictionary<connect_4_core.IBoard, Connect4AI.PlaySequenceSet>;
using Debug = System.Diagnostics.Debug;

namespace Connect4AI
{
    public class BoardGenerator
    {
        public BoardDict GenerateAllBoards(IBoard initBoard, PlaySequenceSet pss, uint lookAhead, uint player)
        {
            var boards = new BoardDict();
            boards[initBoard] = pss;

            for (uint i = 0; i < lookAhead; i++)
            {
                // Generate boards from each of the current boards
                var newBoards = new BoardDict();
                foreach (var entry in boards)
                {
                    var b = entry.Key;
                    var ps = entry.Value;
                    var p = i % 2 == 0 ? player : 1 - player;
                    var bd = GenerateBoards(b, p, ps);

                    foreach (var e in bd)
                    {
                        var bb = e.Key;
                        var sq = e.Value;

                        if (newBoards.ContainsKey(bb))
                        {
                            newBoards[bb].Merge(sq);
                        }
                        else
                        {
                            newBoards[bb] = sq;
                        }
                        Debug.WriteLine(sq);
                    }

                    boards = newBoards;
                }
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

                    if (c == 'o' || c == '0')
                    {
                        newBoard.Set(j, i, 0);
                    }
                    if (c == 'x' || c == '1')
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

        public BoardDict GenerateBoards(IBoard board, uint player, PlaySequenceSet playSequences)
        {
            Debug.WriteLine($"original: {playSequences}");
            var boards = new BoardDict();

            for (uint i = 0; i < 7; i++)
            {
                if (board.IsColFull(i))
                {
                    continue;
                }

                var b = new Board(board);
                var cloneSequences = new PlaySequenceSet(playSequences);
                if (cloneSequences.Count == 0)
                {
                    cloneSequences.Add(new PlaySequence());
                }
                b.DropPiece(i, player);
                foreach (var seq in cloneSequences)
                {
                    Debug.WriteLine($"before: {seq}");
                    seq.Add(i);
                    Debug.WriteLine($"after: {seq}");
                }
                boards.Add(b, new PlaySequenceSet(cloneSequences));
            }

            return boards;
        }
    }
}
