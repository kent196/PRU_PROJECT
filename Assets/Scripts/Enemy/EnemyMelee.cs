using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayer;

    private float radius = 0.5f;

    Rigidbody2D rb;
    Animator anim;

    public int Damage { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        Damage = EnemyDamage();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private int EnemyDamage()
    {
        if (this.gameObject.CompareTag("Goblin"))
        {
            return 15;
        }
        else if (this.gameObject.CompareTag("Orc"))
        {
            return 20;
        }
        else if (this.gameObject.CompareTag("Mummy"))
        {
            return 10;
        }
        else if (this.gameObject.CompareTag("Skelly"))
        {
            return 5;
        }
        else
        {
            return 0;
        }
    }
}
