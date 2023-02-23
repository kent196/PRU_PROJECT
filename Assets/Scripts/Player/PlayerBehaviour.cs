using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private UnityEvent OnBegin, OnDone;

    Rigidbody2D rb;
    Animator anim;

    private int health, maxHealth;
    private HealthStats healthStats;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        healthBar = FindObjectOfType<HealthBar>();
        healthStats = new HealthStats(100, 100);
        health = healthStats.Health;
        maxHealth = healthStats.MaxHealth;
        maxHealth = health;
        Debug.Log("Health = " + health + "Max health = " + maxHealth);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            health -= 5;
            healthBar.SetHealth(health);
        }
    }

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(.15f);
        rb.velocity = Vector2.zero;
        OnDone?.Invoke();
    }
}
