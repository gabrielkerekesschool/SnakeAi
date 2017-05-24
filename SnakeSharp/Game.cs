using System;
using System.Collections.Generic;
using System.Threading;

namespace SnakeSharp
{
    public class Game
    {
        private bool directionChanged;
        private Snake.Direction newDirection;

        public Snake CurrentSnake { get; set; }
        public Food CurrentFood { get; set; }
        public GameField CurrentGameField { get; set; }
        public int Score { get; set; }

        public Timer Timer { get; set; }

        public SnakeGeneticAi SnakeAi { get; set; }

        public bool ShouldRun { get; set; }

        public int TimeAlive { get; set; }

        private FoodCreator foodCreator;

        public Game(SnakeGeneticAi snakeAi)
        {
            SnakeAi = snakeAi;
            foodCreator = new FoodCreator();

            Restart();
        }

        public void Restart()
        {
            foodCreator.Reset();

            CurrentSnake = new Snake();
            CurrentFood = new Food(foodCreator.GetNextFoodPosition());
            CurrentGameField = new GameField();

            Score = 0;
            ShouldRun = true;
            TimeAlive = 0;
        }

        public void TryPlay(List<int> directions)
        {
            while (ShouldRun)
            {
                if (directions.Count <= TimeAlive)
                {
                    ShouldRun = false;
                    continue;
                }

                CurrentSnake.MakeTurn((Snake.Turn) directions[TimeAlive]);

                CurrentSnake.Move();

                if (CurrentSnake.CheckWallCollision() || CurrentSnake.CheckSelfCollision())
                {
                    ShouldRun = false;
                }

                if (CurrentSnake.Body[0].X == CurrentFood.Position.X && CurrentSnake.Body[0].Y == CurrentFood.Position.Y)
                {
                    CurrentSnake.Grow();
                    CurrentFood = new Food(foodCreator.GetNextFoodPosition());
                    Score += 9;
                }

                TimeAlive++;
            }
        }

        public void ShowGame(List<int> directions)
        {
            while (ShouldRun)
            {
                CurrentGameField.InitFields();
                CurrentGameField.MarkFields(CurrentSnake.Body, Snake.Sign);
                CurrentGameField.MarkField(CurrentFood.Position, Food.Sign);

                Console.Clear();
                CurrentGameField.Print();
                Console.Write(Score + "\n");
                //Console.WriteLine($"Gen: {SnakeAi.GenerationCount}\tFitness:{SnakeAi.GenerationFitness}\tHighestScore:{SnakeAi.HighestScore}");
                Console.WriteLine(CurrentSnake.CurrentDirection.ToString());


                //if (directionChanged)
                //    CurrentSnake.CurrentDirection = newDirection;
                //SnakeAi.ChangeDirection();
                CurrentSnake.MakeTurn((Snake.Turn)directions[TimeAlive]);
                //SnakeAi.CalculateFitness();

                CurrentSnake.Move();

                if (CurrentSnake.CheckWallCollision() || CurrentSnake.CheckSelfCollision())
                {
                    // todo: notify snakeai and don't exit
                    //Environment.Exit(1);
                    //SnakeAi.SnakeDied();
                    //Timer.Change(Timeout.Infinite, Timeout.Infinite);
                    //Timer.Dispose();
                    //Restart();
                    ShouldRun = false;
                    //if (print)
                    //    Console.WriteLine($"Gen: {SnakeAi.GenerationCount}\tFitness:{SnakeAi.GenerationFitness}\tHighestScore:{SnakeAi.HighestScore}");

                }

                if (CurrentSnake.Body[0].X == CurrentFood.Position.X && CurrentSnake.Body[0].Y == CurrentFood.Position.Y)
                {
                    CurrentSnake.Grow();
                    CurrentFood = new Food(foodCreator.GetNextFoodPosition());
                    Score += 9;
                }

                TimeAlive++;

                Thread.Sleep(100);
            }
        }
    }
}
