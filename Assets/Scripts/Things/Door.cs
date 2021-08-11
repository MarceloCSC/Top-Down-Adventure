using UnityEngine;

public enum DoorType
{
    locked,
    unlocked,
    buttonTriggered
}

public class Door : Interactable
{

    [SerializeField] DoorType doorType = default;
    [SerializeField] Inventory playerInventory = default;
    [TextArea]
    [SerializeField] string text = default;

    private bool isOpen;

    #region Cached references
    private Animator animator;
    private BoxCollider2D boxCollider;
    #endregion


    private void Update()
    {
        if (Input.GetButtonDown("Interact") && playerInRange)
        {
            if (doorType == DoorType.locked)
            {
                if (!isOpen && playerInventory.numberOfKeys > 0)
                {
                    playerInventory.numberOfKeys--;
                    UnlockDoor();
                }
                else
                {
                    if (dialogueBox.activeInHierarchy)
                    {
                        dialogueBox.SetActive(false);
                    }
                    else
                    {
                        dialogueBox.SetActive(true);
                        dialogueText.text = text;
                    }
                }
            }
        }
    }

    private void UnlockDoor()
    {
        animator.SetBool("isOpen", true);
        boxCollider.enabled = false;
        isOpen = true;
    }

    protected override void SetReferences()
    {
        base.SetReferences();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

}
