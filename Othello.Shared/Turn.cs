using System;

namespace Othello.Shared
{
    public class Turn
    {
        private Piece[,] board;
        private Piece turnPiece { get; set; }
        public const int MAX_POS_X = 8;
        public const int MAX_POS_Y = 8;

        public Turn(Piece[,] board, Piece turnPiece)
        {
            this.board = board;
            this.turnPiece = turnPiece;
        }

        public int GetPossibleSetStoneCount()
        {
            int returnValue = 0;

            for (int x = 0; x <= MAX_POS_X - 1; x++)
            {
                for (int y = 0; y <= MAX_POS_Y - 1; y++)
                {
                    if (IsEffectiveJudgment(x, y) == true) returnValue++;
                }
            }

            return returnValue;
        }

        public int GetStoneCount()
        {
            int returnValue = 0;

            for (int x = 0; x <= MAX_POS_X - 1; x++)
            {
                for (int y = 0; y <= MAX_POS_Y - 1; y++)
                {
                    if (this.board[x, y] == turnPiece) returnValue++;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// 受け取った座標が置ける場所か判定
        /// </summary>
        /// <param name="x">Ｘ座標</param>
        /// <param name="y">Ｙ座標</param>
        /// <returns></returns>
        public Boolean IsEffectiveJudgment(int x, int y)
        {
            if (this.board[x, y] != Piece.None) return false;

            //８方向の判定を行う
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0) continue;

                    if (ReversalCountByOneDirection(x, y, i, j, this.turnPiece, 0) > 0) return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 受け取った座標に石を置いたときに石が裏返る数を取得する。
        /// </summary>
        /// <param name="x">Ｘ座標</param>
        /// <param name="y">Ｙ座標</param>
        /// <returns></returns>
        public int ReversalCount(int x, int y)
        {
            int returnValue = 0;
            //８方向の判定を行う
            for (int xAddValue = -1; xAddValue <= 1; xAddValue++)
            {
                for (int yAddValue = -1; yAddValue <= 1; yAddValue++)
                {
                    if (xAddValue == 0 && yAddValue == 0) continue;

                    returnValue += ReversalCountByOneDirection(x, y, xAddValue, yAddValue, this.turnPiece, 0);
                }
            }

            return returnValue;
        }

        /// <summary>
        /// 石を置いたときに１方向で石が裏返る数を取得する。
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="xAddValue"></param>
        /// <param name="yAddValue"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public int ReversalCountByOneDirection(int x, int y, int xAddValue, int yAddValue, Piece turnPiece, int count)
        {
            int checkX = x + xAddValue;
            int checkY = y + yAddValue;

            if (checkX < 0 || checkX > MAX_POS_X - 1 || checkY < 0 || checkY > MAX_POS_Y - 1)
            {
                return 0;
            }
            else if (board[checkX, checkY] == Piece.None)
            {
                return 0;
            }
            else if (board[checkX, checkY] == GetOpponentStone(turnPiece))
            {
                return ReversalCountByOneDirection(checkX, checkY, xAddValue, yAddValue, turnPiece, count + 1);
            }
            else if (board[checkX, checkY] == turnPiece)
            {
                return count;
            }

            return 0;
        }

        public Piece GetOpponentStone(Piece turnPiece)
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
    }
}
