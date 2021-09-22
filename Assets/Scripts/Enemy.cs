using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int goldReward = 15;
    [SerializeField] int goldPenalty = 15;

    private Bank bank;

    // Start is called before the first frame update
    void Start()
    {
        bank = FindObjectOfType<Bank>();
    }

    public void RewardGold()
    {
        if (bank == null)
        {
            return;
        }

        bank.DepositAmount(goldReward);
    }

    public void PenaliseGold()
    {
        if (bank == null) { return; }

        bank.WithdrawAmount(goldPenalty);
    }
}
