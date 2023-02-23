using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private Vector3 playerPosition;

    Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        offset = new Vector3(0, 0, -10);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(player != null)
        {
            playerPosition = player.transform.position + offset;
            transform.position = Vector3.Lerp(transform.position, playerPosition, 3 * Time.deltaTime);
        }
        
    }
}
