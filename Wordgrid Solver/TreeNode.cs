using System.Collections.Generic;

namespace WordgridSolver
{
    public class TreeNode
    {
        public TreeNode()
        {
            ChildNodes = new Dictionary<string, TreeNode>();
        }

        /// <summary>
        /// The parent node
        /// </summary>
        public TreeNode Parent { get; set; }
        /// <summary>
        /// All available child nodes - the key of the Dictionary is the next character
        /// </summary>
        public Dictionary<string, TreeNode> ChildNodes { get; set; }

        /// <summary>
        /// Name of the word up to this node
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// If set, the current node represents a word in its own right
        /// </summary>
        public bool IsWord { get; set; }
    }
}