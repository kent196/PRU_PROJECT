using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAbility : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private GameObject lightning;
    [SerializeField] private Transform player;

    private float radius = 0.9f;
    private float castCooldown;
    private float attackCooldown;
    private float duration = 1f;

    Animator anim;

    public int Damage { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        castCooldown = duration;
        attackCooldown = duration;
        Damage = BossDamage();
        anim = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    bool PlayerNear()
    {
        return Physics2D.OverlapCircle(transform.position, radius, playerLayer);
    }

    void Casting()
    {
        StartCoroutine(CastSpell());
    }

    bool CanCast()
    {
        if (castCooldown <= 0)
        {
            return true;
        }
        else
        {
            castCooldown -= Time.deltaTime;
            return false;
        }
    }

    bool CanAttack()
    {
        if (attackCooldown <= 0)
        {
            return true;
        }
        else
        {
            attackCooldown -= Time.deltaTime;
            return false;
        }
    }

    IEnumerator CastSpell()
    {
        Vector2 playerPos = player.position;
        yield return new WaitForSeconds(0.5f);
        Instantiate<GameObject>(lightning, playerPos, Quaternion.identity);
        castCooldown = duration;
    }

    IEnumerator AttackCooldown()
    {
        Vector2 direction = player.position - transform.position;
        direction.Normalize();
        anim.SetFloat("direction", direction.x);
        anim.SetBool("Attacking", true);
        yield return new WaitForSeconds(0.8f);
        anim.SetBool("Attacking", false);
        attackCooldown = duration;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    void Attack()
    {
        if (PlayerNear())
        {
            anim.SetBool("Casting", false);
            if (CanAttack())
            {
                StartCoroutine(AttackCooldown());
            }
            else
            {
                Debug.Log("cant attack");
            }

        }
        else
        {
            anim.SetBool("Attacking", false);
            if (CanCast())
            {
                anim.SetBool("Casting", true);
            }
            else
            {
                anim.SetBool("Casting", false);
            }
        }
    }

    int BossDamage()
    {
        if (PlayerNear())
        {
            return 200;
        }
        else
        {
            return 300;
        }
    }
}
