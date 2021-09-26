using System.Collections;
using UnityEngine;


public class Tower : MonoBehaviour
{
    [SerializeField] int cost = 75;
    [SerializeField] float buildTime = 2f;

    Bank bank;

    private void Start()
    {
        StartCoroutine(BuildComponent());
    }

    IEnumerator BuildComponent()
    {
        // disable all children

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
            foreach (Transform grandChild in child)
            {
                grandChild.gameObject.SetActive(false);
            }
        }

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(buildTime);
            foreach (Transform grandChild in child)
            {
                grandChild.gameObject.SetActive(true);
            }
        }

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
            if (bank.CurrentReserve > cost)
            {
                bank.WithdrawAmount(cost);
                Instantiate(prefab, position, Quaternion.identity);
                return true;
            }

        }
        return false;
    }
}
