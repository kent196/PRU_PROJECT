using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform rectTransform;

    [SerializeField] private float maxScale = 1.3f;
    private bool mouse_over = false;
    private float defaultScale;
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        defaultScale = rectTransform.localScale.x;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (gameObject.CompareTag("PowerUp"))
        {
            AudioManager.Instance.PlaySFX("PowUpBtn", .8f);
        }
        else AudioManager.Instance.PlaySFX("BtnEffect", .8f);
        rectTransform.localScale = new Vector3(maxScale, maxScale, 1);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        rectTransform.localScale = new Vector3(defaultScale, defaultScale, 1);
    }
}
