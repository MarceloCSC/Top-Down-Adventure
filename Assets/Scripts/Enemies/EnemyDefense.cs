using System.Collections;
using UnityEngine;

public class EnemyDefense : MonoBehaviour, IHealth, IDefense
{

    public float DebugHealth { set => currentHealth = value; }

    private float currentHealth;
    private float maxHealth;
    private float stunTime;
    private float knockbackTime;
    private float knockbackForce;

    private bool isInvincible;
    private bool finalBlow;

    #region Cached references
    private Enemy enemy;
    private Rigidbody2D enemyRigidbody;
    private Animator animator;
    #endregion


    private void Awake()
    {
        SetReferences();
        SetValues();
    }

    private void OnEnable()
    {
        currentHealth = maxHealth;
    }

    public void DecreaseHealth(float amount)
    {
        if (!isInvincible)
        {
            currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);

            if (currentHealth == 0)
            {
                finalBlow = true;
                StartCoroutine(enemy.Die());
            }

            animator.SetTrigger("getHurt");
            TimeManager.HitStop(finalBlow);
        }
    }

    public bool IsDefending()
    {
        return false;
    }

    public void Knockback(Transform other)
    {
        if (!isInvincible)
        {
            enemy.ChangeState(EnemyState.stun);

            Vector2 difference = transform.position - other.position;
            difference = difference.normalized * knockbackForce;
            enemyRigidbody.AddForce(difference, ForceMode2D.Impulse);

            StartCoroutine(GetStunned());
        }
    }

    private IEnumerator GetStunned()
    {
        isInvincible = true;

        yield return new WaitForSeconds(knockbackTime);

        isInvincible = false;
        enemyRigidbody.velocity = Vector2.zero;

        yield return new WaitForSeconds(stunTime);

        enemy.ChangeState(EnemyState.idle);
    }

    private void SetReferences()
    {
        enemy = GetComponent<Enemy>();
        enemyRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void SetValues()
    {
        maxHealth = enemy.enemyStats.GetHealth;
        stunTime = enemy.enemyStats.GetStunTime;
        knockbackTime = enemy.enemyStats.GetKnockbackTime;
        knockbackForce = enemy.enemyStats.GetKnockbackForce;
    }

}
