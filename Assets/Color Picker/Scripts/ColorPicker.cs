using UnityEngine;
using UnityEngine.UI;
using System;

public class ColorPicker : MonoBehaviour
{
    public HPicker hPicker;
    public SVPicker svPicker;
    public Image mainColor;
    public Image[] pickers;

    public event Action OnColorChange;

    private void Awake()
    {
        hPicker.OnChange += () =>
        {
            if (OnColorChange != null)
                OnColorChange.Invoke();
        };

        svPicker.OnChange += () =>
        {
            if (OnColorChange != null)
                OnColorChange.Invoke();
        };
    }

    private void Update()
    {
        SetMainColor();
        SetPickerColor();
    }

    public Color GetColor()
    {
        float h = hPicker.GetH();
        float s = svPicker.GetS();
        float v = svPicker.GetV();

        return ConvertToRGB(h, s, v);
    }

    public void SetColor(Color color)
    {
        Vector3 hsv = ConvertToHSV(color);

        hPicker.SetH(hsv.x);
        svPicker.SetS(hsv.y);
        svPicker.SetV(hsv.z);
    }

    private void SetPickerColor()
    {
        foreach (var picker in pickers)
        {
            picker.color = GetColor();
        }
    }

    private void SetMainColor()
    {
        float h = hPicker.GetH();
        float s = 1f;
        float v = 1f;

        mainColor.color = ConvertToRGB(h, s, v);
    }

    private Color ConvertToRGB(float h, float s, float v)
    {
        float c = v * s;
        float x = c * (1f - Mathf.Abs((h / 60f) % 2 - 1f));
        float m = v - c;

        float r;
        float g;
        float b;

        if (h >= 0 && h < 60)
        {
            r = c;
            g = x;
            b = 0f;
        }
        else if (h >= 60 && h < 120)
        {
            r = x;
            g = c;
            b = 0f;
        }
        else if (h >= 120 && h < 180)
        {
            r = 0f;
            g = c;
            b = x;
        }
        else if (h >= 180 && h < 240)
        {
            r = 0f;
            g = x;
            b = c;
        }
        else if (h >= 240 && h < 300)
        {
            r = x;
            g = 0f;
            b = c;
        }
        else
        {
            r = c;
            g = 0f;
            b = x;
        }

        r += m;
        g += m;
        b += m;

        return new Color(r, g, b);
    }

    private Vector3 ConvertToHSV(Color color)
    {
        float r = color.r / 255f;
        float g = color.g / 255f;
        float b = color.b / 255f;

        float cMax = Mathf.Max(r, g, b);
        float cMin = Mathf.Min(r, g, b);

        float delta = cMax - cMin;

        float h;
        float s;
        float v;

        if (cMax == r)
        {
            h = 60f * ((g - b) / delta) % 6;
        }
        else if (cMax == g)
        {
            h = 60f * ((b - r) / delta) + 2;
        }
        else if (cMax == b)
        {
            h = 60f * ((r - g) / delta) + 4;
        }
        else
        {
            h = 0f;
        }

        if (cMax == 0f)
        {
            s = 0f;
        }
        else
        {
            s = delta / cMax;
        }

        v = cMax;

        return new Vector3(h, s, v);
    }
}
