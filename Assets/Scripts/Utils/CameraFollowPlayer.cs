using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 minValue, maxValue;

    private Vector3 playerPosition;

    Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector3(0, 0, -10);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerPosition = player.position + offset;

        Vector3 boundPosition = new Vector3(
            Mathf.Clamp(playerPosition.x, minValue.x, maxValue.x),
            Mathf.Clamp(playerPosition.y, minValue.y, maxValue.y),
            Mathf.Clamp(playerPosition.z, minValue.z, maxValue.z));

        transform.position = Vector3.Lerp(transform.position, boundPosition, 3 * Time.deltaTime);
    }
}
