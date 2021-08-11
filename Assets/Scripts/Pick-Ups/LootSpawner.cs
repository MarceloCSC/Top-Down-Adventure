using System.Collections.Generic;
using UnityEngine;

public class LootSpawner : MonoBehaviour
{

    public enum LootByTag
    {
        Coin,
        Health,
        Mana
    };


    [SerializeField] List<LootByTag> lootToDrop = default;
    [SerializeField] int totalLootAmount = 1;
    [SerializeField] bool amountIsRandom = false;
    [SerializeField] bool lootIsRandom = false;


    public void DropLoot()
    {
        if (amountIsRandom)
        {
            int maxLootAmount = totalLootAmount + 1;
            totalLootAmount = Random.Range(1, maxLootAmount);
        }

        if (totalLootAmount < lootToDrop.Count || lootIsRandom)
        {
            for (int i = 0; i < totalLootAmount; i++)
            {
                int randomLoot = Random.Range(0, lootToDrop.Count);
                var loot = lootToDrop[randomLoot];
                Spawn(loot);
            }
        }

        else if (totalLootAmount % lootToDrop.Count == 0)
        {
            foreach (LootByTag loot in lootToDrop)
            {
                for (int i = 0; i < totalLootAmount / lootToDrop.Count; i++)
                {
                    Spawn(loot);
                }
            }
        }
        else
        {
            bool isRemainderOne = totalLootAmount % lootToDrop.Count == 1;
            int roundedDown = totalLootAmount / lootToDrop.Count;
            int roundedUp = roundedDown + 1;

            for (int i = 0; i < lootToDrop.Count; i++)
            {
                var loot = lootToDrop[i];
                int amountToDrop;

                if (isRemainderOne)
                {
                    amountToDrop = i == lootToDrop.Count - 1 ? roundedUp : roundedDown;
                }
                else
                {
                    amountToDrop = i == lootToDrop.Count - 1 ? roundedDown : roundedUp;
                }

                for (int j = 0; j < amountToDrop; j++)
                {
                    Spawn(loot);
                }
            }
        }
    }

    private void Spawn(LootByTag loot)
    {
        Vector3 randomPosition = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        ObjectPooler.Instance.SpawnObject(loot.ToString(), transform.position + randomPosition, Quaternion.identity);
    }

}
