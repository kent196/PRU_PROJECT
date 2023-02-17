using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform player;

    Animator anim;
    Rigidbody2D rb;

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
        Moving();
    }

    void Moving()
    {
        direction = player.position - transform.position;
        direction.Normalize();
        rb.velocity = direction * speed;
        GetAnimation();
    }

    void GetAnimation()
    {
        anim.SetFloat("moveX",direction.x);
        anim.SetFloat("moveY",direction.y);
        if (direction.x == 1 || direction.x == -1 || direction.y == 1 || direction.y == -1)
        {
            anim.SetFloat("lastMoveX", direction.x);
            anim.SetFloat("lastMoveY", direction.y);
        }
    }
}
