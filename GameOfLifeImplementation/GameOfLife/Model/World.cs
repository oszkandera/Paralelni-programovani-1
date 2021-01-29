using System;
using System.Threading.Tasks;

namespace GameOfLife.Model
{
    public class World
    {
        public bool[,] WorldMap { get; private set; }
        public int Generation { get; set; }
        public int WorldSize { get; private set; }

        private Task<bool[,]> generationProcess { get; set; }


        public World(int worldSize)
        {
            WorldSize = worldSize;
            Generation = 0;
            WorldMap = new bool[WorldSize , WorldSize];
        }

        public void UpdateWorld()
        {
            Generation++;
            if(generationProcess != null)
            {
                while (!generationProcess.IsCompleted) ;
            }

            generationProcess = GenerateNextGeneration();
            WorldMap = generationProcess.Result;
        }

        public void UpdateWorldSync()
        {
            Generation++;
            WorldMap = GenerateNextGenerationSync();
        }

        private Task<bool[,]> GenerateNextGeneration()
        {
            return Task.Run(() =>
            {
                var nextGeneration = new bool[WorldSize, WorldSize];
                Parallel.For(0, WorldSize, row =>
                {
                    Parallel.For(0, WorldSize, col =>
                    {
                        var newStateOfCell = GetStateOfCellForNewGeneration(row, col);
                        nextGeneration[row, col] = newStateOfCell;
                    });
                });
                return nextGeneration;
            });
        }

        private bool[,] GenerateNextGenerationSync()
        {
            var nextGeneration = new bool[WorldSize, WorldSize];
            for (int row = 0; row < WorldSize; row++)
            {
                for (int col = 0; col < WorldSize; col++)
                {
                    var newStateOfCell = GetStateOfCellForNewGeneration(row, col);

                    nextGeneration[row, col] = newStateOfCell;
                }
            }

            return nextGeneration;
        }

        private bool GetStateOfCellForNewGeneration(int row, int col)
        {
            int aliveNeighboards = GetCountOfAliveNeighboards(row, col);
            bool isAlive = WorldMap[row, col];

            return (isAlive && (aliveNeighboards == 2 || aliveNeighboards == 3)) || (!isAlive && aliveNeighboards == 3);
        }

        public void Toogle(int row, int col)
        {
            if (IsOutOfRange(row, col))
            {
                throw new ArgumentException("Souřadnice jsou mimo hranice světa.");
            }

            WorldMap[col, row] = !WorldMap[col, row];
        }

        private int GetCountOfAliveNeighboards(int row, int col)
        {
            var aliveNeighboardsCount = 0;
            for (var x = row - 1; x <= row + 1; x++){
                for(int y = col - 1; y <= col + 1; y++)
                {
                    if (!IsOutOfRange(x, y) && WorldMap[x, y] && (x != row || y != col)) aliveNeighboardsCount++;
                }
            }

            return aliveNeighboardsCount;
        }

        private bool IsOutOfRange(int row, int col)
        {
            return row < 0 || row >= WorldSize || col < 0 || col >= WorldSize;
        }
    }
}
