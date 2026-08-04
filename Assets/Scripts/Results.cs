using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Results : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI resultText;

    InputAction continueAction;

    private void Start()
    {
        continueAction = InputSystem.actions.FindAction("Jump");

        resultText.text = "Sheep counted: " + PlayerPrefs.GetInt("HighScore");
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
