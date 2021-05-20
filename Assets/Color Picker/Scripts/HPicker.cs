using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class HPicker : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler {
    public event Action OnChange;
    public Camera cam;

    [SerializeField] private GameObject pickerBackground;

    private RectTransform rectTransform;
    private Rect rect;
    private float top;
    private float bottom;

    private void Start() {
        rectTransform = GetComponent<RectTransform>();
        rect = rectTransform.rect;

        top = rect.height;
        bottom = 0f;
    }

    public float GetH() {
        return pickerBackground.transform.localPosition.y / (rect.height / 360f);
    }

    // TODO
    public void SetH(float h) {
        float hue = h * rect.height;
        SetPickerPosition(new Vector2(transform.position.x, hue));
    }

    private void UpdateH() {
        Vector2 hue = new Vector2(0f, Input.mousePosition.y);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, hue, cam, out hue);
        SetPickerPosition(hue);
    }

    private void SetPickerPosition(Vector3 position) {
        position.x = 0f;
        position.y = Mathf.Clamp(position.y, bottom, top);
        position.z = 0f;

        pickerBackground.transform.localPosition = position;

        OnChange.Invoke();
    }

    public void OnDrag(PointerEventData eventData) {
        UpdateH();
    }

    public void OnPointerDown(PointerEventData eventData) {
        UpdateH();
    }

    public void OnPointerUp(PointerEventData eventData) {
        UpdateH();
    }
}
