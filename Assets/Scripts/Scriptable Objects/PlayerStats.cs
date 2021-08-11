using UnityEngine;

[CreateAssetMenu(fileName = "Player Stats", menuName = "Character Stats/New Player Stats")]
public class PlayerStats : ScriptableObject
{

    [Header("Speed")]
    [SerializeField] float walkSpeed = 5.0f;
    [SerializeField] float dashSpeed = 30.0f;
    [Header("Health")]
    [SerializeField] float maxHealth = 20.0f;
    [Header("Stamina")]
    [SerializeField] float maxStamina = 10.0f;
    [SerializeField] float costPerUse = 2.5f;
    [Header("Mana")]
    [SerializeField] float maxMana = 10.0f;
    [Header("Knockback")]
    [SerializeField] float stunTime = 0.5f;
    [SerializeField] float knockbackTime = 0.05f;
    [SerializeField] float knockbackForce = 10.0f;

    #region Properties
    public float GetSpeed => walkSpeed;
    public float GetDashSpeed => dashSpeed;
    public float GetHealth => maxHealth;
    public float GetStamina => maxStamina;
    public float StaminaCost => costPerUse;
    public float GetMana => maxMana;
    public float GetStunTime => stunTime;
    public float GetKnockbackTime => knockbackTime;
    public float GetKnockbackForce => knockbackForce;
    #endregion

}
