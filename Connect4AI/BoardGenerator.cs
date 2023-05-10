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
        public BoardDict GenerateBoards(IBoard initBoard, uint lookAhead, uint player)
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
        public IBoard BuildBoard(string b)
        {
            var newBoard = new Board();
            var lines = b.Split(Environment.NewLine).Where(line => line.Length > 0).ToArray();
            if (lines.Length != 6)
            {
                throw new Exception("Invalid # of lines.");
            }

            for (uint i = 0; i < 6; i++)
            {
                var line = lines[i].Trim();

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

        public IBoard DisplayBoard(string b)
        {
            var newBoard = new Board();
            return newBoard;
        }

        private BoardDict generateBoards(IBoard board, uint player, HashSet<List<uint>> playSequences)
        {
            var boards = new BoardDict();

            for (uint i = 0; i < 7; i++)
            {
                var b = new Board(board);
                if (!b.IsColFull(i))
                {
                    var cloneSequences = new HashSet<List<uint>>(playSequences);

                    b.DropPiece(i, player);
                    foreach (var seq in cloneSequences)
                    {
                        seq.Add(i);
                    }
                    boards.Add(b, cloneSequences);
                }
            }

            return boards;
        }
    }
}
