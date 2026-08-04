using UnityEngine;
using UnityEngine.InputSystem;

public class ColorManager : MonoBehaviour
{
    public static ColorManager instance;

    [SerializeField] public Color32 darkColor;
    [SerializeField] public Color32 lightColor;

    public bool inverted;

    InputAction invertAction;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        invertAction = InputSystem.actions.FindAction("Invert");
    }

    private void Update()
    {
        if (invertAction.WasPressedThisFrame())
        {
            InvertColors();
        }
    }

    public void InvertColors()
    {
        inverted = !inverted;
    }
}
