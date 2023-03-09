using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBehavior : MonoBehaviour
{
    public GameObject sm;
    public Text txtAtk, txtSpd, txtTime, txtFinalTime, txtBestTime;
    private int atk;
    private float spd;
    private GameObject playerAbilities;
    private GameObject playerMovement;


    private void Start()
    {
        playerAbilities = GameObject.FindGameObjectWithTag("Player");
        playerMovement = GameObject.FindGameObjectWithTag("Player");
        atk = playerAbilities.GetComponent<PlayerAbilities>().Damage;
        spd = playerMovement.GetComponent<PlayerMovement>().Speed;
        sm = GameObject.FindGameObjectWithTag("SpawnManager");
        txtAtk.text = "Damage: " + atk.ToString();
        txtSpd.text = "Speed: " + spd.ToString();
        txtTime.text = "" + PlayTimer.Instance.GetTime();
        txtBestTime.text = PlayTimer.Instance.GetBestTime();
    }
    private void Update()
    {
        atk = playerAbilities.GetComponent<PlayerAbilities>().Damage;
        spd = playerMovement.GetComponent<PlayerMovement>().Speed;
        txtAtk.text = "Damage: " + atk.ToString();
        txtSpd.text = "Speed: " + spd.ToString();
        txtTime.text = "" + PlayTimer.Instance.GetTime();
        txtFinalTime.text = "" + PlayTimer.Instance.GetTime();
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
