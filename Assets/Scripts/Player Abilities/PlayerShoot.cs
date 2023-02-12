using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject projectilePrefab;
    private float projectileSpeed = 5.0f;
    private float projectileSpeedX = 5.0f;
    private float projectileSpeedY = 0.0f;
    public Rigidbody2D rigidbody2D;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetButtonDown("Fire1"))
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileSpeedX, projectileSpeedY);
        }*/
        AimAndShoot();
    }

    void AimAndShoot()
    {
        Vector2 mouseCursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mouseCursorPos - rigidbody2D.position;
        lookDir.Normalize();
        if (Input.GetMouseButtonDown(0))
        {
            GameObject projectileShooting = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectileShooting.GetComponent<Rigidbody2D>().velocity = lookDir * projectileSpeed;
            projectileShooting.transform.Rotate(.0f, .0f, Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg);
            Destroy(projectileShooting, 3.0f);
        }
    }
}
