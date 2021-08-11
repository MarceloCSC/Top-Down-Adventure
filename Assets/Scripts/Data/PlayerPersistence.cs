using UnityEngine;

internal class PlayerPersistence
{

    internal static PlayerData GetData()
    {
        if (PlayerPrefs.HasKey("Health") == false || PlayerPrefs.GetFloat("Health") == 0)
        {
            return GetNewPlayerData();
        }

        return LoadFromPlayerPrefs();
    }

    public static PlayerData GetNewPlayerData()
    {
        return new PlayerData()
        {
            Health = GameObject.FindWithTag("Player").GetComponent<PlayerController>().myStats.GetHealth,
            Mana = GameObject.FindWithTag("Player").GetComponent<PlayerController>().myStats.GetMana
        };
    }

    private static PlayerData LoadFromPlayerPrefs()
    {
        float health = PlayerPrefs.GetFloat("Health");
        float mana = PlayerPrefs.GetFloat("Mana");

        return new PlayerData()
        {
            Health = health,
            Mana = mana
        };
    }

    internal static void SaveData(PlayerData playerData)
    {
        PlayerPrefs.SetFloat("Health", playerData.Health);
        PlayerPrefs.SetFloat("Mana", playerData.Mana);
    }

}
