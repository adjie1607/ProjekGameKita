using UnityEngine;
using TMPro;
using System.Collections;

public class DialogSystem : MonoBehaviour
{
    public GameObject dialogPanel;
    public TextMeshProUGUI dialogText;

    [TextArea]
    public string[] dialogLines;

    public float typingSpeed = 0.03f;

    private int index = 0;
    private bool isActive = false;
    private bool isTyping = false;

    void Start()
    {
        dialogPanel.SetActive(false);
    }

    void Update()
    {
        if (!isActive) return;

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            if (isTyping)
            {
                StopAllCoroutines();
                dialogText.text = dialogLines[index];
                isTyping = false;
            }
            else
            {
                NextDialog();
            }
        }
    }

    public void StartDialog()
    {
        dialogPanel.SetActive(true);
        isActive = true;
        index = 0;

        StartCoroutine(TypeText(dialogLines[index]));
    }

    void NextDialog()
    {
        index++;

        if (index < dialogLines.Length)
        {
            StartCoroutine(TypeText(dialogLines[index]));
        }
        else
        {
            EndDialog();
        }
    }

    IEnumerator TypeText(string line)
    {
        isTyping = true;
        dialogText.text = "";

        foreach (char c in line.ToCharArray())
        {
            dialogText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    void EndDialog()
    {
        dialogPanel.SetActive(false);
        isActive = false;
    }
}