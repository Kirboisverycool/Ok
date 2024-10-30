using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Bank : MonoBehaviour
{
    [SerializeField] int startingBalance;
    [SerializeField] int currentBalance;
    //[SerializeField] TextMeshProUGUI canvasGoldText;
    TextMeshPro goldText;
    int minGold = 0;

    private void Awake()
    {
        currentBalance = startingBalance;
        UpdateGoldText();
    }

    // Start is called before the first frame update
    void Start()
    {
        goldText = GetComponentInChildren<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentBalance <= minGold)
        {
            currentBalance = minGold;
        }

        goldText.text = "Gold: " + currentBalance;
    }

    private void UpdateGoldText()
    {
        //canvasGoldText.text = "Gold: " + currentBalance;
    }

    public int GetCurrentBalance()
    {
        return currentBalance;
    }

    public void Withdraw(int amount)
    {
        currentBalance -= amount;
        UpdateGoldText();
        /*if (currentBalance < 0)
        {
            ReloadScene();
        }*/
    }

    public void Deposit(int amount)
    {
        currentBalance += amount;
        UpdateGoldText();
    }

    void ReloadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
