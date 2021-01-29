using System.Threading.Tasks;

namespace PageRank
{
    public class PageRank
    {
        private readonly int _graphSize;
        private readonly int[][] _graph;

        private double[] _pageRankArray;
        public PageRank(int[][] graph)
        {
            _graphSize = graph.Length;
            _graph = graph;
            _pageRankArray = PageRankFactory.CreateDefaultPageRankArray(_graphSize);
        }

        public void CalculatePageRank()
        {
            var newPageRankArray = PageRankFactory.CreateEmptyPageRankArray(_graphSize);
            for(int x = 0; x < _graphSize; x++)
            {
                var pageRank = 0.0;
                for(int y = 0; y < _graphSize; y++)
                {
                    if(_graph[y][x] == 1)
                    {
                        pageRank += (double)_pageRankArray[y] / GetSum(y);
                    }
                }
                newPageRankArray[x] = pageRank;
            }
            _pageRankArray = newPageRankArray;
        }

        public void CalculatePageRankParallel()
        {
            var newPageRankArray = PageRankFactory.CreateEmptyPageRankArray(_graphSize);
            Parallel.For(0, _graphSize, x =>
            {
                var pageRank = 0.0;
                Parallel.For(0, _graphSize, y =>
                {
                    if (_graph[y][x] == 1)
                    {
                        pageRank += (double)_pageRankArray[y] / GetSum(y);
                    }
                });
                newPageRankArray[x] = pageRank;
            });
            _pageRankArray = newPageRankArray;
        }

        private int GetSum(int y)
        {
            var sum = 0;
            for(int x = 0; x < _graphSize; x++)
            {
                sum += _graph[y][x];
            }
            return sum;
        }
    }
}
