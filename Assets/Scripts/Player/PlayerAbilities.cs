using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    private float projectileSpeed = 5.0f;
    private float projectileSpeedX = 5.0f;
    private float projectileSpeedY = 0.0f;

    Rigidbody2D rigidbody2D;
    Animator anim;

    private float cooldownTime = .15f;
    private float attackCooldown;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        attackCooldown = cooldownTime;
    }

    // Update is called once per frame
    void Update()
    {
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
            Vector3 offset = new Vector3(GetAttackDirection().x, GetAttackDirection().y, 0);
            GameObject projectileShooting = Instantiate(projectilePrefab, transform.position + offset/3, Quaternion.identity);
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
