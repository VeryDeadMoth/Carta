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



    public void NextDialogue()
    {
        index++;
        if (dialogue.Length > index)
        {
            dialogueText.text = dialogue[index];
        }
        else
        {
            //LAUNCH QUIZ QUEST HERE
            PlayerStateManager player = PlayerStateManager.Instance;
            player.SwitchState(player.idleState);
            index = 0;
            dialoguePanel.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

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
