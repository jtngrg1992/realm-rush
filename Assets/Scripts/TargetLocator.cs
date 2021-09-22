
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] Transform weapon;
    [SerializeField] float targetRange = 15f;
    [SerializeField] ParticleSystem projectileSystem;

    private Transform target;

    void FindClosestTarget()
    {
        float maxDistance = Mathf.Infinity;
        Enemy closestTarget = null;

        Enemy[] allEnemies = FindObjectsOfType<Enemy>();

        foreach (Enemy enemy in allEnemies)
        {
            float enemyDistance = Vector3.Distance(transform.position, enemy.transform.position);
            if (enemyDistance < maxDistance)
            {
                maxDistance = enemyDistance;
                closestTarget = enemy;
            }
        }

        if (closestTarget != null)
        {
            target = closestTarget.transform;
        }

    }
    // Update is called once per frame
    void Update()
    {
        FindClosestTarget();
        Attack(AimWeapon());
    }

    bool AimWeapon()
    {
        if (target == null)
        {
            return false;
        }

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance <= targetRange)
        {
            weapon.LookAt(target);
            return true;
        }

        return false;
    }

    void Attack(bool shouldEnable)
    {
        var emissionModule = projectileSystem.emission;
        emissionModule.enabled = shouldEnable;
    }
}
