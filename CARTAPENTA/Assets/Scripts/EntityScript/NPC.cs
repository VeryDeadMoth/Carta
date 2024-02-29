using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPC : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public string[] dialogue;
    private int index;

    public Button contButton;
    public float wordSpeed;
    public bool playerIsClose;

    public delegate void InteractedWith(string name);
    public static event InteractedWith OnDialogueEnded;
    private bool hasBeenTalkedTo;

    private void Start()
    {
        this.hasBeenTalkedTo = false;
    }

    public void NextDialogue()
    {
        index++;
        if (dialogue.Length > index)
        {
            dialogueText.text = dialogue[index];
        }
        else
        {
            
            index = 0;
            dialoguePanel.SetActive(false);
            //LAUNCH QUIZ QUEST HERE
            OnDialogueEnded?.Invoke(this.gameObject.name);

            //MOVE THIS ELSEWHERE WHEN QUIZ IS OVER
            PlayerStateManager player = PlayerStateManager.Instance;
            player.SwitchState(player.idleState);
            
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasBeenTalkedTo)
        {
            hasBeenTalkedTo = true;
            playerIsClose = true;
            dialoguePanel.SetActive(true);
            dialogueText.text = dialogue[0];
            PlayerStateManager player = other.GetComponent<PlayerStateManager>();
            player.SwitchState(player.listeningState);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
        }
    }
}
