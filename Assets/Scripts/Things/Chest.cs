using UnityEngine;

public class Chest : Interactable
{

    [SerializeField] Item contents = default;
    [SerializeField] Inventory playerInventory = default;

    private bool isOpen;

    #region Cached references
    private Animator animator;
    #endregion


    private void Update()
    {
        if (Input.GetButtonDown("Interact") && playerInRange)
        {
            if (!isOpen)
            {
                OpenChest();
            }
            else
            {
                AlreadyOpened();
            }
        }
    }

    private void OpenChest()
    {
        isOpen = true;
        animator.SetTrigger("open");

        dialogueBox.SetActive(true);
        dialogueText.text = contents.itemDescription;

        playerInventory.AddItem(contents);
        playerInventory.currentItem = contents;
    }

    private void AlreadyOpened()
    {
        if (dialogueBox.activeInHierarchy)
        {
            dialogueBox.SetActive(false);
        }

        playerInventory.currentItem = null;
    }

    protected override void SetReferences()
    {
        base.SetReferences();
        animator = GetComponent<Animator>();
    }

}
