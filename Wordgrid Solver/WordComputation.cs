using System.Collections.Generic;

namespace WordgridSolver
{
    public class WordComputation
    {
        protected string WordMatrix { get; private set; }
        protected TreeNodeManager Dictionary { get; private set; }
        protected Queue<WordgridGrid> Grids { get; set; }

        public WordComputation(string wordMatrix, TreeNodeManager dictionary)
        {
            WordMatrix = wordMatrix;
            Dictionary = dictionary;
        }

        /// <summary>
        /// Gets possible word in the Word Matrix of this instace
        /// </summary>
        /// <returns>Words found in the grid that are also in the dictionary</returns>
        public IEnumerable<string> GetWords()
        {
            //Stack that will contain our grids that we have to check
            Grids = new Queue<WordgridGrid>();

            //Create the starting positions - each cell might be the start of the word and therefore we need to create 16 starting positions
            for (var i = 0; i < 4; i++)
            {
                for (var j = 0; j < 4; j++)
                {
                    var grid = new WordgridGrid(WordMatrix, i, j);
                    Grids.Enqueue(grid);
                }
            }

            //Save the already found words, so that words aren't displayed multiple times if they can be created using different paths
            var foundWords = new HashSet<string>();

            //Iterate as long as we have grids in our stack
            while (Grids.Count > 0)
            {
                //Get the first grid
                var grid = Grids.Dequeue();

                var word = grid.Path;
                if (word.Length > 2)
                {
                    //Check if the word is not already found and not in the dictionary
                    if (!foundWords.Contains(word) && Dictionary.IsWord(word))
                    {
                        yield return word;
                        foundWords.Add(word);
                    }
                }

                //Add possible grids to our stack
                foreach (var nextGrid in grid.GetNextGrids())
                {
                    //Add the grid only if the path of the grid is in the dictionary tree. Otherwise, the word and all words that start with this word are not on the dictionary and we can ignore them.
                    if (Dictionary.ContainsWordBase(nextGrid.Path))
                    {
                        Grids.Enqueue(nextGrid);
                    }
                }
            }
        }

    }
}