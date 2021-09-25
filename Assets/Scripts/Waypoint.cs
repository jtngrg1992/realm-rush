using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{

    [SerializeField] bool isPlacable = false;
    [SerializeField] Tower towerPrefab;

    Vector2Int coordinates = new Vector2Int();
    GridManager gridManager;
    PathFinder pathFinder;

    public bool IsPlacable { get { return isPlacable; } }


    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinder>();
    }

    private void Start()
    {

        if (gridManager != null)
        {
            coordinates = gridManager.GetCoordindatesFromPosition(transform.position);

            if (!isPlacable)
            {
                gridManager.BlockNode(coordinates);
            }
        }
    }

    private void OnMouseDown()
    {
        if (gridManager.GetNode(coordinates).isWalkable && !pathFinder.WillBlockPath(coordinates))
        {
            SpawnTower();
            pathFinder.NotifyPathChange();
        }
    }

    private void SpawnTower()
    {
        bool result = towerPrefab.CreateTower(towerPrefab, transform.position);
        if (result)
        {
            isPlacable = false;
            gridManager.BlockNode(coordinates);
        }
        else
        {
            isPlacable = true;
        }
    }
}
