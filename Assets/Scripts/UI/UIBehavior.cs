using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBehavior : MonoBehaviour
{
    public GameObject sm;

    private void Start()
    {
        sm = GameObject.FindGameObjectWithTag("SpawnManager");
    }

    public void PowerUpSelected()
    {
        sm.GetComponent<SpawnManager>().PowerUpSelected();
    }
}
