using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank : MonoBehaviour
{
    [SerializeField] int reserve = 150;
    [SerializeField] int currentReserve = 0;

    public int CurrentReserve { get { return currentReserve; } }

    // Start is called before the first frame update
    void Start()
    {
        currentReserve = reserve;
    }

    public void DepositAmount(int amount)
    {
        currentReserve += Mathf.Abs(amount);
    }

    public void WithdrawAmount(int amount)
    {
        currentReserve -= Mathf.Abs(amount);
    }
}
