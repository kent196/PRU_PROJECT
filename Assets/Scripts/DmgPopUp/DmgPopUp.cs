using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Principal;
using TMPro;
using UnityEngine;

public class DmgPopUp : MonoBehaviour
{
    //Create dmgPopUp
    public static DmgPopUp Create(Vector3 position, int dmg)
    {
        Transform dmgPopUpTransform = Instantiate(GameAssets.i.pfDmgPopUp, position, Quaternion.identity);

        DmgPopUp dmgPopUp = dmgPopUpTransform.GetComponent<DmgPopUp>();
        dmgPopUp.Setup(dmg);
        return dmgPopUp;
    }


    private TextMeshPro textMesh;
    private float disappearTimer;
    private Color textColor;

    // Start is called before the first frame update
    private void Awake()
    {
        textMesh = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    public void Setup(int dmg)
    {
        textMesh.SetText(dmg.ToString());
        textColor = textMesh.color;
        disappearTimer = .5f;
    }
    private void Update()
    {
        float moveSpd = 1f;
        transform.position += new Vector3(0, moveSpd) * Time.deltaTime;
        disappearTimer -= Time.deltaTime;
        if (disappearTimer < 0)
        {
            float disappearSpeed = 3f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if (textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
