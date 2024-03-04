using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class DialogueSequence
{
    public string[] dialogues;
}

[System.Serializable]
public class NPC : MonoBehaviour
{
    public static GameObject currentNPC;

    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI ProfilName;
    public Image ProfilImage;
    public GameObject contButton;

    public bool playerIsClose;

    public bool playerSpeaking = false; // Public flag for determining who speaks first

    private int currentNPCSequenceIndex = 0;
    private int currentPlayerSequenceIndex = 0;
    private int currentDialogueIndex = 0;

    [Header("NPC Elements")]
    public Sprite NpcImage;
    public string NpcName;
    public DialogueSequence[] npcSequences;

    [Header("Player Elements")]
    public Sprite PlayerImage;
    public string PlayerName;
    public DialogueSequence[] playerSequences;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
            dialoguePanel.SetActive(true);
            PlayerStateManager player = other.GetComponent<PlayerStateManager>();
            player.SwitchState(player.listeningState);
            StartDialogue();
            currentNPC = this.gameObject;
            GameObject.FindObjectOfType<Window_QuestPointer>().OnPlayerCollidedWithNPC();

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
        }
    }

    private void StartDialogue()
    {
        currentNPCSequenceIndex = 0;
        currentPlayerSequenceIndex = 0;
        currentDialogueIndex = 0;
        NextDialogue();
    }

    public void NextDialogue()
    {
        if (!playerSpeaking)
        {
            // Set NPC profile image and name
            ProfilImage.sprite = NpcImage;
            ProfilName.text = NpcName;

            // Display NPC dialogues
            if (currentNPCSequenceIndex >= npcSequences.Length && currentPlayerSequenceIndex >= playerSequences.Length)
            {
                EndDialogue();
                PlayerStateManager player = PlayerStateManager.Instance;
                player.SwitchState(player.idleState);
                dialoguePanel.SetActive(false);
            }
            else
            {
                if (currentDialogueIndex < npcSequences[currentNPCSequenceIndex].dialogues.Length)
                {
                    dialogueText.text = npcSequences[currentNPCSequenceIndex].dialogues[currentDialogueIndex];
                    currentDialogueIndex++;
                }
                else
                {
                    // Switch to player dialogues
                    playerSpeaking = true;
                    currentDialogueIndex = 0;
                    currentNPCSequenceIndex++;
                    NextDialogue();
                }
            }
        }
        else
        {
            // Set player profile image and name
            ProfilImage.sprite = PlayerImage;
            ProfilName.text = PlayerName;

            // Display player dialogues
            if (currentNPCSequenceIndex >= npcSequences.Length && currentPlayerSequenceIndex >= playerSequences.Length)
            {
                EndDialogue();
                PlayerStateManager player = PlayerStateManager.Instance;
                player.SwitchState(player.idleState);
                dialoguePanel.SetActive(false);
            }
            else
            {
                if (currentDialogueIndex < playerSequences[currentPlayerSequenceIndex].dialogues.Length)
                {
                    dialogueText.text = playerSequences[currentPlayerSequenceIndex].dialogues[currentDialogueIndex];
                    currentDialogueIndex++;
                }
                else
                {
                    // Switch to next NPC sequence or reset if reached the end
                    playerSpeaking = false;
                    currentDialogueIndex = 0;
                    currentPlayerSequenceIndex++;
                    NextDialogue();
                }
            }
        }
    }

    private void EndDialogue()
    {
        currentNPCSequenceIndex = 0;
        currentPlayerSequenceIndex = 0;
        currentDialogueIndex = 0;
        // playerSpeaking!!!!!
        // TODO: When dialogue ends !!!!!!!
    }
}