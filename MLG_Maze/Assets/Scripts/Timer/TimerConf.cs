using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerConf : MonoBehaviour
{

    [SerializeField] Timer timer;

    private void Start()
    {
        timer
        .SetDuration(120)
        .OnEnd(() => Debug.Log("Timer ended"))
        .OnEnd(() => Time.timeScale = 1f)
        .OnEnd(() => SceneManager.LoadScene("You loose"))
        .Begin();
    }

}