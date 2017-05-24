using System;
using System.Collections.Generic;

namespace SnakeSharp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var ai = new SnakeGeneticAi();

            //var game = new Game(ai);
            //Console.ReadLine();
            //var random = new Random();
            //var max = 0;
            //for (int j = 0; j < 1000000; j++)
            //{
            //    var list = new List<int>();
            //    for (int i = 0; i < 500; i++)
            //    {
            //        list.Add(random.Next(0, 2));
            //    }

            //    game.TryPlay(list);

            //    if (game.Score > max)
            //    {
            //        max = game.Score;
            //    }

            //    if (j % 10000 == 0)
            //        Console.WriteLine(max);
            //}
            //Console.WriteLine(max);

            //game.ShowGame(new List<int> { 3, 1, 2, 1, 1, 0, 0, 3, 0, 1, 0, 0, 3, 1, 1, 0, 1, 2, 1, 2, 2, 3, 2, 1, 3, 2, 0, 1, 3, 2, 0, 1, 0, 3, 1, 0, 0, 3, 3, 3, 1, 3, 2, 1, 3, 0, 1, 0, 0, 3, 1, 3, 3, 2, 3, 1, 2, 0, 1, 3, 1, 2, 1, 3, 1, 2, 0, 2, 1, 1, 0, 3, 3, 2, 1, 0, 3, 0, 0, 0, 0, 1, 3, 0, 1, 1, 2, 0, 3, 2, 1, 1, 2, 0, 1, 1, 2, 1, 0, 3, 3, 3, 2, 1, 1, 3, 3, 1, 2, 0, 0, 0, 1, 0, 0, 1, 3, 0, 2, 1, 1, 0, 3, 3, 3, 1, 0, 3, 1, 1, 0, 2, 2, 0, 1, 0, 1, 3, 3, 2, 1, 1, 3, 3, 2, 0, 3, 1, 3, 1, 0, 3, 0, 3, 3, 3, 1, 3, 3, 3, 1, 2, 0, 2, 0, 1, 1, 3, 0, 0, 3, 3, 1, 3, 0, 3, 2, 0, 1, 3, 3, 1, 1, 2, 3, 3, 3, 1, 1, 2, 3, 0, 0, 3, 2, 0, 0, 0, 3, 1, 2 });
        }
    }
}
