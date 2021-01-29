using System;
using System.IO;
using System.Linq;

namespace PageRank
{
    public class Loader
    {
        public int[][] LoadGraph(string path, string delimiter)
        {
            var data = File.ReadAllLines(path).Where(x => !x.StartsWith("%")).ToArray();
            var numberOfNodes = GetNumberOfNodes(data, delimiter);
            var graph = InitGraph(numberOfNodes);

            for(int i = 0; i < data.Length; i++)
            {
                var (nodeX, nodeY) = GetNodesFromRow(data[i], delimiter);

                graph[nodeX - 1][nodeY - 1] = 1;
            }


            return graph;
        }

        private int[][] InitGraph(int numberOfNodes)
        {
            var graph = new int[numberOfNodes][];

            for(int i = 0; i < numberOfNodes; i++)
            {
                graph[i] = new int[numberOfNodes];
                for(int y = 0; y < numberOfNodes; y++)
                {
                    graph[i][y] = 0;
                }
            }

            return graph;
        }

        private int GetNumberOfNodes(string[] data, string delimiter)
        {
            var max = 0;
            for(int i = 0; i < data.Length; i++)
            {
                var (nodeX, nodeY) = GetNodesFromRow(data[i], delimiter);
                
                if(nodeX > max) max = nodeX;
                if(nodeY > max) max = nodeY;
            }

            return max;
        }

        private (int, int) GetNodesFromRow(string row, string delimiter)
        {
            var splittedRow = row.Split(delimiter);
            return (Convert.ToInt32(splittedRow[0]), Convert.ToInt32(splittedRow[1]));
        }
    }
}
