using UnityEngine;

public class EnemyHitManager : MonoBehaviour
{
    [SerializeField] float hp = 5;
    private float currentHP = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentHP = hp;
    }

    // Update is called once per frame
    void Update()
    {

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
            Destroy(gameObject);
        }
    }
}
