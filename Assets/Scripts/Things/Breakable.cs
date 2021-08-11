using UnityEngine;

public class Breakable : MonoBehaviour, IHealth
{

    [SerializeField] float maxHealth = 0.1f;

    private float currentHealth;
    private bool finalBlow;

    #region Cached references
    private Animator animator;
    private LootSpawner lootSpawner;
    #endregion


    private void Awake()
    {
        SetReferences();
        SetValues();
    }

    public void DecreaseHealth(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);

        if (currentHealth == 0)
        {
            finalBlow = true;
            animator.SetTrigger("break");
            lootSpawner.DropLoot();
            EnableColliders(false);
        }

        animator.SetTrigger("hit");
        TimeManager.HitStop(finalBlow);
    }

    private void EnableColliders(bool deactivate)
    {
        foreach (Collider2D colliders in GetComponents<Collider2D>())
        {
            colliders.enabled = deactivate;
        }
    }

    private void SetReferences()
    {
        animator = GetComponent<Animator>();
        lootSpawner = GetComponent<LootSpawner>();
    }

    private void SetValues()
    {
        currentHealth = maxHealth;
    }

}
