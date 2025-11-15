using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ReturnHomeUI : MonoBehaviour
{
    [Header("UI References")]
    public GameObject panel;              // The whole ReturnHomePanel
    public TextMeshProUGUI messageText;   // Dialogue text
    public Button startAgainButton;       // Restart button

    [TextArea]
    public string defaultMessage =
        "I'm tired now. I think I'm going to return home to take a rest.\n" +
        "This is not done. I'll come back to find out the answer.";

    private void Start()
    {
        // Hide panel at the start
        if (panel != null)
            panel.SetActive(false);

        if (startAgainButton != null)
            startAgainButton.onClick.AddListener(OnStartAgainClicked);
    }

    // Called by PlayerState when hydration reaches 0
    public void ShowReturnHome()
    {
        if (panel != null)
            panel.SetActive(true);

        if (messageText != null)
            messageText.text = defaultMessage;

        // Pause game & show cursor for UI
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void OnStartAgainClicked()
    {
        // Unpause
        Time.timeScale = 1f;

        // Optional: lock cursor again for FPS control
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Restart current scene (will change it to the first scene)
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.buildIndex);
    }
}
