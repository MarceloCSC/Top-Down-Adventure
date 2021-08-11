
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{

    [SerializeField] Transform[] patrolPath = default;
    [SerializeField] float waypointRange = 0.2f;

    private int currentWaypoint;
    private Transform nextWaypoint;

    #region Properties
    public bool ShouldPatrol { get; private set; } = true;
    #endregion

    #region Cached references
    private Enemy enemy;
    private EnemyMovement enemyMovement;
    #endregion


    private void Awake()
    {
        SetReferences();
    }

    private void OnEnable()
    {
        CheckForWaypoints();
    }

    private void FixedUpdate()
    {
        if (ShouldPatrol && enemy.CurrentState == EnemyState.patrol)
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        if (Vector2.Distance(patrolPath[currentWaypoint].position, transform.position) > waypointRange)
        {
            enemyMovement.Move(patrolPath[currentWaypoint]);
        }
        else
        {
            ChangeDestination();
        }
    }

    private void ChangeDestination()
    {
        if (currentWaypoint == patrolPath.Length - 1)
        {
            currentWaypoint = 0;
            nextWaypoint = patrolPath[0];
        }
        else
        {
            currentWaypoint++;
            nextWaypoint = patrolPath[currentWaypoint];
        }
    }

    private void CheckForWaypoints()
    {
        if (patrolPath.Length <= 1)
        {
            Debug.LogWarning("No waypoints have been assigned.");
            ShouldPatrol = false;
        }

        for (int i = 0; i < patrolPath.Length; i++)
        {
            if (patrolPath[i] == null)
            {
                Debug.LogWarning("No waypoint has been assigned.");
                ShouldPatrol = false;
            }
        }
    }

    private void SetReferences()
    {
        enemy = GetComponent<Enemy>();
        enemyMovement = GetComponent<EnemyMovement>();
    }

}
