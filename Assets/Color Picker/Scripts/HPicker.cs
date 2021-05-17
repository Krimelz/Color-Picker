using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class HPicker : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public event Action OnChange;
    public Camera cam;

    [SerializeField] private GameObject pickerBackground;

    private Rect rect;
    private float h;

    private void Start()
    {
        rect = GetComponent<RectTransform>().rect;
    }

    public float GetH()
    {
        return pickerBackground.transform.localPosition.y / (rect.height / 360f);
    }

    public void SetH(float h)
    {
        this.h = h;
        SetPickerPosition(new Vector2(transform.position.x, h));
    }

    private void UpdateH()
    {
        h = Input.mousePosition.y;
    }

    private void SetPickerPosition(Vector3 position)
    {
        // TODO: Set correct bounds
        position.x = transform.position.x;
        position.y = Mathf.Clamp(position.y, transform.position.y, transform.position.y + 3.5f);
        position.z = 0f;

        pickerBackground.transform.position = position;

        OnChange.Invoke();
    }

    public void OnDrag(PointerEventData eventData)
    {
        UpdateH();
        SetPickerPosition(new Vector3(transform.position.x, h));
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        UpdateH();
        SetPickerPosition(new Vector3(transform.position.x, h));
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        UpdateH();
        SetPickerPosition(new Vector3(transform.position.x, h));
    }
}
