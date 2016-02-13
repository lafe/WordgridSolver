using System.Collections.Generic;

namespace WordgridSolver
{
    public class WordComputation
    {
        protected string WordMatrix { get; private set; }
        protected TreeNodeManager Dictionary { get; private set; }
        protected List<WordgridGrid> Grids { get; set; }

        public WordComputation(string wordMatrix, TreeNodeManager dictionary)
        {
            WordMatrix = wordMatrix;
            Dictionary = dictionary;
        }

        public IEnumerable<string> GetWords()
        {
            Grids = new List<WordgridGrid>();
            for (var i = 0; i < 4; i++)
            {
                for (var j = 0; j < 4; j++)
                {
                    var grid = new WordgridGrid(WordMatrix, i, j);
                    Grids.Add(grid);
                }
            }


            var foundWords = new List<string>();

            while (Grids.Count > 0)
            {
                var grid = Grids[0];
                Grids.RemoveAt(0);

                var word = grid.Path;
                if (word.Length > 2)
                {
                    if (!foundWords.Contains(word) && Dictionary.IsWord(word))
                    {
                        yield return word;
                        foundWords.Add(word);
                    }
                }

                foreach (var nextGrid in grid.GetNextGrids())
                {
                    if (Dictionary.ContainsWordBase(nextGrid.Path))
                    {
                        Grids.Add(nextGrid);
                    }
                }
            }
        }

    }
}