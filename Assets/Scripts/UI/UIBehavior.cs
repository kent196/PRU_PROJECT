using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBehavior : MonoBehaviour
{
    public GameObject sm;
    public Text txtAtk, txtSpd, txtPoints;
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

    }

    public void PowerUpSelected()
    {
        sm.GetComponent<SpawnManager>().PowerUpSelected();
    }

    public int AddPoints(int points)
    {
        currentPoints += points;
        Debug.Log(currentPoints);
        txtPoints.text = "Points: " + currentPoints.ToString();
        return currentPoints;
    }
}
