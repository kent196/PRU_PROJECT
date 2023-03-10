using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private AudioSource audioSource;
    private float projectileSpeed = 5.0f;

    AudioClip spearThrow;
    Rigidbody2D rigidbody2D;
    Animator anim;

    private float cooldownTime = .5f;
    private float attackCooldown;
    private int attackDamage;


    public int Damage { get { return attackDamage; } set { attackDamage = value; } }
    private void Awake()
    {
        Damage = 50;

    }
    // Start is called before the first frame update
    void Start()
    {
        spearThrow = Resources.Load<AudioClip>("SFX/spearThrow5");
        audioSource = GetComponent<AudioSource>();
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
            AudioManager.Instance.PlaySFX("Fire", 0.1f);
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
