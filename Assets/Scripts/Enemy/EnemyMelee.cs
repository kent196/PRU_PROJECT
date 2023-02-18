using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayer;

    [SerializeField] private float radius = 1f;

    PlayerBehaviour playerBehaviour;
    Rigidbody2D rb;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        //playerBehaviour = FindObjectOfType<PlayerBehaviour>();
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
        Gizmos.DrawSphere(transform.position, radius);
    }
}
