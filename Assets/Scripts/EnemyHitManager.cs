using UnityEngine;

public class EnemyHitManager : MonoBehaviour
{
    [SerializeField] float hp = 5;
    private float currentHP = 0;
    private Enemy enemy;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    void OnEnable()
    {
        currentHP = hp;
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }

    void ProcessHit()
    {
        currentHP--;

        if (currentHP < 1)
        {
            // dead
            gameObject.SetActive(false);
            enemy.RewardGold();
        }
    }
}
