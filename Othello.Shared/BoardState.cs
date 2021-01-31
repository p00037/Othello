using System;

namespace Othello.Shared
{
    public class BoardState
    {
        private Piece[,] board;
        private Piece turnPiece { get; set; }
        public const int MAX_POS_X = 8;
        public const int MAX_POS_Y = 8;

        /// <summary>
        /// 盤面の初期配列をセットする。
        /// </summary>
        public BoardState()
        {
            this.board = GetInitBoard();
            this.turnPiece = Piece.Black;
        }

        public Piece[,] Board
        {
            get
            {
                return this.board;
            }
        }

        public Piece TurnPiece
        {
            get
            {
                return this.turnPiece;
            }
        }

        public int PieceCount(Piece piece)
        {
            int returnValue = 0;

            for (int x = 0; x <= MAX_POS_X - 1; x++)
            {
                for (int y = 0; y <= MAX_POS_Y - 1; y++)
                {
                    if (this.board[x, y] == piece) returnValue++;
                }
            }

            return returnValue;
        }

        public Piece[,] GetInitBoard()
        {
            Piece[,] board = new Piece[8, 8]; ;
            for (int x = 0; x <= MAX_POS_X - 1; x++)
            {
                for (int y = 0; y <= MAX_POS_Y - 1; y++)
                {
                    board[x, y] = Piece.None;
                }
            }
            board[3, 3] = Piece.White;
            board[4, 4] = Piece.White;
            board[3, 4] = Piece.Black;
            board[4, 3] = Piece.Black;

            return board;
        }

        /// <summary>
        /// 石を置いた後のボードの配列データを取得します。
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public void ExecuteTurn(BoardPoint point)
        {
            this.board[point.X, point.Y] = this.turnPiece;
            var turn = new Turn(this.board, this.turnPiece);

            //８方向の判定を行う
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0) continue;

                    int tempCount = turn.ReversalCountByOneDirection(point.X, point.Y, i, j, this.turnPiece, 0);
                    for (int k = 1; k <= tempCount; k++)
                    {
                        this.board[point.X + (i * k), point.Y + (j * k)] = this.turnPiece;
                    }
                }
            }

            ChangeTurn();
        }

        private void ChangeTurn()
        {
            this.turnPiece = OpponentStone(this.turnPiece);

            var turn = new Turn(this.board, this.turnPiece);
            if (turn.GetPossibleSetStoneCount() == 0)
            {
                this.turnPiece = OpponentStone(this.turnPiece);
            }
        }

        public Piece OpponentStone(Piece turnPiece)
        {
            if (turnPiece == Piece.Black)
            {
                return Piece.White;
            }
            else
            {
                return Piece.Black;
            }
        }

        public int GetPossibleSetStoneCount(Piece turnPiece)
        {
            var turn = new Turn(this.board, this.turnPiece);
            int returnValue = 0;

            for (int x = 0; x <= MAX_POS_X - 1; x++)
            {
                for (int y = 0; y <= MAX_POS_Y - 1; y++)
                {
                    if (turn.IsEffectiveJudgment(x, y) == true) returnValue++;
                }
            }

            return returnValue;
        }
    }
}
