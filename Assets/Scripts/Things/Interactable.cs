using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{

    [SerializeField] protected bool usingContextClue = false;

    protected bool playerInRange;

    #region Cached references
    protected GameObject dialogueBox;
    protected Text dialogueText;
    protected ContextClue contextClue;
    #endregion


    protected virtual void Awake()
    {
        SetReferences();
    }

    protected virtual void Start()
    {
        dialogueBox.SetActive(false);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            playerInRange = true;
            ContextClueEnabled(true);
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            playerInRange = false;
            dialogueBox.SetActive(false);
            ContextClueEnabled(false);
        }
    }

    protected virtual void ContextClueEnabled(bool enabled)
    {
        if (usingContextClue)
        {
            if (enabled)
            {
                contextClue.Enable();
            }
            else
            {
                contextClue.Disable();
            }
        }
    }

    protected virtual void SetReferences()
    {
        dialogueBox = GameObject.FindWithTag("UI Dialogue");
        dialogueText = dialogueBox.GetComponentInChildren<Text>();

        if (usingContextClue)
        {
            contextClue = GameObject.FindWithTag("Player").GetComponentInChildren<ContextClue>();
        }
    }

}
