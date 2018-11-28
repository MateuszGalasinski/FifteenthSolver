using Model;
using System.Collections.Generic;

namespace SolverConsoleApp.Models
{
    public class AppParameters
    {
        public List<StrategyType> StrategyTypes { get; set; } = new List<StrategyType>();
        public List<MetricType> MetricTypes { get; set; } = new List<MetricType>();
        public string StartFilePath { get; set; }
        public string SolutionFilePath { get; set; }
        public string AdditionalFilePath { get; set; }
        public List<List<Direction>> SearchOrders { get; set; } = new List<List<Direction>>();
    }
}
