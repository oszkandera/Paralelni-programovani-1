using GameOfLife.Model;
using System;
using System.Diagnostics;

namespace GameOfLifeConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var world1 = PrepareWorld();

            Stopwatch stopwatch1 = new Stopwatch();
            stopwatch1.Start();
            for(int i = 0; i < 20; i++)
            {
                world1.UpdateWorld();
            }
            stopwatch1.Stop();


            var world2 = PrepareWorld();

            Stopwatch stopwatch2 = new Stopwatch();
            stopwatch2.Start();
            for (int i = 0; i < 20; i++)
            {
                world2.UpdateWorldSync();
            }
            stopwatch2.Stop();

            Console.WriteLine($"Time elapsed (parallel): {stopwatch1.ElapsedMilliseconds}");
            Console.WriteLine($"Time elapsed (sync): {stopwatch2.ElapsedMilliseconds}");

        }

        private static World PrepareWorld()
        {
            var world = new World(1500);

            world.Toogle(3, 3);
            world.Toogle(3, 4);
            world.Toogle(3, 5);
            world.Toogle(3, 6);
            world.Toogle(4, 3);
            world.Toogle(4, 4);
            world.Toogle(4, 6);
            world.Toogle(4, 7);
            world.Toogle(5, 6);
            world.Toogle(7, 6);
            world.Toogle(6, 6);
            world.Toogle(8, 6);
            world.Toogle(9, 6);
            world.Toogle(10, 6);
            world.Toogle(11, 6);
            world.Toogle(12, 6);
            world.Toogle(8, 8);
            world.Toogle(8, 9);
            world.Toogle(8, 10);
            world.Toogle(8, 11);
            world.Toogle(9, 11);
            world.Toogle(10, 11);
            world.Toogle(12, 11);
            world.Toogle(14, 11);
            world.Toogle(13, 11);
            world.Toogle(9, 10);
            world.Toogle(13, 20);
            world.Toogle(14, 19);
            world.Toogle(15, 18);
            world.Toogle(16, 17);
            world.Toogle(18, 17);
            world.Toogle(20, 15);
            world.Toogle(21, 14);
            world.Toogle(21, 13);
            world.Toogle(21, 12);
            world.Toogle(21, 11);
            world.Toogle(21, 10);
            world.Toogle(21, 9);
            world.Toogle(21, 8);
            world.Toogle(21, 7);
            world.Toogle(2, 7);
            world.Toogle(2, 8);
            world.Toogle(2, 9);
            world.Toogle(2, 10);
            world.Toogle(2, 11);
            world.Toogle(2, 12);
            world.Toogle(2, 13);

            return world;
        }
    }
}
