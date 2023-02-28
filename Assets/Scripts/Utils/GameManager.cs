using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Animator animator;
    public static GameManager Instance { get; private set; }
    public GameObject powerUpSelect;
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
        powerUpSelect = GameObject.FindWithTag("PowerUpSelect");
        powerUpSelect.SetActive(false);
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

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
}
