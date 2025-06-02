using UnityEngine;
using UnityEngine.EventSystems;

public class AtomDragger : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private bool manuallyDragging = false;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        manuallyDragging = false; // Let normal drag take over
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (canvas == null) return;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 localPoint
        );

        rectTransform.anchoredPosition = localPoint;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        manuallyDragging = false;
    }

    public void BeginManualDrag()
    {
        manuallyDragging = true;
    }

    void Update()
    {
        if (manuallyDragging && canvas != null)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.transform as RectTransform,
                Input.mousePosition,
                null,
                out Vector2 localPoint
            );
            rectTransform.anchoredPosition = localPoint;

            // Release on mouse up
            if (Input.GetMouseButtonUp(0))
                manuallyDragging = false;
        }
    }
}