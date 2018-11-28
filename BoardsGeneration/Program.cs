using System.Linq;

namespace BoardsGeneration
{
    class Program
    {
        static void Main(string[] args)
        {
            Generator generator = new Generator();
            Writer writer = new Writer();
            writer.SaveBoards(generator.GeneratedBoards.ToList());
        }
    }
}
