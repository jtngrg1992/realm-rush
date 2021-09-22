using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{

    [SerializeField] bool isPlacable = false;
    [SerializeField] GameObject towerPrefab;

    public bool IsPlacable { get { return isPlacable; } }

    private void OnMouseDown()
    {
        if (isPlacable)
        {
            SpawnTower();
        }
    }

    private void SpawnTower()
    {
        Instantiate(towerPrefab, transform.position, Quaternion.identity);
        isPlacable = false;
    }
}
