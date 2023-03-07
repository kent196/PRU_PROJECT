using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayTimer : MonoBehaviour
{
    [SerializeField] private int playTime = 0;
    [SerializeField] private int bestPlayTime;

    public bool activated = false;
    private string time;
    private int seconds = 0;
    private int minutes = 0;
    private int hours = 0;

    public static PlayTimer Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        bestPlayTime = SaveSystem.GetInt("bestPlayTime");
        activated = true;
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        while (activated)
        {
            yield return new WaitForSeconds(1);
            playTime += 1;
            seconds = playTime % 60;
            minutes = (playTime / 60) % 60;
            hours = (playTime / 3600) % 24;
            SetTime();
            Debug.Log(time);
        }

    }


    private void Update()
    {
        Debug.Log(playTime);
    }

    public string GetTime()
    {
            return time;
    }

    private void SetTime()
    {
        if (hours > 0)
        {
            time = "Time:" + hours.ToString() + ":" + minutes.ToString() + ":" + seconds.ToString();
        }
        else
        {
            time = "Time:" + minutes.ToString() + ":" + seconds.ToString();
        }

    }

    public void RunTimer()
    {
        activated = true;
    }

    public void PauseTimer()
    {
        activated = false;
    }

    public void RestartTimer()
    {
        playTime = 0;
        activated = true;
    }

    public void EndTimer()
    {
        activated = false;
        if (bestPlayTime == 0)
        {
            SaveSystem.SetInt("bestPlayTime", playTime);
        }
        else
        {
            if (playTime < bestPlayTime)
            {
                SaveSystem.SetInt("bestPlayTime", playTime);
                SaveSystem.SaveToDisk();
            }
            else
            {
                return;
            }
        }
    }
}
