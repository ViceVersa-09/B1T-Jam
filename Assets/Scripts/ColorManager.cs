using UnityEngine;

public class ColorManager : MonoBehaviour
{
    public static ColorManager instance;

    [SerializeField] public Color32 darkColor;
    [SerializeField] public Color32 lightColor;

    public bool inverted;

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
    }

    public void InvertColors()
    {
        inverted = !inverted;
    }
}
