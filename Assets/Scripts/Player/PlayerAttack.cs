using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    [SerializeField] AttackStats myStats = default;
    [SerializeField] Transform combatAim = default;
    [SerializeField] float maxDistance = 6.0f;
    [SerializeField] float minDistance = 2.5f;
    [SerializeField] float withinReach = 1.0f;

    private float dashSpeed;
    private Attack attack;
    private AttackType attackType = 0;
    private Dictionary<int, Attack> myAttacks;

    #region Properties
    public bool IsAttacking { get; private set; }
    #endregion

    #region Cached references
    private PlayerController myController;
    private PlayerMovement myMovement;
    private PlayerStamina myStamina;
    private DamageDealer damageDealer;
    private Animator animator;
    #endregion


    private void Awake()
    {
        SetReferences();
        SetValues();
    }

    private void Update()
    {
        if (myMovement.CanMove && !myController.IsDead)
        {
            if (Input.GetButtonDown("Attack"))
            {
                UpdateAnimation();

                if (Input.GetButton("Special 2") && IsInRange() && !myStamina.IsOutOfStamina)
                {
                    myStamina.SpendStamina();
                    StartCoroutine(DashTowards());
                }
                else
                {
                    Attack();
                }
            }
        }
    }

    private void Attack()
    {
        int currentType = (int)attackType;

        if (myAttacks.ContainsKey(currentType))
        {
            attack = myAttacks[currentType];
            StopAllCoroutines();
            StartCoroutine(AttackCoroutine());
        }
    }

    private IEnumerator AttackCoroutine()
    {
        IsAttacking = true;
        attackType++;
        animator.SetTrigger(attack.animation);

        yield return new WaitForSeconds(attack.duration * 0.65f);

        attackType++;

        yield return new WaitForSeconds(attack.duration * 0.6f);

        IsAttacking = false;
        attackType = 0;
    }

    private IEnumerator DashTowards()
    {
        while (Vector2.Distance(transform.Center(), combatAim.position) > withinReach)
        {
            transform.position = Vector2.MoveTowards(transform.position, combatAim.position, dashSpeed * Time.fixedDeltaTime);

            yield return null;
        }
        Attack();
    }

    private bool IsInRange()
    {
        return Vector2.Distance(transform.Center(), combatAim.position) >= minDistance
            && Vector2.Distance(transform.Center(), combatAim.position) <= maxDistance;
    }

    public void DealDamage(AnimationEvent animationEvent)
    {
        if (animationEvent.animatorClipInfo.weight > 0.5)
        {
            damageDealer.AttackHitBox(attack.damage);
        }
    }

    private Vector2 AimDirection()
    {
        Vector2 aim = (Vector2)combatAim.position - transform.Center();

        return aim.Direction();
    }

    private void UpdateAnimation()
    {
        if (Vector2.Distance(combatAim.position, transform.Center()) > 0.1f)
        {
            animator.SetFloat("moveX", AimDirection().x);
            animator.SetFloat("moveY", AimDirection().y);
        }
    }

    private void SetReferences()
    {
        myController = GetComponent<PlayerController>();
        myMovement = GetComponent<PlayerMovement>();
        myStamina = GetComponent<PlayerStamina>();
        damageDealer = GetComponent<DamageDealer>();
        animator = GetComponent<Animator>();
    }

    private void SetValues()
    {
        dashSpeed = myController.myStats.GetDashSpeed;
        myAttacks = myStats.BuildDictionary();
    }

}
