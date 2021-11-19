using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitScript : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        //3 key on player + player touch exit cube
        if (other.name == "Player(Clone)" && ScoringSystem.theScore == 3)
        {
            SceneManager.LoadScene("Menu");
            SceneManager.UnloadSceneAsync("MainScene");
        }
    }

}
