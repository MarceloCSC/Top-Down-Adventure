
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    private float walkSpeed;

    #region Cached references
    private Rigidbody2D enemyRigidbody;
    private Enemy enemy;
    #endregion


    private void Awake()
    {
        SetReferences();
        SetValues();
    }

    private void FixedUpdate()
    {
        if (!enemy.IsDead)
        {
            if (enemy.CurrentState == EnemyState.chase)
            {
                Move(enemy.Target);
            }
        }
        else
        {
            enemyRigidbody.velocity = Vector2.zero;
        }
    }

    public void Move(Transform target)
    {
        Vector2 destination = Vector2.MoveTowards(transform.position, target.position, walkSpeed * Time.fixedDeltaTime);

        enemyRigidbody.MovePosition(destination);

        FlipToTarget(target);
    }

    public void FlipToTarget(Transform target)
    {
        if (target.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (target.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }

    private void SetReferences()
    {
        enemy = GetComponent<Enemy>();
        enemyRigidbody = GetComponent<Rigidbody2D>();
    }

    private void SetValues()
    {
        walkSpeed = enemy.enemyStats.GetSpeed;
    }

}
