using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    HealthStats healthStats;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        healthStats = new HealthStats(20, 20);
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Dead();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Arrow"))
        {
            anim.SetTrigger("Hit");
            healthStats.DamageUnit(5);
        }
    }

    void Dead()
    {
        if (healthStats.Health == 0)
        {
            anim.SetBool("Dead",true);
        }
    }

    void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
