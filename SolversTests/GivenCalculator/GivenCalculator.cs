using GameSolvers.Solvers.Metrics;

namespace SolversTests.GivenCalculator
{
    class GivenCalculator
    {
        protected IMetricCalculator Calculator;

        public void With_Calculator(string calculatorName)
        {
            switch (calculatorName)
            {
                case "Manhattan":
                    Calculator = new ManhattanMetricCalculator();
                    break;
                case "Hamming":
                    Calculator = new HammingMetricCalculator();
                    break;
            }
        }
    }
}
