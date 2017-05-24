using System;
using System.Collections.Generic;

namespace SnakeSharp
{
    public class FoodCreator
    {
        public readonly List<Point> FoodPoints = new List<Point>
        {
            new Point(5, 6),
            new Point(7, 7),
            new Point(3, 2),
            new Point(6, 1),
            new Point(7, 3),
            new Point(4, 5),
            new Point(2, 7),
            new Point(8, 8),
            new Point(1, 9),
            new Point(8, 1),
        };

        private int currentIndex = 0;

        public Point GetNextFoodPosition()
        {
            var newFoodPosition = FoodPoints[currentIndex % FoodPoints.Count];
            currentIndex++;

            return newFoodPosition;
        }

        public void Reset()
        {
            currentIndex = 0;
        }
    }

    public class Food
    {
        public const char Sign = 'F';

        public static Random random;

        public Point Position { get; set; }

        public Food()
        {
            //if (random == null)
            //    random = new Random(2);

            //Position = new Point(random.Next() % (GameField.FieldWidth - 2) + 1, random.Next() % (GameField.FieldHeight - 2) + 1);
        }

        public Food(Point position)
        {
            Position = position;
        }
    }
}
