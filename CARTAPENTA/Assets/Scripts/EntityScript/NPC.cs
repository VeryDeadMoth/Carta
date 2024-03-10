using System;
using UnityEngine.SceneManagement;
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

    public GameObject checkMark;

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

    public bool isSpriteFacingRight;
    public SpriteRenderer spriteRenderer;

    public Animator animator;

    private void Start()
    {
        this.hasBeenTalkedTo = false;
        // Subscribe to the event
        OnDialogueEnded += OnDialogueEndHandler;
    }

    private void OnDialogueEndHandler(string name)
    {
        // Check if the event is for this NPC
        if (name == gameObject.name)
        {
            EndDialogue();
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe from the event
        OnDialogueEnded -= OnDialogueEndHandler;
    }

    public delegate void InteractedWith(string name);
    public static event InteractedWith OnDialogueEnded;
    private bool hasBeenTalkedTo;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasBeenTalkedTo)
        {
            hasBeenTalkedTo = true;
            playerIsClose = true;
            dialoguePanel.SetActive(true);
            PlayerStateManager player = other.GetComponent<PlayerStateManager>();

            //Flip sprite towards player
            this.spriteRenderer.flipX = isSpriteFacingRight ^ player.transform.position.x >= this.gameObject.transform.position.x;

            player.SwitchState(player.listeningState);
            StartDialogue();
            currentNPC = this.gameObject;
            GameObject.FindObjectOfType<Window_QuestPointer>().OnPlayerCollidedWithNPC();


            animator.ResetTrigger("Talk");
            animator.SetTrigger("Talk");
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
                // Invoke the event
                OnDialogueEnded?.Invoke(gameObject.name);
                animator.ResetTrigger("Idle");
                animator.SetTrigger("Idle");
                //PlayerStateManager player = PlayerStateManager.Instance;
                //player.SwitchState(player.idleState);
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
                // Invoke the event
                OnDialogueEnded?.Invoke(gameObject.name);
                //PlayerStateManager player = PlayerStateManager.Instance;
                //player.SwitchState(player.idleState);
                animator.ResetTrigger("Idle");
                animator.SetTrigger("Idle");
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
        if (currentNPC.name == "NPC0")
        {
            if (checkMark != null)
                checkMark.SetActive(true);
            SceneManager.LoadScene("Game");

        }
        else
        {
            // Retrieve the QuizHandler instance

            QuizHandler quizHandler = FindObjectOfType<QuizHandler>();
            if (quizHandler != null)
            {
                // Start the quiz mode
                quizHandler.StartQuizMode(gameObject.name);

            }

        }

    }
}

