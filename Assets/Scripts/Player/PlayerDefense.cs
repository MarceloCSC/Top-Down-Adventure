using System.Collections;
using UnityEngine;

public class PlayerDefense : MonoBehaviour, IDefense
{

    private float stunTime;
    private float knockbackTime;
    private float knockbackForce;

    #region Properties
    public bool IsInvincible { get; set; }
    #endregion

    #region Cached references
    private PlayerController myController;
    private PlayerMovement myMovement;
    private Rigidbody2D myRigidbody;
    #endregion


    private void Awake()
    {
        SetReferences();
        SetValues();
    }

    public bool IsDefending()
    {
        return false;
    }

    public void Knockback(Transform other)
    {
        if (!IsInvincible)
        {
            Vector2 difference = transform.position - other.position;
            difference = difference.normalized * knockbackForce;
            myRigidbody.AddForce(difference, ForceMode2D.Impulse);

            StartCoroutine(GetStunned());
        }
    }

    private IEnumerator GetStunned()
    {
        IsInvincible = true;
        myMovement.CanMove = false;

        yield return new WaitForSeconds(knockbackTime);

        myRigidbody.velocity = Vector2.zero;

        yield return new WaitForSeconds(stunTime);

        IsInvincible = false;
        myMovement.CanMove = true;
    }

    private void SetReferences()
    {
        myController = GetComponent<PlayerController>();
        myMovement = GetComponent<PlayerMovement>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    private void SetValues()
    {
        stunTime = myController.myStats.GetStunTime;
        knockbackTime = myController.myStats.GetKnockbackTime;
        knockbackForce = myController.myStats.GetKnockbackForce;
    }

}
