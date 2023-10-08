using connect_4_core;
using MiniMax;
using System.Diagnostics;
using System.Text;

namespace Connect4AI
{
    public class Node : Board, MiniMax.INode
    {
        public Node(Board board) : base(board)
        {
        }

        private bool IsTerminal()
        {
            var boardIsFull = Enumerable.Range(0, 7)
            .Select(i => IsColFull((uint)i)).All(x => x);

            //var player1Wins = this.FindWinningCols(PlayerID.One).Count > 0;
            var player1Wins = this.CalculateScore(Player.One) >= 10000;
            var player2Wins = this.CalculateScore(Player.Two) >= 10000;

            return boardIsFull || player1Wins || player2Wins;
        }

        private IEnumerable<MiniMax.INode> GetChildren()
        {
            var children = new HashSet<Node>();


            for (uint i = 0; i < 7; i++)
            {
                if (IsColFull(i))
                {
                    continue;
                }

                var pieceCount = CountPieces();
                var player = pieceCount % 2 == 0 ? PlayerID.One : PlayerID.Two;

                var b = new Board(this);
                b.DropPiece(i, player);
                children.Add(new Node(b));
            }

            return children;
        }

        public bool Terminal => IsTerminal();

        public IEnumerable<MiniMax.INode> Children => GetChildren();

        private int CalculatePlayerScore(Board b, int x, int y, int dx, int dy, PlayerID player)
        {
            var score = 0;

            // At most 4 windows
            for (uint i = 0; i < 4; i++)
            {
                var col = (int)(x + i * dx);
                var row = (int)(y + i * dy);

                if ((row > 2 && dy == 1) || (row < 3 && dy == -1))
                {
                    continue;
                }

                if ((col > 3 && dx == 1) || (col < 3 && dx == -1))
                {
                    continue;
                }

                var pieces = CountPiecesFromPoint(b, col, row, dx, dy, player);
                switch (pieces)
                {
                    case 2:
                        score += 10;
                        break;
                    case 3:
                        score += 20;
                        break;
                    case 4:
                        score += 1000;
                        break;
                    default:
                        break;
                }
            }
            return score;
        }

        private uint CountPiecesFromPoint(Board b, int col, int row, int dx, int dy, PlayerID player)
        {
            uint pieces = 0;
            for (uint i = 0; i < 4; i++)
            {
                var px = col + i * dx;
                var py = row + i * dy;

                var p = PlayerID.None;
                try
                {
                    p = b.Get((uint)px, (uint)py);
                }
                catch
                {
                    p = b.Get((uint)px, (uint)py);
                }

                if (p == player)
                {
                    pieces++;
                }
                else if (p == player.Other())
                {
                    return 0; // there is a piece of the other player in the window
                }
            }
            return pieces;
        }

        private uint CountPieces(Board b, int dx, int dy)
        {
            uint pieces = 1;
            var col = b.LastPiece;
            var row = b.FindTopRow(col);
            var topRow = row == Board.INVALID_ROW;
            if (topRow)
            {
                row = 0;
            }
            for (uint i = 0; i < 3; i++)
            {
                if (dx < 0)
                {
                    if (col == 0)
                    {
                        continue;
                    }
                    col--;
                }
                else if (dx > 0)
                {
                    if (col == 6)
                    {
                        continue;
                    }
                    col++;
                }
                if (dy < 0 && !topRow)
                {
                    if (row == 0)
                    {
                        continue;
                    }
                    row--;
                }
                else if (dy > 0)
                {
                    if (row == 5)
                    {
                        continue;
                    }
                    row++;
                }

                if (b.Get(col, row) == b.LastPlayer)
                {
                    pieces++;
                }
                else if (b.Get(col, row) == b.LastPlayer.Other())
                {
                    break;
                }
            }
            return pieces;
        }

