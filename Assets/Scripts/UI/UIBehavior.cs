using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBehavior : MonoBehaviour
{
    public GameObject sm;
    public Text txtAtk, txtSpd, txtTime, txtFinalTime, txtBestTime, txtHealthStat;
    private int attk, atk, health, maxHealth;
    private float spd, aspd;
    private GameObject playerAbilities;
    private GameObject playerMovement;
    private GameObject player;


    private void Start()
    {
        playerAbilities = GameObject.FindGameObjectWithTag("Player");
        playerMovement = GameObject.FindGameObjectWithTag("Player");
        player = GameObject.FindGameObjectWithTag("Player");

        health = player.GetComponent<PlayerBehaviour>().Health;
        maxHealth = player.GetComponent<PlayerBehaviour>().MaxHealth;
        atk = playerAbilities.GetComponent<PlayerAbilities>().Damage;
        spd = playerMovement.GetComponent<PlayerMovement>().Speed;
        sm = GameObject.FindGameObjectWithTag("SpawnManager");
        aspd = spd * 100;
        txtAtk.text = "Damage: " + atk.ToString();
        txtSpd.text = "Speed: " + aspd.ToString();
        txtTime.text = "" + PlayTimer.Instance.GetTime();
        txtBestTime.text = PlayTimer.Instance.GetBestTime();
        Debug.Log(health + "/" + maxHealth);
    }
    private void Update()
    {
        atk = playerAbilities.GetComponent<PlayerAbilities>().Damage;
        spd = playerMovement.GetComponent<PlayerMovement>().Speed;
        aspd = spd * 100;
        txtAtk.text = "Damage: " + atk.ToString();
        txtSpd.text = "Speed: " + aspd.ToString();
        txtTime.text = "" + PlayTimer.Instance.GetTime();
        txtFinalTime.text = "" + PlayTimer.Instance.GetTime();
        txtHealthStat.text = health.ToString() + "/" + maxHealth.ToString();

    }
    private void LateUpdate()
    {
        health = player.GetComponent<PlayerBehaviour>().Health;
        maxHealth = player.GetComponent<PlayerBehaviour>().MaxHealth;
        txtHealthStat.text = health + "/" + maxHealth;

    }

    public void PowerUpSelected()
    {
        sm.GetComponent<SpawnManager>().PowerUpDmgSelected();
        sm.GetComponent<SpawnManager>().PowerUpSpdSelected();
        sm.GetComponent<SpawnManager>().PowerUpHPSelected();
    }

    public void Resume()
    {
        PlayTimer.Instance.RunTimer();
        GameManager.Instance.ResumeGame();
    }


    public void Restart()
    {
        AudioManager.Instance.PlayMusic("Theme");
        PlayTimer.Instance.RestartTimer();
        GameManager.Instance.RestartGame();
    }

    public void OpenSettings()
    {
        GameManager.Instance.OpenSettings();
    }

    public void ConfirmBox()
    {
        GameManager.Instance.ConfirmBox();
    }

    public void ConfirmYes()
    {
        AudioManager.Instance.PauseMusic("Boss Round");
        AudioManager.Instance.PauseMusic("Theme");
        GameManager.Instance.ConfirmYes();
    }

    public void ConfirmNo()
    {
        GameManager.Instance.ConfirmNo();
    }

}
