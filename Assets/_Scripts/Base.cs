using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class Base : MonoBehaviour
{
    public GameObject healthBar;
    private ProgressBarCircle healthScript;
    private string rocketTag = "Rocket";
    public GameObject GameOverUI;
    public TextMeshProUGUI finalScore;
    private void Start()
    {
        healthScript = healthBar.GetComponent<ProgressBarCircle>();
        GameOverUI.SetActive(false);
    }

    private void GameOver()
    {
        GameOverUI.SetActive(true);
        Time.timeScale = 0;
        GameObject.Find("Crosshair").SetActive(false);
        GameObject.Find("Score").SetActive(false);
        finalScore.text = "Final Score: " + Score.scoreCount.ToString();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(rocketTag))
        {
            Debug.Log("Missile hit");
           
            healthScript.BarValue -= 5;
            if(healthScript.BarValue <= 0)
            {
                GameOver();
            }
        }
    }
}
