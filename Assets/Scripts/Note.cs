using UnityEngine;

public class Note : MonoBehaviour
{
    [TextArea]
    public string text;

    public void Read()
    {
        NoteUI.Instance.OpenNote(text);
    }
}