using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartUI : MonoBehaviour
{
    public GameObject settingUI;
    // Start is called before the first frame update
    private void Start()
    {
        settingUI = GameObject.FindWithTag("Settings");
        settingUI.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            settingUI.SetActive(false);
        }
    }
    public void StartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Map");
        AudioManager.Instance.PlayMusic("Theme");
        PlayTimer.Instance.RestartTimer();
    }

    public void OpenSettings()
    {
        settingUI.SetActive(true);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
