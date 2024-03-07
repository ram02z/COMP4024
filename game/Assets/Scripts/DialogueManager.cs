using UnityEngine;

/*
 * This class is responsible for registering esc key
 * presses and subsequently activating the dialogue popup
 * which asks the user to confirming quitting the game
 */
public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueUI;

    /*
     * Set the dialogue UI object tp be inactive when the scene starts
     */
    void Start()
    {
        if (dialogueUI != null)
        {
            dialogueUI.SetActive(false);
        }
    }

    /*
     * Check for esc key presses then call ToggleDialogueUI
     * if the key is pressed
     */
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleDialogueUI();
        }
    }

    /*
     * Set the dialogue UI object to be active on screen
     */
    void ToggleDialogueUI()
    {
        if (dialogueUI != null)
        {
            dialogueUI.SetActive(!dialogueUI.activeSelf);
        }
    }
}

