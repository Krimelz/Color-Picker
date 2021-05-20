using UnityEngine;
using UnityEngine.UI;
using System;

public class ColorPicker : MonoBehaviour {
    public HPicker hPicker;
    public SVPicker svPicker;
    public Image mainColor;
    public Image[] pickers;

    public event Action OnColorChange;

    private void Awake() {
        hPicker.OnChange += () => {
            if (OnColorChange != null) {
                OnColorChange.Invoke();
            }
        };

        svPicker.OnChange += () => {
            if (OnColorChange != null) {
                OnColorChange.Invoke();
            }
        };
    }

    // TODO: Update only on move
    private void Update() {
        SetMainColor();
        SetPickerColor();
    }

    public Color GetColor() {
        float h = hPicker.GetH() / 360f;
        float s = svPicker.GetS();
        float v = svPicker.GetV();

        return Color.HSVToRGB(h, s, v);
    }

    public void SetColor(Color color) {
        float h = 0f;
        float s = 0f;
        float v = 0f;

        Color.RGBToHSV(color, out h, out s, out v);

        hPicker.SetH(h);
        svPicker.SetS(s);
        svPicker.SetV(v);
    }

    private void SetPickerColor() {
        foreach (var picker in pickers) {
            picker.color = GetColor();
        }
    }

    private void SetMainColor() {
        float h = hPicker.GetH() / 360f;
        float s = 1f;
        float v = 1f;

        mainColor.color = Color.HSVToRGB(h, s, v);
    }
}
