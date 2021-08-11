using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewInventory", menuName = "Inventory/New Inventory")]
public class Inventory : ScriptableObject
{
    
    public event Action<int> OnCoinCollected = delegate { };

    public Item currentItem;
    public List<Item> items = new List<Item>();
    public int numberOfKeys;
    public int maxCoins = 100;
    public int coins;


    public void AddItem(Item itemToAdd)
    {
        if (itemToAdd.isKey)
        {
            numberOfKeys++;
        }
        else
        {
            if (!items.Contains(itemToAdd))
            {
                items.Add(itemToAdd);
            }
        }
    }

    public void AddCoins(int amount)
    {
        coins = Mathf.Clamp(coins + amount, 0, maxCoins);
        OnCoinCollected(coins);
    }

}
