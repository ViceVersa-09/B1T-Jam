using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Results : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI resultText;
    [SerializeField] TextMeshProUGUI highscoreText;

    InputAction continueAction;

    private void Start()
    {
        continueAction = InputSystem.actions.FindAction("Jump");

        resultText.text = "Sheep counted: " + PlayerPrefs.GetInt("Score");
        highscoreText.text = "Highscore: " + PlayerPrefs.GetInt("HighScore");
    }

    private void Update()
    {
        CheckSwitch();
    }

    void CheckSwitch()
    {
        if (continueAction.WasPressedThisFrame())
        {
            SceneManager.LoadScene(0);
        }
    }
}
