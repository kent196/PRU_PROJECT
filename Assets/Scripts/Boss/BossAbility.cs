using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAbility : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private GameObject lightning;
    [SerializeField] private Transform player;

    private float radius = 0.3f;
    private float castCooldown;
    private float duration = 1f;

    Animator anim;

    public int Damage { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        castCooldown = duration;
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

    IEnumerator CastSpell()
    {
        Vector2 playerPos = player.position;
        yield return new WaitForSeconds(0.5f);
        Instantiate<GameObject>(lightning, playerPos, Quaternion.identity);
        castCooldown = duration;
    }

    void Attack()
    {
        if (PlayerNear())
        {
            anim.SetBool("Attacking",true);
        }
        else
        {
            anim.SetBool("Attacking", false);
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
