namespace MoraJai.Lib;

public class Solver
{
    private int MaxLevels {get; set;}

    // to avoid cycles
    private HashSet<int> visitedStates = new HashSet<int>();

    private SolutionNode? deepestNode = null;

    public SolutionNode? SolveStart(GameState initState, int maxLevels = 15)
    {
        var root = new SolutionNode(initState, null, (-1, -1));
        MaxLevels = maxLevels;
        return Solve(root);
    }

    //breadth first search
    // add root to queue and mark as visited
    // while queue not empty
    // dequeue node
    // if node is solved, return node
    // else
    // get all possible moves from this node.
    // for each move, create new node and enqueue if not already visited
    // if no solution found, return null
    public SolutionNode? Solve(SolutionNode node)
    {
        var queue = new Queue<SolutionNode>();
        queue.Enqueue(node);
        visitedStates.Add(node.State.GetHashCode());

        while (queue.Count > 0)
        {
            var currentNode = queue.Dequeue();
            if (currentNode.Depth > (deepestNode?.Depth ?? -1)) deepestNode = currentNode;

            if (currentNode.State.IsSolved())
            {
                return currentNode;
            }

            if(currentNode.Depth >= MaxLevels)
            {
                Console.WriteLine("Max levels reached.");
                break;
            }

            for (int r = 0; r < 3; r++)
            {
                for (int c = 0; c < 3; c++)
                {
                    var tile = currentNode.State.Board[r, c].ToTile();
                    var newState = tile.Press(currentNode.State, r, c);
                    var newNode = new SolutionNode(newState, currentNode, (r, c));

                    if (!visitedStates.Contains(newState.GetHashCode()))
                    {
                        visitedStates.Add(newState.GetHashCode());
                        queue.Enqueue(newNode);
                    }
                }
            }
        }

        Console.WriteLine("No solution found, deepest state reached: " + deepestNode?.Depth);
        Console.WriteLine(deepestNode);
        return null;
    }
}
