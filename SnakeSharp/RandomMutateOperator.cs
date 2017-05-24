using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GAF;

namespace SnakeSharp
{
    public class RandomMutateOperator : IGeneticOperator
    {
        private Random random;
        private double mutationProbability;

        public RandomMutateOperator(double mutationProbability)
        {
            random = new Random();

            this.mutationProbability = mutationProbability;
        }

        public void Invoke(Population currentPopulation, ref Population newPopulation, FitnessFunction fitnesFunctionDelegate)
        {
            foreach (var chromosome in newPopulation.Solutions)
            {
                var mutated = false;
                foreach (var gene in chromosome.Genes)
                {
                    var shouldMutate = random.Next(0, 100) < mutationProbability * 100;

                    if (shouldMutate)
                    {
                        gene.ObjectValue = (Snake.Turn) random.Next(0, 2);
                        mutated = true;
                    }
                }

                if (mutated)
                {
                    chromosome.Evaluate(fitnesFunctionDelegate);
                }
            }
        }

        public int GetOperatorInvokedEvaluations()
        {
            throw new NotImplementedException();
        }

        public bool Enabled { get; set; }
        public bool RequiresEvaluatedPopulation { get; set; }
    }
}
