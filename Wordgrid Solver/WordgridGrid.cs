using System;
using System.Collections.Generic;

namespace WordgridSolver
{
    public class WordgridGrid
    {
        /// <summary>
        /// The matrix that determines the current grid state (e.g. characters in each cell and if a cell has already been visited)
        /// </summary>
        public WordgridCell[,] WordMatrix { get; private set; }
        /// <summary>
        /// The current position on the X-axis
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// The current position on the Y-axis
        /// </summary>
        public int Y { get; set; }
        /// <summary>
        /// The path that has been taken to reach the current state
        /// </summary>
        public string Path { get; set; }

        public WordgridGrid(string wordMatrix, int startX, int startY)
        {
            X = startX;
            Y = startY;
            WordMatrix = new WordgridCell[4, 4];

            for (var i = 0; i < 4; i++)
            {
                for (var j = 0; j < 4; j++)
                {
                    WordMatrix[i, j] = new WordgridCell(wordMatrix.Substring(i * 4 + j, 1));
                }
            }

            Path = GetCurrentChar();
            GetCurrentCell().Visited = true;
        }

        /// <summary>
        /// Creates a new <see cref="WordgridGrid"/> based on an exisiting <paramref name="grid"/> but with a new position
        /// </summary>
        /// <param name="grid">The existing grid that is the base for the new <see cref="WordgridGrid"/></param>
        /// <param name="startX">The new start position on the X-Axis</param>
        /// <param name="startY">The new start position on the Y-Axis</param>
        /// <remarks>The <paramref name="startX"/> and <paramref name="startY"/> positions must be reachable from the exisiting <paramref name="grid"/>, because the new <see cref="Path"/> is computed based on the existing <see cref="Path"/> of the given <paramref name="grid"/>. It is not checked if this requirement is met!</remarks>
        public WordgridGrid(WordgridGrid grid, int startX, int startY)
        {
            X = startX;
            Y = startY;
            WordMatrix = new WordgridCell[4, 4];
            for (var i = 0; i < 4; i++)
            {
                for (var j = 0; j < 4; j++)
                {
                    var cell = grid.WordMatrix[i, j];
                    WordMatrix[i, j] = new WordgridCell(cell);
                }
            }
            Path = grid.Path + GetCurrentChar();
            GetCurrentCell().Visited = true;
        }

        /// <summary>
        /// Gets the current cell based on the <see cref="X"/> and <see cref="Y"/> position
        /// </summary>
        /// <returns></returns>
        public WordgridCell GetCurrentCell()
        {
            return WordMatrix[X, Y];
        }

        /// <summary>
        /// Gets the content of the current cell
        /// </summary>
        public string GetCurrentChar()
        {
            return GetCurrentCell().Character;
        }

        /// <summary>
        /// Computes the next grids (aka next paths) that can be reached from the current state of the grid
        /// </summary>
        /// <returns>An enumeration of possible grids. If no grids are possible, an empty enumeration is returned.</returns>
        public IEnumerable<WordgridGrid> GetNextGrids()
        {
            var grids = new List<WordgridGrid>();

            var addToGrid = new Action<WordgridGrid>(grid =>
            {
                if (grid != null)
                {
                    grids.Add(grid);
                }
            });

            addToGrid(GetNextGrid(X, Y - 1));
            addToGrid(GetNextGrid(X, Y + 1));
            addToGrid(GetNextGrid(X + 1, Y));
            addToGrid(GetNextGrid(X + 1, Y - 1));
            addToGrid(GetNextGrid(X + 1, Y + 1));
            addToGrid(GetNextGrid(X - 1, Y));
            addToGrid(GetNextGrid(X - 1, Y - 1));
            addToGrid(GetNextGrid(X - 1, Y + 1));

            return grids;
        }

        /// <summary>
        /// Gets a <see cref="WordgridGrid"/> with the given parameters. If the parameters are illegal (<paramref name="x"/> or <paramref name="y"/> are out of the grid, <c>null</c> is returned)
        /// </summary>
        /// <param name="x">The new X-Position</param>
        /// <param name="y">The new Y-Position</param>
        /// <returns>The new <see cref="WordgridGrid"/> based on the given parameters</returns>
        private WordgridGrid GetNextGrid(int x, int y)
        {
            if (x < 0 || x > 3 || y < 0 || y > 3)
            {
                //Out of bounds
                return null;
            }

            if (WordMatrix[x, y].Visited)
            {
                //Already visited
                return null;
            }

            var grid = new WordgridGrid(this, x, y);
            return grid;
        }
    }
}