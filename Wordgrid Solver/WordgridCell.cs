namespace WordgridSolver
{
    public class WordgridCell
    {
        public WordgridCell(string character)
        {
            Character = character;
        }
        public WordgridCell(WordgridCell cell)
        {
            Character = cell.Character;
            Visited = cell.Visited;
        }

        /// <summary>
        /// The character in this cell
        /// </summary>
        public string Character { get; set; }
        /// <summary>
        /// Has this cell already been visited
        /// </summary>
        public bool Visited { get; set; }
    }
}