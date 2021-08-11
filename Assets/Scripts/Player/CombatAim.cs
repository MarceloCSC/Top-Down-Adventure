using System.Collections.Generic;
using UnityEngine;

public class CombatAim : MonoBehaviour
{

    [SerializeField] float snapToPlayer = 0.5f;
    [SerializeField] float snapToEnemy = 1.5f;

    private Vector2 aimPosition;
    private Transform target;

    #region Properties
    private bool IsNotSafe => SurveyArea();
    #endregion

    #region Cached references
    private List<Enemy> enemies;
    private Camera mainCamera;
    #endregion


    private void Start()
    {
        SetReferences();
        SurveyArea();
    }

    private void Update()
    {
        MoveAim();

        if (IsNotSafe)
        {
            TrackEnemies();
            TargetNearestEnemy();
        }
    }

    private void MoveAim()
    {
        aimPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        if (Vector2.Distance(transform.parent.Center(), aimPosition) > snapToPlayer)
        {
            transform.position = aimPosition;
        }
        else
        {
            transform.position = transform.parent.Center();
        }
    }

    private void TrackEnemies()
    {
        float distanceToTarget = Mathf.Infinity;

        foreach (Enemy enemy in enemies)
        {
            float currentDistance = Vector2.Distance(transform.position, enemy.transform.Center());

            if (currentDistance < distanceToTarget)
            {
                distanceToTarget = currentDistance;
                target = enemy.transform;
            }
        }
    }

    private void TargetNearestEnemy()
    {
        if (target != null)
        {
            if (Vector2.Distance(transform.position, target.Center()) < snapToEnemy)
            {
                transform.position = target.Center();
            }
        }
    }

    private bool SurveyArea()
    {
        if (FindObjectOfType<EnemyTracker>() != null)
        {
            enemies = FindObjectOfType<EnemyTracker>().activeEnemies;
            return true;
        }
        return false;
    }

    private void SetReferences()
    {
        mainCamera = Camera.main;
    }

}
