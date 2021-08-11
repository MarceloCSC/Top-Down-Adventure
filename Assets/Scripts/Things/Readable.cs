using UnityEngine;

public class Readable : Interactable
{

    [TextArea]
    [SerializeField] string text = default;


    private void Update()
    {
        if (Input.GetButtonDown("Interact") && playerInRange)
        {
            if (dialogueBox.activeInHierarchy)
            {
                dialogueBox.SetActive(false);
                ContextClueEnabled(true);
            }
            else
            {
                dialogueBox.SetActive(true);
                dialogueText.text = text;
                ContextClueEnabled(false);
            }
        }
    }

}
