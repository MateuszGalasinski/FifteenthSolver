using Model;
using System.Collections.Generic;

namespace SolverConsoleApp.Models
{
    public class AppParameters
    {
        public StrategyType StrategyType { get; set; }
        public MetricType MetricType { get; set; }
        public string StartFilePath { get; set; }
        public string SolutionFilePath { get; set; }
        public string AdditionalFilePath { get; set; }
        public List<Direction> SearchOrder { get; set; }
    }
}
