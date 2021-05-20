using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SVPicker : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler {
    public event Action OnChange;
    public Camera cam;

    [SerializeField] private GameObject pickerBackground;

    private RectTransform rectTransform;
    private Rect rect;
    private Vector2 sv;
    private float left;
    private float right;
    private float top;
    private float bottom;

    private void Start() {
        rectTransform = GetComponent<RectTransform>();
        rect = rectTransform.rect;

        left =  -rect.width / 2f;
        right = rect.width / 2f;
        top = rect.height / 2f;
        bottom = -rect.height / 2f;
    }

    public float GetS() {
        return (pickerBackground.transform.localPosition.x / (rect.width / 100f) + 50f) / 100f;
    }

    public float GetV() {
        return (pickerBackground.transform.localPosition.y / (rect.height / 100f) + 50f) / 100f;
    }

    // TODO
    public void SetS(float s) {
        sv.x = s * rect.width - rect.width / 2f;
        SetPickerPosition(sv);
    }

    // TODO
    public void SetV(float v) {
        sv.y = v * rect.height - rect.height / 2f;
        SetPickerPosition(sv);
    }

    private void UpdateSV() {
        sv = Input.mousePosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, sv, cam, out sv);
        SetPickerPosition(sv);
    }

    private void SetPickerPosition(Vector3 position) {
        position.x = Mathf.Clamp(position.x, left, right);
        position.y = Mathf.Clamp(position.y, bottom, top);
        position.z = 0f;

        pickerBackground.transform.localPosition = position;

        OnChange.Invoke();
    }

    public void OnDrag(PointerEventData eventData) {
        UpdateSV();
    }

    public void OnPointerDown(PointerEventData eventData) {
        UpdateSV();
    }

    public void OnPointerUp(PointerEventData eventData) {
        UpdateSV();
    }
}
