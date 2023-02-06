using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 10f;

    Rigidbody2D rb;

    private float moveX;
    private float moveY;
    private Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");

        GetDirection();
    }
    private void LateUpdate()
    {
        Moving();
    }

    private void GetDirection()
    {
        direction = new Vector2(moveX, moveY);
        direction.Normalize();
    }
    private void Moving()
    {
        rb.velocity = direction * speed;
    }
}
