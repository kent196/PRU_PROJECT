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
    private float timer = 0;
    private int seconds = 0;
    private int minutes = 0;
    private int hours = 0;
    private int bestSeconds = 0;
    private int bestMinutes = 0;
    private int bestHours = 0;

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
        GetBestTime();
    }


    private void Update()
    {
        if (activated)
        {
            timer += Time.deltaTime;
            playTime = Mathf.RoundToInt(timer);
            seconds = playTime % 60;
            minutes = (playTime / 60) % 60;
            hours = (playTime / 3600) % 24;
            SetTime();
            Debug.Log("playtime:");
        }
    }

    public string GetBestTime()
    {
        bestPlayTime = SaveSystem.GetInt("bestPlayTime");
        bestSeconds = bestPlayTime % 60;
        bestMinutes = (bestPlayTime / 60) % 60;
        bestHours = (bestPlayTime / 3600) % 24;
        if (bestHours > 0)
        {
            return "Personal best: " + bestHours.ToString() + ":" + bestMinutes.ToString() + ":" + bestSeconds.ToString();
        }
        else
        {
            return "Personal best: " + bestMinutes.ToString() + ":" + bestSeconds.ToString();
        }

    }

    public string GetTime()
    {
        return time;
    }

    private void SetTime()
    {
        if (hours > 0)
        {
            time = "Time: " + hours.ToString() + ":" + minutes.ToString() + ":" + seconds.ToString();
        }
        else
        {
            time = "Time: " + minutes.ToString() + ":" + seconds.ToString();
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
        activated = true;
        timer = 0;

    }

    public void EndTimer()
    {
        activated = false;
        if (bestPlayTime == 0)
        {
            SaveSystem.SetInt("bestPlayTime", playTime);
            SaveSystem.SaveToDisk();
        }
        else
        {
            if (playTime < bestPlayTime)
            {
                SaveSystem.SetInt("bestPlayTime", playTime);
                SaveSystem.SaveToDisk();
                Debug.Log("game saved");
            }
            else
            {
                return;
            }
        }
    }
}
