using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoringSystem : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public static int theScore = 0;


    //Key nomber for ui
    void Update()
    {
        scoreText.text = " KEYS: " + theScore.ToString() + "/3" ;
    }
}
