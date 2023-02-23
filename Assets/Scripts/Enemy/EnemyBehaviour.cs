using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyBehaviour : MonoBehaviour
{
    HealthStats healthStats;
    Animator anim;
    Rigidbody2D rb;

    [SerializeField] private UnityEvent OnBegin,OnDone;

    // Start is called before the first frame update
    void Start()
    {
        healthStats = new HealthStats(20, 20);
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
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
            StopAllCoroutines();
            OnBegin?.Invoke();
            anim.SetTrigger("Hit");
            Vector2 direction = (transform.position - collision.gameObject.transform.position).normalized;
            rb.AddForce(direction * 2, ForceMode2D.Impulse);
            healthStats.DamageUnit(5);
            StartCoroutine(Reset());
        }
    }

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(.15f);
        rb.velocity = Vector2.zero;
        OnDone?.Invoke();
    }

    void Dead()
    {
        if (healthStats.Health == 0)
        {
            anim.SetBool("Dead", true);
        }
    }

    void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
