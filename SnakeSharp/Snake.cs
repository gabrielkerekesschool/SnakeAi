using System;
using System.Linq;

namespace SnakeSharp
{
    public class Snake
    {
        public enum Direction
        {
            Up, Right, Down, Left
        }

        public enum Turn
        {
            None, Left, Right,   
        }

        public const int InitialSize = 3;
        public const char Sign = 'O';
        
        public Direction CurrentDirection { get; set; }
        public Point[] Body { get; set; }

        public Snake()
        {
            Body = new Point[InitialSize];
            Body[0] = new Point(5, 5);
            Body[1] = new Point(5, 4);
            Body[2] = new Point(5, 3);

            CurrentDirection = Direction.Down;
        }

        public void MakeTurn(Turn turn)
        {
            if (turn == Turn.Left)
                MakeLeftTurn();
            else if (turn == Turn.Right)
                MakeRightTurn();
        }

        public void MakeLeftTurn()
        {
            switch (CurrentDirection)
            {
                case Direction.Up:
                    CurrentDirection = Direction.Left;
                    break;
                case Direction.Right:
                    CurrentDirection = Direction.Up;
                    break;
                case Direction.Down:
                    CurrentDirection = Direction.Right;
                    break;
                case Direction.Left:
                    CurrentDirection = Direction.Down;
                    break;
            }
        }

        public void MakeRightTurn()
        {
            switch (CurrentDirection)
            {
                case Direction.Up:
                    CurrentDirection = Direction.Right;
                    break;
                case Direction.Right:
                    CurrentDirection = Direction.Down;
                    break;
                case Direction.Down:
                    CurrentDirection = Direction.Left;
                    break;
                case Direction.Left:
                    CurrentDirection = Direction.Right;
                    break;
            }
        }

        public void Move()
        {
            var tmpPoint = new Point(Body[0].X, Body[0].Y);

            switch (CurrentDirection)
            {
                case Direction.Up:
                    Body[0].Y--;
                    MoveBody(tmpPoint);
                    break;
                case Direction.Right:
                    Body[0].X++;
                    MoveBody(tmpPoint);
                    break;
                case Direction.Down:
                    Body[0].Y++;
                    MoveBody(tmpPoint);
                    break;
                case Direction.Left:
                    Body[0].X--;
                    MoveBody(tmpPoint);
                    break;
            }
        }

        public void MoveBody(Point tmpPoint)
        {
            for (int i = 1; i < Body.Length; i++)
            {
                Point tmpPoint2 = new Point(Body[i].X, Body[i].Y);
                Body[i] = new Point(tmpPoint.X, tmpPoint.Y);
                tmpPoint = new Point(tmpPoint2.X, tmpPoint2.Y);
            }
        }

        public bool CheckSelfCollision()
        {
            var head = Body[0];

            return Body.Skip(1).Any(b => b.X == head.X && b.Y == head.Y);
        }

        public bool CheckWallCollision()
        {
            if (Body[0].X == 0 || Body[0].X == GameField.FieldWidth - 1 || Body[0].Y == 0 || Body[0].Y == GameField.FieldHeight - 1)
            {
                return true;
            }

            return false;
        }

        public void Grow()
        {
            Point[] tmp = Body;
            int bodyLength = Body.Length;
            Array.Resize(ref tmp, bodyLength + 1);
            Body = tmp;
            
            Body[bodyLength] = new Point(Body[bodyLength - 1].X, Body[bodyLength -1].Y);
        }
    }
}
