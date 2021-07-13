using System.Collections.Generic;

namespace Katas
{
    public abstract class LoopDetector
    {
        public abstract class Node
        {
            public abstract Node Next();
        }
    }


    public static class LinkedListLoop
    {
        public static int GetLoopSize(LoopDetector.Node startNode)
        {
            var nodes = new Dictionary<LoopDetector.Node, int> {{startNode, 0}};
            var currentNode = startNode;
            var i = 0;

            while (true)
            {
                i++;
                currentNode = currentNode.Next();

                if (!nodes.TryAdd(currentNode, i))
                {
                    var result = nodes.Count - nodes[currentNode];
                    return result;
                }
            }
        }
    }
}