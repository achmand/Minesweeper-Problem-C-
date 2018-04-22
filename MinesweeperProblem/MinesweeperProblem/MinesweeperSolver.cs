using System;
using System.Text;

namespace MinesweeperProblem
{
    public struct Coordinate
    {
        public int X;
        public int Y;
    }

    public sealed class MinesweeperSolver
    {
        public int[][] MineGrid;
        private readonly int _rowLength;
        private readonly int _columnLength;
        private bool _hasBombCoordinates;
        private StringBuilder _stringBuilder;
        private readonly Coordinate[] _bombCoordinates;

        public MinesweeperSolver(int rowLength, int columnLength)
        {
            _rowLength = rowLength;
            _columnLength = columnLength;
            _stringBuilder = new StringBuilder();
            _hasBombCoordinates = false; // so I have to ask for coordinates

            InitializeGrid();
        }

        // bomb coordinates must be sorted if not must add a sorting method for bomb coordinates before init grid 
        public MinesweeperSolver(int rowLength, int columnLength, Coordinate[] bombCoordinates)
        {
            _rowLength = rowLength;
            _columnLength = columnLength;
            _stringBuilder = new StringBuilder();
            _hasBombCoordinates = true; // since we are getting the coordinates
            _bombCoordinates = bombCoordinates;

            InitializeGrid();
        }

        // initialize to mine grid
        private void InitializeGrid()
        {
            MineGrid = new int[_rowLength][];
            var bombCoordinatesLength = _bombCoordinates.Length;
            if (bombCoordinatesLength <= 0)
            {
                throw new Exception("Must have at least one bomb !");
            }

            var currentBombCoordinate = _bombCoordinates[0];
            var bombCounter = 0;
            for (int i = 0; i < MineGrid.Length; i++)
            {
                var columns = new int[_columnLength];
                for (int j = 0; j < columns.Length; j++)
                {
                    if (currentBombCoordinate.X == i && currentBombCoordinate.Y == j)
                    {
                        columns[j] = -1;
                        bombCounter++;
                        if (bombCounter < bombCoordinatesLength)
                        {
                            currentBombCoordinate = _bombCoordinates[bombCounter];
                        }

                        continue;
                    }

                    columns[j] = 0;
                }

                MineGrid[i] = columns;
            }
        }

        public void SolveMineSweeper()
        {
            //var clonedJaggedArray = CopyArrayBuiltIn(MineGrid);

            for (int i = 0; i < _bombCoordinates.Length; i++)
            {
                var bombCoordinate = _bombCoordinates[i];
                var coordinateX = bombCoordinate.X;
                var coordinateY = bombCoordinate.Y;

                // we must check for 8 adjacent squares
                var upperSide = coordinateX - 1;
                var bottomSide = coordinateX + 1;
                var leftSide = coordinateY - 1;
                var rightSide = coordinateY + 1;
                
                // upper 
                IncrementPointValue(upperSide, coordinateY);
                IncrementPointValue(upperSide, leftSide);
                IncrementPointValue(upperSide, rightSide);

                // bottom 
                IncrementPointValue(bottomSide, coordinateY);
                IncrementPointValue(bottomSide, leftSide);
                IncrementPointValue(bottomSide, rightSide);

                // sides
                IncrementPointValue(coordinateX, leftSide);
                IncrementPointValue(coordinateX, rightSide);
            }
        }

        private void IncrementPointValue(int x, int y)
        {
            if (x < 0 || x >= _rowLength || y < 0 || y > _columnLength)
            {
                return;
            }

            var point = MineGrid[x][y];
            if (point != -1)
            {
                MineGrid[x][y]++;
            }
        }

        public void GridMineToString()
        {
            for (int i = 0; i < MineGrid.Length; i++)
            {
                var innerArray = MineGrid[i];
                for (int a = 0; a < innerArray.Length; a++)
                {
                    var element = innerArray[a] == -1 ? "*" : innerArray[a].ToString();
                    Console.Write(element + " ");
                }

                Console.WriteLine();
            }
        }
    }
}
