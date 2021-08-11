using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealth
{

    public event Action<float> OnHealthChanged = delegate { };


    private float maxHealth;
    private float currentHealth;

    private bool finalBlow;

    #region Properties
    public bool HealthIsFull => currentHealth == maxHealth;
    public float DebugHealth { set => currentHealth = value; }
    #endregion

    #region Cached references
    private PlayerController myController;
    private PlayerDefense myDefense;
    private Animator animator;
    #endregion


    private void Awake()
    {
        SetReferences();
        SetValues();
    }

    private void Start()
    {
        currentHealth = myController.PlayerData.Health;
    }

    public void DecreaseHealth(float amount)
    {
        if (!myDefense.IsInvincible)
        {
            ChangeHealth(-amount);

            if (currentHealth == 0)
            {
                finalBlow = true;
                animator.SetTrigger("getKilled");
            }

            animator.SetTrigger("getHurt");
            TimeManager.HitStop(finalBlow);
        }
    }

    public void ChangeHealth(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        OnHealthChanged(currentHealth);
    }

    private void SetReferences()
    {
        myController = GetComponent<PlayerController>();
        myDefense = GetComponent<PlayerDefense>();
        animator = GetComponent<Animator>();
    }

    private void SetValues()
    {
        maxHealth = myController.myStats.GetHealth;
    }

}