        public int CalculateScore(MiniMax.Player maximizingPlayer)
        {
            var score = 0;
            // score 1000/-1000 for each sure victory
            var win = VictoryChecker.CheckVictory(this, LastPiece, LastPlayer);
            if (win)
            {
                if ((int)LastPlayer == (int)maximizingPlayer)
                {
                    return 1000;
                }
                else
                {
                    return -1000;
                }
            }

            var player = (PlayerID)maximizingPlayer;
            // calculate row scores
            for (var row = 0; row < 6; row++)
            {
                score += CalculatePlayerScore(this, 0, row, 1, 0, player);
            }

            // calculate col scores
            for (var col = 0; col < 7; col++)
            {
                score += CalculatePlayerScore(this, col, 0, 0, 1, player);
            }

            // calculate diag scores
            foreach (var col in Enumerable.Range(0, 3))
            {
                // descending diag (starting from top row)
                score += CalculatePlayerScore(this, col, 0, 1, 1, player);
                // ascending diag (starting from bottom row)
                score += CalculatePlayerScore(this, col, 6, -1, 1, player);
            }
            foreach (var row in Enumerable.Range(1, 4))
            {
                if (row < 3)
                {
                    // descending diag (starting from leftmost col)
                    score += CalculatePlayerScore(this, 0, row, 1, 1, player);
                }
                else
                {
                    // ascending diag (starting from leftmost col)
                    score += CalculatePlayerScore(this, 0, row, -1, 1, player);
                }
            }

            // add 30 for each piece in the center column            
            for (uint row = 0; row < 6; row++)
            {
                if (Get(3, row) == player)
                {
                    score += 30;
                }
            }

            return score;
        }
    }


    public record ScoredBoard(Board Board, int Lookahead, int Score);


    public class MiniMaxDebugger : IMiniMaxEvents
    {
        int lookahead;
        public MiniMaxDebugger(int lookahead) 
        { 
            this.lookahead = lookahead;
        }
        Dictionary<int, List<ScoredBoard>> scoredBoards = new Dictionary<int, List<ScoredBoard>>();
        public void OnNodeScore(INode n, int score, int index)
        {
            var b = (Board)n;
            var scoredBoard = new ScoredBoard(b, index, score);
            // make index ascending from zero
            index = lookahead - index;
            if (!scoredBoards.ContainsKey(index)) {
                scoredBoards[index] = new List<ScoredBoard>();
            }
            scoredBoards[index].Add(scoredBoard);
        }

        private void dumpBoard(StreamWriter s, ScoredBoard b)
        {
            var x = "\u25CF"; // black circle
            var o = "\u25CB"; // white circle

            var p = b.Board.LastPlayer == PlayerID.One ? x : o;

            var header = $"score: {b.Score}, player: {p}, lookahead: {lookahead - b.Lookahead}";
            s.WriteLine(header);
            var data = b.Board.ToString();
            data = data.Replace("x", x); // black circle
            data = data.Replace("o", o); // white circle
            
            s.WriteLine(data);
        }
        public void Dump(string filename)
        {
            FileStream stream = new FileStream(filename, FileMode.Create);
            using (StreamWriter s = new StreamWriter(stream, Encoding.UTF8))
            {
                for (var i = 0; i < scoredBoards.Count; i++)
                {
                    s.WriteLine($"=== lookahead {i} ===");
                    var a = scoredBoards[i];
                    for (var j = 0; j < a.Count; j++)
                    {
                        var b = a[j];
                        dumpBoard(s, b);
                    }
                }
            }            
        }
    }

    public class MiniMaxAIPlayer : IPlayer
    {
        PlayerID aiPlayer;
        PlayerID human;
        uint lookAhead;
        


        public MiniMaxAIPlayer(PlayerID player, uint lookAhead)
        {
            aiPlayer = player;
            human = aiPlayer.Other();
            this.lookAhead = lookAhead;
        }

        Random rnd = new Random();

        public uint Play(Board board)
        {
            var debugger = new MiniMaxDebugger((int)lookAhead);
            try
            {
                // Store all the boards that were scoreed during this play                
                Thread.Sleep(1000);

                // Check if AI wins
                var cols = board.FindWinningCols(aiPlayer);
                if (cols.Count > 0)
                {
                    return cols.First();
                }

                // Check if human wins
                cols = board.FindWinningCols(human);
                if (cols.Count > 0)
                {
                    return cols.First();
                }


                var highestScore = 0;
                uint bestCol = 0;

                // Use MiniMax algorithm to find best next move
                for (uint i = 0; i < 7; i++)
                {
                    var b = new Board(board);
                    if (b.IsColFull(i))
                    {
                        continue;
                    }
                    var curPlayer = b.LastPlayer.Other();
                    b.DropPiece(i, curPlayer);
                    var n = new Node(b);
                    var score = MiniAax.MiniMax(n, (int)lookAhead, -10000, 10000, (MiniMax.Player)curPlayer, debugger);
                    if (score > highestScore)
                    {
                        highestScore = score;
                        bestCol = i;
                    }
                }                
                return bestCol;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }
            finally
            {
                var filename = "minimax-debugger.txt";
                debugger.Dump(filename);
            }
        }
    }
}