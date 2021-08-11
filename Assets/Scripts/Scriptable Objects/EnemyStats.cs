using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Stats", menuName = "Character Stats/New Enemy Stats")]
public class EnemyStats : ScriptableObject
{

    [SerializeField] string characterName = default;
    [Header("Speed")]
    [SerializeField] float walkSpeed = 1.0f;
    [Header("Health")]
    [SerializeField] float maxHealth = 10.0f;
    [Header("Attack")]
    [SerializeField] float attack1Damage = 2.0f;
    [SerializeField] float attack2Damage = 1.0f;
    [Header("Knockback")]
    [SerializeField] float stunTime = 0.8f;
    [SerializeField] float knockbackTime = 0.05f;
    [SerializeField] float knockbackForce = 10.0f;

    #region Properties
    public string GetName => characterName;
    public float GetSpeed => walkSpeed;
    public float GetHealth => maxHealth;
    public float GetAttack1Damage => attack1Damage;
    public float GetAttack2Damage => attack2Damage;
    public float GetStunTime => stunTime;
    public float GetKnockbackTime => knockbackTime;
    public float GetKnockbackForce => knockbackForce;
    #endregion

}
