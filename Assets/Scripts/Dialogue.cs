using System.Collections;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour {

    private bool isPlayerInRange;
    private bool didDialogueStart;
    private int lineIndex;
    private Player player;
    [SerializeField] private GameObject dialogueMark;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private float typingTime;
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;

    private void Update() {
        if (isPlayerInRange && Input.GetButtonDown("Fire1")) {
            if (!didDialogueStart) {
                StartDialogue();
            }
            else if (dialogueText.text == dialogueLines[lineIndex]) {
                NextDialogueLine();
            }
            else {
                StopAllCoroutines();
                dialogueText.text = dialogueLines[lineIndex];
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.CompareTag("Player")) {
            isPlayerInRange = true;
            dialogueMark.SetActive(true);
            player = collision.gameObject.GetComponent<Player>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            isPlayerInRange = false;
            dialogueMark.SetActive(false);
            player = null;
        }
    }

    private void StartDialogue() { 
        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        dialogueMark.SetActive(false);
        lineIndex = 0;
        Time.timeScale = 0f;
        StartCoroutine(nameof(ShowLine));
        player.SetInDialogue(true);
    }

    private IEnumerator ShowLine() {
        dialogueText.text = string.Empty;

        foreach (char ch in dialogueLines[lineIndex]) {
            dialogueText.text += ch;
            yield return new WaitForSecondsRealtime(typingTime);
        }
    }

    private void NextDialogueLine() {
        lineIndex++;
        if (lineIndex < dialogueLines.Length) {
            StartCoroutine(nameof(ShowLine));
        }
        else {
            didDialogueStart = false;
            dialoguePanel.SetActive(false);
            dialogueMark.SetActive(true);
            Time.timeScale = 1f;
            player.SetInDialogue(false);
        }
    }
}
