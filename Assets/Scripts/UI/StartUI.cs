using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartUI : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Map");
        AudioManager.Instance.PlayMusic("Theme");
        PlayTimer.Instance.RestartTimer();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
