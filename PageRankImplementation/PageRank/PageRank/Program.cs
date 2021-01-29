using System.Diagnostics;

namespace PageRank
{
    class Program
    {
        private static string path = "web-NotreDame.edges";
        static void Main(string[] args)
        {
            var loader = new Loader();

            var graph = loader.LoadGraph(path, " ");

            var pageRank1 = new PageRank(graph);
            var pageRank2 = new PageRank(graph);

            Stopwatch stopwatch1 = new Stopwatch();
            Stopwatch stopwatch2 = new Stopwatch();


            stopwatch1.Start();
            pageRank1.CalculatePageRankParallel();
            stopwatch1.Stop();


            stopwatch2.Start();
            pageRank2.CalculatePageRank();
            stopwatch2.Stop();

            System.Console.WriteLine($"PageRank time elapsed (sync): {stopwatch2.ElapsedMilliseconds}");
            System.Console.WriteLine($"PageRank time elapsed (parallel): {stopwatch1.ElapsedMilliseconds}");
        }
    }
}
