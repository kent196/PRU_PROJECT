using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayer;

    private float radius = 0.3f;

    Animator anim;

    public int Damage { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        Damage = EnemyDamage();
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
            return 30;
        }
        else if (this.gameObject.CompareTag("Orc"))
        {
            return 50;
        }
        else if (this.gameObject.CompareTag("Mummy"))
        {
            return 20;
        }
        else if (this.gameObject.CompareTag("Skelly"))
        {
            return 10;
        }
        else
        {
            return 0;
        }
    }
}
