using UnityEngine;
using TMPro;

public class NoteUI : MonoBehaviour
{
    public static NoteUI Instance { get; private set; }

    [Header("UI References")]
    public GameObject panel;              // NotePanel
    public TextMeshProUGUI noteText;      // NoteText inside the panel
    public KeyCode closeKey = KeyCode.C;  // Key to close

    private bool isOpen = false;
    public bool IsOpen => isOpen;

    private void Awake()
    {
        // Singleton so PlayerInventory can call it easily
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        if (panel != null)
            panel.SetActive(false);
    }

    public void OpenNote(string text)
    {
        if (panel == null || noteText == null)
            return;

        isOpen = true;

        panel.SetActive(true);
        noteText.text = text;

        // Pause gameplay & unlock cursor
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseNote()
    {
        if (!isOpen)
            return;

        isOpen = false;

        panel.SetActive(false);

        // Resume gameplay & lock cursor
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // Press C to close
        if (isOpen && Input.GetKeyDown(closeKey))
        {
            CloseNote();
        }
    }
}