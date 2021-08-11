using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack Stats", menuName = "Character Stats/New Attack Stats")]
public class AttackStats : ScriptableObject
{

    [SerializeField] Attack[] attacks = default;


    public Dictionary<int, Attack> BuildDictionary()
    {
        var dictionary = new Dictionary<int, Attack>();

        foreach (Attack attack in attacks)
        {
            dictionary.Add((int)attack.type, attack);
        }

        return dictionary;
    }

}

[System.Serializable]
public struct Attack
{
    public AttackType type;
    public float damage;
    public string animation;
    public float duration;
}

public enum AttackType
{
    firstAttack = 0,
    secondAttack = 2,
    finalAttack = 4
}
