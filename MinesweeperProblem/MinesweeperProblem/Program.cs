using System;

namespace MinesweeperProblem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("MINESWEEPER SOLVER");

            // setting a mine sweeper grid as a 4 x 4 
            var mineSweeperSolver = new MinesweeperSolver(4, 4, new[] { new Coordinate { X = 0, Y = 0 }, new Coordinate { X = 2, Y = 1 } });
            mineSweeperSolver.SolveMineSweeper();
            mineSweeperSolver.GridMineToString();
            Console.ReadLine();
        }
    }
}
