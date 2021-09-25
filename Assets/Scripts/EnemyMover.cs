using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    [SerializeField] [Range(1f, 10f)] float speed = 1f;
    private Enemy enemy;

    void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    void OnEnable()
    {
        FindPath();
        MoveToStart();
        StartCoroutine(FollowPath());
    }

    void FindPath()
    {
        path.Clear();
        GameObject pathParent = GameObject.FindGameObjectWithTag("Path");

        foreach (Transform waypoint in pathParent.transform)
        {
            Waypoint way = waypoint.GetComponent<Waypoint>();
            if (way != null)
            {
                path.Add(waypoint.GetComponent<Waypoint>());
            }
        }
    }

    void MoveToStart()
    {
        transform.position = path[0].transform.position;
    }


    void FinishPath()
    {

        gameObject.SetActive(false);
        enemy.PenaliseGold();
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
        // reached end
        FinishPath();
    }
}
