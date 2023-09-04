using connect_4_core;
using Connect4AI;
using System.Diagnostics;


namespace connect_4
{
    enum BaseColScore : uint
    {
        Victory = 10000,
        Center = 6,          // piece center column 
        ThreeOutfOfFour = 4, // 3 pieces and 1 empty
        TwoOutOfFour = 2     // 2 pieces and 2 empty
    }

    public class Node : MiniMax.INode
    {
        Board board;

        public Node(Board board)
        {
            this.board = board;
        }

        private bool IsTerminal()
        {
            var boardIsFull = Enumerable.Range(0, 7)
            .Select(i => board.IsColFull((uint)i)).All(x => x);

            var player1Wins = board.FindWinningCols(PlayerID.One).Count > 0;
            var player2Wins = board.FindWinningCols(PlayerID.Two).Count > 0;

            return boardIsFull || player1Wins || player2Wins;
        }

        private IEnumerable<MiniMax.INode> GetChildren() {
            var children = new HashSet<Node>();

            
            for (uint i = 0; i < 7; i++)
            {
                if (board.IsColFull(i))
                {
                    continue;
                }

                var pieceCount = board.CountPieces();
                var player = pieceCount % 2 == 0 ? PlayerID.One : PlayerID.Two;

                var b = new Board(board);
                b.DropPiece(i, player);
                children.Add(new Node(b));
            }

            return children;
        }

        public bool Terminal => IsTerminal();
            
        public IEnumerable<MiniMax.INode> Children => GetChildren();

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

                if (b.Get(col, row) == b.LastPlayer) { 
                    pieces++;
                } else if (b.Get(col, row) == b.LastPlayer.Other())
                {
                    break;
                }
            }
            return pieces;
        }

        public int CalculateScore(MiniMax.Player maximizingPlayer)
        {
            var dirs = new Tuple<int, int>[]
            {
                new Tuple<int, int>( 0, 1 ),   // down
                new Tuple<int, int>( -1, 0 ),  // left
                new Tuple<int, int>( 1, 0 ),   // right
                new Tuple<int, int>( -1, -1 ), // up left
                new Tuple<int, int>( 1, -1 ),  // up right
                new Tuple<int, int>( -1, 1 ),  // down left
                new Tuple<int, int>( 1, 1 ),   // down right
            };

            var score = 0;
            // score 100/-100 for each sure victory
            var win = VictoryChecker.CheckVictory(board, board.LastPiece, board.LastPlayer);
            if (win)
            {
                if ((int)board.LastPlayer == (int)maximizingPlayer)
                {
                    return 100;
                }
                else
                {
                    return -100;
                }
            } 
            
            // if the last piece was played in the center column, add 30
            if (board.LastPiece == 3)
            {
                score += 30;
            }

            // score 10/-10 for each 3-in-a-row with empty slot in each direction (except up)
            // starting from the last played piece, go 3 steps in a direction if possible
            foreach(var dir in dirs)
            {
                var pieces = CountPieces(board, dir.Item1, dir.Item2);
                if (pieces > 2 )
                {
                    score += 10;
                }
                else if (pieces > 1)
                {
                    score += 5;
                }
            }

            return ((int)board.LastPlayer == (int)maximizingPlayer) ? score : -score; 
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
                var score = n.CalculateScore((MiniMax.Player)curPlayer);
                if (score > highestScore)
                {
                    highestScore = score;
                    bestCol = i;
                }
            }

            return bestCol;
        }


    }
}