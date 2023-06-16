using connect_4_core;
using System.Text;

using BoardDict = System.Collections.Generic.Dictionary<string, Connect4AI.SuperBoard>;
using Debug = System.Diagnostics.Debug;

namespace Connect4AI
{
    public class BoardGenerator
    {
        public BoardDict GenerateAllBoards(Board initBoard, PlaySequenceSet pss, uint lookAhead, PlayerID player)
        {
            var boards = new BoardDict();
            boards[initBoard.ToString()] = new SuperBoard(initBoard, pss);

            for (uint i = 0; i < lookAhead; i++)
            {
                // Generate boards from each of the current boards
                var newBoards = new BoardDict();
                foreach (var entry in boards)
                {
                    var key = entry.Key;
                    var superBoard = entry.Value;
                    var p = i % 2 == 0 ? player : player == PlayerID.One ? PlayerID.Two : PlayerID.One;
                    var bd = GenerateBoards(superBoard, p);

                    foreach (var e in bd)
                    {
                        var sb = e.Value;
                        if (newBoards.ContainsKey(e.Key))
                        {
                            newBoards[e.Key].PlaySequenceSet.Merge(sb.PlaySequenceSet);
                        }
                        else
                        {
                            newBoards[e.Key] = sb;
                        }
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
        public Board BuildBoard(string bb)
        {
            var b = UnindentString(bb);
            var newBoard = new Board();
            var lines = b.Split(Environment.NewLine).Where(line => line.Length > 0).ToArray();
            if (lines.Length != 6)
            {
                throw new Exception("Invalid # of lines.");
            }

            var d = new Dictionary<char, PlayerID>
            {
                {'x', PlayerID.One },
                {'o', PlayerID.Two},
                {'.', PlayerID.None },
            };

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
                    var player = d[c];
                    newBoard.Set(j, i, player);
                }
            }

            return newBoard;
        }

        public string DisplayBoard(Board b)
        {
            var bb = (Board)b;
            return bb.ToString();
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

        public BoardDict GenerateBoards(SuperBoard sb, PlayerID player)
        {
            var board = sb.Board;
            var playSequences = sb.PlaySequenceSet;
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
                boards[b.ToString()] = new SuperBoard(b, cloneSequences);
            }

            return boards;
        }
    }
}
