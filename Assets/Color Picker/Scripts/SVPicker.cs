using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SVPicker : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public event Action OnChange;
    public Camera cam;

    [SerializeField] private GameObject pickerBackground;

    private Rect rect;
    private Vector3 sv;

    private void Start()
    {
        rect = GetComponent<RectTransform>().rect;
    }

    public float GetS()
    {
        return (pickerBackground.transform.localPosition.x / (rect.width / 100f) + 50f) / 100f;
    }

    public float GetV()
    {
        return (pickerBackground.transform.localPosition.y / (rect.height / 100f) + 50f) / 100f;
    }

    public void SetS(float s)
    {
        sv.x = s * 100f;
        SetPickerPosition(sv);
    }

    public void SetV(float v)
    {
        sv.y = v * 100f;
        SetPickerPosition(sv);
    }

    private void UpdateSV()
    {
        RectTransformUtility.ScreenPointToWorldPointInRectangle(GetComponent<RectTransform>(), Input.mousePosition, cam, out sv);
    }

    private void SetPickerPosition(Vector3 position)
    {
        // TODO: Set correct bounds
        position.x = Mathf.Clamp(position.x, transform.position.x - 1.27f, transform.position.x + 1.27f);
        position.y = Mathf.Clamp(position.y, transform.position.y - 1.85f, transform.position.y + 1.85f);
        position.z = 0f;

        pickerBackground.transform.position = position;

        OnChange.Invoke();
    }

    public void OnDrag(PointerEventData eventData)
    {
        UpdateSV();
        SetPickerPosition(sv);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        UpdateSV();
        SetPickerPosition(sv);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        UpdateSV();
        SetPickerPosition(sv);
    }
}
