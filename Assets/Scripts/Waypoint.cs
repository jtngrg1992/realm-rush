using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{

    [SerializeField] bool isPlacable = false;

    private void OnMouseDown()
    {
        if (isPlacable)
        {
            Debug.Log(name);
        }

    }
}
