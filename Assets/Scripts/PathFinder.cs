using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] Vector2Int startCoordinates;
    public Vector2Int StartCoordinates { get { return startCoordinates; } }
    [SerializeField] Vector2Int destinationCoordinates;
    public Vector2Int DestinationCoordinates { get { return destinationCoordinates; } }

    private Node startNode;
    private Node destinationNode;
    private Node currentSearchNode;
    private List<Node> currentPath = new List<Node>();
    public List<Node> CurrentPath
    {
        get
        {
            return currentPath;
        }
    }

    Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();
    Queue<Node> frontier = new Queue<Node>();
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();

    Vector2Int[] directions = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };
    GridManager gridManager;


    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();

        if (gridManager != null)
        {
            grid = gridManager.Grid;
            startNode = grid[startCoordinates];
            destinationNode = grid[destinationCoordinates];
        }
    }

    void Start()
    {
        currentPath = CreateNewPath();
    }


    public List<Node> CreateNewPath()
    {
        return CreateNewPath(startCoordinates);
    }

    public List<Node> CreateNewPath(Vector2Int coordinates)
    {
        gridManager.ResetNodes();
        BreadthFirstSearch(coordinates);
        List<Node> path = BuildPath();
        return path;
    }


    private void ExploreNeighbours()
    {
        if (grid == null) { return; }

        List<Node> neighbors = new List<Node>();

        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighborCoords = currentSearchNode.coordinates + direction;
            if (grid.ContainsKey(neighborCoords))
            {
                neighbors.Add(grid[neighborCoords]);
            }
        }

        foreach (Node neighbor in neighbors)
        {
            if (!reached.ContainsKey(neighbor.coordinates) && neighbor.isWalkable)
            {
                neighbor.connectedTo = currentSearchNode;
                reached.Add(neighbor.coordinates, neighbor);
                frontier.Enqueue(neighbor);
            }
        }
    }

    private void BreadthFirstSearch(Vector2Int coordinates)
    {
        startNode.isWalkable = true;
        destinationNode.isWalkable = true;
        currentPath.Clear();
        frontier.Clear();
        reached.Clear();

        bool isRunning = true;

        Node node = gridManager.Grid[coordinates];
        node.isPath = true;
        frontier.Enqueue(node);
        reached.Add(coordinates, node);

        while (isRunning && frontier.Count > 0)
        {
            currentSearchNode = frontier.Dequeue();
            currentSearchNode.isExplored = true;
            ExploreNeighbours();
            if (currentSearchNode.coordinates == destinationCoordinates)
            {
                isRunning = false;
            }
        }
    }

    private List<Node> BuildPath()
    {

        List<Node> path = new List<Node>();
        Node currentNode = destinationNode;

        path.Add(currentNode);
        currentNode.isPath = true;

        while (currentNode.connectedTo != null)
        {
            Node currentNodeParent = currentNode.connectedTo;
            path.Add(currentNodeParent);
            currentNode.isPath = true;
            currentNode = currentNodeParent;
        }

        path.Reverse();
        return path;
    }

    public bool WillBlockPath(Vector2Int coordinates)
    {
        if ((currentPath.Find(item => item.coordinates == coordinates)) != null)
        {
            // current path will be blocked, check if new path can be constructed
            bool originalValue = grid[coordinates].isWalkable;
            grid[coordinates].isWalkable = false;
            List<Node> newPath = CreateNewPath();
            grid[coordinates].isWalkable = originalValue;

            if (newPath.Count <= 1)
            {
                // new path can't be constructed
                currentPath = CreateNewPath();
                return true;
            }
            else
            {
                currentPath = newPath;
                return false;
            }

        }
        return false;
    }

    public void NotifyPathChange()
    {
        BroadcastMessage("RecalculatePath", false, SendMessageOptions.DontRequireReceiver);
    }
}
