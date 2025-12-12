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

    private void Awake()
    {
        if (panel == null)
        {
            panel = gameObject;
        }
    }

    private void Start()
    {
        
    }

    // Called by PlayerState when hydration reaches 0
    public void ShowReturnHome()
    {
        Debug.Log("ShowReturnHome() called on ReturnHomeUI");

        if (panel != null)
        {
            Debug.Log("Enabling ReturnHome panel: " + panel.name);
            panel.SetActive(true);
        }
        else
        {
            Debug.LogWarning("ReturnHomeUI: panel is NULL!");
        }

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