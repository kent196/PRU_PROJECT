using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform player;

    private float lerpSpeed = 2f;
    private Vector3 offset;
    private Vector3 playerPos;
    void Start()
    {
        offset = new Vector3(0, 0, -10);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        playerPos = player.position + offset;
        //transform.position = Vector3.Lerp(transform.position, playerPos, lerpSpeed * Time.deltaTime);
        transform.position = playerPos;
    }
}
