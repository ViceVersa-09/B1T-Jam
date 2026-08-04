using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Timer")]
    [SerializeField] Slider timerSlider;
    [SerializeField] float time;

    [Header("Score")]
    [SerializeField] public TextMeshProUGUI scoreText;

    public int jumps;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        scoreText.text = jumps.ToString();
        timerSlider.maxValue = time;
        timerSlider.value = timerSlider.maxValue;
    }

    private void Update()
    {
        UpdateTimer();

        if (time <= 0)
        {
            EndGame();
        }
    }

    void UpdateTimer()
    {
        timerSlider.value = time;
        time -= Time.deltaTime;
    }

    public void Score()
    {
        jumps++;
        scoreText.text = jumps.ToString();

        if (jumps > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", jumps);
        }
    }

    void EndGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
