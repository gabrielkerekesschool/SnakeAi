using System;

namespace SnakeSharp
{
    public class GameField
    {
        public const int FIELD_SIZE = 15;
        public const int FieldWidth = 20;
        public const int FieldHeight = 15;

        public char[,] Fields { get; set; }

        public GameField()
        {
            InitFields();
        }

        public void InitFields()
        {
            Fields = new char[FieldHeight, FieldWidth];

            for (int row = 0; row < FieldHeight; row++)
            {
                for (int column = 0; column < FieldWidth; column++)
                {
                    if (row == 0 || row == FieldHeight - 1 || column == 0 || column == FieldWidth - 1)
                    {
                        Fields[row, column] = '#';
                    }
                    else
                    {
                        Fields[row, column] = ' ';
                    }
                }
            }
        }

        public void Print()
        {
            Console.WriteLine();
            Console.WriteLine();

            for (int row = 0; row < FieldHeight; row++)
            {
                Console.Write("          ");
                for (int column = 0; column < FieldWidth; column++)
                {
                    Console.Write(Fields[row, column]);
                }
                Console.Write("\n");
            }
        }

        public void MarkField(Point point, char sign)
        {
            Fields[point.Y, point.X] = sign;
        }

        public void MarkFields(Point[] points, char sign)
        {
            foreach (Point point in points)
            {
                MarkField(point, sign);
            }
        }
    }
}
