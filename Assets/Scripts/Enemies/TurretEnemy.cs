using UnityEngine;

public class TurretEnemy : MonoBehaviour
{

    [SerializeField] float attackRadius = 7.0f;
    [SerializeField] float cooldown = 2.0f;
    [SerializeField] GameObject projectileType = default;
    [SerializeField] Transform spawner = default;

    private float timeToAttack;
    private bool canAttack;

    #region Properties
    public bool IsActive { get; set; }
    #endregion

    #region Cached references
    private Transform target;
    #endregion


    private void Awake()
    {
        SetReferences();
    }

    private void OnEnable()
    {
        IsActive = true;
        canAttack = true;
    }

    private void Update()
    {
        CheckTimer();

        if (IsActive && canAttack)
        {
            CheckDistance();
        }
    }

    private void CheckDistance()
    {
        if (Vector2.Distance(transform.position, target.Center()) <= attackRadius)
        {
            Attack();
        }
    }

    private void Attack()
    {
        Vector2 direction = (target.Center() - (Vector2)spawner.transform.position).normalized;

        GameObject projectile = ObjectPooler.Instance.SpawnObject(projectileType.name.ToString(), spawner.position, Quaternion.identity);
        projectile.GetComponent<Projectile>().Launch(direction);

        timeToAttack = Time.time + cooldown;
        canAttack = false;
    }

    private void CheckTimer()
    {
        if (Time.time > timeToAttack)
        {
            canAttack = true;
        }
    }

    private void SetReferences()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

}
