using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicators : MonoBehaviour
{
    [SerializeField] private GameObject indicator;
    [SerializeField] private GameObject player;

    SpriteRenderer sp;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        sp = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sp.isVisible == false)
        {
            if (indicator.activeSelf == false)
            {
                Debug.Log("1");
                indicator.SetActive(true);
            }

            Vector2 direction = player.transform.position - transform.position;

            RaycastHit2D ray = Physics2D.Raycast(transform.position, direction, Mathf.Infinity, LayerMask.GetMask("CameraBoundary"));

            if (ray.collider != null)
            {
                Debug.Log("2");
                indicator.transform.position = ray.point;
            }
        }
        else
        {
            if (indicator.activeSelf == true)
            {
                Debug.Log("3");
                indicator.SetActive(false);
            }
        }
    }
}
