using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int cost = 75;

    Bank bank;

    private void Start()
    {

    }

    public bool CreateTower(Tower prefab, Vector3 position)
    {
        bank = FindObjectOfType<Bank>();
        if (bank == null)
        {
            return false;
        }
        else
        {
            bank.WithdrawAmount(cost);
            Instantiate(prefab, position, Quaternion.identity);
            return true;
        }

    }
}
