using System.IO;

namespace WordgridSolver
{
    public class FileLoader
    {
        public TreeNodeManager LoadDictionary(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException($"File \"{fileName}\" could not be found");
            }

            var words = new TreeNodeManager();
            using (var fs = File.OpenText(fileName))
            {
                while (!fs.EndOfStream)
                {
                    var line = fs.ReadLine();
                    if (line == null || line.StartsWith("#"))
                    {
                        continue;
                    }

                    words.AddNode(line);
                }
            }
            return words;
        }
    }
}