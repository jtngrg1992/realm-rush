using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] Vector2Int startCoordinates;
    [SerializeField] Vector2Int destinationCoordinates;

    private Node startNode;
    private Node destinationNode;
    private Node currentSearchNode;
    private List<Node> currentPath = new List<Node>();

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
        }


    }

    void Start()
    {
        startNode = gridManager.Grid[startCoordinates];
        destinationNode = gridManager.Grid[destinationCoordinates];
        currentPath = CreateNewPath();
    }


    private List<Node> CreateNewPath()
    {
        gridManager.ResetNodes();
        BreadthFirstSearch();
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

    private void BreadthFirstSearch()
    {
        currentPath.Clear();
        frontier.Clear();
        reached.Clear();

        bool isRunning = true;

        frontier.Enqueue(startNode);
        startNode.isPath = true;
        startNode.isExplored = true;
        reached.Add(startCoordinates, startNode);

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
}
