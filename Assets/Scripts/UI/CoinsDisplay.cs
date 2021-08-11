using UnityEngine;
using UnityEngine.UI;

public class CoinsDisplay : MonoBehaviour
{

    [SerializeField] Text currentAmount = default;
    [SerializeField] Inventory playerInventory = default;


    private void Awake()
    {
        currentAmount.text = playerInventory.coins.ToString();
    }

    private void OnEnable()
    {
        playerInventory.OnCoinCollected += UpdateCoins;
    }

    private void OnDisable()
    {
        playerInventory.OnCoinCollected -= UpdateCoins;
    }

    private void UpdateCoins(int amount)
    {
        currentAmount.text = amount.ToString();
    }

}
