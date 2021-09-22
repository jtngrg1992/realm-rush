using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    [SerializeField] [Range(1f, 10f)] float speed = 1f;

    void Start()
    {
        FindPath();
        StartCoroutine(FollowPath());
    }

    void FindPath()
    {
        GameObject pathParent = GameObject.FindGameObjectWithTag("Path");

        foreach (Transform waypoint in pathParent.transform)
        {
            path.Add(waypoint.GetComponent<Waypoint>());
        }
    }


    IEnumerator FollowPath()
    {
        foreach (Waypoint point in path)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = point.transform.position;

            float travelPercent = 0f;

            transform.LookAt(endPosition);

            while (travelPercent < 1)
            {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
