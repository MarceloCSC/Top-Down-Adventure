using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    private float attack1Damage;
    private float attack2Damage;

    [SerializeField] float attack1Duration = 0.58f;
    [SerializeField] float attack1Cooldown = 0.5f;
    [SerializeField] float attack2Duration = 1f;
    [SerializeField] float attack2Cooldown = 0.5f;
    private float currentAttackDamage;

    #region Cached references
    private Enemy enemy;
    private DamageDealer damageDealer;
    private Animator animator;
    #endregion


    private void Awake()
    {
        SetReferences();
        SetValues();
    }

    public void ChooseAttack()
    {
        if (enemy.CurrentState != EnemyState.attack)
        {
            enemy.ChangeState(EnemyState.attack);

            int randomNumber = Random.Range(1, 3);

            if (randomNumber == 1)
            {
                currentAttackDamage = attack1Damage;
                StartCoroutine(Attack("attack1", attack1Duration + attack1Cooldown));
            }
            else if (randomNumber == 2)
            {
                currentAttackDamage = attack2Damage;
                StartCoroutine(Attack("attack2", attack2Duration + attack2Cooldown));
            }
        }
    }

    private IEnumerator Attack(string animation, float time)
    {
        animator.SetTrigger(animation);

        yield return new WaitForSeconds(time);

        if (enemy.CurrentState != EnemyState.stun)
        {
            enemy.ChangeState(EnemyState.idle);
        }
    }

    public void DealDamage()
    {
        damageDealer.AttackHitBox(currentAttackDamage);
    }

    private void SetReferences()
    {
        enemy = GetComponent<Enemy>();
        damageDealer = GetComponent<DamageDealer>();
        animator = GetComponent<Animator>();
    }

    private void SetValues()
    {
        attack1Damage = enemy.enemyStats.GetAttack1Damage;
        attack2Damage = enemy.enemyStats.GetAttack2Damage;
    }

}
