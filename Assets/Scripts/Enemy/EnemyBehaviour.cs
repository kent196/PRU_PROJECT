using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyBehaviour : MonoBehaviour
{
    HealthStats healthStats;
    Animator anim;
    Rigidbody2D rb;
    Collider2D col;
    GameObject ui;

    [SerializeField] private UnityEvent OnBegin, OnDone;
    int health, maxHealth;
    PlayerAbilities playerability;


    // Start is called before the first frame update
    void Start()
    {
        ui = GameObject.FindWithTag("UI");
        EnemyHealth();
        col = GetComponent<Collider2D>();
        healthStats = new HealthStats(health, maxHealth);
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerability = FindObjectOfType<PlayerAbilities>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Dead();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Arrow"))
        {
            AudioManager.Instance.PlaySFX("EnemyHit", 0.2f);
            StopAllCoroutines();
            OnBegin?.Invoke();
            anim.SetTrigger("Hit");
            Vector2 direction = (transform.position - collision.gameObject.transform.position).normalized;
            rb.AddForce(direction * 2, ForceMode2D.Impulse);
            healthStats.DamageUnit(playerability.Damage);
            Debug.Log(playerability.Damage);
            StartCoroutine(Reset());
            DmgPopUp.Create(this.transform.position, playerability.Damage);

        }
    }

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(.15f);
        rb.velocity = Vector2.zero;
        OnDone?.Invoke();
    }

    void Dead()
    {
        if (healthStats.Health <= 0)
        {
            col.isTrigger = true;
            anim.SetBool("Dead", true);
        }
    }



    void DestroyEnemy()
    {
        var UI = ui.GetComponent<UIBehavior>();
        if (this.gameObject.CompareTag("Skelly"))
        {
        }
        if (this.gameObject.CompareTag("Goblin"))
        {
        }
        if (this.gameObject.CompareTag("Mummy"))
        {
        }
        if (this.gameObject.CompareTag("Orc"))
        {
        }
        Destroy(gameObject);
    }

    void EnemyHealth()
    {
        if (this.gameObject.CompareTag("Goblin"))
        {
            health = 300;
            maxHealth = 300;
        }
        else if (this.gameObject.CompareTag("Orc"))
        {
            health = 500;
            maxHealth = 500;
        }
        else if (this.gameObject.CompareTag("Mummy"))
        {
            health = 200;
            maxHealth = 200;
        }
        else if (this.gameObject.CompareTag("Skelly"))
        {
            health = 150;
            maxHealth = 150;
        }
    }
}
