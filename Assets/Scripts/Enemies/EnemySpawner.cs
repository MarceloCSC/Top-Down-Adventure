using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public enum EnemyByTag
    {
        WeakSkeleton,
        FastSkeleton
    };


    [SerializeField] List<EnemyByTag> enemyToSpawn = default;
    //[SerializeField] Transform[] spawnPoints = default;
    [SerializeField] float timeBetweenSpawns = 5.0f;
    [SerializeField] bool isSpawning = false;

    #region Properties
    public bool IsSpawning { get => isSpawning; set => isSpawning = value; }
    #endregion


    private void Start()
    {
        if (isSpawning)
        {
            foreach (EnemyByTag enemy in enemyToSpawn)
            {
                StartCoroutine(Spawn(enemy));
            }
        }
    }

    private IEnumerator Spawn(EnemyByTag enemy)
    {
        EnemyPooler.Instance.SpawnObject(enemy.ToString(), transform.position, Quaternion.identity);

        yield return new WaitForSeconds(timeBetweenSpawns);

        StartCoroutine(Spawn(enemy));
    }

}
