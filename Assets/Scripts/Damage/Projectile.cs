using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] bool isFriendly = false;
    [SerializeField] protected float speed = 5.0f;
    [SerializeField] protected float damage = 2.0f;
    [SerializeField] float timeToDespawn = 8.0f;

    #region Properties
    public bool IsFriendly { get => isFriendly; set => isFriendly = value; }
    #endregion

    #region Cached references
    protected Rigidbody2D projectileRigidbody;
    protected DamageDealer damageDealer;
    #endregion


    protected virtual void Awake()
    {
        damageDealer = GetComponent<DamageDealer>();
    }

    private void OnEnable()
    {
        StartCoroutine(AutoDespawn());
        projectileRigidbody = GetComponent<Rigidbody2D>();
    }

    public void Launch(Vector2 direction)
    {
        projectileRigidbody.velocity = direction * speed;
        transform.right = projectileRigidbody.velocity;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GetComponent<Collider2D>().enabled = false;
        StopCoroutine(AutoDespawn());
        StartCoroutine(Collision());
    }

    protected virtual IEnumerator Collision()
    {
        damageDealer.AttackHitBox(damage);

        yield return null;

        gameObject.SetActive(false);
    }

    private IEnumerator AutoDespawn()
    {
        yield return new WaitForSeconds(timeToDespawn);

        StartCoroutine(Collision());
    }

}
