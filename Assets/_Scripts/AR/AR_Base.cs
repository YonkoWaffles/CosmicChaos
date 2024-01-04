using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class AR_Base : MonoBehaviour
{
    [SerializeField] GameObject healthBar;
    [SerializeField] GameObject GameOverUI;
    [SerializeField] TextMeshProUGUI finalScore;
    private void Awake()
    {

        GameOverUI.SetActive(false);
    }

    public void GameOver()
    {
        GameOverUI.SetActive(true);
        Time.timeScale = 0;
        GameObject.Find("Crosshair").SetActive(false);
        GameObject.Find("Score").SetActive(false);
        finalScore.text = "Final Score: " + Score.scoreCount.ToString();
    }

    
}
