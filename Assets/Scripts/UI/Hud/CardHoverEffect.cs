using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;

public class CardHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float hoverDelay = 0.3f;
    public float scaleFactor = 1.3f;
    public float scaleSpeed = 25f;

    private Vector3 originalScale;
    private Coroutine hoverCoroutine;
    private bool isHovering = false;
    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalScale = rectTransform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverCoroutine != null)
        {
            StopCoroutine(hoverCoroutine);
        }
        isHovering = true;
        hoverCoroutine = StartCoroutine(ScaleCard(originalScale * scaleFactor));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (hoverCoroutine != null)
        {
            StopCoroutine(hoverCoroutine);
        }
        isHovering = false;
        hoverCoroutine = StartCoroutine(ScaleCard(originalScale));
    }

    private IEnumerator ScaleCard(Vector3 targetScale)
    {        
        while (rectTransform.localScale != targetScale)
        {
            rectTransform.localScale = Vector3.Lerp(rectTransform.localScale, targetScale, Time.deltaTime * scaleSpeed);
            yield return null;
        }
    }

    private void OnDisable()
    {
        if (rectTransform != null)
        {
            rectTransform.localScale = originalScale;
            isHovering = false;
            if (hoverCoroutine != null)
            {
                StopCoroutine(hoverCoroutine);
            }
        }
    }
}