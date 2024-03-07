using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public Button continueButton;

    private void Start()
    {
        // Add listener to the continue button
        continueButton.onClick.AddListener(ContinueDialogue);
    }

    private void ContinueDialogue()
    {
        // Check if currentNPC is not null
        if (NPC.currentNPC != null)
        {
            // Get the NPC script attached to the currentNPC GameObject
            NPC npc = NPC.currentNPC.GetComponent<NPC>();
            // Call the NextDialogue function of the NPC
            npc.NextDialogue();
        }
    }
}
