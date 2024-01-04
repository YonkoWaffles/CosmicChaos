using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Score : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public static int scoreCount = 0;
    // Start is called before the first frame update
    void Awake()
    {
        scoreCount = 0;
        scoreText.text = scoreCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = scoreCount.ToString();
    }
}
