using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ButtonBehaviour : MonoBehaviour
{
    public static ButtonBehaviour instance;

    [SerializeField] bool dontDestroy;
    [SerializeField] GameObject pauseParent;
    [SerializeField] TextMeshProUGUI highScoreText;

    InputAction pauseAction;

    private void Awake()
    {
        if (dontDestroy)
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

        pauseAction = InputSystem.actions.FindAction("Pause");

        if (highScoreText != null)
        {
            highScoreText.text = "Highscore: " + PlayerPrefs.GetInt("HighScore");
        }
    }

    private void Update()
    {
        if (pauseAction.WasPressedThisFrame() && pauseParent != null)
        {
            PauseMenu();
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        if (dontDestroy)
        {
            pauseParent.SetActive(false);
        }
    }

    public void StartGame()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.buttonClip);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Tutorial()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.buttonClip);

        if (SceneManager.sceneCountInBuildSettings >= 4)
        {
            SceneManager.LoadScene(3);
        }
    }

    public void Options()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.buttonClip);
        OptionsManager.instance.gameObject.SetActive(true);
    }

    public void MainMenu()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.buttonClip);
        SceneManager.LoadScene(0);
    }

    public void PauseMenu()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.buttonClip);
        pauseParent.SetActive(!pauseParent.activeInHierarchy);

        if (!pauseParent.activeInHierarchy)
        {
            Time.timeScale = 1;
        }
        else if (pauseParent.activeInHierarchy)
        {
            Time.timeScale = 0;
        }
    }
}
