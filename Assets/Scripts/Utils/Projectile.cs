using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float strength = 1000f;

    Vector2 forceDirection;
    Rigidbody2D rb;
    [SerializeField]LayerMask knockBackLayer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == knockBackLayer)
        {
            forceDirection = (collision.gameObject.transform.position - transform.position).normalized;
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(forceDirection * strength, ForceMode2D.Force);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
}