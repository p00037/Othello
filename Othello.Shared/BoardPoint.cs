using System;

namespace Othello.Shared
{
    public class BoardPoint
    {
        public BoardPoint(int x , int y)
        {
            this.X = x;
            this.Y = y;
        }

        public int X { get; private set; }
        public int Y{ get; private set; }
    }
}
