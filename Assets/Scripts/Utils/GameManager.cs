using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool GameIsPaused = false;
    [SerializeField] public GameObject gui;
    [SerializeField] public GameObject pauseMenu;
    public GameObject victoryUI;


    private Animator animator;
    public static GameManager Instance { get; private set; }
    public GameObject powerUpSelect;
    public GameObject endMenu;
    public GameObject confirmBox;
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
        victoryUI = GameObject.FindWithTag("Win");
        victoryUI.SetActive(false);
        confirmBox = GameObject.FindWithTag("Confirm Box");
        confirmBox.SetActive(false);
        gui = GameObject.FindWithTag("GUI");
        pauseMenu = GameObject.FindWithTag("Pause Menu");
        pauseMenu.SetActive(false);
        endMenu = GameObject.FindWithTag("End UI");
        endMenu.SetActive(false);
        powerUpSelect = GameObject.FindWithTag("PowerUpSelect");
        powerUpSelect.SetActive(false);
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        HandlePauseGame();

    }

    public void HandlePauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (GameIsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        PlayTimer.Instance.RunTimer();
        pauseMenu.SetActive(false);
        gui.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void PauseGame()
    {
        PlayTimer.Instance.PauseTimer();
        pauseMenu.SetActive(true);
        gui.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void PowerUpSelect()
    {
        Time.timeScale = 0f;
        powerUpSelect.SetActive(true);
    }

    public void PowerUpSelected()
    {
        Time.timeScale = 1f;
        powerUpSelect.SetActive(false);
    }

    public void EndGame()
    {
        PlayTimer.Instance.PauseTimer();
        Time.timeScale = 0f;
        gui.SetActive(false);
        endMenu.SetActive(true);
    }

    public void RestartGame()
    {
        PlayTimer.Instance.RestartTimer();
        Time.timeScale = 1f;
        gui.SetActive(true);
        endMenu.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Map");
    }

    public void Win()
    {
        victoryUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ConfirmBox()
    {
        confirmBox.SetActive(true);
    }

    public void ConfirmYes()
    {
        SceneManager.LoadScene("Start");
    }

    public void ConfirmNo()
    {
        confirmBox.SetActive(false);
    }
}
