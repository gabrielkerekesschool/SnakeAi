using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GAF;
using GAF.Extensions;
using GAF.Operators;
using Math = System.Math;

namespace SnakeSharp
{
    public class SnakeGeneticAi
    {
        public List<int> DirectionSequence { get; set; }

        public List<List<int>> GenerationsDirectionsSequences { get; set; }

        public List<double> GenerationsFitnesses { get; set; }

        public int GenerationCount { get; set; }

        public double GenerationFitness { get; set; }

        public Game CurrentGame { get; set; }

        public Population Population { get; set; }
        public Elite Elite { get; set; }
        public Crossover Crossover { get; set; }
        public SwapMutate SwapMutate { get; set; }
        public GeneticAlgorithm GeneticAlgorithm { get; set; }

        public Stopwatch Sw { get; set; }

        public readonly int NumOfPopulations = 500;
        public readonly int NumOfGenes = 20;

        public readonly int DefaultEliteCount = 2;
        public readonly double DefaultCrossoverProbability = 0.8;
        public readonly double DefaultSwapProbability = 0.5;

        public readonly int NumOfGenerations = 10000;

        public SnakeGeneticAi()
        {
            DirectionSequence = new List<int>();
            GenerationsDirectionsSequences = new List<List<int>>();
            GenerationsFitnesses = new List<double>();

            GenerationCount = 0;
            GenerationFitness = 0;
            HighestScore = 0;

            Sw = new Stopwatch();

            CurrentGame = new Game(this);
            //CurrentGame.Play();

            Population = new Population();
            for (var p = 0; p < NumOfPopulations; p++)
            {
                var chromosome = new Chromosome();
                for (var g = 0; g < NumOfGenes; g++)
                {
                    chromosome.Genes.Add(new Gene(g % 3));
                }
                chromosome.Genes.ShuffleFast();
                Population.Solutions.Add(chromosome);
            }

            //create the elite operator
            Elite = new Elite(DefaultEliteCount);

            //create the crossover operator
            Crossover = new Crossover(DefaultCrossoverProbability, true, 
                CrossoverType.SinglePoint, ReplacementMethod.GenerationalReplacement);

            //create the mutation operator
            SwapMutate = new SwapMutate(DefaultSwapProbability);

            var randomMutateOperator = new RandomMutateOperator(0.2);

            //var randomReplace = new RandomReplace(10, true);

            //create the GA
            GeneticAlgorithm = new GeneticAlgorithm(Population, CalculateFitness);

            //hook up to some useful events
            GeneticAlgorithm.OnGenerationComplete += ga_OnGenerationComplete;
            GeneticAlgorithm.OnRunComplete += ga_OnRunComplete;

            //add the operators
            GeneticAlgorithm.Operators.Add(Elite);
            GeneticAlgorithm.Operators.Add(Crossover);
            GeneticAlgorithm.Operators.Add(SwapMutate);
            GeneticAlgorithm.Operators.Add(randomMutateOperator);
            //GeneticAlgorithm.Operators.Add(randomReplace);

            Sw.Start();

            //run the GA
            GeneticAlgorithm.Run(Terminate);
        }

        private bool Terminate(Population population, int currentgeneration, long currentevaluation)
        {
            return currentgeneration > NumOfGenerations;
        }

        private void ga_OnRunComplete(object sender, GaEventArgs e)
        {
            var fittest = e.Population.GetTop(1)[0];

            CurrentGame.Restart();
            CurrentGame.ShowGame(fittest.Genes.Select(g => (int)g.RealValue).ToList());
            //foreach (var gene in fittest.Genes)
            //{
            //    Console.WriteLine(_cities[(int)gene.RealValue].Name);
            //}

            Console.WriteLine(string.Join(",", fittest.Genes.Take(CurrentGame.TimeAlive).Select(g => (int)g.RealValue)));
        }

        private void ga_OnGenerationComplete(object sender, GaEventArgs e)
        {
            //if (e.Generation % 50 == 0)
            //{
            //    SwapMutate.MutationProbability = 0.9;
            //}
            //else
            //{
            //    SwapMutate.MutationProbability = DefaultSwapProbability;
            //}

            var fittest = e.Population.GetTop(1)[0];

            CurrentGame.Restart();
            CurrentGame.TryPlay(fittest.Genes.Select(g => (int) g.RealValue).ToList());

            GenerationCount = e.Generation;
            GenerationFitness = fittest.Fitness;

            Console.WriteLine("Generation: {0}, Fitness: {1}, HighestScore: {2}, Time: {3}", e.Generation, fittest.Fitness, HighestScore, Sw.Elapsed);
            Console.WriteLine(string.Join(",", fittest.Genes.Take(CurrentGame.TimeAlive).Select(g => (int)g.RealValue)));
            Console.WriteLine("Average fitness: " + e.Population.AverageFitness);
            Console.WriteLine("NumOfGenes: " + fittest.Genes.Count);

            if (fittest.Genes.Count - CurrentGame.TimeAlive < 50)
            {
                var chromosome = new Chromosome();
                for (var g = 0; g < NumOfGenes; g++)
                {
                    chromosome.Genes.Add(new Gene(g % 4));
                }
                chromosome.Genes.ShuffleFast();

                foreach (var c in e.Population.Solutions)
                {
                    c.AddRange(chromosome.Genes);
                }
            }

        }


        public void ChangeDirection()
        {
            var newTurn = (Snake.Turn)new Random().Next(0, 3);
            DirectionSequence.Add((int) newTurn);

            CurrentGame.CurrentSnake.MakeTurn(newTurn);
        }

        public int HighestScore { get; set; }

        public double CalculateFitness(Chromosome chromosome)
        {
            //var distanceFromFood = CalculateFoodDistanceFromSnake();

            CurrentGame.Restart();
            CurrentGame.TryPlay(chromosome.Genes.Select(g => (int)g.RealValue).ToList());

            if (CurrentGame.Score > HighestScore)
            {
                HighestScore = CurrentGame.Score;
            }

            return /*-distanceFromFood + */Math.Max(0, (double)Math.Min(1000, CurrentGame.Score * 5 /*- (double)CurrentGame.TimeAlive / 10*/) /1000);
        }

        private double CalculateFoodDistanceFromSnake()
        {
            var foodPosition = CurrentGame.CurrentFood.Position;
            var snakePosition = CurrentGame.CurrentSnake.Body[0];

            return foodPosition.CalculateDistanceFrom(snakePosition);
        }

        public void SnakeDied()
        {
            //GenerationsDirectionsSequences.Add(DirectionSequence);
            //GenerationCount++;
            //GenerationsFitnesses.Add(CalculateFitness());
        }
    }
}

