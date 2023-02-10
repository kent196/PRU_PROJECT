using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 10f;

    Rigidbody2D rb;
    Animator anim;

    private float moveX;
    private float moveY;
    private Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");
    }
    private void LateUpdate()
    {
        GetDirection();
        Moving();
    }

    private void GetDirection()
    {
        direction = new Vector2(moveX, moveY);
        direction.Normalize();
        anim.SetFloat("moveX", direction.x);
        anim.SetFloat("moveY", direction.y);
        if (moveX == 1 || moveX == -1 || moveY == 1 || moveY == -1)
        {
            anim.SetFloat("lastMoveX", moveX);
            anim.SetFloat("lastMoveY", moveY);
        }
    }
    private void Moving()
    {
        rb.velocity = direction * speed;
    }
}
