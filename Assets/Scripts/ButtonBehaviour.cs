using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBehaviour : MonoBehaviour
{
    public static ButtonBehaviour instance;

    [SerializeField] bool dontDestroy;

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
    }

    private void OnLevelWasLoaded(int level)
    {
        gameObject.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Tutorial()
    {
        SceneManager.LoadScene(2);
    }

    public void Options()
    {
        OptionsManager.instance.gameObject.SetActive(true);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void PauseMenu()
    {
        gameObject.SetActive(!gameObject.activeInHierarchy);

        if (!gameObject.activeInHierarchy)
        {
            Time.timeScale = 1;
        }
        else if (gameObject.activeInHierarchy)
        {
            Time.timeScale = 0;
        }
    }
}
