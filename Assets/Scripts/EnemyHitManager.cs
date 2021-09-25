using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHitManager : MonoBehaviour
{
    [SerializeField] float hp = 5;
    [SerializeField] float difficultyRamp = 1;

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
            // increase health points for next round
            hp += difficultyRamp;
        }
    }
}
