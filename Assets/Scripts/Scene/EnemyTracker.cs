using System.Collections.Generic;
using UnityEngine;

public class EnemyTracker : MonoBehaviour
{

    public List<Enemy> activeEnemies;


    private void Awake()
    {
        activeEnemies = new List<Enemy>();
    }

    private void Start()
    {
        activeEnemies.AddRange(FindObjectsOfType<Enemy>());
    }

    public void Register(Enemy enemy)
    {
        activeEnemies.Add(enemy);
    }

    public void Deregister(Enemy enemy)
    {
        activeEnemies.Remove(enemy);
    }

}
