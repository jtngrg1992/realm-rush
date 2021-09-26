using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] [Range(0.5f, 10f)] float speed = 0.5f;

    List<Node> path = new List<Node>();
    PathFinder pathFinder;
    GridManager gridManager;
    private Enemy enemy;


    private void Awake()
    {
        pathFinder = FindObjectOfType<PathFinder>();
        gridManager = FindObjectOfType<GridManager>();
        enemy = GetComponentInParent<Enemy>();
    }

    void OnEnable()
    {
        MoveToStart();
        RecalculatePath(true);
    }

    void MoveToStart()
    {
        transform.position = gridManager.GetPositionFromCoordinates(pathFinder.StartCoordinates);
    }


    void FinishPath()
    {
        gameObject.SetActive(false);
        Debug.Log("Calling finish path");
        enemy.PenaliseGold();
    }

    void RecalculatePath(bool shouldReset)
    {

        path.Clear();

        if (shouldReset)
        {
            path = pathFinder.CreateNewPath(pathFinder.StartCoordinates);
        }
        else
        {
            Vector2Int currentCoordinates = gridManager.GetCoordindatesFromPosition(transform.position);
            path = pathFinder.CreateNewPath(currentCoordinates);
        }

        StopAllCoroutines();
        StartCoroutine(FollowPath());
    }

    IEnumerator FollowPath()
    {
        if (path.Count > 1)
        {
            for (int i = 1; i < path.Count; i++)
            {
                Vector3 startPosition = transform.position;
                Vector3 endPosition = gridManager.GetPositionFromCoordinates(path[i].coordinates);

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
}
