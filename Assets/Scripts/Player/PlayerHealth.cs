using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private int health, maxHealth;
    private HealthStats healthStats;
    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        healthBar = FindObjectOfType<HealthBar>();
        healthStats = new HealthStats(100, 100);
        health = healthStats.Health;
        maxHealth = healthStats.MaxHealth;
        maxHealth = health;
        Debug.Log("Health = " + health + "Max health = " + maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            health -= 5;
            healthBar.SetHealth(health);
        }
    }
}
