using connect_4_core;


namespace Connect4AI
{
    public class NormalAIPlayer : IPlayer
    {
        PlayerID aiPlayer;
        PlayerID human;
        public NormalAIPlayer(PlayerID player)
        {
            aiPlayer = player;
            human = aiPlayer.Other();
        }

        Random rnd = new Random();
        public uint Play(Board board)
        {
            Thread.Sleep(1000);

            // Check if AI wins
            var cols = FindWinningCols(board, aiPlayer);
            if (cols.Count > 0) {
                return cols.First();
            }

            // Check if human wins
            cols = FindWinningCols(board, human);
            if (cols.Count > 0)
            {
                return cols.First();
            }

            uint col = (uint)rnd.Next(7);
            while (board.IsColFull(col))
            {
                col = (uint)rnd.Next(7);
            }
            return col;
        }

        private HashSet<uint> FindWinningCols(Board board, PlayerID player)
        {
            var cols = new HashSet<uint>();
            for (uint i = 0; i < 7; i++)
            {
                //check if column is full
                if (board.IsColFull(i)) {
                    continue;
                }

                //clone board and drop piece in current col
                var b = new Board(board);
                b.DropPiece(i, player);

                //if top row is replaced with ai piece checks if the ai will win
                if (VictoryChecker.CheckVictory(b, i, player))
                {
                    cols.Add(i);
                }
            }
            return cols;
        }
    }
}