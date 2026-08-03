using TMPro;
using UnityEngine;
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
    }
}
