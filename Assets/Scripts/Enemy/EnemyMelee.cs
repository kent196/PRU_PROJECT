using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    HealthStats healthStats;

    [SerializeField] private LayerMask playerLayer;

    [SerializeField] private float radius = 1f;

    PlayerBehaviour playerBehaviour;
    Rigidbody2D rb;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        healthStats = new HealthStats(20, 20);
        //playerBehaviour = FindObjectOfType<PlayerBehaviour>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
        Dead();
    }

    bool PlayerInRange()
    {
        return Physics2D.OverlapCircle(transform.position, radius, playerLayer);
    }

    void Attack()
    {
        if (PlayerInRange())
        {
            anim.SetBool("Attacking", true);
        }
        else
        {
            anim.SetBool("Attacking", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Arrow"))
        {
            healthStats.DamageUnit(5);
        }
    }

    void Dead()
    {
        if (healthStats.Health == 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
