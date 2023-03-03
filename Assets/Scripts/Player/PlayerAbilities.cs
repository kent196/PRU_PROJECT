using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    private float projectileSpeed = 5.0f;

    Rigidbody2D rigidbody2D;
    Animator anim;

    private float cooldownTime = .5f;
    private float attackCooldown;
    private int attackDamage;

    public int Damage { get { return attackDamage; } private set { attackDamage = value; } }
    // Start is called before the first frame update
    void Start()
    {
        Damage = 2000;
        rigidbody2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        attackCooldown = cooldownTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            Damage += 10;
            Debug.Log(Damage);
        }
        if (CanAttack())
        {
            AimAndShoot();
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
    void AimAndShoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject projectileShooting = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectileShooting.GetComponent<Rigidbody2D>().velocity = GetAttackDirection() * projectileSpeed;
            projectileShooting.transform.Rotate(.0f, .0f, Mathf.Atan2(GetAttackDirection().y, GetAttackDirection().x) * Mathf.Rad2Deg);
            Destroy(projectileShooting, 3.0f);
            anim.SetTrigger("Attacking");
            attackCooldown = cooldownTime;
        }

    }

    Vector2 GetAttackDirection()
    {
        return new Vector2(anim.GetFloat("lastMoveX"), anim.GetFloat("lastMoveY"));
    }
}
