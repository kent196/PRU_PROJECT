using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBehavior : MonoBehaviour
{
    public GameObject sm;
    public Text txtAtk, txtSpd, txtPoints, txtFinalPoints;
    private int atk;
    private int currentPoints = 0;
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
        txtPoints.text = "Points: " + currentPoints.ToString();
    }
    private void Update()
    {
        atk = playerAbilities.GetComponent<PlayerAbilities>().Damage;
        spd = playerMovement.GetComponent<PlayerMovement>().Speed;
        txtAtk.text = "Damage: " + atk.ToString();
        txtSpd.text = "Speed: " + spd.ToString();
        txtFinalPoints.text = "Total Points: " + currentPoints.ToString();

    }


    public void PowerUpSelected()
    {
        sm.GetComponent<SpawnManager>().PowerUpDmgSelected();
        sm.GetComponent<SpawnManager>().PowerUpSpdSelected();
        sm.GetComponent<SpawnManager>().PowerUpHPSelected();
    }

    public int AddPoints(int points)
    {
        currentPoints += points;
        Debug.Log(currentPoints);
        txtPoints.text = "Points: " + currentPoints.ToString();
        return currentPoints;
    }

    public void Resume()
    {
        GameManager.Instance.ResumeGame();
    }

    public void Restart()
    {
        GameManager.Instance.RestartGame();
    }

    public void ConfirmBox()
    {
        GameManager.Instance.ConfirmBox();
    }

    public void ConfirmYes()
    {
        GameManager.Instance.ConfirmYes();
    }

    public void ConfirmNo()
    {
        GameManager.Instance.ConfirmNo();
    }

}
