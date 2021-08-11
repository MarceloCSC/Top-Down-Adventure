using System.Collections;
using UnityEngine;

public class Fireball : Projectile
{

    [SerializeField] float animationDuration = 0.44f;

    #region Cached references
    private Animator animator;
    #endregion


    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
    }

    protected override IEnumerator Collision()
    {
        damageDealer.AttackHitBox(damage);
        animator.SetTrigger("hit");

        yield return new WaitForSeconds(animationDuration);

        gameObject.SetActive(false);
    }

}
