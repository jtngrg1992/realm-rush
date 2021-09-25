using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] Vector2Int gridSize;

    public Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();


    private void Awake()
    {
        CreateGrid();

        Debug.Log(grid);
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
}
