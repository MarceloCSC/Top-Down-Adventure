using System.Collections;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{

    [SerializeField] float hitBoxRadius = 1.2f;
    [SerializeField] Transform hitBox = default;
    [SerializeField] LayerMask canHurt = default;
    [SerializeField] bool toggleGizmos = true;


    public void AttackHitBox(float damage)
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(hitBox.position, hitBoxRadius, canHurt);

        foreach (Collider2D other in detectedObjects)
        {
            if (other.isTrigger)
            {
                Harm(damage, other);
                Knockback(other);
            }
        }
    }

    private static void Harm(float damage, Collider2D other)
    {
        if (other.GetComponent<IHealth>() != null)
        {
            other.GetComponent<IHealth>().DecreaseHealth(damage);
        }
    }

    private void Knockback(Collider2D other)
    {
        if (other.GetComponent<IDefense>() != null)
        {
            if (other.GetComponent<IDefense>().IsDefending())
            {
                return;
            }
            else
            {
                other.GetComponent<IDefense>().Knockback(transform);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (toggleGizmos)
        {
            Gizmos.DrawWireSphere(hitBox.position, hitBoxRadius);
        }
    }

}
