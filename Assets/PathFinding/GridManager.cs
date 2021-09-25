using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] Vector2Int gridSize;
    [Tooltip("World grid size. Should match unity editor snap settings")]
    [SerializeField] int worldGridSize = 10;

    public int UnityGridSize { get { return worldGridSize; } }
    private Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    public Dictionary<Vector2Int, Node> Grid { get { return grid; } }

    private void Awake()
    {
        CreateGrid();
    }


    private void CreateGrid()
    {
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector2Int coordinates = new Vector2Int(x, y);
                Node newNode = new Node(coordinates, true);
                grid.Add(coordinates, newNode);
            }
        }

    }

    public Node GetNode(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            return grid[coordinates];
        }
        return null;
    }

    public void BlockNode(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            // Debug.Log("blocking node at coordinates: " + coordinates);
            grid[coordinates].isWalkable = false;
        }
    }

    public Vector2Int GetCoordindatesFromPosition(Vector3 position)
    {
        Vector2Int coordindates = new Vector2Int();
        coordindates.x = Mathf.RoundToInt(position.x / worldGridSize);
        coordindates.y = Mathf.RoundToInt(position.z / worldGridSize);
        return coordindates;
    }

    public Vector3 GetPositionFromCoordinates(Vector2Int coordindates)
    {
        Vector3 position = new Vector3();
        position.x = coordindates.x * worldGridSize;
        position.z = coordindates.y * worldGridSize;

        return position;
    }

    public void ResetNodes()
    {
        foreach (KeyValuePair<Vector2Int, Node> entry in grid)
        {
            entry.Value.connectedTo = null;
            entry.Value.isExplored = false;
            entry.Value.isPath = false;
        }
    }
}
