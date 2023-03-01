using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1.5f;
    public float Speed { get { return moveSpeed; } private set { moveSpeed = value; } }


    Rigidbody2D rb;
    Animator anim;

    private float moveX;
    private float moveY;
    private Vector2 direction;
    private Vector2 lastDirection;

    private float activeMoveSpeed;
    private float dashSpeed = 2f;
    private float dashLength = .5f, dashCooldown = 1f;
    private float dashCounter;
    private float dashCoolCounter;
    private bool dashing = false;
    // Start is called before the first frame update
    void Start()
    {
        activeMoveSpeed = moveSpeed;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!dashing)
        {
            moveX = Input.GetAxisRaw("Horizontal");
            moveY = Input.GetAxisRaw("Vertical");
        }

    }
    private void LateUpdate()
    {
        if (!anim.GetBool("Dead"))
        {
            GetDirection();
            Moving();
            Dash();
        }

    }
    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (dashCoolCounter <= 0 && dashCounter <= 0)
            {
                activeMoveSpeed = dashSpeed;
                dashCounter = dashLength;
                dashing = true;
                StartCoroutine(DashCooldown());
            }
        }
        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;
            if (dashCounter <= 0)
            {
                activeMoveSpeed = moveSpeed;
                dashCoolCounter = dashCooldown;
            }
        }
        if (dashCoolCounter > 0)
        {
            dashCoolCounter -= Time.deltaTime;
        }
    }

    IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(dashLength);
        dashing = false;
    }

    private void GetDirection()
    {
        direction = new Vector2(moveX, moveY);
        lastDirection = direction;
        direction.Normalize();
        lastDirection.Normalize();

        anim.SetFloat("moveX", direction.x);
        anim.SetFloat("moveY", direction.y);
        if (moveX == 1 || moveX == -1 || moveY == 1 || moveY == -1)
        {
            anim.SetFloat("lastMoveX", lastDirection.x);
            anim.SetFloat("lastMoveY", lastDirection.y);
        }
    }
    private void Moving()
    {
        rb.velocity = direction * activeMoveSpeed;
    }
}
