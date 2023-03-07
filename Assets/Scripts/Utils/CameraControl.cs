using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private GameObject boss;

    CinemachineVirtualCamera camera;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        camera = playerCamera.GetComponent<CinemachineVirtualCamera>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(boss != null)
        {
            anim = boss.GetComponent<Animator>();
            if (anim.GetBool("Dead")==true)
            {
                camera.Priority = 0;
            }
        }
        else
        {
            Debug.Log("no boss");
        }
        
    }

    private void FixedUpdate()
    {
        if(boss == null)
        {
            boss = GameObject.FindWithTag("Boss");
        }
        else
        {
            return;
        }
        
    }
}
