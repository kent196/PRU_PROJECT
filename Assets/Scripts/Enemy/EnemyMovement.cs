using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private LayerMask playerLayer;

    Animator anim;
    Rigidbody2D rb;

    float radius = 0.5f;
    float speed = 1f;
    Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerInRange() || anim.GetBool("Hit") == true || anim.GetBool("Dead") == true)
        {
            rb.velocity = Vector2.zero;
            GetAnimation();
        }
        else
        {
            Moving();
            GetAnimation();
        }

    }

    void Moving()
    {
        direction = player.position - transform.position;
        direction.Normalize();
        rb.velocity = direction * speed;

    }
    void GetAnimation()
    {
        anim.SetFloat("moveX", direction.x);
        anim.SetFloat("moveY", direction.y);
        if (direction.x > 0 || direction.x < 0 || direction.y > 0 || direction.y < 0)
        {
            anim.SetFloat("lastMoveX", direction.x);
            anim.SetFloat("lastMoveY", direction.y);
        }
    }
    bool PlayerInRange()
    {
        return Physics2D.OverlapCircle(transform.position, radius, playerLayer);
    }
}
