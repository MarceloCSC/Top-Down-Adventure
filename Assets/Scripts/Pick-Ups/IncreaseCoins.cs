
using UnityEngine;

public class IncreaseCoins : PickUpable
{

    [SerializeField] int coinValue = 1;
    [SerializeField] Inventory playerInventory = default;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            if (playerInventory.coins < playerInventory.maxCoins)
            {
                playerInventory.AddCoins(coinValue);
                gameObject.SetActive(false);
            }
            else { return; }
        }
    }

}
