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
        anim = boss.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(boss != null)
        {
            if (anim.GetBool("Dead"))
            {
                camera.Priority = 0;
            }
        }
        
    }
}
