using System.Collections;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ColorController : MonoBehaviour
{
    enum Color
    {
        dark,
        light,
    }

    [SerializeField] Color color;

    bool invertChecker;

    Color32 darkColor;
    Color32 lightColor;
    SpriteRenderer spriteRenderer;
    Image image;

    private void Start()
    {
        darkColor = ColorManager.instance.darkColor; 
        lightColor = ColorManager.instance.lightColor;
        invertChecker = ColorManager.instance.inverted;
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            image = GetComponent<Image>();
        }

        SetColors();
    }

    private void Update()
    {
        CheckInvert();
    }

    void SetColors()
    {
        if (spriteRenderer != null)
        {
            if (color == Color.dark)
            {
                spriteRenderer.color = darkColor;
            }
            else if (color == Color.light)
            {
                spriteRenderer.color = lightColor;
            }
        }
        else if (image != null)
        {
            if (color == Color.dark)
            {
                image.color = darkColor;
            }
            else if (color == Color.light)
            {
                image.color = lightColor;
            }
        }
        else if (GetComponent<TextMeshProUGUI>() != null)
        {
            TextMeshProUGUI text = GetComponent<TextMeshProUGUI>();

            if (color == Color.dark)
            {
                text.color = darkColor;
            }
            else if (color == Color.light)
            {
                text.color = lightColor;
            }
        }
    }

    void CheckInvert()
    {
        if (invertChecker != ColorManager.instance.inverted)
        {
            if (color == Color.dark)
            {
                color = Color.light;
            }
            else if (color == Color.light)
            {
                color = Color.dark;
            }

            SetColors();
            invertChecker = ColorManager.instance.inverted;
        }
    }
}
