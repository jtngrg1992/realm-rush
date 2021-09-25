using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class Bank : MonoBehaviour
{
    [SerializeField] int reserve = 150;
    [SerializeField] int currentReserve = 0;
    [SerializeField] TextMeshProUGUI balanceIndicator;

    public int CurrentReserve { get { return currentReserve; } }

    // Start is called before the first frame update
    void Start()
    {
        currentReserve = reserve;
        UpdateDisplay();
    }

    public void DepositAmount(int amount)
    {
        currentReserve += Mathf.Abs(amount);
        UpdateDisplay();
    }

    public void WithdrawAmount(int amount)
    {
        currentReserve -= Mathf.Abs(amount);
        UpdateDisplay();

        if (currentReserve < 0)
        {
            // loose game
            StartCoroutine(ReloadLevel());
        }
    }

    private IEnumerator ReloadLevel()
    {
        yield return new WaitForSeconds(1f);
        Scene currentScene = SceneManager.GetActiveScene();
        int sceneIndex = currentScene.buildIndex;
        SceneManager.LoadScene(sceneIndex);
    }

    private void UpdateDisplay()
    {
        balanceIndicator.text = $"Gold: {currentReserve}";
    }
}
