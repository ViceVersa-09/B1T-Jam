using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    public static OptionsManager instance;

    [SerializeField] Slider masterSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sFXSlider;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentSceneIndex == 0)
        {
            masterSlider.value = PlayerPrefs.GetFloat("Master", 1);
            musicSlider.value = PlayerPrefs.GetFloat("Music", 1);
            sFXSlider.value = PlayerPrefs.GetFloat("SFX", 1);
        }

        gameObject.SetActive(false);
    }

    public void CloseOptions()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.buttonClip);
        gameObject.SetActive(false);
    }

    public void InvertColors()
    {
        ColorManager.instance.InvertColors();
    }

    public void VolumeSliders()
    {
        PlayerPrefs.SetFloat("Master", masterSlider.value);
        PlayerPrefs.SetFloat("Music", musicSlider.value);
        PlayerPrefs.SetFloat("SFX", sFXSlider.value);
    }
}
