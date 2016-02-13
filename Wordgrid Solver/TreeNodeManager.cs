using System;
using System.Linq;

namespace WordgridSolver
{
    public class TreeNodeManager
    {
        public TreeNode Root { get; private set; }
        public TreeNodeManager()
        {
            Root = new TreeNode();
        }

        /// <summary>
        /// Adds the given <paramref name="word"/> to the Tree
        /// </summary>
        public void AddNode(string word)
        {
            AddNode(Root, 0, word.ToUpperInvariant());
        }

        /// <summary>
        /// Adds the given <paramref name="word"/> to the Tree
        /// </summary>
        /// <param name="node">The current <see cref="TreeNode"/></param>
        /// <param name="position">The current position within the <paramref name="word"/></param>
        /// <param name="word">The word that should be added</param>
        protected void AddNode(TreeNode node, int position, string word)
        {
            if (word.Length < position)
            {
                //Cannot process this word further, because it is not long enough for the position; this should not happen!
                throw new IndexOutOfRangeException("Position parameter is too long for the given word");
            }

            if (word.Length == position)
            {
                //We have found a word :)
                node.IsWord = true;
                return;
            }

            var nextChar = word.Substring(position, 1);
            if (node.ChildNodes.ContainsKey(nextChar))
            {
                //We already know the first characters of the word and do not have to process it any further
                AddNode(node.ChildNodes[nextChar], position + 1, word);
            }
            else
            {
                //We do not know the next character and we discover uncharted territory. Add the word.
                var newNode = new TreeNode
                {
                    Parent = node,
                    Path = word.Substring(0, position + 1)
                };
                node.ChildNodes.Add(nextChar, newNode);
                //Let's process the following content
                AddNode(newNode, position + 1, word);
            }
        }

        /// <summary>
        /// Checks if the given <paramref name="word"/> is marked as a word in the tree
        /// </summary>
        /// <param name="word">The word that should be checked for validity</param>
        /// <returns><c>true</c> if the <paramref name="word"/> is a valid word. If the <paramref name="word"/> is not valid or not present in the tree, <c>false</c> is returned</returns>
        public bool IsWord(string word)
        {
            var node = GetNode(word);
            if (node == null)
            {
                return false;
            }
            return node.IsWord;
        }

        /// <summary>
        /// Checks if the given <paramref name="path"/> can be found in the tree
        /// </summary>
        /// <param name="path">The path that should be checked</param>
        /// <returns><c>true</c> if the path exists; otherwise <c>false</c></returns>
        public bool ContainsWordBase(string path)
        {
            var node = GetNode(path);
            return node != null;
        }

        /// <summary>
        /// Returns the <see cref="TreeNode"/> that belongs to a given <paramref name="path"/>
        /// </summary>
        /// <param name="path">The path of the <see cref="TreeNode"/> that should be retrieved</param>
        /// <returns>The <see cref="TreeNode"/> if it is present in the Tree, otherwise <c>null</c></returns>
        public TreeNode GetNode(string path)
        {
            //Convert the path to a string array of single characters (which leads to the question: Why did I use a string as the key in the dictionary in the first place?!?)
            var characters = path.ToCharArray().Select(c => c.ToString()).ToArray();
            //Get the root of the Tree
            var node = Root;
            foreach (var character in characters)
            {
                //Check if we can reach the next part of the path (e.g. the next character) and set the resulting node as the new root for our subtree
                if (node.ChildNodes.ContainsKey(character))
                {
                    node = node.ChildNodes[character];
                }
                else
                {
                    //The path is not reachable in the tree
                    return null;
                }
            }
            return node;
        }
    }
}